using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using KMModel;

public partial class AddBestPractice : System.Web.UI.Page
{
    KMDBOperation objKMDB = new KMDBOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["KBUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        clearMessages();

        if (Request.QueryString["Edit"] != null)
        {
            if (!Page.IsPostBack)
            {
                ListData();
            }
        }
        else
        {
            AddData();
        }
    }

    private void UpdateData()
    {
        pnlList.Visible = false;
        pnlAddNew.Visible = true;

        lnkViewDetails.Visible = true;
        btnCancel.Visible = true;
        btnSubmit.Text = "Update Data";
        FillDropdowns();
    }

    private void AddData()
    {
        pnlList.Visible = false;
        pnlAddNew.Visible = true;

        lnkViewDetails.Visible = true;
        btnCancel.Visible = false;
        btnSubmit.Text = "Save Data";
        if (!Page.IsPostBack)
        {
            FillDropdowns();
        }
    }

    private void ListData()
    {
        KMEntities objEntities = new KMEntities();
        pnlList.Visible = true;
        pnlAddNew.Visible = false;
        List<CRM_KB_GetAll_BestPractice_Result> objListAll = objEntities.CRM_KB_GetAll_BestPractice_Result().ToList();
        if (objListAll.Count > 0)
        {
            dgKB.DataSource = objListAll;
            dgKB.DataBind();
            dgKB.Visible = true;
        }
        else
        {
            dgKB.Visible = false;
            ShowMessage(divAlert, "Note", "NO Best Practice data found!");
        }
    }

    private void FillDropdowns()
    {
        KMMastrData objMastData = new KMMastrData();
        //objMastData.BindProductCategory(ddProdCategory);
        objMastData.BindUnit(ddCompanyUnit);
        //objMastData.BindArea(ddArea);
        //ddArea.Items[0].Text = "--Select Area--";
        //ddProdCategory.Items[0].Text = "--Select Product Item--";
        ddCompanyUnit.Items[0].Text = "--Select Unit--";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        KMEntities objEntities = new KMEntities();
        DateTime dt1 = new DateTime();
        try
        {
            dt1 = System.Convert.ToDateTime(txtPreparedDate.Text.ToString());
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Date Format Error!");
            return;
        }       
        try
        {
            if (ViewState["SLNO"] != null)
            {

                objKMDB.UpdateBestPractice(System.Convert.ToInt32(ViewState["SLNO"].ToString()), txtArea.Text.ToString(), txtActivity.Text.ToString(), txtProdCategory.Text.ToString(),
                    ddCompanyUnit.SelectedItem.Text.ToString(), txtSearchTags.Text.ToString(), txtMonitoringSystem.Text.ToString()
                    , txtBestPracticeSummary.Text.ToString(), txtSOPReferenceAndVersionNo.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString());

                /*objEntities.CRM_KB_Update_BestPractice(System.Convert.ToInt32(ViewState["SLNO"].ToString()), txtArea.Text.ToString(), txtActivity.Text.ToString(), txtProdCategory.Text.ToString(),
                    ddCompanyUnit.SelectedItem.Text.ToString(), txtSearchTags.Text.ToString(), txtMonitoringSystem.Text.ToString()
                    , txtBestPracticeSummary.Text.ToString(), txtSOPReferenceAndVersionNo.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString());
                 * */
                FillCache();
                ShowMessage(divSuccess, "Success", "ATR Updated successfully!");
            }
            else
            {
                objKMDB.CreateBestPractice(txtArea.Text.ToString(), txtActivity.Text.ToString(), txtProdCategory.Text.ToString(),
                    ddCompanyUnit.SelectedItem.Text.ToString(), txtSearchTags.Text.ToString(), txtMonitoringSystem.Text.ToString()
                    , txtBestPracticeSummary.Text.ToString(), txtSOPReferenceAndVersionNo.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString(), Session["KBUserID"].ToString());
                /*
                objEntities.CRM_KB_Insert_BestPractice(txtArea.Text.ToString(), txtActivity.Text.ToString(), txtProdCategory.Text.ToString(),
                    ddCompanyUnit.SelectedItem.Text.ToString(), txtSearchTags.Text.ToString(), txtMonitoringSystem.Text.ToString()
                    , txtBestPracticeSummary.Text.ToString(), txtSOPReferenceAndVersionNo.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString(), Session["KBUserID"].ToString());
                */
                ShowMessage(divSuccess, "Success", "Best Practice added successfully!");
                FillCache();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while updating data!: " + ex.Message.ToString());
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ListData();
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
        divListSuccess.Visible = false;
        divListError.Visible = false;
    }

    protected void dgKB_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        KMEntities objEntities = new KMEntities();
        if (e.CommandName == "cmDelete")
        {
            try
            {
                objEntities.CRM_KB_Delete_BestPractice_BYID(System.Convert.ToInt32(e.CommandArgument.ToString()));
                ListData();
                ShowMessage(divListSuccess, "Success", "Best Practice data deleted successfully!");
            }
            catch
            {
                ShowMessage(divListError, "Error", "Error while deleting Best Practice data!");
            }
        }
        else if (e.CommandName == "cmEdit")
        {
            UpdateData();
            List<CRM_KB_Get_BestPractice_BYID_Result> objList = objEntities.CRM_KB_Get_BestPractice_BYID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
            if (objList.Count > 0)
            {
                ViewState["SLNO"] = e.CommandArgument.ToString();
                try
                {
                    ddCompanyUnit.SelectedValue = objList[0].UnitName.ToString();
                }
                catch { }
                //try
                //{
                //    ddProdCategory.SelectedValue = objList[0].Product.ToString();
                //}
                //catch { }
                txtProdCategory.Text = objList[0].Product.ToString();
                txtApprovedBY.Text = objList[0].ApprovedBY.ToString();
                txtBenefitAchieved.Text = objList[0].BenefitAchieved.ToString();
                txtPreparedBy.Text = objList[0].PreparedBy.ToString();
                txtPreparedDate.Text = objList[0].PreparedDate.ToString();
                txtSearchTags.Text = objList[0].SearchTag.ToString();
                //try
                //{
                //    ddArea.SelectedValue = objList[0].Area.ToString();
                //}
                //catch { }
                txtArea.Text = objList[0].Area.ToString();
                txtActivity.Text = objList[0].Activity.ToString();
                txtBestPracticeSummary.Text = objList[0].BestPracticeSummary.ToString();
                txtMonitoringSystem.Text = objList[0].MonitoringSystem.ToString();
                txtSOPReferenceAndVersionNo.Text = objList[0].SOPReferenceAndVersionNo.ToString();                
            }
        }
    }

    private void FillCache()
    {
        if (Cache["KeyWordsBP"] != null)
        {
            Cache.Remove("KeyWordsBP");
        }
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "CRM_KB_GetBestPracticeKeyWords";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    Cache.Insert("KeyWordsBP", dt, sqlDep);
                }
                dt.Dispose();
                conn.Close();
            }
        }
    }
}