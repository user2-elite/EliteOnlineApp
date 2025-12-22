using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMModel;

public partial class SearchKM : System.Web.UI.Page
{
    KMDBOperation myDBOperation = new KMDBOperation();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["KBUserID"] == null)
        //{
        //    Response.Redirect("login.aspx");
        //}
        if (!Page.IsPostBack)
        {
            FillDropdowns();
        }
    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        {
            string TypeOfComplaint = "0";
            string ProdCategory = "0";
            string CompanyUnit = "0";

            string strText = txtSearch1.Text.ToString().Trim();
            string strCompStatus = "0"; //ddlSearchType.SelectedValue.ToString();
            if (ddTypeOfComplaint.SelectedItem.Value.ToString() != "0")
            {
                TypeOfComplaint = ddTypeOfComplaint.SelectedItem.Text.ToString();
            }
            if (ddProdCategory.SelectedItem.Value.ToString() != "0")
            {
                ProdCategory = ddProdCategory.SelectedItem.Text.ToString();
            }
            if (ddCompanyUnit.SelectedItem.Value.ToString() != "0")
            {
                CompanyUnit = ddCompanyUnit.SelectedItem.Text.ToString();
            }

            //Show KB Search Results
            divSearchResultsCRM.InnerHtml = "<BR>";
            divSearchResultsCRM.InnerHtml = myDBOperation.GetKBResults(TypeOfComplaint, ProdCategory, CompanyUnit, strText, strCompStatus);
        }
    }

    private void FillDropdowns()
    {
        KMMastrData objMastData = new KMMastrData();
        objMastData.BindComplaintTypes(ddTypeOfComplaint);
        objMastData.BindProductCategory(ddProdCategory);
        objMastData.BindUnit(ddCompanyUnit);
        ddTypeOfComplaint.Items[0].Text = "--All Complaint Types--";
        ddProdCategory.Items[0].Text = "--All Product Items--";
        ddCompanyUnit.Items[0].Text = "--All Units--";
    }
    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        KMModel.KMEntities objKMEntities = new KMModel.KMEntities();            
        string strBPKeyWords = txtSearch2.Text.ToString();
        List<KMModel.CRM_KB_Search_BestPracticeResults_Result> objList = objKMEntities.CRM_KB_Search_BestPracticeResults(strBPKeyWords.ToString()).ToList();
        if (objList.Count > 0)
        {

        }
    }
    protected void btnSubmit3_Click(object sender, EventArgs e)
    {
        KMModel.KMEntities objKMEntities = new KMModel.KMEntities();
        string strATRKeyWord = txtSearch3.Text.ToString();
        List<KMModel.CRM_KB_Search_ATRResults_Result> objList = objKMEntities.CRM_KB_Search_ATRResults(strATRKeyWord.ToString()).ToList();
        if (objList.Count > 0)
        {

        }
    }
}