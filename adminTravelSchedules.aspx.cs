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
using INTModel;

public partial class adminTravelSchedules : System.Web.UI.Page
{
    INTModelContainer objEntities = new INTModelContainer();
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (!Page.IsPostBack)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "2")
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            ListData();
        }
    }

    private void ListData()
    {
        List<INT_Get_Travel_Schedule_Result> objListAll = objEntities.INT_Get_Travel_Schedule().ToList();
        if (objListAll.Count > 0)
        {
            txtDetails.Text = objListAll[0].Travel_Details.ToString();
            string TravelInfo = objListAll[0].Travel_Details.ToString().Replace('\"', '\'');
            if (Cache["TravelDetails"] != null)
            {
                Cache.Remove("TravelDetails");
            }
            Cache.Insert("TravelDetails", TravelInfo);
        }
        else
        {
            if (Cache["TravelDetails"] != null)
            {
                Cache.Remove("TravelDetails");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            string strDetails = txtDetails.Text.Trim();
            objEntities.INT_Update_Travel_Schedule(strDetails);
            ShowMessage(divSuccess, "Success", "added successfully!");
            ListData();

        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while updating data!: " + ex.Message.ToString());
        }
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

}