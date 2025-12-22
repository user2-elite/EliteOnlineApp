using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;


public partial class adminGalleryAdd : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDataList();
            divCategory.Visible = false;
            divgalleryItems.Visible = true;
        }
    }
    protected void BindDataList()
    {
        con.Open();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT ImageName,Description,Title,GalleryGroupID from INT_GalleryItems", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        da.Fill(dt);
        dlImages.DataSource = dt;
        dlImages.DataBind();
        con.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Get Filename from fileupload control
        string filename = Path.GetFileName(fileuploadimages.PostedFile.FileName);
        //Save images into SlideImages folder
        fileuploadimages.SaveAs(Server.MapPath("Gallery/SlideImages/" + filename));
        //Open the database connection
        con.Open();
        //Query to insert images name and Description into database
        SqlCommand cmd = new SqlCommand("Insert into INT_GalleryItems(GalleryGroupID,ImageName,Description,Title,AddedOn,AddedBy) values(@GalleryGroupID,@ImageName,@Description,@Title,@AddedOn,@AddedBy", con);
        //Passing parameters to query

        cmd.Parameters.AddWithValue("@GalleryGroupID", ddlGallery.SelectedItem.Value.ToString());
        cmd.Parameters.AddWithValue("@ImageName", filename);
        cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        cmd.Parameters.AddWithValue("@AddedOn", DateTime.Now.ToShortDateString());
        cmd.Parameters.AddWithValue("@AddedBy", Session["UserID"].ToString());
        cmd.ExecuteNonQuery();
        //Close dbconnection
        con.Close();
        txtDesc.Text = string.Empty;
        BindDataList();
    }
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
        //Open the database connection
        con.Open();
        //Query to insert images name and Description into database
        SqlCommand cmd = new SqlCommand("Insert into INT_Gallery(GalleryName,Gallery_Descr,CreatedOn,CreatedBy) values(@GalleryName,@Gallery_Descr,@CreatedOn,@CreatedBy", con);
        //Passing parameters to query
        cmd.Parameters.AddWithValue("@GalleryName", txtCatTitle.Text.ToString());
        cmd.Parameters.AddWithValue("@Gallery_Descr", txtCatDescr.Text.ToString());
        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToShortDateString());
        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
        cmd.ExecuteNonQuery();
        //Close dbconnection
        con.Close();
        txtDesc.Text = string.Empty;
    }
}

