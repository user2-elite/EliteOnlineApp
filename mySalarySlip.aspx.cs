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

public partial class mySalarySlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clearMessages();
        if (Session["UserID"] != null && Session["LogonUserFullName"] != null)
        {
            if (!Page.IsPostBack)
            {
                BindMonth(ddlSalMonth);
                BindYear(ddlSalYear);
            }
        }
        else
        {
            Response.Redirect("default.aspx");
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

    protected void btnOpen_Click(object sender, EventArgs e)
    {
        CreatePDF(Session["UserID"].ToString());
    }

    private void CreatePDF(string UID)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = GetSalData(UID);

            if (dt1 == null)
            {
                ShowMessage(divListError, "Could not find the Salary Slip: ", "You may verify the Year/Month selected or contact administrator to verify that the salary details are updated on portal against your Employee Code.");
                return;
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
            else
            {
                ShowMessage(divListError, "No Data: ", "Could not find the Salary Slip for the Month - '<B>" + getMonthName(ddlSalMonth.SelectedItem.Text.ToString()) + ", " + ddlSalYear.SelectedItem.Text.ToString() + "</B>'. You may verify the Year/Month or contact administrator to verify that the salary details are updated on portal against your Employee Code.");
                return;
            }
        }
        catch(Exception ex)
        {
            ShowMessage(divListError, "Error: ", "Error while downloading the Pay Slip. You may verify the Year/Month or contact administrator to verify that your salary details are updated on portal.");
            return;
        }
    }

    private DataTable GetSalData(string UID)
    {
        try
        {
            SqlConnection conn = null;
            string connstring = "";
            conn = new SqlConnection();
            connstring = System.Configuration.ConfigurationManager.ConnectionStrings["enterprise"].ToString();
            conn.ConnectionString = connstring;
            conn.Open();
            SqlCommand cmd = new SqlCommand("INT_SAL_GetSalData_ByUID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", UID.ToString());
            cmd.Parameters.AddWithValue("@Year", ddlSalYear.SelectedItem.Value.ToString());
            cmd.Parameters.AddWithValue("@Month", ddlSalMonth.SelectedItem.Value.ToString());
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

}