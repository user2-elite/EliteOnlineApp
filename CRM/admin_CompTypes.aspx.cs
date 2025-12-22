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

public partial class admin_CompTypes : System.Web.UI.Page
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
            Session["CTypeId"] = null;
            FillGrid();
            BindCategory();
            if (Session["CRMselectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }
    }

    private void BindCategory()
    {
        MastData objmasterdata = new MastData();
        objmasterdata.BindComplaintTypeCategory(ddlCategory);
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
                cmd.CommandText = "CRM_InsertComplaintTypes";
                cmd.Parameters.AddWithValue("@ComplaintTypes", txtComplaintTypes.Text.ToString());
                cmd.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedItem.Value.ToString());
                
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                lblMessage.Text = "Success: New Complaint Type Created.";
                lblMessage.Style.Add("color", "Green");
                FillGrid();
            }
            else if (btnSave.Text.Trim() == "Update")
            {
                if (Session["CTypeId"] != null)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CRM_UpdateComplaintTypes";
                    cmd.Parameters.AddWithValue("@ComplaintTypes", txtComplaintTypes.Text.ToString());
                    cmd.Parameters.AddWithValue("@CTypeId", Session["CTypeId"].ToString());
                    cmd.Parameters.AddWithValue("@CategoryID", ddlCategory.SelectedItem.Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    lblMessage.Text = "Success: Complaint Type updated.";
                    lblMessage.Style.Add("color", "Green");
                }
                else
                {
                    lblMessage.Text = "Error: Complaint Type ID not Found. Please click on Edit link in below list and update.";
                    lblMessage.Style.Add("color", "RED");
                }
            }
            FillGrid();
        }
        catch
        {
            lblMessage.Text = "Error: Complaint Type not added/updated. Please verify the text details and try again.";
            lblMessage.Style.Add("color", "RED");
        }

    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditRow")
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            cmd = new SqlCommand("CRM_GetComplaintTypesByID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CTypeId", e.CommandArgument.ToString());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                txtComplaintTypes.Text = dr["ComplaintTypes"].ToString();
                Session["CTypeId"] = dr["CTypeId"].ToString();
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
            cmd.CommandText = "CRM_GetAllComplaintTypes";
            SqlDataAdapter daroleshow = new SqlDataAdapter();
            DataSet dsroleshow = new DataSet();
            daroleshow.SelectCommand = cmd;
            daroleshow.Fill(dsroleshow, "GVdata");
            GridView1.DataSource = dsroleshow.Tables[0];
            GridView1.DataBind();
        }
        catch
        {
            lblMessage.Text = "Error: Complaint Type details failed to load. Please try again.";
            lblMessage.Style.Add("color", "RED");
        }
    }
}