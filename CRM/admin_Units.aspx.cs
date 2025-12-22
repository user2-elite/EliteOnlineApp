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

public partial class admin_Units : System.Web.UI.Page
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
            Session["unitID"] = null;
            FillGrid();
            if (Session["CRMselectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }
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
                cmd.CommandText = "CRM_InsertUnits";
                cmd.Parameters.AddWithValue("@UnitName", txtUnitName.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.Dispose();
                lblMessage.Text = "Success: New Unit name Created.";
                lblMessage.Style.Add("color", "Green");
                FillGrid();
            }
            else if (btnSave.Text.Trim() == "Update")
            {
                if (Session["unitID"] != null)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CRM_UpdateUnits";
                    cmd.Parameters.AddWithValue("@UnitName", txtUnitName.Text.ToString());
                    cmd.Parameters.AddWithValue("@unitID", Session["unitID"].ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    lblMessage.Text = "Success: Unit name updated.";
                    lblMessage.Style.Add("color", "Green");
                }
                else
                {
                    lblMessage.Text = "Error: Unit ID not Found. Please click on Edit link in below list and update.";
                    lblMessage.Style.Add("color", "RED");
                }
            }
            FillGrid();
        }
        catch
        {
            lblMessage.Text = "Error: Unit ID not added/updated. Please verify the text details and try again.";
            lblMessage.Style.Add("color", "RED");
        }

    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditRow")
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            cmd = new SqlCommand("CRM_GetUnitByID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@unitID", e.CommandArgument.ToString());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                txtUnitName.Text = dr["UnitName"].ToString();
                Session["unitID"] = dr["unitID"].ToString();
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
            cmd.CommandText = "CRM_GetAllUnits";
            SqlDataAdapter daroleshow = new SqlDataAdapter();
            DataSet dsroleshow = new DataSet();
            daroleshow.SelectCommand = cmd;
            daroleshow.Fill(dsroleshow, "GVdata");
            GridView1.DataSource = dsroleshow.Tables[0];
            GridView1.DataBind();
        }
        catch
        {
            lblMessage.Text = "Error: Unit details failed to load. Please try again.";
            lblMessage.Style.Add("color", "RED");
        }
    }
}