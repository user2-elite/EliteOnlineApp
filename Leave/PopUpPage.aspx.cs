using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PopUpPage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Requeststatus"] = "";
            ViewState["Code"] = Request["Code"];
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Get_TranDetals", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", ViewState["Code"]);
            cmd.Parameters.AddWithValue("@ApproverCode", Session["EmpCode"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TypeofLeave.InnerText = ds.Tables[0].Rows[0]["AbsenseText"].ToString();
                Time.InnerText = ds.Tables[0].Rows[0]["LeaveDayType"].ToString();
                StartDate.InnerText = ds.Tables[0].Rows[0]["StartDate"].ToString();
                EndDate.InnerText = ds.Tables[0].Rows[0]["EndDate"].ToString();
                //ManagerCode.InnerText= ds.Tables[0].Rows[0]["Description"].ToString();
                Description.InnerText = ds.Tables[0].Rows[0]["Description"].ToString();

                string status = ds.Tables[0].Rows[0]["Status"].ToString();
                ViewState["Requeststatus"] = "SentForCancel";
                if (status == "SentForCancel")
                {
                    ViewState["Requeststatus"] = "SentForCancel";
                    Reject.Visible = false;
                }
            }
        }

    }

    protected void Reject_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("UpdateLeaveRejectStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Comments", txtDescription.Text);
        cmd.Parameters.AddWithValue("@SlNo", ViewState["Code"]);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblMessage.Text = "Leave request has been rejected in system";
        lblMessage.ForeColor = System.Drawing.Color.Brown;
        try
        {
            LeaveEmail objEmail = new LeaveEmail();
            string reqDetails = getRequsterDetails(ViewState["Code"].ToString());
            string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Leave request has been rejected in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
            string MailSubject = "Leave Request Rejected for " + reqDetails;
            objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByLeaveID(ViewState["Code"].ToString())), Session["Email"].ToString());
        }
        catch { }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
    }

    protected void Approve_Click(object sender, EventArgs e)
    {
        LeaveEmail objEmail = new LeaveEmail();
        bool checkStatus = true;
        if (ViewState["Requeststatus"].ToString() != "SentForCancel")
        {
            checkStatus = objEmail.CheckLeaveAvailabilityBySlNO(ViewState["Code"].ToString());
        }
        if (checkStatus)
        {
            SqlCommand cmd = new SqlCommand("UpdateLeaveStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", ViewState["Code"]);
            cmd.Parameters.AddWithValue("@Comments", txtDescription.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            string reqDetails = getRequsterDetails(ViewState["Code"].ToString());

            if (ViewState["Requeststatus"].ToString() != "SentForCancel")
            {
                lblMessage.Text = "Leave Request has been approved in system";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                try
                {
                    string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Leave request has been approved in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
                    string MailSubject = "Leave Request Approved for "+ reqDetails;
                    objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByLeaveID(ViewState["Code"].ToString())), Session["Email"].ToString());
                }
                catch { }
            }
            else
            {
                lblMessage.Text = "Leave cancellation request has been approved in system";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                try
                {
                    string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Leave cancellation request has been approved in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/>";
                    string MailSubject = "Leave Cancellation Request Approved for " + reqDetails;
                    objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByLeaveID(ViewState["Code"].ToString())), Session["Email"].ToString());
                }
                catch { }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
        }
        else
        {
            lblMessage.Text = "Could not approve request since number of days requested exceeds the quata limit. Please reject the transaction and request employee to create new using other leave type.";
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
}