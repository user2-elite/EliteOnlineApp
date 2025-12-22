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

public partial class admin_AssignRole : System.Web.UI.Page
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
            divHelpDesk.InnerHtml = "&nbsp;&nbsp;Selected Helpdesk: "+ Session["HelpDeskName"].ToString();
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

            SqlCommand cmd = new SqlCommand("GetALLActiveWorkSchedule", con);
            cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            daAccess.Fill(ds, "Group");
            ViewState["Groupset"] = ds.Tables["Group"];
            GroupDD.DataSource = ds.Tables["Group"];
            GroupDD.DataTextField = "GroupName";
            GroupDD.DataValueField = "GroupID";
            GroupDD.DataBind();

            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            ListItem Listitem = new ListItem();
            Listitem.Value = "0";
            Listitem.Text = "----Select----";
            GroupDD.Items.Insert(0, Listitem);

        }

    }


    protected void ResolverSubmit_Click(object sender, EventArgs e)
    {
        //Page.Validate("Group1");
        if (!Page.IsValid)
        {
            return;
        }

        if (GroupDD.SelectedIndex == 0)
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
            if (ResolverSubmit.Text == "Add")
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("InsertResolver", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@Resolver", ResolverTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Role", RoleDD.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Group", GroupDD.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ProfLevel", "4");
                cmd.Parameters.AddWithValue("@status", Status.SelectedValue.ToString());

                cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

                string Msg = "Assigned the role to user.";
                Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location=\"admin_AssignRole.aspx\";\n</script>\n");
            }
            else if (ResolverSubmit.Text == "Update")
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("UpdateAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@ResolverID", ResolverSubmit.CommandArgument.ToString());
                cmd.Parameters.AddWithValue("@Resolver", ResolverTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Role", RoleDD.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Group", GroupDD.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ProfLevel", "4");
                cmd.Parameters.AddWithValue("@status", Status.SelectedValue.ToString());

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();

                string Msg = "Status Updated. If duplicate entries are created, updations will not happen";
                Response.Write("<script language=\"javascript\">\nalert('" + Msg + "');window.location=\"admin_AssignRole.aspx\";\n</script>\n");
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
            if (ResolverSubmit.Text != "Update" )
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("GetEmpDetailsforDupeCheck", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@empno", ResolverTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
                cmd.Parameters.AddWithValue("@Group", GroupDD.SelectedValue.ToString());

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.Read())
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid =true;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();
            }
            else
            {
                args.IsValid = true;
            }

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
                cmd = new SqlCommand("GetALLResolversByResolverID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ResolverID", e.CommandArgument.ToString());
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.Read())
                {
                    ResolverTxt.Text = dr["Resolver"].ToString();
                    RoleDD.SelectedValue = dr["Admin"].ToString();
                    Status.SelectedValue = dr["ResolverStatus"].ToString();

                    if ((dr["ResolverGroup"].ToString() == null) || (dr["ResolverGroup"].ToString() == ""))
                    {
                        GroupDD.SelectedIndex = 0;
                    }
                    else
                    {
                        DataTable dt = (DataTable)ViewState["Groupset"];
                        DataRow[] matchingRows = dt.Select("GroupID=" + dr["ResolverGroup"].ToString());
                        if (matchingRows.Length > 0)
                        {
                            GroupDD.SelectedValue = dr["ResolverGroup"].ToString().Trim();
                        }
                        else
                        {
                            GroupDD.SelectedIndex = 0;
                        }
                    }

                    ResolverSubmit.CommandArgument = dr["ResolverID"].ToString();
                    ResolverSubmit.Text = "Update";
                    ResolverTxt.Enabled = false;
                    ResolverSubmit.Focus();
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

}