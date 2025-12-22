using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logoff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Session["Password"] = null;
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
}