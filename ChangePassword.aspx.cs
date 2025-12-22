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

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (Session["UserID"] == null)
        {
            Response.Redirect("Default.aspx");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserID"].ToString() != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                SqlCommand cmd = new SqlCommand("GetPassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", Session["UserID"].ToString());
                conn.Open();
                string password = cmd.ExecuteScalar().ToString();
                conn.Close();
                if (password.Length > 0)
                {
                    if (password == txtOldPassword.Text.Trim())
                    {
                        SqlCommand cmd1 = new SqlCommand("UpdatePassword", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@UID", Session["UserID"].ToString());
                        cmd1.Parameters.AddWithValue("@password", txtNewPassword.Text.Trim());
                        conn.Open();
                        cmd1.ExecuteNonQuery();
                        conn.Close();
                        ShowMessage(divSuccess, "Success", "Password modified successfully.");
                        Session["Password"] = txtNewPassword.Text.Trim();
                        Response.Redirect("home.aspx");
                    }
                    else
                    {
                        ShowMessage(diverror, "Error", "Old and new passwords are not matching");
                    }
                }
                else
                {
                    ShowMessage(diverror, "Error", "Invalid User ID/Password");                    
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void LogOff_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Session["Password"] = null;
        Session.Abandon();
        Response.Redirect("Default.aspx");

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

        divSuccess.InnerHtml = "";
        divSuccess.Visible = false;
        diverror.InnerHtml = "";
        diverror.Visible = false;
    }
}
