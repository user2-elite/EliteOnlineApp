using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

public partial class ViewAllRequests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("login.aspx");
        }
        if (Session["HelpDeskID"] != null)
        {
            divHelpDesk.InnerHtml = "&nbsp;&nbsp;Selected Helpdesk: " + Session["HelpDeskName"].ToString();
        }
        else
        {
            Response.Redirect("Home.aspx");
        }        
        if (!IsPostBack)
        {
            ViewState["selectedRole"] = Session["selectedRole"].ToString();
            if (Request.QueryString["viewtype"] != null)
            {
                ViewState["selectedRole"] = "3";
            }
            ViewState["adminClicked"] = "0";

            if (ViewState["selectedRole"].ToString() != "1" && ViewState["selectedRole"].ToString() != "2" && ViewState["selectedRole"].ToString() != "3")
            {
                Response.Redirect("home.aspx");
            }
        }
        if (ViewState["ReqType"] == null)
        {
            OpenReqs();
        }
        if (ViewState["ReqType"] == "1")
        {
            OpenReqs();
        }
        if (ViewState["ReqType"] == "2")
        {
            ClosedReqs();
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

    private void OpenReqs()
    {
        FillGrid();
        pnlOpenReq.Visible = true;
        pnlClosedReq.Visible = false;
        ViewState["ReqType"] = "1";
    }

    private void ClosedReqs()
    {
        FillGrid2();
        pnlOpenReq.Visible = false;
        pnlClosedReq.Visible = true;
        ViewState["ReqType"] = "2";
    }

   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string AssingedTo = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString());

            string ExpResDtTime = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Values[2].ToString());

            string status = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Values[3].ToString());

            LinkButton HL = (LinkButton)e.Row.FindControl("lnkView");
            if (ExpResDtTime != "")
            {
                if (Convert.ToDateTime(ExpResDtTime) <= CommonClass.GetDateTimeIST() && (status != "4"))
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#F0F0F0");
                    e.Row.ForeColor = System.Drawing.Color.FromName("#FF0000");
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.FromName("#000000");
                }
                    e.Row.Cells[4].Text = ExpResDtTime.ToString();
                    HL.ForeColor = System.Drawing.Color.Blue;
            }
            string RequestID = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString());            
            if (HL != null)
            {
                if (ViewState["selectedRole"].ToString() == "3")
                {
                    if (AssingedTo == Session["UserID"].ToString())
                        HL.Text = "View/Resolve/Re-Assign Request";
                    else if (AssingedTo == "")
                        HL.Text = "View/Resolve/Assign Request";
                    else
                        HL.Text = "View/Resolve/Re-Assign Request";
                }
                else if (ViewState["selectedRole"].ToString() == "1" || ViewState["selectedRole"].ToString() == "2")
                {
                    if (AssingedTo == Session["UserID"].ToString())
                        HL.Text = "View/Resolve/Re-Assign Request";
                    else if (AssingedTo == "")
                        HL.Text = "View/Resolve/Assign Request";
                    else
                        HL.Text = "View/Resolve/Re-Assign Request";
                }
                string Urladdress = "ViewRequest.aspx?ReqID=" + RequestID + "&Requestaction=" + HL.Text;
                HL.CommandArgument = Urladdress;
            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            LinkButton HL = (LinkButton)e.Row.FindControl("lnkView");
            string RequestID = Convert.ToString(GridView2.DataKeys[e.Row.RowIndex].Values[0].ToString());
            if (HL != null)
            {
                string Urladdress = "ViewRequest.aspx?ReqID=" + RequestID + "&Requestaction=" + HL.Text;
                HL.CommandArgument = Urladdress;
            }
        }
    }

    protected void FillGrid()
    {
        try
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.SelectCommand = "GetALLActiveRequests";
            SqlDataSource1.SelectParameters.Add("HelpDeskID", Session["HelpDeskID"].ToString());
            SqlDataSource1.SelectParameters.Add("AssignedTo", Session["UserID"].ToString());
            SqlDataSource1.SelectParameters.Add("Role", ViewState["selectedRole"].ToString());
        }
        catch(Exception ex)
        {
            Response.Write("Error. Please try again");
        }
    }

    protected void FillGrid2()
    {
        try
        {
            SqlDataSource2.SelectParameters.Clear();
            SqlDataSource2.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource2.SelectCommand = "GetALLClosedRequests";
            SqlDataSource2.SelectParameters.Add("AssignedTo", Session["UserID"].ToString());
            SqlDataSource2.SelectParameters.Add("Role", ViewState["selectedRole"].ToString());
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
                string actionString = arg.ToString();
                string Urladdress = actionString;
                System.Web.UI.AttributeCollection aCol = newWin.Attributes;
                aCol.Add("src", Urladdress);
                Page.Controls.Add(new LiteralControl("<script language='javascript'>OpenWindow();</script>"));
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
                string actionString = arg.ToString();
                string Urladdress = actionString;
                System.Web.UI.AttributeCollection aCol = newWin.Attributes;
                aCol.Add("src", Urladdress);
                Page.Controls.Add(new LiteralControl("<script language='javascript'>OpenWindow();</script>"));
            }
        }
        catch (Exception ex)
        {

        }
    }


}