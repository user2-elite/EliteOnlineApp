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
        clearMessages();
        ShowMessage(divNotice, "Note: ", "Please know your authentication level. If you are a CRM Member or Unit Head or QA Member or Legal Representative or Production Team Member or Materials Team Member or Despatch Team Member, Please enter your User ID and Password to login below.");

if (Session["CRMUserID"] != null)
                    {
                        
                        Response.Redirect("home.aspx", false);

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

    protected void ibtnLogin_Click(object sender, EventArgs e)
    {
        string strInvalidUserMsg = "Invalid User Id/Password.";
        try
        {
            Session["CRMUserID"] = null;
            string strLoginID = "", strPassword = "";
            strLoginID = txtUserID.Text.ToString();
            strPassword = txtPassword.Text.ToString();
            Session["CRMselectedRole"] = null;
            if (strLoginID.Length > 0 && strPassword.Length > 0)
            {
                //HARDCODED For Testing
                if (strLoginID == "jm" && strPassword == "jm")
                {
                    Session["CRMUserID"] = strLoginID.ToString();
                    Session["Password"] = strPassword.ToString();
                    Session["CRMselectedRole"] = "1";
                    Session["LogonName"] = "Super User";
                    Session["CRMselectedRoleName"] = "Super User";
                    Session["UnitId"] = "1";
                    Session["UnitName"] = "Test";
                    Session["Email"] = "Test@Test.com";
                    Session["Phone"] = "00000000";
                    Response.Redirect("home.aspx", false);
                }

                if (Session["CRMselectedRole"] == null)
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
                    SqlCommand cmd = new SqlCommand("CRM_ValidateUserLogon", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UID", strLoginID.ToString());
                    cmd.Parameters.AddWithValue("@PASSWORD", strPassword.ToString());
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Logon");

                    if (ds.Tables["Logon"].Rows.Count != 0)
                    {
                        Session["CRMUserID"] = strLoginID.ToString();
                        Session["Password"] = strPassword.ToString();
                        Session["LogonName"] = ds.Tables["Logon"].Rows[0]["Name"].ToString();
                        Session["CRMselectedRole"] = ds.Tables["Logon"].Rows[0]["RoleID"].ToString();
                        Session["CRMselectedRoleName"] = ds.Tables["Logon"].Rows[0]["RoleName"].ToString();
                        Session["UnitId"] = ds.Tables["Logon"].Rows[0]["RoleID"].ToString();
                        Session["UnitName"] = ds.Tables["Logon"].Rows[0]["UnitName"].ToString();
                        Session["Email"] = ds.Tables["Logon"].Rows[0]["Email"].ToString();
                        Session["Phone"] = ds.Tables["Logon"].Rows[0]["Phone"].ToString();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                        Response.Redirect("home.aspx", false);
                    }
                    else
                    {
                        Session["CRMselectedRole"] = "0";
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                        ShowMessage(diverror, "Error", strInvalidUserMsg);
                    }
                }
            }
            else
            {
                ShowMessage(diverror, "Error", strInvalidUserMsg);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", strInvalidUserMsg);
        }
    }

}

