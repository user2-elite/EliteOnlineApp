using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddKB : System.Web.UI.Page
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
        //Show KB Destails
        myDBOperation.BindKBwithGridview(dgKB, "0");
    }

    protected void btnSaveKB_Click(object sender, EventArgs e)
    {

        if (myDBOperation.CreateKB("0", ddProdCategory.SelectedItem.Text.ToString(),
            ddCompanyUnit.SelectedItem.Text.ToString(), ddTypeOfComplaint.SelectedItem.Text.ToString()
            , txtRootCauseAnalysis.Text.ToString(), txtKBDetails.Text.ToString(), txtKBSolution.Text.ToString(), Session["CRMUserID"].ToString()))
        {
            lblMessageText5.Text = "Success: Knowledge Management Information updated.";
            txtKBDetails.Text = "";
            txtKBSolution.Text = "";
            txtRootCauseAnalysis.Text = "";
            lblMessageText5.CssClass = "alert-box success";
            myDBOperation.BindKBwithGridview(dgKB, "0");
        }
        else
        {
            lblMessageText5.Text = "Error: Knowledge Management Information could not save. Please verify the details and try again.";
            lblMessageText5.CssClass = "alert-box error";
        }
    }

    protected void dgKB_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Click")
        {
            if (myDBOperation.DeleteKB(e.CommandArgument.ToString()))
            {
                lblMessageText5.Text = "Success: Knowledge Management Information deleted.";
                lblMessageText5.CssClass = "alert-box success";
                myDBOperation.BindKBwithGridview(dgKB, "0");
            }
            else
            {
                lblMessageText5.Text = "Error: Knowledge Management Information could not delete. Please verify the details and try again.";
                lblMessageText5.CssClass = "alert-box error";
            }
        }
    }
    private void FillDropdowns()
    {
        MastData objMastData = new MastData();
        objMastData.BindComplaintTypes(ddTypeOfComplaint);
        objMastData.BindProductCategory(ddProdCategory);
        objMastData.BindUnit(ddCompanyUnit);
    }



}