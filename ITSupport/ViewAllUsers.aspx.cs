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
        if (Session["AdminLinks"] == null)
        {
            Session["AdminLinks"] = "0";
        }        

        //Session["UserID"] = "jay";
        //Session["selectedRole"] = "1";
        //string strTitle = "&nbsp;&nbsp;&nbsp;&nbsp;Elite IT SupportDesk&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        //Page.Title = strTitle;
        //Session["masterChecking"] = "1";
        //Session["MyTimeZoneID"] = "1";
        //if (Session["UserID"].ToString() == "jay")
        //{
        //    Session["AdminLinks"] = "1";
        //}

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
    }

    protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            HiddenField hUID = (HiddenField)e.Row.FindControl("hUID");
            if (hUID != null)
            {
                Session["EditUID"] = hUID.Value.ToString();
            }
            //DropDownList Title = (DropDownList)e.Row.FindControl("Title");
            //HiddenField hTitle = (HiddenField)e.Row.FindControl("hTitle");
            //if (Title != null)
            //{
            //    Title.SelectedItem.Text = hTitle.Value.ToString();
            //}

            //DropDownList DataCardAllotted = (DropDownList)e.Row.FindControl("DataCardAllotted");
            //HiddenField hDataCardAllotted = (HiddenField)e.Row.FindControl("hDataCardAllotted");
            //if (DataCardAllotted != null)
            //{
            //    BindList(DataCardAllotted);
            //    DataCardAllotted.SelectedItem.Text = hDataCardAllotted.Value.ToString();
            //}

            //DropDownList SystemType = (DropDownList)e.Row.FindControl("SystemType");
            //HiddenField hSystemType = (HiddenField)e.Row.FindControl("hSystemType");
            //if (SystemType != null)
            //{                
            //    SystemType.SelectedItem.Text = hSystemType.Value.ToString();
            //}

            //DropDownList MouseAllotted = (DropDownList)e.Row.FindControl("MouseAllotted");
            //HiddenField hMouseAllotted = (HiddenField)e.Row.FindControl("hMouseAllotted");
            //if (MouseAllotted != null)
            //{
            //    BindList(MouseAllotted);
            //    MouseAllotted.SelectedItem.Text = hMouseAllotted.Value.ToString();
            //}

            //DropDownList ExtMemoryAllotted = (DropDownList)e.Row.FindControl("ExtMemoryAllotted");
            //HiddenField hExtMemoryAllotted = (HiddenField)e.Row.FindControl("hExtMemoryAllotted");
            //if (ExtMemoryAllotted != null)
            //{
            //    BindList(ExtMemoryAllotted);
            //    ExtMemoryAllotted.SelectedItem.Text = hExtMemoryAllotted.Value.ToString();
            //}
            
            //DropDownList dl1 = (DropDownList)e.Row.FindControl("ddlLocation");
            //DropDownList dl2 = (DropDownList)e.Row.FindControl("ddlDepartment");
            //DropDownList dl3 = (DropDownList)e.Row.FindControl("ddlArea");

            //if (dl1 != null && dl2 != null && dl3 != null)
            //{
            //    BindDepartmentsandLocations(dl1, dl2, dl3);

            //    HiddenField hddlArea = (HiddenField)e.Row.FindControl("hddlArea");
            //    if (dl3 != null)
            //    {
            //        dl3.SelectedValue = hddlArea.Value.ToString();
            //    }

            //    HiddenField hDepartment = (HiddenField)e.Row.FindControl("hDepartment");
            //    if (dl1 != null)
            //    {
            //        dl1.SelectedValue = hDepartment.Value.ToString();
            //    }

            //    HiddenField hddlLocation = (HiddenField)e.Row.FindControl("hddlLocation");
            //    if (dl2 != null)
            //    {
            //        dl2.SelectedValue = hddlLocation.Value.ToString();
            //    }

              
            //}
        }
    }

    private void BindList(DropDownList drp1)
    {
        ListItem item1 = new ListItem("Yes", "1");
        ListItem item2 = new ListItem("No", "0");
        drp1.Items.Add(item1);
        drp1.Items.Add(item2);
    }

    private void BindDepartmentsandLocations(DropDownList ddlLocation, DropDownList ddlDepartments, DropDownList ddlArea)
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

    protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UpdateUser";
        cmd.CommandType = CommandType.StoredProcedure;

        DropDownList ddlItem = new DropDownList();
        //cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = ((HiddenField)gvUsers.Rows[e.RowIndex].Cells[66].Controls[0]).Value.ToString();
        cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = (Session["EditUID"].ToString());
        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].Cells[1].FindControl("Title");
        cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();
        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString();
        cmd.Parameters.Add("@EmpCode", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString();        
        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString();
        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[5].Controls[0]).Text.ToString();
        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[7].Controls[0]).Text.ToString();
        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("SystemType");
        cmd.Parameters.Add("@SystemType", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();
        //cmd.Parameters.Add("@AssetName", SqlDbType.VarChar).Value = ((TextBox)gvUsers.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddAssetName");
        cmd.Parameters.Add("@AssetName", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("MouseAllotted");
        cmd.Parameters.Add("@MouseAllotted", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ExtMemoryAllotted");
        cmd.Parameters.Add("@ExtMemoryAllotted", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("DataCardAllotted");
        cmd.Parameters.Add("@DataCardAllotted", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlDepartment");
        cmd.Parameters.Add("@Department", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlLocation");
        cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("ddlArea");
        cmd.Parameters.Add("@Area", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();

        ddlItem = (DropDownList)gvUsers.Rows[e.RowIndex].FindControl("Status");
        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = ddlItem.SelectedItem.Value.ToString();    

        cmd.Connection = con;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();

        gvUsers.EditIndex = -1;
        Response.Redirect("ViewAllUsers.aspx");
    }
}