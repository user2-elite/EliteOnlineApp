using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;
using System.Drawing;

public partial class HRDashboards : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        emptyRecords1.Visible = false;
        trReport2.Visible = false;
        divError.InnerHtml = "";
        if (Session["UserID"] == null)
        {
            Response.Redirect("default.aspx");
        }

        if (Session["selectedRole"] != null)
        {
            if (Session["selectedRole"].ToString() != "5" && Session["selectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }

        if (!IsPostBack)
        {
            bindyearMonth();
            ViewState["type"] = "1";
            divReportName.Visible = true;
            ResetLinkColor();
            lnkLateComingDaily.CssClass = "KBAddSelected";
            showFilters(ViewState["type"].ToString());
            RenderReport("1");
            divReportName.InnerHtml = "Late coming report - Current date";
            divInfo.InnerHtml = "Late coming - daily: In-time between 9:10 and 10:00 will be displayed here";
            reportCaption1.InnerHtml = "";
            reportCaption2.InnerHtml = "";
        }
    }

    private void bindyearMonth()
    {
        ListItem item1;

        ddlSYear.Items.Clear();
        ddlSMonth.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            item1 = new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), i.ToString());
            ddlSMonth.Items.Add(item1);
        }

        ListItem item2;
        for (int j = 2018; j <= DateTime.Now.Year; j++)
        {
            item2 = new ListItem(j.ToString(), j.ToString());
            ddlSYear.Items.Add(item2);
        }

        ddlSMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

        ddlSYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }

    private void ResetLinkColor()
    {
        lnkAttendence.CssClass = "KBAdd";
        lnkLateComingDaily.CssClass = "KBAdd";
        lnkLateComingMonthly.CssClass = "KBAdd";
        lnkLeaveWithoutInform.CssClass = "KBAdd";
        lnkLeaveRequests.CssClass = "KBAdd";
        lnkODRequests.CssClass = "KBAdd";
        lnkODDailyView.CssClass = "KBAdd";
    }

    private void RenderReport(string type)
    {

        try
        {
            if (type != "1" && type != "0" && type != "3" && type != "8")
            {
                string stMonth = ddlSMonth.SelectedItem.Value.ToString();
                string stYear = ddlSYear.SelectedItem.Value.ToString();
                string stDate;
                stDate = stMonth + "-01-" + stYear;
                BindReportData(type);
            }
            else
            {
                BindReportData(type);
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
    private void showFilters(string type)
    {
        btnShowReportSAP.Visible = false;
        try
        {
            if (type != "1" && type != "0" && type != "3" && type != "8")
            {
                divPeriod1.Visible = true;
                divPeriod2.Visible = false;
                divPeriod3.Visible = false;
                if (type == "4" || type == "5")
                {
                    btnShowReportSAP.Visible = true;
                }
            }
            else
            {
                divPeriod1.Visible = false;
                divPeriod2.Visible = true;
                divPeriod3.Visible = false;
            }

        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        RenderReport(ViewState["type"].ToString());
    }

    protected void btnShowReportSAP_Click(object sender, EventArgs e)
    {
        if (ViewState["type"].ToString() == "4")
        {
            RenderReport("6");

        }
        else if (ViewState["type"].ToString() == "5")
        {
            RenderReport("7");
        }
        else
        {
            RenderReport(ViewState["type"].ToString());
        }
    }
    
    protected void lnkAttendence_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Daily attendence details. In-time of each employee will be displayed here.";
        grid1.Visible = false;
        ViewState["type"] = "0";
        ResetLinkColor();
        lnkAttendence.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "Attendence Report";
        showFilters(ViewState["type"].ToString());
        RenderReport(ViewState["type"].ToString());
    }

    protected void lnkLateComingDaily_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Late coming - daily: In-time between 9:10 and 10:00 will be displayed here.";
        grid1.Visible = false;
        ViewState["type"] = "1";
        ResetLinkColor();
        lnkLateComingDaily.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "Late coming report - Current date";
        showFilters(ViewState["type"].ToString());
        RenderReport(ViewState["type"].ToString());
    }

    protected void lnkLateComingMonthly_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Late coming - monthly: In-time between 9:10 and 10:00 will be displayed here. Click on 'show report' to proceed";
        grid1.Visible = false;
        ViewState["type"] = "2";
        ResetLinkColor();
        lnkLateComingMonthly.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "Late coming report - Monthly";
        showFilters(ViewState["type"].ToString());
    }

    protected void lnkLeaveWithoutInform_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Employee not created Leave request, OD request and not used access card for the day. Punching data will be taken as of yesterday data.";
        grid1.Visible = false;
        ViewState["type"] = "3";
        ResetLinkColor();
        lnkLeaveWithoutInform.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "Leave Without Inform";
        showFilters(ViewState["type"].ToString());
    }

    protected void lnkLeaveRequests_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Employee Leave request details. Click on 'show report' to proceed";
        grid1.Visible = false;
        ViewState["type"] = "4";
        ResetLinkColor();
        lnkLeaveRequests.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "Leave Requests";
        showFilters(ViewState["type"].ToString());
    }

    protected void lnkODRequests_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Employee OD request details. Click on 'show report' to proceed";
        grid1.Visible = false;
        ViewState["type"] = "5";
        ResetLinkColor();
        lnkODRequests.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "On Duty Requests";
        showFilters(ViewState["type"].ToString());
    }

    protected void lnkODDailyView_Click(object sender, EventArgs e)
    {
        divInfo.InnerHtml = "Employee Leave/OD request - Daily Report. Click on 'show report' to proceed";
        grid1.Visible = false;
        ViewState["type"] = "8";
        ResetLinkColor();
        lnkODDailyView.CssClass = "KBAddSelected";
        divReportName.InnerHtml = "Leave/OD - Daily Report";
        showFilters(ViewState["type"].ToString());
    }
    

    private void BindReportData(string type)
    {
        ViewState["ObjDetails1"] = null;
        ViewState["ObjDetails2"] = null;
        try
        {
            grid1.Visible = false;
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Enterprise"].ToString());
            DataTable dt = new DataTable();
            string ReportSPName = "";
            if (type == "0")
            {
                ReportSPName = "OD_GetAttendenceReportToday";
            }
            else if (type == "1")
            {
                ReportSPName = "OD_GetLateComingReportToday";
            }
            else if (type == "2")
            {
                ReportSPName = "OD_GetLateComingReportMonthly";
            }
            else if (type == "4")
            {
                ReportSPName = "Leave_GetRequestReport";
            }
            else if (type == "5")
            {
                ReportSPName = "OD_GetRequestReport";
            }
            else if (type == "6")
            {
                ReportSPName = "Leave_GetRequestReport_ForSAP";
            }
            else if (type == "7")
            {
                ReportSPName = "OD_GetRequestReport_ForSAP";
            }
            else if (type == "8") { 
                trReport2.Visible = true;
                reportCaption1.InnerHtml = "Band M1 and above";
                reportCaption2.InnerHtml = "Band below M1";                
                ReportSPName = "OD_GetRequestReportDaily";
            }

            SqlCommand cmd = new SqlCommand(ReportSPName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (type != "1" && type != "0" && type != "3" && type != "8")
            {
                cmd.Parameters.AddWithValue("@month", ddlSMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@year", ddlSYear.SelectedValue);
            }
            else
            {
                cmd.Parameters.AddWithValue("@date", TextBox1.Text);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                grid1.DataSource = dt;
                grid1.DataBind();
                grid1.Visible = true;
                ViewState["ObjDetails1"] = dt;
            }
            else
            {
                grid1.Visible = false;
                emptyRecords1.Visible = true;
            }

            if (type == "8") {
                DataTable dt2 = new DataTable();
                cmd.Parameters.AddWithValue("@BandType", "BelowM1");
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                da2.Fill(dt2);
                if(dt2.Rows.Count > 0) {
                    grid2.DataSource = dt2;
                    grid2.DataBind();
                    grid2.Visible = true;
                    ViewState["ObjDetails2"] = dt2;
                } else {
                    grid2.Visible = false;
                    emptyRecords2.Visible = true;
                }
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
        }
        catch
        {
            divError.InnerHtml = "Error While loading data. Please try later.";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["ObjDetails1"] != null)
            {
                //here session value is stored before bind data on gridview
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=HRReport_" + CommonClass.GetDateTimeIST().ToShortDateString() + ".xls");
                Response.ContentType = "application/ms-excel";
                if (ViewState["ObjDetails1"] != null && ViewState["ObjDetails2"] != null)
                {
                    DataTable dt1 = ViewState["ObjDetails1"] as DataTable;
                    Response.Write("\n");
                    Response.Write(reportCaption1.InnerHtml);
                    Response.Write("\n");
                    Response.Write("\n");
                    exportToExcel(dt1);
                    Response.Write("\n");
                    Response.Write("\n");
                    Response.Write("\n");

                    DataTable dt2 = ViewState["ObjDetails2"] as DataTable;
                    Response.Write(reportCaption2.InnerHtml);
                    Response.Write("\n");
                    Response.Write("\n");
                    exportToExcel(dt2);
                    Response.Write("\n");
                }
                else {
                    DataTable dt = ViewState["ObjDetails1"] as DataTable;
                    exportToExcel(dt);
                }
                Response.End();
            }
        }
        catch
        {

        }
    }

    private void exportToExcel(DataTable dt) {
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
    }

    protected void lnkQuotaView_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveQuotaView.aspx");
    }
}