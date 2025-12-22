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

public class DBOperation
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CRM"].ToString());

    public string GetLastComplaintID()
    {
        try
        {

            SqlCommand cmd = new SqlCommand("CRM_GetLastComplaintID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            SqlParameter returnparameter = cmd.Parameters.Add("@ComplaintCount", SqlDbType.Int);
            returnparameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int LastComplaintID = (int)returnparameter.Value;
            con.Close();
            return LastComplaintID.ToString();
        }
        catch (Exception E)
        {

            return E.ToString();
            //Catch the log and write in to log file
        }
    }


    public int BindWorklist(GridView GV1, string RoleID, string UserID)
    {
        int returnVal = 0;
        DataSet dsAllComplaint = new DataSet();
        try
        {
            // , 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "CRM_GetMyWorklist";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoleID", RoleID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dsAllComplaint);

            GV1.DataSource = dsAllComplaint;
            GV1.DataBind();
            if (dsAllComplaint.Tables[0].Rows.Count > 0)
            {
                returnVal = dsAllComplaint.Tables[0].Rows.Count;
            }
            con.Close();
            return returnVal;
        }
        catch (Exception E)
        {
            //Write code
            return returnVal;
        }
    }


    //Populate Gridview in complaint list page with all complaints> can be deleted once table operation is sucessfull
    public DataSet BindComplaintswithGridview(GridView GV1, string UserID, string strText, string strCompStatus)
    {

        DataSet dsAllComplaint = new DataSet();
        try
        {
            // , 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "CRM_GetAllComplaintBySearch";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            if (strText.Length > 0)
            {
                cmd.Parameters.AddWithValue("@strText", strText);
            }
            if (strCompStatus.Length > 0)
            {
                cmd.Parameters.AddWithValue("@Status", strCompStatus);
            }
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dsAllComplaint);

            GV1.DataSource = dsAllComplaint;
            GV1.DataBind();
            con.Close();
            return dsAllComplaint;
        }
        catch (Exception E)
        {
            //Write code
            return null;
        }
    }


    //Populate Table in complaint list page with all complaints
    public DataSet returnAllComplaints()
    {

        DataSet dsAllComplaint = new DataSet();
        try
        {
            // , 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "CRM_GetAllComplaint";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dsAllComplaint);
            con.Close();
            return dsAllComplaint;
        }
        catch (Exception E)
        {
            return null;
            //Write code
        }
    }

    public DataSet GetComplaintByID(string complaintID)
    {
        DataSet dsAllComplaint = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = "CRM_GetComplaintDataByComplaintID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompliantID", complaintID);
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dsAllComplaint);
            con.Close();
            return dsAllComplaint;
        }
        catch (Exception E)
        {
            return null;
            //Write code
        }
    }

    ////For loading the complaint satisfaction types in the feedback form
    //public void BindSatisfaction(DropDownList ddl8)
    //{
    //    ddl8.Items.Clear();
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.Connection = con;
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        cmd.CommandText = "SP_GetSatisfaction";
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Connection = con;
    //        da.SelectCommand = cmd;
    //        da.Fill(ds);
    //        con.Close();
    //        ddl8.DataSource = ds;
    //        ddl8.DataTextField = "Satisfaction_Type";
    //        ddl8.DataValueField = "Satisfaction_ID";
    //        ddl8.DataBind();
    //    }
    //    catch (Exception E)
    //    {

    //    }
    //}


    public bool CreateKB(string strCOmplaintID, string ProdCategory
            , string Unit, string TypeOfComplaint,string RootCause, string KBDesc, string KBSolution, string createdBy)
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
                    returnString += "<div style='border: 1px; background-color: #819bcb; width: 100%; padding:5px; color:#ffffff; font-size:13px'>";
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
            if (strstring.Length > 0)
            {
                returnString = returnString.Replace(strstring, "<font color='blue'><b>" + strstring + "</b></font>");
            }
            returnString = HeaderString + returnString;
            con.Close();
            return returnString;
        }
        catch
        {
            return "<font color='red'>Error in KB search. Please try again later.</font>";
        }
    }

    //Creating feedback for complaints
    public bool CreateFeedback(string strCOmplaintID, string intcomplaintSatisfaction, string strcomplaintSatisfactionComment, string intcomplaintEmployeeBehavior, string strcomplaintResponseandActionTimeliness, string strcomplaintRequirementMet, string strcomplaint_Requirement_Comment, string strFeedback)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@complaint_ID", strCOmplaintID);
            cmd.Parameters.AddWithValue("@complaint_Satisfaction", intcomplaintSatisfaction);
            cmd.Parameters.AddWithValue("@complaint_Satisfaction_Comment", strcomplaintSatisfactionComment);
            cmd.Parameters.AddWithValue("@complaint_Employee_Behavior", intcomplaintEmployeeBehavior);
            cmd.Parameters.AddWithValue("@complaint_ResponseandAction_Timeliness", strcomplaintResponseandActionTimeliness);
            cmd.Parameters.AddWithValue("@complaint_Requirement_Met", strcomplaintRequirementMet);
            cmd.Parameters.AddWithValue("@complaint_Requirement_Comment", strcomplaint_Requirement_Comment);
            cmd.Parameters.AddWithValue("@complaint_Feedback", strFeedback);
            con.Open();
            cmd.CommandText = "CRM_UpdateComplaintFeedback";
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

    //Creating a complaint with new complaint ID
    public string CreateComplaint(string strCustomerName, string strCustomerPlace, string strCustomerMobile, string strCustomerTelephone, string strCustomerAddress, string strComplaintNature, string strArea, string strComplaintType, string strProductDetails, int intQuantity, string strProductBatchNO, string strRegistrationDate, string strCategory, string CompDescr, string contactedBy, string userID, string Distributor, decimal TotalCost, string CustomerType)
    {
        string ComplaintID = "";
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customer_Name", strCustomerName);
            cmd.Parameters.AddWithValue("@customer_Place", strCustomerPlace);
            cmd.Parameters.AddWithValue("@customer_Mobile", strCustomerMobile);
            cmd.Parameters.AddWithValue("@customer_Telephone", strCustomerTelephone);
            cmd.Parameters.AddWithValue("@customer_Address", strCustomerAddress);
            cmd.Parameters.AddWithValue("@complaint_Nature", strComplaintNature);
            cmd.Parameters.AddWithValue("@Area", strArea);
            cmd.Parameters.AddWithValue("@complaint_Type", strComplaintType);
            cmd.Parameters.AddWithValue("@product_Details", strProductDetails);
            cmd.Parameters.AddWithValue("@product_Quantity", intQuantity);
            cmd.Parameters.AddWithValue("@product_Batch_NO", strProductBatchNO);
            cmd.Parameters.AddWithValue("@registration_Time", strRegistrationDate);
            cmd.Parameters.AddWithValue("@category", strCategory);
            cmd.Parameters.AddWithValue("@CompDescr", CompDescr);
            cmd.Parameters.AddWithValue("@contactedBy", contactedBy);
            cmd.Parameters.AddWithValue("@DistributorName", Distributor);
            cmd.Parameters.AddWithValue("@CreatedBy", userID);
            cmd.Parameters.AddWithValue("@TotalCost", TotalCost);
            cmd.Parameters.AddWithValue("@CustomerType", CustomerType);
            con.Open();
            cmd.CommandText = "CRM_CreateComplaint";
            ComplaintID = cmd.ExecuteScalar().ToString();
            con.Close();
            return ComplaintID;
        }
        catch (Exception E)
        {
            return "";
            //Catch the log and write in to log file
        }
    }

    //UPdate complaint once it is created based on the complaint ID
    public int UpdateComplaint(string strCOmplaintID, string strCustomerName, string strCustomerPlace, string strCustomerMobile, string strCustomerTelephone, string strCustomerAddress, string strComplaintNature,string strArea, string strComplaintType, string strProductDetails, int intQuantity, string strProductBatchNO, string strRegistrationDate, string strCategory, string CompDescr, string contactedBy, string userID, string Distributor, decimal TotalCost, string CustomerType)
    {
        int status = 0;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@complaint_ID", strCOmplaintID);
            cmd.Parameters.AddWithValue("@customer_Name", strCustomerName);
            cmd.Parameters.AddWithValue("@customer_Place", strCustomerPlace);
            cmd.Parameters.AddWithValue("@customer_Mobile", strCustomerMobile);
            cmd.Parameters.AddWithValue("@customer_Telephone", strCustomerTelephone);
            cmd.Parameters.AddWithValue("@customer_Address", strCustomerAddress);
            cmd.Parameters.AddWithValue("@complaint_Nature", strComplaintNature);
            cmd.Parameters.AddWithValue("@Area", strArea);
            cmd.Parameters.AddWithValue("@complaint_Type", strComplaintType);
            cmd.Parameters.AddWithValue("@product_Details", strProductDetails);
            cmd.Parameters.AddWithValue("@product_Quantity", intQuantity);
            cmd.Parameters.AddWithValue("@product_Batch_NO", strProductBatchNO);
            cmd.Parameters.AddWithValue("@registration_Time", strRegistrationDate);
            cmd.Parameters.AddWithValue("@category", strCategory);
            cmd.Parameters.AddWithValue("@CompDescr", CompDescr);
            cmd.Parameters.AddWithValue("@contactedBy", contactedBy);
            cmd.Parameters.AddWithValue("@DistributorName", Distributor);
            cmd.Parameters.AddWithValue("@LastModifiedBY", userID);
            cmd.Parameters.AddWithValue("@TotalCost", TotalCost);
            cmd.Parameters.AddWithValue("@CustomerType", CustomerType);
            con.Open();
            cmd.CommandText = "CRM_UpdateComplaint";
            cmd.ExecuteNonQuery();
            con.Close();
            return 1;
        }
        catch (Exception E)
        {

            return status;
            //Catch the log and write in to log file
        }
    }


    //complaint Worklist
    public bool AddToWorklist(string complaintID, string ActionPendingWithRole, string ActionAssignedBy, string ActionAssignedByRoleID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Complaint_ID", complaintID);
            cmd.Parameters.AddWithValue("@ActionPendingWithRole", ActionPendingWithRole);
            cmd.Parameters.AddWithValue("@ActionAssignedBy", ActionAssignedBy);
            cmd.Parameters.AddWithValue("@ActionAssignedByRoleID", ActionAssignedByRoleID);
            con.Open();
            cmd.CommandText = "CRM_InsertActionWorklist";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            return false;
            //Catch the log and write in to log file
        }

    }

    public bool DeleteWorklist(string complaintID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Complaint_ID", complaintID);
            con.Open();
            cmd.CommandText = "CRM_DeleteActionWorklist";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            return false;
            //Catch the log and write in to log file
        }

    }
    
    //complaint Action closure update
    public bool UpdateAction(string Complaint_ID, string ActionByRole, string ActionByUID, string ActionTaken, int StatusID)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Complaint_ID", Complaint_ID);
            cmd.Parameters.AddWithValue("@ActionByRole", ActionByRole);
            cmd.Parameters.AddWithValue("@ActionByUID", ActionByUID);
            cmd.Parameters.AddWithValue("@ActionTaken", ActionTaken);
            if (StatusID > 0)
            {
                cmd.Parameters.AddWithValue("@StatusID", StatusID);
            }
            con.Open();
            cmd.CommandText = "CRM_InsertActionClosure";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            return false;
            //Catch the log and write in to log file
        }

    }

    public bool CorrectiveActionFields(string ComplaintID, string strUnitID,string SubUnit, string strExpectedClosureDate, string strActualClosureDate, string strContactedONAfterReview, string strSampleReceivedTime, string strCost, string strComplaintOutCome, string ModifiedBY)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Complaint_ID", ComplaintID);
            cmd.Parameters.AddWithValue("@Complaint_Unit", strUnitID);
            cmd.Parameters.AddWithValue("@SubUnit", SubUnit);
            if (strExpectedClosureDate.Length > 0 && (strExpectedClosureDate != "____/__/__ __:__"))
            {
                cmd.Parameters.AddWithValue("@ExpectedClosureDate", strExpectedClosureDate);
            }
            if (strActualClosureDate.Length > 0 && (strActualClosureDate != "____/__/__ __:__"))
            {
                cmd.Parameters.AddWithValue("@ActualClosureDate", strActualClosureDate);
            }

            if (strContactedONAfterReview.Length > 0 && (strContactedONAfterReview != "____/__/__ __:__"))
            {
                cmd.Parameters.AddWithValue("@CustometContactedOnAfterReview", strContactedONAfterReview);
            }
            if (strSampleReceivedTime.Length > 0 && (strSampleReceivedTime != "____/__/__ __:__"))
            {
                cmd.Parameters.AddWithValue("@SampleReceivedDateTime", strSampleReceivedTime);
            }
            cmd.Parameters.AddWithValue("@TotalCost", strCost);
            cmd.Parameters.AddWithValue("@OutComeofComplaint", strComplaintOutCome);
            cmd.Parameters.AddWithValue("@ModifiedBY", ModifiedBY);                       
            con.Open();
            cmd.CommandText = "CRM_UpdateCorrectiveActionFields";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            return false;
            //Catch the log and write in to log file
        }        
    }

    public bool UpdateLegalActions(string Complaint_ID, string PointsUpdated, string InitiatedThrough, string ChecklistItems, string CompletedON, string UpdatedBY)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Complaint_ID", Complaint_ID);
            cmd.Parameters.AddWithValue("@Dispute_Points", PointsUpdated);
            cmd.Parameters.AddWithValue("@Action_InitiatedThrough", InitiatedThrough);
            cmd.Parameters.AddWithValue("@PointsInChecklist", ChecklistItems);
            cmd.Parameters.AddWithValue("@Dispute_CompletedOn", CompletedON);
            cmd.Parameters.AddWithValue("@Dispute_UpdatedBY", UpdatedBY);
            con.Open();
            cmd.CommandText = "CRM_Update_ComplaintDispute";
            cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }
        catch
        {
            return false;
            //Catch the log and write in to log file
        }
    }
    
}