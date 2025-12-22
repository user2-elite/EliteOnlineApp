using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InnerPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {      
            liSettings.Visible = false;
        liDashboard.Visible = false;
        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            lblLogonuser.Text = Session["LogonUserFullName"].ToString();
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() == "1" || Session["selectedRole"].ToString() == "2")
                {
                    liSettings.Visible = true;
                }

                if (Session["selectedRole"].ToString() == "5" || Session["selectedRole"].ToString() == "3" || Session["selectedRole"].ToString() == "1")
                {
                    liDashboard.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }
}
