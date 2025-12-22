using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KBView : System.Web.UI.Page
{
    DBOperation myDBOperation = new DBOperation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CRMUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (!Page.IsPostBack)
        {
            FillDropdowns();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string TypeOfComplaint = "0";
        string ProdCategory = "0";
        string CompanyUnit = "0";

        string strText = txtSearchText.Text.ToString().Trim();
        string strCompStatus = ddlSearchType.SelectedValue.ToString();
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
        divSearchResults.InnerHtml = "<BR>";
        divSearchResults.InnerHtml = myDBOperation.GetKBResults(TypeOfComplaint, ProdCategory,CompanyUnit, strText, strCompStatus);                
    }

    private void FillDropdowns()
    {
        MastData objMastData = new MastData();
        objMastData.BindComplaintTypes(ddTypeOfComplaint);
        objMastData.BindProductCategory(ddProdCategory);
        objMastData.BindUnit(ddCompanyUnit);
        ddTypeOfComplaint.Items[0].Text = "--All Complaint Types--";
        ddProdCategory.Items[0].Text = "--All Product Items--";
        ddCompanyUnit.Items[0].Text = "--All Units--";
    }
}