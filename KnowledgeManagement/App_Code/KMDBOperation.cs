using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

public class KMDBOperation
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());


    public DataTable getDatatable(string SP, SqlConnection conn)
    {
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(SP, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;

        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }


    public bool CreateKB(string strCOmplaintID, string ProdCategory
            , string Unit, string TypeOfComplaint, string RootCause, string KBDesc, string KBSolution, string createdBy)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@complaint_ID", strCOmplaintID);
            cmd.Parameters.AddWithValue("@ProductItem", ProdCategory);
            cmd.Parameters.AddWithValue("@PoductionUnit", Unit);
            cmd.Parameters.AddWithValue("@ComplaintNature", TypeOfComplaint);
            cmd.Parameters.AddWithValue("@RootCauseAnalysis", RootCause);
            cmd.Parameters.AddWithValue("@KB_Description", KBDesc);
            cmd.Parameters.AddWithValue("@KB_Solution", KBSolution);
            cmd.Parameters.AddWithValue("@createdBy", createdBy);
            con.Open();
            cmd.CommandText = "CRM_InsertKB";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception E)
        {

            return false;
            //Catch the log and write in to log file
        }
    }

    public bool CreateATR(string Category, string GeneralCategory, string ComplaintDetails,
                   string ProdCategory, string RootCause, string CAShortData, string SearchTags, 
        string CompanyUnit, string CARNo, string CARDetails, string ATRNo, string ATRDetails, string BenefitAchieved
                       , string PreparedBy, DateTime PreparedDate, string ApprovedBY, string createdBy)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@GeneralCategory", GeneralCategory);
            cmd.Parameters.AddWithValue("@ComplaintDetails", ComplaintDetails);
            cmd.Parameters.AddWithValue("@Product", ProdCategory);
            cmd.Parameters.AddWithValue("@RootCause", RootCause);
            cmd.Parameters.AddWithValue("@CAShortData", CAShortData);
            cmd.Parameters.AddWithValue("@SearchTag", SearchTags);
            cmd.Parameters.AddWithValue("@UnitName", CompanyUnit);
            cmd.Parameters.AddWithValue("@CARNo", CARNo);
            cmd.Parameters.AddWithValue("@CARDetails", CARDetails);
            cmd.Parameters.AddWithValue("@ATRNo", ATRNo);
            cmd.Parameters.AddWithValue("@ATRDetails", ATRDetails);
            cmd.Parameters.AddWithValue("@BenefitAchieved", BenefitAchieved);
            cmd.Parameters.AddWithValue("@PreparedBy", PreparedBy);
            cmd.Parameters.AddWithValue("@PreparedDate", PreparedDate);
            cmd.Parameters.AddWithValue("@ApprovedBY", ApprovedBY);
            cmd.Parameters.AddWithValue("@createdBy", createdBy);
            con.Open();
            cmd.CommandText = "CRM_KB_Insert_ATR";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception E)
        {

            return false;
            //Catch the log and write in to log file
        }
    }


    public bool UpdateATR(int SLNO, string Category, string GeneralCategory, string ComplaintDetails,
                   string ProdCategory, string RootCause, string CAShortData, string SearchTags,
        string CompanyUnit, string CARNo, string CARDetails, string ATRNo, string ATRDetails, string BenefitAchieved
                       , string PreparedBy, DateTime PreparedDate, string ApprovedBY)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SLNO", SLNO);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@GeneralCategory", GeneralCategory);
            cmd.Parameters.AddWithValue("@ComplaintDetails", ComplaintDetails);
            cmd.Parameters.AddWithValue("@Product", ProdCategory);
            cmd.Parameters.AddWithValue("@RootCause", RootCause);
            cmd.Parameters.AddWithValue("@CAShortData", CAShortData);
            cmd.Parameters.AddWithValue("@SearchTag", SearchTags);
            cmd.Parameters.AddWithValue("@UnitName", CompanyUnit);
            cmd.Parameters.AddWithValue("@CARNo", CARNo);
            cmd.Parameters.AddWithValue("@CARDetails", CARDetails);
            cmd.Parameters.AddWithValue("@ATRNo", ATRNo);
            cmd.Parameters.AddWithValue("@ATRDetails", ATRDetails);
            cmd.Parameters.AddWithValue("@BenefitAchieved", BenefitAchieved);
            cmd.Parameters.AddWithValue("@PreparedBy", PreparedBy);
            cmd.Parameters.AddWithValue("@PreparedDate", PreparedDate);
            cmd.Parameters.AddWithValue("@ApprovedBY", ApprovedBY);
            con.Open();
            cmd.CommandText = "CRM_KB_Update_ATR";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception E)
        {

            return false;
            //Catch the log and write in to log file
        }
    }

    

    public bool CreateBestPractice(string Area, string Activity, string ProdCategory,
                    string CompanyUnit, string SearchTags, string MonitoringSystem, string BestPracticeSummary, string SOPReferenceAndVersionNo, string BenefitAchieved, string PreparedBy
                    , DateTime PreparedDate, string ApprovedBY, string CreatedBy)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Area", Area);
            cmd.Parameters.AddWithValue("@Activity", Activity);
            cmd.Parameters.AddWithValue("@Product", ProdCategory);
            cmd.Parameters.AddWithValue("@UnitName", CompanyUnit);
            cmd.Parameters.AddWithValue("@SearchTag", SearchTags);
            cmd.Parameters.AddWithValue("@MonitoringSystem", MonitoringSystem);
            cmd.Parameters.AddWithValue("@BestPracticeSummary", BestPracticeSummary);
            cmd.Parameters.AddWithValue("@SOPReferenceAndVersionNo", SOPReferenceAndVersionNo);
            cmd.Parameters.AddWithValue("@BenefitAchieved", BenefitAchieved);
            cmd.Parameters.AddWithValue("@PreparedBy", PreparedBy);
            cmd.Parameters.AddWithValue("@PreparedDate", PreparedDate);
            cmd.Parameters.AddWithValue("@ApprovedBY", ApprovedBY);
            cmd.Parameters.AddWithValue("@createdBy", CreatedBy);
            con.Open();
            cmd.CommandText = "CRM_KB_Insert_BestPractice";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception E)
        {

            return false;
            //Catch the log and write in to log file
        }
    }



    public bool UpdateBestPractice(int SLNO, string Area, string Activity, string ProdCategory,
                    string CompanyUnit, string SearchTags, string MonitoringSystem, string BestPracticeSummary, string SOPReferenceAndVersionNo, string BenefitAchieved, string PreparedBy
                    , DateTime PreparedDate, string ApprovedBY)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SLNO", SLNO);
            cmd.Parameters.AddWithValue("@Area", Area);
            cmd.Parameters.AddWithValue("@Activity", Activity);
            cmd.Parameters.AddWithValue("@Product", ProdCategory);
            cmd.Parameters.AddWithValue("@UnitName", CompanyUnit);
            cmd.Parameters.AddWithValue("@SearchTag", SearchTags);
            cmd.Parameters.AddWithValue("@MonitoringSystem", MonitoringSystem);
            cmd.Parameters.AddWithValue("@BestPracticeSummary", BestPracticeSummary);
            cmd.Parameters.AddWithValue("@SOPReferenceAndVersionNo", SOPReferenceAndVersionNo);
            cmd.Parameters.AddWithValue("@BenefitAchieved", BenefitAchieved);
            cmd.Parameters.AddWithValue("@PreparedBy", PreparedBy);
            cmd.Parameters.AddWithValue("@PreparedDate", PreparedDate);
            cmd.Parameters.AddWithValue("@ApprovedBY", ApprovedBY);
            con.Open();
            cmd.CommandText = "CRM_KB_Insert_BestPractice";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception E)
        {

            return false;
            //Catch the log and write in to log file
        }
    }

    public bool DeleteKB(string ID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", @ID);
            con.Open();
            cmd.CommandText = "CRM_DelteKBByID";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch (Exception E)
        {

            return false;
            //Catch the log and write in to log file
        }
    }



    public void BindKBwithGridview(DataGrid GV1, string ComplaintID)
    {

        DataSet dsAllComplaint = new DataSet();
        try
        {
            // , 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "CRM_GetKBByComplaintID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Complaint_ID", ComplaintID);
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dsAllComplaint);

            GV1.DataSource = dsAllComplaint;
            GV1.DataBind();
            con.Close();
        }
        catch (Exception E)
        {
            //Write code
        }
    }
              
    public string GetKBResults(string TypeOfComplaint, string ProdCategory, string CompanyUnit, string strstring, string mode)
    {
        DataSet dsKBList = new DataSet();
        try
        {
            //if (strstring.Length < 3)
            //{
            //    return "<font color='blue'>Please enter minimum 3 character in the text box to search for the Knowledge Base data.</font>";
            //}
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "CRM_SearchKBResults";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchString", strstring.ToString());
            cmd.Parameters.AddWithValue("@Mode", mode.ToString());
            cmd.Parameters.AddWithValue("@PoductionUnit", CompanyUnit.ToString());
            cmd.Parameters.AddWithValue("@ComplaintNature", TypeOfComplaint.ToString());
            cmd.Parameters.AddWithValue("@ProductItem", ProdCategory.ToString());
            
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dsKBList);
            string HeaderString = "";
            HeaderString = "<div><B><font color='blue'> Total '" + dsKBList.Tables[0].Rows.Count + "' Result(s) found in your search criteria</font></B><BR></div>";
            string returnString = "";
            string complaintID = "";
            if (dsKBList.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsKBList.Tables[0].Rows.Count; i++)
                {
                    if (dsKBList.Tables[0].Rows[i]["Complaint_ID"].ToString() != "0")
                    {
                        complaintID = dsKBList.Tables[0].Rows[i]["Complaint_ID"].ToString();
                    }
                    else
                    {
                        complaintID = "";
                    }
                    returnString += "<div style='border: 1px; background-color: #819bcb; width: 100%; padding:8px; color:#ffffff;'>";
                    returnString += "<b>Product Item: </B>" + dsKBList.Tables[0].Rows[i]["ProductItem"].ToString() +" ";
                    returnString += "<b>Complaint Type: </B>" + dsKBList.Tables[0].Rows[i]["ComplaintNature"].ToString() + " ";
                    returnString += "<b>Poduction Unit: </B>" + dsKBList.Tables[0].Rows[i]["PoductionUnit"].ToString() + " ";
                    returnString += "<b>Complaint ID: </B>" + complaintID + " ";                    
                    returnString += "</div>";
                    returnString += "<div><b>Description as per CRM: </b>" + dsKBList.Tables[0].Rows[i]["KB_Description"].ToString() + "</div>";
                    returnString += "<div><b>Root cause Analysis: </b>" + dsKBList.Tables[0].Rows[i]["RootCauseAnalysis"].ToString() + "</div>";
                    returnString += "<div><b>Corrective Action: </b>" + dsKBList.Tables[0].Rows[i]["KB_Solution"].ToString() + "</div>";
                    returnString += "<div style='border: 1px; background-color: #EEEEEE; width: 100%; padding:5px;'><font color='maroon'> Created By: " + dsKBList.Tables[0].Rows[i]["createdBy"].ToString() + " Created On: " + dsKBList.Tables[0].Rows[i]["CreatedOn"].ToString() + "</font></div>";
                    returnString += "<div><hr width='100%; height:1px'/></div>";                    
                }
            }
            //if (strstring.Length > 0)
            //{
            //    returnString = returnString.Replace(strstring, "<font color='blue'><b>" + strstring + "</b></font>");
            //}
            returnString = HeaderString + returnString;
            con.Close();
            return returnString;
        }
        catch
        {
            return "<font color='red'>Error in KB search. Please try again later.</font>";
        }
    }

}