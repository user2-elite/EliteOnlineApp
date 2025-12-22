using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using INTModel;

public partial class AddQuote : System.Web.UI.Page
{

    INTModelContainer objEntities = new INTModelContainer();
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (!Page.IsPostBack)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1" && Session["selectedRole"].ToString() != "4")
                {
                    Response.Redirect("home.aspx");
                }
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            ListData();
        }
    }

  
    private void ListData()
    {
        try
        {
            pnlList.Visible = true;
            List<INT_Get_QuoteText_Result> objListAll = objEntities.INT_Get_QuoteText().ToList();
            if (objListAll.Count > 0)
            {
                grid1.DataSource = objListAll;
                grid1.DataBind();
                grid1.Visible = true;
            }
            else
            {
                if (Cache["QuoteFortheDay"] != null)
                {
                    Cache.Remove("QuoteFortheDay");
                }
                pnlList.Visible = false;
                grid1.Visible = false;
                ShowMessage(divAlert, "Note", "No data found!");
            }
        }
        catch
        {
            ShowMessage(diverror, "Error", "Error while displaying data!");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string QuoteText = txtQuote.Text.Trim();
            if (QuoteText.Length > 24)
            {
                if (ViewState["ID"] != null)
                {
                    objEntities.INT_Update_QuoteText(System.Convert.ToInt32(ViewState["ID"].ToString()), QuoteText);
                    ShowMessage(divSuccess, "Success", "Updated successfully!");
                }
                else
                {
                    objEntities.INT_Insert_QuoteText(QuoteText, DateTime.Now, "");
                    ShowMessage(divSuccess, "Success", "Added successfully!");
                }
                FillCache();                
                ListData();
            }
            else
            {
                ShowMessage(diverror, "Error", "Please enter at least 25 character length text!");
            }

        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while updating data!: " + ex.Message.ToString());
        }
    }


    private void ShowMessage(HtmlGenericControl div1, string spanText, string Message)
    {
        clearMessages();
        div1.InnerHtml = "<span>" + spanText + "</span>";
        div1.InnerHtml += ": ";
        div1.InnerHtml += Message;
        div1.Visible = true;
    }
    private void clearMessages()
    {
        divAlert.InnerHtml = "";
        divAlert.Visible = false;
        divSuccess.InnerHtml = "";
        divSuccess.Visible = false;
        diverror.InnerHtml = "";
        diverror.Visible = false;
        divNotice.InnerHtml = "";
        divNotice.Visible = false;
        divListSuccess.Visible = false;
        divListError.Visible = false;
    }

    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            try
            {
                List<INT_Get_QuoteText_BYID_Result> objListAll = objEntities.INT_Get_QuoteText_BYID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
                if (objListAll.Count > 0)
                {
                    string QText = objListAll[0].QuoteText.ToString();
                    ViewState["ID"] = e.CommandArgument.ToString();
                    txtQuote.Text = QText;
                    btnSubmit.Text = "Update Details";
                    txtQuote.Focus();
                }
            }
            catch
            {
                ShowMessage(diverror, "Error", "Error while editing data!");
            }
        }
        else if (e.CommandName == "cmDelete")
        {
            try
            {
                objEntities.INT_Delete_QuoteText(System.Convert.ToInt32(e.CommandArgument.ToString()));
                ListData();
                ShowMessage(divListSuccess, "Success", "Deleted successfully!");
                FillCache();
            }
            catch
            {
                ShowMessage(divListError, "Error", "Error while deleting ATR data!");
            }
        }
    }
    private void FillCache()
    {        
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "INT_Get_LatestQuoteText";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string Quote = dt.Rows[0]["QuoteText"].ToString();
                        if (Quote.Trim().Length > 0)
                        {
                            if (Cache["QuoteFortheDay"] != null)
                            {
                                Cache.Remove("QuoteFortheDay");
                            }
                            Cache.Insert("QuoteFortheDay", Quote);
                        }
                    }
                    //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                }
                dt.Dispose();
                conn.Close();
            }
        }
    }

 
}