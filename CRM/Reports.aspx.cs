using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CRMUserID"] == null)
        {
           Response.Redirect("login.aspx");
        }

        if (!IsPostBack)
        {

            gvReport.Visible = false;
            btnExport1.Visible = false;
            btnExport2.Visible = false;
        }       
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToShortDateString();
            txtToDate.Text = DateTime.Now.ToShortDateString();
            RenderReport("Daily");
            divReportName.InnerHtml = "View Report - Datewise";
        }
    }

/*
    private void bindYearWeek()
    {
        ListItem item1;
        for (int i = 1; i <= 53; i++)
        {
            item1 = new ListItem("WK-"+ i.ToString(), i.ToString());
            ddlWeek.Items.Add(item1);
        }
        //ddlWeek.SelectedItem.Value = DateTime.Now.DayOfWeek.ToString();
        ListItem item2;
        for (int j = 2014; j <= DateTime.Now.Year + 1; j++)
        {
            item2 = new ListItem(j.ToString(), j.ToString());
            ddlYear.Items.Add(item2);
        }
        ddlYear.SelectedItem.Value = DateTime.Now.Year.ToString();
    }
*/

    protected void lnkDaily_Click(object sender, EventArgs e)
    {
        RenderReport("Daily");
        divReportName.InnerHtml = "View Report - Datewise";
    }


    protected void lnkWeekly_Click(object sender, EventArgs e)
    {
            //bindYearWeek();
        RenderReport("Weekly");
        divReportName.InnerHtml = "View Weekly Report";
    }

    private void RenderReport( string type)
    {
        try
        {
            
            gvReport.Visible = false;
            btnExport1.Visible = false;
            btnExport2.Visible = false;
            DataTable dtObjReportDetails = new DataTable();
            if (type == "Daily")
            {
                divDailyFilter.Visible = true;
                divWeeklyFilter.Visible = false;
                dtObjReportDetails = BindGridDaily("CRM_RPT_GetAllComplaintByDate");
            }            
            ViewState["ObjReportDetails"] = dtObjReportDetails;
            
            gvReport.DataSource = dtObjReportDetails;
            gvReport.DataBind();
            if (dtObjReportDetails.Rows.Count > 0)
            {
                gvReport.Visible = true;
                btnExport1.Visible = true;
                btnExport2.Visible = true;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

   /* public DataTable BindGridWeekly(string SP)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@Year", ddlYear.SelectedItem.Value.ToString());
            cmd.Parameters.Add("@WKNO", ddlWeek.SelectedItem.Value.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }*/

    public DataTable BindGridDaily(string SP)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@FromDt", txtFromDate.Text.ToString());
            cmd.Parameters.Add("@ToDt", txtToDate.Text.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //here session value is stored before bind data on gridview
        DataTable dt = ViewState["ObjReportDetails"] as DataTable;
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=Report_" + CommonClass.GetDateTimeIST().ToShortDateString() + ".xls");
        Response.ContentType = "application/ms-excel";
        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");

        int i;
        string Content = "";
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Content = "";
                Content = dr[i].ToString();
                Content = Content.Replace("\r\n", " ");
                Content = Content.Replace("  ", " ");
                Response.Write(tab + Content);
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    protected void btnDailyReport_Click(object sender, EventArgs e)
    {
        RenderReport("Daily");
        divReportName.InnerHtml = "View Report - Datewise";

    }

    protected void btnWeeklyReport_Click(object sender, EventArgs e)
    {
        RenderReport("Weekly");
        divReportName.InnerHtml = "View Weekly Report";
    }

    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["ObjReportDetails"];

        gvReport.DataSource = dt;
        gvReport.DataBind();
    }
}