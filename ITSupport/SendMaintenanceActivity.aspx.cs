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

public partial class SendMaintenanceActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SendMaintenanceActivitymailTrigger();
        SendMaintenanceActivitymailEscalation();
    }

    private void SendMaintenanceActivitymailTrigger()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'>";
        string htmlfooter = @"<br>Regards,<br>SupportDesk Team</td></tr></table></body></html>";

        string Automatedmail = "<font color='blue'><b><I>Auto-Generated e-mail. Please do not reply.<br></font></I></b>";
        string RequestBody = "";

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();

        

        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TASK_IT_Get_MaintenanceActivityTrigger";
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            string ID = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("SupportDesk@eliteonline.in");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.rediffmailpro.com";"relay-hosting.secureserver.net";
                        //smtp.Host = "202.137.236.12";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                        smtp.EnableSsl = false;

                        RequestBody = "<BR><table width='100%' cellpadding='3' cellspacing='3'><tr><td bgcolor=#F0F0F0 width='100%'>";
                        ID = ds.Tables[0].Rows[i]["ID"].ToString().Trim();
                        RequestBody += "<B>Asset Details</B><br>";
                        RequestBody += "Asset Name : " + ds.Tables[0].Rows[i]["AssetName"].ToString() + "<br>";
                        RequestBody += "Asset Description: " + ds.Tables[0].Rows[i]["AssetDescription"].ToString() + "<br>";
                        RequestBody += "Asset Type: " + ds.Tables[0].Rows[i]["AssetType"].ToString() + "<br>";
                        RequestBody += "Activity Responsible To: " + ds.Tables[0].Rows[i]["NAME"].ToString() + "<br><br>";
                        RequestBody += "Next Activity Date: " + ds.Tables[0].Rows[i]["NextActivityDate"].ToString() + "<br>";
                        RequestBody += "Next Activity In Days: " + ds.Tables[0].Rows[i]["NextActivityInDays"].ToString() + "<br><BR>";                        
                        RequestBody += "</TD></TR></Table><BR>";
                        if (ds.Tables[0].Rows[i]["NextActivityInDays"].ToString() == "5")
                        {
                            mailsubj = "Scheduled Maintenance Activity - Reminder 1";
                        }
                        else if (ds.Tables[0].Rows[i]["NextActivityInDays"].ToString() == "1")
                        {
                            mailsubj = "Scheduled Maintenance Activity - Reminder 2";
                        }
                        else if (ds.Tables[0].Rows[i]["NextActivityInDays"].ToString() == "0")
                        {
                            mailsubj = "Scheduled Maintenance Activity - Due Date";
                        }
                        else
                        {
                            mailsubj = "Scheduled Maintenance Activity - Reminder";
                        }

                        tomail = ds.Tables[0].Rows[i]["Email"].ToString().Trim().ToLower();
                        ccmail = "";
                        strBody = Automatedmail;
                        strBody += "<B>Dear " + ds.Tables[0].Rows[i]["NAME"].ToString() + ",</B><BR><BR>";

                        strBody += "<B><font color='green'>Please find the asset details and the scheduled date for the maintenance activity.</font></B><BR>";

                        strBody += RequestBody;
                       
                        strBody += "<BR><B>Login to the <a href='http://www.eliteonline.in/itsupport/'><U>SupportDesk Website</U></a> to view all upcoming scheduled activities. Please update the activity details in system, once the activity is completed.</B><br>";
                        strBody += "<br>";

                        tomail = tomail.ToLower();
                        ccmail = ccmail.Trim().ToLower();
                        if (tomail.Trim() == "")
                        {
                            tomail = "helpdesk@eliteindia.com"; //ds.Tables[0].Rows[i]["HLD_Email"].ToString().ToLower().Trim();
                        }

                        strBody = @"<table border='0' cellpadding='3' cellspacing='1' style='color:black;' width='100%'><tr><td>" + strBody;
                        strBody = htmlheader + strBody + htmlfooter;

                        try
                        {
                            message.Subject = mailsubj;
                            message.Body = strBody;
                            //hardcoded
                            //message.CC.Add(new MailAddress("pinku@eliteindia.com"));
                            message.To.Add(new MailAddress(tomail));                            
                            if (ccmail.Length > 0)
                            {
                                message.CC.Add(new MailAddress(ccmail));
                            }
                            smtp.Send(message);
                            //update Notification status   
                            message.Dispose();
                            message = null;
                            smtp.Dispose();
                            smtp = null;
                        }
                        catch
                        {

                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void SendMaintenanceActivitymailEscalation()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'>";
        string htmlfooter = @"<br>Regards,<br>SupportDesk Team</td></tr></table></body></html>";

        string Automatedmail = "<font color='blue'><b><I>Auto-Generated e-mail. Please do not reply.<br></font></I></b>";
        string RequestBody = "";

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();

       

        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TASK_IT_Get_MaintenanceActivityTriggerEscalation";
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            string ID = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("SupportDesk@eliteonline.in");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.rediffmailpro.com";
                        //smtp.Host = "202.137.236.12";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                        smtp.EnableSsl = false;
                        RequestBody = "<BR><table width='100%' cellpadding='3' cellspacing='3'><tr><td bgcolor=#F0F0F0 width='100%'>";
                        ID = ds.Tables[0].Rows[i]["ID"].ToString().Trim();
                        RequestBody += "<B>Asset Details</B><br>";
                        RequestBody += "Asset Name : " + ds.Tables[0].Rows[i]["AssetName"].ToString() + "<br>";
                        RequestBody += "Asset Description: " + ds.Tables[0].Rows[i]["AssetDescription"].ToString() + "<br>";
                        RequestBody += "Asset Type: " + ds.Tables[0].Rows[i]["AssetType"].ToString() + "<br>";
                        RequestBody += "Activity Responsible To: " + ds.Tables[0].Rows[i]["NAME"].ToString() + "<br><br>";
                        RequestBody += "Planned Activity Date: " + ds.Tables[0].Rows[i]["NextActivityDate"].ToString() + "<br>";
                        RequestBody += "Violation in Days: " + ds.Tables[0].Rows[i]["NextActivityInDays"].ToString() + "<br><BR>";
                        RequestBody += "</TD></TR></Table><BR>";
                        
                        mailsubj = "Scheduled Maintenance Activity - Violation";

                        tomail = ds.Tables[0].Rows[i]["Email"].ToString().Trim().ToLower();
                        ccmail = "helpdesk@eliteindia.com";
                        strBody = Automatedmail;
                        strBody += "<B>Dear " + ds.Tables[0].Rows[i]["NAME"].ToString() + ",</B><BR><BR>";

                        strBody += "<B><font color='red'>Below asset Maintenance Activity is not executed on due date. Please find the asset details and the planned scheduled date for the maintenance activity.</font></B><BR>";

                        strBody += RequestBody;

                        strBody += "<BR><B>Login to the <a href='http://www.eliteonline.in/itsupport/'><U>SupportDesk Website</U></a> to view all upcoming/Pending scheduled activities. Please update the activity details in system, once the activity is completed.</B><br>";
                        strBody += "<br>";

                        tomail = tomail.ToLower();
                        ccmail = ccmail.Trim().ToLower();
                        if (tomail.Trim() == "")
                        {
                            tomail = "helpdesk@eliteindia.com"; //ds.Tables[0].Rows[i]["HLD_Email"].ToString().ToLower().Trim();
                        }

                        strBody = @"<table border='0' cellpadding='3' cellspacing='1' style='color:black;' width='100%'><tr><td>" + strBody;
                        strBody = htmlheader + strBody + htmlfooter;

                        try
                        {
                            message.Subject = mailsubj;
                            message.Body = strBody;
                            //hardcoded
                            message.To.Add(new MailAddress(tomail));
                            if (ccmail.Length > 0)
                            {
                                message.CC.Add(new MailAddress(ccmail));
                            }
                            message.CC.Add(new MailAddress("cgp@eliteindia.com"));
                            smtp.Send(message);
                            message.Dispose();
                            message = null;
                            smtp.Dispose();
                            smtp = null;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
   
}