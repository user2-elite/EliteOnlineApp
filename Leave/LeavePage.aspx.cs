using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class LeavePage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * Leave Workflow Status
         * Sent
           Approved
            Cancelled
            Rejected
            SentForCancel
         * 
         * */
        if (!IsPostBack)
        {
            if(Session["FunctionalReportingEmpNo"] == null || Session["FunctionalReportingEmpNo"].ToString().Trim().Length == 0)
            {
                pnlEmpty.Visible = true;
                pnlRequest.Visible = false;
                return;
            }
            pnlEmpty.Visible = false;
            pnlRequest.Visible = true;
            ViewState["PERNR"] = Session["EmpCode"].ToString();
            ViewState["Repmanager"] = Session["FunctionalReportingEmpNo"].ToString();
            //TextBox3.Text = ViewState["Repmanager"].ToString();
            LeaveEmail objEmail = new LeaveEmail();
            TextBox5.Text = objEmail.GetEmpNameByEmpCode(ViewState["Repmanager"].ToString()) + " (" + ViewState["Repmanager"].ToString() + ")";
            GetData(ViewState["PERNR"].ToString());
            GetLeaveRequest(ViewState["PERNR"].ToString());
            GetLeaveQuato(ViewState["PERNR"].ToString());
        }

    }
    public DataSet GetLeaveQuato(string empno)
    {

        try
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetLeaveQuato", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersNo", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
            return ds;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public DataSet GetLeaveRequest(string empno)
    {
        try
        {        
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetLeaveRequest", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersNo", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            return ds;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public DataSet GetData(string empno)
    {

        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Get_AbsenseQuotaTypes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersNo", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DropDownList1.DataSource = ds;
                    DropDownList1.DataBind();
                }
            }
            return ds;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public bool CheckLeaveAvailability(string empno, string LeaveType, string StartDate, string EndDate, string leaveDayType)
    {

        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Check_LeaveAvailability", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersNo", empno);
            cmd.Parameters.AddWithValue("@LeaveType", LeaveType);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@LeaveDayType", leaveDayType);            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            string returnValue = "0";
            if (ds.Tables.Count > 0)
            {
                returnValue = ds.Tables[0].Rows[0]["AvailableStatus"].ToString();
            }
            if (returnValue == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {

            return false;
        }
    }

    public bool CheckIsLeaveDuplicate(string empno, string StartDate, string EndDate)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Check_LeaveDuplicate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersNo", empno);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            string returnValue = "0";
            if (ds.Tables.Count > 0)
            {
                returnValue = ds.Tables[0].Rows[0]["IsDuplicateRecord"].ToString();
            }
            if(returnValue.Trim() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {

            return false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(DropDownList2.SelectedItem.Value) != 1)
        {
            TextBox2.Text = TextBox1.Text;
        }

        if (DropDownList1.SelectedItem.Text == "<--Select-->")
        {
            lblMessage.Text = "Leave type is mandatory";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }
        if (string.IsNullOrEmpty(TextBox1.Text))
        {
            lblMessage.Text = "Start date is mandatory";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }
        if (string.IsNullOrEmpty(TextBox2.Text))
        {
            lblMessage.Text = "End date is mandatory";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        DateTime dtstdate = Convert.ToDateTime(TextBox1.Text);
       
        DateTime dteddate = Convert.ToDateTime(TextBox2.Text);
     
        if (dtstdate > dteddate)
        {
            lblMessage.Text = "Start Date should not be greater than End date";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        if (CheckLeaveAvailability(ViewState["PERNR"].ToString(), DropDownList1.SelectedItem.Value, TextBox1.Text, TextBox2.Text, DropDownList2.SelectedItem.Text))
        {
            if (!CheckIsLeaveDuplicate(ViewState["PERNR"].ToString(), TextBox1.Text, TextBox2.Text))
            {
                SqlCommand cmd = new SqlCommand("Ins_LeaveTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersNo", ViewState["PERNR"].ToString());
                cmd.Parameters.AddWithValue("@ReportingEmpNo", ViewState["Repmanager"].ToString());
                cmd.Parameters.AddWithValue("@LeaveType", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@StartDate", TextBox1.Text);//
                cmd.Parameters.AddWithValue("@EndDate", TextBox2.Text);//
                cmd.Parameters.AddWithValue("@LeaveDayType", DropDownList2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Description", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Status", "Sent");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblMessage.Text = "Leave request sent for approval.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                GetLeaveRequest(ViewState["PERNR"].ToString());
                GetLeaveQuato(ViewState["PERNR"].ToString());
                try
                {
                    LeaveEmail objEmail = new LeaveEmail();
                    string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>New Leave request has been logged in system by <B>" + Session["LogonUserFullName"] + ".</B> Manager can approve the request after log in to portal below. <br/><br/>";
                    Mailcontent += "<br/>Leave From Date: " + TextBox1.Text;
                    Mailcontent += "<br/>Leave To Date: " + TextBox2.Text;
                    Mailcontent += "<br/>Full Day/Half Day: " + DropDownList2.SelectedItem.Text;
                    Mailcontent += "<br/>Description: " + TextBox4.Text;
                    Mailcontent += "<br/>Link to approve: https://eliteonline.in/default.aspx?leaveapprove=1 </div>";
                    string MailSubject = "New Leave request: " + Session["LogonUserFullName"];
                    objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailByEmpCode(ViewState["Repmanager"].ToString()), objEmail.GetEmailList(Session["UserID"].ToString()));
                }
                catch { }
            }
            else
            {
                lblMessage.Text = "One or more dates in the selected period is already applied in the system. Please check the start and end date and also check the Leave History.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblMessage.Text = "Number of leave days exceeded the available quata balance. Please check the Leave quata above.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
       
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        if (Convert.ToInt32(DropDownList2.SelectedItem.Value) != 1)
        {
            divEndDate.Visible = false;
            //datetimepicker2.Disabled.
            //datetimepicker2.Attributes.Add("Disabled", "Disabled");
            //datetimepicker2.Disabled = true;
        }
        else
        {
            divEndDate.Visible = true;
        }

    }
   

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        int SlNo = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["SlNo"].ToString());
        SqlCommand cmd = new SqlCommand("UpdateCaneclLeave", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SlNo", SlNo);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblMessage.Text = "Cancellation updated, however it may trigger to manager for verification and approval. Please check the status in Leave history tab.";
        lblMessageHeader.Text = "Cancellation updated, however it may trigger to manager for verification and approval. Please check the status in Leave history tab.";
        lblMessage.ForeColor = System.Drawing.Color.Blue;
        lblMessageHeader.ForeColor = System.Drawing.Color.Blue;
        GetLeaveRequest(ViewState["PERNR"].ToString());
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {

        foreach (GridViewRow gvrow in GridView1.Rows)
        {
           
            string cell_5_Value = GridView1.Rows[gvrow.RowIndex].Cells[4].Text;
            if(cell_5_Value== "Cancelled" || cell_5_Value == "Rejected" || cell_5_Value == "SentForCancel")
            {
                LinkButton lnk = (LinkButton)gvrow.FindControl("lnkCancel");
                lnk.Visible = false;
            }           
          
          
        }

    }
}