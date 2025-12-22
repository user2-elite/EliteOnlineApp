using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;

public partial class ListComplaint : System.Web.UI.Page
  {
    
        DBOperation myDBOperation = new DBOperation();
        DataSet allComplaintsDS = new DataSet();         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CRMUserID"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                DataSet ds1 = myDBOperation.BindComplaintswithGridview(gvComplaint, Session["CRMUserID"].ToString(), "", "");
                ViewState["ds1"] = (DataSet)ds1;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strText = txtSearchText.Text.ToString().Trim();
            string strCompStatus = ddlStatusSearch.SelectedValue.ToString();
            DataSet ds1 = myDBOperation.BindComplaintswithGridview(gvComplaint, Session["CRMUserID"].ToString(), strText, strCompStatus);
            ViewState["ds1"] = (DataSet)ds1;
        }
    


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string ComplaintID = e.CommandArgument.ToString();
                if (e.CommandName == "EditRow")
                {
                    Session["ComplaintID"] = ComplaintID.Trim();
                    string Urladdress = "ComplaintRegister.aspx"; //?arg=" + ComplaintID.ToString();
                    Response.Redirect(Urladdress);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvComplaint_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvComplaint.PageIndex = e.NewPageIndex;
            DataSet ds = new DataSet();
            ds = (DataSet)ViewState["ds1"];

            gvComplaint.DataSource = ds;
            gvComplaint.DataBind();
        }
        
    }