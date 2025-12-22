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

public partial class admin_Severity : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (Session["HelpDeskID"] != null)
        {
            divHelpDesk.InnerHtml = "&nbsp;&nbsp;Selected Helpdesk: " + Session["HelpDeskName"].ToString();
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
        
        if (!IsPostBack)
        {
            if (Session["selectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());

            //SqlCommand cmd = new SqlCommand("GetALLActiveWorkSchedule", con);
            //cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
            //cmd.CommandType = CommandType.StoredProcedure;
            //con.Open();
            //SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //daAccess.Fill(ds, "Group");
            //ViewState["Groupset"] = ds.Tables["Group"];
            //GroupDD.DataSource = ds.Tables["Group"];
            //GroupDD.DataTextField = "GroupName";
            //GroupDD.DataValueField = "GroupID";
            //GroupDD.DataBind();
            GroupDD.Visible = false;
            SqlCommand cmd = new SqlCommand("[GetALLCategories]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            daAccess.Fill(ds, "Group");
            ViewState["Groupset"] = ds.Tables["Group"];
            ddlCat.DataSource = ds.Tables["Group"];
            ddlCat.DataTextField = "RequestType1";
            ddlCat.DataValueField = "RequestType1ID";
            ddlCat.DataBind();

            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            ListItem Listitem = new ListItem();
            Listitem.Value = "0";
            Listitem.Text = "----Select----";
            ddlCat.Items.Insert(0, Listitem);
        }

    }


    protected void SeveritySubmit_Click(object sender, EventArgs e)
    {

        if (ddlCat.SelectedIndex == 0)
        {
            GroupErrorLb.Visible = true;
            return;
        }
        else
        {
            GroupErrorLb.Visible = false;
        }


        try
        {
            if (SeveritySubmit.Text == "Add")
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("InsertSeverity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@Severity", SeverityCode.Text.Trim());
                cmd.Parameters.AddWithValue("@SResolutionTime", txtResolutionTime.Text);
                cmd.Parameters.AddWithValue("@SResponseTime", txtResponseTime.Text);
                cmd.Parameters.AddWithValue("@Status", Status.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Category", ddlCat.SelectedItem.Value.ToString());

                cmd.Parameters.AddWithValue("@DefaultSeverity", "0");

                cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

                string Msg = "New Severity Level Added";
                Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location=\"admin_Severity.aspx\";\n</script>\n");
            }
            else if (SeveritySubmit.Text == "Update")
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("UpdateSeverity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@SeverityID", SeveritySubmit.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("@Severity", SeverityCode.Text.Trim());
                cmd.Parameters.AddWithValue("@SResolutionTime", txtResolutionTime.Text);
                cmd.Parameters.AddWithValue("@SResponseTime", txtResponseTime.Text);
                cmd.Parameters.AddWithValue("@Status", Status.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Group", ddlCat.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@DefaultSeverity", "0");
                cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());

               cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

                string Msg = "Severity Level Updated";
                Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location=\"admin_Severity.aspx\";\n</script>\n");
            }

        }
        catch
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();
            Response.Write("Error. Please try again");
        }

    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("DoSeverityDupCheck", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
                cmd.Parameters.AddWithValue("@Severity", SeverityCode.Text.Trim());
                cmd.Parameters.AddWithValue("@SeverityID", SeveritySubmit.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("@Group", ddlCat.SelectedValue.ToString());

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.Read())
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();
            }
   
        catch
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();

            args.IsValid = false;
        }
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName.ToString() == "editdetails")
            {

                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("GetAllSeveritiesByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SeverityID", e.CommandArgument.ToString());
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.Read())
                {
                    SeverityCode.Text = dr["Severity"].ToString();
                    Status.SelectedValue = dr["Status"].ToString();

                    txtResponseTime.Text = dr["SResponseTime"].ToString();
                    txtResolutionTime.Text = dr["SResolutionTime"].ToString();

                    SeveritySubmit.CommandArgument = dr["SeverityID"].ToString();

                    if ((dr["GroupId"].ToString() == null) || (dr["GroupId"].ToString() == ""))
                    {
                        ddlCat.SelectedIndex = 0;
                    }
                    else
                    {
                        DataTable dt = (DataTable)ViewState["Groupset"];
                        DataRow[] matchingRows = dt.Select("GroupID=" + dr["GroupId"].ToString());
                        if (matchingRows.Length > 0)
                        {
                            ddlCat.SelectedValue = dr["GroupId"].ToString().Trim();
                        }
                        else
                        {
                            ddlCat.SelectedIndex = 0;
                        }
                    }



                    SeveritySubmit.Text = "Update";
                    SeveritySubmit.Focus();
                }

                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();
            }

        }
        catch
        {
            if (con != null)
                if (con.State == ConnectionState.Open)
                    con.Close();
            Response.Write("Error. Please try again");
        }

    }


    protected void ClearForm_Click(object sender, EventArgs e)
    {
        SeverityCode.Text = "";
        SeveritySubmit.Text = "Add";
        Status.SelectedValue = "Active";

    }


}