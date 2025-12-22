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

public partial class admin_SubCategory : System.Web.UI.Page
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
            divHelpDesk.InnerHtml = "&nbsp;&nbsp;Selected Helpdesk: " + Session["HelpDeskName"].ToString();
            if (!Page.IsPostBack)
            {
                bindGroup();
            }
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
        
        if (Session["selectedRole"].ToString() != "1")
        {
            Response.Redirect("~/home.aspx");
        }

    }

    private void bindGroup()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
        SqlCommand cmd = new SqlCommand("GetALLActiveWorkSchedule", con);
        cmd.Parameters.AddWithValue("@HelpDeskID", Session["HelpDeskID"].ToString());
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataAdapter daAccess = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        daAccess.Fill(ds, "Group");
        DDGroup.DataSource = ds.Tables["Group"];
        DDGroup.DataTextField = "GroupName";
        DDGroup.DataValueField = "GroupID";
        DDGroup.DataBind();

        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();

        ListItem Listitem = new ListItem();
        Listitem.Value = "0";
        Listitem.Text = "----Select----";
        DDGroup.Items.Insert(0, Listitem);
    }

    protected void AddnewCat_Click(object sender, EventArgs e)
    {
        try
        {
                SqlDataSource1.InsertParameters.Clear();
                SqlDataSource1.InsertParameters.Add("RequestType1", CateogryTxt.Text);
                SqlDataSource1.InsertParameters.Add("HelpDeskID", Session["HelpDeskID"].ToString());
                SqlDataSource1.InsertParameters.Add("GroupID", DDGroup.SelectedItem.Value.ToString());
                SqlDataSource1.InsertParameters.Add("CreatedBy", Session["UserID"].ToString());

                SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
                SqlDataSource1.InsertCommand = "InsertRequestType1";
                SqlDataSource1.Insert();
                CateogryTxt.Text = "";

        }
        catch
        {
        }
    }

    //protected void AddSubCategory_Click(object sender, EventArgs e)
    //{
    //        if (AddSubCategory.CommandArgument.ToString() != "")
    //        {
    //            try
    //            {
    //                SqlDataSource1.InsertParameters.Clear();
    //                SqlDataSource1.InsertParameters.Add("RequestType1ID", AddSubCategory.CommandArgument.ToString());
    //                SqlDataSource1.InsertParameters.Add("RequestType2", SubCatTxt.Text);

    //                SqlDataSource1.InsertParameters.Add("CreatedBy", Session["UserID"].ToString());

    //                SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
    //                SqlDataSource1.InsertCommand = "InsertRequestType2";
    //                SqlDataSource1.Insert();
    //                SubCatTxt.Text = "";
    //            }
    //            catch
    //            {
    //            }
    //    }
    //    else
    //    {
    //        MainCategory.Text = "Error : Main category should be selected";
    //        MainCategory.ForeColor = System.Drawing.Color.Red;
    //    }
    //}


    //protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    GridViewRow row = (GridViewRow)GridView1.Rows[e.NewSelectedIndex];
    //    Label LB1 = (Label)row.FindControl("Label1");
    //    if (LB1 != null)
    //    {
    //        MainCategory.Text = "Cateogry : " + LB1.Text;
    //        MainCategory.ForeColor = System.Drawing.Color.Black;
    //    }
    //    AddSubCategory.CommandArgument = GridView1.DataKeys[e.NewSelectedIndex].Values["RequestType1ID"].ToString();
    //}

    //protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    DropDownList DDLStatus = (DropDownList)GridView2.Rows[e.RowIndex].FindControl("StatusDDL");
    //    SqlDataSource2.UpdateParameters.Add("Status", DDLStatus.SelectedValue.ToString());
    //}

    //protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    Label lb1 = (Label)GridView2.Rows[e.NewEditIndex].FindControl("label2");
    //    ViewState["Statuskey2"] = lb1.Text;
    //}

    //protected void GridView2_PreRender(object sender, EventArgs e)
    //{
    //    if (GridView2.EditIndex != -1)
    //    {
    //        DropDownList DDLStatus = (DropDownList)GridView2.Rows[GridView2.EditIndex].FindControl("StatusDDL");
    //        DDLStatus.SelectedValue = ViewState["Statuskey2"].ToString();
    //    }
    //}

 }
