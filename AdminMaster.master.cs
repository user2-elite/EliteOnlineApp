using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMaster : System.Web.UI.MasterPage
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
                if (Session["selectedRole"].ToString() == "1" || Session["selectedRole"].ToString() == "2" || Session["selectedRole"].ToString() == "4" || Session["selectedRole"].ToString() == "5")
                {
                    liSettings.Visible = true;
                    if (Session["selectedRole"].ToString() == "2")
                    {
						//edit travel shedule role
                        link1.Visible = false;
                        link2.Visible = false;
                        link3.Visible = false;
                        link4.Visible = false;
                        link5.Visible = false;
                        link6.Visible = false;
                        link7.Visible = false;
                        link8.Visible = false;
                        link9.Visible = false;
                        link10.Visible = false;
                        //link11.Visible = false; Travel enabled
                        link12.Visible = false;
						link13.Visible = false;
						link14.Visible = false;
						link15.Visible = false;
						link16.Visible = false;
						link17.Visible = false;
						link18.Visible = false;
                    }
                    if (Session["selectedRole"].ToString() == "4")
                    {
						//edit quote role
                        link1.Visible = false;
                        link2.Visible = false;
                        link3.Visible = false;
                        link4.Visible = false;
                        link5.Visible = false;
                        //link6.Visible = false;//Quote Enabled
                        link7.Visible = false;
                        link8.Visible = false;
                        link9.Visible = false;
                        link10.Visible = false;
                        link11.Visible = false;
                        link12.Visible = false;
						link13.Visible = false;
						link14.Visible = false;
						link15.Visible = false;
						link16.Visible = false;
						link17.Visible = false;
						link18.Visible = false;
                    }
					if (Session["selectedRole"].ToString() == "5" || Session["selectedRole"].ToString() == "6")
                    {
						//HR role
                        link1.Visible = false;
                        link2.Visible = false;
                        //link3.Visible = false;
                        //link4.Visible = false;
                        link5.Visible = false;
                        link6.Visible = false;
                        link7.Visible = false;
                        link8.Visible = false;
                        link9.Visible = false;
                        link10.Visible = false;
                        link11.Visible = false;
                        link12.Visible = false;
						link13.Visible = false;
						link14.Visible = false;
						link15.Visible = false;
						link16.Visible = false;
						link17.Visible = false;
						link18.Visible = false;
                    }                    
                    if (Session["selectedRole"].ToString() == "1")
                    {
                        liDashboard.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }

    }
}
