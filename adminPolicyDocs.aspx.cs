using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class adminPolicyDocs : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
    //DataView dtView;   
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (!IsPostBack)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1")
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            divCategory.Visible = false;
            divCategoryItems.Visible = true;
            BindDropDown();
            if (ddlCategory.Items.Count > 0)
            {
                BindDataList(ddlCategory.SelectedItem.Value.ToString());
            }
        }
    }

    protected void BindDataList(string GroupID)
    {
        DataTable dt = new DataTable();
        con.Open();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT ID,FileName,Description,Title,CategoryGroupID from INT_PolicyDocItems WHERE CategoryGroupID=" + GroupID + " Order by ID DESC", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        //DataTable dt = new DataTable();
        da.Fill(dt);
        //dtView = new DataView(dt);
        //dtView.Sort = "ID";
        if (dt.Rows.Count > 0)
        {
            dlImages.DataSource = dt;
            dlImages.DataBind();
        }
        con.Close();
    }

    protected void BindDropDown()
    {
        DataTable dt = new DataTable();
        con.Open();
        ddlCategory.Items.Clear();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT CategoryGroupID,CategoryName from INT_PolicyDocCategory", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        //DataTable dt = new DataTable();
        da.Fill(dt);
        //dtView = new DataView(dt);
        //dtView.Sort = "ID";
        ddlCategory.DataSource = dt;
        ddlCategory.DataValueField = "CategoryGroupID";
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataBind();
        con.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            //Get Filename from fileupload control
            if (fileuploadimages.PostedFile.ContentLength >= 524288)
            {
                ShowMessage(diverror, "Warning: ", "File size should be less than 500 KB.");
                return;
            }
            else if (fileuploadimages.PostedFile.ContentLength <= 0)
            {
                ShowMessage(diverror, "Warning: ", "Please upload an image file. File size should be within 500 KB size.");
                return;
            }
            string filename = GetUniQueFileName("~/Gallery/PolicyDocs", Path.GetFileName(fileuploadimages.PostedFile.FileName));
            //string filename = Path.GetFileName(fileuploadimages.PostedFile.FileName);
            //Save images into SlideImages folder
            fileuploadimages.SaveAs(Server.MapPath("~/Gallery/PolicyDocs/" + filename));
            string strTitle = txtTitle.Text.ToString();
            if (strTitle.Length == 0)
            {
                strTitle = filename;
            }
            //Open the database connection
            con.Open();
            //Query to insert images name and Description into database
            SqlCommand cmd = new SqlCommand("Insert into INT_PolicyDocItems(CategoryGroupID,FileName,Description,Title,AddedOn,AddedBy) values(@CategoryGroupID,@FileName,@Description,@Title,@AddedOn,@AddedBy)", con);
            //Passing parameters to query

            cmd.Parameters.AddWithValue("@CategoryGroupID", ddlCategory.SelectedItem.Value.ToString());
            cmd.Parameters.AddWithValue("@FileName", filename);
            cmd.Parameters.AddWithValue("@Description", txtDesc.Text.ToString());
            cmd.Parameters.AddWithValue("@Title", strTitle);
            cmd.Parameters.AddWithValue("@AddedOn", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@AddedBy", Session["UserID"].ToString());
            cmd.ExecuteNonQuery();
            //Close dbconnection
            con.Close();
            txtDesc.Text = string.Empty;
            BindDataList(ddlCategory.SelectedItem.Value.ToString());
            ShowMessage(divSuccess, "Success: ", "Document Added.");
        }
        catch(Exception ex)
        {
            ShowMessage(diverror, "Error: ", "Error while adding to Document." + ex.Message.ToString());
        }
    }


    protected void Delete_Command(Object sender, DataListCommandEventArgs e)
    {
        // Retrieve the name of the item to remove.
        String item = ((Label)e.Item.FindControl("ItemLabel")).Text;
        // Filter the CartView for the selected item and remove it from
        // the data source.
        //dtView.RowFilter = "ID='" + item + "'";
        //if (dtView.Count > 0)
        //{
        //    dtView.Delete(0);
        //}
        //dtView.RowFilter = "";
        try
        {
            con.Open();
            //Query to insert images name and Description into database
            SqlCommand cmd = new SqlCommand("INT_delete_PolicyDocItems", con);
            //Passing parameters to query
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", item);
            cmd.ExecuteNonQuery();
            //Close dbconnection
            con.Close();

            // Set the EditItemIndex property to -1 to exit editing mode. Be sure
            // to rebind the DataList to the data source to refresh the control.
            //dlImages.EditItemIndex = -1;
            BindDropDown();
            BindDataList(ddlCategory.SelectedItem.Value.ToString());
            ShowMessage(divSuccess, "Success: ", "Document Item Deleted.");
        }
        catch
        {

            ShowMessage(diverror, "Error: ", "Error while deleting item.");
        }
    }

    protected void lnkaddCategory_Click(object sender, EventArgs e)
    {
        divCategory.Visible = true;
        divCategoryItems.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divCategory.Visible = false;
        divCategoryItems.Visible = true;
    }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        try
        {            
            //Open the database connection
            con.Open();
            //Query to insert images name and Description into database
            SqlCommand cmd = new SqlCommand("Insert into INT_PolicyDocCategory(CategoryName,Category_Descr,CreatedOn,CreatedBy) values(@CategoryName,@Category_Descr,@CreatedOn,@CreatedBy)", con);
            //Passing parameters to query
            cmd.Parameters.AddWithValue("@CategoryName", txtCatTitle.Text.ToString());
            cmd.Parameters.AddWithValue("@Category_Descr", txtCatDescr.Text.ToString());
            cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
            cmd.ExecuteNonQuery();
            //Close dbconnection
            con.Close();
            ShowMessage(divSuccess, "Success: ", "Category Created.");
            BindDropDown();
        }
        catch
        {
            ShowMessage(diverror, "Error: ", "Error while adding Category.");
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
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataList(ddlCategory.SelectedItem.Value.ToString());
    }

 
    private string GetUniQueFileName(string Path, string FileName)
    {
        string NewFileName = "";
        try
        {
            if (File.Exists(Server.MapPath(Path + "/" + FileName)))
            {
                for (int i = 1; i <= 100; i++)
                {
                    NewFileName = "docs" + i + "_" + FileName;
                    if (!File.Exists(Server.MapPath(Path + "/" + NewFileName)))
                    {
                        break;
                    }
                }
            }
            else
            {
                NewFileName = FileName;
            }
        }
        catch
        {
            NewFileName = FileName;
        }
        return NewFileName;
    }
 
}

