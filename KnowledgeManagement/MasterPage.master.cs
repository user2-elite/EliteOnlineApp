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
        if (Session["KBUserID"] != null)
        {
            PanelAdmin.Visible = true;

            if (Session["KBUserID"].ToString() == "km")
            {
                lnkAddKB.Visible = true;
                lnkAddATR.Visible = true;
                lnkAddBPractice.Visible = true;
                lnkSearch.Visible = true;
                lnkLogOff.Visible = true;
            }
            else
            {
                lnkAddKB.Visible = false;
                lnkAddATR.Visible = false;
                lnkAddBPractice.Visible = false;
                lnkSearch.Visible = true;
                lnkLogOff.Visible = true;
            }
        }
    }
}
