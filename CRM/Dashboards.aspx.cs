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

public partial class Dashboards : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CRMUserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            if (Session["CRMselectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }

        if (!IsPostBack)
        {
            compLabel.Visible = false;
            ddComplaintForwardedTO.Visible = false;
            req1.Visible = false;
            bindyearMonth();
            ViewState["type"] = "1";
            divReportName.InnerHtml = "Complaint data - Unitwise. (Enter the complaint register time period below:)";
            ResetColor();
            lnkUnitWise.CssClass = "KBAddSelected";
            RenderReport("1");
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

        ddlSYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
        ddlEYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }

    protected void lnkUnitWise_Click(object sender, EventArgs e)
    {
        compLabel.Visible = false;
        ddComplaintForwardedTO.Visible = false;
        req1.Visible = false;
        bindyearMonth();
        ViewState["type"] = "1";
        divReportName.InnerHtml = "Complaint data - Unitwise. (Enter the complaint register time period below:)";
        ResetColor();
        lnkUnitWise.CssClass = "KBAddSelected";
        RenderReport("1");
    }

    protected void lnkCompTypeWise_Click(object sender, EventArgs e)
    {
        compLabel.Visible = false;
        ddComplaintForwardedTO.Visible = false;
        req1.Visible = false;
        ViewState["type"] = "2";
        bindyearMonth();
        RenderReport("2");
        divReportName.InnerHtml = "Complaint Typewise. (Enter the complaint register time period below:)";
        ResetColor();
        lnkCompTypeWise.CssClass = "KBAddSelected";
    }

    protected void lnkComplaintsStatus_Click(object sender, EventArgs e)
    {
        ViewState["type"] = "3";
        bindyearMonth();
        RenderReport("3");
        divReportName.InnerHtml = "Complaint Status Details. (Enter the complaint register time period and Company Unit below:)";
        ResetColor();
        lnkComplaintsStatus.CssClass = "KBAddSelected";
        compLabel.Visible = true;
        ddComplaintForwardedTO.Visible = true;
        req1.Visible = true;
        if (ddComplaintForwardedTO.Items.Count == 0)
        {
            MastData objMastData = new MastData();
            objMastData.BindUnit(ddComplaintForwardedTO);
        }
    }

    private void ResetColor()
    {
        lnkUnitWise.CssClass = "KBAdd";
        lnkCompTypeWise.CssClass = "KBAdd";
        lnkComplaintsStatus.CssClass = "KBAdd";
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
            if (type == "1")
            {
                cColChart.Visible = true;
                cDashboard.Visible = true;
                tblChart.Visible = false;
                cColChart.Width = 1100; 
                cDashboard.Width = 500;


                DataSet dsObjReportDetails = BindChartandPie("RPT_DashBoadUnitWiseData", stDate, enDate);
                cDashboard.Series[0].ChartType = SeriesChartType.Pie;
                ////DataIte
                //DataPoint point = new DataPoint();
                //point.YValues = 30;
                //point.AxisLabel = "Test 123";
                //cDashboard.Series[0].Points.Add(point);
                cDashboard.Titles.Add("Complaint data:- Below chart shows the total complaints(Both Major and Minor) received under the selected company unit and the time period.");
                cDashboard.Series["Series1"].XValueMember = "UnitName";
                cDashboard.Series["Series1"].YValueMembers = "ComplaintCount";
                cDashboard.Series["Series1"].AxisLabel = "ComplaintCount";
                cDashboard.Series["Series1"].IsVisibleInLegend = true;
  
                //cDashboard.Series["Series1"].AxisLabel = "ResolverName";
                //cDashboard.Series["Series1"].SmartLabelStyle.MovingDirection = LabelAlignmentStyles.BottomLeft;
                //cDashboard.Series["Series1"].SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                cDashboard.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;

                //cDashboard.Titles.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                cDashboard.ChartAreas[0].AxisX.LabelStyle.Angle = -90;


                Legend legend = new Legend("UnitName");
                cDashboard.Legends.Add(legend);


                cDashboard.DataSource = dsObjReportDetails.Tables[0];
                cDashboard.DataBind();
                cDashboard.Series["Series1"].Color = System.Drawing.Color.Tomato;

                //------------------------------------------------------------------------------------------------------------------------------------------------------


                cColChart.Titles.Add("Complaints :- Below dshboard shows the lists of complaints received under major and minor categories between the selected time period. Blue legend color reperesents Minor complaints and maroon represents Major complaints");
				cColChart.Series[0].ChartType = SeriesChartType.Column;
                cColChart.Series["Series1"].XValueMember = "UnitName";
                cColChart.Series["Series1"].YValueMembers = "Major";
                //cColChart.Series["Series1"].Label = "Major";
                //cColChart.Series["Series1"].Label = "\nMajor(#VALY)\n\n";
                cColChart.Series["Series1"].LabelForeColor = System.Drawing.Color.White;
                cColChart.Series["Series1"].LabelBackColor = System.Drawing.Color.Maroon;
                cColChart.Series["Series1"].LabelBorderDashStyle = ChartDashStyle.DashDotDot;

                cColChart.Series["Series2"].XValueMember = "UnitName";
                cColChart.Series["Series2"].YValueMembers = "Minor";
                //cColChart.Series["Series2"].Label = "Minor";
                //cColChart.Series["Series2"].Label = "\nMinor(#VALY)\n\n";
                cColChart.Series["Series2"].LabelForeColor = System.Drawing.Color.White;
                cColChart.Series["Series2"].LabelBackColor = System.Drawing.Color.Blue;
                cColChart.Series["Series2"].LabelBorderDashStyle = ChartDashStyle.DashDotDot;

                cColChart.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cColChart.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cColChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cColChart.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;

                //cColChart.Titles.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cColChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                //cColChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
				cColChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                cColChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                cColChart.DataSource = dsObjReportDetails.Tables[1];;
                cColChart.DataBind();

            }
            else if (type == "2")
            {
                cColChart.Visible = false;
                cDashboard.Visible = true;
                tblChart.Visible = true;
                cDashboard.Width = 1000;


                DataSet dsObjReportDetails = BindChartandPie("RPT_DashBoadComplaintTypeWiseData", stDate, enDate);
                cDashboard.Series[0].ChartType = SeriesChartType.Pie;

                cDashboard.Titles.Add("Complaint data:- Below chart shows the number of complaints(Both Major and Minor) received under each complaint type for the selected time period. (Note: Pie chart shows top 10 among the list)");
                cDashboard.Series["Series1"].XValueMember = "ComplaintTypes";
                cDashboard.Series["Series1"].YValueMembers = "ComplaintCount";
                cDashboard.Series["Series1"].AxisLabel = "ComplaintCount";
                cDashboard.Series["Series1"].IsVisibleInLegend = true;

                cDashboard.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                cDashboard.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;

                cDashboard.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                cDashboard.ChartAreas[0].AxisX.LabelStyle.Angle = -90;

                Legend legend = new Legend("ComplaintTypes");
                cDashboard.Legends.Add(legend);


                cDashboard.DataSource = dsObjReportDetails.Tables[0];
                cDashboard.DataBind();
                cDashboard.Series["Series1"].Color = System.Drawing.Color.Tomato;

                string tblData = "<table width='300px' cellpadding='3' cellspacing='3' align='left' border='1'>";
                tblData += "<TR bgcolor='#E0E0E0'><TD align='left' valign='top'><B>Complaint Type</B></TD><TD align='left' valign='top'><B>Complaints Count</B></TD></TR>";
                for (int i = 0; i < dsObjReportDetails.Tables[1].Rows.Count; i++)
                {
                    tblData += "<TR><TD align='left' valign='top' bgcolor='#FFFFFF'>" + dsObjReportDetails.Tables[1].Rows[i]["ComplaintTypes"].ToString() + "</TD>";
                    tblData += "<TD align='left' valign='top' bgcolor='#FFFFFF'>" + dsObjReportDetails.Tables[1].Rows[i]["ComplaintCount"].ToString() + "</TD></TR>";
                }
                tblData += "</table>";
                tblChart.InnerHtml = tblData;
            }
            else if (type == "3")
            {
                cColChart.Visible = false;
                cDashboard.Visible = true;
                tblChart.Visible = false;
                cDashboard.Width = 1200;
                if (ddComplaintForwardedTO.SelectedItem != null)
                {
                    dtObjReportDetails = BindChart_StatusDetails("RPT_DashBoadStatusWiseData", stDate, enDate, ddComplaintForwardedTO.SelectedItem.Value.ToString());
                    cDashboard.Series[0].ChartType = SeriesChartType.Column;
                    cDashboard.Titles.Add("Complaint data:- Below chart shows the complaints status (Both Major and Minor) under each company unit for the selected time period.");
                    cDashboard.Series["Series1"].XValueMember = "Status_Name";
                    cDashboard.Series["Series1"].YValueMembers = "ComplaintCount";
                    cDashboard.Series["Series1"].AxisLabel = "Status_Name";

                    //cDashboard.Series["Series1"].AxisLabel = "ResolverName";
                    //cDashboard.Series["Series1"].SmartLabelStyle.MovingDirection = LabelAlignmentStyles.BottomLeft;
                    //cDashboard.Series["Series1"].SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
                    cDashboard.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cDashboard.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cDashboard.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cDashboard.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;

                    //cDashboard.Titles.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cDashboard.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
					cDashboard.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
					cDashboard.ChartAreas[0].AxisX.LabelStyle.Interval= 1;
				
                    cDashboard.DataSource = dtObjReportDetails;
                    cDashboard.DataBind();
                    cDashboard.Series["Series1"].Color = System.Drawing.Color.Tomato;
                }
                else
                {
                    cDashboard.Titles.Add("Complaint data:- Please choose company Unit and proceed.");
                    cDashboard.AlternateText = "Please select the Unit Name to view the report";
                }
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

   
                     

  public DataSet BindChartandPie(string SP, string stDate, string enDate)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            SqlCommand cmd = new SqlCommand(SP, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();
            return ds;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public DataTable BindChart(string SP, string stDate, string enDate)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
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

    public DataTable BindChart_StatusDetails(string SP, string stDate, string enDate, string unit)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
            cmd.Parameters.Add("@UnitID", unit.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
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