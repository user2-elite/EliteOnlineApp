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

public partial class adminSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        gvPublishedData.Visible = false;
        gvFiles.Visible = false;
        divPublishedTitle.InnerHtml = "";
        if (!Page.IsPostBack)
        {
            DeleteTemporaryData();
            BindMonth(ddlSalMonth);
            BindYear(ddlSalYear);
            ConfDB objConfDB = new ConfDB();
            objConfDB.BindUnit(ddCompanyUnit);
            btnUpdate.Visible = false;
            btnUpdate1.Visible = false;
            if (Session["selectedRole"] != null)
            {
                if (Session["selectedRole"].ToString() != "1")
                {
                    Response.Redirect("home.aspx");
                }
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

    protected void btnGetPublishedData_Click(object sender, EventArgs e)
    {
        GetPublishedData();
    }
    
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        gvFinalData.Visible = false;
        divFinalData.Visible = false;
        if (!upldExcelData.HasFile)
        {
            ShowMessage(divListError, "Error: ", "Please upload the excel file");
            return;
        }

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        string str = "select (isnull(max(Doc_ID),0)+1) from INT_SAL_Attach_Docs";
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
            if (upldExcelData.FileName.Contains(".xlsx") || upldExcelData.FileName.Contains(".xls"))
            {
                UploadExcel();
                ExtractDataFromExcel(docid);
                ShowFileDetails();
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
            SqlCommand cmd = new SqlCommand("INT_SAL_Delete_TemporaryData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@createdBy", Session["UserID"].ToString());
            cmd.ExecuteNonQuery();
            con.Close();
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
                SqlCommand cmd = new SqlCommand("INT_SAL_Insert_Attach_Docs", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Doc_AttachedBy", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Doc_FileName", upldExcelData.FileName.ToString());
                cmd.ExecuteNonQuery();
            }
            con.Close();
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }
    }

    protected void ExtractDataFromExcel(string docid)
    {
        string filename = "";
        if (upldExcelData.HasFile)
        {
            string fileName = "";
            if (upldExcelData.FileName.Contains(".xlsx"))
            {
                fileName = "DataFile.xlsx";
            }
            else
            {
                fileName = "DataFile.xls";
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

        int sDoc_ID = System.Convert.ToInt32(docid);
        string sEmpCode = "", sMonth = "", sYear = "", sBank = "";
        string sAccNo = "", sEmployeeName = "", sDesignation = "", sCompanyUnit = "", sBasic = "0";
        string sHRA = "0", sTA = "0", sPFDeduction = "0", sEarningTotal = "0", sDeductionTotal = "0";
        string sNetPay = "0", sClaims = "0";
        string sTS = "0", sSpecialAllowance = "0", sArrears = "0", sLOP = "0", sAllocatedCL = "0", sUtilizedCL = "0";
        string sAllocatedSL = "0", sUtilizedSL = "0", sAllocatedEL = "0", sUtilizedEL = "0";
        string sESI = "0", sFestivalAdvance = "0", sSalaryAdvance = "0", sProfessionalTax = "0", sTozhilaliKshemaNidhi = "0";
        string sLWF = "0", sTDS = "0", sLIC = "0", sLWF1 = "0";
        string sCreatedOn = DateTime.Now.ToShortDateString(), sCreatedBy = Session["UserID"].ToString();
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
            if (sheetName.ToLower().Contains("salary"))
            {
                break;
            }
        }
        //sheetName = dtSheets.Rows[0]["Table_Name"].ToString();

        string CmdText = "select * from [" + sheetName + "A4:AC2000]"; // where [" + sheetName + "A6:AC2000].[Emp. No] IS NOT NULL";

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
            int excelrow = 4;
            DataColumn Doc_ID = new DataColumn("Doc_ID", typeof(Int32));
            DataColumn EmpCode = new DataColumn("EmpCode", typeof(string));
            DataColumn Month = new DataColumn("Month", typeof(string));
            DataColumn Year = new DataColumn("Year", typeof(string));
            DataColumn Bank = new DataColumn("Bank", typeof(string));
            DataColumn AccNo = new DataColumn("AccNo", typeof(string));
            DataColumn EmployeeName = new DataColumn("EmployeeName", typeof(string));
            DataColumn Designation = new DataColumn("Designation", typeof(string));
            DataColumn CompanyUnit = new DataColumn("CompanyUnit", typeof(string));
            DataColumn Basic = new DataColumn("Basic", typeof(Decimal));
            DataColumn HRA = new DataColumn("HRA", typeof(Decimal));
            DataColumn TA = new DataColumn("TA", typeof(Decimal));
            DataColumn TS = new DataColumn("TS", typeof(Decimal));
            DataColumn SpecialAllowance = new DataColumn("SpecialAllowance", typeof(Decimal));
            DataColumn Arrears = new DataColumn("Arrears", typeof(Decimal));
            DataColumn LOP = new DataColumn("LOP", typeof(Decimal));
            DataColumn AllocatedCL = new DataColumn("AllocatedCL", typeof(Decimal));
            DataColumn UtilizedCL = new DataColumn("UtilizedCL", typeof(Decimal));
            DataColumn AllocatedSL = new DataColumn("AllocatedSL", typeof(Decimal));
            DataColumn UtilizedSL = new DataColumn("UtilizedSL", typeof(Decimal));
            DataColumn AllocatedEL = new DataColumn("AllocatedEL", typeof(Decimal));
            DataColumn UtilizedEL = new DataColumn("UtilizedEL", typeof(Decimal));
            DataColumn ESI = new DataColumn("ESI", typeof(Decimal));
            DataColumn FestivalAdvance = new DataColumn("FestivalAdvance", typeof(Decimal));
            DataColumn SalaryAdvance = new DataColumn("SalaryAdvance", typeof(Decimal));
            DataColumn ProfessionalTax = new DataColumn("ProfessionalTax", typeof(Decimal));
            DataColumn TozhilaliKshemaNidhi = new DataColumn("TozhilaliKshemaNidhi", typeof(Decimal));
            DataColumn LWF = new DataColumn("LWF", typeof(Decimal));
            DataColumn TDS = new DataColumn("TDS", typeof(Decimal));
            DataColumn LIC = new DataColumn("LIC", typeof(Decimal));
            DataColumn LWF1 = new DataColumn("LWF1", typeof(Decimal));

            DataColumn PFDeduction = new DataColumn("PFDeduction", typeof(Decimal));
            DataColumn EarningTotal = new DataColumn("EarningTotal", typeof(Decimal));
            DataColumn DeductionTotal = new DataColumn("DeductionTotal", typeof(Decimal));
            DataColumn NetPay = new DataColumn("NetPay", typeof(Decimal));
            DataColumn Claims = new DataColumn("Claims", typeof(Decimal));
            DataColumn createdOn = new DataColumn("createdOn", typeof(DateTime));
            DataColumn createdBy = new DataColumn("createdBy", typeof(string));
            dtTrans.Columns.Add(Doc_ID);
            dtTrans.Columns.Add(EmpCode);
            dtTrans.Columns.Add(Month);
            dtTrans.Columns.Add(Year);
            dtTrans.Columns.Add(Bank);
            dtTrans.Columns.Add(AccNo);
            dtTrans.Columns.Add(EmployeeName);
            dtTrans.Columns.Add(Designation);
            dtTrans.Columns.Add(CompanyUnit);
            dtTrans.Columns.Add(Basic);
            dtTrans.Columns.Add(HRA);
            dtTrans.Columns.Add(TA);
            dtTrans.Columns.Add(TS);
            dtTrans.Columns.Add(SpecialAllowance);
            dtTrans.Columns.Add(Arrears);
            dtTrans.Columns.Add(LOP);
            dtTrans.Columns.Add(AllocatedCL);
            dtTrans.Columns.Add(UtilizedCL);
            dtTrans.Columns.Add(AllocatedSL);
            dtTrans.Columns.Add(UtilizedSL);
            dtTrans.Columns.Add(AllocatedEL);
            dtTrans.Columns.Add(UtilizedEL);
            dtTrans.Columns.Add(ESI);
            dtTrans.Columns.Add(FestivalAdvance);
            dtTrans.Columns.Add(SalaryAdvance);
            dtTrans.Columns.Add(ProfessionalTax);
            dtTrans.Columns.Add(TozhilaliKshemaNidhi);
            dtTrans.Columns.Add(LWF);
            dtTrans.Columns.Add(TDS);
            dtTrans.Columns.Add(LIC);
            dtTrans.Columns.Add(LWF1);
            dtTrans.Columns.Add(PFDeduction);
            dtTrans.Columns.Add(EarningTotal);
            dtTrans.Columns.Add(DeductionTotal);
            dtTrans.Columns.Add(NetPay);
            dtTrans.Columns.Add(Claims);
            dtTrans.Columns.Add(createdOn);
            dtTrans.Columns.Add(createdBy);
            for (int i = 0; i < dtExcelData.Rows.Count; i++)
            {
                excelrow++;
                sEmpCode = dtExcelData.Rows[i][0].ToString();
                if (sEmpCode.Trim().Trim().Length > 0)
                {
                    sMonth = ddlSalMonth.SelectedItem.Value.ToString();
                    sYear = ddlSalYear.SelectedItem.Value.ToString();
                    sEmployeeName = dtExcelData.Rows[i][1].ToString();
                    sDesignation = dtExcelData.Rows[i][2].ToString();
                    sBank = ""; // dtExcelData.Rows[i][1].ToString();
                    sAccNo = ""; // dtExcelData.Rows[i][2].ToString();
                    sCompanyUnit = ddCompanyUnit.SelectedItem.Text.ToString();
                    sBasic = dtExcelData.Rows[i][3].ToString();
                    sHRA = dtExcelData.Rows[i][4].ToString();
                    sTA = dtExcelData.Rows[i][5].ToString();
                    sTS = dtExcelData.Rows[i][6].ToString();
                    sSpecialAllowance = dtExcelData.Rows[i][7].ToString();
                    sArrears = dtExcelData.Rows[i][8].ToString();
                    sLOP = dtExcelData.Rows[i][9].ToString();
                    sAllocatedCL = dtExcelData.Rows[i][10].ToString();
                    sUtilizedCL = dtExcelData.Rows[i][11].ToString();
                    sAllocatedSL = dtExcelData.Rows[i][12].ToString();
                    sUtilizedSL = dtExcelData.Rows[i][13].ToString();
                    sAllocatedEL = dtExcelData.Rows[i][14].ToString();
                    sUtilizedEL = dtExcelData.Rows[i][15].ToString();
                    sEarningTotal = dtExcelData.Rows[i][16].ToString();
                    sPFDeduction = dtExcelData.Rows[i][17].ToString();
                    sESI = dtExcelData.Rows[i][18].ToString();
                    sFestivalAdvance = dtExcelData.Rows[i][19].ToString();
                    sSalaryAdvance = dtExcelData.Rows[i][20].ToString();
                    sProfessionalTax = dtExcelData.Rows[i][21].ToString();
                    sTozhilaliKshemaNidhi = dtExcelData.Rows[i][22].ToString();
                    sLWF = dtExcelData.Rows[i][23].ToString();
                    sTDS = dtExcelData.Rows[i][24].ToString();
                    sLIC = dtExcelData.Rows[i][25].ToString();
                    sLWF1 = dtExcelData.Rows[i][26].ToString();
                    sDeductionTotal = dtExcelData.Rows[i][27].ToString();
                    sNetPay = dtExcelData.Rows[i][28].ToString();
                    sClaims = "0";

                    if (sEmpCode.ToString().Trim().Length == 0 && sNetPay.ToString().Trim().Length == 0)
                    {
                        //Nothing to do
                    }
                    else
                    {
                        if (sEmpCode.ToString().Trim().Length == 0 //|| sMonth.ToString().Trim().Length == 0 || sYear.ToString().Trim().Length == 0 || sBank.ToString().Trim().Length == 0 || sAccNo.ToString().Trim().Length == 0 
                           || sEmployeeName.ToString().Trim().Length == 0 || sDesignation.ToString().Trim().Length == 0 || sCompanyUnit.ToString().Trim().Length == 0 || sBasic.ToString().Trim().Length == 0 || sHRA.ToString().Trim().Length == 0 //|| sTA.ToString().Trim().Length == 0
                            || sPFDeduction.ToString().Trim().Length == 0 || sEarningTotal.ToString().Trim().Length == 0 || sDeductionTotal.ToString().Trim().Length == 0 || sNetPay.ToString().Trim().Length == 0)
                        {

                            string MissingFields = "";

                            if (sEmpCode.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Employee Code";

                            if (sEmployeeName.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Employee Name";
                            if (sDesignation.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Designation";
                            if (sCompanyUnit.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Company Unit";
                            if (sBasic.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Basic";
                            if (sHRA.ToString().Trim().Length == 0)
                                MissingFields += "<BR>HRA";
                            if (sPFDeduction.ToString().Trim().Length == 0)
                                MissingFields += "<BR>PF Deductions";
                            if (sEarningTotal.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Earning Total";
                            if (sDeductionTotal.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Deduction Total";
                            if (sNetPay.ToString().Trim().Length == 0)
                                MissingFields += "<BR>Net Pay Amount";

                            ReaderError = "Mandatory fields are not filled in the row number " + excelrow.ToString() + ". Missing Fields are:-<B>" + MissingFields + "</B><BR><I>If the cell is not empty and still you are experiencing the issue, please check the format of the cell.</I>";
                            break;
                        }
                        if (!isDecimal(sBasic))
                        {
                            ReaderError = "Basic should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }

                        if (!isDecimal(sHRA))
                        {
                            ReaderError = "HRA should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sTA))
                        {
                            ReaderError = "TA should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sPFDeduction))
                        {
                            ReaderError = "PFDeduction should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sEarningTotal))
                        {
                            ReaderError = "EarningTotal should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sDeductionTotal))
                        {
                            ReaderError = "DeductionTotal should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sNetPay))
                        {
                            ReaderError = "NetPay should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sClaims))
                        {
                            ReaderError = "Claims should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sTS))
                        {
                            ReaderError = "TS should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sSpecialAllowance))
                        {
                            ReaderError = "SpecialAllowance should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sArrears))
                        {
                            ReaderError = "Arrears should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sLOP))
                        {
                            ReaderError = "LOP should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sAllocatedCL))
                        {
                            ReaderError = "AllocatedCL should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sUtilizedCL))
                        {
                            ReaderError = "UtilizedCL should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sAllocatedSL))
                        {
                            ReaderError = "AllocatedSL should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sUtilizedSL))
                        {
                            ReaderError = "UtilizedSL should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sAllocatedEL))
                        {
                            ReaderError = "AllocatedEL should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sUtilizedEL))
                        {
                            ReaderError = "UtilizedEL should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sESI))
                        {
                            ReaderError = "ESI should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sFestivalAdvance))
                        {
                            ReaderError = "FestivalAdvance should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sSalaryAdvance))
                        {
                            ReaderError = "SalaryAdvance should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sProfessionalTax))
                        {
                            ReaderError = "ProfessionalTax should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sTozhilaliKshemaNidhi))
                        {
                            ReaderError = "TozhilaliKshemaNidhi should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sLWF))
                        {
                            ReaderError = "LWF should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sTDS))
                        {
                            ReaderError = "TDS should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sLIC))
                        {
                            ReaderError = "LIC should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        if (!isDecimal(sLWF1))
                        {
                            ReaderError = "LWF1 should be a numeric value. Row Number: " + excelrow.ToString();
                            break;
                        }
                        row = dtTrans.NewRow();
                        row["Doc_ID"] = sDoc_ID;
                        row["EmpCode"] = sEmpCode;
                        row["Month"] = sMonth;
                        row["Year"] = sYear;
                        row["Bank"] = sBank;
                        row["AccNo"] = sAccNo;
                        row["EmployeeName"] = sEmployeeName;
                        row["Designation"] = sDesignation;
                        row["CompanyUnit"] = sCompanyUnit;
                        row["Basic"] = sBasic;
                        row["HRA"] = sHRA;
                        if (sTA.Trim().Length > 0)
                            row["TA"] = System.Convert.ToDecimal(sTA);
                        else
                            row["TA"] = 0;
                        if (sTS.Trim().Length > 0)
                            row["TS"] = System.Convert.ToDecimal(sTS);
                        else
                            row["TS"] = 0;

                        if (sSpecialAllowance.Trim().Length > 0)
                            row["SpecialAllowance"] = System.Convert.ToDecimal(sSpecialAllowance);
                        else
                            row["SpecialAllowance"] = 0;

                        if (sArrears.Trim().Length > 0)
                            row["Arrears"] = System.Convert.ToDecimal(sArrears);
                        else
                            row["Arrears"] = 0;
                        if (sLOP.Trim().Length > 0)
                            row["LOP"] = System.Convert.ToDecimal(sLOP);
                        else
                            row["LOP"] = 0;
                        if (sAllocatedCL.Trim().Length > 0)
                            row["AllocatedCL"] = System.Convert.ToDecimal(sAllocatedCL);
                        else
                            row["AllocatedCL"] = 0;
                        if (sUtilizedCL.Trim().Length > 0)
                            row["UtilizedCL"] = System.Convert.ToDecimal(sUtilizedCL);
                        else
                            row["UtilizedCL"] = 0;
                        if (sAllocatedSL.Trim().Length > 0)
                            row["AllocatedSL"] = System.Convert.ToDecimal(sAllocatedSL);
                        else
                            row["AllocatedSL"] = 0;
                        if (sUtilizedSL.Trim().Length > 0)
                            row["UtilizedSL"] = System.Convert.ToDecimal(sUtilizedSL);
                        else
                            row["UtilizedSL"] = 0;
                        if (sAllocatedEL.Trim().Length > 0)
                            row["AllocatedEL"] = System.Convert.ToDecimal(sAllocatedEL);
                        else
                            row["AllocatedEL"] = 0;
                        if (sUtilizedEL.Trim().Length > 0)
                            row["UtilizedEL"] = System.Convert.ToDecimal(sUtilizedEL);
                        else
                            row["UtilizedEL"] = 0;
                        if (sESI.Trim().Length > 0)
                            row["ESI"] = System.Convert.ToDecimal(sESI);
                        else
                            row["ESI"] = 0;
                        if (sFestivalAdvance.Trim().Length > 0)
                            row["FestivalAdvance"] = System.Convert.ToDecimal(sFestivalAdvance);
                        else
                            row["FestivalAdvance"] = 0;
                        if (sSalaryAdvance.Trim().Length > 0)
                            row["SalaryAdvance"] = System.Convert.ToDecimal(sSalaryAdvance);
                        else
                            row["SalaryAdvance"] = 0;
                        if (sProfessionalTax.Trim().Length > 0)
                            row["ProfessionalTax"] = System.Convert.ToDecimal(sProfessionalTax);
                        else
                            row["ProfessionalTax"] = 0;
                        if (sTozhilaliKshemaNidhi.Trim().Length > 0)
                            row["TozhilaliKshemaNidhi"] = System.Convert.ToDecimal(sTozhilaliKshemaNidhi);
                        else
                            row["TozhilaliKshemaNidhi"] = 0;
                        if (sLWF.Trim().Length > 0)
                            row["LWF"] = System.Convert.ToDecimal(sLWF);
                        else
                            row["LWF"] = 0;
                        if (sTDS.Trim().Length > 0)
                            row["TDS"] = System.Convert.ToDecimal(sTDS);
                        else
                            row["TDS"] = 0;
                        if (sLIC.Trim().Length > 0)
                            row["LIC"] = System.Convert.ToDecimal(sLIC);
                        else
                            row["LIC"] = 0;
                        if (sLWF1.Trim().Length > 0)
                            row["LWF1"] = System.Convert.ToDecimal(sLWF1);
                        else
                            row["LWF1"] = 0;
                        if (sPFDeduction.Trim().Length > 0)
                            row["PFDeduction"] = System.Convert.ToDecimal(sPFDeduction);
                        else
                            row["PFDeduction"] = 0;
                        if (sEarningTotal.Trim().Length > 0)
                            row["EarningTotal"] = System.Convert.ToDecimal(sEarningTotal);
                        else
                            row["EarningTotal"] = 0;
                        if (sDeductionTotal.Trim().Length > 0)
                            row["DeductionTotal"] = System.Convert.ToDecimal(sDeductionTotal);
                        else
                            row["DeductionTotal"] = 0;
                        if (sNetPay.Trim().Length > 0)
                            row["NetPay"] = System.Convert.ToDecimal(sNetPay);
                        else
                            row["NetPay"] = 0;
                        if (sClaims.Trim().Length > 0)
                            row["Claims"] = System.Convert.ToDecimal(sClaims);
                        else
                            row["Claims"] = 0;

                        row["createdOn"] = sCreatedOn;
                        row["createdBy"] = sCreatedBy;
                        dtTrans.Rows.Add(row);
                    }
                }
            }
            dtExcelData.Clear();
            reader.Close();
            Conn.Close();

            if (ReaderError.Length > 0)
            {
                ShowMessage(divListError, "Error: ", ReaderError.ToString());
                return;
            }
            SqlBulkCopy bulkCopy = new SqlBulkCopy(con);
            //Adding Excel data to temporary data
            bulkCopy.DestinationTableName = "INT_SAL_TemporaryData";
            bulkCopy.ColumnMappings.Add("Doc_ID", "Doc_ID");
            bulkCopy.ColumnMappings.Add("EmpCode", "EmpCode");
            bulkCopy.ColumnMappings.Add("Month", "Month");
            bulkCopy.ColumnMappings.Add("Year", "Year");
            bulkCopy.ColumnMappings.Add("Bank", "Bank");
            bulkCopy.ColumnMappings.Add("AccNo", "AccNo");
            bulkCopy.ColumnMappings.Add("EmployeeName", "EmployeeName");
            bulkCopy.ColumnMappings.Add("Designation", "Designation");
            bulkCopy.ColumnMappings.Add("CompanyUnit", "CompanyUnit");
            bulkCopy.ColumnMappings.Add("Basic", "Basic");
            bulkCopy.ColumnMappings.Add("HRA", "HRA");
            bulkCopy.ColumnMappings.Add("TA", "TA");

            bulkCopy.ColumnMappings.Add("TS", "TS");
            bulkCopy.ColumnMappings.Add("SpecialAllowance", "SpecialAllowance");
            bulkCopy.ColumnMappings.Add("Arrears", "Arrears");
            bulkCopy.ColumnMappings.Add("LOP", "LOP");
            bulkCopy.ColumnMappings.Add("AllocatedCL", "AllocatedCL");
            bulkCopy.ColumnMappings.Add("UtilizedCL", "UtilizedCL");
            bulkCopy.ColumnMappings.Add("AllocatedSL", "AllocatedSL");
            bulkCopy.ColumnMappings.Add("UtilizedSL", "UtilizedSL");
            bulkCopy.ColumnMappings.Add("AllocatedEL", "AllocatedEL");
            bulkCopy.ColumnMappings.Add("UtilizedEL", "UtilizedEL");
            bulkCopy.ColumnMappings.Add("ESI", "ESI");
            bulkCopy.ColumnMappings.Add("FestivalAdvance", "FestivalAdvance");
            bulkCopy.ColumnMappings.Add("SalaryAdvance", "SalaryAdvance");
            bulkCopy.ColumnMappings.Add("ProfessionalTax", "ProfessionalTax");
            bulkCopy.ColumnMappings.Add("TozhilaliKshemaNidhi", "TozhilaliKshemaNidhi");
            bulkCopy.ColumnMappings.Add("LWF", "LWF");
            bulkCopy.ColumnMappings.Add("TDS", "TDS");
            bulkCopy.ColumnMappings.Add("LIC", "LIC");
            bulkCopy.ColumnMappings.Add("LWF1", "LWF1");

            bulkCopy.ColumnMappings.Add("PFDeduction", "PFDeduction");
            bulkCopy.ColumnMappings.Add("EarningTotal", "EarningTotal");
            bulkCopy.ColumnMappings.Add("DeductionTotal", "DeductionTotal");
            bulkCopy.ColumnMappings.Add("NetPay", "NetPay");
            bulkCopy.ColumnMappings.Add("Claims", "Claims");
            bulkCopy.ColumnMappings.Add("createdOn", "createdOn");
            bulkCopy.ColumnMappings.Add("createdBy", "createdBy");

            try
            {
                // Write from the source to the destination.       
                bulkCopy.WriteToServer(dtTrans);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                bulkCopy.Close();
            }
            //updating data
            ViewState["DocID"] = sDoc_ID.ToString();
            SqlCommand cmd = new SqlCommand("INT_SAL_GetTemporaryData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DocId", sDoc_ID.ToString());
            SqlDataAdapter daListing = new SqlDataAdapter();
            daListing.SelectCommand = cmd;
            DataSet dsListing = new DataSet();
            daListing.Fill(dsListing);
            if (dsListing.Tables[0].Rows.Count > 0)
            {
                gvFinalData.DataSource = dsListing.Tables[0];
                gvFinalData.DataBind();
                gvFinalData.Visible = true;
                btnUpdate.Visible = true;
                btnUpdate1.Visible = true;
                divFinalData.Visible = true;
            }
            else
            {
                gvFinalData.Visible = false;
                btnUpdate.Visible = false;
                btnUpdate1.Visible = false;
                divFinalData.Visible = false;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();

        }
        catch (Exception ex)
        {
            if (ex.Message.ToString().ToLower().Contains("external table is not in the expected format"))
            {
                ShowMessage(divListError, "Error: ", "Please upload excel file. Excel data should be in the first sheet & the name of the sheet should be 'Sheet1'"); //"Excel data should be in the first sheet & the name of the sheet should be 'Sheet1'";
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

    private void ShowFileDetails()
    {
        SqlConnection conn = null;
        SqlCommand cmd;
        string connstring = "";
        conn = new SqlConnection();
        try
        {
            connstring = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString();
            conn.ConnectionString = connstring;
            conn.Open();
            cmd = new SqlCommand("INT_SAL_Get_Docs_To_Extract", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Doc_AttachedBy", Session["UserID"].ToString());
            SqlDataAdapter daListing = new SqlDataAdapter();
            daListing.SelectCommand = cmd;
            DataSet dsListing = new DataSet();
            daListing.Fill(dsListing);

            if (dsListing.Tables[0].Rows.Count > 0)
            {
                gvFiles.DataSource = dsListing.Tables[0];
                gvFiles.DataBind();
                gvFiles.Visible = true;
            }
            else
            {
                gvFiles.Visible = false;
            }
            cmd.Parameters.Clear();
            cmd.Dispose();
        }
        catch
        {

        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int count = gvFinalData.Rows.Count;
            int CheckedCount = 0;
            int SuccessCount = 0;
            for (int i = 0; i < count; i++)
            {
                CheckBox chkApprove = new CheckBox();
                chkApprove = (CheckBox)gvFinalData.Rows[i].FindControl("chkSelection");
                if (chkApprove.Checked)
                {
                    CheckedCount += 1;
                    if (gvFinalData.Rows[i].Cells[2].Text.Trim().Length > 0)
                    {
                        string ID = gvFinalData.Rows[i].Cells[2].Text.Trim();
                        //string satatusDesc = gvFinalData.Rows[i].Cells[13].Text.Trim();
                        string CommandText = "";
                        CommandText = "INT_SAL_Update_Transaction";
                        if (CommandText.Length > 0)
                        {
                            using (SqlConnection connTrans = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
                            {
                                SqlCommand cmdTrans = new SqlCommand(CommandText, connTrans);
                                cmdTrans.Parameters.AddWithValue("@ID", ID);
                                cmdTrans.CommandType = CommandType.StoredProcedure;
                                connTrans.Open();
                                cmdTrans.ExecuteNonQuery();
                                connTrans.Close();
                                SuccessCount += 1;
                            }
                        }
                    }
                }
            }
            if (CheckedCount > 0)
            {
                string updStatus = "Total Selected :" + CheckedCount + " ";
                updStatus += "Total Updated :" + SuccessCount + ". verify the transactions. ";
                ShowMessage(divListSuccess, "Success: ", updStatus);
                try
                {
                    SqlConnection conn = null;
                    string connstring = "";
                    conn = new SqlConnection();
                    connstring = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString();
                    conn.ConnectionString = connstring;
                    conn.Open();

                    //Refershing the records with new status
                    SqlCommand cmd = new SqlCommand("INT_SAL_GetTemporaryData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DocId", ViewState["DocID"].ToString());
                    SqlDataAdapter daListing = new SqlDataAdapter();
                    daListing.SelectCommand = cmd;
                    DataSet dsListing = new DataSet();
                    daListing.Fill(dsListing);
                    if (dsListing.Tables[0].Rows.Count > 0)
                    {
                        gvFinalData.DataSource = dsListing.Tables[0];
                        gvFinalData.DataBind();
                        gvFinalData.Visible = true;
                        btnUpdate.Visible = true;
                        btnUpdate1.Visible = true;
                    }
                    else
                    {
                        gvFinalData.Visible = false;
                        btnUpdate.Visible = false;
                        btnUpdate1.Visible = false;
                    }
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    conn.Close();


                }
                catch { }

                //GetPublishedData();
            }
            else
            {
                ShowMessage(divListError, "Error: ", "Please select the transactions and click on the update button");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(divListError, "Error: ", "Error while updating the data. Description: " + ex.Message.ToString());
        }
        finally
        {

        }
    }

    protected void gvFinalData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName == "cmPreview")
            {
                //ViewState["EmpCode"] = ID.Trim();
                CreatePDF(ID.Trim(), "1");
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void GetPublishedData()
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("INT_SAL_GetPublishedData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Month", ddlSalMonth.SelectedItem.Value.ToString());
            cmd.Parameters.AddWithValue("@Year", ddlSalYear.SelectedItem.Value.ToString());
            cmd.Parameters.AddWithValue("@Unit", ddCompanyUnit.SelectedItem.Text.ToString());
            SqlDataAdapter daListing = new SqlDataAdapter();
            daListing.SelectCommand = cmd;
            DataSet dsListing = new DataSet();
            daListing.Fill(dsListing);
            gvPublishedData.DataSource = dsListing.Tables[0];
            gvPublishedData.DataBind();
            if (dsListing.Tables[0].Rows.Count > 0)
            {
                divPublishedTitle.InnerHtml = "<BR><B>Selected Company Unit: " + ddCompanyUnit.SelectedItem.Text.ToString() + ". Selected Period: " + ddlSalMonth.SelectedItem.Text.ToString() + ", " + ddlSalYear.SelectedItem.Value.ToString() + "</B>";
            }
            else
            {
                divPublishedTitle.InnerHtml = "";
            }
            gvPublishedData.Visible = true;            
            cmd.Parameters.Clear();
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            ShowMessage(divListError, "Error: ", "Could not load published data for the selected month, Year & Unit");

        }
        finally
        {
            con.Close();
            if (con != null)
            {
                con.Close();
            }
        }
    }

    protected void gvPublishedData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string ID = e.CommandArgument.ToString();
            if (e.CommandName == "cmPreview")
            {
                //ViewState["EmpCode"] = ID.Trim();
                CreatePDF(ID.Trim(),"2");
            }
            else if (e.CommandName == "cmDelete")
            {
                //ViewState["EmpCode"] = ID.Trim();
                DeleteData(ID.Trim());
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void DeleteData(string ID)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString());
        con.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("INT_SAL_DeletePublishedData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID.ToString());
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            ShowMessage(divListError, "Error: ", "Could not delete the details. Please try again later.");
        }
        catch (Exception ex)
        {
            ShowMessage(divListSuccess, "Error: ", "Successfully deleted the details.");
        }
        finally
        {
            con.Close();
            if (con != null)
            {
                con.Close();
            }
        }
    }


    private void CreatePDF(string ID, string Type)
    {
        DataTable dt1 = new DataTable();
        if(Type == "1")
        {
            dt1 = GetSalTemporaryData(ID);
        }
        else if (Type == "2")
        {
            dt1 = GetPublishedData(ID);
        }        
        
        if (dt1.Rows.Count > 0)
        {
            // Create a Document object
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            string Month = ddlSalMonth.SelectedItem.Value.ToString(); //System.DateTime.Now.Month.ToString();
            string Year = ddlSalYear.SelectedItem.Value.ToString(); //System.DateTime.Now.Year.ToString();
            // Create a new PdfWrite object, writing the output to a MemoryStream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            // Open the Document for writing
            document.Open();

            // Read in the contents of the Receipt.htm HTML template file
            string contents = File.ReadAllText(Server.MapPath("template.htm"));
            // Replace the placeholders with the user-specified text
            contents = contents.Replace("[name]", dt1.Rows[0]["EmployeeName"].ToString());
            contents = contents.Replace("[payperiod]", getMonthName(dt1.Rows[0]["Month"].ToString()) + ", " + dt1.Rows[0]["Year"].ToString());
            contents = contents.Replace("[empcode]", dt1.Rows[0]["EmpCode"].ToString());
            contents = contents.Replace("[bank]", dt1.Rows[0]["Bank"].ToString());
            contents = contents.Replace("[designation]", dt1.Rows[0]["Designation"].ToString());
            contents = contents.Replace("[companyunit]", dt1.Rows[0]["CompanyUnit"].ToString());
            contents = contents.Replace("[accno]", dt1.Rows[0]["AccNo"].ToString());

            contents = contents.Replace("[basic]", dt1.Rows[0]["Basic"].ToString());
            contents = contents.Replace("[pfdeductions]", dt1.Rows[0]["PFDeduction"].ToString());
            contents = contents.Replace("[hra]", dt1.Rows[0]["HRA"].ToString());
            contents = contents.Replace("[totalearning]", dt1.Rows[0]["EarningTotal"].ToString());
            contents = contents.Replace("[totaldeductions]", dt1.Rows[0]["DeductionTotal"].ToString());

            contents = contents.Replace("[claims]", dt1.Rows[0]["Claims"].ToString());
            contents = contents.Replace("[netpay]", dt1.Rows[0]["NetPay"].ToString());

            contents = contents.Replace("[clOpen]", dt1.Rows[0]["AllocatedCL"].ToString());
            contents = contents.Replace("[clTaken]", dt1.Rows[0]["UtilizedCL"].ToString());


            //--------------------------------------------------------------------------
            //Optional -----------------------------------------------------------------
            if (dt1.Rows[0]["TA"].ToString() != "0.00")
            {
                contents = contents.Replace("[TA]", dt1.Rows[0]["TA"].ToString());
                contents = contents.Replace("lblTA", "TA");
            }
            else
            {
                contents = contents.Replace("<br />[TA]", "");
                contents = contents.Replace("<br />lblTA", ""); ;
            }

            if (dt1.Rows[0]["TS"].ToString() != "0.00")
            {
                contents = contents.Replace("[TS]", dt1.Rows[0]["TS"].ToString());
                contents = contents.Replace("lblTS", "TS");
            }
            else
            {
                contents = contents.Replace("<br />[TS]", "");
                contents = contents.Replace("<br />lblTS", ""); ;
            }
            if (dt1.Rows[0]["SpecialAllowance"].ToString() != "0.00")
            {
                contents = contents.Replace("[SpecialAllowance]", dt1.Rows[0]["SpecialAllowance"].ToString());
                contents = contents.Replace("lblSpecialAllowance", "SpecialAllowance");
            }
            else
            {
                contents = contents.Replace("<br />[SpecialAllowance]", "");
                contents = contents.Replace("<br />lblSpecialAllowance", ""); ;
            }
            if (dt1.Rows[0]["Arrears"].ToString() != "0.00")
            {
                contents = contents.Replace("[Arrears]", dt1.Rows[0]["Arrears"].ToString());
                contents = contents.Replace("lblArrears", "Arrears");
            }
            else
            {
                contents = contents.Replace("<br />[Arrears]", "");
                contents = contents.Replace("<br />lblArrears", ""); ;
            }

            if (dt1.Rows[0]["ESI"].ToString() != "0.00")
            {
                contents = contents.Replace("[ESI]", dt1.Rows[0]["ESI"].ToString());
                contents = contents.Replace("lblESI", "ESI");
            }
            else
            {
                contents = contents.Replace("<br />[ESI]", "");
                contents = contents.Replace("<br />lblESI", ""); ;
            }


            if (dt1.Rows[0]["FestivalAdvance"].ToString() != "0.00")
            {
                contents = contents.Replace("[FestivalAdvance]", dt1.Rows[0]["FestivalAdvance"].ToString());
                contents = contents.Replace("lblFestivalAdvance", "FestivalAdvance");
            }
            else
            {
                contents = contents.Replace("<br />[FestivalAdvance]", "");
                contents = contents.Replace("<br />lblFestivalAdvance", ""); ;
            }

            if (dt1.Rows[0]["SalaryAdvance"].ToString() != "0.00")
            {
                contents = contents.Replace("[SalaryAdvance]", dt1.Rows[0]["SalaryAdvance"].ToString());
                contents = contents.Replace("lblSalaryAdvance", "SalaryAdvance");
            }
            else
            {
                contents = contents.Replace("<br />[SalaryAdvance]", "");
                contents = contents.Replace("<br />lblSalaryAdvance", ""); ;
            }

            if (dt1.Rows[0]["ProfessionalTax"].ToString() != "0.00")
            {
                contents = contents.Replace("[ProfessionalTax]", dt1.Rows[0]["ProfessionalTax"].ToString());
                contents = contents.Replace("lblProfessionalTax", "ProfessionalTax");
            }
            else
            {
                contents = contents.Replace("<br />[ProfessionalTax]", "");
                contents = contents.Replace("<br />lblProfessionalTax", ""); ;
            }

            if (dt1.Rows[0]["TozhilaliKshemaNidhi"].ToString() != "0.00")
            {
                contents = contents.Replace("[TozhilaliKshemaNidhi]", dt1.Rows[0]["TozhilaliKshemaNidhi"].ToString());
                contents = contents.Replace("lblTozhilaliKshemaNidhi", "TozhilaliKshemaNidhi");
            }
            else
            {
                contents = contents.Replace("<br />[TozhilaliKshemaNidhi]", "");
                contents = contents.Replace("<br />lblTozhilaliKshemaNidhi", ""); ;
            }

            if (dt1.Rows[0]["LWF"].ToString() != "0.00")
            {
                contents = contents.Replace("[LWF]", dt1.Rows[0]["LWF"].ToString());
                contents = contents.Replace("lblLWF", "LWF");
            }
            else
            {
                contents = contents.Replace("<br />[LWF]", "");
                contents = contents.Replace("<br />lblLWF", ""); ;
            }

            if (dt1.Rows[0]["TDS"].ToString() != "0.00")
            {
                contents = contents.Replace("[TDS]", dt1.Rows[0]["TDS"].ToString());
                contents = contents.Replace("lblTDS", "TDS");
            }
            else
            {
                contents = contents.Replace("<br />[TDS]", "");
                contents = contents.Replace("<br />lblTDS", ""); ;
            }

            if (dt1.Rows[0]["LIC"].ToString() != "0.00")
            {
                contents = contents.Replace("[LIC]", dt1.Rows[0]["LIC"].ToString());
                contents = contents.Replace("lblLIC", "LIC");
            }
            else
            {
                contents = contents.Replace("<br />[LIC]", "");
                contents = contents.Replace("<br />lblLIC", ""); ;
            }
            if (dt1.Rows[0]["LWF1"].ToString() != "0.00")
            {
                contents = contents.Replace("[1LWF]", dt1.Rows[0]["LWF1"].ToString());
                contents = contents.Replace("lbl1LWF", "LWF1");
            }
            else
            {
                contents = contents.Replace("<br />[1LWF]", "");
                contents = contents.Replace("<br />lbl1LWF", ""); ;
            }

            //--------------------------------------------------------------------------


            decimal CLBalnce;
            try
            {
                CLBalnce = System.Convert.ToDecimal(dt1.Rows[0]["AllocatedCL"]) - System.Convert.ToDecimal(dt1.Rows[0]["UtilizedCL"]);
            }
            catch
            {
                CLBalnce = 0;
            }
            contents = contents.Replace("[clBalance]", CLBalnce.ToString());

            contents = contents.Replace("[elOpen]", dt1.Rows[0]["AllocatedEL"].ToString());
            contents = contents.Replace("[elTaken]", dt1.Rows[0]["UtilizedEL"].ToString());
            decimal elBalance;
            try
            {
                elBalance = System.Convert.ToDecimal(dt1.Rows[0]["AllocatedEL"]) - System.Convert.ToDecimal(dt1.Rows[0]["UtilizedEL"]);
            }
            catch
            {
                elBalance = 0;
            }
            contents = contents.Replace("[elBalance]", elBalance.ToString());

            contents = contents.Replace("[slOpen]", dt1.Rows[0]["AllocatedSL"].ToString());
            contents = contents.Replace("[slTaken]", dt1.Rows[0]["UtilizedSL"].ToString());
            decimal slBalance;
            try
            {
                slBalance = System.Convert.ToDecimal(dt1.Rows[0]["AllocatedSL"]) - System.Convert.ToDecimal(dt1.Rows[0]["UtilizedSL"]);
            }
            catch
            {
                slBalance = 0;
            }
            contents = contents.Replace("[slBalance]", slBalance.ToString());

            contents = contents.Replace("[esiOpen]", "");
            contents = contents.Replace("[esiTaken]", "");
            contents = contents.Replace("[esiBalance]", "");

            contents = contents.Replace("[lopFOpen]", "");
            contents = contents.Replace("[lopFTaken]", dt1.Rows[0]["LOP"].ToString());
            contents = contents.Replace("[lopFBalance]", "");

            contents = contents.Replace("[lopHOpen]", "");
            contents = contents.Replace("[lopHTaken]", "");
            contents = contents.Replace("[lopHBalance]", "");

            var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);
            foreach (var htmlElement in parsedHtmlElements)
                document.Add(htmlElement as IElement);

            // You can add additional elements to the document. Let's add an image in the upper right corner
            //var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/logo.jpg"));
            //logo.SetAbsolutePosition(440, 800);
            //document.Add(logo);

            document.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename=payslip-{0}.pdf", Month + "" + Year));
            Response.BinaryWrite(output.ToArray());
        }
    }

    private DataTable GetSalTemporaryData(string ID)
    {
        try
        {
            SqlConnection conn = null;
            string connstring = "";
            conn = new SqlConnection();
            connstring = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString();
            conn.ConnectionString = connstring;
            conn.Open();
            SqlCommand cmd = new SqlCommand("INT_SAL_GetTemporaryData_ByID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID.ToString());
            SqlDataAdapter daListing = new SqlDataAdapter();
            daListing.SelectCommand = cmd;
            DataSet dsListing = new DataSet();
            daListing.Fill(dsListing);
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
            return dsListing.Tables[0];
        }
        catch
        {
            return null;
        }
    }

    private DataTable GetPublishedData(string ID)
    {
        try
        {
            SqlConnection conn = null;
            string connstring = "";
            conn = new SqlConnection();
            connstring = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString();
            conn.ConnectionString = connstring;
            conn.Open();
            SqlCommand cmd = new SqlCommand("INT_SAL_GetPublishedData_ByID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID.ToString());
            SqlDataAdapter daListing = new SqlDataAdapter();
            daListing.SelectCommand = cmd;
            DataSet dsListing = new DataSet();
            daListing.Fill(dsListing);
            cmd.Parameters.Clear();
            cmd.Dispose();
            conn.Close();
            return dsListing.Tables[0];
        }
        catch
        {
            return null;
        }
    }


    private void BindMonth(DropDownList ddl1)
    {
        try
        {
            for (int i = 1; i <= 12; i++)
            {
                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem();
                item1.Text = getMonthName(i.ToString());
                item1.Value = i.ToString();
                ddl1.Items.Add(item1);
            }
            ddl1.SelectedValue = System.DateTime.Now.Month.ToString();
        }
        catch { }
    }

    private void BindYear(DropDownList ddl1)
    {
        try
        {
            for (int i = (System.DateTime.Now.Year - 1); i <= System.DateTime.Now.Year; i++)
            {
                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem();
                item1.Text = i.ToString();
                item1.Value = i.ToString();
                ddl1.Items.Add(item1);
            }
            ddl1.SelectedValue = System.DateTime.Now.Year.ToString();
        }
        catch { }
    }

    private string getMonthName(string month)
    {
        if (month == "1")
        {
            return "January";
        }
        else if (month == "2")
        {
            return "February";
        }
        else if (month == "3")
        {
            return "March";
        }
        else if (month == "4")
        {
            return "April";
        }
        else if (month == "5")
        {
            return "May";
        }
        else if (month == "6")
        {
            return "June";
        }
        else if (month == "7")
        {
            return "July";
        }
        else if (month == "8")
        {
            return "August";
        }
        else if (month == "9")
        {
            return "September";
        }
        else if (month == "10")
        {
            return "October";
        }
        else if (month == "11")
        {
            return "November";
        }
        else if (month == "12")
        {
            return "December";
        }
        else
        {
            return month.ToString();
        }
    }

}