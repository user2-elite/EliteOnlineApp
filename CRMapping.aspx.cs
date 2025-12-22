using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class CRMapping : System.Web.UI.Page
{
  
    Consolidated objCons = new Consolidated();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objCons.GetDDLs("SPCR_GetFacilities", ddlFacName, "UnitName", "unitID");
            objCons.FillGrid("SPCR_FillCRMGrid", gvCRMapping);
            clearMessages();
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
    }
    protected void btnMap_Click(object sender, EventArgs e)
    {
        try
        {
            int dupCheck = DuplicateCRCheck(Convert.ToInt32(ddlFacName.SelectedValue), txtConfRoom.Text.ToString());
            if (dupCheck == 0)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand comm = new SqlCommand("SPCR_InsertConfRoomMaster", conn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@UnitID", Convert.ToInt32(ddlFacName.SelectedValue));
                comm.Parameters.AddWithValue("@CRName", txtConfRoom.Text.ToString());
                //comm.Parameters.AddWithValue("@cr_id", 1);
                conn.Open();
                comm.ExecuteNonQuery();
                objCons.FillGrid("SPCR_FillCRMGrid", gvCRMapping);
                ShowMessage(divSuccess, "Success", "Added Succussesfully!");
            }
            }
            else
            {
                ShowMessage(diverror, "Error", "Already exists!");
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error:", ex.Message.ToString());
        }      
    }
    protected void gvCRMapping_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvCRMapping.EditIndex = e.NewEditIndex;
        objCons.FillGrid("SPCR_FillCRMGrid", gvCRMapping);
    }   

    private int DuplicateCRCheck(int fsid, string crName)
    {
        try
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["enterprise"].ToString()))
            {
                SqlCommand comm = new SqlCommand("SPCR_CheckDuplicateRoomNames", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CRname", crName);
                conn.Open();
                string retStr = comm.ExecuteScalar().ToString();
                conn.Close();
                if (retStr == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error:", ex.Message.ToString());
            return 1;
        }
    }

}