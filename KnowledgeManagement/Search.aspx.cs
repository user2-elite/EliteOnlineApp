using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMModel;

public partial class Search : System.Web.UI.Page
{
    KMDBOperation myDBOperation = new KMDBOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["KBUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
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
        try
        {
            divSearchResultsATR.InnerHtml = "";
            string strBestPracticeResults = "";
            KMModel.KMEntities objKMEntities = new KMModel.KMEntities();
            string strBPKeyWords = txtSearch2.Text.ToString();
            string searchType = ddlSearchType2.SelectedItem.Value.ToString();
            strBPKeyWords = searchType + '|' + strBPKeyWords;
            List<KMModel.CRM_KB_Search_BestPracticeResults_Result> objList = objKMEntities.CRM_KB_Search_BestPracticeResults(strBPKeyWords.ToString()).ToList();
            strBestPracticeResults += "<br><div style='width:99%; padding:3px;' class='searchheader'>Total number of result(s) found: " + objList.Count.ToString() + "</div>";
            if (objList.Count > 0)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    strBestPracticeResults += "<table cellpadding='8' cellspacing='1' width='100%' class='ui-search-results'>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left' width='25%'><B>Area : </B></td><td valign='top' align='left' width='75%'>" + objList[i].Area.ToString() + "</td></tr>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Activity : </B></td><td valign='top' align='left'>" + objList[i].Activity.ToString() + "</td></tr>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Product : </B></td><td valign='top' align='left'>" + objList[i].Product.ToString() + "</td></tr>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Unit Name : </B></td><td valign='top' align='left'>" + objList[i].UnitName.ToString() + "</td></tr>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Monitoring System : </B></td><td valign='top' align='left'>" + objList[i].MonitoringSystem.ToString() + "</td></tr>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Best Practice Summary : </B></td><td valign='top' align='left'>" + objList[i].BestPracticeSummary.ToString() + "</td></tr>";
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>SOP Reference and version No : </B></td><td valign='top' align='left'>" + objList[i].SOPReferenceAndVersionNo.ToString() + "</td></tr>";                    
                    strBestPracticeResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Benefit Achieved : </B></td><td valign='top' align='left'>" + objList[i].BenefitAchieved.ToString() + "</td></tr>";
                    strBestPracticeResults += "</table>";
                    strBestPracticeResults += "<div style='border: 1px; background-color: #E0E0E0; width: 99%; padding:5px;'><font color='maroon'><B>Prepared By :&nbsp;&nbsp;&nbsp;" + objList[i].PreparedBy.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Approved By:&nbsp;&nbsp;&nbsp;" + objList[i].ApprovedBY.ToString() + "</B></font></div>";
                    strBestPracticeResults += "<hr width='100%; height:1px' /></BR>";
                }
            }
            divSearchResultsBP.InnerHtml = strBestPracticeResults;
        }
        catch (Exception ex)
        {
            divSearchResultsBP.InnerHtml = "<font color='red'>Error in search. Please verify the error message. Message: " + ex.Message.ToString() + "</font>";
        }


    }
    protected void btnSubmit3_Click(object sender, EventArgs e)
    {
        try
        {
            divSearchResultsATR.InnerHtml = "";
            string strATRResults = "";
            KMModel.KMEntities objKMEntities = new KMModel.KMEntities();
            string strATRKeyWord = txtSearch3.Text.ToString();
            string searchType = ddlSearchType3.SelectedItem.Value.ToString();
            strATRKeyWord = searchType + '|' + strATRKeyWord;
            List<KMModel.CRM_KB_Search_ATRResults_Result> objList = objKMEntities.CRM_KB_Search_ATRResults(strATRKeyWord.ToString()).ToList();
            strATRResults += "<br><div style='width:99%; padding:3px;' class='searchheader'>Total number of result(s) found: " + objList.Count.ToString() + "</div>";
            if (objList.Count > 0)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    strATRResults += "<table cellpadding='8' cellspacing='1' width='100%' class='ui-search-results'>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left' width='25%'><B>Category : </B></td><td valign='top' align='left' width='75%'>" + objList[i].Category.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>General Category : </B></td><td valign='top' align='left'>" + objList[i].GeneralCategory.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Complaint Details : </B></td><td valign='top' align='left'>" + objList[i].ComplaintDetails.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Product : </B></td><td valign='top' align='left'>" + objList[i].Product.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Root Cause : </B></td><td valign='top' align='left'>" + objList[i].RootCause.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Short data about C/A: </B></td><td valign='top' align='left'>" + objList[i].CAShortData.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Unit Name : </B></td><td valign='top' align='left'>" + objList[i].UnitName.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>CAR in short : </B></td><td valign='top' align='left'><B>CAR NO: </B>" + objList[i].CARNo.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>Details : </B>" + objList[i].CARDetails.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>ATR in short : </B></td><td valign='top' align='left'><B>ATR NO: </B>" + objList[i].ATRNo.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>Details : </B>" + objList[i].ATRDetails.ToString() + "</td></tr>";
                    strATRResults += "<tr class='ui-search-resultsTR'><td valign='top' align='left'><B>Benefit Achieved : </B></td><td valign='top' align='left'>" + objList[i].BenefitAchieved.ToString() + "</td></tr>";
                    strATRResults += "</table>";
                    strATRResults += "<div style='border: 1px; background-color: #E0E0E0; width: 99%; padding:5px;'><font color='maroon'><B>Prepared By :&nbsp;&nbsp;&nbsp;" + objList[i].PreparedBy.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Approved By:&nbsp;&nbsp;&nbsp;" + objList[i].ApprovedBY.ToString() + "</B></font></div>";

                    strATRResults += "<hr width='100%; height:1px' /></BR>";
                }
            }
            divSearchResultsATR.InnerHtml = strATRResults;
        }
        catch(Exception ex)
        {
            divSearchResultsATR.InnerHtml = "<font color='red'>Error in search. Please verify the error message. Message: " + ex.Message.ToString() + "</font>";
        }
    }
}