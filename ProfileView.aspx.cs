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

public partial class ProfileView : System.Web.UI.Page
{
    //protected int ProfileCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindList();
    }

    private void BindList()
    {
        try
        {
            DataSet  ds = new DataSet();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_All_MGMTList_ForMain";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        string divContent = "";
                        sda.Fill(ds);
                        //ProfileCount = dt.Rows.Count;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[0].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[0].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[0].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[0].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                //divContent += "<B>"+ ds.Tables[0].Rows[i]["MGMT_Desig"].ToString() +"</B>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";
                                
                                if (ds.Tables[0].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";
                            }
                        }
                        divmyPopup1.InnerHtml = divContent;
                        divContent = "";

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[1].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");

                                //profileText = "Test test test Test test test Test test test Test test test Test test test Test test test";
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[1].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[1].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[1].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";

                                if (ds.Tables[1].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";
                            }
                        }
                        divmyPopup2.InnerHtml = divContent;
                        divContent = "";

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[2].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[2].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[2].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[2].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";

                                if (ds.Tables[2].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";

                            }
                        }
                        divmyPopup3.InnerHtml = divContent;
                        divContent = "";

                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[3].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[3].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[3].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[3].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";

                                if (ds.Tables[3].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";
                            }
                        }
                        divmyPopup4.InnerHtml = divContent;
                        divContent = "";

                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[4].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[4].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[4].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[4].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";

                                if (ds.Tables[4].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";
                            }
                        }
                        divmyPopup5.InnerHtml = divContent;
                        divContent = "";

                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[5].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[5].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[5].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[5].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";

                                if (ds.Tables[5].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";
                            }
                        }
                        divmyPopup6.InnerHtml = divContent;
                        divContent = "";

                         if (ds.Tables[6].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                            {
                                string profileText = ds.Tables[6].Rows[i]["MGMT_Profile"].ToString();
                                profileText = profileText.Replace("<P>", "</br>");
                                profileText = profileText.Replace("</P>", "</br>");
                                profileText = profileText.Replace("<p>", "</br>");
                                profileText = profileText.Replace("</p>", "</br>");
                                divContent += "<div class='media'>";
                                //divContent += "<div class='pure-u-1 pure-u-md-1-3' >";
                                divContent += "<p class='pull-left'><img src='Gallery/Management/" + ds.Tables[6].Rows[i]["MGMT_Picture_Name"].ToString() + "' class='img-thumbnail'/></p>";
                                divContent += "<div class='media-body'>";
                                divContent += "<B>" + ds.Tables[6].Rows[i]["MGMT_Name"].ToString() + " (" + ds.Tables[6].Rows[i]["MGMT_Desig"].ToString() + ")</B><BR>";
                                divContent += profileText;
                                divContent += "</div>";
                                //divContent += "</div>";

                                if (ds.Tables[6].Rows.Count > 1)
                                {
                                    divContent += "<HR>";
                                }
                                divContent += "</div>";
                            }
                        }
                         divmyPopup7.InnerHtml = divContent;
                        divContent = "";


                        
                    }
                    ds.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            //ShowMessage(diverror, "Error", "Error while loading Management Team data on screen!");
        }
    }

}