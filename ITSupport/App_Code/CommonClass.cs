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

    public DataTable getDatatable(string SP,SqlConnection conn)
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

    public static void sendmails(string RequestID, string MessageType, string createdUser, string AdditionalMessage,string ccRequiredMails)
    {
        string mailsubj = "";
        string strBody = "";
        string tomail = "";
        string ccmail = "";

        string htmlheader = @"<html><head></head><body topmargin='1' leftmargin='5' style='font-family:Verdana; color:#000000'>";
        string htmlfooter = @"<br>Regards,<br>SupportDesk Team</td></tr></table></body></html>";
        SqlConnection con = null;
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        SqlCommand cmd = new SqlCommand("GetALLActiveRequestsFullDetails", con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RequestID", RequestID);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        try
        {
            if (dr.Read())
            {
               string Automatedmail = "<font color='red'><b><I>Auto-Generated e-mail. Please do not reply.<br></font></I></b>";

                string RequestBody = "<BR><table width='100%' cellpadding='1' cellspacing='1'><tr><td bgcolor=#F0F0F0 width='100%'>";
                RequestBody += "<B>Request Details</B><br>";
                RequestBody += "Request  No : " + RequestID.ToString().Trim() + "<br>";
                RequestBody += "Subject : " + dr["RequestSubject"].ToString().Trim() + "<br>";
                RequestBody += "Description : " + dr["RequestDescription"].ToString().Replace("\r\n", "<br>") + "<br><br>";
                RequestBody += "</TD></TR></Table><BR>";

                string RequestViolationStatus = "";
                if(dr["ResolutionTimeViolated"].ToString() == "1")
                  RequestViolationStatus = "Yes";
                else
                  RequestViolationStatus = "No";
           
                if (MessageType == "1")
                    {
                        mailsubj = "New Request: " + RequestID.ToString() + ". Created in SupportDesk";
                        tomail = dr["EmpEmail"].ToString().Trim().ToLower();
                        
                        ccmail = dr["HLD_Email"].ToString();
                        
                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["EmpName"].ToString() + ",</B><BR>";
                        
                        strBody += RequestBody;
                        strBody += "Request Status : Pending" + "<br>";
                        strBody += "Created Date: " + dr["CreatedDate"].ToString() + "<br><br>";
                        strBody += "<B>Login to the SupportDesk Website to track the request status</B><br>";                        
                    }
                    else if (MessageType == "10") //Request Response mail to the user
                    {
                        mailsubj = "Request No: " + RequestID.ToString() + ". has been Assigned";
                        tomail = dr["EmpEmail"].ToString().Trim().ToLower();
                        ccmail = dr["AssignedtoEmail"].ToString().Trim().ToLower();

                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["EmpName"].ToString() + ",</B><BR>";

                        strBody += "Your Request is assigned to the Support Engineer.<br>";
                        
                        strBody += RequestBody;
                        strBody += "Status: " + dr["StatusName"].ToString() + "<br>";
                        strBody += "Severity: " + dr["SeverityName"].ToString() + "<br>";
                        strBody += "Support Engineer: " + dr["NAME"].ToString() + "<br>";
                        strBody += "Expected Resolution Date: " + dr["ExpectedResolutionDtTime"].ToString() + "<br>";
                        
                    }
                    else if (MessageType == "2") //Request Assignment mail
                    {
                        mailsubj = "FYI: New Support Request No: " + RequestID.ToString() + ". assigned";
                        tomail = dr["AssignedtoEmail"].ToString().Trim().ToLower();
                        ccmail = dr["EmpEmail"].ToString().Trim().ToLower();

                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["NAME"].ToString() + ",</B><BR>";

                        strBody += "New Request Assigned<br>";                        
                        strBody += RequestBody;
                        strBody += "Status : " + dr["StatusName"].ToString() + "<br>";
                        strBody += "Severity : " + dr["SeverityName"].ToString() + "<br>";
                        strBody += "Request Violated : " + RequestViolationStatus + "<br>";
                        strBody += "Expected Resolution Date Time : " + dr["ExpectedResolutionDtTime"].ToString() + "<br>";

                        strBody += "Requester Name : " + dr["EmpName"].ToString() + "<br>";
                        strBody += "Requester Email : " + dr["EmpEmail"].ToString() + "<br>";
                        strBody += "Phone : " + dr["PHONE"].ToString() + "<br>";

                    }
                    else if (MessageType == "3") //Request Resolved mail.
                    {
                        mailsubj = "Request No : " + RequestID.ToString() + " is resolved.";
                        tomail = dr["EmpEmail"].ToString().Trim().ToLower();
                        ccmail = dr["AssignedtoEmail"].ToString().Trim().ToLower();

                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["EmpName"].ToString() + ",</B><BR>";

                        strBody += "Request No : " + RequestID.ToString() + " is closed. Please see the resolution details<br>";
                        
                        strBody += RequestBody;

                        strBody += "<font color='navy'><b>Request  Resolution : </b>" + dr["ClosureDesc"].ToString().Replace("\r\n", "<br>") + "</font><br>";

                        strBody += "Support Engineer : " + dr["NAME"].ToString() + "<br>";

                    }
                    else if (MessageType == "5") // Request status changed to Pending to user. So mail has to be triggered to the user.
                    {
                        tomail = dr["EmpEmail"].ToString().Trim().ToLower();
                        ccmail = dr["AssignedtoEmail"].ToString().Trim().ToLower();

                        mailsubj = "FYI: Request No : " + RequestID.ToString() + ". is pending.";
                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["EmpName"].ToString() + ",</B><BR>";
                        strBody += "Your Request No : " + RequestID.ToString() + " Need additional informations Please see the support engineer comments.<br><br>";
                        strBody += "<font color='blue'><BR><B>" + AdditionalMessage.Trim().ToString().Replace("\r\n", "</B><br>") + "</font><br><br>";
                        strBody += "Support engineer can proceed with this Request only after the relevant inputs from you. ";
                        strBody += RequestBody;

                    }
                    else if (MessageType == "6") // Release of Pending Status - Mail should be sent to the support staff
                    {
                        tomail = dr["AssignedtoEmail"].ToString().Trim().ToLower();
                        ccmail = dr["EmpEmail"].ToString().Trim().ToLower();

                        mailsubj = "FYI: Request No : " + RequestID.ToString() + ". User provided information";
                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["NAME"].ToString() + ",</B><BR>";

                        strBody += "<B>User comments: </B><br><br>";
                        strBody += "<font color='blue'>Message : " + AdditionalMessage.Trim().ToString().Replace("\r\n", "<br>") + "</font><br><br>";
                        strBody += RequestBody;
                    }
                    else if (MessageType == "7") // Rejected Request resolution - Mail should be sent to the respective helpdesk
                    {
                        tomail = dr["AssignedtoEmail"].ToString().Trim().ToLower();
                        if (dr["AssignedtoEmail"].ToString().Trim() == "")
                            tomail = dr["HLD_Email"].ToString();

                        ccmail = dr["EmpEmail"].ToString().Trim().ToLower();

                        mailsubj = "Request No : " + RequestID.ToString() + "- Not accepted the solution by user";
                        strBody = Automatedmail;
                        strBody += "<B>Dear " + dr["NAME"].ToString() + ",</B><BR>";

                        strBody += "User rejected the Request resolution: <br><br><B>User Message</B><BR>";
                        strBody += "<font color='blue'>Message : " + AdditionalMessage.Trim().ToString().Replace("\r\n", "<br>") + "</font><br><br>";
                        strBody += "Request you to do the needful at the earliest.<br><br>";

                        strBody += RequestBody;

                    }
                    else if (MessageType == "8" || MessageType == "9") // 8-Message to user, 9-Support Staff
                    {
                        if (MessageType == "8") // 8-Message to user
                        {
                            tomail = dr["EmpEmail"].ToString().Trim().ToLower();
                            ccmail = dr["AssignedtoEmail"].ToString().Trim().ToLower();
                            mailsubj = "FYI: Request No : " + RequestID.ToString() + ". New Message from Support Engineer";
                            strBody = Automatedmail;
                            strBody += "<B>Dear " + dr["EmpName"].ToString() + ",</B><BR>";

                            strBody += "New Meessage received from Support Engineer: <br><br>";
                            strBody += "<font color='blue'>Message: " + AdditionalMessage.Trim().ToString().Replace("\r\n", "<br>") + "</font><br><br>";
                            strBody += RequestBody;
                        }
                        else //  9-Message to Support Staff
                        {
                            if (dr["AssignedtoEmail"].ToString().Trim().ToLower() != "")
                                tomail = dr["AssignedtoEmail"].ToString().ToLower().Trim();
                            else
                                tomail = dr["HLD_Email"].ToString().ToLower().Trim();

                            ccmail = dr["EmpEmail"].ToString().Trim().ToLower();
                            mailsubj = "FYI: Request No : " + RequestID.ToString() + ". New Message from User";
                            strBody = Automatedmail;
                            if (tomail == dr["HLD_Email"].ToString().ToLower().Trim())
                                strBody += "<B>Hi,</B><BR>";
                            else
                                strBody += "<B>Dear " + dr["EmpName"].ToString() + ",</B><BR>";

                            strBody += "New Meessage received from User: <br><br>";
                            strBody += "<font color='blue'>Message : " + AdditionalMessage.Trim().ToString().Replace("\r\n", "<br>") + "</font><br><br>";
                            strBody += RequestBody;
                        }

                    }
                    tomail = tomail.ToLower();
                    ccmail = ccmail.Trim().ToLower();

                    if (tomail.Trim() == "")
                    {
                        tomail = "helpdesk@eliteindia.com"; //dr["HLD_Email"].ToString().ToLower().Trim();
                        ccmail = ccmail.Trim().ToLower();
                        string Newmailsubj = "SupportDesk mail Failed FYI:" + mailsubj;
                        mailsubj = Newmailsubj;
                        string strBodyNew = @"<table border='0' cellpadding='3' cellspacing='1' style='color:black;' width='100%'><tr><td><B>SupportDesk mail Failed FYI:" + mailsubj + "</B><BR><BR>";
                        strBodyNew += strBody;
                        strBody = htmlheader + strBodyNew + htmlfooter;
                    }
	                else
	                {
	                   strBody = @"<table border='0' cellpadding='3' cellspacing='1' style='color:black;' width='100%'><tr><td>" + strBody;
                                    strBody = htmlheader + strBody + htmlfooter;
	                }
                 
            }
            cmd.Dispose();
            con.Close();

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("ITsupport@eliteindia.com");
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
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();         
        }
    }

    public static void RequestViolationNotification(string tomail, string ccmail, string Requestid, string supportstaffname, string sub, string Requestdesc, string violatiotext, string helpdeskemail, string createdby)
    {
        //Request violation notifications
        string mailsubj = "";
        string strBody = "";
        string htmlheader = @"<html><head></head><body>";
        string htmlfooter = @"</td></tr></table></body></html>";
        mailsubj = "Support Request-Closure Violation Notification";
        strBody += "<p>Dear&nbsp; " + supportstaffname + ",</p>";
        strBody += "The following Request has been violated : <br> ";

        strBody += "Request Ticket No : " + Requestid + "<br>";
        strBody += "Request Subject : " + sub + "<br>";
        strBody += "Request Description : " + Requestdesc + "<br>";

        strBody += "<font color='red'>Request Violation Reasons : - </font>" + violatiotext + "<br>";


        strBody += "With Warm Regards <br><br> <b> HelpDesk Team </b>";

        strBody = @"<table class='RequestViolationstyle'><tr><td>" + strBody;

        strBody = htmlheader + strBody + htmlfooter;

           //send MAil

    }

    public static bool IsNumeric(string strTextEntry)
    {
        Regex objNotWholePattern = new Regex("[^0-9]");
        return !objNotWholePattern.IsMatch(strTextEntry);
    }
    
    public static int getmemberstatus(string memberID, string helpdeskid)
    {
        SqlConnection con = null;
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());

            SqlCommand cmd = new SqlCommand("GetALLActiveadmins", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memberID", memberID);
            cmd.Parameters.AddWithValue("@HelpDeskID", helpdeskid);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.Read())
            {
                return Convert.ToInt32(dr["Admin"].ToString());
            }
            else
            {
                return 1000;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

        }

        catch
        {
            if (con != null)
               if (con.State == ConnectionState.Open)
	                con.Close();
            
            return 5;
        }
    }

    public static DateTime GetRRByTime(string createddate, int NoofWorkingDays, double Startinghour, double Endinghour, bool Holidaysincluded, int HolidayLocation, double RRTime, string WorkGroupTimeZone)
    {
        HttpContext.Current.Session["HolidaysList"] = null;
        HttpContext.Current.Session["NoHolidays"] = "1";
        HttpContext.Current.Session["TimeUsed"] = null;

        DateTime createddttime = Convert.ToDateTime(createddate);

        createddttime = ConvertToTaragetedTimeZone(createddttime, WorkGroupTimeZone);


        //Checks whether the Request is created after before starting hour or after ending hour
        createddttime = ChKGroupTimings(createddttime, Startinghour, Endinghour);

        //Checks whether it is created on Holiday then move it to next working day
        DateTime newcreateddttime = RemoveHolidays(createddttime, Holidaysincluded, HolidayLocation, NoofWorkingDays);
        
        //Checks after holidy removal, change the time to the next working days starting hour
        if (newcreateddttime.Date != createddttime.Date)
        {
            createddttime = newcreateddttime.Date.Add(TimeSpan.FromHours(Startinghour));
        }

        DateTime RRByTime = RemoveNightHoursHolidays(createddttime, NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, RRTime);

        return ConvertToLocalTimeZone (RRByTime,WorkGroupTimeZone);

    }

    public static TimeSpan CompleltedHours(string createddate, int NoofWorkingDays, double Startinghour, double Endinghour, bool Holidaysincluded, int HolidayLocation, string WorkGroupTimeZone)
    {

        HttpContext.Current.Session["HolidaysList"] = null;
        HttpContext.Current.Session["NoHolidays"] = "1";
        HttpContext.Current.Session["TimeUsed"] = null;

        DateTime createddttime = ConvertToTaragetedTimeZone(Convert.ToDateTime(createddate),WorkGroupTimeZone);

        //Checks whether the Request is created after after the ending hour or before the starting hour
        createddttime = ChKGroupTimings(createddttime, Startinghour, Endinghour);

        //Checks whether it is created on Holiday then move it to next working day
        DateTime newcreateddttime = RemoveHolidays(createddttime, Holidaysincluded, HolidayLocation, NoofWorkingDays);

        //Checks after holidy removal, change the time to the next working days starting hour
        if (newcreateddttime.Date != createddttime.Date)
        {
            createddttime = newcreateddttime.Date.Add(TimeSpan.FromHours(Startinghour));
        }

        //Calculates Completed hours
        TimeSpan Completedtime = TimeCompleted(createddttime, Startinghour, Endinghour, NoofWorkingDays, Holidaysincluded, HolidayLocation, WorkGroupTimeZone);
        HttpContext.Current.Session["TimeUsed"] = Completedtime.TotalHours.ToString("00.00");
        return Completedtime;

    }

    public static TimeSpan TimeCompleted(DateTime createddttime, double Startinghour, double Endinghour, int NoofWorkingDays, bool Holidaysincluded, int HolidayLocation, string WorkGroupTimeZone)
    {
        //Checks whether the Request is created after after the ending hour or before the starting hour
        DateTime CurrentDateTime = ChKGroupTimings(ConvertToTaragetedTimeZone(GetDateTimeIST(), WorkGroupTimeZone), Startinghour, Endinghour);
        
        //Checks whether the current date is a holiday
        DateTime RemovedCurrentDateTime = RemoveHolidays(CurrentDateTime, Holidaysincluded, HolidayLocation, NoofWorkingDays);

        //Checks after holidy removal, change the time to the next working days starting hour
        if (CurrentDateTime.Date != RemovedCurrentDateTime.Date)
        {
            CurrentDateTime = RemovedCurrentDateTime.Date.Add(TimeSpan.FromHours(Startinghour));
        }

        DateTime newcreatedate = createddttime;
        TimeSpan timeused = new TimeSpan();

        if (createddttime.Date == CurrentDateTime.Date)
        {
            timeused = CurrentDateTime.Subtract(createddttime);
        }
        else
        {
            while (createddttime.Date < CurrentDateTime.Date)
            {
                //Adds one day with the createddatetime and checks that it is equal to CurrentDateTime, if not equal adds up the timeused.
                if ((newcreatedate.Date == createddttime.Date) && (createddttime.Date != CurrentDateTime.Date))
                {
                    timeused = timeused.Add(TimeSpan.FromHours(Endinghour - Startinghour));
                }
                createddttime = createddttime.AddDays(1);
                newcreatedate = RemoveHolidays(createddttime, Holidaysincluded, HolidayLocation, NoofWorkingDays);
            }

            if (createddttime <= CurrentDateTime)
            {
                TimeSpan timeuse = CurrentDateTime.TimeOfDay.Subtract(createddttime.TimeOfDay);
                timeused = timeused.Add(timeuse);
            }
            else if (createddttime > CurrentDateTime)
            {
                TimeSpan timeuse = createddttime.TimeOfDay.Subtract(CurrentDateTime.TimeOfDay);
                timeused = timeused.Subtract(timeuse);
                //timeused = timeused.Subtract(TimeSpan.FromHours(endtime - starttime));
            }
        }
        return timeused;

    }

    public static DateTime RemoveNightHoursHolidays(DateTime createddttime, int NoofWorkingDays, double Startinghour, double Endinghour, bool Holidaysincluded, int HolidayLocation, double RRTime)
    {

        DateTime ActualResolutionDate = createddttime.Date.AddHours(Startinghour);
        TimeSpan differnecefromstartingtime = createddttime.Subtract(createddttime.Date.AddHours(Startinghour));

        //Gets remiander after dividing the RRTime by the number of working hours in a day (ie endinghour - startinghour)
        double remainder = (RRTime / 60) % (Endinghour - Startinghour);

        //Gets number of days to be added to the created Date as resolution/response date
        double noofdays = ((RRTime / 60) - remainder) / (Endinghour - Startinghour);

        //Adds one by one and if falls on holiday removes it.
        if (noofdays >= 1)
        {
            double countnoofdays = 0;
            while (countnoofdays < noofdays)
            {
                ActualResolutionDate = ActualResolutionDate.AddDays(1);
                ActualResolutionDate = RemoveHolidays(ActualResolutionDate, Holidaysincluded, HolidayLocation, NoofWorkingDays);
                countnoofdays++;
            }
        }

        if (remainder > 0)
        {
            ActualResolutionDate = ActualResolutionDate.AddHours(remainder);
           // ActualResolutionDate = ChKRemainingHours(ActualResolutionDate, Group, starttime, endtime);
           // ActualResolutionDate = RemoveHolidays(ActualResolutionDate, Group);
        }
        // After iteration if created time is grater than the RRBTime
        if (differnecefromstartingtime > TimeSpan.FromHours(0))
        {
            ActualResolutionDate = ActualResolutionDate.Add(differnecefromstartingtime);
            ActualResolutionDate = ChKRemainingHours(ActualResolutionDate, Startinghour, Endinghour);
            ActualResolutionDate = RemoveHolidays(ActualResolutionDate, Holidaysincluded, HolidayLocation, NoofWorkingDays);
        }

        //Check whether the new date falls on a holiday if yes removes the holiday

        return ActualResolutionDate;
    }

    public static DateTime GetAutoClosureDate(int NoofWorkingDays, bool Holidaysincluded, int HolidayLocation)
    {
        DateTime expectedautoclosuredate = GetDateTimeIST();
            expectedautoclosuredate = RemoveHolidays(expectedautoclosuredate, Holidaysincluded, HolidayLocation, NoofWorkingDays);
            for (int countofdays=0; countofdays<3; countofdays++)
            {
                expectedautoclosuredate = expectedautoclosuredate.AddDays(1);
                expectedautoclosuredate = RemoveHolidays(expectedautoclosuredate, Holidaysincluded, HolidayLocation, NoofWorkingDays);
            }

           //Check whether the new date falls on a holiday if yes removes the holiday
            return expectedautoclosuredate;
    }

    public static DateTime RemoveHolidays(DateTime CreatedDate, bool Holidaysincluded, int HolidayLocation, int NoofWorkingDays)
    {
            SqlCommand cmd = null;
            SqlConnection con = null;

            //Remove saturdays and sundays according to the number of working days
            if (NoofWorkingDays == 6)
            {
                if (CreatedDate.DayOfWeek.ToString() == "Sunday")
                    CreatedDate = CreatedDate.AddDays(1);
            }
            else if (NoofWorkingDays == 5)
            {
                if (CreatedDate.DayOfWeek.ToString() == "Saturday")
                    CreatedDate = CreatedDate.AddDays(2);
                else if (CreatedDate.DayOfWeek.ToString() == "Sunday")
                    CreatedDate = CreatedDate.AddDays(1);
            }
            
            try
            {
                /*if (Holidaysincluded)
                {
                    if ((HttpContext.Current.Session["HolidaysList"] == null) && (HttpContext.Current.Session["NoHolidays"].ToString() == "1"))
                    {
                        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                        cmd = new SqlCommand("GetHolidaysbyCRDate", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        //This needs to be checked. For the time being 5 days before the created date is taken to findout the holidays
                        cmd.Parameters.AddWithValue("@createdate", CreatedDate.Date.Subtract(TimeSpan.FromDays(2)));
                        cmd.Parameters.AddWithValue("@HolidayLocation", HolidayLocation);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Tab");

                        if (ds.Tables["Tab"].Rows.Count != 0)
                        {
                            HttpContext.Current.Session["HolidaysList"] = ds;
                            HttpContext.Current.Session["NoHolidays"] = "1";
                        }
                        else
                        {
                            HttpContext.Current.Session["NoHolidays"] = "0";
                        }
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        con.Close();
                    }
                }


                if (HttpContext.Current.Session["HolidaysList"] != null)
                {
                    DataSet ds1 = (DataSet)HttpContext.Current.Session["HolidaysList"];

                    if (ds1.Tables["Tab"].Rows.Count != 0)
                    {
                        for (int rcount = 0; rcount < ds1.Tables["Tab"].Rows.Count; rcount++)
                        {
                            if (CreatedDate.Date == (Convert.ToDateTime(ds1.Tables["Tab"].Rows[rcount]["Holiday"].ToString()).Date))
                                //Remove the holidays and move it to next working day starting working time
                                CreatedDate = CreatedDate.AddDays(1);

                            //Remove saturdays and sundays according to the number of working days
                            if (NoofWorkingDays == 6)
                            {
                                if (CreatedDate.DayOfWeek.ToString() == "Sunday")
                                    CreatedDate = CreatedDate.AddDays(1);
                            }
                            else if (NoofWorkingDays == 5)
                            {
                                if (CreatedDate.DayOfWeek.ToString() == "Saturday")
                                    CreatedDate = CreatedDate.AddDays(2);
                                else if (CreatedDate.DayOfWeek.ToString() == "Sunday")
                                    CreatedDate = CreatedDate.AddDays(1);
                            }
                        }
                    }
                }
                */
                return CreatedDate;
            }
            catch
            {
                return CreatedDate;
            }
    }

    public static DateTime ChKGroupTimings(DateTime createddttime, double starttime, double endtime)
    {

        //Checks whether the Request is created after End Time or before the Start Time then move it to next working day
        if (createddttime >= createddttime.Date.AddHours(endtime))
        {
            createddttime = createddttime.Date.AddHours(24 + starttime);
        }
        else if (createddttime < createddttime.Date.AddHours(starttime))
        {
            createddttime = createddttime.Date.AddHours(starttime);
        }


        return createddttime;
    }

    public static DateTime ChKRemainingHours(DateTime ActualResolutionDate, double starttime, double endtime)
    {
        //Checks whether the ActualResolutionDate falls on night hours after the end time.
        if (ActualResolutionDate > ActualResolutionDate.Date.AddHours(endtime))
        {
            TimeSpan nighttime = ActualResolutionDate.Subtract(ActualResolutionDate.Date.AddHours(endtime));
            ActualResolutionDate = ActualResolutionDate.Date.AddHours(24 + starttime).Add(nighttime);
        }
        //Checks whether the ActualResolutionDate falls on morning hours before the start time.
        else if (ActualResolutionDate < ActualResolutionDate.Date.AddHours(starttime))
        {
            TimeSpan extratime = TimeSpan.FromHours(12 - starttime);
            TimeSpan nighttime = ActualResolutionDate.Subtract(ActualResolutionDate.Date);
            ActualResolutionDate = ActualResolutionDate.Date.AddHours(starttime).Add(nighttime.Add(extratime));
        }
        return ActualResolutionDate;
    }

    public static DateTime ConvertToTaragetedTimeZone(DateTime createddttime, string WorkGroupTimeZone)
    {
        if (WorkGroupTimeZone == "" || WorkGroupTimeZone == "India Standard Time")
        {
            return createddttime;
        }
        else
        {
            try
            {
                //TimeZoneInfo toTimeZone = TimeZoneInfo.FindSystemTimeZoneById(WorkGroupTimeZone);

                //DateTime NewDateTime = TimeZoneInfo.ConvertTime(createddttime, System.TimeZoneInfo.Local, toTimeZone);

                //return NewDateTime;
                return createddttime;
            }
            catch
            {
                return createddttime;
            }
        }
    }

    public static DateTime ConvertToLocalTimeZone(DateTime createddttime, string WorkGroupTimeZone)
    {
        if (WorkGroupTimeZone == "" || WorkGroupTimeZone == "India Standard Time")
        {
            return createddttime;
        }
        else
        {
            try
            {
                return createddttime;
                //TimeZoneInfo toTimeZone = TimeZoneInfo.FindSystemTimeZoneById(WorkGroupTimeZone);

                //DateTime NewDateTime = TimeZoneInfo.ConvertTime(createddttime, toTimeZone, System.TimeZoneInfo.Local);

                //return NewDateTime;
            }
            catch
            {
                return createddttime;
            }
        }
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
             con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
             SqlCommand cmd = new SqlCommand("SP_GetDateTimeIST", con);
             cmd.CommandType = CommandType.StoredProcedure;
             con.Open();
             SQLDateTime = Convert.ToDateTime(cmd.ExecuteScalar().ToString());
             con.Close();
             cmd.Dispose();
             return SQLDateTime;
         }
    }
 }
