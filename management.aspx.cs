using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class management : System.Web.UI.Page
{
    protected int ProfileCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //clearMessages();
    }

    //private void BindList()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        using (SqlConnection conn = new SqlConnection())
    //        {
    //            conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
    //            using (SqlCommand cmd = new SqlCommand())
    //            {
    //                cmd.CommandText = "INT_Get_All_MGMTList";
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Connection = conn;
    //                conn.Open();
    //                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //                {
    //                    string divContent = "";
    //                    sda.Fill(dt);
    //                    ProfileCount = dt.Rows.Count;
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        for (int i = 0; i < dt.Rows.Count; i++)
    //                        {
    //                            string profileText = dt.Rows[i]["MGMT_Profile"].ToString();
    //                            profileText = profileText.Replace("<P>", "</br>");
    //                            profileText = profileText.Replace("</P>", "</br>");
    //                            profileText = profileText.Replace("<p>", "</br>");
    //                            profileText = profileText.Replace("</p>", "</br>");

    //                            divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
    //                            divContent += "<p class='e" + i + "'><img src='Gallery/Management/" + dt.Rows[i]["MGMT_Picture_Name"].ToString() + "' width='140' height='140' style='border:1px; border-color:#F0F0F0;' /></p>";
    //                            divContent += "<BR><B>" + dt.Rows[i]["MGMT_Name"].ToString() + "</B>";
    //                            divContent += "<BR>" + dt.Rows[i]["MGMT_Desig"].ToString();
    //                            divContent += "<p class='exp" + i + "' style='padding:10px; margin:10px; background-color:#E6E6FA; color:#000000; border:1px;'><BR>" + profileText + "</p>";
    //                            divContent += "</div>";
    //                        }
    //                    }
    //                    conn.Close();
    //                    divList.InnerHtml = divContent;
    //                    //return dt;
    //                }
    //                dt.Dispose();
    //                conn.Close();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage(diverror, "Error", "Error while loading Management Team data on screen!");
    //    }
    //}

    //private void ShowMessage(HtmlGenericControl div1, string spanText, string Message)
    //{
    //    clearMessages();
    //    div1.InnerHtml = "<span>" + spanText + "</span>";
    //    div1.InnerHtml += ": ";
    //    div1.InnerHtml += Message;
    //    div1.Visible = true;
    //}

    //private void clearMessages()
    //{
    //    divAlert.InnerHtml = "";
    //    divAlert.Visible = false;
    //    divSuccess.InnerHtml = "";
    //    divSuccess.Visible = false;
    //    diverror.InnerHtml = "";
    //    diverror.Visible = false;
    //    divNotice.InnerHtml = "";
    //    divNotice.Visible = false;
    //}

}