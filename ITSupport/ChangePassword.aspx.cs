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
        if (Session["UserID"] != null)
        {
            PanelUser.Visible = true;
        }
        else
        {
            Response.Redirect("login.aspx");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserID"].ToString() != null)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
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
                        //lblError.Text = "Password modified successfully.";
                        //lblError.ForeColor = System.Drawing.Color.Green;
                        Session["Password"] = txtNewPassword.Text.Trim();
                        Response.Redirect("home.aspx");
                    }
                    else
                    {
                        lblError.Text = "Old Password not matching";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblError.Text = "Invalid User ID/Password";
                    lblError.ForeColor = System.Drawing.Color.Red;
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
        Response.Redirect("login.aspx");

    }

}
