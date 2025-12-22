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

public partial class login1 : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd1 = null;

    protected void Page_Load(object sender, EventArgs e)
    {

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
        diverror.InnerHtml = "";
        diverror.Visible = false;
    }
    protected void ibtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string strInvalidUserMsg = "";
            strInvalidUserMsg = "<font color='red'>Invalid User ID/Password</font>";
            Session["KBUserID"] = null;
            string strLoginID = "", strPassword = "";
            strLoginID = txtUserID.Text.ToString();
            strPassword = txtPassword.Text.ToString();
            if (strLoginID.Length > 0 && strPassword.Length > 0)
            {
                //HARDCODED For KB
                if ((strLoginID == "km" && strPassword == "km@2015") || (strLoginID == "kmsearch" && strPassword == "km@2015"))
                {
                    Session["KBUserID"] = strLoginID.ToString();
                    Session["KBPassword"] = strPassword.ToString();
                    Response.Redirect("Search.aspx", false);
                }
                else
                {
                    ShowMessage(diverror, "Error", strInvalidUserMsg);                    
                }
            }
            else
            {
                ShowMessage(diverror, "Error", strInvalidUserMsg);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while connecting to database. Please try later.");
        }
    }
}