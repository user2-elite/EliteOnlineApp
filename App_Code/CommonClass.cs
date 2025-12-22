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
             con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
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
