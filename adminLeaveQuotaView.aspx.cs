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

public partial class adminLeaveQuotaView : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    CommonClass objCommonClass = new CommonClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        QuotaEdit.Visible = false;
        if (Session["selectedRole"] != null)
        {
            if (Session["selectedRole"].ToString() != "1")
            {
                Response.End();
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }
        if (Session["UserID"] == null)
        {
            Response.Write("<BR><BR><h3>You do not have access to view this page. Please login as administrator and check again.</h3>");
            Response.End();
        }
        if (!IsPostBack)
        {
            //BindQuotaList();
            //if (Session["selectedRole"].ToString() != "1")
            //{
            //    Response.Redirect("home.aspx");
            //}
        }
    }



    private void ShowMessage(HtmlGenericControl div1, string spanText, string Message)
    {
        clearMessages();
        div1.InnerHtml = "<span>" + spanText + "</span>";
        div1.InnerHtml += ": ";
        div1.InnerHtml += Message;
        div1.Visible = true;
    }
    private void clearMessages()
    {
        divAlert.InnerHtml = "";
        divAlert.Visible = false;
        divSuccess.InnerHtml = "";
        divSuccess.Visible = false;
        diverror.InnerHtml = "";
        diverror.Visible = false;
        divNotice.InnerHtml = "";
        divNotice.Visible = false;
    }


    private void BindQuotaList()
    {
        SqlConnection conn = null;
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Leave_ViewAllQuota", conn);
            cmd.Parameters.AddWithValue("@EName", txtEmpName.Text.ToString());
            cmd.Parameters.AddWithValue("@Type", ddlType.SelectedItem.Value.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                grid1.DataSource = dt;
                grid1.DataBind();
                grid1.Visible = true;
                ViewState["ObjQuotaDetails"] = dt;
            }
            else
            {
                grid1.Visible = false;
                ViewState["ObjQuotaDetails"] = null;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Error While loading data. Please try later.");
        }
        finally
        {
            conn.Close();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Session["SelectedName"] = txtEmpName.Text.ToString();
        BindQuotaList();
        QuotaEdit.Visible = false;
    }

    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            try
            {
                txtQuota.Text = "";
                string Summary = e.CommandArgument.ToString();  
                string[] summaryAr = Summary.Split('|');
                ViewState["EditQuotaID"] = summaryAr[0];
                ViewState["EditEID"] = summaryAr[1];
                ViewState["EditEName"] = summaryAr[2];
                ViewState["EditType"] = summaryAr[3];
                txtQuota.Text = summaryAr[4].ToString().Trim();
                divSummary.InnerHtml = "<BR><b>&nbsp;&nbsp;Employee ID: </b>" + ViewState["EditEID"] + "<b>&nbsp;&nbsp;Employee Name: </b>" + ViewState["EditEName"] + "<b>&nbsp;&nbsp;Leave Type: </b>" + ViewState["EditType"];

                QuotaEdit.Visible = true;
            }
            catch
            {
            }
        }      
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ObjQuotaDetails"] != null)
            {
                //here session value is stored before bind data on gridview
                DataTable dt = ViewState["ObjQuotaDetails"] as DataTable;
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=QuotaList_" + CommonClass.GetDateTimeIST().ToShortDateString() + ".xls");
                Response.ContentType = "application/ms-excel";
                string tab = "";
                int col = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (col > 1)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = "\t";
                    }
                    col++;
                }
                Response.Write("\n");

                int i;
                string Content = "";
                foreach (DataRow dr in dt.Rows)
                {
                    col = 0;
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        if (col > 1)
                        {
                            Content = "";
                            Content = dr[i].ToString();
                            Content = Content.Replace("\r\n", " ");
                            Content = Content.Replace("  ", " ");
                            Response.Write(tab + Content);
                            tab = "\t";
                        }
                        col++;                        
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        catch
        {

        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("Leave_UpdateQuotaByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ViewState["EditQuotaID"].ToString());
            cmd.Parameters.AddWithValue("@PersNo", ViewState["EditEID"].ToString());
            cmd.Parameters.AddWithValue("@QuotaValue", txtQuota.Text.Trim().ToString());
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            BindQuotaList();
            QuotaEdit.Visible = false;
            ShowMessage(divSuccess, "Success: ", "Quota value updated. Please verify.");

        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Could not update the details. Please try again later.");
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
}