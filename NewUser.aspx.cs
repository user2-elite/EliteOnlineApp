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


public partial class NewUser : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    SqlCommand cmd3 = null;
    SqlCommand cmd4 = null;
    SqlCommand cmd5 = null;
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

            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "5")
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
            if (Session["EditUID"] != null)
            {
                ViewState["EditUID"] = Session["EditUID"].ToString();
                Session["EditUID"] = null;
                BinduserData(ViewState["EditUID"].ToString());

                if (ViewState["EditId"] != null) {
                    BindFileList(ViewState["EditId"].ToString());
                }
                ibtnBack.Visible = true;
                ibtnSubmit.Text = "Update Details";
                divPassword.Visible = true;
                divStatus.Visible = true;
            }
            else
            {
                ibtnBack.Visible = false;
                divPassword.Visible = false;
                divStatus.Visible = false;
            }
        }
        if (ViewState["EditUID"] != null)
        {
            h3Title.InnerHtml = "Edit Employee Details";
        }
    }

    protected void ibtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["EditUID"] == null)
            {
                ShowMessage(diverror, "Failed: ", "Please use onboarding user screen for creating new user.");
            }
            else
            {

                string DOB = txtDOB.Text.ToString();
                string DOJ = txtDOJ.Text.ToString();
                if (DOB.Length > 10)
                {
                    DOB = DOB.Substring(0, 10);
                }
                if (DOJ.Length > 10)
                {
                    DOJ = DOJ.Substring(0, 10);
                }
                DOB = DOB.Trim();
                DOJ = DOJ.Trim();


                SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                SqlCommand cmdSave;
                connSave.Open();

                if (ViewState["EditUID"] != null)
                {
                    cmdSave = new SqlCommand("UpdateUser", connSave);
                    cmdSave.CommandType = CommandType.StoredProcedure;
                    if (Session["UserID"].ToString() == "pinku")
                    {
                        cmdSave.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    }
                    else {
                        cmdSave.Parameters.AddWithValue("@Password", ViewState["Password"].ToString());
                    }
                    
                    cmdSave.Parameters.AddWithValue("@Status", ddlStatus.SelectedItem.Value.ToString());
                }
                else
                {
                    cmdSave = new SqlCommand("CreateNewUser", connSave);
                    ShowMessage(diverror, "Failed: ", "Please enter unique UserID.");
                    cmdSave.CommandType = CommandType.StoredProcedure;
                }

                cmdSave.Parameters.AddWithValue("@UID", txtUID.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Title", ddlPrifix.SelectedItem.Value.Trim());
                cmdSave.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                cmdSave.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text.ToString());
                cmdSave.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                cmdSave.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmdSave.Parameters.AddWithValue("@SystemType", ddlStype.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@AssetName", ddlAssetName.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@MouseAllotted", isMouse.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@ExtMemoryAllotted", isExtD.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@DataCardAllotted", isDatacard.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@Department", ddlDepartments.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Area", ddlArea.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@DOB", DOB.ToString());

                cmdSave.Parameters.AddWithValue("@ManagerID", ddlManager.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@AdminId", ddlAdminManager.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@PayRollHR", ddlpayrolHR.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@LocationHR", ddlLocationHR.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@PayrollArea", ddlPayrollArea.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Role", ddlRole.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Band", ddlBand.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Designation", ddlDesignation.SelectedItem.Value.ToString());

                cmdSave.Parameters.AddWithValue("@DateOfJoining", DOJ.ToString());
                cmdSave.Parameters.AddWithValue("@CompanySIM", ddlcompanySim.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@NewOrReplacement", ddlNewOrReplacement.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@PayrollNameIfNew", txtPayrollName.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PayrollNameIfReplacement", txtPayrollReplacement.Text.Trim());
                cmdSave.Parameters.AddWithValue("@VendorCode", txtVendor.Text.Trim());
                cmdSave.Parameters.AddWithValue("@ReplacementFor", txtReplacementForName.Text.Trim());
                cmdSave.Parameters.AddWithValue("@ReplacementEmpCode", txtReplacementForEmpId.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Email_Personal", txtEmailP.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Phone_Personal", txtPhoneP.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Shift_Details", txtShiftDetails.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Casual_Leave", txtCasualLeave.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Sick_Leave", txtSickLeave.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PermanentAddress", txtAddress.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AppraiserInAppraisal", txtAppraiser.Text.Trim());
                cmdSave.Parameters.AddWithValue("@ReviewerInAppraisal", txtReviewer.Text.Trim());
                cmdSave.Parameters.AddWithValue("@BankName", txtBank.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AccountNumber", txtAccount.Text.Trim());
                cmdSave.Parameters.AddWithValue("@IFSC", txtIFSC.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PANNumber", txtPAN.Text.Trim());
                cmdSave.Parameters.AddWithValue("@PhysicalLocation", txtPhysicalLocation.Text.Trim());
                cmdSave.Parameters.AddWithValue("@ESIC", txtESIC.Text.Trim());
                cmdSave.Parameters.AddWithValue("@Gadgets", ddlGadgets.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@MailIdCommonOrIndividual", ddlcommonOrIndividual.SelectedItem.Value.ToString());

                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                connSave.Close();

                if (ViewState["EditUID"] != null)
                {
                    ShowMessage(divSuccess, "Success: ", "User details updated successfully");
                }
                else
                {
                    txtUID.Text = "";
                    ShowMessage(divSuccess, "Success: ", "New user added successfully");
                }
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
                ShowMessage(diverror, "Failed: ", "Error while saving details. Please correct the data. " + ex.ToString());
            }
        }
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
            SqlCommand cmd = new SqlCommand("Onboarding_GetFilesByIDForHiredEmployee", conn);
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
        catch(Exception ex)
        {
            ShowMessage(diverror, "Error: ", "Error While loading file data. Please try later."+ ex.Message.ToString());
        }
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
            ddlArea.DataSource = dr2;
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "AreaName";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, Listitem0);
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

    private void BinduserData(string UID)
    {

        try
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_ViewUserBYID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UID", UID);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtFullName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtEmpCode.Text = ds.Tables[0].Rows[0]["EmpCode"].ToString();
                            txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                            txtPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                            txtUID.Text = ds.Tables[0].Rows[0]["UID"].ToString();
                            txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                            if (Session["UserID"].ToString() == "pinku")
                            {
                                txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                                txtPassword.Enabled = true;
                                divPassword.Visible = true;
                            }
                            else {
                                txtPassword.Text = "------------";
                                txtPassword.Enabled = false;
                                divPassword.Visible = false;
                            }
                            txtUID.Enabled = false;
                            ViewState["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();

                            txtPhoneP.Text = ds.Tables[0].Rows[0]["Phone_Personal"].ToString();
                            txtEmailP.Text = ds.Tables[0].Rows[0]["Email_Personal"].ToString();
                            txtDOJ.Text = ds.Tables[0].Rows[0]["DateOfJoining"].ToString();
                            txtVendor.Text = ds.Tables[0].Rows[0]["VendorCode"].ToString();
                            txtPayrollName.Text = ds.Tables[0].Rows[0]["PayrollNameIfNew"].ToString();
                            txtPayrollReplacement.Text = ds.Tables[0].Rows[0]["PayrollNameIfReplacement"].ToString();
                            txtReplacementForName.Text = ds.Tables[0].Rows[0]["ReplacementFor"].ToString();
                            txtReplacementForEmpId.Text = ds.Tables[0].Rows[0]["ReplacementEmpCode"].ToString();
                            txtShiftDetails.Text = ds.Tables[0].Rows[0]["Shift_Details"].ToString();
                            txtCasualLeave.Text = ds.Tables[0].Rows[0]["Casual_Leave"].ToString();
                            txtSickLeave.Text = ds.Tables[0].Rows[0]["Sick_Leave"].ToString();
                            txtAddress.Text = ds.Tables[0].Rows[0]["PermanentAddress"].ToString();
                            txtAppraiser.Text = ds.Tables[0].Rows[0]["AppraiserInAppraisal"].ToString();
                            txtReviewer.Text = ds.Tables[0].Rows[0]["ReviewerInAppraisal"].ToString();
                            txtBank.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                            txtAccount.Text = ds.Tables[0].Rows[0]["AccountNumber"].ToString();
                            txtIFSC.Text = ds.Tables[0].Rows[0]["IFSC"].ToString();
                            txtPAN.Text = ds.Tables[0].Rows[0]["PANNumber"].ToString();
                            txtPhysicalLocation.Text = ds.Tables[0].Rows[0]["PhysicalLocation"].ToString();
                            txtESIC.Text = ds.Tables[0].Rows[0]["ESIC"].ToString();
                            ViewState["EditId"] = ds.Tables[0].Rows[0]["Id"].ToString();

                            if (ds.Tables[0].Rows[0]["MouseAllotted"].ToString() == "Yes")
                            {
                                isMouse.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["ExtMemoryAllotted"].ToString() == "Yes")
                            {
                                isExtD.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["DataCardAllotted"].ToString() == "Yes")
                            {
                                isDatacard.Checked = true;
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
                                ddlArea.SelectedValue = ds.Tables[0].Rows[0]["Area"].ToString();
                            }
                            catch
                            {

                            }
                            try
                            {
                                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
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
                            try
                            {
                                ddlcommonOrIndividual.SelectedValue = ds.Tables[0].Rows[0]["MailIdCommonOrIndividual"].ToString();
                            }
                            catch
                            {

                            }

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
}
