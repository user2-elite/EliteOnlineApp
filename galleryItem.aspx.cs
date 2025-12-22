using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class galleryItem : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
    //DataView dtView;   
    protected void Page_Load(object sender, EventArgs e)
    {		
	  if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
        if (!IsPostBack)
        {
			if(Request.QueryString["ID"] != null)
			{
				string GalleryGroupID = Request.QueryString["ID"];
				BindDataList(GalleryGroupID);
			}
        }
		}
		else
		{
		 Response.Redirect("default.aspx");
		}
    }

    protected void BindDataList(string GalleryGroupID)
    {
        DataTable dt = new DataTable();
        con.Open();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT ID,ImageName,Description,Title,GalleryGroupID from INT_GalleryItems WHERE GalleryGroupID="+ GalleryGroupID +" Order by ID DESC", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        //dtView = new DataView(dt);
        //dtView.Sort = "ID";
        dlImages.DataSource = dt;
        dlImages.DataBind();
        con.Close();
    }

}

