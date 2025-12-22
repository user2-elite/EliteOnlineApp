using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for Documents
/// </summary>
public class Documents
{
    private string strAppConn;
    public Documents()
    {
        strAppConn = ConfigurationManager.ConnectionStrings["CRM"].ToString();
    }
    public DataSet DownloadFile(string Complaint_ID)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(strAppConn);
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRM_Download_File";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Complaint_ID", Complaint_ID);
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public void BindFiles(DataGrid dgDoc, string Complaint_ID)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(strAppConn.ToString());
        cmd.Connection = conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        conn.Open();
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CRM_DownloadAllFiles";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Complaint_ID", Complaint_ID);
            da = new SqlDataAdapter(cmd);
            ds.Clear();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dgDoc.DataSource = ds;
                dgDoc.DataBind();
                dgDoc.Visible = true;

            }
            else
            {
                dgDoc.Visible = false;

            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open) ;
                conn.Close();
            }
        }
    }
    
}
