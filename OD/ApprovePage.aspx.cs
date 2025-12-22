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
         * OD Workflow Status
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
            GetSentRequestOfReportees(ViewState["PERNR"].ToString());
            GetSentForCancelDetails(ViewState["PERNR"].ToString());
            GetRepoteeRequestNotSent(ViewState["PERNR"].ToString());
        }

    }


    public DataSet GetSentForCancelDetails(string empno)
    {
        try
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("OD_GetSentForCancel", con);
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


    public DataSet GetRepoteeRequestNotSent(string empno)
    {

        try
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("OD_GetRepoteeRequestnotSent", con);
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
    public DataSet GetSentRequestOfReportees(string empno)
    {
        try
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("OD_GetReporteeRequest", con);
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = string.Empty;
        string reqDetails = "";
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

                SqlCommand cmd = new SqlCommand("OD_UpdateRequestStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SlNo", str);
                cmd.Parameters.AddWithValue("@Comments", comments.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                ApprovedCount++;
                isApproved = true;

                if (isApproved)
                {
                    try
                    {
                        reqDetails = getRequsterDetails(str);
                        string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Request has been approved in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
                        string MailSubject = "";
                        if(reqDetails.Length > 0)
                        {
                            MailSubject = "OD Request Approved for employee - " + reqDetails;
                        } else
                        {
                            MailSubject = "OD Request Approved";
                        }
                        objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByODID(str)), Session["Email"].ToString());
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

        if (ApprovedCount > 0)
        {
            lblMessage.ForeColor = System.Drawing.Color.Blue;
            GetSentRequestOfReportees(ViewState["PERNR"].ToString());
            GetRepoteeRequestNotSent(ViewState["PERNR"].ToString());
        }
        else
        {
            lblMessage.Text = "Please select a request and proceed.";
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
                SqlCommand cmd = new SqlCommand("OD_UpdateRejectStatus", con);
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
                    string Mailcontent = "<div style='margin:5px'>Hi, <BR/><BR/>Request has been rejected in system by <B>" + Session["LogonUserFullName"] + ".</B><br/><B>Requester Name/Employee Id: " + reqDetails + "</B><br/><br/>";
                    string MailSubject = "OD Request Rejected for "+ reqDetails;
                    objEmail.SendMailReport(Mailcontent, MailSubject, Session["Email"].ToString(), objEmail.GetEmailList(objEmail.GetUserEmailByODID(str)), Session["Email"].ToString());
                }
                catch { }

            }
        }

        if (isRejected)
        {
            lblMessage.Text = "Selected request(s) has been rejected";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            GetSentRequestOfReportees(ViewState["PERNR"].ToString());
            GetRepoteeRequestNotSent(ViewState["PERNR"].ToString());
        }
        else
        {
            lblMessage.Text = "Please select a request and proceed.";
            lblMessage.ForeColor = System.Drawing.Color.Blue;
        }

    }
}