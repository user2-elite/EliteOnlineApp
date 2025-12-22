using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using INTModel;
using System.Net;
using System.Net.Mail;

public partial class Meeting : System.Web.UI.Page
{
    ConfDB objConfDB = new ConfDB();
    INTModel.INTModelContainer objModel = new INTModel.INTModelContainer();
    bool isOrganizer = false;
    bool isAdmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        divAvailability.Visible = false;
        clearMessages();
        if (Session["UserID"] == null || Session["LogonUserFullName"] == null)
        {
            UpdatePanel1.Visible = false;
            return;
        }

        if (Session["selectedRole"] != null)
        {
            if (Session["selectedRole"].ToString() == "1")
            {
                isAdmin = true;
            }
        }
        if (Request.QueryString["id"] != null)
        {
            divBack.Visible = true;
            btnCancel.Visible = true;
            ViewState["MeetingID"] = Request.QueryString["id"].ToString();
        }
        else
        {
            divBack.Visible = false;
            btnCancel.Visible = false;
        }
        if (!Page.IsPostBack)
        {
            FillDropdowns();
            txtTitle.Focus();
            if (ViewState["MeetingID"] != null)
            {
                txtOutCome.Enabled = true;
                fillData(ViewState["MeetingID"].ToString());
            }
            else
            {
                txtOutCome.Enabled = false;
                ViewState["OrganizerID"] = Session["UserID"].ToString();
            }
        }
        if (Request.QueryString["id"] != null)
        {
            if (Request.QueryString["admin"] != null)
            {
                lnkBack.HRef = "adminAllSchedules.aspx?uid=" + ViewState["OrganizerID"].ToString();
            }
            else if (Request.QueryString["allsch"] != null)
            {
                lnkBack.HRef = "AllSchedules.aspx";
            }
            else
            {
                lnkBack.HRef = "home.aspx";
            }
        }

    }

    private void FillDropdowns()
    {
        objConfDB.BindUnit(ddCompanyUnit);
        //objConfDB.BindConfRoom(ddCompanyUnit.SelectedItem.Value, ddConfrooms);
        objConfDB.BindUsers(lstparticipants);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string mailType = "1";
            string MeetingID = "";
            string Meetingdate = txtMeetingDate.Text.ToString();
            if (Meetingdate.Length > 10)
            {
                Meetingdate = Meetingdate.Substring(0, 10);
            }
            Meetingdate = Meetingdate.Trim();
            string StartTime, EndTime;
            StartTime = Meetingdate + " " + txtStartTime.Text.ToString();
            EndTime = Meetingdate + " " + txtEndTime.Text.ToString();
            if (ViewState["MeetingID"] != null)
            {
                MeetingID = ViewState["MeetingID"].ToString();
            }
            string ConfReturn = objConfDB.CheckConf_Room(MeetingID, ViewState["OrganizerID"].ToString(), ddConfrooms.SelectedValue.ToString(), Meetingdate, StartTime, EndTime);
            if (ConfReturn.Length > 0)
            {
                divAvailability.InnerHtml = "<img src='Images/unavailable.png' border='0' />&nbsp;<B>Not Available</B>";
                divAvailability.Visible = true;
                if (ConfReturn == "Error")
                {
                    ShowMessage(diverror, "Error", "Conference Room is not available for the given schedule. Please modify the Time/Conference Room and try again.");
                }
                else
                {
                    ShowMessage(diverror, "Error", "Conference Room is not available for the given schedule. Find the conference status below<BR>" + ConfReturn + "Please contact administrator incase you want to override other users meeting schedules.");
                }
                return;
            }
            if (MeetingID.Length > 0)
            {
                //Update
                mailType = "2";
                MeetingID = ViewState["MeetingID"].ToString();
                List<Conf_UpdateMeeting_Result> objConfUpdList = objModel.Conf_UpdateMeeting(System.Convert.ToInt32(MeetingID), System.Convert.ToInt32(ddConfrooms.SelectedItem.Value), txtTitle.Text.ToString(), ViewState["OrganizerID"].ToString(), System.Convert.ToDateTime(Meetingdate), System.Convert.ToDateTime(StartTime),
                System.Convert.ToDateTime(EndTime), Session["UserID"].ToString(), txtAgenda.Text.ToString(), txtOutCome.Text.ToString(), chkNotify.Checked, 1).ToList();
            }
            else
            {
                List<Conf_AddMeeting_Result> objConfAddList = objModel.Conf_AddMeeting(System.Convert.ToInt32(ddConfrooms.SelectedItem.Value), ViewState["OrganizerID"].ToString(), txtTitle.Text.ToString(), System.Convert.ToDateTime(Meetingdate), System.Convert.ToDateTime(StartTime),
                    System.Convert.ToDateTime(EndTime), Session["UserID"].ToString(), txtAgenda.Text.ToString(), txtOutCome.Text.ToString(), chkNotify.Checked).ToList();

                MeetingID = objConfAddList[0].MeetingID.ToString();
                ViewState["MeetingID"] = MeetingID;

            }
            //check if insert is success, then new meeting id will be created.
            if (MeetingID.Length > 0)
            {
                //string[] ItemVal;
                //string TextValUID, TextValEmail = "", TextValEmailList = "";
                //string x = Request.Form["lstparticipantsSelected"].ToString();                
                //for (int k = 0; k < lstparticipantsSelected.Items.Count; k++)
                //{
                //if (lstparticipants.Items[k].Selected)
                //{
                // ItemVal = lstparticipantsSelected.Items[k].Value.ToString().Split('|');
                //  TextValUID = ItemVal[0].ToString();
                //  TextValEmail = ItemVal[1].ToString();
                //  TextValEmailList += TextValEmail + ";";
                //  objModel.Conf_ADD_Participants(System.Convert.ToInt32(MeetingID), TextValUID, TextValEmail, Session["UserID"].ToString());
                //}
                //}
                string TextValEmailList = "";
                string strListValues = hdnListValues.Value;
                string strListTexts = hdnListTexts.Value;
                //lblRText.Text = hdnListTexts.Value;
                //lblRvalue.Text = hdnListValues.Value;
                string[] arrTexts;
                string[] arrValues;
                arrTexts = strListTexts.Split(',');
                arrValues = strListValues.Split(',');

                string[] ItemVal;
                string TextValUID, TextValEmail = "";
                for (int i = 0; i < arrValues.Length; i++)
                {
                    if (arrValues[i].ToString().Length > 0)
                    {
                        ItemVal = arrValues[i].ToString().Split('|');
                        TextValUID = ItemVal[0].ToString();
                        TextValEmail = ItemVal[1].ToString();
                        if (TextValUID.Length > 0)
                        {
                            TextValEmailList += TextValEmail + ";";
                            objModel.Conf_ADD_Participants(System.Convert.ToInt32(MeetingID), TextValUID, TextValEmail, Session["UserID"].ToString());
                        }
                    }
                }
                try
                {
                    if (chkNotify.Checked)
                    {
                        //    //send email to TextValEmailList.
                        string strVenue = "";
                        strVenue = ddCompanyUnit.SelectedItem.Text.ToString() + " ( " + ddConfrooms.SelectedItem.Text.ToString() + " )";
                        SendMail(mailType, TextValEmailList, Session["Email"].ToString(), txtAgenda.Text.ToString(), txtTitle.Text.ToString(), Meetingdate, strVenue, txtStartTime.Text.ToString(), txtEndTime.Text.ToString(), Session["LogonUserFullName"].ToString(), Session["Email"].ToString());

                        ShowMessage(divSuccess, "Success", "New Meeting Scheduled and an email triggered to all participants.");
                    }
                    else
                    {
                        ShowMessage(divSuccess, "Success", "New Meeting Scheduled. email not triggered to participants.");
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(divSuccess, "Success", "New Meeting Scheduled, however the email not triggered to all participants.");
                }
            }
            else
            {
                ShowMessage(diverror, "Error", "Failed to create meeting schedule. Please verify details and try again");
            }

        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Failed to create meeting schedule. Please verify details and try again");
        }
    }

    private void SendMail(string meetingType, string tomail, string ccmail, string Agenda, string Title, string MeetingDate, string Venue, string StartTime, string EndTime, string Organizer, string FromEmail)
    {

        string MessageText = "<html><head></head><title></title><body>";
        MessageText += "<table cellpadding='3' cellspacing='3' width='100%'><tr><td align='left' valign='top'>Dear Team Member,<BR><BR>";

        if (meetingType == "1")
        {
            MessageText += "<b><font color='blue'>New Meeting has been scheduled in the Intraweb portal</font></b></td></tr>";
        }
        else if (meetingType == "2")
        {
            MessageText += "<b><font color='blue'>Meeting schedule has been updated in the Intraweb portal</font></b></td></tr>";
        }
        else if (meetingType == "3")
        {
            MessageText += "<b><font color='red'>Meeting schedule has been cancelled in the Intraweb portal</font></b></td></tr>";
        }
        else
        {
            MessageText += "</td></tr>";
        }

        MessageText += "<tr><td align='left' valign='top' ><BR><U>Please see the meeting details below:</U></td></tr>";
        MessageText += "<tr><td><BR><HR></td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Organizer: " + Organizer + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'>Meeting Title: " + Title + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'>Date: " + MeetingDate + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'>Location: " + Venue + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'>Start Time: " + StartTime + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'>End Time: " + EndTime + "</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR><B><U>Agenda of the Meeting</U></B></td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'>" + Agenda + "</td></tr>";

        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Please login to www.eliteonline.in to see the meeting updates.</td></tr>";
        MessageText += "<tr><td align='left' valign='top' style='bgcolor:#F0F0F0'><BR>Best Regards, <BR>" + Organizer + "</td></tr>";
        MessageText += "</table>";
        MessageText += "</body></html>";

        string mailsubj = "";
        if (meetingType == "1")
        {
            mailsubj = "New Meeting request ( " + Title + " )";
        }
        else if (meetingType == "2")
        {
            mailsubj = "Meeting request Updated ( " + Title + " )";
        }
        else if (meetingType == "3")
        {
            mailsubj = "Meeting request Cancelled ( " + Title + " )";
        }
        else
        {
            mailsubj = "Meeting request";
        }
        MailMessage message = new MailMessage();
        if (FromEmail.Length == 0)
        {
            FromEmail = "conference@eliteonline.in";
        }
        message.From = new MailAddress(FromEmail);
        message.Subject = mailsubj;
        message.Body = MessageText;

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
        //message.Bcc.Add(new MailAddress(""));
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string MeetingID = "";
        try
        {
            if (ViewState["MeetingID"] != null)
            {
                //Update
                MeetingID = ViewState["MeetingID"].ToString();
                objModel.Conf_CancelMeeting(System.Convert.ToInt32(MeetingID));
                try
                {
                    if (chkNotify.Checked)
                    {
                        string Meetingdate = txtMeetingDate.Text.ToString();
                        string strVenue = "";
                        strVenue = ddCompanyUnit.SelectedItem.Text.ToString() + " ( " + ddConfrooms.SelectedItem.Text.ToString() + " )";
                        if (Meetingdate.Length > 10)
                        {
                            Meetingdate = Meetingdate.Substring(0, 10);
                        }
                        Meetingdate = Meetingdate.Trim();

                        //string TextValEmail = "", TextValEmailList = "";
                        //string[] ItemVal;
                        //for (int k = 0; k < lstparticipantsSelected.Items.Count; k++)
                        //{
                        //    //if (lstparticipants.Items[k].Selected)
                        //    //{
                        //    ItemVal = lstparticipantsSelected.Items[k].Value.ToString().Split('|');
                        //        TextValEmail = ItemVal[1].ToString();
                        //        TextValEmailList += TextValEmail + ";";
                        //    //}
                        //}

                        string TextValEmailList = "";
                        string strListValues = hdnListValues.Value;
                        string[] arrValues;
                        arrValues = strListValues.Split(',');

                        string[] ItemVal;
                        string TextValUID, TextValEmail = "";
                        for (int i = 0; i < arrValues.Length; i++)
                        {
                            if (arrValues[i].ToString().Length > 0)
                            {
                                ItemVal = arrValues[i].ToString().Split('|');
                                TextValUID = ItemVal[0].ToString();
                                TextValEmail = ItemVal[1].ToString();
                                if (TextValUID.Length > 0)
                                {
                                    TextValEmailList += TextValEmail + ";";
                                }
                            }
                        }
                        //send email to TextValEmailList.                        
                        SendMail("3", TextValEmailList, Session["Email"].ToString(), txtAgenda.Text.ToString(), txtTitle.Text.ToString(), Meetingdate, strVenue, txtStartTime.Text.ToString(), txtEndTime.Text.ToString(), Session["LogonUserFullName"].ToString(), Session["Email"].ToString());
                        ShowMessage(divSuccess, "Success", "Meeting schedule cancelled and sent an email to all recepients");
                    }
                    else
                    {
                        ShowMessage(divSuccess, "Success", "Meeting schedule cancelled. email not triggered to participants");
                    }
                }
                catch
                {
                    ShowMessage(divSuccess, "Success", "Meeting schedule cancelled, however the email not triggered to participants.");
                }

                disableFields();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Failed to cancel the meeting schedule. Please try again");
        }
    }

    private void ShowMessage(HtmlGenericControl div1, string spanText, string Message)
    {
        clearMessages();
        div1.InnerHtml = "<span>" + spanText + "</span>";
        div1.InnerHtml += ": ";
        div1.InnerHtml += Message;
        div1.Visible = true;
    }

    private void clearMessages()
    {
        divAlert.InnerHtml = "";
        divAlert.Visible = false;
        divSuccess.InnerHtml = "";
        divSuccess.Visible = false;
        diverror.InnerHtml = "";
        diverror.Visible = false;
        divNotice.InnerHtml = "";
        divNotice.Visible = false;
    }

    protected void ddCompanyUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        objConfDB.BindConfRoom(ddCompanyUnit.SelectedItem.Value, ddConfrooms);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ////string[] ItemVal;
        //string ItemText = "";
        //string ItemAppend = "";
        ////string TextValUID, TextValEmail = "";
        //txtParticipants.Text = "";

        //for (int k = 0; k < lstparticipants.Items.Count; k++)
        //{
        //    if (lstparticipants.Items[k].Selected)
        //    {
        //        //ItemVal = lstparticipants.Items[k].Value.ToString().Split('|');
        //        //TextValUID = ItemVal[0].ToString();
        //        //TextValEmail = ItemVal[1].ToString();
        //        ItemText = lstparticipants.Items[k].Text;
        //        if (ItemAppend.Length > 0)
        //        {
        //            ItemAppend += ", " + ItemText;
        //        }
        //        else
        //        {
        //            ItemAppend = ItemText;
        //        }
        //    }
        //}
        //txtParticipants.Text = ItemAppend;
    }

    private void disableFields()
    {
        txtTitle.Enabled = false;
        txtAgenda.Enabled = false;
        txtEndTime.Enabled = false;
        txtMeetingDate.Enabled = false;
        txtOutCome.Enabled = false;
        txtStartTime.Enabled = false;
        ddCompanyUnit.Enabled = false;
        ddConfrooms.Enabled = false;
        //btnAdd.Enabled = false;
        btnSubmit.Enabled = false;
        btnCancel.Enabled = false;
        chkNotify.Enabled = false;
    }

    private void fillData(string ID)
    {

        try
        {
            btnSubmit.Text = "Update Meeting";
            DataSet ds = new DataSet();
            ds = objConfDB.GetConf_RoomBYID(ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtTitle.Text = ds.Tables[0].Rows[0]["Meeting_Title"].ToString();
                txtAgenda.Text = ds.Tables[0].Rows[0]["Meeting_description"].ToString();
                txtEndTime.Text = ds.Tables[0].Rows[0]["conf_end_time"].ToString();
                txtOutCome.Text = ds.Tables[0].Rows[0]["Meeting_outcome"].ToString();
                txtStartTime.Text = ds.Tables[0].Rows[0]["conf_start_time"].ToString();
                ddCompanyUnit.SelectedValue = ds.Tables[0].Rows[0]["UnitID"].ToString();
                ViewState["OrganizerID"] = ds.Tables[0].Rows[0]["UID"].ToString();
                string Meetingdate = ds.Tables[0].Rows[0]["conf_date"].ToString();
                if (Meetingdate.Length > 10)
                {
                    Meetingdate = Meetingdate.Substring(0, 10);
                }
                Meetingdate = Meetingdate.Trim();
                txtMeetingDate.Text = Meetingdate;
                try
                {
                    objConfDB.BindConfRoom(ddCompanyUnit.SelectedItem.Value, ddConfrooms);
                    ddConfrooms.SelectedValue = ds.Tables[0].Rows[0]["cr_id"].ToString();
                }
                catch { }
                //txtParticipants.Text = ds.Tables[0].Rows[0]["sssss"].ToString();
                if (Session["UserID"].ToString().ToLower().Trim() == ViewState["OrganizerID"].ToString().ToLower().Trim())
                {
                    isOrganizer = true;
                }
            }
            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //}
            //string strRecepients = "";
            if (ds.Tables[1].Rows.Count > 0)
            {
                lstparticipants.SelectionMode = ListSelectionMode.Multiple;
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    try
                    {
                        string ListValue = ds.Tables[1].Rows[i]["RecepientUID"].ToString() + "|" + ds.Tables[1].Rows[i]["RecepientEmail"].ToString();
                        //ListItem item1 = new ListItem();
                        //item1.Text = ds.Tables[1].Rows[i]["RecepientUID"].ToString();
                        //item1.Value = ListValue;
                        //lstparticipantsSelected.Items.Add(item1);
                        //lstparticipants.SelectedValue = ListValue;
                        //ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(),
                        //   "myFunction", "MoveItem('lstparticipants', 'lstparticipantsSelected');", true);
                        lstparticipants.Items.FindByValue(ListValue).Selected = true;                        
                        /*if (strRecepients.Length > 0)
                        {
                            strRecepients += ", " + ds.Tables[1].Rows[i]["RecepientUID"].ToString();
                        }
                        else
                        {
                            strRecepients = ds.Tables[1].Rows[i]["RecepientUID"].ToString();
                        }*/
                    }
                    catch { }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "MoveItem('lstparticipants', 'lstparticipantsSelected');", true);  
            }
            //txtParticipants.Text = strRecepients;
        }
        catch
        {
        }
        if (!isOrganizer && !isAdmin)
        {
            disableFields();
        }
    }

    protected void lnkCheckAvailability_Click(object sender, EventArgs e)
    {
        string Meetingdate = txtMeetingDate.Text.ToString();
        if (Meetingdate.Length > 10)
        {
            Meetingdate = Meetingdate.Substring(0, 10);
        }
        Meetingdate = Meetingdate.Trim();
        string StartTime, EndTime;
        StartTime = Meetingdate + " " + txtStartTime.Text.ToString();
        EndTime = Meetingdate + " " + txtEndTime.Text.ToString();
        string MeetingID = "";
        if (ViewState["MeetingID"] != null)
        {
            MeetingID = ViewState["MeetingID"].ToString();
        }
        string ConfReturn = objConfDB.CheckConf_Room(MeetingID, ViewState["OrganizerID"].ToString(), ddConfrooms.SelectedValue.ToString(), Meetingdate, StartTime, EndTime);
        divAvailability.Visible = true;
        if (ConfReturn.Length > 0)
        {
            if (ConfReturn == "Error")
            {
                divAvailability.InnerHtml = "<img src='Images/unavailable.png' border='0' />&nbsp;<B>Not Available</B>";
            }
            else
            {
                divAvailability.InnerHtml = "<img src='Images/unavailable.png' border='0' />&nbsp;<font color='red'><B>Not Available.</B> Find the conference room booking status below</font><BR>" + ConfReturn + "<font color='red'>Please contact administrator incase you want to override the above schedule.</font>";
            }
        }
        else
        {
            divAvailability.InnerHtml = "<img src='Images/available.png' border='0' />&nbsp;<B>Available</B>";
        }
    }

}