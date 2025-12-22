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

public partial class SendMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//try
//{
//sendTestMail();
//}
        //catch (Exception ex)
        //{
        //                       Response.Write(ex.Message.ToString());
        //}
        try
        {
            sendResponseViolationMails();

        }
        catch (Exception ex)
        {

        }
        try
        {
            sendUnderObservationViolationMails();

        }
        catch (Exception ex)
        {

        }

        
    }

    private void sendResponseViolationMails()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'>";
        string htmlfooter = @"<br>Regards,<br>SupportDesk Team</td></tr></table></body></html>";

        string Automatedmail = "<font color='blue'><b><I>Auto-Generated e-mail. Please do not reply.<br></font></I></b>";
        string RequestBody = "<BR><table width='100%' cellpadding='3' cellspacing='3'><tr><td bgcolor=#F0F0F0 width='100%'>";

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();

        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TASK_IT_GetResponseViolationList";
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            string ReqID = "";
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
                        smtp.Host = "relay-hosting.secureserver.net";
                        //smtp.Host = "202.137.236.12";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                       //smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                        smtp.EnableSsl = false;

                        ReqID = ds.Tables[0].Rows[i]["RequestID"].ToString().Trim();
                        RequestBody += "<B>Request Details</B><br>";
                        RequestBody += "Request  No : " + ReqID + "<br>";
                        RequestBody += "Request Status : Pending" + "<br>";
                        RequestBody += "Created Date: " + ds.Tables[0].Rows[i]["CreatedDate"].ToString() + "<br>";
                        RequestBody += "Created By: " + ds.Tables[0].Rows[i]["EmpName"].ToString() + "<br>";
                        RequestBody += "User ID: " + ds.Tables[0].Rows[i]["UID"].ToString() + "<br><br>";
                        RequestBody += "<B><U>Contact Details</U></B><BR><BR>";
                        RequestBody += "Phone: " + ds.Tables[0].Rows[i]["PHONE"].ToString() + "<br>";
                        RequestBody += "Email: " + ds.Tables[0].Rows[i]["EmpEmail"].ToString() + "<br><BR>";

                        RequestBody += "Subject : " + ds.Tables[0].Rows[i]["RequestSubject"].ToString().Trim() + "<br>";
                        RequestBody += "Description : " + ds.Tables[0].Rows[i]["RequestDescription"].ToString().Replace("\r\n", "<br>") + "<br><br>";
                        RequestBody += "</TD></TR></Table><BR>";

                        mailsubj = "Support Desk - Response Violation Notification";
                        tomail = ds.Tables[0].Rows[i]["HLD_Email"].ToString().Trim().ToLower();

                        ccmail = ds.Tables[0].Rows[i]["HLD_Email"].ToString();

                        strBody = Automatedmail;
                        strBody += "<B>Dear Support Desk Admin,</B><BR><BR>";

                        strBody += "<B><font color='red'>This is to notify that below given request has been violated its response time.</font></B><BR>";

                        strBody += RequestBody;

                        strBody += "<BR><B>Login to the <a href='http://www.eliteonline.in/itsupport/'><U>SupportDesk Website</U></a> and request you to do the needful at the earliest.</B><br>";
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
                            message.CC.Add(new MailAddress("cgp@eliteindia.com"));
                            message.CC.Add(new MailAddress("bijoy.francis@eliteindia.com"));
                            smtp.Send(message);

                            message.Dispose();
                            message = null;
                            smtp.Dispose();
                            smtp = null;

                            //update Notification status
                            UpdateResponseAction(ReqID);
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


    private void sendUnderObservationViolationMails()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'>";
        string htmlfooter = @"<br>Regards,<br>SupportDesk Team</td></tr></table></body></html>";

        string Automatedmail = "<font color='blue'><b><I>Auto-Generated e-mail. Please do not reply.<br></font></I></b>";
        string RequestBody = "<BR><table width='100%' cellpadding='3' cellspacing='3'><tr><td bgcolor=#F0F0F0 width='100%'>";

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TASK_IT_GetUnderObservationViolationList";
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            string ReqID = "";
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
                        smtp.Host = "relay-hosting.secureserver.net";
                        //smtp.Host = "202.137.236.12";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        //smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                        smtp.EnableSsl = false;

                        ReqID = ds.Tables[0].Rows[i]["RequestID"].ToString().Trim();
                        RequestBody += "<B>Request Details</B><br>";
                        RequestBody += "Request  No : " + ReqID + "<br>";
                        RequestBody += "Request Status : Under Observation" + "<br>";
                        RequestBody += "Created Date: " + ds.Tables[0].Rows[i]["CreatedDate"].ToString() + "<br>";
                        RequestBody += "Created By: " + ds.Tables[0].Rows[i]["EmpName"].ToString() + "<br>";
                        RequestBody += "User ID: " + ds.Tables[0].Rows[i]["UID"].ToString() + "<br><br>";
                        RequestBody += "<B><U>Contact Details</U></B><BR><BR>";
                        RequestBody += "Phone: " + ds.Tables[0].Rows[i]["PHONE"].ToString() + "<br>";
                        RequestBody += "Email: " + ds.Tables[0].Rows[i]["EmpEmail"].ToString() + "<br><BR>";

                        RequestBody += "Assigned Date: " + ds.Tables[0].Rows[i]["AssingedDate"].ToString() + "<br>";
                        RequestBody += "Assinged To: " + ds.Tables[0].Rows[i]["ResolverName"].ToString() + "<br>";
                        RequestBody += "Last Updated On: " + ds.Tables[0].Rows[i]["Lastupdateddate"].ToString() + "<br><BR>";

                        RequestBody += "Subject : " + ds.Tables[0].Rows[i]["RequestSubject"].ToString().Trim() + "<br>";
                        RequestBody += "Description : " + ds.Tables[0].Rows[i]["RequestDescription"].ToString().Replace("\r\n", "<br>") + "<br><br>";
                        RequestBody += "</TD></TR></Table><BR>";

                        mailsubj = "Support Desk - Under Observation Status Violation Notification";
                        tomail = ds.Tables[0].Rows[i]["ResolverEmail"].ToString().Trim().ToLower();

                        ccmail = ds.Tables[0].Rows[i]["HLD_Email"].ToString();

                        strBody = Automatedmail;
                        strBody += "<B>Dear " + ds.Tables[0].Rows[i]["ResolverName"].ToString() + ",</B><BR><BR>";

                        strBody += "<B><font color='red'>This is to notify that below given request has been mark as unsatisfactory resolution since it got violated its maximum under observation period of 5 days.</font></B><BR>";

                        strBody += RequestBody;

                        strBody += "<BR><B>Login to the <a href='http://www.eliteonline.in/itsupport/'><U>SupportDesk Website</U></a> and request you to do the needful at the earliest.</B><br>";
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
                            message.CC.Add(new MailAddress("bijoy.francis@eliteindia.com"));
		                    
                            smtp.Send(message);

                            message.Dispose();
                            message = null;
                            smtp.Dispose();
                            smtp = null;

                            //update Notification status
                            try
                            {
                                UpdateUnderObservationViolation(ReqID);
                            }
                            catch { }
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

    public void UpdateResponseAction(string ReqID)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqID", ReqID);
            conn.Open();
            cmd.CommandText = "Update_ResponseViolationNotificationStatus";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch
        {
        }

    }

    public void UpdateResolutionAction(string ReqID)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqID", ReqID);
            conn.Open();
            cmd.CommandText = "Update_ResolutionViolationNotificationStatus";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch
        {
        }

    }

    public void UpdateUnderObservationViolation(string ReqID)
    {
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqID", ReqID);
            conn.Open();
            cmd.CommandText = "Update_UnderObservationViolationStatus";
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch
        {
        }

    }

private void sendTestMail()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("SupportDesk@eliteonline.in");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "relay-hosting.secureserver.net";
                        smtp.Host = "202.137.236.12";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = false;
                        //smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                        smtp.EnableSsl = false;


                        mailsubj = "Support Desk - Test";
                        tomail = "pinku@eliteindia.com";

                        ccmail = "";

                        strBody = "Test Mail. Plz ignore!";
                        tomail = tomail.ToLower();
                        ccmail = ccmail.Trim().ToLower();
                        
                        try
                        {
                            message.Subject = mailsubj;
                            message.Body = strBody;
                            //hardcoded
                            message.To.Add(new MailAddress(tomail));
                            message.CC.Add(new MailAddress(ccmail));
                            smtp.Send(message);

                            message.Dispose();
                            message = null;
                            smtp.Dispose();
                            smtp = null;

                        }
                        catch (Exception ex)
                        {
                               Response.Write(ex.Message.ToString());
                        }

    }


}