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

public partial class SendBDayNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SendBDaymailTrigger();
    }

    private void SendBDaymailTrigger()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        //string htmlheader = @"<html><head></head><body topmargin='0' leftmargin='0' style='font-family:Georgia;font-size:13px;color:#000000' background='http://www.eliteonline.in/Images/bdaybg2.jpg'>";
        string htmlheader = @"<html><head></head><body topmargin='10' leftmargin='20' style='font-family:Georgia;font-size:13px;color:#8000FF' background='http://www.eliteonline.in/Images/bdaybg2.jpg'><B>";
        string htmlfooter = @"<br>Best Wishes,<br>Elite Team<br><a href='http://www.eliteonline.in'>www.eliteonline.in</a></B></td></tr></table></body></html>";

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();
        
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "INT_Get_BDayList";
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            string ID = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strBody = "<BR><BR><BR>";
                    try
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("notification@eliteonline.in");
                        message.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.rediffmailpro.com";
                        //smtp.Host = "202.137.236.12";
                        smtp.Port = 25;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
                        smtp.EnableSsl = false;
                        
                        mailsubj = "Happy Birthday !!!!";

                        tomail = ds.Tables[0].Rows[i]["Email"].ToString().Trim().ToLower();
                        ccmail = "";
                        strBody += "Dear " + ds.Tables[0].Rows[i]["Name"].ToString() + ",<BR><BR>";

                        strBody += "Team Elite wishing you a birthday filled with joy and success. Happy Birthday!!!<BR>";

                       
                        strBody += "<br>";

                        tomail = tomail.ToLower();
                        ccmail = ccmail.Trim().ToLower();
                        if (tomail.Trim() == "")
                        {
                            tomail = "hr@eliteindia.com"; //ds.Tables[0].Rows[i]["HLD_Email"].ToString().ToLower().Trim();
                        }

                        strBody = @"<table border='0' cellpadding='10' cellspacing='10' style='color:#8000FF;' width='100%'><tr><td>" + strBody;
                        strBody = htmlheader + strBody + htmlfooter;

                        try
                        {
                            message.Subject = mailsubj;
                            message.Body = strBody;
                            //hardcoded
                            message.To.Add(new MailAddress("pinku@eliteindia.com"));
                            //message.CC.Add(new MailAddress("pinku@eliteindia.com"));

                            //if (tomail.Length > 0)
                            //{
                                //message.To.Add(new MailAddress(tomail));                            
                            //}
                            //if (ccmail.Length > 0)
                            //{
                            //    message.CC.Add(new MailAddress(ccmail));
                            //}
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

 
}