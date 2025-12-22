using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddPayrollAreaName : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (!Page.IsPostBack)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1" )
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            ListData();
        }
    }

  
    private void ListData()
    {
        try
        {
            pnlList.Visible = true;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ConnectionString);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("ListPayrollArea", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (objListAll.Count > 0)
            {
                grid1.DataSource = objListAll;
                grid1.DataBind();
                grid1.Visible = true;
            }
            else
            {
                pnlList.Visible = false;
                grid1.Visible = false;
                ShowMessage(divAlert, "Note", "No data found!");
            }
        }
        catch
        {
            ShowMessage(diverror, "Error", "Error while displaying data!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            SqlCommand cmdSave;
            connSave.Open();
            string strValue = txtName.Text.ToString();
            if (strValue.Length > 1)
            {
                if (ViewState["ID"] != null)
                {
                    cmdSave = new SqlCommand("UpdatePayrollArea", connSave);
                    cmdSave.CommandType = CommandType.StoredProcedure;
                    cmdSave.Parameters.AddWithValue("@Id", ViewState["ID"].ToString());
                    cmdSave.Parameters.AddWithValue("@Name", strValue);
                }
                else
                {
                    cmdSave = new SqlCommand("CreatePayrollArea", connSave);
                    cmdSave.CommandType = CommandType.StoredProcedure;
                    cmdSave.Parameters.AddWithValue("@Name", strValue);
                    cmdSave.Parameters.AddWithValue("@AddedBy", Session["UserID"].ToString());
                }
                cmdSave.ExecuteNonQuery();
                cmdSave.Parameters.Clear();
                connSave.Close();

                if (ViewState["ID"] != null)
                {
                    ShowMessage(divSuccess, "Success: ", "Updated successfully");
                }
                else
                {
                    txtName.Text = "";
                    ShowMessage(divSuccess, "Success: ", "Created successfully");
                }
                ListData();
            }
            else
            {
                ShowMessage(diverror, "Error", "Please enter a valid Input!");
            }

        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while updating data!: " + ex.Message.ToString());
        }
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
        divListSuccess.Visible = false;
        divListError.Visible = false;
    }

    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            try
            {

                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetPayrollAreaByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", e.CommandArgument.ToString());
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                string strValue = dt.Rows[0]["Name"].ToString();
                                ViewState["ID"] = e.CommandArgument.ToString();
                                txtName.Text = strValue;
                                btnSubmit.Text = "Update Details";
                            }
                        }
                        dt.Dispose();
                        conn.Close();
                    }
                }
            }
            catch
            {
                ShowMessage(diverror, "Error", "Error while editing data!");
            }
        }
        else if (e.CommandName == "cmDelete")
        {
            try
            {
                SqlConnection connSave = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
                SqlCommand cmdDelete;
                connSave.Open();
                cmdDelete = new SqlCommand("DeletePayrollArea", connSave);
                cmdDelete.CommandType = CommandType.StoredProcedure;
                cmdDelete.Parameters.AddWithValue("@Id", e.CommandArgument.ToString());
                cmdDelete.ExecuteNonQuery();
                cmdDelete.Parameters.Clear();
                connSave.Close();
                ListData();
                ShowMessage(divListSuccess, "Success", "Deleted successfully!");
            }
            catch
            {
                ShowMessage(divListError, "Error", "Error while deleting data!");
            }
        }
    }
 
}