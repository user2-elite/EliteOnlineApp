using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for CommonClass
/// </summary>
public class CommonClass
{
    SqlCommand cmd = null;
    SqlConnection con = null;

    public DataTable getDatatable(string SP, SqlConnection conn)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }

    public static void sendmails(string ComplaintId, DataTable dtComplaintdata, string MailType, string ActionTaken, string ActionTakenUpdatedBY, string ActionUpdatedON, string tomail, string ccmail, string currentStatus)
    {
        string mailsubj = "";
        string strBody = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'><div style='font-size:11px;font-family:calibri'>";
        //htmlheader += @"<div style='align:left'><img src=''</div>";
        string htmlfooter = @"<br>Regards,<br>Elite CRM</td></tr></table></div></body></html>";
        //DBOperation objDBOperation = new DBOperation();
        //DataSet dsComplaintdata = objDBOperation.GetComplaintByID(ComplaintId);   
        string Automatedmail = "<font color='red'><b><I>Auto-Generated e-mail triggered from CRM Portal. Please do not reply to this email. Login to the portal and do necessary action.<br></font></I></b>";
        string RequestBody = "";
        try
        {

            DBOperation objDBOperation = new DBOperation();
            if (dtComplaintdata == null)
            {
                DataSet dsComplaintdata = objDBOperation.GetComplaintByID(ComplaintId);
                dtComplaintdata = (DataTable)dsComplaintdata.Tables[0];
            }

            if (dtComplaintdata.Rows.Count != 0)
            {

                RequestBody = "<BR><table width='100%' cellpadding='5' cellspacing='3'><tr><td bgcolor=#536f9d width='100%' style='color:#FFFFFF;'>";
                RequestBody += "<B>Complaint Details</B><br><br>";
                RequestBody += "Complaint ID : " + dtComplaintdata.Rows[0]["Complaint_ID"].ToString() + "<br><br>";
                RequestBody += "Created On : " + dtComplaintdata.Rows[0]["createdOn"].ToString() + "<br><br>";
                RequestBody += "Complainant Name : " + dtComplaintdata.Rows[0]["Customer_Name"].ToString() + "<br><br>";
                RequestBody += "Call Taken Time : " + dtComplaintdata.Rows[0]["Registration_Time"].ToString() + "<br><br>";
                RequestBody += "Complaint Details : " + dtComplaintdata.Rows[0]["CompDescr"].ToString().Replace("\r\n", "<br>") + "<br><br>";
                RequestBody += "Current Status: <B>" + currentStatus + "</B><br>";
                RequestBody += "</TD></TR></Table><BR>";
            }
            if (MailType == "NEW")
            {
                mailsubj = "CRM - FYI: New complaint has been Created";
            }
            else if (MailType == "FORWARD")
            {
                mailsubj = "CRM - FYI: " + currentStatus;
            }
            else if (MailType == "CLOSE")
            {
                mailsubj = "CRM - FYI: Complaint has been closed ";
            }

            tomail = tomail.ToLower();
            ccmail = ccmail.Trim().ToLower();

            string BodyAddOn = "";
            if (tomail.Length < 5)
            {
                tomail = "crm@eliteindia.com;admin@eliteindia.com";
                BodyAddOn = "<font color='red'>Important Note: Below email trigger not sent to its actual recepients. ";
                BodyAddOn += "Please configure the recepient email id in the master setup.</font><hr>";
            }

            string ContentBody = "";
            ContentBody = "<BR><table width='100%' cellpadding='5' cellspacing='3'><tr><td bgcolor=#CCCCCC width='100%'>";
            ContentBody += "<B>Last Updated By: " + ActionTakenUpdatedBY + "</B><br><br>";
            ContentBody += "<B>Last Updated On: " + ActionUpdatedON + "</B><br><br>";
            ContentBody += "<B><U>Action Updated</U></B><br>";
            ContentBody += "" + ActionTaken.ToString().Replace("\r\n", "<br>") + "<br><br>";
            ContentBody += "</TD></TR></Table><BR>";

            strBody = "";
            if (BodyAddOn.Length > 0)
            {
                strBody += BodyAddOn;
            }
            strBody += Automatedmail;
            strBody += RequestBody;
            strBody += ContentBody;
            strBody += "<B>Please <a href='www.eliteonline.in/crm'><B>Click Here</B></a> to login to the portal. You can track the Complaint status and do necessary action. Please ignore the mail if it is not applicable to you.</B><br>";
            strBody = @"<table border='0' cellpadding='5' cellspacing='1' style='color:black;' width='100%'><tr><td>" + strBody;
            strBody = htmlheader + strBody + htmlfooter;
            try
            {

                MailMessage message = new MailMessage();
                message.From = new MailAddress("crm@eliteonline.in");
                message.Subject = mailsubj;
                message.Body = strBody;

                //To be uncommented
                string[] ToMuliId = tomail.Split(';');
                foreach (string ToEMailId in ToMuliId)
                {
                    if (ToEMailId.Length > 0)
                    {
                        message.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
                    }
                }
                string[] CCId = ccmail.Split(';');
                foreach (string CCEmail in CCId)
                {
                    if (CCEmail.Length > 0)
                    {
                        message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                    }
                }

                //hardcoded
                //message.CC.Add(new MailAddress("pinku@eliteindia.com"));

                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.rediffmailpro.com";
                //smtp.Host = "202.137.236.12";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                smtp.EnableSsl = false;
                smtp.Send(message);
            }
            catch (Exception ex)
            {

            }
        }
        catch
        {

        }
    }

    public static bool IsNumeric(string strTextEntry)
    {
        Regex objNotWholePattern = new Regex("[^0-9]");
        return !objNotWholePattern.IsMatch(strTextEntry);
    }

    public static DateTime GetDateTimeIST()
    {

        try
        {
            DateTime timeutc = System.DateTime.UtcNow;
            TimeZoneInfo cstzone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime csttime = TimeZoneInfo.ConvertTimeFromUtc(timeutc, cstzone);
            return csttime;
        }
        catch
        {
            DateTime SQLDateTime;
            SqlConnection con = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            SqlCommand cmd = new SqlCommand("CRM_GetDateTimeIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SQLDateTime = Convert.ToDateTime(cmd.ExecuteScalar().ToString());
            con.Close();
            cmd.Dispose();
            return SQLDateTime;
        }
    }


    public static string GetEmailByRoleandUnit(string UnitID, string RoleID)
    {
        string EmailID = "";
        try
        {
            SqlConnection con = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            SqlCommand cmd = new SqlCommand("CRM_GetEmailByRoleAndUnit", con);
            cmd.Parameters.AddWithValue("@RoleID", RoleID);
            cmd.Parameters.AddWithValue("@UnitID", UnitID);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            EmailID = cmd.ExecuteScalar().ToString();
            con.Close();
            cmd.Dispose();
            return EmailID;
        }
        catch
        {
            return "";
        }
    }

    public static string GetEmailForRSM(string Area)
    {
        string EmailID = "";
        try
        {
            SqlConnection con = null;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            SqlCommand cmd = new SqlCommand("CRM_GetEmailForRSM", con);
            cmd.Parameters.AddWithValue("@Area", Area);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            EmailID = cmd.ExecuteScalar().ToString();
            con.Close();
            cmd.Dispose();
            return EmailID;
        }
        catch
        {
            return "";
        }
    }
}
