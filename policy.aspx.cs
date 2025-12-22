using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class policy : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
    //DataView dtView;   
    protected void Page_Load(object sender, EventArgs e)
    {
	if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
        if (!IsPostBack)
        {
            BindDataList();
        }
		}
		else
		{
			Response.Redirect("default.aspx");
		}
    }

    protected void BindDataList()
    {
        DataTable dt = new DataTable();
        con.Open();
        SqlCommand command = new SqlCommand("INT_GetCategoryFolders", con);
        command.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(command);
        //DataTable dt = new DataTable();
        da.Fill(dt);
        dlImages.DataSource = dt;
        dlImages.DataBind();
        con.Close();
    }

}

