using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LeaveMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            if (Session["UserText"] == null)
            {
                Session["UserText"] = "";
            }
            lblLogonuser.Text = Session["LogonUserFullName"].ToString();
        }
        else
        {
            Response.Redirect("../default.aspx");
        }
    }
}
