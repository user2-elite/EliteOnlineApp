using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllSchedules : System.Web.UI.Page
{
    protected string UserID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            UserID = Session["UserID"].ToString();
        }
        else
        {
            Response.End();
        }
    }
}