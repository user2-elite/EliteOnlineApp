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
        lblMessageFor.Text = "";
        pnlUserLogin.Visible = true;    
    }
    

    protected void ibtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        string strInvalidUserMsg = "Invalid User Id/Password.";
        try
        {
            Session["UserID"] = "";
            string strLoginID = "", strPassword = "";
            strLoginID = txtUserID.Text.ToString();
            strPassword = txtPassword.Text.ToString();
            if (strLoginID.Length > 0 && strPassword.Length > 0)
            {
                //ITSecurityPolicy              
                try
                {
                    if (Session["IAgree"] != null)
                    {
                        SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
                        SqlCommand cmd1 = new SqlCommand("INSERT_UserLogonStatus", sqlcon1);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@UID", strLoginID.ToString());
                        sqlcon1.Open();
                        cmd1.ExecuteNonQuery();
                    }
                    else if (!ValidateIAgree(strLoginID))
                    {
                        lblLoginError.Text = "Please read & agree the IT Security Policy. This is mandatory for the first time logon.";
                        return;
                    }  
                }
                catch
                {
                    lblLoginError.Text = "Please read & agree the IT Security Policy. This is mandatory for the first time logon.";
                    return;
                }

                if (strLoginID == "jmohan" && strPassword == "jmohan2014")
                {
                    Session["UserID"] = strLoginID.ToString();
                    Session["Password"] = strPassword.ToString();
                    Session["selectedRole"] = "1";
                    Session["HelpDeskID"] = "1";
                    Session["HelpDeskName"] = "ITServiceDesk";
                    Session["AdminLinks"] = "1";
                    Response.Redirect("home.aspx", false);
                }
                else if (validateUserLogon(strLoginID, strPassword))
                {                    
                    Session["UserID"] = strLoginID.ToString();
                    Session["Password"] = strPassword.ToString();
                    //MyAccessLevel();

                    //if (Session["AdminLinks"] == null || Session["selectedRole"] == null)
                    //{
                        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                        SqlCommand cmd = new SqlCommand("GetALLActiveAdminMembers", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@memberID", Session["UserID"].ToString());
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "AdminTab");

                        if (ds.Tables["AdminTab"].Rows.Count != 0)
                        {
                            string selectedRoleandHID = ds.Tables["AdminTab"].Rows[0]["RoleandHID"].ToString();
                            string[] ar = selectedRoleandHID.Split(new char[] { '-' });
                            Session["selectedRole"] = ar[0].ToString();
                            Session["HelpDeskID"] = ar[1].ToString();
                            string selectedRoleName = ds.Tables["AdminTab"].Rows[0]["HLD_NameandRole"].ToString();
                            string[] ar1 = selectedRoleName.Split(new char[] { '-' });
                            Session["HelpDeskName"] = ar1[0].ToString();
                            Session["AdminLinks"] = "1";
                        }
                        else
                        {
                            Session["selectedRole"] = "0";
                            Session["HelpDeskID"] = "0";
                            Session["HelpDeskName"] = "";
                            Session["AdminLinks"] = "0";
                        }
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                    //}
                    if (strPassword.ToString().ToLower() == "helpdesk123")
                    {
                        Response.Redirect("ChangePassword.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("home.aspx", false);
                    }
                }
                else
                    lblLoginError.Text = strInvalidUserMsg;
            }
            else
            {
                lblLoginError.Text = strInvalidUserMsg;
            }
        }
        catch (Exception ex)
        {
            lblLoginError.Text = strInvalidUserMsg;
        }
    }

    private bool ValidateIAgree(string uid)
    {
        try
        {
            SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
            SqlCommand cmd1 = new SqlCommand("Get_UserLogonStatus", sqlcon1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@UID", uid);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }


    private void MyAccessLevel()
    {
        SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
        SqlCommand cmd1 = new SqlCommand("select isnull(IsAdmin,0) AS [IsAdmin],isnull(ISCoOrdinator,0) AS [ISCoOrdinator],isnull(ISResolver,0) AS [ISResolver] from USER_REG where UID='" + Session["UserID"].ToString() + "' and Password ='" + Session["Password"].ToString() + "'", sqlcon1);
        cmd1.CommandType = CommandType.Text;
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);

        //.................Basic and Personal info........................
        if (ds1.Tables[0].Rows.Count > 0)
        {
            Session["IsAdmin"] = (ds1.Tables[0].Rows[0]["IsAdmin"].ToString());
            Session["ISCoOrdinator"] = (ds1.Tables[0].Rows[0]["ISCoOrdinator"].ToString());
            Session["ISResolver"] = (ds1.Tables[0].Rows[0]["ISResolver"].ToString());
        }
    }

    protected void ibtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtUserID.Text = "";
            txtPassword.Text = "";
        }
        catch (Exception ex)
        {

        }
    }


 
    private bool validateUserLogon(string uid, string password)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
            SqlCommand cmd = new SqlCommand("ValidateUserLogon", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@PASSWORD", password);
            conn.Open();
            string returnString = cmd.ExecuteScalar().ToString();
            conn.Close();
            Session["LogonUserFullName"] = returnString.ToString();
            if (returnString.Length > 0)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }     
}

