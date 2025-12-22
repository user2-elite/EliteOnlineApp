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
public partial class NewUserOnBoarding : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    SqlCommand cmd3 = null;
    SqlCommand cmd4 = null;
    SqlCommand cmd5 = null;
    SqlCommand cmd6 = null;

    CommonClass objCommonClass = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        clearMessages();
        if (Session["UserID"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            btnProceedToIT.Enabled = false;
            defaultPanel();
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "5" && Session["selectedRole"].ToString() != "6")
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            //if (Session["selectedRole"].ToString() != "1")
            //{
            //    Response.Redirect("home.aspx");
            //}
            BindDepartmentsandLocations();
            BindAsset();
            BindUserDropdownList(ddlManager);
            BindUserDropdownList(ddlAdminManager);
            BindUserDropdownList(ddlpayrolHR);
            BindUserDropdownList(ddlLocationHR);
            if (Session["EditId"] != null)
            {
                ViewState["EditId"] = Session["EditId"].ToString();
                Session["EditId"] = null;
                BinduserData(ViewState["EditId"].ToString());
                divPassword.Visible = true;
            }
            else
            {
                divPassword.Visible = false;
            }
        }

        enableDisablePanel();
    }

        protected void lnkHRAction1_Click(object sender, EventArgs e)
        {
            resetPanel();
            lnkHRAction1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;

    }

        protected void lnkITAction2_Click(object sender, EventArgs e)
        {
            resetPanel();
            lnkITAction2.CssClass = "Clicked";
            MainView.ActiveViewIndex = 1;
    }

        protected void lnkHRFinalAction_Click(object sender, EventArgs e)
        {
            resetPanel();
            lnkHRFinalAction.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
    }

        private void resetPanel()
        {
            lnkHRAction1.CssClass = "Initial";
            lnkITAction2.CssClass = "Initial";
            lnkHRFinalAction.CssClass = "Initial";
    }

        private void defaultPanel()
        {
            lnkHRAction1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;
            // resetPanel();
            //HRPanel.Visible = true;
            //pnlFileUpload.Visible = true;
    }


    private void enableDisablePanel() {
        //HRPanel.Enabled = false;
        //ITPanel.Enabled = false;
        //AdminPanel.Enabled = false;
        //pnlFileUpload.Enabled = false;
        //ibtnRejectCandidate.Enabled = false;

        ibtnHRSave.Enabled = false;
        btnProceedToIT.Enabled = false;
        ibtnRejectCandidate.Enabled = false;
        btnUpload.Enabled = false;
        btnUploadPO.Enabled = false;
        ibtnITSave.Enabled = false;
        ibtnProceedToHR.Enabled = false;
        ibtnSaveFinal.Enabled = false;
        ibtnSubmitFinal.Enabled = false;
        ibtnHold.Enabled = false;

        string applicationStatus = "";

        if (ViewState["ApplicationStatus"] != null) {
            applicationStatus = ViewState["ApplicationStatus"].ToString();
        }

        if (ViewState["HRUpdate"] == null || (ViewState["HRUpdate"].ToString() == "False" || ViewState["HRUpdate"].ToString() == "0"))
        {
            if (Session["selectedRole"].ToString() == "5" || Session["selectedRole"].ToString() == "1")
            {
                ibtnHRSave.Enabled = true;
                btnProceedToIT.Enabled = true;
                ibtnRejectCandidate.Enabled = true;

                if (ViewState["EditId"] != null)
                {
                    pnlFileUpload.Enabled = true;
                    ibtnRejectCandidate.Enabled = true;
                    btnUpload.Enabled = true;

                }
            }
            //ShowMessage(divAlert, "Status: New submission", "Pending HR submission");

        }


        //If HR action is done
        if (ViewState["HRUpdate"] != null && (ViewState["HRUpdate"].ToString() == "True" || ViewState["HRUpdate"].ToString() == "1") && applicationStatus != "Onboarding Cancelled")
        {
            if (Session["selectedRole"].ToString() == "1" || Session["selectedRole"].ToString() == "6")
            {
                //ITPanel.Enabled = true;
                btnUploadPO.Enabled = true;
                ibtnITSave.Enabled = true;
                ibtnProceedToHR.Enabled = true;
            }
            ShowMessage(divAlert, "Status: HR Submitted", "IT action Pending");
        }

        //If IT action is done
        if (ViewState["ITUpdate"] != null && (ViewState["ITUpdate"].ToString() == "True" || ViewState["ITUpdate"].ToString() == "1") && applicationStatus != "Onboarding Cancelled")
        {
            if (Session["selectedRole"].ToString() == "5" || Session["selectedRole"].ToString() == "1")
            {
                //AdminPanel.Enabled = true;
                ibtnSaveFinal.Enabled = true;
                ibtnSubmitFinal.Enabled = true;
                ibtnHold.Enabled = true;
            }

            ShowMessage(divAlert, "Status: IT Updated", "HR Final action pending");

        }
        


        if (ViewState["EditId"] != null)
        {
            BindFileList(ViewState["EditId"].ToString());
            BindPOFileList(ViewState["EditId"].ToString());
            h3Title.InnerHtml = "Edit User Details";
        }

        if (applicationStatus == "Onboarding Cancelled") {
            ibtnHRSave.Enabled = false;
            ibtnRejectCandidate.Text = "Revoke";
            ShowMessage(divAlert, "Onboarding Cancelled: ", "Onboarding Cancelled by HR");
        }

        if (applicationStatus == "EmployeeId Created")
        {
            //HRPanel.Enabled = false;
            //ITPanel.Enabled = false;
            //AdminPanel.Enabled = false;
            //pnlFileUpload.Enabled = false;
            ibtnHRSave.Enabled = false;
            btnProceedToIT.Enabled = false;
            ibtnRejectCandidate.Enabled = false;
            btnUpload.Enabled = false;
            btnUploadPO.Enabled = false;
            ibtnITSave.Enabled = false;
            ibtnProceedToHR.Enabled = false;
            ibtnSaveFinal.Enabled = false;
            ibtnSubmitFinal.Enabled = false;
            ibtnHold.Enabled = false;
            ShowMessage(divAlert, "Employee ID Created: ", "Onboarding process completed and Employee ID already created for this candidate. Please click on View Employee link incase any modification required.");
        }
        else
        {
            if (ViewState["HRUpdate"] != null && (ViewState["HRUpdate"].ToString() == "True" || ViewState["HRUpdate"].ToString() == "1") && applicationStatus != "Onboarding Cancelled")
            {
                //HRPanel.Enabled = false;
                ibtnHRSave.Enabled = false;
                btnProceedToIT.Enabled = false;
                ibtnRejectCandidate.Enabled = false;
                btnUpload.Enabled = false;
                //ShowMessage(divAlert, "Status: HR Initiation completed", "");

            }
            if (ViewState["ITUpdate"] != null && (ViewState["ITUpdate"].ToString() == "True" || ViewState["ITUpdate"].ToString() == "1"))
            {
                //ITPanel.Enabled = false;
                btnUploadPO.Enabled = false;
                ibtnITSave.Enabled = false;
                ibtnProceedToHR.Enabled = false;
                //ShowMessage(divAlert, "Status: IT action completed", "");
            }
        }
        
    }

    protected void ibtnSaveFinal_Click(object sender, EventArgs e)
    {
    }
    protected void ibtnHold_Click(object sender, EventArgs e)
    {
    }
    
        
    protected void ibtnHRSave_Click(object sender, EventArgs e)
    {
        saveOrUpdateInitialData(0);
    }

    protected void btnProceedToIT_Click(object sender, EventArgs e)
    {
        saveOrUpdateInitialData(1);
    }
    

    private void saveOrUpdateInitialData(int status) {
        try
        {

            string DOB = txtDOB.Text.ToString();
            string DOJ = txtDOJ.Text.ToString();
            if (DOB.Length > 10)
            {
                DOB = DOB.Substring(0, 10);
                if (DOB == "01/01/1900")
                {
                    DOB = "";
                }
            }
            if (DOJ.Length > 10)
            {
                DOJ = DOJ.Substring(0, 10);
                if (DOJ == "01/01/1900")
                {
                    DOJ = "";
                }
            }
            DOB = DOB.Trim();
            DOJ = DOJ.Trim();

            SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            SqlCommand cmdSave;
            connSave.Open();

            if (ViewState["EditId"] != null)
            {
                String EditId = ViewState["EditId"].ToString();
                cmdSave = new SqlCommand("Onboarding_UpdateUserReg_HR", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@Id", EditId);
            }
            else
            {
                cmdSave = new SqlCommand("Onboarding_InsertUserReg_HR", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
            }
            cmdSave.Parameters.AddWithValue("@Title", ddlPrifix.SelectedItem.Value.Trim());
            cmdSave.Parameters.AddWithValue("@Name", txtFirstName.Text.Trim());
            cmdSave.Parameters.AddWithValue("@Department", ddlDepartments.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@Area", ddlSubDepartments.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@DOB", DOB.ToString());
            cmdSave.Parameters.AddWithValue("@PayrollHREmpNo", ddlpayrolHR.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@LocationHREmpNo", ddlLocationHR.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@PayrollArea", ddlPayrollArea.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@Band", ddlBand.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@Designation", ddlDesignation.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@DateOfJoining", DOJ.ToString());
            cmdSave.Parameters.AddWithValue("@CompanySIM", ddlcompanySim.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@NewOrReplacement", ddlNewOrReplacement.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@PayrollNameIfNew", ""); // txtPayrollName.Text.Trim());
            cmdSave.Parameters.AddWithValue("@PayrollNameIfReplacement", ddlPayrollReplacement.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@VendorCode", txtVendor.Text.Trim());
            cmdSave.Parameters.AddWithValue("@ReplacementFor", txtReplacementForName.Text.Trim());
            cmdSave.Parameters.AddWithValue("@ReplacementEmpCode", txtReplacementForEmpId.Text.Trim());
            cmdSave.Parameters.AddWithValue("@Email_Personal", txtEmailP.Text.Trim());
            cmdSave.Parameters.AddWithValue("@Phone_Personal", txtPhoneP.Text.Trim());
            cmdSave.Parameters.AddWithValue("@Shift_Details", ddlShiftDetail.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@Casual_Leave", txtCasualLeave.Text.Trim());
            cmdSave.Parameters.AddWithValue("@Sick_Leave", txtSickLeave.Text.Trim());
            cmdSave.Parameters.AddWithValue("@PermanentAddress", txtAddress.Text.Trim());
            cmdSave.Parameters.AddWithValue("@AppraiserInAppraisal", "");
            cmdSave.Parameters.AddWithValue("@ReviewerInAppraisal", "");
            cmdSave.Parameters.AddWithValue("@BankName", txtBank.Text.Trim());
            cmdSave.Parameters.AddWithValue("@AccountNumber", txtAccount.Text.Trim());
            cmdSave.Parameters.AddWithValue("@IFSC", txtIFSC.Text.Trim());
            cmdSave.Parameters.AddWithValue("@PANNumber", txtPAN.Text.Trim());
            cmdSave.Parameters.AddWithValue("@PhysicalLocation", ""); // txtPhysicalLocation.Text.Trim());
            cmdSave.Parameters.AddWithValue("@ESIC", txtESIC.Text.Trim());
            cmdSave.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text.Trim());
            cmdSave.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
            cmdSave.Parameters.AddWithValue("@ComputerRequired", ddlComputerRequired.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@OfficialEmailRequired", ddlOfficialEmailRequired.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@CompanynumberRequired", ""); // ddlCompanyNumberRequired.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@AlternateMobile", ""); // txtAlternateMobile.Text.Trim());
            cmdSave.Parameters.AddWithValue("@HRPhas1UpdatedBy", Session["UserID"].ToString());
            cmdSave.Parameters.AddWithValue("@SystemType", ddlStype.SelectedItem.Value.ToString());
            cmdSave.Parameters.AddWithValue("@Division", txtDivision.Text.Trim());
            cmdSave.Parameters.AddWithValue("@HRUpdate", status);
            cmdSave.Parameters.AddWithValue("@HRNotes", txtHRNotes.Text.Trim());


            cmdSave.ExecuteNonQuery();
            cmdSave.Parameters.Clear();
            connSave.Close();

            if (ViewState["EditId"] != null)
            {
                ShowMessage(divSuccess, "Success: ", "User details updated successfully");
            }
            else
            {
                txtUID.Text = "";
                ShowMessage(divSuccess, "Success: ", "New user details added successfully");
                Response.Redirect("ViewOnBoardingDashboard.aspx");
            }


        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Failed: ", "Error while saving details. Please correct the data. " + ex.Message.ToString());
        }
    }
    protected void ibtnITSave_Click(object sender, EventArgs e) { 
    
    }
    
    protected void ibtnProceedToHR_Click(object sender, EventArgs e)
    {
        try
        {

            SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            SqlCommand cmdSave;
            connSave.Open();

            if (ViewState["EditId"] != null)
            {
                String EditId = ViewState["EditId"].ToString();
                cmdSave = new SqlCommand("Onboarding_UpdateUserReg_IT", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@Id", EditId);
                cmdSave.Parameters.AddWithValue("@SystemType", ddlStype.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@AssetName", ddlAssetName.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@MouseAllotted", isMouse.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@ExtMemoryAllotted", isExtD.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@DataCardAllotted", isDatacard.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@Gadgets", ddlGadgets.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@MailIdCommonOrIndividual", ""); // ddlcommonOrIndividual.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@SystemPurchase", txtSystemPurchase.Text.Trim());
                cmdSave.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmdSave.Parameters.AddWithValue("@MobileAllotted", isMobile.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@HeadSetAllotted", isHeadSet.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@ITPhas2UpdatedBy", Session["UserID"].ToString());
                cmdSave.Parameters.AddWithValue("@Phone", ""); // txtPhone.Text.Trim());
                cmdSave.Parameters.AddWithValue("@CapexNumber", txtCapexNumber.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PoNumber", txtPONumber.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                cmdSave.Parameters.AddWithValue("@ITNotes", txtITNotes.Text.Trim());


                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                connSave.Close();

                ShowMessage(divSuccess, "Success: ", "IT details updated successfully");
            }
            else {
                ShowMessage(diverror, "Failed: ", "User details not found");

            }

        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Failed: ", "Error while saving details. Please correct the data. " + ex.Message.ToString());
        }
    }

    protected void ibtnRejectCandidate_Click(object sender, EventArgs e)
    {
        
        try
        {
            SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            SqlCommand cmdSave;
            connSave.Open();
            if (ViewState["EditId"] != null)
            {
                String EditId = ViewState["EditId"].ToString();
                cmdSave = new SqlCommand("Onboarding_UpdateUserReg_Reject", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@Id", EditId);
                if (ibtnRejectCandidate.Text == "Revoke")
                {
                    cmdSave.Parameters.AddWithValue("@Action", "Revoke");
                }
                else {
                    cmdSave.Parameters.AddWithValue("@Action", "Reject");
                }
                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                connSave.Close();
                ShowMessage(divSuccess, "Success: ", "User details updated successfully. ");
                Response.Redirect("ViewOnBoardingDashboard.aspx");
            }
            else
            {
                ShowMessage(diverror, "Failed: ", "User details not found");

            }
        }

        catch (Exception ex)
        {
            ShowMessage(diverror, "Failed: ", "Error while saving details. Please correct the data. " + ex.Message.ToString());
        }
    }

    protected void ibtnSubmitFinal_Click(object sender, EventArgs e)
    {
        try
        {
            string DOB = txtDOB.Text.ToString();
            if (DOB.Length > 10)
            {
                DOB = DOB.Substring(0, 10);
            }
            DOB = DOB.Trim();

            SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            SqlCommand cmdSave;
            connSave.Open();
            if (ViewState["EditId"] != null)
            {
                String EditId = ViewState["EditId"].ToString();
                cmdSave = new SqlCommand("Onboarding_UpdateUserReg_Final", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@Id", EditId);
                cmdSave.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text.ToString());
                cmdSave.Parameters.AddWithValue("@FunctionalReportingEmpNo", ddlManager.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@AdministrativeReportingEmpno", ddlAdminManager.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@VendorCode", txtVendor.Text.Trim());
                cmdSave.Parameters.AddWithValue("@DOB", DOB.ToString());
                cmdSave.Parameters.AddWithValue("@PayrollHREmpNo", ddlpayrolHR.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@LocationHREmpNo", ddlLocationHR.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@UID", txtUID.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Password", ""); // txtPassword.Text.ToString());
                cmdSave.Parameters.AddWithValue("@Role", ddlRole.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Shift_Details", ddlShiftDetail.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Casual_Leave", txtCasualLeave.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Sick_Leave", txtSickLeave.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PermanentAddress", txtAddress.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AppraiserInAppraisal", "");
                cmdSave.Parameters.AddWithValue("@ReviewerInAppraisal", "");
                cmdSave.Parameters.AddWithValue("@BankName", txtBank.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AccountNumber", txtAccount.Text.Trim());
                cmdSave.Parameters.AddWithValue("@IFSC", txtIFSC.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PANNumber", txtPAN.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PFNumber", txtPFNumber.Text.Trim());
                cmdSave.Parameters.AddWithValue("@HRNotes", txtHRNotesFinal.Text.Trim());


                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                connSave.Close();
                HRPanel.Enabled = false;
                ITPanel.Enabled = false;
                AdminPanel.Enabled = false;
                pnlFileUpload.Enabled = false;
                ShowMessage(divSuccess, "Employee ID Created: ", "Onboarding process completed and Employee ID created for this candidate. Please click on View Employee link incase any modification required.");
            }
            else
            {
                ShowMessage(diverror, "Failed: ", "User details not found");

            }

        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper().Contains("VIOLATION OF PRIMARY KEY"))
            {
                ShowMessage(diverror, "Failed: ", "Please enter unique UserID.");
            }
            else
            {
                ShowMessage(diverror, "Failed: ", "Error while saving details. Please correct the data. " + ex.Message.ToString());
            }
        }
    }

    protected void ibtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("NewUserOnBoarding.aspx");
    }

    protected void LogRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void LogOff_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Session["Password"] = null;
        Response.Redirect("Default.aspx");
    }

    private void BindDepartmentsandLocations()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("GetLocations", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "----Select----";

        if (dr1.HasRows)
        {
            ddlLocation.DataSource = dr1;
            ddlLocation.DataTextField = "LocationDetails";
            ddlLocation.DataValueField = "LocationDetails";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();

        cmd1 = new SqlCommand("GetDepartments", con);
        cmd1.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr.HasRows)
        {
            ddlDepartments.DataSource = dr;
            ddlDepartments.DataTextField = "DepartmentName";
            ddlDepartments.DataValueField = "DepartmentName";
            ddlDepartments.DataBind();
            ddlDepartments.Items.Insert(0, Listitem0);
        }
        cmd1.Parameters.Clear();
        cmd1.Dispose();
        con.Close();

        cmd2 = new SqlCommand("GetAreas", con);
        cmd2.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr2.HasRows)
        {
            ddlSubDepartments.DataSource = dr2;
            ddlSubDepartments.DataTextField = "AreaName";
            ddlSubDepartments.DataValueField = "AreaName";
            ddlSubDepartments.DataBind();
            ddlSubDepartments.Items.Insert(0, Listitem0);
        }
        cmd2.Parameters.Clear();
        cmd2.Dispose();
        con.Close();
        
        //----------------------------------------------

        cmd3 = new SqlCommand("ListBand", con);
        cmd3.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr3.HasRows)
        {
            ddlBand.DataSource = dr3;
            ddlBand.DataTextField = "Name";
            ddlBand.DataValueField = "Name";
            ddlBand.DataBind();
            ddlBand.Items.Insert(0, Listitem0);
        }
        cmd3.Parameters.Clear();
        cmd3.Dispose();
        con.Close();

        //--------------------------------------------------------

        cmd4 = new SqlCommand("ListDesignation", con);
        cmd4.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr4 = cmd4.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr4.HasRows)
        {
            ddlDesignation.DataSource = dr4;
            ddlDesignation.DataTextField = "Name";
            ddlDesignation.DataValueField = "Name";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, Listitem0);
        }
        cmd4.Parameters.Clear();
        cmd4.Dispose();
        con.Close();

        //--------------------------------------------------------

        cmd5 = new SqlCommand("ListPayrollArea", con);
        cmd5.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr5 = cmd5.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr5.HasRows)
        {
            ddlPayrollArea.DataSource = dr5;
            ddlPayrollArea.DataTextField = "Name";
            ddlPayrollArea.DataValueField = "Name";
            ddlPayrollArea.DataBind();
            ddlPayrollArea.Items.Insert(0, Listitem0);                     
        }
        cmd5.Parameters.Clear();
        cmd5.Dispose();
        con.Close();


        cmd6 = new SqlCommand("ListPayrollArea", con);
        cmd6.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr6 = cmd6.ExecuteReader(CommandBehavior.CloseConnection);

        if (dr6.HasRows)
        {           
            ddlPayrollReplacement.DataSource = dr6;
            ddlPayrollReplacement.DataTextField = "Name";
            ddlPayrollReplacement.DataValueField = "Name";
            ddlPayrollReplacement.DataBind();
            ddlPayrollReplacement.Items.Insert(0, Listitem0);

        }
        cmd6.Parameters.Clear();
        cmd6.Dispose();
        con.Close();

        //--------------------------------------------------------

    }

    private void BindAsset()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("IT_GetAssetNames", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "----Select----";

        if (dr1.HasRows)
        {
            ddlAssetName.DataSource = dr1;
            ddlAssetName.DataTextField = "AssetName";
            ddlAssetName.DataValueField = "AssetName";
            ddlAssetName.DataBind();
            ddlAssetName.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    private void BindUserDropdownList(DropDownList ddlList)
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("INT_UserListForDropdown", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "---";
        Listitem0.Text = "----Select----";

        if (dr1.HasRows)
        {
            ddlList.DataSource = dr1;
            ddlList.DataTextField = "Name";
            ddlList.DataValueField = "EmpCode";
            ddlList.DataBind();
            ddlList.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
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

    private void BinduserData(string id)
    {
        try
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Onboarding_GetUserRegOnboarding_ById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtFirstName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtMiddleName.Text = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                            txtLastName.Text = ds.Tables[0].Rows[0]["LastName"].ToString();

                            txtEmpCode.Text = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                            txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                            //txtPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                            txtUID.Text = ds.Tables[0].Rows[0]["UID"].ToString();
                            txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                            //txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                            if (txtUID.Text.Length > 0)
                            {
                                txtUID.Enabled = false;
                            }
                            else {
                                txtUID.Text = txtFirstName.Text + txtLastName.Text;

                            }


                            txtPhoneP.Text = ds.Tables[0].Rows[0]["Phone_Personal"].ToString();
                            txtEmailP.Text = ds.Tables[0].Rows[0]["Email_Personal"].ToString();
                            txtDOJ.Text = ds.Tables[0].Rows[0]["DateOfJoining"].ToString();
                            txtVendor.Text = ds.Tables[0].Rows[0]["VendorCode"].ToString();
                            txtPayrollName.Text = ""; // ds.Tables[0].Rows[0]["PayrollNameIfNew"].ToString();
                            txtReplacementForName.Text = ds.Tables[0].Rows[0]["ReplacementFor"].ToString();
                            txtReplacementForEmpId.Text = ds.Tables[0].Rows[0]["ReplacementEmpCode"].ToString();
                            //txtShiftDetails.Text = ds.Tables[0].Rows[0]["Shift_Details"].ToString();
                            txtCasualLeave.Text = ds.Tables[0].Rows[0]["Casual_Leave"].ToString();
                            txtSickLeave.Text = ds.Tables[0].Rows[0]["Sick_Leave"].ToString();
                            txtAddress.Text = ds.Tables[0].Rows[0]["PermanentAddress"].ToString();
                            txtAppraiser.Text = ds.Tables[0].Rows[0]["AppraiserInAppraisal"].ToString();
                            txtReviewer.Text = ds.Tables[0].Rows[0]["ReviewerInAppraisal"].ToString();
                            txtBank.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                            txtAccount.Text = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                            txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                            txtPAN.Text = ds.Tables[0].Rows[0]["PANNumber"].ToString();
                            txtPhysicalLocation.Text = ""; // ds.Tables[0].Rows[0]["PhysicalLocation"].ToString();
                            txtESIC.Text = ds.Tables[0].Rows[0]["ESIC"].ToString();
                            txtSystemPurchase.Text = ds.Tables[0].Rows[0]["SystemPurchase"].ToString();
                            txtMobileNumber.Text = ds.Tables[0].Rows[0]["MobileNumber"].ToString();
                            txtAlternateMobile.Text = ""; // ds.Tables[0].Rows[0]["AlternateMobile"].ToString();
                            txtCapexNumber.Text = ds.Tables[0].Rows[0]["CapexNumber"].ToString();
                            txtDivision.Text = ds.Tables[0].Rows[0]["Division"].ToString();
                            txtPONumber.Text = ds.Tables[0].Rows[0]["PONumber"].ToString();
                            txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                            txtPFNumber.Text = ds.Tables[0].Rows[0]["PFNumber"].ToString();
                            txtHRNotes.Text = ds.Tables[0].Rows[0]["HRNotes"].ToString();
                            txtHRNotesFinal.Text = ds.Tables[0].Rows[0]["HRNotes"].ToString();
                            txtITNotes.Text = ds.Tables[0].Rows[0]["ITNotes"].ToString();

                            ViewState["ITUpdate"] = ds.Tables[0].Rows[0]["ITUpdate"].ToString();
                            ViewState["HRUpdate"] = ds.Tables[0].Rows[0]["HRUpdate"].ToString();

                            ViewState["ApplicationStatus"] = ds.Tables[0].Rows[0]["Application_Status"].ToString();
                            btnProceedToIT.Enabled = true;
                            if (ds.Tables[0].Rows[0]["MouseAllotted"].ToString() == "True" || ds.Tables[0].Rows[0]["MouseAllotted"].ToString() == "Yes")
                            {
                                isMouse.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["ExtMemoryAllotted"].ToString() == "True" || ds.Tables[0].Rows[0]["ExtMemoryAllotted"].ToString() == "Yes")
                            {
                                isExtD.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["DataCardAllotted"].ToString() == "True" || ds.Tables[0].Rows[0]["DataCardAllotted"].ToString() == "Yes")
                            {
                                isDatacard.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["MobileAllotted"].ToString() == "True" || ds.Tables[0].Rows[0]["MobileAllotted"].ToString() == "Yes")
                            {
                                isMobile.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["HeadSetAllotted"].ToString() == "True" || ds.Tables[0].Rows[0]["HeadSetAllotted"].ToString() == "Yes")
                            {
                                isHeadSet.Checked = true;
                            }
                            try
                            {
                                ddlPrifix.SelectedValue = ds.Tables[0].Rows[0]["Title"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlStype.SelectedValue = ds.Tables[0].Rows[0]["SystemType"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlShiftDetail.SelectedValue = ds.Tables[0].Rows[0]["Shift_Details"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlPayrollReplacement.SelectedValue = ds.Tables[0].Rows[0]["PayrollNameIfReplacement"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlAssetName.SelectedValue = ds.Tables[0].Rows[0]["AssetName"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlDepartments.SelectedValue = ds.Tables[0].Rows[0]["Department"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["Location"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlSubDepartments.SelectedValue = ds.Tables[0].Rows[0]["Area"].ToString();
                            }
                            catch
                            {

                            }

                            try
                            {
                                ddlManager.SelectedValue = ds.Tables[0].Rows[0]["FunctionalReportingEmpNo"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlcompanySim.SelectedValue = ds.Tables[0].Rows[0]["CompanySIM"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlNewOrReplacement.SelectedValue = ds.Tables[0].Rows[0]["NewOrReplacement"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlGadgets.SelectedValue = ds.Tables[0].Rows[0]["Gadgets"].ToString();
                            }
                            catch
                            {

                            }
                            /*try
                            {
                                ddlcommonOrIndividual.SelectedValue = ds.Tables[0].Rows[0]["MailIdCommonOrIndividual"].ToString();
                            }
                            catch
                            {

                            }*/
                            try
                            {
                                ddlAdminManager.SelectedValue = ds.Tables[0].Rows[0]["AdministrativeReportingEmpno"].ToString();
                            }
                            catch
                            {

                            }

                            try
                            {
                                ddlpayrolHR.SelectedValue = ds.Tables[0].Rows[0]["PayrollHREmpNo"].ToString();
                            }
                            catch
                            {

                            }

                            try
                            {
                                ddlLocationHR.SelectedValue = ds.Tables[0].Rows[0]["LocationHREmpNo"].ToString();
                            }
                            catch
                            {

                            }

                            try
                            {
                                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["Role"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlPayrollArea.SelectedValue = ds.Tables[0].Rows[0]["PayrollArea"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlBand.SelectedValue = ds.Tables[0].Rows[0]["Band"].ToString();
                            }
                            catch
                            {

                            }

                            try
                            {
                                ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["Designation"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlComputerRequired.SelectedValue = ds.Tables[0].Rows[0]["ComputerRequired"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlOfficialEmailRequired.SelectedValue = ds.Tables[0].Rows[0]["OfficialEmailRequired"].ToString();
                            }
                            catch
                            {

                            }
                           /* try
                            {
                                ddlCompanyNumberRequired.SelectedValue = ds.Tables[0].Rows[0]["CompanynumberRequired"].ToString();
                            }
                            catch
                            {

                            }*/


                        }

                    }
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!uplAadhar.HasFile)
        {
            ShowMessage(diverror, "Error: ", "Please upload the file");
            return;
        }

        /*SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        string str = "select (isnull(max(FileID),0)+1) from USER_REG_Files";
        string docid = "";
        SqlCommand cmd2 = new SqlCommand(str, con);
        cmd2.CommandType = CommandType.Text;

        SqlDataReader dr = cmd2.ExecuteReader();
        while (dr.Read())
        {
            docid = dr[0].ToString();
        }
        dr.Close();
        */

        try
        {
            if (uplAadhar.FileName.Contains(".pdf") || uplAadhar.FileName.Contains(".jpeg") || uplAadhar.FileName.Contains(".jpg"))
            {
                if (ViewState["EditId"] != null)
                {
                    UploadFile(ViewState["EditId"].ToString());
                    ShowMessage(divSuccess, "Success: ", "File details updated successfully. ");
                    BindFileList(ViewState["EditId"].ToString());
                }
                else {
                    ShowMessage(diverror, "Failed: ", "User details not found");
                }
                /*SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                SqlCommand cmdSave;
                connSave.Open();
                if (ViewState["EditId"] != null)
                {
                    String EditId = ViewState["EditId"].ToString();
                    cmdSave = new SqlCommand("Onboarding_UpdateFileID", connSave);
                    cmdSave.CommandType = CommandType.StoredProcedure;
                    cmdSave.Parameters.AddWithValue("@Id", EditId);
                    cmdSave.Parameters.AddWithValue("@FileID", docid);
                    cmdSave.ExecuteNonQuery();
                    cmdSave.Parameters.Clear();
                    connSave.Close();
                    ShowMessage(divSuccess, "Success: ", "File details updated successfully. ");
                }
                else
                {
                    ShowMessage(diverror, "Failed: ", "User details not found");

                }*/
            }
            else
            {
                ShowMessage(diverror, "Error: ", "Please upload only PDF/JPEG format file");
            }

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Microsoft.ACE.OLEDB.12.0"))
            {
                ShowMessage(diverror, "Error: ", "Please upload Office Excel 97 (.xls) Format");
            }
            else
            {
                ShowMessage(diverror, "Error: ", ex.Message);
            }
        }
        finally
        {

            /*if (con != null)
            {
                con.Close();
            }*/
        }
    }

    protected bool UploadFile(string editId)
    {
        try
        {
            string[] filetype;
        filetype = uplAadhar.FileName.Split('.');
        int fileLen = Convert.ToInt32(uplAadhar.PostedFile.ContentLength.ToString());

        if (fileLen <= 0)
        {
            return false;
        }

        if (fileLen > 1048576)
        {
            string Msg = "Max. allowed file size is 1 MB. You may convert the file to PDF or JPEG and upload it again";
            ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Max. allowed file size is 1 MB. You may convert the file to PDF or JPEG and upload it again');", true);
            return false;
        }

        byte[] fileBinaryData = new byte[fileLen];

        uplAadhar.PostedFile.InputStream.Read(fileBinaryData, 0, fileLen);
        string FileName = uplAadhar.FileName.ToString();
        FileName = FileName.Replace("_", "");
        string Filecontenttype = uplAadhar.PostedFile.ContentType.ToString();
        string Filesize = uplAadhar.PostedFile.ContentLength.ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        cmd = new SqlCommand("[Onboarding_Insert_Attach_Docs]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
            
         cmd.Parameters.AddWithValue("@EditId", editId);
         cmd.Parameters.AddWithValue("@Filename1", FileName);
        cmd.Parameters.AddWithValue("@Filetype1", Filecontenttype);
        cmd.Parameters.AddWithValue("@Filedata1", fileBinaryData);
        cmd.Parameters.AddWithValue("@Filesize1", Filesize);

        cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();

        return true;
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error: ", ex.Message);
            return false;
        }
    }


    protected void ImagesGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "Viewfile")
            {
                int i = Convert.ToInt32(e.CommandArgument);

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                cmd = new SqlCommand("Onboarding_GetFullFilesByFileID", con);
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

            ShowMessage(diverror, "Error: ", ex.Message);
        }
    }


    private void BindFileList(string Id)
    {
        try
        {

            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Onboarding_GetFilesByID", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                ImagesGrid.DataSource = dt;
                ImagesGrid.DataBind();
                ImagesGrid.Visible = true;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Error While loading file data. Please try later.");
        }
    }


    protected void btnUploadPO_Click(object sender, EventArgs e)
    {
        if (!uplPO.HasFile)
        {
            ShowMessage(diverror, "Error: ", "Please upload the file");
            return;
        }

        /*SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        string str = "select (isnull(max(FileID),0)+1) from USER_REG_PO_Files";
        string docid = "";
        SqlCommand cmd2 = new SqlCommand(str, con);
        cmd2.CommandType = CommandType.Text;

        SqlDataReader dr = cmd2.ExecuteReader();
        while (dr.Read())
        {
            docid = dr[0].ToString();
        }
        dr.Close();
        */

        try
        {
            if (uplPO.FileName.Contains(".pdf") || uplPO.FileName.Contains(".jpeg") || uplPO.FileName.Contains(".jpg"))
            {
                if (ViewState["EditId"] != null)
                {
                    UploadPOFile(ViewState["EditId"].ToString());
                    ShowMessage(divSuccess, "Success: ", "File details updated successfully. ");
                    BindPOFileList(ViewState["EditId"].ToString());
                }
                else
                {
                    ShowMessage(diverror, "Failed: ", "User details not found");
                }
            }
            else
            {
                ShowMessage(diverror, "Error: ", "Please upload only PDF/JPEG format file");
            }

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Microsoft.ACE.OLEDB.12.0"))
            {
                ShowMessage(diverror, "Error: ", "Please upload Office Excel 97 (.xls) Format");
            }
            else
            {
                ShowMessage(diverror, "Error: ", ex.Message);
            }
        }
        finally
        {

            /*if (con != null)
            {
                con.Close();
            }*/
        }
    }

    protected bool UploadPOFile(string editId)
    {
        try
        {
            string[] filetype;
            filetype = uplPO.FileName.Split('.');
            int fileLen = Convert.ToInt32(uplPO.PostedFile.ContentLength.ToString());

            if (fileLen <= 0)
            {
                return false;
            }

            if (fileLen > 1048576)
            {
                string Msg = "Max. allowed file size is 1 MB. You may convert the file to PDF or JPEG and upload it again";
                ScriptManager.RegisterStartupScript(this, GetType(), "MyScript2", "alert('Max. allowed file size is 1 MB. You may convert the file to PDF or JPEG and upload it again');", true);
                return false;
            }

            byte[] fileBinaryData = new byte[fileLen];

            uplPO.PostedFile.InputStream.Read(fileBinaryData, 0, fileLen);
            string FileName = uplPO.FileName.ToString();
            FileName = FileName.Replace("_", "");
            string Filecontenttype = uplPO.PostedFile.ContentType.ToString();
            string Filesize = uplPO.PostedFile.ContentLength.ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            cmd = new SqlCommand("[Onboarding_Insert_Attach_PO_Docs]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@EditId", editId);
            cmd.Parameters.AddWithValue("@Filename1", FileName);
            cmd.Parameters.AddWithValue("@Filetype1", Filecontenttype);
            cmd.Parameters.AddWithValue("@Filedata1", fileBinaryData);
            cmd.Parameters.AddWithValue("@Filesize1", Filesize);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            return true;
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error: ", ex.Message);
            return false;
        }
    }


    protected void ImagesGridPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "ViewPOfile")
            {
                int i = Convert.ToInt32(e.CommandArgument);

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                cmd = new SqlCommand("Onboarding_GetFullPOFilesByFileID", con);
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

            ShowMessage(diverror, "Error: ", ex.Message);
        }
    }


    private void BindPOFileList(string Id)
    {
        try
        {

            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Onboarding_GetPOFilesByID", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                gvPO.DataSource = dt;
                gvPO.DataBind();
                gvPO.Visible = true;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Error While loading file data. Please try later.");
        }
    }

}
