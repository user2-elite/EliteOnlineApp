using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

public partial class Home : System.Web.UI.Page
{
    protected string UserID = "";
    protected string TravelInfo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["BDay"] == null)
            {
                string bDaList = GetBdayList();
                if (bDaList.Length > 0)
                {
                    divEmpBday.InnerHtml = bDaList;
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "bday", "<script type='text/javascript'>openBirthdayDialog();</script>", false);
                    //divPopups.Visible = true;
                    divPopups.Visible = false;
                    Session["BDay"] = "1";
                }
                else
                {
                    divPopups.Visible = false;
                }
            }
            else
            {
                divPopups.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
        divSuccess.Visible = false;
        divError.Visible = false;
        liSettings.Visible = false;
        liDashboard.Visible = false;
        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            UserID = Session["UserID"].ToString();
            lblLogonuser.Text = Session["LogonUserFullName"].ToString();

            if (Cache["TravelDetails"] == null)
            {
                FillCacheSchedule();
            }

            if (Cache["QuoteFortheDay"] == null)
            {
                FillCacheQuote();
            }

            if (Cache["ExtraLinks"] == null)
            {
                FillCacheExtraLinks();
            }
            if (Cache["LatestNews"] == null)
            {
                FillCacheNews();
            }
            if (Cache["MGMTTalk"] == null)
            {
                FillCacheMGMT();
            }

            if (Cache["GalleryItem"] == null)
            {
                FillCacheGallery();
            }

            //Traveler Info
            if (Cache["TravelDetails"] != null)
            {
                TravelInfo = Cache["TravelDetails"].ToString();
            }
            else
            {
                TravelInfo = "Currently no information available.";
            }
            TravelInfo += "</BR></BR>";

            //Quote for the day
            if (Cache["QuoteFortheDay"] != null)
            {
                divQuote.InnerHtml = Cache["QuoteFortheDay"].ToString();
            }
            else
            {
                divQuote.InnerHtml = Cache["QuoteFortheDay"].ToString();
            }

            //Settings Link
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() == "1" || Session["selectedRole"].ToString() == "2" || Session["selectedRole"].ToString() == "4"  || Session["selectedRole"].ToString() == "5")
                {
                    liSettings.Visible = true;
                }

                if (Session["selectedRole"].ToString() == "5" || Session["selectedRole"].ToString() == "3" || Session["selectedRole"].ToString() == "1")
                {
                    liDashboard.Visible = true;
                }
            }
            //Left Links
            try
            {
                DataTable dtLinks;
                dtLinks = (DataTable)Cache["ExtraLinks"];
                if (dtLinks.Rows.Count > 0)
                {
                    string leftLinks = "";
                    leftLinks += "<ul class='list-unstyled text-right'>";
                    for (int linkIndex = 0; linkIndex < dtLinks.Rows.Count; linkIndex++)
                    {
                        leftLinks += "<li style='padding: 3px;'><a href='page.aspx?id=" + dtLinks.Rows[linkIndex]["ID"].ToString() + "'>" + dtLinks.Rows[linkIndex]["Page_Link_Name"].ToString() + "</a></li>";
                    }
                    leftLinks += "</ul>";
                    divLeftLinks.InnerHtml = leftLinks;
                }
            }
            catch
            {

            }
            //Latest News
            try
            {
                DataTable dtNews;
                dtNews = (DataTable)Cache["LatestNews"];
                if (dtNews.Rows.Count > 0)
                {
                    string newsID = dtNews.Rows[0]["ID"].ToString();
                    string newsText = dtNews.Rows[0]["News_Title"].ToString();
                    divNewsText.InnerHtml = newsText;
                    lnkNewsID.HRef = "eventItem.aspx?ID=" + newsID;
                    lnkNewsID.Style.Add("color", "yellow");
                    imgNews.Src = "~/Gallery/News/" + dtNews.Rows[0]["News_Picture_Name"].ToString();
                }
            }
            catch
            {

            }

            //MGMT Talk
            try
            {
                DataTable dtMGMT;
                //divMGMTTalk
                dtMGMT = (DataTable)Cache["MGMTTalk"];
                if (dtMGMT != null && dtMGMT.Rows.Count > 0)
                {
                    string mid = dtMGMT.Rows[0]["ID"].ToString();
                    string Title = dtMGMT.Rows[0]["MGMT_Title"].ToString();
                    Title += "<BR><BR><font color='maroon'>Written By: " + dtMGMT.Rows[0]["MGMT_Author"].ToString() + "</font>";
                    divMGMTTalk.InnerHtml = Title;
                    lnkMGMTTalk.HRef = "MGMTTalk.aspx?ID=" + mid;
                    lnkMGMTTalk.Style.Add("color", "lightblue");
                    imgMGMTTalk.Src = "~/Gallery/Management/" + dtMGMT.Rows[0]["MGMT_Photo_Name"].ToString();
                }
                else
                {
                    divMGMTTalk.InnerHtml = "[Data not available]";
                    imgMGMTTalk.Src = "~/Gallery/mgmt.png";
                }
            }
            catch
            {

            }

            //Gallery Pictures
            try
            {
                if (Cache["GalleryItem"] != null)
                {
                    DataTable dtGallery;
                    dtGallery = (DataTable)Cache["GalleryItem"];
                    if (dtGallery.Rows.Count > 0)
                    {
                        string GID = dtGallery.Rows[0]["GalleryGroupID"].ToString();
                        string GTitle = dtGallery.Rows[0]["Title"].ToString();
                        lnkFirstSlideImage.HRef = "galleryItem.aspx?ID=" + GID;
                        imgFirstSlide.Src = "~/Gallery/SlideImages/" + dtGallery.Rows[0]["ImageName"].ToString();


                        GID = dtGallery.Rows[1]["GalleryGroupID"].ToString();
                        GTitle = dtGallery.Rows[1]["Title"].ToString();
                        lnkSecondSlideImage.HRef = "galleryItem.aspx?ID=" + GID;
                        imgSecondSlide.Src = "~/Gallery/SlideImages/" + dtGallery.Rows[1]["ImageName"].ToString();

                        GID = dtGallery.Rows[2]["GalleryGroupID"].ToString();
                        GTitle = dtGallery.Rows[2]["Title"].ToString();
                        lnkThirdSlideImage.HRef = "galleryItem.aspx?ID=" + GID;
                        imgThirdSlide.Src = "~/Gallery/SlideImages/" + dtGallery.Rows[2]["ImageName"].ToString();

                    }
                }
            }
            catch
            {

            }

        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }


    private string GetBdayList()
    {
        string strList = "";
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_BDayNames";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            strList = dt.Rows[0]["OutPutValue"].ToString();
                        }
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
            return strList;
        }
        catch (Exception ex)
        {
            return strList;
        }
    }


    private void FillCacheQuote()
    {
        try
        {
            if (Cache["QuoteFortheDay"] != null)
            {
                Cache.Remove("QuoteFortheDay");
            }
            Cache["QuoteFortheDay"] = "[Not Available]";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_LatestQuoteText";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string Quote = dt.Rows[0]["QuoteText"].ToString();
                            if (Quote.Trim().Length > 0)
                            {
                                if (Cache["QuoteFortheDay"] != null)
                                {
                                    Cache.Remove("QuoteFortheDay");
                                }
                                Cache.Insert("QuoteFortheDay", Quote);
                            }
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Cache["QuoteFortheDay"] = "[Not Available]";
        }
    }

    private void FillCacheMGMT()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_Latest_MGMT_Talk";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["MGMTTalk"] != null)
                            {
                                Cache.Remove("MGMTTalk");
                            }
                            Cache.Insert("MGMTTalk", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }


    private void FillCacheGallery()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT top 3 GalleryGroupID,ID,ImageName,Title,Description FROM INT_GalleryItems ORDER BY ID DESC";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["GalleryItem"] != null)
                            {
                                Cache.Remove("GalleryItem");
                            }
                            Cache.Insert("GalleryItem", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void FillCacheExtraLinks()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_ExtraLinks_ForHome";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["ExtraLinks"] != null)
                            {
                                Cache.Remove("ExtraLinks");
                            }
                            Cache.Insert("ExtraLinks", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void FillCacheNews()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_Latest_News";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["LatestNews"] != null)
                            {
                                Cache.Remove("LatestNews");
                            }
                            Cache.Insert("LatestNews", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void FillCacheSchedule()
    {
        try
        {
            INTModel.INTModelContainer objEntities = new INTModel.INTModelContainer();
            List<INTModel.INT_Get_Travel_Schedule_Result> objListAll = objEntities.INT_Get_Travel_Schedule().ToList();
            if (objListAll.Count > 0)
            {
                if (Cache["TravelDetails"] != null)
                {
                    Cache.Remove("TravelDetails");
                }
                string TravelInfo = objListAll[0].Travel_Details.ToString().Replace('\"', '\'');
                Cache.Insert("TravelDetails", TravelInfo);
            }
            else
            {
                if (Cache["TravelDetails"] != null)
                {
                    Cache.Remove("TravelDetails");
                }
            }
        }
        catch
        {

        }
    }

    protected void btnSendFeedback_Click(object sender, EventArgs e)
    {
        string strName = txtEmpName.Text.ToString();
        string strFeedbackOn = ddlSuggestionType.SelectedItem.Text.ToString();
        string strFeedBack = txtFeedback.Text.ToString();

        string MessageText = "<html><head></head><title></title><body>";
        MessageText += "<table cellpadding='3' cellspacing='3' width='100%'><tr><td align='left' valign='top'>Dear HR Team,<BR><BR>";
        MessageText += "<b>New Suggestion/Feedback received from Intraweb portal</b></td></tr>";
        MessageText += "<tr><td><BR><U>Please see the feedback/suggestion details below:</U></td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Employee Name: " + strName + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Employee User ID: " + UserID + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Suggestion/feedback On: " + strFeedbackOn + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Suggestion/feedback Given: " + strFeedBack + "</td></tr>";
        MessageText += "</table>";
        MessageText += "</body></html>";

        string mailsubj = "Employee Suggestion/Feedback from Intraweb";

        try
        {
            string ToEMailId = "suggestions@eliteindia.com";
            string CCEmail = "";
            string BCCEmail = "";
            MailMessage message = new MailMessage();
            message.From = new MailAddress("feedback@eliteonline.in");
            message.Subject = mailsubj;
            message.Body = MessageText;

            //To be uncommented
            message.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id
            if (CCEmail.Length > 0)
            {
                message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
            }
            if (BCCEmail.Length > 0)
            {
                message.Bcc.Add(new MailAddress(BCCEmail));
            }
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.rediffmailpro.com";
            //smtp.Host = "202.137.236.12";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential("manojk@eliteindia.com", "molu97");
            smtp.EnableSsl = false;
            smtp.Send(message);
            divSuccess.Visible = true;
        }
        catch (Exception ex)
        {
            divError.Visible = true;
        }
    }
}