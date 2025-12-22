using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class adminLeaveQuota : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (!Page.IsPostBack)
        {
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1")
                {
                    Response.Redirect("home.aspx");
                }
                DeleteTemporaryData();
                BindYear(ddlYear, "");
            }
            else
            {
                Response.Redirect("default.aspx");
            }

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
        divListSuccess.InnerHtml = "";
        divListSuccess.Visible = false;
        divListError.InnerHtml = "";
        divListError.Visible = false;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlType.SelectedItem.Value == "" || ddlYear.SelectedItem.Value == "")
        {
            ShowMessage(divListError, "Error: ", "Please select Leave Type & Year");
            return;
        }
        if (!upldExcelData.HasFile)
        {
            ShowMessage(divListError, "Error: ", "Please upload the excel file");
            return;
        }
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        string str = "select (isnull(max(Doc_ID),0)+1) from INT_LeaveQuota_Attach_Docs";
        string docid = "";
        SqlCommand cmd2 = new SqlCommand(str, con);
        cmd2.CommandType = CommandType.Text;

        SqlDataReader dr = cmd2.ExecuteReader();
        while (dr.Read())
        {
            docid = dr[0].ToString();
        }
        dr.Close();

        try
        {
            if (!DeleteTemporaryData())
            {
                ShowMessage(divListError, "Error: ", "Delete temp data failed. Please try again or clear the table - `LeaveQuotaDetails_ExcelData` and proceed.");
                return;
            }
            if (upldExcelData.FileName.Contains(".xlsx") || upldExcelData.FileName.Contains(".xls"))
            {
                if (UploadExcel())
                {
                    ExtractDataFromExcelAndUpdateQuota(docid);
                    //ShowMessage(divListSuccess, "Success: ", "Quota Uploaded. Please check the Quota");
                }
                else
                {
                    ShowMessage(divListError, "Error: ", "Upload excel format failed.");
                }
            }
            else
            {
                ShowMessage(divListError, "Error: ", "Please upload only excel format file");
            }

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Microsoft.ACE.OLEDB.12.0"))
            {
                ShowMessage(divListError, "Error: ", "Please upload Office Excel 97 (.xls) Format");
            }
            else
            {
                ShowMessage(divListError, "Error: ", ex.Message);
            }
        }
        finally
        {

            if (con != null)
            {
                con.Close();
            }
        }
    }


    protected bool DeleteTemporaryData()
    {
        SqlConnection con = null;
        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        if (con != null)
            con.Open();

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("INT_LeaveQuota_Delete_TemporaryData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@createdBy", Session["UserID"].ToString());
            cmd.ExecuteNonQuery();
            if (con != null)
            {
                con.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            if (con != null)
            {
                con.Close();
            }
            return false;
        }
    }

    private string getActiveYear()
    {
        string Year = "";
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "LEAVE_GET_Active_YEAR";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Year = dt.Rows[0]["ActiveYear"].ToString();
                        }
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch { }
        finally {
            if (Year.Length == 0)
            {
                Year = DateTime.Now.Year.ToString();
            }
        }
        return Year;
    }

    protected bool UploadExcel()
    {

        SqlConnection con = null;
        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        if (con != null)
            con.Open();

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        try
        {
            if (upldExcelData.HasFile)
            {
                string filenamepath = upldExcelData.PostedFile.FileName;
                SqlCommand cmd = new SqlCommand("INT_LeaveQuota_Insert_Attach_Docs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Doc_AttachedBy", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Doc_FileName", upldExcelData.FileName.ToString());
                cmd.ExecuteNonQuery();
            }
            if (con != null)
            {
                con.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            if (con != null)
            {
                con.Close();
            }
            return false;
        }
    }

    protected bool UpdateQuota(string uplType, string QType, string year)
    {
        SqlConnection con = null;
        con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        if (con != null)
            con.Open();

        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        try
        {
            if (upldExcelData.HasFile)
            {
                string filenamepath = upldExcelData.PostedFile.FileName;
                SqlCommand cmd = new SqlCommand("LEAVE_UPDATE_QUOTA_BULK", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UplodType", uplType);
                cmd.Parameters.AddWithValue("@QuotaType", QType);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserID"].ToString());
                cmd.ExecuteNonQuery();
                if (con != null)
                {
                    con.Close();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            if (con != null)
            {
                con.Close();
            }
            return false;
        }
    }

    protected void ExtractDataFromExcelAndUpdateQuota(string docid)
    {
        string filename = "";
        if (upldExcelData.HasFile)
        {
            string fileName = "";
            if (upldExcelData.FileName.Contains(".xlsx"))
            {
                fileName = "LeaveQuotaFile.xlsx";
            }
            else
            {
                fileName = "LeaveQuotaFile.xls";
            }
            filename = Server.MapPath(@"~\Gallery\SlideImages\" + fileName);
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch
                {
                    fileName = docid + fileName;
                }
            }
            upldExcelData.SaveAs(@filename);
            ReadDataFromExcel(filename, docid);
        }
    }

    protected void ReadDataFromExcel(string filename, string docid)
    {
        string lPersNo = "";
        string lCreatedOn = DateTime.Now.ToShortDateString(), lCreatedBy = Session["UserID"].ToString();
        string lNumber = "";
        string lAbsenceQuotaType = "";
        string lStartDate = "";
        string lEndDate = "";
        string lDedFrom = lStartDate;
        string lDedTo = lEndDate;
        string lChangedby = "";
        string lDeduction = "0";
        string lBalance = "0";
        string ConnString = "";

        if (filename.Contains(".xlsx"))
        {
            ConnString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"", filename);
        }
        else if (filename.Contains(".xls"))
        {
            ConnString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"", filename);
        }

        OleDbConnection Conn = new OleDbConnection(ConnString);
        DataTable dtSheets = new DataTable();
        Conn.Open();
        dtSheets = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //DataRow drSheet = new DataRow();
        string sheetName = "";
        foreach (DataRow drSheet in dtSheets.Rows)
        {
            sheetName = drSheet["TABLE_NAME"].ToString();
            if (sheetName.ToLower().Contains("quota"))
            {
                break;
            }
        }
        //sheetName = dtSheets.Rows[0]["Table_Name"].ToString();

        string CmdText = "select * from [" + sheetName + "A1:B1501]"; // where [" + sheetName + "A2:A1501].[Emp. No] IS NOT NULL";

        OleDbCommand Cmd = new OleDbCommand(CmdText, Conn);
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        string ReaderError = "";
        try
        {
            OleDbDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable dtExcelData = new DataTable();
            dtExcelData.Load(reader);
            DataTable dtTrans = new DataTable();
            DataRow row = null;
            int excelrow = 1;
            DataColumn Doc_ID = new DataColumn("Doc_ID", typeof(Int32));
            DataColumn PersNo = new DataColumn("PersNo", typeof(string));
            DataColumn AbsenceQuotaType = new DataColumn("AbsenceQuotaType", typeof(string));
            DataColumn StartDate = new DataColumn("StartDate", typeof(string));
            DataColumn EndDate = new DataColumn("EndDate", typeof(string));
            DataColumn Changedby = new DataColumn("Changedby", typeof(string));
            DataColumn Number = new DataColumn("Number", typeof(string));
            DataColumn Deduction = new DataColumn("Deduction", typeof(string));
            DataColumn Balance = new DataColumn("Balance", typeof(string));
            DataColumn DedFrom = new DataColumn("DedFrom", typeof(string));
            DataColumn DedTo = new DataColumn("DedTo", typeof(string));
            DataColumn createdBy = new DataColumn("createdBy", typeof(string));
            DataColumn createdOn = new DataColumn("createdOn", typeof(DateTime));

            dtTrans.Columns.Add(Doc_ID);
            dtTrans.Columns.Add(PersNo);
            dtTrans.Columns.Add(AbsenceQuotaType);
            dtTrans.Columns.Add(StartDate);
            dtTrans.Columns.Add(EndDate);
            dtTrans.Columns.Add(Changedby);
            dtTrans.Columns.Add(Number);
            dtTrans.Columns.Add(Deduction);
            dtTrans.Columns.Add(Balance);
            dtTrans.Columns.Add(DedFrom);
            dtTrans.Columns.Add(DedTo);
            dtTrans.Columns.Add(createdBy);
            dtTrans.Columns.Add(createdOn);
            for (int i = 0; i < dtExcelData.Rows.Count; i++)
            {
                excelrow++;
                lPersNo = dtExcelData.Rows[i][0].ToString();
                lNumber = dtExcelData.Rows[i][1].ToString();
                lCreatedBy = Session["UserID"].ToString();
                if (lPersNo.Trim().Length == 0 && lNumber.Trim().Length == 0)
                {
                    //Nothing to do blank Line
                }
                else if (lPersNo.Trim().Length == 0)
                {
                    ReaderError = "Employee ID null at line Number " + excelrow.ToString() + " Cannot not proceed.";
                    break;
                }
                else if(lNumber.Trim().Length == 0)
                {
                    ReaderError = "Quota value is null at line Number " + excelrow.ToString() + " Cannot not proceed.";
                    break;
                }
                else 
                {
                    if (!isDecimal(lNumber))
                    {
                        ReaderError = "Quota value should be a numeric value. Row Number: " + excelrow.ToString() + " Cannot not proceed.";
                        break;
                    }
                    lAbsenceQuotaType = ddlType.SelectedItem.Value.ToString();
                    lStartDate = ddlYear.SelectedItem.Value.ToString() + "-01-01";
                    lEndDate = ddlYear.SelectedItem.Value.ToString() + "-12-31";
                    lDedFrom = lStartDate;
                    lDedTo = lEndDate;
                    lChangedby = "";

                    lDeduction = "0";
                    lBalance = "0";

                    row = dtTrans.NewRow();
                    row["Doc_ID"] = System.Convert.ToInt32(docid);
                    row["PersNo"] = lPersNo;
                    row["AbsenceQuotaType"] = lAbsenceQuotaType;
                    row["StartDate"] = lStartDate;
                    row["EndDate"] = lEndDate;
                    row["Changedby"] = lChangedby;
                    row["Number"] = lNumber;
                    row["Deduction"] = lDeduction;
                    row["Balance"] = lBalance;
                    row["DedFrom"] = lDedFrom;
                    row["DedTo"] = lDedTo;
                    row["createdBy"] = lCreatedBy;
                    row["createdOn"] = lCreatedOn;
                    dtTrans.Rows.Add(row);
                }
            }
            dtExcelData.Clear();
            reader.Close();

            if (ReaderError.Length > 0)
            {
                if (con != null)
                {
                    con.Close();
                }
                ShowMessage(divListError, "Error: ", ReaderError.ToString());
                return;
            }

            SqlBulkCopy bulkCopy = new SqlBulkCopy(con);
            //Adding Excel data to temporary data
            bulkCopy.DestinationTableName = "LeaveQuotaDetails_ExcelData";
            bulkCopy.ColumnMappings.Add("Doc_ID", "Doc_ID");
            bulkCopy.ColumnMappings.Add("PersNo", "PersNo");
            bulkCopy.ColumnMappings.Add("AbsenceQuotaType", "AbsenceQuotaType");
            bulkCopy.ColumnMappings.Add("StartDate", "StartDate");
            bulkCopy.ColumnMappings.Add("EndDate", "EndDate");
            bulkCopy.ColumnMappings.Add("Changedby", "Changedby");
            bulkCopy.ColumnMappings.Add("Number", "Number");
            bulkCopy.ColumnMappings.Add("Deduction", "Deduction");
            bulkCopy.ColumnMappings.Add("Balance", "Balance");
            bulkCopy.ColumnMappings.Add("DedFrom", "DedFrom");
            bulkCopy.ColumnMappings.Add("DedTo", "DedTo");
            bulkCopy.ColumnMappings.Add("createdBy", "createdBy");
            bulkCopy.ColumnMappings.Add("createdOn", "createdOn");

            try
            {
                // Write from the source to the destination.       
                bulkCopy.WriteToServer(dtTrans);
                //Update DB Here
                if (UpdateQuota(rdUplType.SelectedItem.Value.ToString(), ddlType.SelectedItem.Value.ToString(), ddlYear.SelectedItem.Value.ToString()))
                {
                    ShowMessage(divListSuccess, "Success: ", "Quota updated successfully."); //"Excel data should be in the first sheet & the name of the sheet should be 'Sheet1'";
                }
                else
                {
                    ShowMessage(divListError, "Error: ", "Quota Upload failed. Please validate excel data"); 
                }
                //Rebind year dropdown to get active year.
                BindYear(ddlYear, rdUplType.SelectedItem.Value.ToString());
            }
            catch (Exception ex)
            {
                ShowMessage(divListError, "Error: ", "Quota Upload failed. Please validate excel data. Error: " + ex.Message.ToString()); 
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                bulkCopy.Close();
            }
           
        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().ToLower().Contains("external table is not in the expected format"))
            {
                ShowMessage(divListError, "Error: ", "Please upload excel file. Excel data should be in the first sheet & the name of the first sheet should be 'quota'"); //"Excel data should be in the first sheet & the name of the sheet should be 'Sheet1'";
            }
            else
            {
                ShowMessage(divListError, "Error: ", ex.Message.ToString()); //"Excel data should be in the first sheet & the name of the sheet should be 'Sheet1'";
            }
        }
        finally
        {
            Conn.Close();
            if (con != null)
            {
                con.Close();
            }
            if (File.Exists(@filename))
            {
                File.Delete(filename);
            }
        }
    }

    private bool isDecimal(string value)
    {
        try
        {
            if (value.Trim().Length == 0)
                value = "0";
            decimal newValue = System.Convert.ToDecimal(value);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

 
    private void BindYear(DropDownList ddl1, string type="")
    {
        try
        {
            string activeYear = getActiveYear();
            ddl1.Enabled = true;
            ddl1.Items.Clear();
            if (type == "bulk")
            {
                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem();
                item1.Text = "--Choose--";
                item1.Value = "";
                ddl1.Items.Add(item1);
                for (int i = (System.Convert.ToInt32(activeYear)); i <= System.DateTime.Now.Year + 1; i++)
                {
                    System.Web.UI.WebControls.ListItem item2 = new System.Web.UI.WebControls.ListItem();
                    item2.Text = i.ToString();
                    item2.Value = i.ToString();
                    ddl1.Items.Add(item2);
                }
            }
            else if (type == "single")
            {
                
                System.Web.UI.WebControls.ListItem item2 = new System.Web.UI.WebControls.ListItem();
                item2.Text = "Active Quota Year - (" + activeYear + ")";
                item2.Value = activeYear;
                ddl1.Items.Add(item2);
            }
            else
            {
                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem();
                item1.Text = "---------------";
                item1.Value = "";
                ddl1.Items.Add(item1);
                ddl1.Enabled = false;
            }
            //ddl1.SelectedValue = System.DateTime.Now.Year.ToString();
        }
        catch { }
    }


    protected void rdUplType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindYear(ddlYear, rdUplType.SelectedItem.Value.ToString());
    }

}