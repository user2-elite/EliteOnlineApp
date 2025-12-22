using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class adminGallery : System.Web.UI.Page
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
            divgalleryItems.Visible = true;
            BindDropDown();
            BindDataList(ddlGallery.SelectedItem.Value.ToString());
        }
    }

    protected void BindDataList(string GroupID)
    {
        DataTable dt = new DataTable();
        con.Open();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT ID,ImageName,Description,Title,GalleryGroupID from INT_GalleryItems WHERE GalleryGroupID=" + GroupID + " Order by ID DESC", con);
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
        ddlGallery.Items.Clear();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT GalleryGroupID,GalleryName from INT_Gallery", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        //DataTable dt = new DataTable();
        da.Fill(dt);
        //dtView = new DataView(dt);
        //dtView.Sort = "ID";
        ddlGallery.DataSource = dt;
        ddlGallery.DataValueField = "GalleryGroupID";
        ddlGallery.DataTextField = "GalleryName";
        ddlGallery.DataBind();
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
            string filename = GetUniQueFileName("~/Gallery/SlideImages", Path.GetFileName(fileuploadimages.PostedFile.FileName));
            //string filename = Path.GetFileName(fileuploadimages.PostedFile.FileName);
            //Save images into SlideImages folder
            fileuploadimages.SaveAs(Server.MapPath("~/Gallery/SlideImages/" + filename));
            
            //Open the database connection
            con.Open();
            //Query to insert images name and Description into database
            SqlCommand cmd = new SqlCommand("Insert into INT_GalleryItems(GalleryGroupID,ImageName,Description,Title,AddedOn,AddedBy) values(@GalleryGroupID,@ImageName,@Description,@Title,@AddedOn,@AddedBy)", con);
            //Passing parameters to query

            cmd.Parameters.AddWithValue("@GalleryGroupID", ddlGallery.SelectedItem.Value.ToString());
            cmd.Parameters.AddWithValue("@ImageName", filename);
            cmd.Parameters.AddWithValue("@Description", txtDesc.Text.ToString());
            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@AddedOn", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@AddedBy", Session["UserID"].ToString());
            cmd.ExecuteNonQuery();
            //Close dbconnection
            con.Close();
            txtDesc.Text = string.Empty;
            BindDataList(ddlGallery.SelectedItem.Value.ToString());
            ShowMessage(divSuccess, "Success: ", "Gallery Picture Added.");
            FillCache();
        }
        catch(Exception ex)
        {
            ShowMessage(diverror, "Error: ", "Error while adding to Gallery." + ex.Message.ToString());
        }
    }

    //protected void Edit_Command(Object sender, DataListCommandEventArgs e)
    //{

    //    // Set the EditItemIndex property to the index of the item clicked
    //    // in the DataList control to enable editing for that item. Be sure
    //    // to rebind the DataList to the data source to refresh the control.
    //    dlImages.EditItemIndex = e.Item.ItemIndex;
    //    BindDataList();

    //}

    //protected void Cancel_Command(Object sender, DataListCommandEventArgs e)
    //{

    //    // Set the EditItemIndex property to -1 to exit editing mode. Be sure
    //    // to rebind the DataList to the data source to refresh the control.
    //    dlImages.EditItemIndex = -1;
    //    BindDataList();

    //}

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
            SqlCommand cmd = new SqlCommand("INT_delete_GalleryItem", con);
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
            BindDataList(ddlGallery.SelectedItem.Value.ToString());
            ShowMessage(divSuccess, "Success: ", "Gallery Item Deleted.");
            FillCache();
        }
        catch
        {

            ShowMessage(diverror, "Error: ", "Error while deleting from Gallery.");
        }
    }

    //    protected void btnDelete_Click(object sender, EventArgs e)
    //    {
    //        string ID = btnDelete.CommanadArgument.ToString();
    //        con.Open();
    //        SqlCommand cmd = new SqlCommand("DELETE FROM INT_GalleryItems WHERE ID="+ ID, con);
    //        cmd.ExecuteNonQuery();
    //        //Close dbconnection
    //        con.Close();
    //        BindDataList();
    //}
    protected void lnkaddCategory_Click(object sender, EventArgs e)
    {
        divCategory.Visible = true;
        divgalleryItems.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divCategory.Visible = false;
        divgalleryItems.Visible = true;
    }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        try
        {
            //Open the database connection
            con.Open();
            //Query to insert images name and Description into database
            SqlCommand cmd = new SqlCommand("Insert into INT_Gallery(GalleryName,Gallery_Descr,CreatedOn,CreatedBy) values(@GalleryName,@Gallery_Descr,@CreatedOn,@CreatedBy)", con);
            //Passing parameters to query
            cmd.Parameters.AddWithValue("@GalleryName", txtCatTitle.Text.ToString());
            cmd.Parameters.AddWithValue("@Gallery_Descr", txtCatDescr.Text.ToString());
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

    protected void ddlGallery_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataList(ddlGallery.SelectedItem.Value.ToString());
    }

    private void FillCache()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT top 3 GalleryGroupID,ID,ImageName,Title,Description FROM INT_GalleryItems ORDER BY ID DESC";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["GalleryItem"] != null)
                            {
                                Cache.Remove("GalleryItem");
                            }
                            Cache.Insert("GalleryItem", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while storing data on Cache!");
        }
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
                    NewFileName = "image" + i + "_" + FileName;
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

