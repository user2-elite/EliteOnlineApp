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
/// Summary description for Class1
/// </summary>
public class LeaveEmail
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString);
    public void SendMailReport(string Content, string Subject, string FromEmail, string ToEmail, string CCEmail)
    {
        string strBody = "";
		string strBodyTest = "";
		//strBodyTest += "To Email: "+ ToEmail;
		//strBodyTest += "<br> CC Email: "+ CCEmail;
        //strBodyTest += "<br><br>--------------------------<br>";
		//ToEmail = "";
		//CCEmail = "";
		FromEmail = "hrinfo@eliteconnect.in";
        
        string htmlheader = @"<html><head></head><body topmargin='10' leftmargin='20' style='font-family:Verdana;font-size:13px;color:#000000;'>";
        string htmlfooter = @"<br></body></html>";

        bool sendMail = false;
        try
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(FromEmail);
            //message.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.rediffmailpro.com";
            //smtp.Host = "202.137.236.12";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = new NetworkCredential("sapadmin@eliteindia.com", "Password9");
            //smtp.EnableSsl = false;


            // message.Bcc.Add(new MailAddress(""));
            //message.To.Add(new MailAddress(""));
            //message.CC.Add(new MailAddress("");
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.rediffmailpro.com";
            //smtp.Host = "202.137.236.12";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
            smtp.EnableSsl = false;
            smtp.Send(message);

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "relay-hosting.secureserver.net";
            //smtp.Port = 25;
            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;

            strBody = strBodyTest + htmlheader + Content + htmlfooter;
            sendMail = true;

            if (sendMail)
            {
                try
                {
                    message.Subject = Subject;
                    message.Body = strBody;
                    ToEmail = ToEmail.ToLower();
                    CCEmail = CCEmail.Trim().ToLower();
                    string[] ccList = CCEmail.Split(';');
                    string[] toList = ToEmail.Split(';');
                    foreach (var item1 in toList)
                    {
                        if (item1.Length > 0)
                        {
                            message.To.Add(new MailAddress(item1.ToString()));
                        }
                    }

                    foreach (var item2 in ccList)
                    {
                        if (item2.Length > 0)
                        {
                            message.CC.Add(new MailAddress(item2.ToString()));
                        }
                    }
                    //message.CC.Add(new MailAddress("pinku@eliteindia.com"));
                    //message.Bcc.Add(new MailAddress(""));          

					//Hardcoded to/cc  for now as the email triggering to wrong recepients
                    smtp.Send(message);
                    message.Dispose();
                    message = null;
                    smtp.Dispose();
                    smtp = null;
                    HttpContext.Current.Response.Write("Mail Sent Successfully");
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message.ToString());
                    HttpContext.Current.Response.Write("Error in sending mail: " + ex.Message.ToString());

                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public string GetEmailList(string UID)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetLeaveEmailList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", UID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["EmailList"].ToString();
            }
            return "";
        }
        catch (Exception ex)
        {

            return "";
        }
    }

    public string GetUserEmailByLeaveID(string ID)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetUserEmailByLeaveID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["UserEmail"].ToString();
            }
            return "";
        }
        catch (Exception ex)
        {

            return "";
        }
    }

    public string GetUserEmailByODID(string ID)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("OD_GetUserEmailByODID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["UserEmail"].ToString();
            }
            return "";
        }
        catch (Exception ex)
        {

            return "";
        }
    }

    public string GetEmpNameByEmpCode(string empno)
    {

        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("INT_UserBYEmpCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Name"].ToString();
            }
            return "";
        }
        catch (Exception ex)
        {

            return "";
        }
    }

    public string GetEmailByEmpCode(string EmpCode)
    {

        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetEmailByEmpCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Email"].ToString();
            }
            return "";
        }
        catch (Exception ex)
        {

            return "";
        }
    }

    public bool CheckLeaveAvailabilityBySlNO(string SlNo)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Check_LeaveAvailability_BySlNO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", SlNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            string returnValue = "0";
            if (ds.Tables.Count > 0)
            {
                returnValue = ds.Tables[0].Rows[0]["AvailableStatus"].ToString();
            }
            return returnValue == "1" ? true : false;
        }
        catch (Exception ex)
        {

            return false;
        }
    }

}
