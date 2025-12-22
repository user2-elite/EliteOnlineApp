using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

/// <summary>
/// Summary description for Consolidated
/// </summary>
public class Consolidated
{
    public Consolidated()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void GetDDLs(string storeProcedure, DropDownList ddl, string textfield, string valuefield)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand comm = new SqlCommand(storeProcedure, conn);
                comm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ddl.DataSource = ds;
                ddl.DataTextField = textfield;
                ddl.DataValueField = valuefield;
                ddl.DataBind();
                ddl.Items.Insert(0, "<---Select--->");
                ddl.Items[0].Value = "0";
            }

        }
        catch (Exception)
        {
            throw;
        }
    }

    public void FillGrid(string storeProcedure, GridView gv)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand comm = new SqlCommand(storeProcedure, conn);
                comm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                gv.DataSource = ds;
                gv.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }

    }


}