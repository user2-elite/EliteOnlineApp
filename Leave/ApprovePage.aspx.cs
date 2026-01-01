using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApprovePage : System.Web.UI.Page
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
            Button1.Visible = false;
            Button2.Visible = false;
            ViewState["PERNR"] = Session["EmpCode"].ToString();
            GetLeaveRequest(ViewState["PERNR"].ToString());
            GetSentForCancelDetails(ViewState["PERNR"].ToString());
            GetRequest(ViewState["PERNR"].ToString());
        }

    }


    public DataSet GetSentForCancelDetails(string empno)
    {
        try
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetSentForCancel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpNo", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                CancelReqNoRecords.Visible = false;
            }
            else
            {
                CancelReqNoRecords.Visible = true;
            }

            return ds;
        }
        catch (Exception ex)
        {

            return null;
        }
    }


    public DataSet GetRequest(string empno)
    {

        try
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("GetRepoteeLeaveRequestnotSent", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FunctionalReportingEmpNo", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                previousReqNoRecords.Visible = false;
            }
            else
            {
                previousReqNoRecords.Visible = true;
            }

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
            SqlCommand cmd = new SqlCommand("GetRepoteeLeaveRequest", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FunctionalReportingEmpNo", empno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ApproveNoRecords.Visible = false;
                Button1.Visible = true;
                Button2.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
                Button1.Visible = false;
                Button2.Visible = false;
                ApproveNoRecords.Visible = true;
            }

            return ds;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    private string getRequsterDetails(string slNo)
    {
        string returnValue = "";
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Leave_GetRequesterDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", slNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                returnValue = ds.Tables[0].Rows[0]["EmpName"].ToString() + " (" + ds.Tables[0].Rows[0]["PersNo"].ToString() + ")";
            }
        }
        catch
        {

        }
        return returnValue;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = string.Empty;
        string reqDetails = "";
        int NotApprovedCount = 0;
        int ApprovedCount = 0;
        bool isApproved = false;
        LeaveEmail objEmail = new LeaveEmail();
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            TextBox comments = (TextBox)gvrow.FindControl("txtcomments");
            if (chk != null & chk.Checked)
            {
                isApproved = false;
                str = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
                if (objEmail.CheckLeaveAvailabilityBySlNO(str))
                {
                    SqlCommand cmd = new SqlCommand("UpdateLeaveStatus", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SlNo", str);
                    cmd.Parameters.AddWithValue("@Comments", comments.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ApprovedCount++;
                    isApproved = true;
                }
                else
                {
                    NotApprovedCount++;
                }
                if (isApproved)
                {
                    try
                    {
                        reqDetails = getRequsterDetails(str);
                        string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Leave Request has been approved in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
                        string MailSubject = "";
                        if (reqDetails.Length > 0)
                        {
                            MailSubject = "Leave Request Approved for employee - " + reqDetails;
                        }
                        else
                        {
                            MailSubject = "Leave Request Approved";
                        }
                        objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByLeaveID(str)), Session["Email"].ToString());
                    }
                    catch { }
                }
            }
        }
        lblMessage.Text = "";
        if (ApprovedCount > 0)
        {
            lblMessage.Text = ApprovedCount + " request(s) Approved. ";
        }
        if (NotApprovedCount > 0)
        {
            lblMessage.Text += "“Some requests were not approved because the number of days requested exceeds the quota limit. Please reject those leave(s) and ask the employee to submit a new request using another leave type.";
        }

        if (ApprovedCount > 0)
        {
            lblMessage.ForeColor = System.Drawing.Color.Blue;
            GetLeaveRequest(ViewState["PERNR"].ToString());
            GetRequest(ViewState["PERNR"].ToString());
        }
        else
        {
            lblMessage.Text = "Some requests were not approved, either because the checkbox(es) were not ticked, or you may need to click 'View Details' to try again and see the exact error.";
            lblMessage.ForeColor = System.Drawing.Color.Blue;
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string str = string.Empty;
        bool isRejected = false;
        foreach (GridViewRow gvrow in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            TextBox comments = (TextBox)gvrow.FindControl("txtcomments");
            if (chk != null & chk.Checked)
            {
                str = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
                SqlCommand cmd = new SqlCommand("UpdateLeaveRejectStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Comments", comments.Text);
                cmd.Parameters.AddWithValue("@SlNo", str);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                isRejected = true;
                try
                {
                    LeaveEmail objEmail = new LeaveEmail();
                    string reqDetails = getRequsterDetails(str);
                    string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Leave request has been rejected in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
                    string MailSubject = "Leave Request Rejected for " + reqDetails;
                    objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByLeaveID(str)), Session["Email"].ToString());
                }
                catch { }

            }
        }

        if (isRejected)
        {
            lblMessage.Text = "Selected leave request(s) has been rejected";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            GetLeaveRequest(ViewState["PERNR"].ToString());
            GetRequest(ViewState["PERNR"].ToString());
        }
        else
        {
            lblMessage.Text = "Please select a leave request and proceed.";
            lblMessage.ForeColor = System.Drawing.Color.Blue;
        }

    }
}