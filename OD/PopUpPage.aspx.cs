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
            SqlCommand cmd = new SqlCommand("OD_Get_TranDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SlNo", ViewState["Code"]);
            cmd.Parameters.AddWithValue("@ApproverCode", Session["EmpCode"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TypeofAbsence.InnerText = ds.Tables[0].Rows[0]["AbsenseType"].ToString();
                Time.InnerText = ds.Tables[0].Rows[0]["ODDayType"].ToString();
                StartDate.InnerText = ds.Tables[0].Rows[0]["StartDate"].ToString();
                EndDate.InnerText = ds.Tables[0].Rows[0]["EndDate"].ToString();
                Description.InnerText = ds.Tables[0].Rows[0]["Description"].ToString();
                TravelAdvance.InnerText = ds.Tables[0].Rows[0]["TravelAdvance"].ToString();

                string status = ds.Tables[0].Rows[0]["Status"].ToString();
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
        SqlCommand cmd = new SqlCommand("OD_UpdateRejectStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Comments", txtDescription.Text);
        cmd.Parameters.AddWithValue("@SlNo", ViewState["Code"]);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblMessage.Text = "Request has been rejected in system";
        lblMessage.ForeColor = System.Drawing.Color.Brown;
        try
        {
            LeaveEmail objEmail = new LeaveEmail();
            string reqDetails = getRequsterDetails(ViewState["Code"].ToString());
            string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>OD request has been rejected in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
            string MailSubject = "OD Request Rejected for "+ reqDetails;
            objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByODID(ViewState["Code"].ToString())), Session["Email"].ToString());
        }
        catch { }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
    }

    protected void Approve_Click(object sender, EventArgs e)
    {
        LeaveEmail objEmail = new LeaveEmail();
        SqlCommand cmd = new SqlCommand("OD_UpdateRequestStatus", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SlNo", ViewState["Code"]);
        cmd.Parameters.AddWithValue("@Comments", txtDescription.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        string reqDetails = getRequsterDetails(ViewState["Code"].ToString());
        if (ViewState["Requeststatus"].ToString() != "SentForCancel")
        {
            lblMessage.Text = "Request has been approved in system";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            try
            {
                string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>OD request has been approved in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/>";
                string MailSubject = "OD Request Approved for "+ reqDetails;
                objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByODID(ViewState["Code"].ToString())), Session["Email"].ToString());
            }
            catch { }
        }
        else
        {
            lblMessage.Text = "Cancellation request has been approved in system";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            try
            {
                string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>OD Cancellation request has been approved in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/>";
                string MailSubject = "OD Cancellation Request Approved for "+ reqDetails;
                objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByODID(ViewState["Code"].ToString())), Session["Email"].ToString());
            }
            catch { }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
    }


    private string getRequsterDetails(string slNo)
    {
        string returnValue = "";
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("OD_GetRequesterDetails", con);
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