using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class MGMTTalk : System.Web.UI.Page
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
        //Query to get ImagesName and Description from database
        SqlCommand command = new SqlCommand("INT_Get_MGMT_Talk", con);
        command.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        dlTalk.DataSource = dt;
        dlTalk.DataBind();
        con.Close();
    }

}

