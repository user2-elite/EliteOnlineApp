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
using System.Web.UI.HtmlControls;

public partial class admin_AssignRole : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["CRMUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }

        if (!IsPostBack)
        {
            Session["EditID"] = null;
            BindUnitandRoles();
            FillGrid();
            if (Session["CRMselectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }
        clearMessages();
        ShowMessage(divNotice, "Information: ", "CRM/Legal role will be mapped to all units irrespective of the Unit name selected while creating the user roles. Please use unique Login Id and Password for all other role users for each company Units.");
    }

    private void ShowMessage(HtmlGenericControl div1, string spanText, string Message)
    {
        clearMessages();
        div1.InnerHtml = "<span>" + spanText + "</span>";
        div1.InnerHtml += ": ";
        div1.InnerHtml += Message;
        div1.Visible = true;
    }
    private void clearMessages()
    {
        divAlert.InnerHtml = "";
        divAlert.Visible = false;
        divSuccess.InnerHtml = "";
        divSuccess.Visible = false;
        diverror.InnerHtml = "";
        diverror.Visible = false;
        divNotice.InnerHtml = "";
        divNotice.Visible = false;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            if (btnSave.Text.Trim() == "Add")
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CRM_InsertUserRole";
                cmd.Parameters.AddWithValue("@RoleID", ddlRole.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@Name", txtFullName.Text.ToString());
                cmd.Parameters.AddWithValue("@UserID", txtUID.Text.ToString());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
                cmd.Parameters.AddWithValue("@UnitId", ddlUnit.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                lblMessage.Text = "Success: New User Created.";
                lblMessage.Style.Add("color", "Green");
                FillGrid();
            }
            else if (btnSave.Text.Trim() == "Update")
            {
                if (Session["EditID"] != null)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CRM_UpdateUserRoles";
                    cmd.Parameters.AddWithValue("@ID", Session["EditID"].ToString());
                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.ToString());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.ToString());
                    cmd.Parameters.AddWithValue("@Active", ddlActive.SelectedItem.Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    lblMessage.Text = "Success: User Details updated.";
                    lblMessage.Style.Add("color", "Green");
                }
                else
                {
                    lblMessage.Text = "Error: User ID not Found. Please click on Edit link in below list and update.";
                    lblMessage.Style.Add("color", "RED");
                }
            }
            FillGrid();
        }
        catch
        {
            lblMessage.Text = "Error: User Details not added/updated. Please verify the text details and try again.";
            lblMessage.Style.Add("color", "RED");
        }

    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditRow")
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            cmd = new SqlCommand("CRM_GetUserDetailsByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                ddlRole.SelectedValue = dr["RoleID"].ToString();
                txtFullName.Text = dr["Name"].ToString();
                txtUID.Text = dr["UserID"].ToString();
                txtPassword.Text = dr["Password"].ToString();
                ddlUnit.SelectedValue = dr["UnitId"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                Session["EditID"] = dr["ID"].ToString();
                txtUID.Enabled = false;
                ddlUnit.Enabled = false;
                ddlRole.Enabled = false;
                ddlActive.Enabled = true;
            }
            btnSave.Text = "Update";
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();
        }
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.PageIndex = 0;
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRM_GetAllUsers";
            SqlDataAdapter daroleshow = new SqlDataAdapter();
            DataSet dsroleshow = new DataSet();
            daroleshow.SelectCommand = cmd;
            daroleshow.Fill(dsroleshow, "GVdata");
            GridView1.DataSource = dsroleshow.Tables[0];
            GridView1.DataBind();
        }
        catch
        {
            lblMessage.Text = "Error: User details failed to load. Please try again.";
            lblMessage.Style.Add("color", "RED");
        }
    }

    private void BindUnitandRoles()
    {
        MastData objmasterdata = new MastData();
        objmasterdata.BindUnit(ddlUnit);
        objmasterdata.BindRoles(ddlRole);
    }

}