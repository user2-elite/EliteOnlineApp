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
        //if (Session["AdminLinks"] == null)
        //{
        //    Session["AdminLinks"] = "0";
        //}        

        //hardcoded. To be removed
        //Session["UserID"] = "jay";
        //Session["selectedRole"] = "1";
        //string strTitle = "&nbsp;&nbsp;&nbsp;&nbsp;Elite IT SupportDesk&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //Page.Title = strTitle;
        Session["masterChecking"] = "1";
        Session["MyTimeZoneID"] = "1";
        //if (Session["UserID"].ToString() == "jay")
        //{
        //    Session["AdminLinks"] = "1";
        //}

        lnkNewUser.Visible = false;
        lnkAllusers.Visible = false;

        PanelAdmin.Visible = false;
        if (Session["UserID"] != null)
        {
            if (Session["AdminLinks"] != null)
            {
                if (Session["AdminLinks"].ToString() == "1")
                {
                    PanelAdmin.Visible = true;
                    if (Session["selectedRole"].ToString() == "1")
                    {
                        lnkViewActivites.Enabled = true;
                        lnkAssetMaster.Enabled = true;
                        //lnkNewUser.Enabled = true;
                        //lnkAllusers.Enabled = true;
                        lnkAllReq.Enabled = true;
                        lnkMyReq.Enabled = true;
                        lnkCategory.Enabled = true;
                        lnkSubCtegory.Enabled = true;
                        lnkPriority.Enabled = true;
                        lnkAssignRole.Enabled = true;
                        lnkReports.Enabled = true;
                        //if (Session["UserID"].ToString() == "pinku")
                        //{
                        //    lnkDashBoard.Enabled = true;
                        //}
                        //else 
                        //{ 
                        //    lnkDashBoard.Enabled = false; 
                        //}
                        lnkDashBoard.Enabled = false; 
                    }
                    else if (Session["selectedRole"].ToString() == "2")
                    {
                        lnkViewActivites.Enabled = true;
                        lnkAllReq.Enabled = true;
                        lnkMyReq.Enabled = true;
                    }
                    else if (Session["selectedRole"].ToString() == "3")
                    {
                        lnkViewActivites.Enabled = true;
                        lnkMyReq.Enabled = true;
                    }
                    else
                    {
                        lnkViewActivites.Enabled = false;
                        lnkAssetMaster.Enabled = false;
                        //lnkNewUser.Enabled = false;
                        //lnkAllusers.Enabled = false;
                        lnkCategory.Enabled = false;
                        lnkSubCtegory.Enabled = false;
                        lnkPriority.Enabled = false;
                        lnkAssignRole.Enabled = false;
                        lnkReports.Enabled = false;
                        lnkDashBoard.Enabled = false;
                    }
                }
            }
        }
    }


    //protected void SelectRole_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (SelectRole.SelectedIndex != 0)
    //    {
    //        string selectedRoleandHID = SelectRole.SelectedValue.ToString();
    //        string[] ar = selectedRoleandHID.Split(new char[] { '-' });
    //        Session["selectedRole"] = ar[0].ToString();
    //        Session["HelpDeskID"] = ar[1].ToString();
    //        string[] ar1 = SelectRole.SelectedItem.ToString().Split(new char[] { '-' });
    //        Session["HelpDeskName"] = ar1[0].ToString();
    //        Response.Write("<script language=\"javascript\">window.location=\"ViewAllRequests.aspx\";\n</script>\n");
    //    }
    //}

}
