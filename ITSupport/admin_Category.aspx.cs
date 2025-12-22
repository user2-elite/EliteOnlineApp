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

public partial class admin_Category : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlConnection con = null;

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
            if (Session["selectedRole"].ToString() != "1")
            {
                Response.Redirect("home.aspx");
            }
        }
    }

    protected void AddWorkSchedule_Click(object sender, EventArgs e)
    {
        if (Noofworkingdays.SelectedValue.ToString() == "")
        {
            LBerrornoofdays.Visible = true;
            return;
        }

        int UnderObservation = 0;
        int PendingtoUser = 0;
        int WaitingforApproval = 0;
        int ExternalSupport = 0;

        if (AddWorkSchedule.Text.Trim() == "Add")
        {
            SqlDataSource1.InsertParameters.Clear();
            SqlDataSource1.InsertParameters.Add("GroupName", GroupName.Text);
            SqlDataSource1.InsertParameters.Add("NoofWorkingDays", Noofworkingdays.SelectedValue.ToString());
            SqlDataSource1.InsertParameters.Add("Startinghour", StartingHour.SelectedValue.ToString());
            SqlDataSource1.InsertParameters.Add("Endinghour", EndingHour.SelectedValue.ToString());
            SqlDataSource1.InsertParameters.Add("Holidaysincluded", "1");
            SqlDataSource1.InsertParameters.Add("HolidayLocation", "1");//HolidayLocation.SelectedValue.ToString());

            SqlDataSource1.InsertParameters.Add("KBUpdationRequired", "0"); //ddlKBUpdationRequired.SelectedValue.ToString());
            SqlDataSource1.InsertParameters.Add("EscalationEmail", txtemailesaclation.Text);

            SqlDataSource1.InsertParameters.Add("Status", Status.SelectedValue.ToString());

            SqlDataSource1.InsertParameters.Add("UnderObservation", UnderObservation.ToString());
            SqlDataSource1.InsertParameters.Add("PendingtoUser", PendingtoUser.ToString());
            SqlDataSource1.InsertParameters.Add("WaitingforApproval", WaitingforApproval.ToString());
            SqlDataSource1.InsertParameters.Add("ExternalSupport", ExternalSupport.ToString());


            SqlDataSource1.InsertParameters.Add("HelpDeskID", Session["HelpDeskID"].ToString());
            SqlDataSource1.InsertParameters.Add("CreatedBy", Session["UserID"].ToString());


            SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.InsertCommand = "InsertWorkSchedule";
            SqlDataSource1.Insert();
            ClearInsertForm();
        }
        else if (AddWorkSchedule.Text.Trim() == "Update")
        {
            SqlDataSource1.UpdateParameters.Clear();

            SqlDataSource1.UpdateParameters.Add("GroupName", GroupName.Text);
            SqlDataSource1.UpdateParameters.Add("NoofWorkingDays", Noofworkingdays.SelectedValue.ToString());
            SqlDataSource1.UpdateParameters.Add("Startinghour", StartingHour.SelectedValue.ToString());
            SqlDataSource1.UpdateParameters.Add("Endinghour", EndingHour.SelectedValue.ToString());
            SqlDataSource1.UpdateParameters.Add("Holidaysincluded", "1");
            SqlDataSource1.UpdateParameters.Add("HolidayLocation", "1");

            SqlDataSource1.UpdateParameters.Add("KBUpdationRequired", "0"); //ddlKBUpdationRequired.SelectedValue.ToString());
            SqlDataSource1.UpdateParameters.Add("EscalationEmail", txtemailesaclation.Text);

            SqlDataSource1.UpdateParameters.Add("Status", Status.SelectedValue.ToString());

            SqlDataSource1.UpdateParameters.Add("UnderObservation", UnderObservation.ToString());
            SqlDataSource1.UpdateParameters.Add("PendingtoUser", PendingtoUser.ToString());
            SqlDataSource1.UpdateParameters.Add("WaitingforApproval", WaitingforApproval.ToString());
            SqlDataSource1.UpdateParameters.Add("ExternalSupport", ExternalSupport.ToString());

            SqlDataSource1.UpdateParameters.Add("GroupID", AddWorkSchedule.CommandArgument.ToString());

            SqlDataSource1.UpdateParameters.Add("CreatedBy", Session["UserID"].ToString());
            SqlDataSource1.UpdateParameters.Add("HelpDeskID", Session["HelpDeskID"].ToString());

            SqlDataSource1.UpdateCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.UpdateCommand = "UpdateWorkSchedule";
            SqlDataSource1.Update();
            ClearInsertForm();
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "editdetails")
        {

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["HelpDesk"].ToString());
            cmd = new SqlCommand("GETWorkSchedulebyGroupID", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@groupID", e.CommandArgument.ToString());
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dr.Read())
            {
                ClearInsertForm();
                GroupName.Text = dr["GroupName"].ToString();
                Noofworkingdays.SelectedValue = dr["NoofWorkingDays"].ToString();
                StartingHour.SelectedValue = dr["Startinghour"].ToString();
                EndingHour.SelectedValue = dr["Endinghour"].ToString();
                Status.SelectedValue = dr["Status"].ToString();
                //ddlKBUpdationRequired.SelectedValue = dr["KBUpdationRequired"].ToString();
                txtemailesaclation.Text = dr["EscalationEmail"].ToString();
                AddWorkSchedule.CommandArgument = e.CommandArgument.ToString();
                AddWorkSchedule.Text = "Update";

                HiddenGroupID.Value = dr["GroupID"].ToString();
            }

            cmd.Parameters.Clear();
            cmd.Dispose();
            con.Close();
        }
    }

    protected void ClearInsertForm()
    {
        AddWorkSchedule.CommandArgument = "";
        txtemailesaclation.Text = "";
        AddWorkSchedule.Text = "Add";
        GroupName.Text = "";
        Noofworkingdays.ClearSelection();
        StartingHour.ClearSelection();
        EndingHour.ClearSelection();
        Status.ClearSelection();
    }

}