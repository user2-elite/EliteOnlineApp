using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaintenanceActivities : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (Session["HelpDeskID"] == null)
        {
            Response.Redirect("Home.aspx");
        }
        if (!IsPostBack)
        {
            ViewState["selectedRole"] = Session["selectedRole"].ToString();
            if (ViewState["selectedRole"].ToString() != "1" && ViewState["selectedRole"].ToString() != "2" && ViewState["selectedRole"].ToString() != "3")
            {
                Response.Redirect("home.aspx");
            }
        }
        if (!IsPostBack)
        {
            OpenReqs();
        }
    }

    protected void lnkOpenReq_Click(object sender, EventArgs e)
    {
        OpenReqs();
    }

    protected void lnkClosedReq_Click(object sender, EventArgs e)
    {
        ClosedReqs();
    }

    protected void lnkAllReq_Click(object sender, EventArgs e)
    {
        AllReqs();
    }

    private void AllReqs()
    {
        lnkAllOpen.BackColor = System.Drawing.Color.White;
        lnkClosedReq.BackColor = System.Drawing.Color.White;
        lnkAllPendingActivities.BackColor = System.Drawing.Color.AliceBlue;

        pnlOpenReq.Visible = false;
        pnlClosedReq.Visible = false;
        pnlAllReq.Visible = true;
        ViewState["ReqType"] = "3";
    }

    private void OpenReqs()
    {
        lnkAllOpen.BackColor = System.Drawing.Color.AliceBlue;
        lnkClosedReq.BackColor = System.Drawing.Color.White;
        lnkAllPendingActivities.BackColor = System.Drawing.Color.White;
        FillGrid();        
        pnlOpenReq.Visible = true;
        pnlClosedReq.Visible = false;
        pnlAllReq.Visible = false;
        ViewState["ReqType"] = "1";
    }

    private void ClosedReqs()
    {
        lnkAllOpen.BackColor = System.Drawing.Color.White;
        lnkClosedReq.BackColor = System.Drawing.Color.AliceBlue;
        lnkAllPendingActivities.BackColor = System.Drawing.Color.White;
        pnlOpenReq.Visible = false;
        pnlClosedReq.Visible = true;
        pnlAllReq.Visible = false;
        ViewState["ReqType"] = "2";
    }

    protected void FillGrid()
    {
        try
        {
            //Open requests
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.SelectCommand = "IT_Get_SCH_UpComingActivities";
            
            //Admin. Show all results
            if (ViewState["selectedRole"].ToString() != "1")
            {
                SqlDataSource1.SelectParameters.Add("UID", Session["UserID"].ToString());
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error. Please try again");
        }
    }

    protected void FillGrid2()
    {
        try
        {
            //closed Requests
            string dt1 = txtFromDate2.Text.ToString(), dt2 = txtToDate2.Text.ToString();
            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource2.SelectCommand = "IT_Get_SCH_AllClosedActivities";
            //Admin. Show all results
            if (ViewState["selectedRole"].ToString() != "1")
            {
                SqlDataSource2.SelectParameters.Add("UID", Session["UserID"].ToString());
            }
            SqlDataSource2.SelectParameters.Add("dt1", dt1);
            SqlDataSource2.SelectParameters.Add("dt2", dt2);
        }
        catch (Exception ex)
        {
            Response.Write("Error. Please try again");
        }
    }

    protected void FillGrid3()
    {
        try
        {
            //All requests
            string dt1 = txtFromDate3.Text.ToString(), dt2 = txtToDate3.Text.ToString();
            SqlDataSource3.SelectParameters.Clear();
            SqlDataSource3.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource3.SelectCommand = "IT_Get_SCH_AllPendingActivities";

            //Admin. Show all results
            if (ViewState["selectedRole"].ToString() != "1")
            {
                SqlDataSource3.SelectParameters.Add("UID", Session["UserID"].ToString());
            }

            SqlDataSource3.SelectParameters.Add("dt1", dt1);
            SqlDataSource3.SelectParameters.Add("dt2", dt2);
        }
        catch (Exception ex)
        {
            Response.Write("Error. Please try again");
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string arg = e.CommandArgument.ToString();
            if (e.CommandName == "View")
            {
                Session["AssetID"] = arg;
                Response.Redirect("UpdateActivity.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string arg = e.CommandArgument.ToString();
            if (e.CommandName == "View")
            {
                Session["AssetID"] = arg;
                Response.Redirect("UpdateActivity.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string arg = e.CommandArgument.ToString();
            if (e.CommandName == "View")
            {
                Session["AssetID"] = arg;
                Response.Redirect("UpdateActivity.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            int ActivityIndays = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString());

            if (ActivityIndays < 0)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#F0F0F0");
                e.Row.ForeColor = System.Drawing.Color.FromName("#FF0000");
            }
            else
            {
                e.Row.ForeColor = System.Drawing.Color.FromName("#000000");
            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            int VolationDays = Convert.ToInt32(GridView2.DataKeys[e.Row.RowIndex].Values[0].ToString());

            if (VolationDays > 0)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#F0F0F0");
                e.Row.ForeColor = System.Drawing.Color.FromName("#FF0000");
            }
            else
            {
                e.Row.ForeColor = System.Drawing.Color.FromName("#000000");
            }
        }
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            int ActivityIndays = Convert.ToInt32(GridView3.DataKeys[e.Row.RowIndex].Values[0].ToString());

            if (ActivityIndays < 0)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#F0F0F0");
                e.Row.ForeColor = System.Drawing.Color.FromName("#FF0000");
            }
            else
            {
                e.Row.ForeColor = System.Drawing.Color.FromName("#000000");
            }
        }
    }

    protected void btnShowResult3_Click(object sender, EventArgs e)
    {
        //All Requests
        FillGrid3();
    }

    protected void btnShowResult2_Click(object sender, EventArgs e)
    {
        //Closed Requests
        FillGrid2();
    }
}