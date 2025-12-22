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
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for MastData
/// </summary>
public class KMMastrData
{
    public KMMastrData()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void BindUnit(DropDownList ddlUnit)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
        SqlCommand cmd = new SqlCommand("CRM_GetAllUnits", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";

        if (dr1.HasRows)
        {
            ddlUnit.DataSource = dr1;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "UnitName";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();        
    }

    public void BindGeneralCategory(DropDownList ddlGeneralCategory)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
        SqlCommand cmd = new SqlCommand("CRM_GetAllGeneralCategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";

        if (dr1.HasRows)
        {
            ddlGeneralCategory.DataSource = dr1;
            ddlGeneralCategory.DataTextField = "CategoryName";
            ddlGeneralCategory.DataValueField = "CategoryName";
            ddlGeneralCategory.DataBind();
            ddlGeneralCategory.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }
    
    public void BindProductCategory(DropDownList ddlProdTypes)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
        SqlCommand cmd = new SqlCommand("CRM_GetAllProductCategory", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";

        if (dr1.HasRows)
        {
            ddlProdTypes.DataSource = dr1;
            ddlProdTypes.DataTextField = "ProductCategory";
            ddlProdTypes.DataValueField = "ProductCategory";
            ddlProdTypes.DataBind();
            ddlProdTypes.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    public void BindArea(DropDownList ddlArea)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
        SqlCommand cmd = new SqlCommand("CRM_GetRSMArea", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";
        if (dr1.HasRows)
        {
            ddlArea.DataSource = dr1;
            ddlArea.DataTextField = "AREA";
            ddlArea.DataValueField = "AREA";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();     
    }

    public void BindComplaintTypes(DropDownList ddlComplaintTypes)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
        SqlCommand cmd = new SqlCommand("CRM_GetAllComplaintTypes", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        SqlDataReader dr1 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        ListItem Listitem0 = new ListItem();
        Listitem0.Value = "0";
        Listitem0.Text = "Choose One";

        if (dr1.HasRows)
        {
            ddlComplaintTypes.DataSource = dr1;
            ddlComplaintTypes.DataTextField = "ComplaintTypes";
            ddlComplaintTypes.DataValueField = "CTypeId";
            ddlComplaintTypes.DataBind();
            ddlComplaintTypes.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }


}