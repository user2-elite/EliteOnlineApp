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

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblMessageFor.Text = "";
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        string strInvalidUserMsg = "Invalid User Id/Password.";
        try
        {
            Session["LogonUserFullName"] = "";
            Session["UserID"] = "";
            string strLoginID = "", strPassword = "";
            strLoginID = txtUserID.Text.ToString();
            strPassword = txtPassword.Text.ToString();
            if (strLoginID.Length > 0 && strPassword.Length > 0)
            {
                //ITSecurityPolicy              
                //try
                //{
                //    if (Session["IAgree"] != null)
                //    {
                //        SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
                //        SqlCommand cmd1 = new SqlCommand("INSERT_UserLogonStatus", sqlcon1);
                //        cmd1.CommandType = CommandType.StoredProcedure;
                //        cmd1.Parameters.AddWithValue("@UID", strLoginID.ToString());
                //        sqlcon1.Open();
                //        cmd1.ExecuteNonQuery();
                //    }
                //    else if (!ValidateIAgree(strLoginID))
                //    {
                //        lblLoginError.Text = "Please read & agree the IT Security Policy. This is mandatory for the first time logon.";
                //        return;
                //    }
                //}
                //catch
                //{
                //    lblLoginError.Text = "Please read & agree the IT Security Policy. This is mandatory for the first time logon.";
                //    return;
                //}

                if (validateUserLogon(strLoginID, strPassword))
                {
                    Session["UserText"] = "";
                    Session["UserID"] = strLoginID.ToString();
                    Session["Password"] = strPassword.ToString();
                    //MyAccessLevel();

                    //if (Session["AdminLinks"] == null || Session["selectedRole"] == null)
                    //{
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                    SqlCommand cmd = new SqlCommand("GetUserRole", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UID", Session["UserID"].ToString());
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "AdminTab");

                    if (ds.Tables["AdminTab"].Rows.Count != 0)
                    {
                        Session["selectedRole"] = ds.Tables["AdminTab"].Rows[0]["RoleID"].ToString();
                    }
                    else
                    {
                        Session["selectedRole"] = "0";
                    }
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    con.Close();
                    //}

                    //Add userData to Session
                    DataSet dsUser = GetUserDetails(strLoginID);
                    string UserInfo = "";
                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        UserInfo = "<div style='paddign:5px; margin:5px; font-weight:bold; color:#257e4a'>Name: " + Session["LogonUserFullName"].ToString();
                        UserInfo += "<BR><BR>Email: " + dsUser.Tables[0].Rows[0]["Email"].ToString();
                        UserInfo += "<BR><BR>Phone: " + dsUser.Tables[0].Rows[0]["Phone"].ToString();
                        UserInfo += "<BR><BR>Location: " + dsUser.Tables[0].Rows[0]["Location"].ToString();
                        UserInfo += "<BR><BR>Department: " + dsUser.Tables[0].Rows[0]["Department"].ToString();
                        UserInfo += "<BR><BR>Area: " + dsUser.Tables[0].Rows[0]["Area"].ToString();
                        UserInfo += "</div>";
                        Session["Email"] = dsUser.Tables[0].Rows[0]["Email"].ToString();
                    }
                    Session["UserText"] = UserInfo;


                    if (strPassword.ToString().ToLower() == "helpdesk123")
                    {
                        Response.Redirect("ChangePassword.aspx", false);
                    }
                    else
                    {
                        if(Request.QueryString["leaveapprove"] != null)
                        {
                            Response.Redirect("Leave/Approve.aspx", false);
                        }
                        else if (Request.QueryString["odapprove"] != null)
                        {
                            Response.Redirect("od/Approve.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("home.aspx", false); 
                        }
                        
                    }
                }
                else
                {
                    lblLoginError.Text = strInvalidUserMsg;
                 }
            }
            else
            {
                lblLoginError.Text = strInvalidUserMsg;
    
            }
        }
        catch (Exception ex)
        {
            lblLoginError.Text = ex.Message.ToString();
        }
    }
    private bool validateUserLogon(string uid, string password)
    {
        try
        {
            string returnString = "";
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            SqlCommand cmd = new SqlCommand("ValidateUserLogon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@PASSWORD", password);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserData");

            if (ds.Tables["UserData"].Rows.Count != 0)
            {
                Session["LogonUserFullName"] = ds.Tables["UserData"].Rows[0]["Name"].ToString();
                Session["EmpCode"] = ds.Tables["UserData"].Rows[0]["EmpCode"].ToString();
                Session["FunctionalReportingEmpNo"] = ds.Tables["UserData"].Rows[0]["FunctionalReportingEmpNo"].ToString();
                Session["AdministrativeReportingEmpno"] = ds.Tables["UserData"].Rows[0]["AdministrativeReportingEmpno"].ToString();
                Session["PayrollHREmpNo"] = ds.Tables["UserData"].Rows[0]["PayrollHREmpNo"].ToString();
                Session["LocationHREmpNo"] = ds.Tables["UserData"].Rows[0]["LocationHREmpNo"].ToString();
                returnString = Session["LogonUserFullName"].ToString();
            }

            if (returnString.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            lblLoginError.Text = ex.Message.ToString();
            return false;
        }
    }


    private DataSet GetUserDetails(string UID)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("INT_Get_MyInfo", con);
                cmd.Parameters.AddWithValue("@UID", UID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsUserInfo = new DataSet();
                da.Fill(dsUserInfo);
                if (dsUserInfo.Tables.Count > 0)
                    if (dsUserInfo.Tables[0].Rows.Count > 0)
                        return dsUserInfo;
                    else
                        return null;
                else
                    return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}