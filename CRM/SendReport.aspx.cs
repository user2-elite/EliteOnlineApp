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

public partial class SendReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SendMailReport();
    }

    private void SendMailReport()
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        string htmlheader = @"<html><head></head><body topmargin='10' leftmargin='20' style='font-family:Verdana;font-size:13px;color:#000000;'>";
        string htmlfooter = @"<br></body></html>";

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();
        bool sendMail = false;
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RPT_ComplaintTypeWiseDataForMail";
            cmd.Parameters.Clear();
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);

            MailMessage message = new MailMessage();
            message.From = new MailAddress("crm@eliteonline.in");
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.rediffmailpro.com";
            //smtp.Host = "202.137.236.12";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
            smtp.EnableSsl = false;
            mailsubj = "CRM - Consolidated Report";
            //ComplaintTypesCategory;
            //ComplaintTypeCount;

            string CategoryName = "";
            string CategoryNamePrev = "";

            if ((ds.Tables[0].Rows.Count > 0) && (ds.Tables[1].Rows.Count > 0))
            {
                strBody += "<BR><BR><font color='navy'><B>Consolidated report of Major/Minor Complaints starting from " + ds.Tables[0].Rows[0]["MonthName"].ToString() + ", " + ds.Tables[0].Rows[0]["Year"].ToString() + " to till date.</B></font><BR>";
                strBody += "<TABLE Cellpadding='5' Cellspacing='5'  style='border:1px; border-color:#000000;'>";
                strBody += "<TR>";
                strBody += "<TD align='left' valign='top' style='background-color: #FFFFFF; font-weight:bold; color:#000000'></td>";
                strBody += "<TD align='left' valign='top' style='background-color: #FFBF00; font-weight:bold; color:#000000' colspan='2' align='center'>Total " + ds.Tables[0].Rows[0]["MonthName"].ToString() + " - Till Date</td>";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    strBody += "<TD align='left' valign='top' style='background-color: #FFBF00; font-weight:bold; color:#000000'>" + ds.Tables[0].Rows[i]["MonthName"].ToString() + "</td>";
                }
                strBody += "</TR>";

                int CategoryTotal = 0;
                int GrandTotal = 0;
                int Month1Total = 0;
                int Month2Total = 0;
                int Month3Total = 0;
                string alterColor = "#EEEEEE";
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    try
                    {
                        CategoryTotal = 0;
                        CategoryTotal = System.Convert.ToInt32(ds.Tables[1].Rows[i]["Month1Count"].ToString()) + System.Convert.ToInt32(ds.Tables[1].Rows[i]["Month2Count"].ToString()) + System.Convert.ToInt32(ds.Tables[1].Rows[i]["Month3Count"].ToString());
                        Month1Total = Month1Total + System.Convert.ToInt32(ds.Tables[1].Rows[i]["Month1Count"].ToString());
                        Month2Total = Month2Total + System.Convert.ToInt32(ds.Tables[1].Rows[i]["Month2Count"].ToString());
                        Month3Total = Month3Total + System.Convert.ToInt32(ds.Tables[1].Rows[i]["Month3Count"].ToString());

                        CategoryName = ds.Tables[1].Rows[i]["ComplaintTypesCategory"].ToString();


                        strBody += "<TR>";
                        if (CategoryNamePrev != CategoryName)
                        {
                            if (alterColor == "#FFBF00")
                            {
                                alterColor = "#EEEEEE";
                            }
                            else
                            {
                                alterColor  = "#FFBF00";
                            }
                            strBody += "<TD rowspan=" + ds.Tables[1].Rows[i]["ComplaintTypeCategoryCount"].ToString() + " align='left' valign='top' style='background-color: " + alterColor + ";' valign='middle'><B>" + CategoryName + "</B></td>";
                        }

                        strBody += "<TD align='left' valign='top' style='background-color: #EFF0FF; font-weight:normal'>" + ds.Tables[1].Rows[i]["ComplaintTypes"].ToString() + "</td>";
                        strBody += "<TD align='left' valign='top' style='background-color: #819FF7; font-weight:normal; width:80px;'>" + CategoryTotal + "</td>";
                        strBody += "<TD align='left' valign='top' style='background-color: #EFF0FF; font-weight:normal'>" + ds.Tables[1].Rows[i]["Month1Count"].ToString() + "</td>";
                        strBody += "<TD align='left' valign='top' style='background-color: #EFF0FF; font-weight:normal'>" + ds.Tables[1].Rows[i]["Month2Count"].ToString() + "</td>";
                        strBody += "<TD align='left' valign='top' style='background-color: #EFF0FF; font-weight:normal'>" + ds.Tables[1].Rows[i]["Month3Count"].ToString() + "</td>";
                        strBody += "</TR>";
                        CategoryNamePrev = CategoryName;
                    }
                    catch
                    {

                    }
                }

                GrandTotal = Month1Total + Month2Total + Month3Total;
                strBody += "<TR>";
                strBody += "<TD align='left' valign='top' style='background-color: lightblue; font-weight:bold'></td>";
                strBody += "<TD align='left' valign='top' style='background-color: lightblue; font-weight:bold'></td>";
                strBody += "<TD align='left' valign='top' style='background-color: lightblue; font-weight:bold'>" + GrandTotal + "</td>";
                strBody += "<TD align='left' valign='top' style='background-color: lightblue; font-weight:bold'>" + Month1Total + "</td>";
                strBody += "<TD align='left' valign='top' style='background-color: lightblue; font-weight:bold'>" + Month2Total + "</td>";
                strBody += "<TD align='left' valign='top' style='background-color: lightblue; font-weight:bold'>" + Month3Total + "</td>";
                strBody += "</TR>";
                strBody += "</TABLE>";
                strBody += "<HR>";
                strBody = htmlheader + strBody + htmlfooter;
                sendMail = true;
            }
            if (sendMail)
            {
                try
                {
                    message.Subject = mailsubj;
                    message.Body = strBody;
                    tomail = "pinku@eliteindia.com";
                    ccmail = "cgp@eliteindia.com;raghulal@eliteindia.com";
                    tomail = tomail.ToLower();
                    ccmail = ccmail.Trim().ToLower();

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
                    //message.To.Add(new MailAddress("shridhar.panshikar@eliteindia.com"));
                    //message.To.Add(new MailAddress("cgp@eliteindia.com"));
                    //message.To.Add(new MailAddress("mgeorge@eliteindia.com"));
                    //message.To.Add(new MailAddress("pinku@eliteindia.com"));

                    smtp.Send(message);
                    message.Dispose();
                    message = null;
                    smtp.Dispose();
                    smtp = null;
                }
                catch
                {

                }
            }
        }
        catch (Exception ex)
        {

        }
    }


}