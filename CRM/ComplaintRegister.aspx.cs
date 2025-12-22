using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

public partial class ComplaintRegister : System.Web.UI.Page
{
    DBOperation myDBOperation = new DBOperation();
    string strToMail = "";
    string strCCMail = "";
    string MgmttMail = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CRMUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            FillDropdowns(); //Fill master data
            disableAllButtons(); //Disable all button by default
            if (Session["ComplaintID"] != null) //Edit Mode or After complaint is saved to db
            {
                ViewState["ComplaintID"] = Session["ComplaintID"].ToString();
                Session["ComplaintID"] = null;
                FillData(ViewState["ComplaintID"].ToString());
            }
            else //When New Request
            {
                ViewState["ComplaintStatus"] = "0";
                ViewState["FeedbackStatus"] = "0";
            }
            //Activate All buttons and form fields based on current status and and logged in user role
            EnableDisableFormAndButtonFieldsForRole(Session["CRMselectedRole"].ToString(), ViewState["ComplaintStatus"].ToString());
        }
        clearMessages(); //Clear all error,notifications and label messages etc.

        //ShowMessage(divNotice, "Notice", "Welcome to Complaint Register");  
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

    private void FillDropdowns()
    {
        MastData objMastData = new MastData();
        objMastData.BindComplaintTypes(ddTypeOfComplaint);
        objMastData.BindComplaintSeverity(ddNatureOfComplaint);
        objMastData.BindArea(ddlArea);
        objMastData.BindProductCategory(ddCategory);
        objMastData.BindUnit(ddComplaintForwardedTO);
    }

    private void FillData(string ComplaintId)
    {
        ViewState["ComplaintStatus"] = "0";
        ViewState["FeedbackStatus"] = "0";
        DBOperation objDBOperation = new DBOperation();
        DataSet dsComplaintdata = objDBOperation.GetComplaintByID(ComplaintId);

        if (dsComplaintdata.Tables[0].Rows.Count != 0)
        {
            ViewState["dtComplaintdata"] = (DataTable)dsComplaintdata.Tables[0];
            txtComplainantName.Text = dsComplaintdata.Tables[0].Rows[0]["Customer_Name"].ToString();
            txtComplaintPlace.Text = dsComplaintdata.Tables[0].Rows[0]["Customer_Place"].ToString();
            txtCustomerMobile.Text = dsComplaintdata.Tables[0].Rows[0]["Mobile"].ToString();
            txtCustomerTelephone.Text = dsComplaintdata.Tables[0].Rows[0]["Telephone"].ToString();
            txtCustomerAddress.Text = dsComplaintdata.Tables[0].Rows[0]["Customer_Address"].ToString();
            ddNatureOfComplaint.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["Complaint_Nature"].ToString();
            ddlArea.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["Area"].ToString();
            ddTypeOfComplaint.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["Complaint_Type"].ToString();
            txtProductDetails.Text = dsComplaintdata.Tables[0].Rows[0]["Product_Details"].ToString();
            txtQuantity.Text = dsComplaintdata.Tables[0].Rows[0]["Product_Quantity"].ToString();
            txtBatchNO.Text = dsComplaintdata.Tables[0].Rows[0]["Product_Batch_NO"].ToString();
            txtCallTakenTime.Text = dsComplaintdata.Tables[0].Rows[0]["Registration_Time"].ToString();
            ddCategory.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["Category"].ToString();
            txtCompDetails.Text = dsComplaintdata.Tables[0].Rows[0]["CompDescr"].ToString();
            txtContactedByName.Text = dsComplaintdata.Tables[0].Rows[0]["contactedBy"].ToString();
            txtDistributor.Text = dsComplaintdata.Tables[0].Rows[0]["DistributorName"].ToString();
            ddComplaintForwardedTO.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["Complaint_Unit"].ToString();            
            if (ddComplaintForwardedTO.SelectedValue == "11")
            {
                divSubDivision.Visible = true;
                try
                {
                    ddSubDivision1.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["SubUnit"].ToString();
                }
                catch { }
                ddSubDivision1.Visible = true;
                ddSubDivision2.Visible = false;
            }
            else if (ddComplaintForwardedTO.SelectedValue == "10")
            {
                divSubDivision.Visible = true;
                try
                {
                    ddSubDivision2.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["SubUnit"].ToString();
                }
                catch { }
                ddSubDivision1.Visible = false;
                ddSubDivision2.Visible = true;
            }
            else
            {
                divSubDivision.Visible = false;
                ddSubDivision1.Visible = false;
                ddSubDivision2.Visible = false;
            }
            string CreatedBY = dsComplaintdata.Tables[0].Rows[0]["createdBy"].ToString();
            string CreatedOn = dsComplaintdata.Tables[0].Rows[0]["createdOn"].ToString();
            string LastModifiedBy = dsComplaintdata.Tables[0].Rows[0]["LastModifiedBY"].ToString();
            string LastModifiedOn = dsComplaintdata.Tables[0].Rows[0]["LastUpdatedOn"].ToString();
            string CompliantStatus = dsComplaintdata.Tables[0].Rows[0]["complaintStatus"].ToString();
            string CompliantStatusDesc = dsComplaintdata.Tables[0].Rows[0]["Status_Name"].ToString();
            string ExpectedClosureDate = dsComplaintdata.Tables[0].Rows[0]["ExpectedClosureDate"].ToString();
            string ActualClosureDate = dsComplaintdata.Tables[0].Rows[0]["ActualClosureDate"].ToString();

            ViewState["ComplaintStatus"] = CompliantStatus.ToString();
            lblcomplaintID.Text = ComplaintId;

            lblCreatedOn.Text = CreatedOn;
            lblCurrentStatus.Text = CompliantStatusDesc;
            lblExpClosure.Text = ExpectedClosureDate;

            txtExpectedClosureDate.Text = ExpectedClosureDate;
            txtActualClosureDate.Text = ActualClosureDate;
            txtContactedONAfterReview.Text = dsComplaintdata.Tables[0].Rows[0]["CustometContactedOnAfterReview"].ToString();
            txtSampleReceivedTime.Text = dsComplaintdata.Tables[0].Rows[0]["SampleReceivedDateTime"].ToString();
            txtComplaintOutCome.Text = dsComplaintdata.Tables[0].Rows[0]["OutComeofComplaint"].ToString();
            txtCost.Text = dsComplaintdata.Tables[0].Rows[0]["TotalCost"].ToString();
            cstType.SelectedValue = dsComplaintdata.Tables[0].Rows[0]["CustomerType"].ToString();

            lblKBcomplaintID.Text = ComplaintId;
            lblKBCompType.Text = ddTypeOfComplaint.SelectedItem.Text.ToString();
            lblKBProdItem.Text = ddCategory.SelectedItem.Text.ToString();
            if (ddComplaintForwardedTO.SelectedItem.Value.ToString() != "0")
            {
                lblKBUnit.Text = ddComplaintForwardedTO.SelectedItem.Text.ToString();
            }
        }

        //Show Action Updated
        if (dsComplaintdata.Tables[1].Rows.Count != 0)
        {
            string role = "";
            string ActionText = "";
            for (int i = 0; i < dsComplaintdata.Tables[1].Rows.Count; i++)
            {
                role = dsComplaintdata.Tables[1].Rows[i]["ActionByRole"].ToString();
                ActionText = dsComplaintdata.Tables[1].Rows[i]["ActionTaken"].ToString();
                if (role == "2")//CRM Executive
                {
                    txtActionUpdatedbyCRM.Text = ActionText;
                }
                else if (role == "3")//Unit Head
                {
                    txtActionUpdatedbyUH.Text = ActionText;
                }
                else if (role == "4")//QA
                {
                    txtActionUpdatedbyQA.Text = ActionText;
                }
                else if (role == "5")//Production
                {
                    txtActionUpdatedbyProd.Text = ActionText;
                }
                else if (role == "6")//Materials
                {
                    txtActionUpdatedbyMat.Text = ActionText;
                }
                else if (role == "7")//Despatch
                {
                    txtActionUpdatedbyDespatch.Text = ActionText;
                }
                else if (role == "8")//Legal
                {
                    txtActionUpdatedbyLegal.Text = ActionText;
                }
                else if (role == "10")//materials
                {
                    txtActionUpdatedbyMaintenance.Text = ActionText;
                }
                else if (role == "11")//purchase
                {
                    txtActionUpdatedbyPurchase.Text = ActionText;
                }
                else if (role == "12")//NPD
                {
                    txtActionUpdatedbyNPD.Text = ActionText;
                }

            }
        }

        //Show Feedback Data
        if (dsComplaintdata.Tables[2].Rows.Count != 0)
        {
            ddFeedbackSatisfaction.SelectedValue = dsComplaintdata.Tables[2].Rows[0]["Complaint_Satisfaction"].ToString();
            txtComplaintHandlingComment.Text = dsComplaintdata.Tables[2].Rows[0]["Complaint_Satisfaction_Comment"].ToString();
            ddEmployeeBehavior.SelectedValue = dsComplaintdata.Tables[2].Rows[0]["Complaint_Employee_Behavior"].ToString();
            txtComplaintTimelinessofResp.Text = dsComplaintdata.Tables[2].Rows[0]["Complaint_ResponseandAction_Timeliness"].ToString();
            string reqMet = dsComplaintdata.Tables[2].Rows[0]["Complaint_Requirement_Met"].ToString();
            if (reqMet.ToUpper() == "TRUE" || reqMet == "1")
            {
                RBRequirementMet.SelectedValue = "1";
            }
            else if (reqMet.ToUpper() == "FALSE" || reqMet == "0")
            {
                RBRequirementMet.SelectedValue = "0";
            }
            txtComplaintReqNotMetComment.Text = dsComplaintdata.Tables[2].Rows[0]["Complaint_Requirement_Comment"].ToString();
            txtFeedback.Text = dsComplaintdata.Tables[2].Rows[0]["Complaint_Feedback"].ToString();
            ViewState["FeedbackStatus"] = "1";
        }

        //Show Legal Section
        if (dsComplaintdata.Tables[3].Rows.Count != 0)
        {
            txtlgDisputePoints.Text = dsComplaintdata.Tables[3].Rows[0]["Dispute_Points"].ToString();
            txtlgPointsInChecklist.Text = dsComplaintdata.Tables[3].Rows[0]["PointsInChecklist"].ToString();
            ddllgActionInitiatedThrough.SelectedValue = dsComplaintdata.Tables[3].Rows[0]["Action_InitiatedThrough"].ToString();
            txtlgDisputeCompletedOn.Text = dsComplaintdata.Tables[3].Rows[0]["Dispute_CompletedOn"].ToString();
        }

        //Show Uploaded Files on GridView
        ShowFilesinGrid(ComplaintId);

        //Show KB Destails
        myDBOperation.BindKBwithGridview(dgKB, ComplaintId);

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strComplaintID = "";
        //int intComplaintUnit = ddDivision.SelectedIndex;
        string strCustomerName = txtComplainantName.Text;
        string strCustomerPlace = txtComplaintPlace.Text;
        string strCustomerMobile = txtCustomerMobile.Text;
        string strCustomerTelephone = txtCustomerTelephone.Text;
        string strCustomerAddress = txtCustomerAddress.Text;
        string strComplaintNature = ddNatureOfComplaint.SelectedItem.Value.ToString();
        string strArea = ddlArea.SelectedItem.Value.ToString();
        string strComplaintType = ddTypeOfComplaint.SelectedItem.Value.ToString();
        string strProductDetails = txtProductDetails.Text;
        int intQuantity = 0;
        try
        {
            intQuantity = Convert.ToInt32(txtQuantity.Text);
        }
        catch
        {
            //ShowMessage(diverror, "Error", "Quantity should be a numeric value");
            lblMessageText1.Text = "Error: Quantity should be a numeric value";
            lblMessageText1.CssClass = "alert-box error";
            lblMessageText1.Focus();
            return;
        }
        string strProductBatchNO = txtBatchNO.Text;
        string strRegistrationDate = txtCallTakenTime.Text;
        string strCategory = ddCategory.SelectedItem.Value.ToString();
        string CompDescr = txtCompDetails.Text.ToString();
        string contactedBy = txtContactedByName.Text;
        string addedBy = Session["CRMUserID"].ToString();
        string Distributor = txtDistributor.Text;
        string strcstType = cstType.SelectedItem.Value.ToString();

        strComplaintID = myDBOperation.CreateComplaint(strCustomerName, strCustomerPlace, strCustomerMobile, strCustomerTelephone, strCustomerAddress, strComplaintNature, strArea, strComplaintType, strProductDetails, intQuantity, strProductBatchNO, strRegistrationDate, strCategory, CompDescr, contactedBy, addedBy, Distributor, 0, strcstType);
        if (strComplaintID.Length > 1)
        {
            ViewState["ComplaintID"] = strComplaintID;
            ViewState["ComplaintStatus"] = "1";
            btnSubmit.Enabled = false;
            btnUpdate.Enabled = true;

            //Send Email While creating the CRM request
            strToMail = "";
            strCCMail = "";
            strToMail = CommonClass.GetEmailByRoleandUnit("1", "2");//CRMEmailID
            strCCMail = CommonClass.GetEmailForRSM(ddlArea.SelectedItem.Value.ToString());//RSM Email

            if (ddNatureOfComplaint.SelectedItem.Text.ToUpper() == "MAJOR")
            {
                MgmttMail = CommonClass.GetEmailByRoleandUnit("1", "9");//MGMT Mail 
                if (strCCMail.Length > 0)
                {
                    strCCMail = strCCMail + ";" + MgmttMail;
                }
                else
                {
                    strCCMail = MgmttMail;
                }
            }
            CommonClass.sendmails(strComplaintID, null, "NEW", "New CRM Request has been created in the portal", "CRM Executive", CommonClass.GetDateTimeIST().ToString(), strToMail, strCCMail, "Complaint Created");

            //ShowMessage(divSuccess, "Success", "New complaint has been created in system.");
            lblMessageText1.Text = "Success: New complaint ID: <B>'" + strComplaintID + "'</B> registered in system. An email notification sent to '" + strToMail + "'. You can click on the 'Forward' button under 'Corrective Action' tab to forward the complaint to a company Unit or close the complaint if it is not major.";
            lblMessageText1.CssClass = "alert-box success";
            lblMessageText1.Focus();
            //Activate All buttons based on new status and and logged in user role
            EnableDisableFormAndButtonFieldsForRole(Session["CRMselectedRole"].ToString(), ViewState["ComplaintStatus"].ToString());

            //Complaint Header Fields
            lblcomplaintID.Text = strComplaintID;
            lblCreatedOn.Text = CommonClass.GetDateTimeIST().ToString();
            lblCurrentStatus.Text = "Complaint Created";
            lblExpClosure.Text = "";

            //KB Header Fields
            lblKBcomplaintID.Text = strComplaintID;
            lblKBCompType.Text = ddTypeOfComplaint.SelectedItem.Text.ToString();
            lblKBProdItem.Text = ddCategory.SelectedItem.Text.ToString();
            if (ddComplaintForwardedTO.SelectedItem.Value != "0")
            {
                lblKBUnit.Text = ddComplaintForwardedTO.SelectedItem.Text.ToString();
            }
        }
        else
        {
            ViewState["ComplaintID"] = null;
            ViewState["ComplaintStatus"] = "0";
            //ShowMessage(diverror, "Error", "complaint creation failed. Please verify the details and save again.");
            lblMessageText1.Text = "Error: complaint creation failed. Please verify the details and save again.";
            lblMessageText1.CssClass = "alert-box error";
            lblMessageText1.Focus();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["ComplaintID"] == null)
        {
            //ShowMessage(diverror, "Error", "complaint update failed. There are no complaint id associated with this request");
            lblMessageText1.Text = "Error: complaint update failed. There are no complaint id associated with this request";
            lblMessageText1.CssClass = "alert-box error";
            lblMessageText1.Focus();
            return;
        }
        string strComplaintID = ViewState["ComplaintID"].ToString();
        //int intComplaintUnit = ddDivision.SelectedIndex;
        string strCustomerName = txtComplainantName.Text;
        string strCustomerPlace = txtComplaintPlace.Text;
        string strCustomerMobile = txtCustomerMobile.Text;
        string strCustomerTelephone = txtCustomerTelephone.Text;
        string strCustomerAddress = txtCustomerAddress.Text;
        string strComplaintNature = ddNatureOfComplaint.SelectedItem.Value.ToString();
        string strArea = ddlArea.SelectedItem.Value.ToString();
        string strComplaintType = ddTypeOfComplaint.SelectedItem.Value.ToString();
        string strProductDetails = txtProductDetails.Text;
        int intQuantity = 0;
        try
        {
            intQuantity = Convert.ToInt32(txtQuantity.Text);
        }
        catch
        {
            //ShowMessage(diverror, "Error", "Quantity should be a numeric value");
            lblMessageText1.Text = "Error: Quantity should be a numeric value";
            lblMessageText1.CssClass = "alert-box error";
            lblMessageText1.Focus();
            return;
        }
        string strProductBatchNO = txtBatchNO.Text;
        string strRegistrationDate = txtCallTakenTime.Text;
        string strCategory = ddCategory.SelectedItem.Value.ToString();
        string CompDescr = txtCompDetails.Text.ToString();
        string contactedBy = txtContactedByName.Text;
        string ModifiedBy = Session["CRMUserID"].ToString();
        string Distributor = txtDistributor.Text;
        string strcstType = cstType.SelectedItem.Value.ToString();
        int intUpdateComplaint = myDBOperation.UpdateComplaint(strComplaintID, strCustomerName, strCustomerPlace, strCustomerMobile, strCustomerTelephone, strCustomerAddress, strComplaintNature, strArea, strComplaintType, strProductDetails, intQuantity, strProductBatchNO, strRegistrationDate, strCategory, CompDescr, contactedBy, ModifiedBy, Distributor, 0, strcstType);
        if (intUpdateComplaint == 1)
        {
            //ShowMessage(divSuccess, "Success", "complaint details has been updated in system.");
            lblMessageText1.Text = "Success: complaint details has been updated in system.";
            lblMessageText1.CssClass = "alert-box success";
            lblMessageText1.Focus();

            //KB Header Fields
            lblKBCompType.Text = ddTypeOfComplaint.SelectedItem.Text.ToString();
            lblKBProdItem.Text = ddCategory.SelectedItem.Text.ToString();

            //Activate All buttons based on new status and and logged in user role
            EnableDisableFormAndButtonFieldsForRole(Session["CRMselectedRole"].ToString(), ViewState["ComplaintStatus"].ToString());
        }
        else
        {
            //ShowMessage(diverror, "Error", "complaint update failed. Please verify the details and save again");
            lblMessageText1.Text = "Error: complaint update failed. Please verify the details and save again";
            lblMessageText1.CssClass = "alert-box error";
            lblMessageText1.Focus();
        }
    }

    private string GetActionTextByRole(string role)
    {
        string ActionText = "";
        if (role == "1" || role == "9" || role == "13")//admin/topmgmt/qahead
        {
            ActionText = "";
            //Nothng to do;
        }
        else if (role == "2")//CRM Executive
        {
            ActionText = txtActionUpdatedbyCRM.Text.ToString();
        }
        else if (role == "3")//Unit Head
        {
            ActionText = txtActionUpdatedbyUH.Text.ToString();
        }
        else if (role == "4")//QA
        {
            ActionText = txtActionUpdatedbyQA.Text.ToString();
        }
        else if (role == "5")//Production
        {
            ActionText = txtActionUpdatedbyProd.Text.ToString();
        }
        else if (role == "6")//Materials
        {
            ActionText = txtActionUpdatedbyMat.Text.ToString();
        }
        else if (role == "7")//Despatch
        {
            ActionText = txtActionUpdatedbyDespatch.Text.ToString();
        }
        else if (role == "8")//Legal
        {
            ActionText = txtActionUpdatedbyLegal.Text.ToString();
        }
        else if (role == "10")//Maintenance
        {
            ActionText =  txtActionUpdatedbyMaintenance.Text.ToString();
        }
        else if (role == "11")//Purchase
        {
            ActionText = txtActionUpdatedbyPurchase.Text.ToString();
        }
        else if (role == "12")//NPD
        {
            ActionText =  txtActionUpdatedbyNPD.Text.ToString();
        }

        return ActionText;

    }

    //Invoke this function when Forward button is clicked. Save Data will not invoke below function
    private void UpdateActionAndStatus(string NextActionRoleID, int StatusID)
    {
        string LoggedInRoleID = Session["CRMselectedRole"].ToString();
        if (ddNatureOfComplaint.SelectedItem.Text.ToUpper() == "MAJOR" || StatusID != 20)
        {
            if (ddComplaintForwardedTO.SelectedItem.Value.ToString() == "" || ddComplaintForwardedTO.SelectedItem.Value.ToString() == "0")
            {
                lblMessageText2.Text = "Error: Please select Unit Name";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
            else
            {
                if (LoggedInRoleID == "2")
                {
                    if (ddComplaintForwardedTO.SelectedItem.Value.ToString() == "11")//Yamuna/Natuarals Unit
                    {
                        if (ddSubDivision1.SelectedValue.ToString() == "0")
                        {
                            lblMessageText2.Text = "Error: Please select Sub Unit Name for " + ddComplaintForwardedTO.SelectedItem.Text.ToString();
                            lblMessageText2.CssClass = "alert-box error";
                            lblMessageText2.Focus();
                            return;
                        }
                    }
                    else if (ddComplaintForwardedTO.SelectedItem.Value.ToString() == "10")
                    {
                        if (ddSubDivision2.SelectedValue.ToString() == "0")
                        {
                            lblMessageText2.Text = "Error: Please select Sub Unit Name for " + ddComplaintForwardedTO.SelectedItem.Text.ToString();
                            lblMessageText2.CssClass = "alert-box error";
                            lblMessageText2.Focus();
                            return;
                        }
                    }
                }
            }
        }
        if (StatusID == 20)
        {
            string strCost = txtCost.Text.ToString();
            if (strCost.Length == 0)
            {
                lblMessageText2.Text = "Error: 'Total Cost' should not be empty.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
            else if (!IsNumeric(strCost))
            {
                lblMessageText2.Text = "Error: 'Total Cost' should be numeric.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
            if (txtExpectedClosureDate.Text == "____/__/__ __:__" || txtExpectedClosureDate.Text == "")
            {
                lblMessageText2.Text = "Error: 'Expected Closure Date' should not be empty.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
            if (txtActualClosureDate.Text == "____/__/__ __:__" || txtActualClosureDate.Text == "")
            {
                lblMessageText2.Text = "Error: 'Actual Closure Date' should not be empty.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
            if (txtContactedONAfterReview.Text == "____/__/__ __:__" || txtContactedONAfterReview.Text == "")
            {
                lblMessageText2.Text = "Error: 'Customer Contacted After Review' should not be empty.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
            if (txtSampleReceivedTime.Text == "____/__/__ __:__" || txtSampleReceivedTime.Text == "")
            {
                lblMessageText2.Text = "Error: 'Sample Received Time' should not be empty.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
        }

        //Get Action text updated by selected Role
        string actionTaken = GetActionTextByRole(LoggedInRoleID);
        if (LoggedInRoleID == "1" || LoggedInRoleID == "2" || LoggedInRoleID == "3" || LoggedInRoleID == "4")
        {
            if (!SaveCorrectiveActionFields())
            {
                return;
            }
        }
        if (actionTaken.Length == 0)
        {
            lblMessageText2.Text = "Error: Please update corrective action taken. Enter 'NA' if no details are available";
            lblMessageText2.CssClass = "alert-box error";
            lblMessageText2.Focus();
            return;
        }

        if (StatusID == 20)
        {
            if (txtComplaintOutCome.Text.Length == 0)
            {
                lblMessageText2.Text = "Error: complaint outcome is mandatory for the final closure of the compliant. Please update the details and try again.";
                lblMessageText2.CssClass = "alert-box error";
                lblMessageText2.Focus();
                return;
            }
        }
        //saving the action and updating the status of request.
        bool actionUpd = myDBOperation.UpdateAction(ViewState["ComplaintID"].ToString(), LoggedInRoleID, Session["CRMUserID"].ToString(), actionTaken, StatusID);
        if (NextActionRoleID.Length > 0 && (actionUpd))
        {
            myDBOperation.AddToWorklist(ViewState["ComplaintID"].ToString(), NextActionRoleID, Session["CRMUserID"].ToString(), LoggedInRoleID);

            //Send Email While assigning the action to other role users
            strToMail = "";
            strCCMail = "";

            string NewStatusinMail = "Complaint Assigned";
            if (NextActionRoleID.ToString() == "2")
            {
                NewStatusinMail += " to CRM";
            }
            else if (NextActionRoleID.ToString() == "3")
            {
                NewStatusinMail += " to Unit Head";
            }
            else if (NextActionRoleID.ToString() == "4")
            {
                NewStatusinMail += " to QA";
            }
            else if (NextActionRoleID.ToString() == "5")
            {
                NewStatusinMail += " to Production";
            }
            else if (NextActionRoleID.ToString() == "6")
            {
                NewStatusinMail += " to Materials";
            }
            else if (NextActionRoleID.ToString() == "7")
            {
                NewStatusinMail += " to Despatch";
            }
            else if (NextActionRoleID.ToString() == "8")
            {
                NewStatusinMail += " to Legal";
            }
            else if (NextActionRoleID.ToString() == "9")
            {
                NewStatusinMail += " to Top Management";
            }
            else if (NextActionRoleID.ToString() == "10")
            {
                NewStatusinMail += " to Maintenance";
            }
            else if (NextActionRoleID.ToString() == "11")
            {
                NewStatusinMail += " to Purchase";
            }
            else if (NextActionRoleID.ToString() == "12")
            {
                NewStatusinMail += " to NPD";
            }
            else if (NextActionRoleID.ToString() == "13")
            {
                NewStatusinMail += " to QA Head";
            }
        
            strToMail = CommonClass.GetEmailByRoleandUnit(ddComplaintForwardedTO.SelectedItem.Value.ToString(), NextActionRoleID);//New assigned RoleUser EmailID
            strCCMail = CommonClass.GetEmailByRoleandUnit(ddComplaintForwardedTO.SelectedItem.Value.ToString(), LoggedInRoleID);//Created/updated RoleUser EmailID
            CommonClass.sendmails(ViewState["ComplaintID"].ToString(), (DataTable)ViewState["dtComplaintdata"], "FORWARD", actionTaken, Session["CRMselectedRoleName"].ToString(), CommonClass.GetDateTimeIST().ToString(), strToMail, strCCMail, NewStatusinMail);
            if (LoggedInRoleID == "8")
            {
                lblMessageLG.Text = "Success: Corrective action updated and assigned the complaint to new Role. An email notification sent to '" + strToMail + "'.";
                lblMessageLG.CssClass = "alert-box success";
            }
            else
            {
                lblMessageText2.Text = "Success: Corrective action updated and assigned the complaint to new Role. An email notification sent to '" + strToMail + "'.";
                lblMessageText2.CssClass = "alert-box success";
                lblMessageText2.Focus();
            }
            //Activate/deactivate All buttons based on new status and and logged in user role
            ViewState["ComplaintStatus"] = StatusID.ToString();
            EnableDisableFormAndButtonFieldsForRole(Session["CRMselectedRole"].ToString(), ViewState["ComplaintStatus"].ToString());
        }
        else if (actionUpd) //When Final Closure click
        {
            myDBOperation.DeleteWorklist(ViewState["ComplaintID"].ToString());

            //Send Email While assigning the action to other role users
            strToMail = "";
            strCCMail = "";
            strToMail = CommonClass.GetEmailByRoleandUnit(ddComplaintForwardedTO.SelectedItem.Value.ToString(), "2");//CRMEmailID            
            CommonClass.sendmails(ViewState["ComplaintID"].ToString(), (DataTable)ViewState["dtComplaintdata"], "CLOSE", txtComplaintOutCome.Text.ToString(), Session["CRMselectedRoleName"].ToString(), CommonClass.GetDateTimeIST().ToString(), strToMail, strCCMail, "Complaint Closed");
            //Activate/deactivate All buttons based on new status and and logged in user role
            ViewState["ComplaintStatus"] = StatusID.ToString();
            EnableDisableFormAndButtonFieldsForRole(Session["CRMselectedRole"].ToString(), ViewState["ComplaintStatus"].ToString());

            lblMessageText2.Text = "Success: Corrective action updated and complaint has been closed. No further modifications are allowed. An email notification sent to '" + strToMail + "'.";
            lblMessageText2.CssClass = "alert-box success";
            lblMessageText2.Focus();
        }
        else
        {
            lblMessageText2.Text = "Error: Corrective action could not save. Please verify the details and try again";
            lblMessageText2.CssClass = "alert-box error";
            lblMessageText2.Focus();
        }

    }

    //protected void btnSave1_Click(object sender, EventArgs e)
    //{

    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FinalAction = txtComplaintOutCome.Text.ToString();
        string LoggedInRoleID = Session["CRMselectedRole"].ToString();
        string actionTaken = GetActionTextByRole(LoggedInRoleID);

        if (SaveCorrectiveActionFields())
        {
            bool updstatus = myDBOperation.UpdateAction(ViewState["ComplaintID"].ToString(), LoggedInRoleID, Session["CRMUserID"].ToString(), actionTaken, 0);
            lblMessageText2.Text = "Success: Corrective actions saved. Cick on the 'Forward' button to forward the complaint to concerned role.";
            lblMessageText2.CssClass = "alert-box success";
            lblMessageText2.Focus();
        }
    }

    private bool SaveCorrectiveActionFields()
    {
        string strUnitID = ddComplaintForwardedTO.SelectedValue.ToString();
        string SubUnit = "";
        if (strUnitID == "11")
        {
            SubUnit = ddSubDivision1.SelectedValue.ToString();
        }
        else if (strUnitID == "10")
        {
            SubUnit = ddSubDivision2.SelectedValue.ToString();
        }
        string strExpectedClosureDate = txtExpectedClosureDate.Text.ToString();
        string strActualClosureDate = txtActualClosureDate.Text.ToString();
        string strContactedONAfterReview = txtContactedONAfterReview.Text.ToString();
        string strSampleReceivedTime = txtSampleReceivedTime.Text.ToString();
        string strCost = txtCost.Text.ToString();
        string strComplaintOutCome = txtComplaintOutCome.Text.ToString();
        if (!IsNumeric(strCost))
        {
            lblMessageText2.Text = "Error: 'Total Cost' should be numeric.";
            lblMessageText2.CssClass = "alert-box error";
            lblMessageText2.Focus();
            return false;
        }
        //if (strUnitID.Length == 0 || strUnitID == "0")
        //{
        //    lblMessageText2.Text = "Error: Please select Company Unit Name.";
        //    lblMessageText2.CssClass = "alert-box error";
        //    return false;
        //}
        bool updstatus = myDBOperation.CorrectiveActionFields(ViewState["ComplaintID"].ToString(), strUnitID, SubUnit, strExpectedClosureDate,
            strActualClosureDate, strContactedONAfterReview, strSampleReceivedTime, strCost, strComplaintOutCome, Session["CRMUserID"].ToString());

        if (!updstatus)
        {
            lblMessageText2.Text = "Error: Error while saving the details. Please verify the details and try again";
            lblMessageText2.CssClass = "alert-box error";
            lblMessageText2.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void btnForwardToQA_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "4";
        UpdateActionAndStatus(NextActionRoleID, 2);
    }

    protected void btnForwardToUnitHead_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "3";
        UpdateActionAndStatus(NextActionRoleID, 6);
    }

    protected void btnForwardToProduction_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "5";
        UpdateActionAndStatus(NextActionRoleID, 3);
    }

    protected void btnForwardToDespatch_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "7";
        UpdateActionAndStatus(NextActionRoleID, 5);
    }

    protected void btnForwardToMaterials_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "6";
        UpdateActionAndStatus(NextActionRoleID, 4);
    }

    protected void btnForwardToMaintenance_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "10";
        UpdateActionAndStatus(NextActionRoleID, 8);
    }
    protected void btnForwardToPurchase_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "11";
        UpdateActionAndStatus(NextActionRoleID, 9);
    }
    protected void btnForwardToNPD_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "12";
        UpdateActionAndStatus(NextActionRoleID, 10);
    }


    protected void btnForwardToLegal_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "8";
        UpdateActionAndStatus(NextActionRoleID, 7);
    }


    protected void btnForwardToCRM_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "2";
        UpdateActionAndStatus(NextActionRoleID, 19);
    }

    protected void btnSavelg_Click(object sender, EventArgs e)
    {
        string LoggedInRoleID = Session["CRMselectedRole"].ToString();
        string actionTaken = GetActionTextByRole(LoggedInRoleID);
        bool updstatus = myDBOperation.UpdateAction(ViewState["ComplaintID"].ToString(), LoggedInRoleID, Session["CRMUserID"].ToString(), actionTaken, 0);
        if (updstatus)
        {
            if (myDBOperation.UpdateLegalActions(ViewState["ComplaintID"].ToString(), txtlgDisputePoints.Text.ToString(), ddllgActionInitiatedThrough.SelectedValue.ToString(), txtlgPointsInChecklist.Text.ToString(), txtlgDisputeCompletedOn.Text.ToString(), Session["CRMUserID"].ToString()))
            {
                lblMessageLG.Text = "Success: Details Saved. Cick on 'Forward To Unit Head' button to forward the complaint.";
                lblMessageLG.CssClass = "alert-box success";
            }
            else
            {
                lblMessageLG.Text = "Error: Legal Actions could not save. Please verify the details and try again.";
                lblMessageLG.CssClass = "alert-box error";
            }
        }
        else
        {
            lblMessageLG.Text = "Error: Legal Actions could not save. Please verify the details and try again.";
            lblMessageLG.CssClass = "alert-box error";
        }

    }

    protected void btnForwardedByLegal_Click(object sender, EventArgs e)
    {
        string NextActionRoleID = "3";
        if (myDBOperation.UpdateLegalActions(ViewState["ComplaintID"].ToString(), txtlgDisputePoints.Text.ToString(), ddllgActionInitiatedThrough.SelectedValue.ToString(), txtlgPointsInChecklist.Text.ToString(), txtlgDisputeCompletedOn.Text.ToString(), Session["CRMUserID"].ToString()))
        {
            UpdateActionAndStatus(NextActionRoleID, 6);
        }
        else
        {
            lblMessageLG.Text = "Error: Legal Actions could not save. Please verify the details and try again.";
            lblMessageLG.CssClass = "alert-box error";
        }
    }

    protected void btnComplaintClosure_Click(object sender, EventArgs e)
    {
        if (SaveCorrectiveActionFields())
        {
            UpdateActionAndStatus("", 20);
        }
    }


    protected void btnFeedback_Click(object sender, EventArgs e)
    {
        if (myDBOperation.CreateFeedback(ViewState["ComplaintID"].ToString(), ddFeedbackSatisfaction.SelectedValue.ToString(), txtComplaintHandlingComment.Text, ddEmployeeBehavior.SelectedValue.ToString(), txtComplaintTimelinessofResp.Text.ToString(), RBRequirementMet.SelectedItem.Value.ToString(), txtComplaintReqNotMetComment.Text, txtFeedback.Text))
        {
            //ShowMessage(divSuccess, "Success", "Feedback saved.");
            lblMessageText3.Text = "Success: Feedback saved.";
            lblMessageText3.CssClass = "alert-box success";
            ViewState["FeedbackStatus"] = "1";
            //Activate final closure button based on new status and and logged in user role
            EnableDisableFormAndButtonFieldsForRole(Session["CRMselectedRole"].ToString(), ViewState["ComplaintStatus"].ToString());
        }
        else
        {
            //ShowMessage(diverror, "Error", "Feedback could not save. Please verify the details and try again.");
            lblMessageText3.Text = "Error: Feedback could not save. Please verify the details and try again.";
            lblMessageText3.CssClass = "alert-box error";
        }
    }


    private void ShowFilesinGrid(string ComplaintID)
    {
        Documents objDocs = new Documents();
        objDocs.BindFiles(dgDocuments, ComplaintID);
    }

    protected void dgDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {
            DownloadFile(e);
        }
    }

    private void DownloadFile(DataGridCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        Documents objDocs = new Documents();
        ds = objDocs.DownloadFile(ViewState["ComplaintID"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            byte[] graphByte = (byte[])ds.Tables[0].Rows[e.Item.ItemIndex]["FileContent"];
            MemoryStream mstream = new MemoryStream(graphByte);
            Context.Response.ContentType = ds.Tables[0].Rows[e.Item.ItemIndex]["FileContent"].ToString();
            Context.Response.Charset = null;
            Response.Clear();

            Response.ContentType = ds.Tables[0].Rows[e.Item.ItemIndex]["ContentType"].ToString();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ds.Tables[0].Rows[e.Item.ItemIndex]["fileName"].ToString() + "\"");
            Response.Charset = null;
            mstream.WriteTo(Context.Response.OutputStream);
            Context.Response.End();
        }
    }

    protected void btnSaveDocs_Click(object sender, EventArgs e)
    {
        UploadFile(ViewState["ComplaintID"].ToString());
        ShowFilesinGrid(ViewState["ComplaintID"].ToString());
    }

    protected int UploadFile(string CompliantID)
    {
        int ReturnValue = 0;
        //string x = fileDocs.PostedFile.ContentLength.ToString();
        if (fileDocs.HasFile)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            try
            {
                cmd.Connection = conn;
                conn.Open();
                //string[] filetype;
                //filetype = fileClientApproval.FileName.Split('.');
                //string ContentType = filetype[1].ToString();
                Stream st = fileDocs.PostedFile.InputStream;
                int fileLen = fileDocs.PostedFile.ContentLength;
                //string fileContentType = fuDocument.PostedFile.ContentType;
                byte[] fileBinaryData = new byte[fileLen];
                fileDocs.PostedFile.InputStream.Read(fileBinaryData, 0, fileLen);
                //int count = 0;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CRM_Upload_File";
                cmd.Parameters.AddWithValue("@Complaint_ID", CompliantID.ToString());
                cmd.Parameters.AddWithValue("@File_Name", fileDocs.FileName);
                cmd.Parameters.AddWithValue("@Content_Type", fileDocs.PostedFile.ContentType.Trim());
                cmd.Parameters.AddWithValue("@Uploaded_File", fileBinaryData);
                cmd.Parameters.AddWithValue("@Document_Type", ddlDocType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Document_Details", txtDocDetails.Text.ToString());
                cmd.Parameters.AddWithValue("@Uploaded_By", Session["CRMUserID"].ToString());
                cmd.Parameters.AddWithValue("@Uploaded_ByRole", Session["CRMselectedRole"].ToString());
                cmd.ExecuteNonQuery();
                conn.Close();
                //if (count > 0)
                //{
                //    ReturnValue = 1;
                //}
                //else
                //{
                //    ReturnValue = 0;
                //}
                ReturnValue = 1;
            }
            catch (Exception ex)
            {
                ReturnValue = 0;

            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
        return ReturnValue;
    }


    protected void btnSaveKB_Click(object sender, EventArgs e)
    {
        string Unit = "";
        if (ddComplaintForwardedTO.SelectedItem.Value.ToString() != "" && ddComplaintForwardedTO.SelectedItem.Value.ToString() != "0")
        {
            Unit = ddComplaintForwardedTO.SelectedItem.Text.ToString();
        }
        if (myDBOperation.CreateKB(ViewState["ComplaintID"].ToString(), ddCategory.SelectedItem.Text.ToString(),
            Unit.ToString(), ddTypeOfComplaint.SelectedItem.Text.ToString()
            , txtRootCauseAnalysis.Text.ToString(), txtKBDetails.Text.ToString(), txtKBSolution.Text.ToString(), Session["CRMUserID"].ToString()))
        {
            lblMessageText5.Text = "Success: Knowledge Management information updated.";
            lblMessageText5.CssClass = "alert-box success";
            myDBOperation.BindKBwithGridview(dgKB, ViewState["ComplaintID"].ToString());
        }
        else
        {
            lblMessageText5.Text = "Error: Knowledge Management information could not save. Please verify the details and try again.";
            lblMessageText5.CssClass = "alert-box error";
        }
    }

    protected void dgKB_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {
            if (myDBOperation.DeleteKB(e.CommandArgument.ToString()))
            {
                lblMessageText5.Text = "Success: Knowledge Management information deleted.";
                lblMessageText5.CssClass = "alert-box success";
                myDBOperation.BindKBwithGridview(dgKB, ViewState["ComplaintID"].ToString());
            }
            else
            {
                lblMessageText5.Text = "Error: Knowledge Management information could not delete. Please verify the details and try again.";
                lblMessageText5.CssClass = "alert-box error";
            }
        }
    }


    private void disableAllButtons()
    {
        btnComplaintClosure.Enabled = false;
        btnFeedback.Enabled = false;
        btnForwardToCRM.Enabled = false;
        btnForwardToDespatch.Enabled = false;
        btnForwardToLegal.Enabled = false;
        btnForwardToMaterials.Enabled = false;
        btnForwardToProduction.Enabled = false;
        btnMaintenance.Enabled = false;
        btnPurchase.Enabled = false;
        btnNPD.Enabled = false;
        btnForwardToQA.Enabled = false;
        btnForwardToUnitHead.Enabled = false;
        btnSave.Enabled = false;
        btnSaveDocs.Enabled = false;
        btnSubmit.Enabled = false;
        btnSaveKB.Enabled = false;
        btnSaveKB.Enabled = false;
        btnSavelg.Enabled = false;
        btnForwardedByLegal.Enabled = false;
    }

    private void EnableDisableFormAndButtonFieldsForRole(string role, string status)
    {
        //******* Roles ***********
        //*************************
        // 1 Administrator
        //2 CRM Executive
        //3	Unit Head
        //4	QA
        //5	Production
        //6	Materials
        //7	Despatch
        //8	Legal
        //9	Top mgmt
        //10 Maintenance
        //11 Purchase
        //12 NPD
        //13 QA Head
        //******* Status ***********
        //*************************
        //1 - Complaint Created             
        //2 - Pending with QA               
        //3 - Pending With Production       
        //4 - Pending With Materials        
        //5 - Pending With Despatch          
        //6 - Pending With Unit Head   
        //7 - Pending With Legal      

        //8 - Pending With Maintenance          
        //9 - Pending With Purchase         
        //10 - Pending With NPD              

        //19 - Partially closed by Unit Head 
        //20 - Complaint Closed by CRM         

        disableAllButtons();
        EnableDisableCompliantRequestFields(true);
        EnableDisableCorrectiveActionFields(true);
        EnableDisableCorrectiveActionHeaderFields(true);
        EnableDisableFeedbackFields(true);
        EnableDisableKBFields(true);
        EnableDisableLegalFields(true);
        if (role == "1" || role == "9" || role == "13")//admin/topmgmt/qahead
        {
            //Nothng to do;
            if (status == "1")
            {
                //btnSave.Enabled = true;
            }
            //btnSaveDocs.Enabled = true;
        }
        else if (role == "2")//CRM Executive
        {
            if (status == "0")
            {
                EnableDisableCompliantRequestFields(false);
                btnSubmit.Enabled = true;
            }
            else if (status == "1" || status == "19")
            {
                EnableDisableCompliantRequestFields(false);
                EnableDisableCorrectiveActionHeaderFields(false);
                EnableDisableFeedbackFields(false);
                EnableDisableKBFields(false);
                txtActionUpdatedbyCRM.ReadOnly = false;
                btnUpdate.Enabled = true;
                btnSaveKB.Enabled = true;
                if (ddNatureOfComplaint.SelectedItem.Text.ToUpper() != "MAJOR")
                {
                    btnComplaintClosure.Enabled = true;
                }
                else if (ViewState["FeedbackStatus"] != null)
                {
                    if (ViewState["FeedbackStatus"].ToString() == "1")
                    {
                        btnComplaintClosure.Enabled = true;
                    }
                }
                //btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnForwardToLegal.Enabled = true;
                btnFeedback.Enabled = true;
                btnSave.Enabled = true;
            }
            else if (status == "20")
            {
                EnableDisableKBFields(false);
            }
            if (status == "1" || status == "2" || status == "3" || status == "4" || status == "5" || status == "6" || status == "7" || status == "19")
            {
                btnSaveDocs.Enabled = true;
            }
        }
        else if (role == "3")//Unit Head
        {
            if (status == "6")
            {
                EnableDisableCorrectiveActionHeaderFields(false);
                EnableDisableKBFields(false);
                txtActionUpdatedbyUH.ReadOnly = false;
                ddComplaintForwardedTO.Enabled = false;
                if (divSubDivision.Visible)
                {
                    ddSubDivision1.Enabled = false;
                    ddSubDivision2.Enabled = false;
                }
                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToProduction.Enabled = true;
                btnForwardToMaterials.Enabled = true;
                btnForwardToDespatch.Enabled = true;
                btnMaintenance.Enabled = true;
                btnPurchase.Enabled = true;
                btnNPD.Enabled = true;
                btnForwardToLegal.Enabled = true;
                btnForwardToCRM.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }
        else if (role == "4")//QA
        {
            if (status == "2")
            {
                EnableDisableCorrectiveActionHeaderFields(false);
                EnableDisableKBFields(false);
                txtActionUpdatedbyQA.ReadOnly = false;
                ddComplaintForwardedTO.Enabled = false;
                if (divSubDivision.Visible)
                {
                    ddSubDivision1.Enabled = false;
                    ddSubDivision2.Enabled = false;
                }
                btnSave.Enabled = true;
                btnForwardToProduction.Enabled = true;
                btnForwardToMaterials.Enabled = true;
                btnForwardToDespatch.Enabled = true;
                btnMaintenance.Enabled = true;
                btnPurchase.Enabled = true;
                btnNPD.Enabled = true;

                btnForwardToLegal.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }
        else if (role == "5")//Production
        {
            if (status == "3")
            {
                EnableDisableKBFields(false);
                txtActionUpdatedbyProd.ReadOnly = false;

                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }
        else if (role == "6")//Materials
        {
            if (status == "4")
            {
                EnableDisableKBFields(false);
                txtActionUpdatedbyMat.ReadOnly = false;

                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }
        else if (role == "7")//Despatch
        {
            if (status == "5")
            {
                EnableDisableKBFields(false);
                txtActionUpdatedbyDespatch.ReadOnly = false;

                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }
        else if (role == "8")//Legal
        {
            if (status == "7")
            {
                EnableDisableKBFields(false);
                EnableDisableLegalFields(false);
                txtActionUpdatedbyLegal.ReadOnly = false;

                btnSavelg.Enabled = true;
                btnForwardedByLegal.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }

        else if (role == "10")//Maintenance
        {
            if (status == "8")
            {
                EnableDisableKBFields(false);
                txtActionUpdatedbyMaintenance.ReadOnly = false;

                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }

        else if (role == "11")//Purchase
        {
            if (status == "9")
            {
                EnableDisableKBFields(false);
                txtActionUpdatedbyPurchase.ReadOnly = false;

                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }

        else if (role == "12")//NPD
        {
            if (status == "10")
            {
                EnableDisableKBFields(false);
                txtActionUpdatedbyNPD.ReadOnly = false;

                btnSave.Enabled = true;
                btnForwardToQA.Enabled = true;
                btnForwardToUnitHead.Enabled = true;
                btnSaveDocs.Enabled = true;
                btnSaveKB.Enabled = true;
            }
        }
    }

    //Header 
    private void EnableDisableCompliantRequestFields(bool ReadOnly)
    {
        txtComplainantName.ReadOnly = ReadOnly;
        txtComplaintPlace.ReadOnly = ReadOnly;
        txtCustomerMobile.ReadOnly = ReadOnly;
        txtCustomerTelephone.ReadOnly = ReadOnly;
        txtCustomerAddress.ReadOnly = ReadOnly;
        ddNatureOfComplaint.Enabled = !ReadOnly;
        ddlArea.Enabled = !ReadOnly;
        ddTypeOfComplaint.Enabled = !ReadOnly;
        txtProductDetails.ReadOnly = ReadOnly;
        txtQuantity.ReadOnly = ReadOnly;
        txtBatchNO.ReadOnly = ReadOnly;
        txtCallTakenTime.ReadOnly = ReadOnly;
        ddCategory.Enabled = !ReadOnly;
        txtCompDetails.ReadOnly = ReadOnly;
        txtContactedByName.ReadOnly = ReadOnly;
        txtDistributor.ReadOnly = ReadOnly;
        cstType.Enabled = !ReadOnly;
    }

    //Corrective Actions header
    private void EnableDisableCorrectiveActionHeaderFields(bool ReadOnly)
    {
        txtExpectedClosureDate.ReadOnly = ReadOnly;
        txtActualClosureDate.ReadOnly = ReadOnly;
        txtContactedONAfterReview.ReadOnly = ReadOnly;
        txtSampleReceivedTime.ReadOnly = ReadOnly;
        txtComplaintOutCome.ReadOnly = ReadOnly;
        txtCost.ReadOnly = ReadOnly;
        ddComplaintForwardedTO.Enabled = !ReadOnly;
        if (divSubDivision.Visible)
        {
            ddSubDivision1.Enabled = !ReadOnly;
            ddSubDivision2.Enabled = !ReadOnly;
        }
    }

    //Corrective Actions items
    private void EnableDisableCorrectiveActionFields(bool ReadOnly)
    {
        txtActionUpdatedbyCRM.ReadOnly = ReadOnly;
        txtActionUpdatedbyUH.ReadOnly = ReadOnly;
        txtActionUpdatedbyQA.ReadOnly = ReadOnly;
        txtActionUpdatedbyProd.ReadOnly = ReadOnly;
        txtActionUpdatedbyMat.ReadOnly = ReadOnly;
        txtActionUpdatedbyDespatch.ReadOnly = ReadOnly;
        txtActionUpdatedbyLegal.ReadOnly = ReadOnly;
        txtActionUpdatedbyMaintenance.ReadOnly = ReadOnly;
        txtActionUpdatedbyPurchase.ReadOnly = ReadOnly;
        txtActionUpdatedbyNPD.ReadOnly = ReadOnly;
    }

    //Feedback
    private void EnableDisableFeedbackFields(bool ReadOnly)
    {
        ddFeedbackSatisfaction.Enabled = !ReadOnly;
        txtComplaintHandlingComment.ReadOnly = ReadOnly;
        ddEmployeeBehavior.Enabled = !ReadOnly;
        txtComplaintTimelinessofResp.ReadOnly = ReadOnly;
        txtComplaintReqNotMetComment.ReadOnly = ReadOnly;
        txtFeedback.ReadOnly = ReadOnly;
    }

    //KB
    private void EnableDisableKBFields(bool ReadOnly)
    {
        txtRootCauseAnalysis.ReadOnly = ReadOnly;
        txtKBDetails.ReadOnly = ReadOnly;
        txtKBSolution.ReadOnly = ReadOnly;
    }

    //Legal
    private void EnableDisableLegalFields(bool ReadOnly)
    {
        txtlgDisputePoints.ReadOnly = ReadOnly;
        txtlgPointsInChecklist.ReadOnly = ReadOnly;
        ddllgActionInitiatedThrough.Enabled = !ReadOnly;
        txtlgDisputeCompletedOn.ReadOnly = ReadOnly;
        txtActionUpdatedbyLegal.ReadOnly = ReadOnly;
    }

    private bool IsNumeric(string value)
    {
        try
        {
            double x = System.Convert.ToDouble(value);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected void ddComplaintForwardedTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Unitselected = ddComplaintForwardedTO.SelectedItem.Value.ToString();
            if (Unitselected == "11")
            {
                divSubDivision.Visible = true;
                ddSubDivision1.Visible = true;
                ddSubDivision2.Visible = false;
            }
            else if (Unitselected == "10")
            {
                divSubDivision.Visible = true;
                ddSubDivision2.Visible = true;
                ddSubDivision1.Visible = false;
            }
             else
            {
                divSubDivision.Visible = false;
                ddSubDivision2.Visible = false;
                ddSubDivision1.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
}