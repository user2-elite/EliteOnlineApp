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

public partial class CRMDashboards : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserID"] == null)
        {
            Response.Redirect("default.aspx");
        }

        if (Session["selectedRole"] != null)
        {
            if (Session["selectedRole"].ToString() != "3" && Session["selectedRole"].ToString() != "1")
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
            //divReportName.InnerHtml = "Complaint data - Unitwise. (Enter the complaint register time period below:)";
            divReportName.Visible = false;
            ResetLinkColor();
            lnkUnitWise.CssClass = "KBAddSelected";
            RenderReport("1");
        }
    }

    private void bindyearMonth()
    {
        divPeriod1.Visible = true;
        divPeriod2.Visible = false;
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

    private void bindTrendReportFields()
    {
        ddlTYear.Items.Clear();
        ListItem item2;
        for (int j = 2015; j <= DateTime.Now.Year + 1; j++)
        {
            item2 = new ListItem(j.ToString(), j.ToString());
            ddlTYear.Items.Add(item2);
        }
        ddlTYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;

        if (ddComplaintForwardedTO2.Items.Count == 0)
        {
            BindUnit(ddComplaintForwardedTO2);
        }
        if (ddComplaintTypes.Items.Count == 0)
        {
            BindComplaintTypes(ddComplaintTypes);
        }

    }

    protected void lnkUnitWise_Click(object sender, EventArgs e)
    {
        compLabel.Visible = false;
        ddComplaintForwardedTO.Visible = false;
        req1.Visible = false;
        bindyearMonth();
        ViewState["type"] = "1";
        //divReportName.InnerHtml = "Complaint data - Unitwise. (Enter the complaint register time period below:)";
        divReportName.Visible = false;
        ResetLinkColor();
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
        //divReportName.InnerHtml = "Complaint Typewise. (Enter the complaint register time period below:)";
        divReportName.Visible = false;
        ResetLinkColor();
        lnkCompTypeWise.CssClass = "KBAddSelected";
    }

    protected void lnkComplaintsStatus_Click(object sender, EventArgs e)
    {
        ViewState["type"] = "3";
        bindyearMonth();
        if (ddComplaintForwardedTO.Items.Count == 0)
        {
            BindUnit(ddComplaintForwardedTO);
        }
        RenderReport("3");
        //divReportName.InnerHtml = "Complaint Status Details. (Enter the complaint register time period and Company Unit below:)";
        divReportName.Visible = false;
        ResetLinkColor();
        lnkComplaintsStatus.CssClass = "KBAddSelected";
        compLabel.Visible = true;
        ddComplaintForwardedTO.Visible = true;
        req1.Visible = true;
    }

    protected void lnkComplaintsTrends_Click(object sender, EventArgs e)
    {
        divPeriod1.Visible = false;
        divPeriod2.Visible = true;
        ViewState["type"] = "4";
        bindTrendReportFields();
        RenderReport("4");
        //divReportName.InnerHtml = "Trend Analysis Report. (Enter the complaint register Year, Company Unit and Complaint type below:)";
        divReportName.Visible = false;
        ResetLinkColor();
        lnkComplaintsTrends.CssClass = "KBAddSelected";
    }

    protected void lnkPivot_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRMPivot.aspx");
    }
    
    private void ResetLinkColor()
    {
        lnkUnitWise.CssClass = "KBAdd";
        lnkCompTypeWise.CssClass = "KBAdd";
        lnkComplaintsStatus.CssClass = "KBAdd";
        lnkComplaintsTrends.CssClass = "KBAdd";
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
                cTrendChart.Visible = false;
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

                cDashboard.Series["Series1"].LabelBackColor = System.Drawing.Color.White;
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
                //cDashboard.Series["Series1"].Color = System.Drawing.Color.Tomato;
                SetColor();

                //------------------------------------------------------------------------------------------------------------------------------------------------------


                cColChart.Titles.Add("Complaints :- Below dshboard shows the lists of complaints received under major and minor categories between the selected time period.");
                cColChart.Series[0].ChartType = SeriesChartType.Column;
                cColChart.Series["Series1"].XValueMember = "UnitName";
                cColChart.Series["Series1"].YValueMembers = "Major";
                //cColChart.Series["Series1"].Label = "Major";
                //cColChart.Series["Series1"].Label = "\nMajor(#VALY)\n\n";
                cColChart.Series["Series1"].LabelForeColor = System.Drawing.Color.White;
                cColChart.Series["Series1"].LabelBackColor = System.Drawing.Color.Red;
                cColChart.Series["Series1"].LabelBorderDashStyle = ChartDashStyle.DashDotDot;

                cColChart.Series["Series2"].XValueMember = "UnitName";
                cColChart.Series["Series2"].YValueMembers = "Minor";
                //cColChart.Series["Series2"].Label = "Minor";
                //cColChart.Series["Series2"].Label = "\nMinor(#VALY)\n\n";
                cColChart.Series["Series2"].LabelForeColor = System.Drawing.Color.Black;
                cColChart.Series["Series2"].LabelBackColor = System.Drawing.Color.Yellow;
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
                cColChart.DataSource = dsObjReportDetails.Tables[1];
                cColChart.DataBind();

            }
            else if (type == "2")
            {
                cColChart.Visible = false;
                cDashboard.Visible = true;
                tblChart.Visible = true;
                cTrendChart.Visible = false;
                cDashboard.Width = 1000;


                DataSet dsObjReportDetails = BindChartandPie("RPT_DashBoadComplaintTypeWiseData", stDate, enDate);
                cDashboard.Series[0].ChartType = SeriesChartType.Pie;

                cDashboard.Titles.Add("Complaint data:- Below chart shows the number of complaints(Both Major and Minor) received under each complaint type for the selected time period. (Note: Pie chart shows only top 10 among the list)");
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
                cDashboard.Series["Series1"].LabelBackColor = System.Drawing.Color.White;
                Legend legend = new Legend("ComplaintTypes");
                cDashboard.Legends.Add(legend);


                cDashboard.DataSource = dsObjReportDetails.Tables[0];
                cDashboard.DataBind();
                //cDashboard.Series["Series1"].Color = System.Drawing.Color.Tomato;

                if (dsObjReportDetails.Tables[0].Rows.Count > 0)
                {
                    SetColorForComplaintTypes(dsObjReportDetails.Tables[0]);
                }

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
                cTrendChart.Visible = false;
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
                    cDashboard.Series["Series1"].LabelBackColor = System.Drawing.Color.Yellow;
                    //cDashboard.Titles.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cDashboard.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                    cDashboard.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                    cDashboard.ChartAreas[0].AxisX.LabelStyle.Interval = 1;

                    cDashboard.DataSource = dtObjReportDetails;
                    cDashboard.DataBind();
                    cDashboard.Series["Series1"].Color = System.Drawing.Color.Red;
                }
            }
            else if (type == "4")
            {
                cColChart.Visible = false;
                cDashboard.Visible = false;
                tblChart.Visible = false;
                cTrendChart.Visible = true;
                cTrendChart.Width = 1200;
                if (ddComplaintForwardedTO2.SelectedIndex  != 0 && ddComplaintTypes.SelectedIndex != 0)
                {
                    dtObjReportDetails = BindTrendReport("RPT_DashBoadComplaintTypeWise_Trend", ddlTYear.SelectedItem.Value.ToString(), ddComplaintForwardedTO2.SelectedItem.Value.ToString(), ddComplaintTypes.SelectedItem.Value.ToString());
                    cTrendChart.Series[0].ChartType = SeriesChartType.Line;
                    cTrendChart.Titles.Add("Trend Analysis:- Below chart shows the monthwise complaint trend (Both Major and Minor) under the selected company unit, Complaint type and year.");
                    cTrendChart.Series["Series1"].XValueMember = "MONTHNAME";
                    cTrendChart.Series["Series1"].YValueMembers = "ComplaintCount";
                    //cTrendChart.Series["Series1"].AxisLabel = "MONTHNAME";

                    cTrendChart.ChartAreas[0].AxisX.Title = "-- Month --";
                    cTrendChart.ChartAreas[0].AxisY.Title = "-- No of Complaints ---";
                    cTrendChart.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cTrendChart.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cTrendChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cTrendChart.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Black;

                    cTrendChart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Calibiri", 12f, System.Drawing.FontStyle.Bold);
                    cTrendChart.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Black;


                    cTrendChart.Series["Series1"].LabelBackColor = System.Drawing.Color.Yellow;
                    cTrendChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Black;
                    cTrendChart.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
                    cTrendChart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;

                    cTrendChart.DataSource = dtObjReportDetails;
                    cTrendChart.DataBind();
                    cTrendChart.Series["Series1"].Color = System.Drawing.Color.Red;

                    //******************************
                    Legend SeriesSymbolLegend = new Legend("SeriesSymbolLegend");
                    SeriesSymbolLegend.Docking = Docking.Bottom;
                    SeriesSymbolLegend.Alignment = System.Drawing.StringAlignment.Center;
                    SeriesSymbolLegend.IsEquallySpacedItems = true;
                    SeriesSymbolLegend.IsTextAutoFit = true;
                    cTrendChart.Legends.Add(SeriesSymbolLegend);
                }
                else
                {
                    cTrendChart.Titles.Add("Trend Analysis:- Please choose Year, Company Unit and Complaint Type to proceed.");
                }
                //******************************
                //ComplaintCount
                //MONTH
            }
            else
            {
                cDashboard.Titles.Add("Complaint data:- Please choose company Unit and proceed.");
                cDashboard.AlternateText = "Please select the Unit Name to view the report";
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
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
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
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
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@stDate", stDate.ToString());
            cmd.Parameters.Add("@enDate", enDate.ToString());
            cmd.Parameters.Add("@UnitID", unit.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }

    public DataTable BindTrendReport(string SP, string TYear, string Unit, string Type)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.Parameters.Add("@Year", TYear.ToString());
            cmd.Parameters.Add("@ComplaintTypes", Type.ToString());
            cmd.Parameters.Add("@UnitID", Unit.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
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

    public void SetColor()
    {
        Color[] myPalette = new Color[10]{
        Color.FromKnownColor(KnownColor.BlueViolet), 
        Color.FromKnownColor(KnownColor.Violet),
        Color.FromKnownColor(KnownColor.Chocolate), 
        Color.FromKnownColor(KnownColor.Yellow),
        Color.FromKnownColor(KnownColor.Orange),
        Color.FromKnownColor(KnownColor.Pink),
        Color.FromKnownColor(KnownColor.BurlyWood),
        Color.FromKnownColor(KnownColor.Crimson),
        Color.FromKnownColor(KnownColor.Brown),
        Color.FromKnownColor(KnownColor.Purple)
        /*
        Color.FromKnownColor(KnownColor.White),
        Color.FromKnownColor(KnownColor.Red),
        Color.FromKnownColor(KnownColor.Green),
        Color.FromKnownColor(KnownColor.Teal),
        Color.FromKnownColor(KnownColor.ForestGreen),
        Color.FromKnownColor(KnownColor.Red),
        Color.FromKnownColor(KnownColor.Black),
        Color.FromKnownColor(KnownColor.Beige),*/
        };
        this.cDashboard.Palette = ChartColorPalette.None;
        this.cDashboard.PaletteCustomColors = myPalette;
    }


    public void SetColorForComplaintTypes(DataTable tbl1)
    {
        if (tbl1.Rows.Count > 0)
        {

            if (tbl1.Rows.Count == 1)
            {
                Color[] myPalette = new Color[1]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()),     
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 2)
            {
                Color[] myPalette = new Color[2]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()),      
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 3)
            {
                Color[] myPalette = new Color[3]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()),      
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 4)
            {
                Color[] myPalette = new Color[4]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()),     
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 5)
            {
                Color[] myPalette = new Color[5]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[4]["ColorCode"].ToString()),      
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 6)
            {
                Color[] myPalette = new Color[6]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[4]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[5]["ColorCode"].ToString()),     
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 7)
            {
                Color[] myPalette = new Color[7]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[4]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[5]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[6]["ColorCode"].ToString()),      
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 8)
            {
                Color[] myPalette = new Color[8]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[4]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[5]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[6]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[7]["ColorCode"].ToString()),     
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 9)
            {
                Color[] myPalette = new Color[9]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[4]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[5]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[6]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[7]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[8]["ColorCode"].ToString()),      
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }
            else if (tbl1.Rows.Count == 10)
            {
                Color[] myPalette = new Color[10]{ 
            Color.FromName(tbl1.Rows[0]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[1]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[2]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[3]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[4]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[5]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[6]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[7]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[8]["ColorCode"].ToString()), 
            Color.FromName(tbl1.Rows[9]["ColorCode"].ToString()),        
                };
                this.cDashboard.Palette = ChartColorPalette.None;
                this.cDashboard.PaletteCustomColors = myPalette;
            }


        }
    }

    private void BindUnit(DropDownList ddlUnit)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
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
            ddlUnit.DataValueField = "UnitID";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, Listitem0);
        }
        cmd.Parameters.Clear();
        cmd.Dispose();
        con.Close();
    }

    public void BindComplaintTypes(DropDownList ddlComplaintTypes)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
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
    private void ShowLineChart()
    {
        // [RPT_DashBoadComplaintTypeWise_Trend]   --'2015','5','10'         
        //@Year INT
        //@ComplaintTypes   INT
        //@UnitID   INT = null     
    }

}