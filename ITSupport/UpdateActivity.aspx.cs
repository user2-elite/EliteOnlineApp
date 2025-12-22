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
public partial class UpdateActivity : System.Web.UI.Page
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
            if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "2" && Session["selectedRole"].ToString() != "3")
            {
                Response.Redirect("home.aspx");
            }
        }
        if (Session["AssetID"] != null)
        {
            ViewState["AssetID"] = Session["AssetID"].ToString();
            Session["AssetID"] = null;
        }
        if (ViewState["AssetID"] != null)
        {
            AddData();
        }
        else
        {
            lblError.Text = "No Activity Details found";
            lblError.Style.Add("Color", "Red");
        }
    }

    private void AddData()
    {
        pnlAddNew.Visible = true;
          try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("IT_get_AssetBYID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ViewState["AssetID"].ToString());
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    txtAssetName.Text = dr["AssetName"].ToString();
                    txtAssetDescription.Text = dr["AssetDescription"].ToString();
                    txtLastActivityDate.Text = dr["LastMntActivityUpdatedOn"].ToString();
                    txtSystemType.Text = dr["AssetType"].ToString();
                    txtPlanneddate.Text = dr["NextActivityDate"].ToString(); 
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();
            }
            catch { }
    }
   
    protected void ibtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlConnection connSave = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
            SqlCommand cmdSave;
            connSave.Open();

            if (ViewState["AssetID"] != null)
            {
                cmdSave = new SqlCommand("IT_Insert_ScheduledActivity_History", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@ID", ViewState["AssetID"].ToString());
                cmdSave.Parameters.AddWithValue("@ActualExecutionDate", txtExectionDate.Text.ToString());
                cmdSave.Parameters.AddWithValue("@ExecutionDescription", txtExecutionDetails.Text.ToString());
                cmdSave.Parameters.AddWithValue("@UID", Session["UserID"].ToString());
                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                lblError.Text = "Updated Successfully";
                lblError.Style.Add("Color", "Green");
            }
            else
            {
                lblError.Text = "Please select the asset details from the previous screen";
                lblError.Style.Add("Color", "Red");
            }
            connSave.Close();
        }
        catch (Exception ex)
        {
            lblError.Text = "Error while saving details. Please correct the data. ";
            lblError.Text += ex.Message.ToString();
            lblError.Style.Add("Color", "Red");
        }
    }

    protected void ibtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AssetMaster.aspx");
    }
  
}