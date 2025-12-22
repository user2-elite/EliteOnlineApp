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

public partial class ViewAllUsers : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;
    SqlCommand cmd1 = null;
    SqlCommand cmd2 = null;
    CommonClass objCommonClass = new CommonClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();    
        if (Request.QueryString["msg"] != null)
        {
            if (Request.QueryString["msg"].ToString() == "s")
            {
                ShowMessage(divSuccess, "Success:", "Details updated successfully.");
            }
            else if (Request.QueryString["msg"].ToString() == "f")
            {
                ShowMessage(diverror, "Error:", "Error while updating the details.");
            }            
        }
        if (Session["selectedRole"] != null)
        {
            if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "5")
            {
                Response.End();
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }
        if (Session["UserID"] == null)
        {
            Response.Write("<BR><BR><h3>You do not have access to view this page. Please login as administrator and check again.</h3>");
            Response.End();
        }
        if (!IsPostBack)
        {
            BindUserList();
            //if (Session["selectedRole"].ToString() != "1")
            //{
            //    Response.Redirect("home.aspx");
            //}
        }
    }


    //protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string msg = "f";
    //    try
    //    {

    //        string DOB = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[8].Controls[0]).Text.ToString();
    //        if (DOB.Length > 10)
    //        {
    //            DOB = DOB.Substring(0, 10);
    //        }
    //        DOB = DOB.Trim();


    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandText = "UpdateUser";
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        DropDownList ddlItem = new DropDownList();
    //        //cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = ((HiddenField)gvUsers.Rows[e.RowIndex].Cells[66].Controls[0]).Value.ToString();
    //        cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = (Session["EditUID"].ToString());
    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].Cells[1].FindControl("Title");
    //        cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();
    //        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString();
    //        cmd.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString();
    //        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString();
    //        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString();
    //        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString();
    //        cmd.Parameters.Add("@DOB", SqlDbType.VarChar).Value = DOB;

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("SystemType");
    //        cmd.Parameters.Add("@SystemType", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();
    //        //cmd.Parameters.Add("@AssetName", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddAssetName");
    //        cmd.Parameters.Add("@AssetName", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("MouseAllotted");
    //        cmd.Parameters.Add("@MouseAllotted", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ExtMemoryAllotted");
    //        cmd.Parameters.Add("@ExtMemoryAllotted", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("DataCardAllotted");
    //        cmd.Parameters.Add("@DataCardAllotted", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlDepartment");
    //        cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlLocation");
    //        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlArea");
    //        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("Status");
    //        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

    //        cmd.Connection = con;
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        msg = "s";
    //        gvUsers.EditIndex = -1;
    //        Response.Redirect("ViewAllUsers.aspx?msg=" + msg, true);
    //    }
    //    catch (Exception ex)
    //    {
    //        //ShowMessage(diverror, "Error:", "Error while updating the details.");
    //        Response.Redirect("ViewAllUsers.aspx?msg=" + msg, true);
    //    }        
    //}

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


    private void BindUserList()
    {
        try
        {

            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("ViewAllUsers", conn);
            cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            //con = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
            //SqlCommand cmd = new SqlCommand("ViewAllUsers", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@EmpName", txtEmpName.Text.ToString());
            //con.Open();
            //SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            
            if (dt.Rows.Count > 0)
            {
                grid1.DataSource = dt;
                grid1.DataBind();
                grid1.Visible = true;
                ViewState["ObjUserDetails"] = dt;
            }
            else
            {
                grid1.Visible = false;
                ViewState["ObjUserDetails"] = null;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Error While loading data. Please try later.");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Session["SelectedName"] = txtEmpName.Text.ToString();
        BindUserList();
    }

    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            try
            {                
                string UID = e.CommandArgument.ToString();  
                Session["EditUID"] =  UID;
                Response.Redirect("NewUser.aspx", true);
            }
            catch
            {
            }
        }      
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ObjUserDetails"] != null)
            {
                //here session value is stored before bind data on gridview
                DataTable dt = ViewState["ObjUserDetails"] as DataTable;
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=UserList_" + CommonClass.GetDateTimeIST().ToShortDateString() + ".xls");
                Response.ContentType = "application/ms-excel";
                string tab = "";
                int headerIndex = 0;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (headerIndex != 7)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = "\t";
                    }
                    headerIndex = headerIndex + 1;
                }
                Response.Write("\n");

                int i;
                string Content = "";
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i != 7)
                        {
                            Content = "";
                            Content = dr[i].ToString();
                            Content = Content.Replace("\r\n", " ");
                            Content = Content.Replace("  ", " ");
                            Response.Write(tab + Content);
                            tab = "\t";
                        }
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        catch
        {

        }
    }

    protected void grid1_ItemDataBound(object sender, DataGridItemEventArgs e)

    {
        if (e.Item.ItemType == ListItemType.Header || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.EditItem)
        {
            e.Item.Cells[8].Visible = false;
        }
    }
}