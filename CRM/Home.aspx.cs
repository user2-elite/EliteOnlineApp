using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.UI.HtmlControls;

public partial class Home : System.Web.UI.Page
{
    DBOperation myDBOperation = new DBOperation();
    DataSet allComplaintsDS = new DataSet();   

    protected void Page_Load(object sender, EventArgs e)
    {
        string strTitle = "&nbsp;&nbsp;&nbsp;&nbsp;CRM";
        Page.Title = strTitle;
        if (Session["CRMUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (Session["LogonName"] != null)
        {
            divWelcome.InnerHtml = "&nbsp;&nbsp;Welcome " + Session["LogonName"].ToString();
            divWelcome.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            divWelcome.InnerHtml += "&nbsp;&nbsp;LoggedIn User Role: " + Session["CRMselectedRoleName"].ToString();
            divWelcome.InnerHtml += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            divWelcome.InnerHtml += "&nbsp;&nbsp;Company Unit: " + Session["UnitName"].ToString();
        }
        clearMessages();
        ShowMessage(divNotice, "Information: ", "All your pending action Item can be seen in this screen.");

        //ShowMessage(divAlert, "Warning", "There are no pending item in your action work list.");

        pnlMyWorlist.Visible = true;
        int returnVal = myDBOperation.BindWorklist(gvComplaint, Session["CRMselectedRole"].ToString(), Session["CRMUserID"].ToString());
        if (returnVal > 0)
        {
            lblNumRows.Text = "You have <font color='red'><b>" + returnVal  + "</b></font> action(s) pending in your worklist";
        }
        else
        {
            lblNumRows.Text = "You have <b>0</b> action(s) pending in your worklist";
        }


    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string ComplaintID = e.CommandArgument.ToString();
            if (e.CommandName == "EditRow")
            {
                Session["ComplaintID"] = ComplaintID.Trim();
                string Urladdress = "ComplaintRegister.aspx"; //?arg=" + ComplaintID.ToString();
                Response.Redirect(Urladdress);
            }
        }
        catch (Exception ex)
        {

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
}