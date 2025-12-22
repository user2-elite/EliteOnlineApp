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

public partial class AssetMaster : System.Web.UI.Page
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
        }

        if (Request.QueryString["Edit"] != null)
        {
            if (!Page.IsPostBack)
            {
                ListData();
            }
        }
        else
        {
            AddData();
        }

    }

    private void AddData()
    {
        pnlList.Visible = false;
        pnlAddNew.Visible = true;
        lnkViewDetails.Visible = true;        
        if (!Page.IsPostBack)
        {
            BindResponsibleMembers();
        }
    }
    private void ListData()
    {
        pnlList.Visible = true;
        pnlAddNew.Visible = false;
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());

        SqlCommand cmd = new SqlCommand("IT_get_AllAssets", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        daAccess.Fill(ds, "List");
        if (ds.Tables["List"].Rows.Count > 0)
        {
            grid1.DataSource = ds.Tables["List"];
            grid1.DataBind();
            grid1.Visible = true;
        }
        else
        {
            grid1.Visible = false;
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    private void UpdateData()
    {
        pnlList.Visible = false;
        pnlAddNew.Visible = true;

        lnkViewDetails.Visible = true;
        BindResponsibleMembers();
    }

    protected void ibtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int Frequency = 0;
            try
            {
                Frequency = System.Convert.ToInt32(txtActivityFrequency.Text.ToString());
            }
            catch
            {
                lblError.Text = "Please Enter a valid number in Frequency Field";
                lblError.Style.Add("Color", "Red");
                return;
            }
            SqlConnection connSave = new SqlConnection(ConfigurationManager.AppSettings["ConnString"].ToString());
            SqlCommand cmdSave;
            connSave.Open();

            if (ViewState["ID"] != null)
            {
                cmdSave = new SqlCommand("IT_Update_Assetmaster", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@AssetName", txtAssetName.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AssetDescription", txtAssetDescription.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AssetType", ddlAssetType.SelectedItem.Text.ToString());
                cmdSave.Parameters.AddWithValue("@AssetMaintenanceResponsible", ddlResponsible.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@NextActivityFrequencyInDays", Frequency);
                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                lblError.Text = "Updated Successfully";
                lblError.Style.Add("Color", "Green");
            }
            else
            {
                cmdSave = new SqlCommand("IT_Insert_Assetmaster", connSave);
                cmdSave.CommandType = CommandType.StoredProcedure;
                cmdSave.Parameters.AddWithValue("@AssetName", txtAssetName.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AssetDescription", txtAssetDescription.Text.Trim());
                cmdSave.Parameters.AddWithValue("@AssetType", ddlAssetType.SelectedItem.Text.ToString());
                cmdSave.Parameters.AddWithValue("@AssetMaintenanceResponsible", ddlResponsible.SelectedItem.Value.ToString());
                cmdSave.Parameters.AddWithValue("@LastMntActivityUpdatedOn", txtLastActivityDate.Text.Trim());
                cmdSave.Parameters.AddWithValue("@NextActivityFrequencyInDays", Frequency);
                string ReturnStr = cmdSave.ExecuteScalar().ToString();
                cmdSave.Parameters.Clear();
                if (ReturnStr == "1")
                {
                    lblError.Text = "Saved Successfully";
                    lblError.Style.Add("Color", "Green");
                    txtAssetName.Text = "";
                    txtAssetDescription.Text = "";
                }
                else
                {
                    lblError.Text = "Please enter an unique Asset Name.";
                    lblError.Style.Add("Color", "Red");
                }
            }
            connSave.Close();
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper().Contains("VIOLATION OF PRIMARY KEY"))
            {
                lblError.Text = "Please enter unique Asset Name.";
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
        Response.Redirect("AssetMaster.aspx");
    }

    private void BindResponsibleMembers()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());

        SqlCommand cmd = new SqlCommand("IT_GetALLResponsibleMembers", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        daAccess.Fill(ds, "List");
        ddlResponsible.DataSource = ds.Tables["List"];
        ddlResponsible.DataTextField = "NAME";
        ddlResponsible.DataValueField = "Resolver";
        ddlResponsible.DataBind();
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();

        ListItem Listitem = new ListItem();
        Listitem.Value = "0";
        Listitem.Text = "----Select----";
        ddlResponsible.Items.Insert(0, Listitem);
    }


    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            ViewState["ID"] = e.CommandArgument.ToString();
            UpdateData();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
                cmd = new SqlCommand("IT_get_AssetBYID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    txtActivityFrequency.Text = dr["NextActivityFrequencyInDays"].ToString();
                    txtAssetName.Text = dr["AssetName"].ToString();
                    txtAssetName.Enabled = false;
                    txtAssetDescription.Text = dr["AssetDescription"].ToString();
                    txtLastActivityDate.Text = dr["LastMntActivityUpdatedOn"].ToString();
                    txtLastActivityDate.Enabled = false;
                    ddlAssetType.SelectedValue = dr["AssetType"].ToString();
                    ddlResponsible.SelectedValue = dr["AssetMaintenanceResponsible"].ToString();
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                con.Close();
            }
            catch { }
        }
    }
}