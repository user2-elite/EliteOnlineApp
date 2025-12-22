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
        Session["KBUserID"] = null;
        Session["KBPassword"] = null;
        Session.Abandon();
        Session.RemoveAll();
        Response.Redirect("login.aspx");
    }
}