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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (!Page.IsPostBack)
        {
            disableMenus();
            PanelAdmin.Visible = false;
            if (Session["CRMUserID"] != null)
            {
                PanelAdmin.Visible = true;
                if (Session["CRMselectedRole"].ToString() == "1")
                {
                    lnkUnitMapping.Visible = true;
                    lnkProducts.Visible = true;
                    lnkCompTypes.Visible = true;
                    lnkAssignRole.Visible = true;
                    lnkReports.Visible = true;
                    //lnkDashboard.Visible = true;
                    lnkRegister.Visible = true;
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "2")
                {
                    lnkReports.Visible = true;
                    lnkRegister.Visible = true;
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "3")
                {
                    lnkReports.Visible = true;
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "4")
                {
                    lnkReports.Visible = true;
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "5")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "6")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "7")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "8")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "9")
                {
                    lnkDashboard.Visible = true;
                    lnkReports.Visible = true;
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "10")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "11")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "12")
                {
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
                else if (Session["CRMselectedRole"].ToString() == "13")
                {
                    lnkReports.Visible = true;
                    lnkChangePassword.Visible = true;
                    lnkHome.Visible = true;
                    lnkListComp.Visible = true;
                }
            }
        }
    }

    private void disableMenus()
    {
        lnkUnitMapping.Visible = false;
        lnkProducts.Visible = false;
        lnkCompTypes.Visible = false;
        lnkAssignRole.Visible = false;
        lnkReports.Visible = false;
        lnkRegister.Visible = false;
        lnkChangePassword.Visible = false;
        lnkHome.Visible = false;
        lnkListComp.Visible = false;
        lnkDashboard.Visible = false;
    }

}
