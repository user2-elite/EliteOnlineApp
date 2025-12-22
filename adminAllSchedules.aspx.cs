using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class adminAllSchedules : System.Web.UI.Page
{
    protected string UserID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"] != null)
                {
                    if (Session["selectedRole"].ToString() != "1")
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
        else
        {
            Response.Redirect("default.aspx");
        }
        divAlert.Visible = false;
        if (!Page.IsPostBack)
        {
            UserID = "";
            if (Request.QueryString["uid"] != null)
            {
                UserID = Request.QueryString["uid"].ToString();
            }
            BindUsers(ddlParticipants, UserID);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strUserID = ddlParticipants.SelectedItem.Value.ToString();
            Response.Redirect("adminAllSchedules.aspx?uid=" + strUserID, false);
        }
        catch
        {
            UserID = "";
            divAlert.InnerText = "Error while loading the calendar for " + ddlParticipants.SelectedItem.Text.ToString();
        }
    }
    private void BindUsers(DropDownList ddl1, string UID)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        SqlCommand cmd = new SqlCommand("Conf_Get_AllUsers", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "";
        Listitem0.Text = "---- Choose Employee Name ---";

        if (dr1.HasRows)
        {
            ddl1.DataSource = dr1;
            ddl1.DataTextField = "Name";
            ddl1.DataValueField = "UID";
            ddl1.DataBind();
            ddl1.Items.Insert(0, Listitem0);
        }
        try
        {
            if (UID.Length > 0)
            {
                ddl1.SelectedValue = UID;
            }
        }
        catch
        {

        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }
}