using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class eventItem : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
    //DataView dtView;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    string ID = Request.QueryString["ID"];
                    BindDataList(ID);
                }
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }

    protected void BindDataList(string ID)
    {
        DataTable dt = new DataTable();
        con.Open();
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("INT_Get_News_BYID", con);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("@ID", ID);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        dlNews.DataSource = dt;
        dlNews.DataBind();
        con.Close();
    }

}

