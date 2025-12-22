using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

public partial class _Home : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    CommonClass objCommonClass = new CommonClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strTitle = "&nbsp;&nbsp;&nbsp;&nbsp;Elite IT SupportDesk&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //Page.Title = strTitle;
        if (Session["masterChecking"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (Session["LogonUserFullName"] != null)
        {
            divWelcome.InnerHtml = "&nbsp;&nbsp;Welcome: " + Session["LogonUserFullName"].ToString();
        }
        // Submit.Visible = false;  
        //hardcoded. To be removed 
        //Session["UserID"] = "jay";
        Session["MyTimeZoneID"] = "1";     
        //Session["AdminLinks"] = "1";
        divChangehdesk.Visible = false;
        divHelpdeskArea.Visible = false;
        if (Session["UserID"] != null)
        {
            PanelUser.Visible = true;
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {           
            ViewState["attachedfiles"] = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());

            cmd1 = new SqlCommand("GetALLActiveHelpDesks", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.HasRows)
            {
                ddHelpDesk.DataSource = dr;
                ddHelpDesk.DataTextField = "HLD_Name";
                ddHelpDesk.DataValueField = "HLP_ID";
                ddHelpDesk.DataBind();
                //ListItem Listitem0 = new ListItem();
                //Listitem0.Value = "0";
                //Listitem0.Text = "----Select----";
                //ddHelpDesk.Items.Insert(0, Listitem0);
                if (Session["AdminLinks"].ToString() == "1")
                {

                    admChnageHelpdesk.Visible = true;
                    cmd2 = new SqlCommand("GetALLActiveHelpDesks", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr2 = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

                    if (dr2.HasRows)
                    {
                        ddChangeHelpDesk.DataSource = dr2;
                        ddChangeHelpDesk.DataTextField = "HLD_Name";
                        ddChangeHelpDesk.DataValueField = "HLP_ID";
                        ddChangeHelpDesk.DataBind();
                        if (Session["HelpDeskID"] != null)
                        {
                            ddChangeHelpDesk.SelectedValue = Session["HelpDeskID"].ToString();
                        }
                        else
                        {
                            Session["HelpDeskID"] = ddChangeHelpDesk.SelectedItem.Value;
                            Session["HelpDeskName"] = ddChangeHelpDesk.SelectedItem.Text.ToString().Trim();
                        }
                    }
                }
                else
                {
                    admChnageHelpdesk.Visible = false;
                }
            }
            cmd1.Parameters.Clear();
            cmd1.Dispose();
            con.Close();

            OpenRequeststbl.Visible = true;
            RequestHistorytbl.Visible = false;
            imageviewtbl.Visible = false;        
        }


    }

    protected void Submit_Click(object sender, EventArgs e)
    {

        int RequestID1 = 0;

        Page.Validate("Group1");
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            cmd = new SqlCommand("InsertRequestsNew", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RequestSubject", Subject.Text.ToString());
            cmd.Parameters.AddWithValue("@RequestDescription", Probelm.Text.ToString());
            cmd.Parameters.AddWithValue("@RegisteredBy", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@RequestHelpDeskID", ddHelpDesk.SelectedValue.ToString());

            //if (ddlWorkGroupCategory.SelectedValue!="0" && Convert.ToBoolean(ViewState["CategoryEnabled"].ToString()) == true && ResolverMapped(Convert.ToInt32(ddlWorkGroupCategory.SelectedValue.ToString())))
            //{
            //    DateTime ResolutionRequiredbydate = GetResolutionDatetime();
            //    cmd.Parameters.AddWithValue("@ExpectedResolutionDtTime", ResolutionRequiredbydate);
            //    cmd.Parameters.AddWithValue("@RequestType1ID", ddlWorkGroupCategory.SelectedValue);
            //    cmd.Parameters.AddWithValue("@Severity", Convert.ToInt32(ViewState["Severity"].ToString()));
            //    cmd.Parameters.AddWithValue("@HelpDeskGroup", Convert.ToInt32(ViewState["HelpDeskGroup"].ToString()));

            //}
             con.Open();
            cmd.Parameters.Add("@RequestID", SqlDbType.Int); // for update not required 
            cmd.Parameters["@RequestID"].Direction = ParameterDirection.Output; // for update not required 

            cmd.ExecuteNonQuery();

            RequestID1 = (int)cmd.Parameters["@RequestID"].Value; // for update not required 

            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            if (RequestID1 > 0)
            {
                if (ViewState["attachedfiles"] != null)
                {
                    UploadfilestoRequestfilestable(RequestID1.ToString());
                }
                CommonClass.sendmails(RequestID1.ToString(), "1", Session["UserID"].ToString(), "", "");
            }
            string Msg = "Supportdesk Request ID - " + RequestID1 + " created. Your request will be assigned to a support staff soon. You can view your request details below.";
            Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location=\"Home.aspx?viewst=1\";\n</script>\n");
        }
        catch(Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();
            lblError.Text = "Error While saving the data.";
            lblError.Text += ex.Message.ToString();
            lblError.Style.Add("Color", "Red");
        }

    }

    private DateTime GetResolutionDatetime()
    {
        try
        {
            int NoofWorkingDays = 0;
            double Startinghour = 0;
            double Endinghour = 0;
            bool Holidaysincluded = false;
            int HolidayLocation = 0;
            double ResponseTime = 0;
            double ResolutionTime = 0;
            string WorkGroupTimeZone = "";

            int SeverityId = 0;
            int GroupID = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            DataSet ds = new DataSet();
           SqlCommand cmd = new SqlCommand("GetDefaultSeverity", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RequestType1ID", ddlWorkGroupCategory.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            if (ds.Tables[1].Rows.Count > 0)
            {
                SeverityId = Convert.ToInt32(ds.Tables[1].Rows[0]["SeverityID"].ToString());
                GroupID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                ViewState["Severity"] = SeverityId;
                ViewState["HelpDeskGroup"] = GroupID;
            }
            
            cmd = new SqlCommand("GetDetailsForSLA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SeverityID",SeverityId );
            cmd.Parameters.AddWithValue("@GroupID", GroupID);
            con.Open();
           SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.Read())
            {
                NoofWorkingDays = Convert.ToInt32(dr["NoofWorkingDays"].ToString());
                Startinghour = Convert.ToDouble(dr["Startinghour"].ToString());
                Endinghour = Convert.ToDouble(dr["Endinghour"].ToString());
                Holidaysincluded = Convert.ToBoolean(dr["Holidaysincluded"].ToString());
                HolidayLocation = Convert.ToInt32(dr["HolidayLocation"].ToString());
                ResponseTime = Convert.ToDouble(dr["SResponseTime"].ToString());
                ResolutionTime = Convert.ToDouble(dr["SResolutionTime"].ToString());
                WorkGroupTimeZone = "";

            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            DateTime ResolutionRequiredbydate = CommonClass.GetRRByTime(CommonClass.GetDateTimeIST().ToString(), NoofWorkingDays, Startinghour,
                Endinghour, Holidaysincluded, HolidayLocation, ResolutionTime, WorkGroupTimeZone);
            return ResolutionRequiredbydate;
        }
        catch (Exception)
        {
            return CommonClass.GetDateTimeIST();
            throw;
        }
    }

    protected void LogRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
        OpenRequeststbl.Visible = false;
        RequestHistorytbl.Visible = false;

    }

    protected void ViewHistory_Click(object sender, EventArgs e)
    {
        try
        {

            RequestHistorytbl.Visible = true;
            OpenRequeststbl.Visible = false;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error in History view.";
            lblError.Text += ex.Message.ToString();
            lblError.Style.Add("Color", "Red");
        }
    }

  
    protected void UploadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string[] filetype;
            filetype = FileUpload1.FileName.Split('.');
            int fileLen = FileUpload1.PostedFile.ContentLength;

            if (fileLen <= 0)
            {
                OpenRequeststbl.Visible = true;
                return;
            }

            if (fileLen > 1048576)
            {
                string Msg = "Max. allowed file size is 1 MB. You may convert the image file to Gif or JPEG and upload it again";
                Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');\n</script>\n");
                return;
            }

            byte[] fileBinaryData = new byte[fileLen];

            FileUpload1.PostedFile.InputStream.Read(fileBinaryData, 0, fileLen);
            string FileName = FileUpload1.FileName.ToString();
            string Filecontenttype = FileUpload1.PostedFile.ContentType.ToString();
            string Filesize = FileUpload1.PostedFile.ContentLength.ToString();

            if (UploadFile.CommandArgument.ToString() != "")
            {
                Uploadfilestodatabase(FileName, Filecontenttype, fileBinaryData, Filesize, UploadFile.CommandArgument.ToString());
                //imagtablevisibletrue(UploadFile.CommandArgument.ToString()); // For binding the attached files
            }
            else
            {
                storefilesintempdataset(FileName, Filecontenttype, fileBinaryData, Filesize);
            }
        }
        catch(Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();
            lblError.Text = "Error in uploading the file.";
            lblError.Text += ex.Message.ToString();
            lblError.Style.Add("Color", "Red");
        }


    }
   
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowIndex > -1)
    //    {
    //        //string AssingedTo = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString());
    //        //string RequestID = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString());
    //        //HyperLink lb = (HyperLink)e.Row.FindControl("HyperLink1");
    //        //lb.Text = RequestID;
    //        //string Urladdress = "ViewRequest.aspx?ReqID=" + RequestID + "&Requestaction=userview";
    //        ////lb.Attributes.Add("OnClick", "window.open('" + Urladdress + "','','top=0,left=0,scrollbars=yes,menubar=no,height=700,width=1050,resizable=yes,toolbar=no,location=no,status=no');");
    //        //System.Web.UI.AttributeCollection aCol = newWin.Attributes;
    //        //aCol.Add("src", Urladdress);
    //        //lb.Attributes.Add("OnClick", "javascript:OpenWindow();");
    //    }
    //}

    //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowIndex > -1)
    //    {
    //        string AssingedTo = Convert.ToString(GridView2.DataKeys[e.Row.RowIndex].Values[0].ToString());
    //        string RequestID = Convert.ToString(GridView2.DataKeys[e.Row.RowIndex].Values[0].ToString());
    //        HyperLink lb = (HyperLink)e.Row.FindControl("ViewHistoryHyperLink");
    //        lb.Text = RequestID;
    //        string Urladdress = "ViewRequest.aspx?ReqID=" + RequestID + "&Requestaction=userview";
    //        //lb.Attributes.Add("OnClick", "window.open('" + Urladdress + "','','top=0,left=0,scrollbars=yes,menubar=no,height=650,width=1000,resizable=yes,toolbar=no,location=no,status=no');");
    //        System.Web.UI.AttributeCollection aCol = newWin.Attributes;
    //        aCol.Add("src", Urladdress);
    //        lb.Attributes.Add("OnClick", "javascript:OpenWindow();");
    //    }
    //}

    protected void btnClosefilewindow_Click(object sender, ImageClickEventArgs e)
    {
        imageviewtbl.Visible = false;
    }

    protected void LogOff_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Session["Password"] = null;
        Session.Abandon();
        Session.RemoveAll();
        Response.Redirect("login.aspx");
    }

    protected void ddChangeHelpDesk_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddChangeHelpDesk.SelectedIndex != null)
        {
            Session["HelpDeskID"] = ddChangeHelpDesk.SelectedItem.Value;
            Session["HelpDeskName"] = ddChangeHelpDesk.SelectedItem.Text.ToString().Trim();
            Response.Redirect("Home.aspx");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            string arg = e.CommandArgument.ToString();
            if (e.CommandName == "View")
            {
                string ReqID = arg.ToString();
                string Urladdress = "ViewRequest.aspx?ReqID=" + ReqID + "&Requestaction=userview";
                System.Web.UI.AttributeCollection aCol = newWin.Attributes;
                aCol.Add("src", Urladdress);
                Page.Controls.Add(new LiteralControl("<script language='javascript'>OpenWindow();</script>"));
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            string arg = e.CommandArgument.ToString();
            if (e.CommandName == "View")
            {
                string ReqID = arg.ToString();
                string Urladdress = "ViewRequest.aspx?ReqID=" + ReqID + "&Requestaction=userview";
                System.Web.UI.AttributeCollection aCol = newWin.Attributes;
                aCol.Add("src", Urladdress);
                //lb.Attributes.Add("OnClick", "javascript:OpenWindow();");                
                Page.Controls.Add(new LiteralControl("<script language='javascript'>OpenWindow();</script>"));
            }
        }
        catch (Exception ex)
        {

        }
    }

    #region upload files

    protected void lnkAttachFiles_Click(object sender, EventArgs e)
    {
        UploadFile.CommandArgument = "";
        imageviewtbl.Visible = true;
        GridforDataTable.Visible = true;
    }

    protected void GridforDataTable_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Deletefile")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            DataTable StoreTable = new DataTable();
            StoreTable = ((DataTable)ViewState["attachedfiles"]);
            string squery = "FileID =" + i.ToString();
            DataRow[] Findtodeleterow = StoreTable.Select(squery);

            DataRow dr = (DataRow)Findtodeleterow.GetValue(0);

            StoreTable.Rows.Remove(dr);

            ViewState["attachedfiles"] = StoreTable;

            GridforDataTable.DataSource = StoreTable;
            GridforDataTable.DataBind();

            OpenRequeststbl.Visible = true;
            imageviewtbl.Visible = true;
            UploadFile.Focus();
        }
    }

    protected void storefilesintempdataset(string FileName, string Filecontenttype, byte[] fileBinaryData, string Filesize)
    {
        if (ViewState["attachedfiles"] == null)
        {

            //created the datatable and put in the view
            DataTable StoreTable = new DataTable();
            DataColumn StoredColumn;

            StoredColumn = new DataColumn();
            StoredColumn.DataType = Type.GetType("System.Int32");
            StoredColumn.ColumnName = "FileID";
            StoreTable.Columns.Add(StoredColumn);

            StoredColumn = new DataColumn();
            StoredColumn.DataType = Type.GetType("System.String");
            StoredColumn.ColumnName = "FileName1";
            StoreTable.Columns.Add(StoredColumn);

            StoredColumn = new DataColumn();
            StoredColumn.DataType = Type.GetType("System.String");
            StoredColumn.ColumnName = "Filecontenttype";
            StoreTable.Columns.Add(StoredColumn);

            StoredColumn = new DataColumn();
            StoredColumn.DataType = Type.GetType("System.String");
            StoredColumn.ColumnName = "FileSize1";
            StoreTable.Columns.Add(StoredColumn);

            StoredColumn = new DataColumn();
            StoredColumn.DataType = Type.GetType("System.Byte[]");
            StoredColumn.ColumnName = "fileBinaryData";
            StoreTable.Columns.Add(StoredColumn);

            DataRow row;
            row = StoreTable.NewRow();
            row["FileID"] = 1;
            row["FileName1"] = FileName;
            row["Filecontenttype"] = Filecontenttype;
            row["FileSize1"] = Filesize;
            row["fileBinaryData"] = fileBinaryData;
            StoreTable.Rows.Add(row);

            ViewState["attachfileno"] = "1";
            ViewState["attachedfiles"] = StoreTable;

            GridforDataTable.DataSource = ((DataTable)ViewState["attachedfiles"]).DefaultView;
            GridforDataTable.DataBind();

            OpenRequeststbl.Visible = true;
            imageviewtbl.Visible = true;
            UploadFile.Focus();
        }
        else
        {
            DataTable StoreTable = new DataTable();
            StoreTable = ((DataTable)ViewState["attachedfiles"]);
            DataRow row;
            row = StoreTable.NewRow();
            row["FileID"] = Convert.ToInt32(ViewState["attachfileno"].ToString()) + 1;
            row["FileName1"] = FileName;
            row["Filecontenttype"] = Filecontenttype;
            row["FileSize1"] = Filesize;
            row["fileBinaryData"] = fileBinaryData;
            StoreTable.Rows.Add(row);

            ViewState["attachfileno"] = Convert.ToInt32(ViewState["attachfileno"].ToString()) + 1;
            ViewState["attachedfiles"] = StoreTable;

            GridforDataTable.DataSource = StoreTable;
            GridforDataTable.DataBind();

            OpenRequeststbl.Visible = true;
            imageviewtbl.Visible = true;
            UploadFile.Focus();
        }
    }

    protected void Uploadfilestodatabase(string FileName, string Filecontenttype, byte[] fileBinaryData, string Filesize, string Requestid)
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        cmd = new SqlCommand("Insertfiles", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();

        cmd.Parameters.AddWithValue("@Filename1", FileName);
        cmd.Parameters.AddWithValue("@Filetype1", Filecontenttype);
        cmd.Parameters.AddWithValue("@Filedata1", fileBinaryData);
        cmd.Parameters.AddWithValue("@Filesize1", Filesize);
        cmd.Parameters.AddWithValue("@RequestID", Requestid);

        cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    protected void UploadfilestoRequestfilestable(string Requestid)
    {
        DataTable StoreTable = new DataTable();
        StoreTable = ((DataTable)ViewState["attachedfiles"]);

        foreach (DataRow theRow in StoreTable.Rows)
        {
            Uploadfilestodatabase(theRow["FileName1"].ToString(), theRow["Filecontenttype"].ToString(), (byte[])theRow["fileBinaryData"], theRow["FileSize1"].ToString(), Requestid);
        }
    }

    #endregion

}
