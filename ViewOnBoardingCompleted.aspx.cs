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
using System.Globalization;

public partial class ViewOnBoardingCompleted : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    CommonClass objCommonClass = new CommonClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();    
        if (Request.QueryString["msg"] != null)
        {
            if (Request.QueryString["msg"].ToString() == "s")
            {
                ShowMessage(divSuccess, "Success:", "Details updated successfully.");
            }
            else if (Request.QueryString["msg"].ToString() == "f")
            {
                ShowMessage(diverror, "Error:", "Error while updating the details.");
            }            
        }
        if (Session["selectedRole"] != null)
        {
            if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "5")
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
            bindyearMonth();
            BindUserList();
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

    private void bindyearMonth()
    {
        ListItem item1;

        ddlSYear.Items.Clear();
        ddlSMonth.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            item1 = new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), i.ToString());
            ddlSMonth.Items.Add(item1);
        }

        ListItem item2;
        for (int j = 2018; j <= DateTime.Now.Year; j++)
        {
            item2 = new ListItem(j.ToString(), j.ToString());
            ddlSYear.Items.Add(item2);
        }

        ddlSMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

        ddlSYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }

    private void BindUserList()
    {
        try
        {

            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Onboarding_GetUserRegOnboarding_Completed", conn);
            cmd.Parameters.AddWithValue("@SearchString", txtEmpName.Text.ToString());
            cmd.Parameters.AddWithValue("@Year", ddlSYear.SelectedItem.Value.ToString());
            cmd.Parameters.AddWithValue("@Month", ddlSMonth.SelectedItem.Value.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            
            if (dt.Rows.Count > 0)
            {
                grid1.DataSource = dt;
                grid1.DataBind();
                grid1.Visible = true;
                ViewState["ObjUserDetails"] = dt;
            }
            else
            {
                grid1.Visible = false;
                ViewState["ObjUserDetails"] = null;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Error While loading data. Please try later.");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindUserList();
    }
    /*
    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            try
            {                
                string Id = e.CommandArgument.ToString();  
                Session["EditId"] =  Id;
                Response.Redirect("NewUserOnBoarding.aspx", true);
            }
            catch
            {
            }
        }      
    }
    */

}