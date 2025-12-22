using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using INTModel;

public partial class adminExtraLinks : System.Web.UI.Page
{
    INTModelContainer objEntities = new INTModelContainer();
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (!Page.IsPostBack)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1")
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            ListData();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strDetails = txtDetails.Text.Trim();
            string strTitle = txtTitle.Text.ToString();
            string strLinkName = txtName.Text.ToString();

            if (ViewState["ID"] != null)
            {
                objEntities.INT_Update_ExtraLinks(System.Convert.ToInt32(ViewState["ID"].ToString()), strLinkName, strTitle, strDetails);
                ShowMessage(divSuccess, "Success", "Updated successfully!");
            }
            else
            {
                objEntities.INT_Insert_ExtraLinks(strLinkName, strTitle, strDetails, Session["UserID"].ToString());
                ShowMessage(divSuccess, "Success", "Added successfully!");
            }
            FillCache();            
            ListData();
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while adding data!");
        }
    }
    
    private void ListData()
    {
        try
        {
            pnlList.Visible = true;
            List<INT_Get_All_ExtraLinks_Result> objListAll = objEntities.INT_Get_All_ExtraLinks().ToList();
            if (objListAll.Count > 0)
            {
                grid1.DataSource = objListAll;
                grid1.DataBind();
                grid1.Visible = true;
            }
            else
            {
                if (Cache["ExtraLinks"] != null)
                {
                    Cache.Remove("ExtraLinks");
                }
                grid1.Visible = false;
                //ShowMessage(divAlert, "Note", "No data found!");
            }
        }
        catch
        {
            ShowMessage(diverror, "Error", "Error while displaying data!");
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
        divListSuccess.InnerHtml = "";
        divListSuccess.Visible = false;
        divListError.InnerHtml = "";
        divListError.Visible = false;
    }

    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {

        if (e.CommandName == "cmEdit")
        {
            try
            {                            
                List<INT_Get_ExtraLinks_ByID_Result> objListAll = objEntities.INT_Get_ExtraLinks_ByID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
                if (objListAll.Count > 0)
                {
                    string LinkName = objListAll[0].Page_Link_Name.ToString();
                    string Content = objListAll[0].Page_Content.ToString();
                    string Header = objListAll[0].Page_Header.ToString();
                    ViewState["ID"] = e.CommandArgument.ToString();
                    txtDetails.Text = Content;
                    txtName.Text = LinkName;
                    txtTitle.Text = Header;
                    btnSubmit.Text = "Update Details";
                    txtName.Focus();
                }
            }
            catch
            {
                ShowMessage(divListError, "Error", "Error while editing data!");
            }
        }
        else if (e.CommandName == "cmDelete")
        {
            try
            {
                objEntities.INT_Delete_ExtraLinks_ByID(System.Convert.ToInt32(e.CommandArgument.ToString()));
                ListData();
                ShowMessage(divListSuccess, "Success", "Deleted successfully!");
                FillCache();
            }
            catch
            {
                ShowMessage(divListError, "Error", "Error while deleting data!");
            }
        }
    }
 
    private void FillCache()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_ExtraLinks_ForHome";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["ExtraLinks"] != null)
                            {
                                Cache.Remove("ExtraLinks");
                            }
                            Cache.Insert("ExtraLinks", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while storing data on Cache!");
        }
    }

}