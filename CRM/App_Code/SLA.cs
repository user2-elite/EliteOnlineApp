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

/// <summary>
/// Summary description for CommonClass
/// </summary>
public class ResolutionClass
{
    SqlCommand cmd = null;
    SqlConnection con = null;

    //GetRRByTime("mm/dd/yyyy 4:44:04 PM",6,'9.0','18.0',true,1,960,'');

    public static DateTime GetRRByTime(string createddate, int NoofWorkingDays, double Startinghour, double Endinghour, bool Holidaysincluded, double RRTime, string WorkGroupTimeZone)
    {
        DateTime createddttime = Convert.ToDateTime(createddate);

        createddttime = ConvertToTaragetedTimeZone(createddttime, WorkGroupTimeZone);

        //Checks whether the Request is created after before starting hour or after ending hour
        createddttime = ChKGroupTimings(createddttime, Startinghour, Endinghour);

        //Checks whether it is created on Holiday then move it to next working day
        DateTime newcreateddttime = RemoveHolidays(createddttime, Holidaysincluded, NoofWorkingDays);

        //Checks after holidy removal, change the time to the next working days starting hour
        if (newcreateddttime.Date != createddttime.Date)
        {
            createddttime = newcreateddttime.Date.Add(TimeSpan.FromHours(Startinghour));
        }

        DateTime RRByTime = RemoveNightHoursHolidays(createddttime, NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, RRTime);

        return ConvertToLocalTimeZone(RRByTime, WorkGroupTimeZone);

    }

    public static TimeSpan CompleltedHours(string createddate, int NoofWorkingDays, double Startinghour, double Endinghour, bool Holidaysincluded, string WorkGroupTimeZone)
    {        
        DateTime createddttime = ConvertToTaragetedTimeZone(Convert.ToDateTime(createddate), WorkGroupTimeZone);

        //Checks whether the Request is created after after the ending hour or before the starting hour
        createddttime = ChKGroupTimings(createddttime, Startinghour, Endinghour);

        //Checks whether it is created on Holiday then move it to next working day
        DateTime newcreateddttime = RemoveHolidays(createddttime, Holidaysincluded, NoofWorkingDays);

        //Checks after holidy removal, change the time to the next working days starting hour
        if (newcreateddttime.Date != createddttime.Date)
        {
            createddttime = newcreateddttime.Date.Add(TimeSpan.FromHours(Startinghour));
        }

        //Calculates Completed hours
        TimeSpan Completedtime = TimeCompleted(createddttime, Startinghour, Endinghour, NoofWorkingDays, Holidaysincluded, WorkGroupTimeZone);
        return Completedtime;

    }

    public static TimeSpan TimeCompleted(DateTime createddttime, double Startinghour, double Endinghour, int NoofWorkingDays, bool Holidaysincluded, string WorkGroupTimeZone)
    {
        //Checks whether the Request is created after after the ending hour or before the starting hour
        DateTime CurrentDateTime = ChKGroupTimings(ConvertToTaragetedTimeZone(CommonClass.GetDateTimeIST(), WorkGroupTimeZone), Startinghour, Endinghour);

        //Checks whether the current date is a holiday
        DateTime RemovedCurrentDateTime = RemoveHolidays(CurrentDateTime, Holidaysincluded, NoofWorkingDays);

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
                newcreatedate = RemoveHolidays(createddttime, Holidaysincluded, NoofWorkingDays);
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

    public static DateTime RemoveNightHoursHolidays(DateTime createddttime, int NoofWorkingDays, double Startinghour, double Endinghour, bool Holidaysincluded, double RRTime)
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
                ActualResolutionDate = RemoveHolidays(ActualResolutionDate, Holidaysincluded, NoofWorkingDays);
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
            ActualResolutionDate = RemoveHolidays(ActualResolutionDate, Holidaysincluded, NoofWorkingDays);
        }

        //Check whether the new date falls on a holiday if yes removes the holiday

        return ActualResolutionDate;
    }

    public static DateTime RemoveHolidays(DateTime CreatedDate, bool Holidaysincluded, int NoofWorkingDays)
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
}
