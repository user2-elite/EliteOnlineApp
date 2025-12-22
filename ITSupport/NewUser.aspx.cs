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


public partial class NewUser : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    CommonClass objCommonClass = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["selectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
            BindDepartmentsandLocations();
            BindAsset();
        }
    }

    protected void ibtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
                SqlConnection connSave = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
                SqlCommand cmdSave;
                connSave.Open();
                cmdSave = new SqlCommand("CreateNewUser", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@Title", ddlPrifix.SelectedItem.Value.Trim());
                cmdSave.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                cmdSave.Parameters.AddWithValue("@EmpCode", txtEmpCode.Text.ToString());
                cmdSave.Parameters.AddWithValue("@Email", txtEmail.Text.ToString());
                cmdSave.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                cmdSave.Parameters.AddWithValue("@UID", txtUID.Text.Trim());
                cmdSave.Parameters.AddWithValue("@SystemType", ddlStype.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@AssetName", ddlAssetName.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@MouseAllotted", isMouse.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@ExtMemoryAllotted", isExtD.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@DataCardAllotted", isDatacard.Checked.ToString());
                cmdSave.Parameters.AddWithValue("@Department", ddlDepartments.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Location", ddlLocation.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@Area", ddlArea.SelectedItem.Value.ToString());                                            
                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();                                                    
                connSave.Close();
                lblError.Text = "Submitted successfully";
                lblError.Style.Add("Color", "Green");
                txtUID.Text = "";
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper().Contains("VIOLATION OF PRIMARY KEY"))
            {
                lblError.Text = "Please enter unique UserID.";
                lblError.Style.Add("Color", "Red");
            }
            else
            {
                lblError.Text = "Error while saving details. Please correct the data. ";
                lblError.Text += ex.Message.ToString();
                lblError.Style.Add("Color", "Red");
            }
        }
    }

    protected void ibtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("NewUser.aspx");
    }

    protected void LogRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }

    protected void LogOff_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Session["Password"] = null;
        Response.Redirect("login.aspx");
    }

    private void BindDepartmentsandLocations()
    {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            SqlCommand cmd = new SqlCommand("GetLocations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            ListItem Listitem0 = new ListItem();
            Listitem0.Value = "0";
            Listitem0.Text = "----Select----";
                 
            if (dr1.HasRows)
            {
                ddlLocation.DataSource = dr1;
                ddlLocation.DataTextField = "LocationDetails";
                ddlLocation.DataValueField = "LocationDetails";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, Listitem0);
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();

            cmd1 = new SqlCommand("GetDepartments", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.HasRows)
            {
                ddlDepartments.DataSource = dr;
                ddlDepartments.DataTextField = "DepartmentName";
                ddlDepartments.DataValueField = "DepartmentName";
                ddlDepartments.DataBind();
                ddlDepartments.Items.Insert(0, Listitem0);
            }
            cmd1.Parameters.Clear();
            cmd1.Dispose();
            con.Close();

            cmd2 = new SqlCommand("GetAreas", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr2.HasRows)
            {
                ddlArea.DataSource = dr2;
                ddlArea.DataTextField = "AreaName";
                ddlArea.DataValueField = "AreaName";
                ddlArea.DataBind();
                ddlArea.Items.Insert(0, Listitem0);
            }
            cmd2.Parameters.Clear();
            cmd2.Dispose();
            con.Close();
    }

    private void BindAsset()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        SqlCommand cmd = new SqlCommand("IT_GetAssetNames", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "----Select----";

        if (dr1.HasRows)
        {
            ddlAssetName.DataSource = dr1;
            ddlAssetName.DataTextField = "AssetName";
            ddlAssetName.DataValueField = "AssetName";
            ddlAssetName.DataBind();
            ddlAssetName.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }
}
