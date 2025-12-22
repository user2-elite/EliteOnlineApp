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

public partial class _Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["selectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }
        
        if (!IsPostBack)
        {
            divPeriod.Visible = false;
            //bindyearMonth();
        }
    }

    private void bindyearMonth()
    {
        divPeriod.Visible = true;
        ListItem item1;

        ddlSYear.Items.Clear();
        ddlEYear.Items.Clear();
        ddlSMonth.Items.Clear();
        ddlEMonth.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            item1 = new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), i.ToString());
            ddlSMonth.Items.Add(item1);
            ddlEMonth.Items.Add(item1);
        }     

        ListItem item2;
        for (int j = 2015; j <= DateTime.Now.Year + 1; j++)
        {
            item2 = new ListItem(j.ToString(), j.ToString());
            ddlSYear.Items.Add(item2);
            ddlEYear.Items.Add(item2);
        }
        
        ddlSMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;
        ddlEMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

        ddlSYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected  = true;
        ddlEYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }

    protected void lnkClosedRequests_Click(object sender, EventArgs e)
    {
        ViewState["type"] = "cl";
        bindyearMonth();
        RenderReport("cl");
        divReportName.InnerHtml = "Request Status. (Enter the time period below:)";
        lnkClosedRequests.Style.Add("background-color", "Wheat");
        lnkUtilization.Style.Add("background-color", "#FFFFFF");
    }

    protected void lnkUtilization_Click(object sender, EventArgs e)
    {
        ViewState["type"] = "ut";
        bindyearMonth();
        RenderReport("ut");
        divReportName.InnerHtml = "Utilization Summary. (Enter the Request closed time period below:";
        lnkUtilization.Style.Add("background-color", "Wheat");
        lnkClosedRequests.Style.Add("background-color", "#FFFFFF");
    }

    private void RenderReport(string type)
    {
        try
        {
            string stMonth = ddlSMonth.SelectedItem.Value.ToString();
            string enMonth = ddlEMonth.SelectedItem.Value.ToString();

            string stYear = ddlSYear.SelectedItem.Value.ToString();
            string enYear = ddlEYear.SelectedItem.Value.ToString();

            string stDate, enDate;
            stDate = stMonth + "-01-" + stYear;

            if (enMonth == "12")
            {
                enDate = "01-01-" + (System.Convert.ToInt32(enYear) + 1).ToString();
            }
            else
            {
                enDate = (System.Convert.ToInt32(enMonth) + 1).ToString() + "-01-" + enYear;
            }

            DataTable dtObjReportDetails = new DataTable();
            if (type == "cl")
            {
                dtObjReportDetails = BindClosedRequests("RPT_DashBoadClosedRequests", stDate, enDate);
                //cDashboardsCharts.Series[0].ChartType = "Pie";
                ////DataIte
                //DataPoint point = new DataPoint();
                //point.YValues = 30;
                //point.AxisLabel = "Test 123";
                //cDashboardsCharts.Series[0].Points.Add(point);
                cDashboardsCharts.Titles.Add("Request Status :- Below dshboard shows the lists of all active resolvers,Total requests created between the selected time period and is pending (blue), Total requests closed between the selected time period (green), Total requests violated between the selected time period (red)");
                cDashboardsCharts.Series["Series1"].XValueMember = "ResolverName";
                cDashboardsCharts.Series["Series1"].YValueMembers = "Closed";
                //cDashboardsCharts.Series["Series1"].Label = "Closed Count";
                //cDashboardsCharts.Series["Series1"].Label = "\nClosed(#VALY)\n\n";
                cDashboardsCharts.Series["Series1"].LabelForeColor = System.Drawing.Color.White;
                cDashboardsCharts.Series["Series1"].LabelBackColor = System.Drawing.Color.Green;
                cDashboardsCharts.Series["Series1"].LabelBorderDashStyle = ChartDashStyle.DashDotDot;

                cDashboardsCharts.Series["Series2"].XValueMember = "ResolverName";
                cDashboardsCharts.Series["Series2"].YValueMembers = "Pending";
                //cDashboardsCharts.Series["Series2"].Label = "Pending Count";
                //cDashboardsCharts.Series["Series2"].Label = "\nPending(#VALY)\n\n";
                cDashboardsCharts.Series["Series2"].LabelForeColor = System.Drawing.Color.White;
                cDashboardsCharts.Series["Series2"].LabelBackColor = System.Drawing.Color.Blue;
                cDashboardsCharts.Series["Series2"].LabelBorderDashStyle = ChartDashStyle.DashDotDot;



                cDashboardsCharts.Series["Series3"].XValueMember = "ResolverName";
                cDashboardsCharts.Series["Series3"].YValueMembers = "Violated";
                //cDashboardsCharts.Series["Series3"].Label = "\nViolated(#VALY)\n\n";
                cDashboardsCharts.Series["Series3"].LabelForeColor = System.Drawing.Color.White;
                cDashboardsCharts.Series["Series3"].LabelBackColor = System.Drawing.Color.OrangeRed;
                cDashboardsCharts.Series["Series3"].LabelBorderDashStyle = ChartDashStyle.DashDotDot;

                //cDashboardsCharts.Series["Series1"].AxisLabel = "ResolverName";
                //cDashboardsCharts.Series["Series1"].SmartLabelStyle.MovingDirection = LabelAlignmentStyles.BottomLeft;
                //cDashboardsCharts.Series["Series1"].SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                cDashboardsCharts.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f,  System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;

                //cDashboardsCharts.Titles.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                cDashboardsCharts.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
				cDashboardsCharts.ChartAreas[0].AxisX.LabelStyle.Interval=1;
                cDashboardsCharts.DataSource = dtObjReportDetails;
                cDashboardsCharts.DataBind();
            }
            else if (type == "ut")
            {
                dtObjReportDetails = BindUtilization("RPT_DashBoadUtilization", stDate, enDate);
                cDashboardsCharts.Titles.Add("Utilization Summary :- Below dshboard shows the lists of active resolvers with total time spent (In Hours) on resolving the tickets for the selected time period. ");
                cDashboardsCharts.Series["Series1"].XValueMember = "ResolverName";
                cDashboardsCharts.Series["Series1"].YValueMembers = "TotalTimeSpend";
                cDashboardsCharts.Series["Series1"].AxisLabel = "ResolverName";

                cDashboardsCharts.Series["Series1"].SmartLabelStyle.MovingDirection = LabelAlignmentStyles.BottomLeft;
                cDashboardsCharts.Series["Series1"].SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                cDashboardsCharts.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);                

                //cDashboardsCharts.Titles.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboardsCharts.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                cDashboardsCharts.ChartAreas[0].AxisX.LabelStyle.Angle = 90;
                cDashboardsCharts.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                cDashboardsCharts.DataSource = dtObjReportDetails;
                cDashboardsCharts.DataBind();
                cDashboardsCharts.Series["Series1"].Color = System.Drawing.Color.LightGreen;
            }
            //ViewState["ObjReportDetails"] = dtObjReportDetails;
            //if (dtObjReportDetails.Rows.Count > 0)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }

    }

    public DataTable BindUtilization(string SP, string stDate, string enDate)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
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

    public DataTable BindClosedRequests(string SP, string stDate, string enDate)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        RenderReport(ViewState["type"].ToString());
    }

}