using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class PolicyItem : System.Web.UI.Page
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
                string CategoryGroupID = Request.QueryString["ID"];
                BindDataList(CategoryGroupID);
			}
        }
		}
		else
		{
		 Response.Redirect("default.aspx");
		}
    }

    protected void BindDataList(string CategoryGroupID)
    {
        DataTable dt = new DataTable();
        con.Open();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("SELECT ID,FileName,Description,Title,CategoryGroupID from INT_PolicyDocItems WHERE CategoryGroupID="+ CategoryGroupID +" Order by ID DESC", con);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        //dtView = new DataView(dt);
        //dtView.Sort = "ID";
        dlImages.DataSource = dt;
        dlImages.DataBind();
        con.Close();
    }

}

