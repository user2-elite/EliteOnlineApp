using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class km : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["KBUserID"] = null;
        if (Request.QueryString["id"] != null)
        {
            Session["KBUserID"] = "kmsearch";
            Session["KBPassword"] = "km@2015";
            Response.Redirect("Search.aspx", false);
        }
    }
}