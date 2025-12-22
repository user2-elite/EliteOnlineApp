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

public partial class AddATR : System.Web.UI.Page
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
        List<CRM_KB_GetAll_ATR_Result> objListAll = objEntities.CRM_KB_GetAll_ATR_Result().ToList();
        if (objListAll.Count > 0)
        {
            dgKB.DataSource = objListAll;
            dgKB.DataBind();
            dgKB.Visible = true;
        }
        else
        {
            dgKB.Visible = false;
            ShowMessage(divAlert, "Note", "NO ATR data found!");
        }
    }

    private void FillDropdowns()
    {
        KMMastrData objMastData = new KMMastrData();
        //objMastData.BindProductCategory(ddProdCategory);
        objMastData.BindUnit(ddCompanyUnit);
       //objMastData.BindGeneralCategory(ddGeneralCategory) ;
        //ddProdCategory.Items[0].Text = "--Select Product Item--";
        ddCompanyUnit.Items[0].Text = "--Select Unit--";
        //ddGeneralCategory.Items[0].Text = "--Select General Category--";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DateTime dt1 = new DateTime();
        try
        {
            dt1 = System.Convert.ToDateTime(txtPreparedDate.Text.ToString());
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Date Format Error. Please correct the date!");
            return;
        }
        try
        {
            KMEntities objEntities = new KMEntities();
            if (ViewState["SLNO"] != null)
            {
                objKMDB.UpdateATR(System.Convert.ToInt32(ViewState["SLNO"].ToString()), rdCategory.SelectedItem.Value.ToString(), txtGeneralCategory.Text.ToString(), txtComplaintDetails.Text.ToString(),
                   txtProdCategory.Text.ToString(), txtRootCause.Text.ToString(), txtCAShortData.Text.ToString()
                    , txtSearchTags.Text.ToString(), ddCompanyUnit.SelectedItem.Text.ToString(), txtCARNo.Text.ToString(), txtCARDetails.Text.ToString(), txtATRNo.Text.ToString(), txtATRDetails.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString());
                /*objEntities.CRM_KB_Update_ATR(System.Convert.ToInt32(ViewState["SLNO"].ToString()), rdCategory.SelectedItem.Value.ToString(), txtGeneralCategory.Text.ToString(), txtComplaintDetails.Text.ToString(),
                   txtProdCategory.Text.ToString(), txtRootCause.Text.ToString(), txtCAShortData.Text.ToString()
                    , txtSearchTags.Text.ToString(), ddCompanyUnit.SelectedItem.Text.ToString(), txtCARNo.Text.ToString(), txtCARDetails.Text.ToString(), txtATRNo.Text.ToString(), txtATRDetails.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString());
                 * */
                FillCache();
                ShowMessage(divSuccess, "Success", "ATR Updated successfully!");
            }
            else
            {
                objKMDB.CreateATR(rdCategory.SelectedItem.Value.ToString(), txtGeneralCategory.Text.ToString(), txtComplaintDetails.Text.ToString(),
                   txtProdCategory.Text.ToString(), txtRootCause.Text.ToString(), txtCAShortData.Text.ToString()
                    , txtSearchTags.Text.ToString(), ddCompanyUnit.SelectedItem.Text.ToString(), txtCARNo.Text.ToString(), txtCARDetails.Text.ToString(), txtATRNo.Text.ToString(), txtATRDetails.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString(), Session["KBUserID"].ToString());
                /*
                objEntities.CRM_KB_Insert_ATR(rdCategory.SelectedItem.Value.ToString(), txtGeneralCategory.Text.ToString(), txtComplaintDetails.Text.ToString(),
                   txtProdCategory.Text.ToString(), txtRootCause.Text.ToString(), txtCAShortData.Text.ToString()
                    , txtSearchTags.Text.ToString(), ddCompanyUnit.SelectedItem.Text.ToString(), txtCARNo.Text.ToString(), txtCARDetails.Text.ToString(), txtATRNo.Text.ToString(), txtATRDetails.Text.ToString(), txtBenefitAchieved.Text.ToString(), txtPreparedBy.Text.ToString()
                    , dt1, txtApprovedBY.Text.ToString(), Session["KBUserID"].ToString());
                 * */
                FillCache();
                ShowMessage(divSuccess, "Success", "ATR added successfully!");
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
                objEntities.CRM_KB_Delete_ATR_BYID(System.Convert.ToInt32(e.CommandArgument.ToString()));
                ListData();
                ShowMessage(divListSuccess, "Success", "ATR deleted successfully!");
            }
            catch
            {
                ShowMessage(divListError, "Error", "Error while deleting ATR data!");
            }
        }
        else if (e.CommandName == "cmEdit")
        {
            UpdateData();
            List<CRM_KB_Get_ATR_BYID_Result> objList = objEntities.CRM_KB_Get_ATR_BYID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
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
                //    ddGeneralCategory.SelectedValue = objList[0].GeneralCategory.ToString();
                //}
                //catch { }
                //try
                //{
                //    ddProdCategory.SelectedValue = objList[0].Product.ToString();
                //}
                //catch { }
                txtGeneralCategory.Text = objList[0].GeneralCategory.ToString();
                txtProdCategory.Text = objList[0].Product.ToString();
                if (objList[0].Category.ToString().ToUpper() == "CC")
                {
                    rdCategory.Items[0].Selected = true;
                }
                else if (objList[0].Category.ToString().ToUpper() == "IC")
                {
                    rdCategory.Items[1].Selected = true;
                }
                txtApprovedBY.Text = objList[0].ApprovedBY.ToString();
                txtATRDetails.Text = objList[0].ATRDetails.ToString();
                txtATRNo.Text = objList[0].ATRNo.ToString();
                txtBenefitAchieved.Text = objList[0].BenefitAchieved.ToString();
                txtCARDetails.Text = objList[0].CARDetails.ToString();
                txtCARNo.Text = objList[0].CARNo.ToString();
                txtCAShortData.Text = objList[0].CAShortData.ToString();
                txtComplaintDetails.Text = objList[0].ComplaintDetails.ToString();
                txtPreparedBy.Text = objList[0].PreparedBy.ToString();
                txtPreparedDate.Text = objList[0].PreparedDate.ToString();
                txtRootCause.Text = objList[0].RootCause.ToString();
                txtSearchTags.Text = objList[0].SearchTag.ToString();
            }
        }
    }
    private void FillCache()
    {
        if (Cache["KeyWordsATR"] != null)
        {
            Cache.Remove("KeyWordsATR");
        }
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "CRM_KB_GetATRKeyWords";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    Cache.Insert("KeyWordsATR", dt, sqlDep);
                }
                dt.Dispose();
                conn.Close();
            }
        }
    }

 
}