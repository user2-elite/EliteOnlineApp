using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using HtmlPivot;
using System.Globalization;
using System.Data.SqlClient; //From the referenced Pivot.dll


public partial class CRMPivot : System.Web.UI.Page
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
            lnkTypeWise.CssClass = "KBAddSelected";
            bindyearMonth();
            ViewState["type"] = "1";
            ShowReports();
        }

    }

    private void bindyearMonth()
    {
        divPeriod1.Visible = true;
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReports();
    }

    private void ShowReports()
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

        DataTable dtData = new DataTable();

        if (ViewState["type"].ToString() == "1")
        {
            dtData = BindDefectCategoryWise(stDate, enDate);
        }
        else if (ViewState["type"].ToString() == "2")
        {
            dtData = BindAreaWise(stDate, enDate);
        }

        //Advanced Pivot
        //Pivot advPivot = new Pivot(DataTableForTesting);
        //HtmlTable advancedPivot = advPivot.PivotTable("Sales Person", "Product", new string[] { "Sale Amount", "Quantity" });
        //div1.Controls.Add(advancedPivot);

        //Simple Pivot
        Pivot pivot = new Pivot(dtData);
        //override default style with css
        pivot.CssTopHeading = "Heading";
        pivot.CssLeftColumn = "LeftColumn";
        pivot.CssItems = "Items";
        pivot.CssTotals = "Totals";
        pivot.CssTable = "Table";
        if (ViewState["type"].ToString() == "1")
        {
            HtmlTable simplePivot = pivot.PivotTable("Category", "UnitName", "DefectCount");
            div2.Controls.Add(simplePivot);
        }
        else if (ViewState["type"].ToString() == "2")
        {
            HtmlTable simplePivot = pivot.PivotTable("Category", "Area", "DefectCount");
            div2.Controls.Add(simplePivot);
        }   

    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CRMDashBoards.aspx");
    }

    protected void lnkTypeWise_Click(object sender, EventArgs e)
    {
        ViewState["type"] = "1";
        lnkTypeWise.CssClass = "KBAddSelected";
        lnkRegionWise.CssClass = "KBAdd";
        ShowReports();
    }

    protected void lnkRegionWise_Click(object sender, EventArgs e)
    {
        ViewState["type"] = "2";
        lnkTypeWise.CssClass = "KBAdd";
        lnkRegionWise.CssClass = "KBAddSelected";
        ShowReports();
    }
    
    public DataTable DataTableForTesting
    {
        get
        {
            DataTable dt = new DataTable("Sales Table1");
            dt.Columns.Add("UnitName");
            dt.Columns.Add("Category");
            dt.Columns.Add("DefectCount");
            dt.Columns.Add("Sale Amount");

            dt.Rows.Add(new object[] { "John", "Pens", 200, 350.00 });
            dt.Rows.Add(new object[] { "John", "Pencils", 400, 500.00 });
            dt.Rows.Add(new object[] { "John", "Notebooks", 100, 300.00 });
            dt.Rows.Add(new object[] { "John", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "John", "Calculators", 120, 1200.00 });
            dt.Rows.Add(new object[] { "John", "Back Packs", 75, 1500.00 });
            dt.Rows.Add(new object[] { "Jane", "Pens", 225, 393.75 });
            dt.Rows.Add(new object[] { "Jane", "Pencils", 335, 418.75 });
            dt.Rows.Add(new object[] { "Jane", "Notebooks", 200, 600.00 });
            dt.Rows.Add(new object[] { "Jane", "Rulers", 75, 150.00 });
            dt.Rows.Add(new object[] { "Jane", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "Jane", "Back Packs", 97, 1940.00 });
            dt.Rows.Add(new object[] { "Sally", "Pens", 202, 353.50 });
            dt.Rows.Add(new object[] { "Sally", "Pencils", 303, 378.75 });
            dt.Rows.Add(new object[] { "Sally", "Notebooks", 198, 600.00 });
            dt.Rows.Add(new object[] { "Sally", "Rulers", 98, 594.00 });
            dt.Rows.Add(new object[] { "Sally", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "Sally", "Back Packs", 101, 2020.00 });
            dt.Rows.Add(new object[] { "Sarah", "Pens", 112, 196.00 });
            dt.Rows.Add(new object[] { "Sarah", "Pencils", 245, 306.25 });
            dt.Rows.Add(new object[] { "Sarah", "Notebooks", 198, 594.00 });
            dt.Rows.Add(new object[] { "Sarah", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "Sarah", "Calculators", 66, 660.00 });
            dt.Rows.Add(new object[] { "Sarah", "Back Packs", 50, 2020.00 });
            dt.Rows.Add(new object[] { "John", "Pens", 200, 350.00 });
            dt.Rows.Add(new object[] { "John", "Pencils", 400, 500.00 });
            dt.Rows.Add(new object[] { "John", "Notebooks", 100, 300.00 });
            dt.Rows.Add(new object[] { "John", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "John", "Calculators", 120, 1200.00 });
            dt.Rows.Add(new object[] { "John", "Back Packs", 75, 1500.00 });
            dt.Rows.Add(new object[] { "Jane", "Pens", 225, 393.75 });
            dt.Rows.Add(new object[] { "Jane", "Pencils", 335, 418.75 });
            dt.Rows.Add(new object[] { "Jane", "Notebooks", 200, 600.00 });
            dt.Rows.Add(new object[] { "Jane", "Rulers", 75, 150.00 });
            dt.Rows.Add(new object[] { "Jane", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "Jane", "Back Packs", 97, 1940.00 });
            dt.Rows.Add(new object[] { "Sally", "Pens", 202, 353.50 });
            dt.Rows.Add(new object[] { "Sally", "Pencils", 303, 378.75 });
            dt.Rows.Add(new object[] { "Sally", "Notebooks", 198, 600.00 });
            dt.Rows.Add(new object[] { "Sally", "Rulers", 98, 594.00 });
            dt.Rows.Add(new object[] { "Sally", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "Sally", "Back Packs", 101, 2020.00 });
            dt.Rows.Add(new object[] { "Sarah", "Pens", 112, 196.00 });
            dt.Rows.Add(new object[] { "Sarah", "Pencils", 245, 306.25 });
            dt.Rows.Add(new object[] { "Sarah", "Notebooks", 198, 594.00 });
            dt.Rows.Add(new object[] { "Sarah", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "Sarah", "Calculators", 66, 660.00 });
            dt.Rows.Add(new object[] { "Sarah", "Back Packs", 50, 2020.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Pens", 200, 350.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Pencils", 400, 500.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Notebooks", 100, 300.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Calculators", 120, 1200.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Back Packs", 75, 1500.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Pens", 225, 393.75 });
            dt.Rows.Add(new object[] { "JAHANE", "Pencils", 335, 418.75 });
            dt.Rows.Add(new object[] { "JAHANE", "Notebooks", 200, 600.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Rulers", 75, 150.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Back Packs", 97, 1940.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Pens", 202, 353.50 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Pencils", 303, 378.75 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Notebooks", 198, 600.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Rulers", 98, 594.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Back Packs", 101, 2020.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Pens", 112, 196.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Pencils", 245, 306.25 });
            dt.Rows.Add(new object[] { "SHARRAH", "Notebooks", 198, 594.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Calculators", 66, 660.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Back Packs", 50, 2020.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Pens", 200, 350.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Pencils", 400, 500.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Notebooks", 100, 300.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Calculators", 120, 1200.00 });
            dt.Rows.Add(new object[] { "JAHAN", "Back Packs", 75, 1500.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Pens", 225, 393.75 });
            dt.Rows.Add(new object[] { "JAHANE", "Pencils", 335, 418.75 });
            dt.Rows.Add(new object[] { "JAHANE", "Notebooks", 200, 600.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Rulers", 75, 150.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "JAHANE", "Back Packs", 97, 1940.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Pens", 202, 353.50 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Pencils", 303, 378.75 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Notebooks", 198, 600.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Rulers", 98, 594.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "SSSAYYY", "Back Packs", 101, 2020.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Pens", 112, 196.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Pencils", 245, 306.25 });
            dt.Rows.Add(new object[] { "SHARRAH", "Notebooks", 198, 594.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Calculators", 66, 660.00 });
            dt.Rows.Add(new object[] { "SHARRAH", "Back Packs", 50, 2020.00 });
            dt.Rows.Add(new object[] { "JAHAN2", "Pens", 200, 350.00 });
            dt.Rows.Add(new object[] { "JAHAN2", "Pencils", 400, 500.00 });
            dt.Rows.Add(new object[] { "JAHAN2", "Notebooks", 100, 300.00 });
            dt.Rows.Add(new object[] { "JAHAN2", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "JAHAN2", "Calculators", 120, 1200.00 });
            dt.Rows.Add(new object[] { "JAHAN2", "Back Packs", 75, 1500.00 });
            dt.Rows.Add(new object[] { "JAHAN2E", "Pens", 225, 393.75 });
            dt.Rows.Add(new object[] { "JAHAN2E", "Pencils", 335, 418.75 });
            dt.Rows.Add(new object[] { "JAHAN2E", "Notebooks", 200, 600.00 });
            dt.Rows.Add(new object[] { "JAHAN2E", "Rulers", 75, 150.00 });
            dt.Rows.Add(new object[] { "JAHAN2E", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "JAHAN2E", "Back Packs", 97, 1940.00 });
            dt.Rows.Add(new object[] { "SSSAYYY1", "Pens", 202, 353.50 });
            dt.Rows.Add(new object[] { "SSSAYYY1", "Pencils", 303, 378.75 });
            dt.Rows.Add(new object[] { "SSSAYYY1", "Notebooks", 198, 600.00 });
            dt.Rows.Add(new object[] { "SSSAYYY1", "Rulers", 98, 594.00 });
            dt.Rows.Add(new object[] { "SSSAYYY1", "Calculators", 80, 800.00 });
            dt.Rows.Add(new object[] { "SSSAYYY1", "Back Packs", 101, 2020.00 });
            dt.Rows.Add(new object[] { "SHARRAH1", "Pens", 112, 196.00 });
            dt.Rows.Add(new object[] { "SHARRAH1", "Pencils", 245, 306.25 });
            dt.Rows.Add(new object[] { "SHARRAH1", "Notebooks", 198, 594.00 });
            dt.Rows.Add(new object[] { "SHARRAH1", "Rulers", 50, 100.00 });
            dt.Rows.Add(new object[] { "SHARRAH1", "Calculators", 66, 660.00 });
            dt.Rows.Add(new object[] { "SHARRAH1", "Back Packs", 50, 2020.00 });


            return dt;
        }
    }

    public DataTable BindDefectCategoryWise(string stDate, string enDate)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("RPT_PivotComplaintTypeWiseData", conn);
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

    public DataTable BindAreaWise(string stDate, string enDate)
    {
        try
        {
            SqlConnection conn = null;
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString());
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("RPT_PivotAreaWiseData", conn);
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
    

}