using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewRequest : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    string RequestID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Write("Close the browser and login again.");
            Response.End();
        }
        string strTitle = "&nbsp;&nbsp;&nbsp;&nbsp;Elite IT SupportDesk&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        Page.Title = strTitle;
        if (Session["masterChecking"] == null)
        {
            Response.End();
        }
        Page.ClientScript.RegisterClientScriptInclude("UniqueKey", "NameControl.js");

        ViewattachedTable.Style.Add("ViewHistoryPos", "z-index: 100; left: 5px; position: absolute; top: 200px");

        if (!IsPostBack)
        {
            DoVisibilityCheck();

            ViewState["SeverityChangeCheck"] = "0";

            ViewState["AccessedByRegisered"] = "0";

            RequestID = Request.QueryString["ReqID"].ToString();

            if (Request.QueryString["Requestaction"] != null)
                ViewState["Requestaction"] = Request.QueryString["Requestaction"].ToString();
            else
                ViewState["Requestaction"] = "";

            if (RequestID != "")
            {

                GetAdminStatus();
                FillRequestInfo(RequestID);

                if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) > 10)
                {
                    FileUpload1.Visible = false;
                    UploadFile.Visible = false;
                }
            }
            else
            {
                Response.Write("Unauthorized Access");
                Response.End();//Not authorised to view this page
            }


            if ((ViewState["StatusHidden"].ToString() == "1") || (ViewState["StatusHidden"].ToString() == "2" || ViewState["StatusHidden"].ToString() == "3" || ViewState["StatusHidden"].ToString() == "5") && (ViewState["Requestaction"].ToString() != "userview"))
            {
                MessageTr.Visible = true;
                MessageButton.Visible = false;
                MessageLabel.Text = "Additional Notes";
            }

            if ((ViewState["admincheck"].ToString() == "1" || ViewState["admincheck"].ToString() == "2") && (Session["selectedRole"].ToString() == "1" || Session["selectedRole"].ToString() == "2") && (ViewState["Requestaction"].ToString() != "userview")) //if accessed by Admin OR Cordinator and selected role is Admin/Resolver
            {
                SelectedRoleprivilagesetting();
                if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) <= 9)
                {
                    CBsendMessage.Enabled = true;
                    CBsendMessage.Text = "Send Message to user";
                }
            }
            else if (ViewState["AssingedTo"].ToString() == Session["UserID"].ToString() && (ViewState["Requestaction"].ToString() != "userview")) //if accessed by a resolver
            {
                SelectedRoleprivilagesetting();
                if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) <= 9)
                {
                    CBsendMessage.Enabled = true;
                    CBsendMessage.Text = "Send Message to user";

                }
            }
            else if (ViewState["RegisteredBy"].ToString() == Session["UserID"].ToString() || ViewState["RegisteredbyUserID"].ToString().ToLower().Trim() == Session["UserID"].ToString().ToLower().Trim()) //If accessed by the employee who registered the Request.
            {
                //This part is taken in the blow section by checking the user is accessing the Request details from the default.aspx page.
                //If user is accessing it from default.aspx, even if he has a role in the support desk, he will have only the privilages of a normal user.
                ViewState["AccessedByRegisered"] = "1";
            }
            else if (ViewState["admincheck"].ToString() == "3") //Request is assigned to somebody but accessed by a resolver.
            {
                SelectedRoleprivilagesetting();
                if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) <= 9)
                {
                    hideRequestmenues();
                    if (ViewState["StatusHidden"].ToString() != "4" || ViewState["StatusHidden"].ToString() != "6")
                    {
                        MessageTr.Visible = true;
                        MessageButton.Visible = true;
                        MessageButton.Text = "Assign this Request to Me";
                        MessageLabel.Text = "Reasons for Taking this Request:";
                        MessageButton.CommandArgument = "SelfAssignment";
                        FileUpload1.Visible = false;
                        UploadFile.Visible = false;

                    }
                }
            }
            else
            {
                lblError.Text = "Unauthorized access";
            }

            if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) > 9) //If Request is closed or invalid no action can be done on the Request.
                hideRequestmenues();

            if (ViewState["AccessedByRegisered"].ToString() == "1" || (ViewState["Requestaction"].ToString() == "userview" && (ViewState["admincheck"].ToString() == "1" || ViewState["admincheck"].ToString() == "2" || ViewState["admincheck"].ToString() == "3")))
            {
                hideRequestmenues();
                ErrorLabel1.Visible = false;
                CBsendMessage.Visible = true;
                lblTimeLeftForResolution.Visible = false;
                TimeLeftForResolution.Visible = false;
                lblHrs.Visible = false;

                CBsendMessage.Text = "Send Message to Support Staff";

                /*if (ViewState["StatusHidden"].ToString() == "11")
                {
                    //Provision for rate the Request
                    CBsendMessage.Visible = false;

                    if (Convert.ToDateTime(ViewState["ClosureCompleteDate"].ToString()).AddDays(5) > CommonClass.GetDateTimeIST())
                    {
                        if (ViewState["RatingRequestid"].ToString() == "0")
                        {
                            RateRequestTR.Visible = true;
                            UserRating.Visible = true;
                        }
                    }
                }
                else if (ViewState["StatusHidden"].ToString() == "10")
                {
                    //Provision for Rejecting the Request resolutions.
                    CBsendMessage.Visible = false;
                    UI_TRID_RequestAcceptance.Visible = true;
                    MessageTr.Visible = true;
                    MessageButton.Text = "Submit";
                    MessageLabel.Text = "Accept/Reject Reasons :";
                    MessageButton.CommandArgument = "AccepetReject";
                    MessageRequiredvalidator.Enabled = false;

                }
                else*/
                if (ViewState["StatusHidden"].ToString() == "4")
                {
                    CBsendMessage.Visible = false;
                    MessageTr.Visible = true;
                    MessageButton.Text = "Release Pending Status";
                    MessageLabel.Text = "Note to the Support Staff : ";
                    MessageButton.CommandArgument = "ReleasePendingStatus";
                }
            }

            if (ViewState["EmailIDoftheExitedemp"].ToString().Trim() != "") // If it is an external user.
            {
                CBsendMessage.Enabled = false; //Messaging option is disabled.
            }

        }

    }

    protected void SelectedRoleprivilagesetting()
    {
        if ((ViewState["StatusHidden"].ToString() == "4") && !(ViewState["admincheck"].ToString() == "1" || ViewState["admincheck"].ToString() == "2"))
        {
            //If Request is put under Pending to user
            hideRequestmenues();
        }
        else if ((ViewState["StatusHidden"].ToString() == "6"))
        {
            //if External support is requested
            hideRequestmenues();
            CBsendMessage.Enabled = true;
            SaveRequest.Visible = true;
        }
        else if (ViewState["StatusHidden"].ToString() == "10" || ViewState["StatusHidden"].ToString() == "11")
        {
            CBsendMessage.Enabled = false;
            hideRequestmenues();
        }


        if (!(ViewState["StatusHidden"].ToString() == "1" || ViewState["StatusHidden"].ToString() == "10" || ViewState["StatusHidden"].ToString() == "11" || DDGroup.SelectedValue == "0" || SeverityDD.SelectedValue == "0"))
        {
            ViewState["proceed"] = "0";
            checkviolations();
            if (ViewState["proceed"].ToString() == "1")
            {
                ViewState["proceed"] = "0";
                return;
            }
        }
    }

    protected void FillRequestInfo(string RequestID)
    {
        Requestclosuredestr.Visible = false;
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            cmd = new SqlCommand("GetALLActiveRequestsFullDetails1", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RequestID", RequestID);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = null;
            ds = new DataSet();
            da.Fill(ds);

            //SqlDataReader dr = cmd.ExecuteReader();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //To decide the rating TR should be visible or not
                if (ds.Tables[0].Rows[0]["RatingRequestid"].ToString() == "")
                {
                    ViewState["RatingRequestid"] = "0";
                    UserRating.Visible = true;
                }
                else
                {
                    ViewState["RatingRequestid"] = "1";
                    UserSatisfaction.SelectedValue = ds.Tables[0].Rows[0]["Rating"].ToString();
                    FeedbackText.Text = ds.Tables[0].Rows[0]["Feedback"].ToString();
                    UserSatisfaction.Enabled = false;
                    FeedbackText.Enabled = false;
                    RateRequestTR.Visible = true;
                    UserRating.Visible = false;
                }

                if (ds.Tables[0].Rows[0]["Transferno"].ToString().ToLower() == "" || ds.Tables[0].Rows[0]["Transferno"].ToString().ToLower() == "0")
                    ViewState["Transferno"] = "0";
                else
                    ViewState["Transferno"] = "1";

                ViewState["HLD_Email"] = ds.Tables[0].Rows[0]["HLD_Email"].ToString().ToLower();

                ViewState["EmailIDoftheExitedemp"] = ds.Tables[0].Rows[0]["EmailIDoftheExitedemp"].ToString();
                ViewState["RegisteredBy"] = ds.Tables[0].Rows[0]["RegisteredBy"].ToString();
                ViewState["RegisteredbyUserID"] = ds.Tables[0].Rows[0]["RegisteredbyUserID"].ToString();

                ViewState["RequestHelpDeskID"] = ds.Tables[0].Rows[0]["RequestHelpDeskID"].ToString();
                ViewState["AssingedTo"] = ds.Tables[0].Rows[0]["AssingedTo"].ToString();
                ViewState["AssignedtoEmail"] = ds.Tables[0].Rows[0]["AssignedtoEmail"].ToString();

                ViewState["AdminMember"] = ds.Tables[0].Rows[0]["AdminMember"].ToString();
                ViewState["AdminEmail"] = ds.Tables[0].Rows[0]["AdminEmail"].ToString();

                ViewState["Location"] = ds.Tables[0].Rows[0]["Location"].ToString();
                ViewState["Department"] = ds.Tables[0].Rows[0]["Department"].ToString();
                ViewState["Area"] = ds.Tables[0].Rows[0]["Area"].ToString();

                DepartmentLB.Text = ViewState["Department"].ToString();
                LocationLB.Text = ViewState["Location"].ToString();
                AreaLB.Text = ViewState["Area"].ToString();

                RequestIDLB.Text = ds.Tables[0].Rows[0]["RequestID"].ToString();
                divRequestDesc.InnerHtml = ds.Tables[0].Rows[0]["RequestDescription"].ToString();
                SubjectLB.Text = ds.Tables[0].Rows[0]["RequestSubject"].ToString();

                EmpNoLB.Text = ds.Tables[0].Rows[0]["RegisteredBy"].ToString();

                if (ds.Tables[0].Rows[0]["EmpName"].ToString() != "")
                {
                    EmpNameLB.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                    EmailLB.Text = ds.Tables[0].Rows[0]["EmpEmail"].ToString();
                    ViewState["EmpEmail"] = ds.Tables[0].Rows[0]["EmpEmail"].ToString();
                    ExtnLB.Text = ds.Tables[0].Rows[0]["PHONT_EXTN"].ToString();
                }
                else
                {
                    string empid = ds.Tables[0].Rows[0]["RegisteredBy"].ToString();
                    EmpNameLB.Text = "";
                    EmailLB.Text = "";
                    ViewState["EmpEmail"] = "";
                    ExtnLB.Text = "";

                }


                AttendedByLB.Text = ds.Tables[0].Rows[0]["AdminName"].ToString();

                if (ds.Tables[0].Rows[0]["Atteneddate"].ToString() != "")
                    AttendedDateLB.Text = ds.Tables[0].Rows[0]["Atteneddate"].ToString();

                AssignedToLB.Text = ds.Tables[0].Rows[0]["NAME"].ToString();

                if (ds.Tables[0].Rows[0]["AssingedDate"].ToString() != "")
                    AssignedDateLB.Text = ds.Tables[0].Rows[0]["AssingedDate"].ToString();

                ViewState["AssingedDate"] = ds.Tables[0].Rows[0]["AssingedDate"].ToString();

                if (ds.Tables[0].Rows[0]["Lastupdateddate"].ToString() != "")
                    LBLastupdatedDate.Text = ds.Tables[0].Rows[0]["Lastupdateddate"].ToString();

                ViewState["Lastupdateddate"] = ds.Tables[0].Rows[0]["Lastupdateddate"].ToString();

                StatusLB.Text = ds.Tables[0].Rows[0]["StatusName"].ToString();
                SeverityLB.Text = ds.Tables[0].Rows[0]["SeverityName"].ToString();

                if (ds.Tables[0].Rows[0]["RequestStatus"].ToString() == "10" || ds.Tables[0].Rows[0]["RequestStatus"].ToString() == "11")
                {
                    Requestclosuredestr.Visible = true;
                    ClosureDesc.Text = ds.Tables[0].Rows[0]["ClosureDesc"].ToString();
                    ClosureDesc.ReadOnly = true;

                    string strActualHours = ds.Tables[0].Rows[0]["ActualHours"].ToString();
                    string strActualMins = ds.Tables[0].Rows[0]["ActualMins"].ToString();
                    try
                    {
                        txtHours.Text = strActualHours;
                        ddlMins.SelectedValue = strActualMins;
                    }
                    catch { }
                    //    string strTimeTaken = ds.Tables[0].Rows[0]["ActualTimeTaken"].ToString();
                    //    if (strTimeTaken.Length > 0)
                    //    {
                    //        try
                    //        {
                    //            string[] timeTaken = ds.Tables[0].Rows[0]["ActualTimeTaken"].ToString().Split(':');
                    //            if (timeTaken[0].ToString().Length > 0)
                    //            {
                    //                txtHours.Text = timeTaken[0].ToString();
                    //            }
                    //            else
                    //            {
                    //                txtHours.Text = "0";
                    //            }
                    //            if (timeTaken[1].ToString().Length > 0)
                    //            {
                    //                ddlMins.SelectedValue = timeTaken[1].ToString();
                    //            }
                    //            else
                    //            {
                    //                ddlMins.SelectedValue = "0";
                    //            }
                    //        }
                    //        catch { }
                    //    }


                }


                else
                {
                    ClosureDesc.Text = "";
                    Requestclosuredestr.Visible = false;
                    ClosureDesc.ReadOnly = false;
                }

                if (ds.Tables[0].Rows[0]["ResponseTimeViolated"].ToString() == "1")
                {

                    Responseviolationtr.Visible = true;
                    ResponseViolationText.Text = ds.Tables[0].Rows[0]["ResponseViolationText"].ToString();
                    ResponseViolationText.ReadOnly = true;
                    Responsetimeviolationlb.Text = "Yes";
                }
                else if (ds.Tables[0].Rows[0]["ResponseTimeViolated"].ToString() == "0")
                {
                    Responseviolationtr.Visible = false;
                    ResponseViolationText.Text = "";
                    Responsetimeviolationlb.Text = "No";
                    ResponseViolationText.ReadOnly = false;
                }
                else
                {
                    Responseviolationtr.Visible = false;
                    ResponseViolationText.Text = "";
                    Responsetimeviolationlb.Text = "";
                    ResponseViolationText.ReadOnly = false;
                }

                if (ds.Tables[0].Rows[0]["ResolutionTimeViolated"].ToString() == "1")
                {
                    Resolutionviolationtr.Visible = true;
                    ResolutionViolationText.Text = ds.Tables[0].Rows[0]["ResolutionViolationText"].ToString();
                    ResolutionViolationText.ReadOnly = true;
                    Resolutiontimeviolationlb.Text = "Yes";
                }
                else if (ds.Tables[0].Rows[0]["ResolutionTimeViolated"].ToString() == "0")
                {
                    Resolutionviolationtr.Visible = false;
                    ResolutionViolationText.Text = "";
                    Resolutiontimeviolationlb.Text = "No";
                    ResolutionViolationText.ReadOnly = false;
                }
                else
                {
                    Resolutionviolationtr.Visible = false;
                    ResolutionViolationText.Text = "";
                    Resolutiontimeviolationlb.Text = "";
                    ResolutionViolationText.ReadOnly = false;
                }

                ViewState["AutoClousureDate"] = ds.Tables[0].Rows[0]["AutoClousureDate"].ToString();

                if (ds.Tables[0].Rows[0]["ExpectedResponseDtTime"].ToString() != "")
                    ResponseBy.Text = ds.Tables[0].Rows[0]["ExpectedResponseDtTime"].ToString();

                if (ds.Tables[0].Rows[0]["ExpectedResolutionDtTime"].ToString() != "")
                    ResolutionBy.Text = ds.Tables[0].Rows[0]["ExpectedResolutionDtTime"].ToString();

                CreateDateLB.Text = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                ViewState["CreatedDate"] = ds.Tables[0].Rows[0]["CreatedDate"].ToString();

                if (ds.Tables[0].Rows[0]["ClosureCompleteDate"].ToString() != "")
                    ViewState["ClosureCompleteDate"] = ds.Tables[0].Rows[0]["ClosureCompleteDate"].ToString();
                else
                    ViewState["ClosureCompleteDate"] = "0";

                if (ds.Tables[0].Rows[0]["RequestType1"].ToString() != "")
                    ViewState["RequestType1Hidden"] = ds.Tables[0].Rows[0]["RequestType1"].ToString();
                else
                    ViewState["RequestType1Hidden"] = "0";

                if (ds.Tables[0].Rows[0]["RequestType2"].ToString() != "")
                    ViewState["RequestType2Hidden"] = ds.Tables[0].Rows[0]["RequestType2"].ToString();
                else
                    ViewState["RequestType2Hidden"] = "0";

                if (ds.Tables[0].Rows[0]["Severity"].ToString() != "0")
                    ViewState["SeverityHidden"] = ds.Tables[0].Rows[0]["Severity"].ToString();
                else
                    ViewState["SeverityHidden"] = "0";

                if (ds.Tables[0].Rows[0]["AssingedTo"].ToString() != "0")
                    ViewState["AssignedToHidden"] = ds.Tables[0].Rows[0]["AssingedTo"].ToString();

                ViewState["StatusHidden"] = ds.Tables[0].Rows[0]["RequestStatus"].ToString();


                ViewState["GroupHidden"] = ds.Tables[0].Rows[0]["HelpDeskGroup"].ToString();

                ViewState["Group"] = ds.Tables[0].Rows[0]["HelpDeskGroup"].ToString();

                //Requests.OpenMinutes,Requests.WorkingMinutes,Requests.PendingMinutes,Requests.ObservationMinutes,Requests.Minuteswithcloseduser,Requests.ApprovalMinutes

                if (ds.Tables[0].Rows[0]["OpenMinutes"].ToString() != "")
                    ViewState["OpenMinutesI"] = ds.Tables[0].Rows[0]["OpenMinutes"].ToString();
                else
                    ViewState["OpenMinutesI"] = "0";

                if (ds.Tables[0].Rows[0]["WorkingMinutes"].ToString() != "")
                    ViewState["WorkingMinutesI"] = ds.Tables[0].Rows[0]["WorkingMinutes"].ToString();
                else
                    ViewState["WorkingMinutesI"] = "0";

                if (ds.Tables[0].Rows[0]["PendingMinutes"].ToString() != "")
                    ViewState["PendingMinutesI"] = ds.Tables[0].Rows[0]["PendingMinutes"].ToString();
                else
                    ViewState["PendingMinutesI"] = "0";

                if (ds.Tables[0].Rows[0]["ObservationMinutes"].ToString() != "")
                    ViewState["ObservationMinutesI"] = ds.Tables[0].Rows[0]["ObservationMinutes"].ToString();
                else
                    ViewState["ObservationMinutesI"] = "0";

                if (ds.Tables[0].Rows[0]["ApprovalMinutes"].ToString() != "")
                    ViewState["ApprovalMinutesI"] = ds.Tables[0].Rows[0]["ApprovalMinutes"].ToString();
                else
                    ViewState["ApprovalMinutesI"] = "0";

                if (ds.Tables[0].Rows[0]["ExternalSupportMinutes"].ToString() != "")
                    ViewState["ExternalSupportMinutesI"] = ds.Tables[0].Rows[0]["ExternalSupportMinutes"].ToString();
                else
                    ViewState["ExternalSupportMinutesI"] = "0";

                if (ds.Tables[0].Rows[0]["Minuteswithcloseduser"].ToString() != "")
                    ViewState["MinuteswithcloseduserI"] = ds.Tables[0].Rows[0]["Minuteswithcloseduser"].ToString();
                else
                    ViewState["MinuteswithcloseduserI"] = "0";

                ViewState["ExpectedResolutionDtTime"] = ds.Tables[0].Rows[0]["ExpectedResolutionDtTime"].ToString();
                ViewState["ResponseTimeViolated"] = ds.Tables[0].Rows[0]["ResponseTimeViolated"].ToString();
                ViewState["ExpectedResponseDtTime"] = ds.Tables[0].Rows[0]["ExpectedResponseDtTime"].ToString();
                ViewState["ResolutionTimeViolated"] = ds.Tables[0].Rows[0]["ResolutionTimeViolated"].ToString();

                ViewState["ResolutionTimeViolatedstatus"] = ds.Tables[0].Rows[0]["ResolutionTimeViolated"].ToString();

                if (ds.Tables[0].Rows[0]["RequestCategory"].ToString() == "1") //Request Type - Incident or Request
                {
                    RequestCategory.SelectedValue = "1";
                }
                else if (ds.Tables[0].Rows[0]["RequestCategory"].ToString() == "2") //Request Type - Incident or Request
                {
                    RequestCategory.SelectedValue = "2";
                }
                else
                {
                    RequestCategory.SelectedValue = "0";
                }

            }

            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            //FillRequestStatus();
            //if (ViewState["StatusHidden"].ToString() != "1")
            //{
            //    RequestStatusDD.SelectedValue = ViewState["StatusHidden"].ToString();
            //}

            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("HelpdeskID", ViewState["RequestHelpDeskID"].ToString());
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.SelectCommand = "GetALLActiveWorkSchedule";

            DDGroup.DataSource = SqlDataSource1;
            DDGroup.DataTextField = "GroupName";
            DDGroup.DataValueField = "GroupID";
            DDGroup.DataBind();

            ListItem Listitem0 = new ListItem();
            Listitem0.Value = "0";
            Listitem0.Text = "----Select----";
            DDGroup.Items.Insert(0, Listitem0);

            if (!(ViewState["GroupHidden"].ToString() == "" || ViewState["GroupHidden"].ToString() == "0"))
            {
                try
                {
                    DDGroup.SelectedValue = ViewState["GroupHidden"].ToString();
                }
                catch
                {
                    DDGroup.SelectedIndex = 0;
                }
            }
            else
            {
                ViewState["GroupHidden"] = "0";
            }
            ViewState["GroupHidden"] = DDGroup.SelectedValue;
            fillDropDownsbyGroup();
        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    protected void DDGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["GroupHidden"] = DDGroup.SelectedValue;
        RequestStatusDD.SelectedValue = "2";
        fillDropDownsbyGroup();
    }

    protected void fillDropDownsbyGroup()
    {
        try
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

            if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) != 3)
            {
                SqlDataSource1.SelectParameters.Add("WorkGroupID", DDGroup.SelectedValue);
                SqlDataSource1.SelectCommand = "GetALLStatusExceptOpen";
            }
            else
            {
                SqlDataSource1.SelectCommand = "GetALLStatusForUnderObservation";
            }

            RequestStatusDD.DataSource = SqlDataSource1;
            RequestStatusDD.DataTextField = "StatusName";
            RequestStatusDD.DataValueField = "StatusID";
            RequestStatusDD.DataBind();

            ListItem Listitem0 = new ListItem();
            Listitem0.Value = "0";
            Listitem0.Text = "----Select----";
            RequestStatusDD.Items.Insert(0, Listitem0);

            /*if (Convert.ToInt32(ViewState["StatusHidden"].ToString()) != 11)
            {
                ListItem closeditem = new ListItem();
                closeditem.Value = "11";
                closeditem.Text = "Closed";
                RequestStatusDD.Items.Remove(closeditem);
            }*/

            if (ViewState["StatusHidden"].ToString() != "1")
            {
                RequestStatusDD.SelectedValue = ViewState["StatusHidden"].ToString();
            }

            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("HelpDeskID", ViewState["RequestHelpDeskID"].ToString());
            SqlDataSource1.SelectParameters.Add("GroupID", ViewState["GroupHidden"].ToString());
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.SelectCommand = "GetALLActiveResolversbyGroup";

            AssignToDD.DataSource = SqlDataSource1;
            AssignToDD.DataTextField = "ResolverNAME";
            AssignToDD.DataValueField = "Resolver";
            AssignToDD.DataBind();

            ListItem Listitem1 = new ListItem();
            Listitem1.Value = "0";
            Listitem1.Text = "----Select----";
            AssignToDD.Items.Insert(0, Listitem1);

            if (ViewState["AssignedToHidden"].ToString() != "" && AssignedToLB.Text.Trim() != "")
            {
                try
                {
                    AssignToDD.SelectedValue = ViewState["AssignedToHidden"].ToString();
                }
                catch
                {
                    AssignToDD.SelectedIndex = 0;
                }

            }

            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("GroupID", ViewState["GroupHidden"].ToString());
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.SelectCommand = "GetRequestType1Active";

            RequestType1DD.DataSource = SqlDataSource1;
            RequestType1DD.DataTextField = "RequestType1";
            RequestType1DD.DataValueField = "RequestType1ID";
            RequestType1DD.DataBind();

            ListItem Listitem3 = new ListItem();
            Listitem3.Value = "0";
            Listitem3.Text = "----Select----";
            RequestType1DD.Items.Insert(0, Listitem3);

            if (ViewState["RequestType1Hidden"].ToString() != "")
            {
                try
                {
                    RequestType1DD.SelectedValue = ViewState["RequestType1Hidden"].ToString();
                    FillSeverity(ViewState["RequestType1Hidden"].ToString());
                }
                catch
                {
                    RequestType1DD.SelectedIndex = 0;
                    SeverityDD.Items.Clear();
                }

                //SqlDataSource1.SelectParameters.Clear();
                //SqlDataSource1.SelectParameters.Add("RequestType1ID", RequestType1DD.SelectedValue);
                //SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                //SqlDataSource1.SelectCommand = "GetRequestType2Active";

                //RequestType2DD.DataSource = SqlDataSource1;
                //RequestType2DD.DataTextField = "RequestType2";
                //RequestType2DD.DataValueField = "RequestType2ID";
                //RequestType2DD.DataBind();

                //ListItem Listitem31 = new ListItem();
                //Listitem31.Value = "0";
                //Listitem31.Text = "----Select----";
                //RequestType2DD.Items.Insert(0, Listitem31);

                //try
                //{
                //    RequestType2DD.SelectedValue = ViewState["RequestType2Hidden"].ToString();
                //}
                //catch
                //{
                //    RequestType2DD.SelectedIndex = 0;
                //}

            }
            else
            {
                ListItem Listitem6 = new ListItem();
                Listitem6.Value = "0";
                Listitem6.Text = "----Select----";
                SeverityDD.Items.Clear();
                SeverityDD.Items.Insert(0, Listitem6);
            }
        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    protected void RequestType1DD_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillSeverity(RequestType1DD.SelectedItem.Value.ToString());
            //SqlDataSource1.SelectParameters.Clear();
            //SqlDataSource1.SelectParameters.Add("RequestType1ID", RequestType1DD.SelectedValue);
            //SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            //SqlDataSource1.SelectCommand = "GetRequestType2Active";

            //RequestType2DD.DataSource = SqlDataSource1;
            //RequestType2DD.DataTextField = "RequestType2";
            //RequestType2DD.DataValueField = "RequestType2ID";
            //RequestType2DD.DataBind();

            //ListItem Listitem5 = new ListItem();
            //Listitem5.Value = "0";
            //Listitem5.Text = "----Select----";
            //RequestType2DD.Items.Insert(0, Listitem5);

            SaveRequest.Focus();
        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    private void FillSeverity(string catid)
    {
        SqlDataSource1.SelectParameters.Clear();
        SqlDataSource1.SelectParameters.Add("HelpDeskID", ViewState["RequestHelpDeskID"].ToString());
        SqlDataSource1.SelectParameters.Add("CategoryID", catid.ToString());
        SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource1.SelectCommand = "GetSeverity";

        SeverityDD.DataSource = SqlDataSource1;
        SeverityDD.DataTextField = "Severity";
        SeverityDD.DataValueField = "SeverityID";
        SeverityDD.DataBind();

        ListItem Listitem2 = new ListItem();
        Listitem2.Value = "0";
        Listitem2.Text = "----Select----";
        SeverityDD.Items.Insert(0, Listitem2);
        if (ViewState["SeverityHidden"] != null)
        {
            try
            {
                SeverityDD.SelectedValue = ViewState["SeverityHidden"].ToString();
            }
            catch
            {
                SeverityDD.SelectedIndex = 0;
            }
        }

    }

    protected void SaveRequest_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            lblError.Text = "Timeout. Close the window and login again";
        }

        if (RequestStatusDD.SelectedValue.ToString() != "11")//other than duplicate/invalid
        {
            if (RequestStatusDD.SelectedValue == "0" || SeverityDD.SelectedValue == "0" || AssignToDD.SelectedValue == "0" || RequestType1DD.SelectedValue == "0" || RequestCategory.SelectedValue == "0" || DDGroup.SelectedValue == "0") //|| RequestType2DD.SelectedValue == "0" 
            {
                ErrorLabel1.Text = "Please select all dropdown values from the selection list.";
                SaveRequest.Focus();
                return;
            }
        }

        if (RequestStatusDD.SelectedValue == "4" && MessageText.Text == "")
        {
            ErrorLabel1.Text = "Reasons for changing the status should be specified";
            SaveRequest.Focus();
            return;
        }

        if (ViewState["admincheck"].ToString() == "3" && AssignToDD.SelectedValue.ToString() != Session["UserID"].ToString() && Convert.ToInt32(RequestStatusDD.SelectedValue.ToString()) > 10)
        {
            ErrorLabel1.Text = "Assinged to cannot be changed during request closure";
            SaveRequest.Focus();
            return;
        }

        if (RequestStatusDD.SelectedValue == "10")
        {
            if (!isNumber(txtHours.Text.Trim()))
            {
                ErrorLabel1.Text = "Please enter a numeric value without any decimals in the actual hour field.";
                return;
            }
            if (ddlMins.SelectedItem.Value.ToString() == "0")
            {
                int actHours = System.Convert.ToInt32(txtHours.Text.ToString());
                if (actHours == 0)
                {
                    ErrorLabel1.Text = "Please enter Actual Hours/Minuits taken to complete the request.";
                    return;
                }
            }
        }


        if (ViewState["SeverityHidden"].ToString() != SeverityDD.SelectedValue && ViewState["StatusHidden"].ToString() != "1")
        {
            ViewState["SeverityChangeCheck"] = "1";
            ViewState["proceed"] = "0";
            checkviolations();
            if (ViewState["proceed"].ToString() == "1")
            {
                ViewState["proceed"] = "0";
                return;
            }
            ViewState["SeverityChangeCheck"] = "0";
        }

        ViewState["proceed"] = "0";
        checkviolations();
        if (ViewState["proceed"].ToString() == "1")
        {
            ViewState["proceed"] = "0";
            return;
        }

        //To get the expected closure date when the Request status is changed to Resolved or Pening to user status
        DateTime ExpectedAutoClosuredate = CommonClass.GetDateTimeIST();
        if (RequestStatusDD.SelectedValue.ToString() == "10" || RequestStatusDD.SelectedValue.ToString() == "4")
        {
            ExpectedAutoClosuredate = CommonClass.GetAutoClosureDate
                (Convert.ToInt32(ViewState["NoofWorkingDays"].ToString()),
                 Convert.ToBoolean(ViewState["Holidaysincluded"].ToString()),
                 Convert.ToInt32(ViewState["HolidayLocation"].ToString()));
        }

        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            cmd = new SqlCommand("UpdateRequestsbyAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@RequestID", RequestIDLB.Text.ToString());

            if ((RequestStatusDD.SelectedItem.Text == "Resolved") || (RequestStatusDD.SelectedItem.Text == "Invalid"))
            {
                cmd.Parameters.AddWithValue("@AssingedTo ", Session["UserID"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@AssingedTo ", AssignToDD.SelectedValue.ToString());
            }

            if (ViewState["AdminMember"].ToString() == "")
            {
                cmd.Parameters.AddWithValue("@AdminMember", Session["UserID"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@AdminMember", ViewState["AdminMember"].ToString());
            }

            cmd.Parameters.AddWithValue("@RequestType1 ", RequestType1DD.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@RequestType2", "0");//RequestType2DD.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@RequestCategory", RequestCategory.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Severity", SeverityDD.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@RequestStatus", RequestStatusDD.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@ClosureDesc", ClosureDesc.Text.ToString());
            cmd.Parameters.AddWithValue("@HelpDeskGroup", DDGroup.SelectedValue.ToString());

            cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

            cmd.Parameters.AddWithValue("@ExpectedAutoClosuredate", ExpectedAutoClosuredate);

            //Has to update LBLastupdatedDate, OpenMinutes,WorkingMinutes,PendingMinutes,ObservationMinutes,ApprovalMinutes,Minuteswithcloseduser

            cmd.Parameters.AddWithValue("@ExpectedResponseDtTime", ViewState["ExpectedResponseDtTime"].ToString());

            if (ViewState["ResponseTimeViolated"] != null)
            {
                cmd.Parameters.AddWithValue("@ResponseTimeViolated", ViewState["ResponseTimeViolated"].ToString());
                if (ViewState["ResponseTimeViolated"].ToString() == "1")
                    cmd.Parameters.AddWithValue("@ResponseViolationText", ResponseViolationText.Text);
                else
                    cmd.Parameters.AddWithValue("@ResponseViolationText", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ResponseTimeViolated", "0");
                cmd.Parameters.AddWithValue("@ResponseViolationText", "");
            }

            cmd.Parameters.AddWithValue("@ExpectedResolutionDtTime", ViewState["ExpectedResolutionDtTime"].ToString());

            cmd.Parameters.AddWithValue("@ResolutionTimeViolated", ViewState["ResolutionTimeViolated"].ToString());

            if (ViewState["ResolutionTimeViolated"].ToString() == "1")
                cmd.Parameters.AddWithValue("@ResolutionViolationText", ResolutionViolationText.Text);
            else
                cmd.Parameters.AddWithValue("@ResolutionViolationText", "");

            cmd.Parameters.AddWithValue("@OpenMinutes", ViewState["OpenMinutesF"].ToString());
            cmd.Parameters.AddWithValue("@WorkingMinutes", ViewState["WorkingMinutesF"].ToString());
            cmd.Parameters.AddWithValue("@PendingMinutes", ViewState["PendingMinutesF"].ToString());
            cmd.Parameters.AddWithValue("@ObservationMinutes", ViewState["ObservationMinutesF"].ToString());
            cmd.Parameters.AddWithValue("@ApprovalMinutes", ViewState["ApprovalMinutesF"].ToString());
            cmd.Parameters.AddWithValue("@ExternalSupportMinutes", ViewState["ExternalSupportMinutesF"].ToString());
            cmd.Parameters.AddWithValue("@Minuteswithcloseduser", ViewState["MinuteswithcloseduserF"].ToString());


            if (MessageText.Text.Trim() != "")
                cmd.Parameters.AddWithValue("@RequestMessage", MessageText.Text);
            else
                cmd.Parameters.AddWithValue("@RequestMessage", DBNull.Value);


            if (RequestStatusDD.SelectedValue == "4")
                cmd.Parameters.AddWithValue("@RequestMessageType", 1); //Pending to User
            else if (RequestStatusDD.SelectedValue == "10")
                cmd.Parameters.AddWithValue("@RequestMessageType", 7); //Knowledge Base
            else
                cmd.Parameters.AddWithValue("@RequestMessageType", 5); //Request assignment Notes

            if (RequestStatusDD.SelectedValue == "10")
            {
                if (txtHours.Text.Trim() != "")
                    cmd.Parameters.AddWithValue("@ActualHours", txtHours.Text.ToString());
                else
                    cmd.Parameters.AddWithValue("@ActualHours", "0");

                cmd.Parameters.AddWithValue("@ActualMins", ddlMins.SelectedItem.Value.ToString());
            }

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            //Request violation notfication to the employee who violated the Request.
            if ((ViewState["ResolutionTimeViolatedstatus"].ToString() == "" || ViewState["ResolutionTimeViolatedstatus"].ToString() == "0") && (ViewState["ResolutionTimeViolated"].ToString() == "1"))
            {
                if (Session["email"].ToString().Trim() != "")
                    CommonClass.RequestViolationNotification
                        (Session["email"].ToString().Trim().ToLower(),
                        Session["MgrEmail"].ToString().Trim().ToLower(),
                        RequestIDLB.Text.ToString(),
                        Session["NameGreet"].ToString().Trim(),
                        SubjectLB.Text.ToString().Trim(),
                        divRequestDesc.InnerHtml.ToString().Replace("\r\n", "<br>"),
                        ResolutionViolationText.Text.ToString().Replace("\r\n", "<br>"),
                        ViewState["HLD_Email"].ToString().ToLower().Trim(),
                        Session["UserID"].ToString());
            }

            if (RequestStatusDD.SelectedValue.ToString() == "10" || RequestStatusDD.SelectedValue.ToString() == "11")
            {
                //Mail type 3 - Request Closed
                CommonClass.sendmails(RequestIDLB.Text.ToString(), "3", Session["UserID"].ToString(), "", "");
            }
            else if (ViewState["AssignedToHidden"].ToString() != AssignToDD.SelectedValue.ToString())
            {
                //Mail type2 - Request Assignement
                CommonClass.sendmails(RequestIDLB.Text.ToString(), "2", Session["UserID"].ToString(), MessageText.Text, "");
            }

            if (RequestStatusDD.SelectedValue == "4")
            {
                //Mail type5 - Request status changed to pending to user.
                CommonClass.sendmails(RequestIDLB.Text.ToString(), "5", Session["UserID"].ToString(), MessageText.Text, "");
            }

            if (ViewState["StatusHidden"].ToString() == "1")
            {
                //Mail type10 - Request asigned acknowledgment
                CommonClass.sendmails(RequestIDLB.Text.ToString(), "10", Session["UserID"].ToString(), "", "");
            }

            string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Request has been updated');window.location='" + Urladdress + "'", true);


        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    protected void checkviolations()
    {
        try
        {
            int NoofWorkingDays = 0;
            double Startinghour = 0;
            double Endinghour = 0;
            bool Holidaysincluded = false;
            int HolidayLocation = 0;
            double ResponseTime = 0;
            double ResolutionTime = 0;
            string WorkGroupTimeZone = "";

            ViewState["OpenMinutesF"] = "0";
            ViewState["WorkingMinutesF"] = "0";
            ViewState["PendingMinutesF"] = "0";
            ViewState["ObservationMinutesF"] = "0";
            ViewState["ApprovalMinutesF"] = "0";
            ViewState["MinuteswithcloseduserF"] = "0";
            ViewState["ExternalSupportMinutesF"] = "0";

            ViewState["NoofWorkingDays"] = "0";
            ViewState["Holidaysincluded"] = "0";
            ViewState["HolidayLocation"] = "0";

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            cmd = new SqlCommand("GetDetailsForSLA", con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (ViewState["SeverityChangeCheck"].ToString() == "1")
            {
                cmd.Parameters.AddWithValue("@SeverityID", ViewState["SeverityHidden"].ToString());
                cmd.Parameters.AddWithValue("@CatID", ViewState["RequestType1Hidden"].ToString());
            }
            else
            {
                cmd.Parameters.AddWithValue("@SeverityID", SeverityDD.SelectedValue);
                cmd.Parameters.AddWithValue("@CatID", RequestType1DD.SelectedValue.ToString());
            }

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.Read())
            {
                //1 - Open, 2-Working in progress, 3-Underobservation,4-Pending to user,5-Wating for approval
                NoofWorkingDays = Convert.ToInt32(dr["NoofWorkingDays"].ToString());
                Startinghour = Convert.ToDouble(dr["Startinghour"].ToString());
                Endinghour = Convert.ToDouble(dr["Endinghour"].ToString());
                Holidaysincluded = Convert.ToBoolean(dr["Holidaysincluded"].ToString());
                HolidayLocation = Convert.ToInt32(dr["HolidayLocation"].ToString());
                ResponseTime = Convert.ToDouble(dr["SResponseTime"].ToString());
                ResolutionTime = Convert.ToDouble(dr["SResolutionTime"].ToString());

                WorkGroupTimeZone = "";

                ViewState["NoofWorkingDays"] = NoofWorkingDays;
                ViewState["Holidaysincluded"] = Holidaysincluded;
                ViewState["HolidayLocation"] = HolidayLocation;
            }

            if (Responsetimeviolationlb.Text == "")
            {
                DateTime ResoponseRequiredbydate = CommonClass.GetRRByTime(ViewState["CreatedDate"].ToString(), NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, ResponseTime, WorkGroupTimeZone);
                ViewState["ExpectedResponseDtTime"] = ResoponseRequiredbydate;

                if (ResoponseRequiredbydate < CommonClass.GetDateTimeIST())
                {
                    if (ResponseViolationText.Text.Trim().Length < 5)
                    {
                        ResponseViolationText.Text = "";
                        ViewState["ResponseTimeViolated"] = "1";
                        Responseviolationtr.Visible = true;
                        ErrorLabel1.Text = "Request Response/Resolution violation reasons needs to be filled";
                        ViewState["proceed"] = "1";
                    }
                    else
                    {
                        ResponseViolationText.Text = ResponseViolationText.Text.ToString() + "\r\n" + Session["UserID"].ToString() + "-" + Session["NameGreet"].ToString();
                        ViewState["proceed"] = "0";
                    }
                }
                else
                {
                    ViewState["ResponseTimeViolated"] = "0";
                }

                DateTime ResolutionRequiredbydate = CommonClass.GetRRByTime(ViewState["CreatedDate"].ToString(), NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, ResolutionTime, WorkGroupTimeZone);
                ViewState["ExpectedResolutionDtTime"] = ResolutionRequiredbydate;

                if (ResolutionRequiredbydate < CommonClass.GetDateTimeIST())
                {
                    if (ResolutionViolationText.Text.Trim().Length < 5)
                    {
                        ResolutionViolationText.Text = "";
                        ViewState["ResolutionTimeViolated"] = "1";
                        Resolutionviolationtr.Visible = true;
                        ErrorLabel1.Text = "Request Resolution/Response violation reasons needs to be filled";
                        ViewState["proceed"] = "1";
                    }
                    else
                    {
                        ResolutionViolationText.Text = ResolutionViolationText.Text.ToString() + "\r\n" + Session["UserID"].ToString() + "-" + Session["NameGreet"].ToString();
                        ViewState["proceed"] = "0";
                    }
                }
                else
                {
                    ViewState["ResolutionTimeViolated"] = "0";
                }
                TimeSpan Completedtime = CommonClass.CompleltedHours(ViewState["CreatedDate"].ToString(), NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, WorkGroupTimeZone);
                if (ViewState["StatusHidden"].ToString() == "1") //Open
                    ViewState["OpenMinutesF"] = Convert.ToInt32(Completedtime.TotalMinutes.ToString("0"));

            }
            else
            {
                //This part has to be worked out.
                DateTime ResolutionRequiredbydate = CommonClass.GetRRByTime(ViewState["CreatedDate"].ToString(), NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, ResolutionTime, WorkGroupTimeZone);
                ViewState["ExpectedResolutionDtTime"] = ResolutionRequiredbydate;

                TimeSpan Completedtime = CommonClass.CompleltedHours(ViewState["Lastupdateddate"].ToString(), NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, WorkGroupTimeZone);
                if (ViewState["StatusHidden"].ToString() == "2") //Work in progess
                    ViewState["WorkingMinutesF"] = Convert.ToInt32(ViewState["WorkingMinutesI"].ToString()) + Convert.ToInt32(Completedtime.TotalMinutes.ToString("0"));
                else
                    ViewState["WorkingMinutesF"] = Convert.ToInt32(ViewState["WorkingMinutesI"].ToString());

                if (ViewState["StatusHidden"].ToString() == "3") //Under observation
                    ViewState["ObservationMinutesF"] = Convert.ToInt32(ViewState["ObservationMinutesI"].ToString()) + Convert.ToInt32(Completedtime.TotalMinutes.ToString("0"));
                else
                    ViewState["ObservationMinutesF"] = Convert.ToInt32(ViewState["ObservationMinutesI"].ToString());

                if (ViewState["StatusHidden"].ToString() == "4") //Pending to user
                    ViewState["PendingMinutesF"] = Convert.ToInt32(ViewState["PendingMinutesI"].ToString()) + Convert.ToInt32(Completedtime.TotalMinutes.ToString("0"));
                else
                    ViewState["PendingMinutesF"] = Convert.ToInt32(ViewState["PendingMinutesI"].ToString());

                if (ViewState["StatusHidden"].ToString() == "5") //Waiting for approval
                    ViewState["ApprovalMinutesF"] = Convert.ToInt32(ViewState["ApprovalMinutesI"].ToString()) + Convert.ToInt32(Completedtime.TotalMinutes.ToString("0"));
                else
                    ViewState["ApprovalMinutesF"] = Convert.ToInt32(ViewState["ApprovalMinutesI"].ToString());

                if (ViewState["StatusHidden"].ToString() == "6") //External Support Requested
                    ViewState["ExternalSupportMinutesF"] = Convert.ToInt32(ViewState["ExternalSupportMinutesI"].ToString()) + Convert.ToInt32(Completedtime.TotalMinutes.ToString("0"));
                else
                    ViewState["ExternalSupportMinutesF"] = Convert.ToInt32(ViewState["ExternalSupportMinutesI"].ToString());


                if (AssignedDateLB.Text != "")
                {
                    TimeSpan Completedhourswithuser = CommonClass.CompleltedHours(ViewState["AssingedDate"].ToString(), NoofWorkingDays, Startinghour, Endinghour, Holidaysincluded, HolidayLocation, WorkGroupTimeZone);
                    ViewState["MinuteswithcloseduserF"] = Completedhourswithuser.TotalMinutes.ToString("0");
                }

                if (Resolutiontimeviolationlb.Text != "Yes")
                {
                    if (Convert.ToDouble(ViewState["WorkingMinutesF"].ToString()) > ResolutionTime)
                    {
                        if (ResolutionViolationText.Text.Trim().Length < 5)
                        {
                            ResolutionViolationText.Text = "";
                            ViewState["ResolutionTimeViolated"] = "1";
                            Resolutionviolationtr.Visible = true;
                            ErrorLabel1.Text = "Request Resolution/Response violation reasons needs to be filled";
                            ViewState["proceed"] = "1";
                        }
                        else
                        {
                            ResolutionViolationText.Text = ResolutionViolationText.Text.ToString() + "\r\n" + Session["UserID"].ToString() + "-" + Session["NameGreet"].ToString();
                            ViewState["proceed"] = "0";
                        }
                    }
                    else
                    {
                        if (ResolutionViolationText.Text.Length > 5)
                            ViewState["ResolutionTimeViolated"] = "1";
                        else
                            ViewState["ResolutionTimeViolated"] = "0";
                    }
                    double noofhoursleft = ((Convert.ToDouble(ResolutionTime) - Convert.ToDouble(ViewState["WorkingMinutesF"].ToString())) / 60);

                    ////Convert into time format
                    //string[] hoursleftcal = noofhoursleft.ToString().Split('.');
                    //if (hoursleftcal.Length > 0)
                    //    if (hoursleftcal[1].ToString() == "0" || hoursleftcal[1].ToString() == "00")
                    //        TimeLeftForResolution.Text = hoursleftcal[0].ToString();
                    //    else
                    //        TimeLeftForResolution.Text = hoursleftcal[0].ToString() + "." + (Convert.ToDecimal(hoursleftcal[1].ToString()) * 60).ToString().Remove(2);
                    //else
                    //     TimeLeftForResolution.Text = hoursleftcal[0].ToString();

                    TimeLeftForResolution.Text = noofhoursleft.ToString("0.0");
                }
                else
                {
                    TimeLeftForResolution.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    protected void RequestStatusDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageTr.Visible = true;
        MessageButton.Visible = false;
        MessageLabel.Text = "Additional Notes";
        //MessageText.ValidationGroup = "GroupMessage";
        //MessageRequiredvalidator.ValidationGroup = "GroupMessage";
        //MessageRequiredvalidator.Enabled = false;

        if (ViewState["EmailIDoftheExitedemp"].ToString() != "" && RequestStatusDD.SelectedValue == "4")
        {
            RequestStatusDD.SelectedValue = "2";
            ErrorLabel1.Text = "Cannot change status to pending to user as the user is already exited from the system";
        }
        else
        {
            if (RequestStatusDD.SelectedValue == "4")
            {
                MessageLabel.Text = "Reasons for changing status to Pending With User : ";
            }
        }

        if (RequestStatusDD.SelectedValue == "10")
        {
            Requestclosuredestr.Visible = true;
            MessageTr.Visible = false;

            SaveRequest.Focus();
            ClosureDesc.Focus();
            //MessageText.ValidationGroup = "SaveRequest";
            //MessageRequiredvalidator.ValidationGroup = "SaveRequest";
            //MessageRequiredvalidator.Enabled = true;
            //MessageLabel.Text = "Problem Resolution for Knowlege Base";
        }
        else
        {
            Requestclosuredestr.Visible = false;
        }

        if (RequestStatusDD.SelectedIndex.ToString() == "0")
        {
            MessageTr.Visible = false;
        }


    }

    protected void hideRequestmenues()
    {
        DDGroup.Enabled = false;
        RequestType1DD.Enabled = false;
        //RequestType2DD.Enabled = false;
        RequestStatusDD.Enabled = false;
        AssignToDD.Enabled = false;
        SeverityDD.Enabled = false;
        RequestCategory.Enabled = false;
        //Requestclosuredestr.Visible = false;
        //Responseviolationtr.Visible = false;
        //Resolutionviolationtr.Visible = false;
        SaveRequest.Visible = false;
        MessageTr.Visible = false;
        //CBsendMessage.Enabled = false;

    }

    protected void ShowRequestmenues()
    {
        DDGroup.Enabled = true;
        RequestType1DD.Enabled = true;
        //RequestType2DD.Enabled = true;
        RequestStatusDD.Enabled = true;
        AssignToDD.Enabled = true;
        SeverityDD.Enabled = true;
        RequestCategory.Enabled = true;

        if (ClosureDesc.Text.Trim() != "")
        {
            Requestclosuredestr.Visible = true;
        }

        if (ResponseViolationText.Text.Trim() != "")
        {
            Responseviolationtr.Visible = true;
        }

        if (ResolutionViolationText.Text.Trim() != "")
        {
            Resolutionviolationtr.Visible = true;
        }

        SaveRequest.Visible = true;

    }

    protected void SeverityDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Responsetimeviolationlb.Text == "Yes")
        {
            Responseviolationtr.Visible = true;
        }
        else
        {
            Responseviolationtr.Visible = false;
            ResponseViolationText.Text = "";
        }

        if (Resolutiontimeviolationlb.Text == "Yes")
        {
            Resolutionviolationtr.Visible = true;
        }
        else
        {
            Resolutionviolationtr.Visible = false;
            ResolutionViolationText.Text = "";
        }

    }

    protected void CBsendMessage_CheckedChanged(object sender, EventArgs e)
    {

        if (SaveRequest.Visible == true)
            ViewState["SMSaveRequestCheck"] = "1";

        if (MessageText.Visible == true)
            ViewState["SMMessageTrCheck"] = "1";

        if (CBsendMessage.Checked == true)
        {
            if (CBsendMessage.Text == "Send Message to Support Staff")
            {
                MessageRequiredvalidator.Enabled = true;
                MessageButton.Visible = true;
                MessageText.Visible = true;
                MessageTr.Visible = true;

                MessageButton.Text = "Send Message";
                MessageLabel.Text = "Message";
                MessageButton.CommandArgument = "MessagetoSupportStaff";
            }
            else if (CBsendMessage.Text == "Send Message to user")
            {
                MessageRequiredvalidator.Enabled = true;

                MessageButton.Visible = true;
                MessageText.Visible = true;
                MessageTr.Visible = true;

                MessageButton.Text = "Send Message";
                MessageLabel.Text = "Message";
                MessageButton.CommandArgument = "Messagetouser";
                SaveRequest.Visible = false;
            }

        }
        else
        {
            if (CBsendMessage.Text == "Send Message to user")
            {
                if (ViewState["SMSaveRequestCheck"].ToString() == "1")
                    SaveRequest.Visible = true;

                if (ViewState["SMMessageTrCheck"].ToString() == "1")
                {
                    MessageTr.Visible = true;
                    MessageButton.Visible = false;
                }
                else
                {
                    MessageTr.Visible = false;
                }
                if (RequestStatusDD.SelectedValue == "6")
                {
                    MessageLabel.Text = "Reasons for changing status to Pending With User : ";
                }
                else
                {
                    MessageLabel.Text = "Additional Notes";
                }



            }

        }
    }

    protected void MessageButton_Click(object sender, EventArgs e)
    {
        try
        {
            //ReleasePendingStatus, SelfAssignment, MessagetoSupportStaff, ReopenRequest
            if (MessageButton.CommandArgument.ToString() == "SelfAssignment")
            {
                if (Session["UserID"] == null)
                {
                    lblError.Text = "Timeout. Close the window and login again";
                }

                if (RequestStatusDD.SelectedValue == "0" || SeverityDD.SelectedValue == "0" || AssignToDD.SelectedValue == "0" || RequestType1DD.SelectedValue == "0" || RequestCategory.SelectedValue == "0" || DDGroup.SelectedValue == "0") //RequestType2DD.SelectedValue == "0" || 
                {
                    ErrorLabel1.Text = "All dropdown fields are mandatory. Please contact helpdesk cordinator to assign the request.";
                    SaveRequest.Focus();
                    return;
                }

                checkviolations();
                if (ViewState["proceed"].ToString() == "1")
                {
                    ViewState["proceed"] = "0";
                    return;
                }

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("SelfAssingment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@RequestID", RequestIDLB.Text.ToString());
                cmd.Parameters.AddWithValue("@AssingedTo", Session["UserID"].ToString());

                //Has to update LBLastupdatedDate, OpenMinutes,WorkingMinutes,PendingMinutes,ObservationMinutes,ApprovalMinutes,Minuteswithcloseduser

                cmd.Parameters.AddWithValue("@ExpectedResolutionDtTime", ViewState["ExpectedResolutionDtTime"].ToString());

                cmd.Parameters.AddWithValue("@ResolutionTimeViolated", ViewState["ResolutionTimeViolated"].ToString());

                if (ViewState["ResolutionTimeViolated"].ToString() == "1")
                    cmd.Parameters.AddWithValue("@ResolutionViolationText", ResolutionViolationText.Text);
                else
                    cmd.Parameters.AddWithValue("@ResolutionViolationText", "");

                cmd.Parameters.AddWithValue("@WorkingMinutes", ViewState["WorkingMinutesF"].ToString());
                cmd.Parameters.AddWithValue("@PendingMinutes", ViewState["PendingMinutesF"].ToString());
                cmd.Parameters.AddWithValue("@ObservationMinutes", ViewState["ObservationMinutesF"].ToString());
                cmd.Parameters.AddWithValue("@ApprovalMinutes", ViewState["ApprovalMinutesF"].ToString());
                cmd.Parameters.AddWithValue("@ExternalSupportMinutes", ViewState["ExternalSupportMinutesF"].ToString());

                cmd.Parameters.AddWithValue("@RequestMessage", MessageText.Text);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

                string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Request Assigned');opener.location='" + Urladdress + "'", true);


                //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");
            }
            else if (MessageButton.CommandArgument.ToString() == "ReleasePendingStatus")
            {
                checkviolations();
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("ReleasePendingStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@RequestID", RequestIDLB.Text.ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@PendingMinutes", ViewState["PendingMinutesF"].ToString());
                cmd.Parameters.AddWithValue("@RequestMessage", MessageText.Text);

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

                //Mail type6 - Pending to status release by the user
                //CommonClass.sendmails(RequestIDLB.Text.ToString(), "6", Session["UserID"].ToString(), MessageText.Text.ToString());

                string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Pending Status Released');opener.location='" + Urladdress + "'", true);

                //string Msg = "Pedning Status Released";
                //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");
            }
            //ResolutionRejected OR Accepted
            else if (MessageButton.CommandArgument.ToString() == "AccepetReject")
            {
                if (AcceptRejectRB.SelectedValue == "")
                {
                    LbAcceptReject.Visible = true;
                    return;

                }
                else if (AcceptRejectRB.SelectedValue == "Rejected")
                {
                    if (MessageText.Text.Trim().Length < 10)
                    {
                        LbAcceptReject.Visible = true;
                        LbAcceptReject.Text = "Rejection reason is mandatory";
                        return;
                    }

                    CreateDateLB.Text = CommonClass.GetDateTimeIST().ToString();
                    AssignedDateLB.Text = CommonClass.GetDateTimeIST().ToString();
                    LBLastupdatedDate.Text = CommonClass.GetDateTimeIST().ToString();
                    Resolutiontimeviolationlb.Text = "";
                    Responsetimeviolationlb.Text = "";
                    ViewState["WorkingMinutesF"] = "0";

                    checkviolations();

                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                    cmd = new SqlCommand("UpdateRequestRejeted", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    cmd.Parameters.AddWithValue("@RequestID", RequestIDLB.Text.ToString());
                    cmd.Parameters.AddWithValue("@ExpectedResponseDtTime", ViewState["ExpectedResponseDtTime"].ToString());
                    cmd.Parameters.AddWithValue("@ExpectedResolutionDtTime", ViewState["ExpectedResolutionDtTime"].ToString());
                    cmd.Parameters.AddWithValue("@ReopnMessage", MessageText.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    con.Close();

                    //Mail type7 - Request Rejected Mail
                    CommonClass.sendmails(RequestIDLB.Text.ToString(), "7", Session["UserID"].ToString(), MessageText.Text.ToString(), "");

                    string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Updated Request Status');window.location='" + Urladdress + "'", true);

                    //string Msg = "Updated Request Status";
                    //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");
                }
                else
                {
                    //Request Accepted and Closed by the user - Message type 7.1
                    SqlDataSource1.UpdateParameters.Clear();
                    SqlDataSource1.UpdateParameters.Add("RequestID", RequestIDLB.Text.ToString());
                    SqlDataSource1.UpdateParameters.Add("CreatedBy", Session["UserID"].ToString());
                    SqlDataSource1.UpdateParameters.Add("AcceptedMessage", MessageText.Text);
                    SqlDataSource1.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
                    SqlDataSource1.UpdateCommand = "UpdateRequestAccepted";
                    SqlDataSource1.Update();

                    CommonClass.sendmails(RequestIDLB.Text.ToString(), "7.1", Session["UserID"].ToString(), "", "");

                    string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Request Closed.');window.location='" + Urladdress + "'", true);

                    //string Msg = "Request Closed. You can now Rate this Request from the Request details page";
                    //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");

                }

                //Should be redirected to default.aspx page
            }
            else if (MessageButton.CommandArgument.ToString() == "Messagetouser")
            {
                //Message to User
                SqlDataSource1.InsertParameters.Clear();
                SqlDataSource1.InsertParameters.Add("RequestID", RequestIDLB.Text.ToString());
                SqlDataSource1.InsertParameters.Add("AssignedTo", EmpNoLB.Text);
                SqlDataSource1.InsertParameters.Add("CreatedBy", Session["UserID"].ToString());
                SqlDataSource1.InsertParameters.Add("MessageType", "1");
                SqlDataSource1.InsertParameters.Add("MessageText", MessageText.Text);
                SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
                SqlDataSource1.InsertCommand = "InsertMessages";
                SqlDataSource1.Insert();

                //Mail type9 - Message to user
                CommonClass.sendmails(RequestIDLB.Text.ToString(), "8", Session["UserID"].ToString(), MessageText.Text.ToString(), "");

                string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Message Sent');window.location='" + Urladdress + "'", true);

                //string Msg = "Message Sent";
                //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");

            }
            else if (MessageButton.CommandArgument.ToString() == "MessagetoSupportStaff")
            {
                //Message to support staff
                SqlDataSource1.InsertParameters.Clear();
                SqlDataSource1.InsertParameters.Add("RequestID", RequestIDLB.Text.ToString());
                SqlDataSource1.InsertParameters.Add("AssignedTo", ViewState["AssingedTo"].ToString().ToLower());
                SqlDataSource1.InsertParameters.Add("CreatedBy", Session["UserID"].ToString());
                SqlDataSource1.InsertParameters.Add("MessageType", "2");
                SqlDataSource1.InsertParameters.Add("MessageText", MessageText.Text);
                SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
                SqlDataSource1.InsertCommand = "InsertMessages";
                SqlDataSource1.Insert();

                //Mail type8 - Message to Support Staff
                CommonClass.sendmails(RequestIDLB.Text.ToString(), "9", Session["UserID"].ToString(), MessageText.Text.ToString(), "");

                string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Message Sent');window.location='" + Urladdress + "'", true);

                //string Msg = "Message Sent";
                //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");

            }

            MessageTr.Visible = false;
        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    protected void ImagesGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "Viewfile")
            {
                int i = Convert.ToInt32(e.CommandArgument);

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("GetFullFilesByFileID", con);
                cmd.Parameters.AddWithValue("@FileID", i.ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                daAccess.Fill(ds, "test1");

                byte[] graphByte = (byte[])ds.Tables["test1"].Rows[0]["FileData1"];


                MemoryStream mstream = new MemoryStream(graphByte);

                Context.Response.ContentType = ds.Tables["test1"].Rows[0]["FileType1"].ToString();

                Context.Response.Charset = null;

                Response.Clear();

                Response.ContentType = ds.Tables["test1"].Rows[0]["FileType1"].ToString();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + ds.Tables["test1"].Rows[0]["FileName1"].ToString() + "\"");

                Response.Charset = null;

                mstream.WriteTo(Context.Response.OutputStream);
                Context.Response.End();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

            }
        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }
    protected void UploadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string[] filetype;
            filetype = FileUpload1.FileName.Split('.');
            int fileLen = Convert.ToInt32(FileUpload1.PostedFile.ContentLength.ToString());

            if (fileLen <= 0)
            {
                return;
            }

            if (fileLen > 1048576)
            {
                string Msg = "Max. allowed file size is 1 MB. You may convert the image file to Gif or JPEG and upload it again";
                ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Max. allowed file size is 1 MB. You may convert the image file to Gif or JPEG and upload it again');", true);
                //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');\n</script>\n");
                return;
            }

            byte[] fileBinaryData = new byte[fileLen];

            FileUpload1.PostedFile.InputStream.Read(fileBinaryData, 0, fileLen);
            string FileName = FileUpload1.FileName.ToString();
            string Filecontenttype = FileUpload1.PostedFile.ContentType.ToString();
            string Filesize = FileUpload1.PostedFile.ContentLength.ToString();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            cmd = new SqlCommand("Insertfiles", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@Filename1", FileName);
            cmd.Parameters.AddWithValue("@Filetype1", Filecontenttype);
            cmd.Parameters.AddWithValue("@Filedata1", fileBinaryData);
            cmd.Parameters.AddWithValue("@Filesize1", Filesize);
            cmd.Parameters.AddWithValue("@RequestID", RequestIDLB.Text.ToString());

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            //string Urladdress = "ViewRequest.aspx?RequestID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();
            //ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Request Details Updated');window.location='" + Urladdress + "'", true);

            string uploadmsg = "Request Details Updated";
            Response.Write("<script language=\"javascript\">\nalert('" + uploadmsg + "');window.location=\"ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString() + "\";\n</script>\n");


        }
        catch (Exception ex)
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            lblError.Text = "Error in Page" + ex.Message.ToString();
        }
    }

    protected void UserRating_Click(object sender, EventArgs e)
    {
        if (UserSatisfaction.SelectedValue.ToString() == "")
        {
            LBRatingError.Visible = true;
            return;
        }
        SqlDataSource1.InsertParameters.Clear();
        SqlDataSource1.InsertParameters.Add("RatingRequestid", RequestIDLB.Text.ToString());
        SqlDataSource1.InsertParameters.Add("Rating", UserSatisfaction.SelectedValue);
        SqlDataSource1.InsertParameters.Add("Feedback", FeedbackText.Text);
        SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource1.InsertCommand = "InsertRating";
        SqlDataSource1.Insert();

        string Urladdress = "ViewRequest.aspx?ReqID=" + RequestIDLB.Text.ToString() + "&Requestaction=" + ViewState["Requestaction"].ToString();
        ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Thank you for providing your valuble feedback.');window.location='" + Urladdress + "'", true);

        //string Msg = "Thank you for providing your valuble feedback.";
        //Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location='" + Urladdress + "';\n</script>\n");

        //Send messages to the reolver
    }
    protected void LNTranscationHistory_Click(object sender, EventArgs e)
    {
        Requesthistorydiv.Style.Add("RequestHistorypos", "z-index: 100; left: 40px; position: absolute; top: 100px; background-color: white; width: 750px");

        Requesthistorydiv.Visible = true;
        SqlDataSource4.SelectParameters.Clear();
        SqlDataSource4.SelectParameters.Add("RequestID", RequestIDLB.Text.ToString());

        if (ViewState["Requestaction"].ToString() == "userview")
            SqlDataSource4.SelectParameters.Add("userview", "1");
        else
            SqlDataSource4.SelectParameters.Add("userview", "0");


        SqlDataSource4.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource4.SelectCommand = "GetRequestHistory";
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlDataSource4.SelectParameters.Clear();
        SqlDataSource4.SelectParameters.Add("RequestID", RequestIDLB.Text.ToString());

        if (ViewState["Requestaction"].ToString() == "userview")
            SqlDataSource4.SelectParameters.Add("userview", "1");
        else
            SqlDataSource4.SelectParameters.Add("userview", "0");


        SqlDataSource4.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
        SqlDataSource4.SelectCommand = "GetRequestHistory";
    }

    protected void CloseHistory_Click(object sender, ImageClickEventArgs e)
    {
        Requesthistorydiv.Visible = false;
    }


    protected void GetAdminStatus()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            SqlCommand cmd = new SqlCommand("GetALLActiveadminsForRequestID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@memberID", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@RequestID", RequestID);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                ViewState["admincheck"] = Convert.ToInt32(dr["Admin"].ToString());
            }
            else
            {
                ViewState["admincheck"] = 1000;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();
        }
        catch
        {
            ViewState["admincheck"] = 1000;
        }
    }

    protected void DoVisibilityCheck()
    {
        RateRequestTR.Visible = false;
        UserRating.Visible = false;

        //Send messages teqwewqewe wqwerwqerwewer
        CBsendMessage.Enabled = false;

        //Response violation Text Table row
        Responseviolationtr.Visible = false;

        //Resolution violation Text Table row
        Resolutionviolationtr.Visible = false;


        //General Messages.
        MessageTr.Visible = false;
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
            e.Row.Cells[5].Text = e.Row.Cells[5].Text;

    }

    private bool isNumber(string value)
    {
        try
        {
            int value1 = System.Convert.ToInt32(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
