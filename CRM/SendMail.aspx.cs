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
        sendmails();
    }

    private void sendmails()
    {
        string mailsubj = "";
        string strBody = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'><div style='font-size:11px;font-family:calibri'>";
        //htmlheader += @"<div style='align:left'><img src=''</div>";
        string htmlfooter = @"<br>Regards,<br>CRM</div></body></html>";
        try
        {

            strBody += "<B>Please <a href='www.eliteonline.in/crm'><B>Click Here</B></a> to login to the portal. You can track the Complaint status and do necessary action. Please ignore the mail if it is not applicable to you.</B><br>";
            strBody = htmlheader + strBody + htmlfooter;
            mailsubj = "CRM Mails";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("crm@eliteonline.in");
                message.Subject = mailsubj;
                message.Body = strBody;

                //hardcoded
                message.To.Add(new MailAddress("pinku@eliteindia.com"));
                //message.To.Add(new MailAddress("pinku@eliteindia.com"));                
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

}


