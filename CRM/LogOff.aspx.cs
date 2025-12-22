using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogOff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["CRMUserID"] = null;
        Session["Password"] = null;
        Session["LogonName"] = null;
        Session["CRMselectedRole"] = null;
        Session["CRMselectedRoleName"] = null;
        Session["UnitId"] = null;
        Session["UnitName"] = null;
        Session["Email"] = null;
        Session["Phone"] = null;
        Session.Abandon();
        Session.RemoveAll();
        Response.Redirect("login.aspx");
    }
}