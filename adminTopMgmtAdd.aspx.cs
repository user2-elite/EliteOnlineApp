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
using System.IO;
using INTModel;

public partial class adminTopMgmtAdd : System.Web.UI.Page
{
    INTModelContainer objEntities = new INTModelContainer();
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
            }
            else
            {
                Response.Redirect("default.aspx");
            }
            ListData();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

           

            //Image img = System.Drawing.Image.FromFile("test.jpg");
            //int width = img.Width;
            //int height = img.Height;
            string strDetails = txtDetails.Text.Trim();
            string strname = txtName.Text.ToString();
            string strDesig = txtDesig.Text.ToString();
            string strUnit = ddlUnit.SelectedItem.Value.ToString();                
            int intSortOrder = 0;
            try
            {
                intSortOrder = System.Convert.ToInt32(txtSortOrder.Text.ToString());
            }
            catch
            {
                ShowMessage(diverror, "Warning: ", "Please enter only numeric value in Sort Order Field");
                return;
            }

            //Get Filename from fileupload control
            if (fileuploadimages.PostedFile.ContentLength > 102400)
            {
                ShowMessage(diverror, "Warning: ", "Image File size should be less than 100 KB.");
                return;
            }
            else if (fileuploadimages.PostedFile.ContentLength <= 0)
            {
                ShowMessage(diverror, "Warning: ", "Please upload an image file. File size should be within 100 KB size.");
                return;
            }
            using (System.Drawing.Image myImage = System.Drawing.Image.FromStream(fileuploadimages.PostedFile.InputStream))
            {
                if (myImage.Height != 140 && myImage.Width != 140)
                {
                    ShowMessage(diverror, "Warning: ", "Image Width and Height should be 140px X 140px resolution.");
                    return;
                }
            }
            string filename = GetUniQueFileName("~/Gallery/Management", Path.GetFileName(fileuploadimages.PostedFile.FileName));            
            if (ViewState["ID"] != null)
            {
                try
                {
                    if (File.Exists(Server.MapPath("~/Gallery/Management/" + ViewState["Image"].ToString())))
                    {
                        File.Delete(Server.MapPath("~/Gallery/Management/" + ViewState["Image"].ToString()));
                    }
                }
                catch { }
                //Save images into folder
                fileuploadimages.SaveAs(Server.MapPath("~/Gallery/Management/" + filename));
                objEntities.INT_Update_MGMTList(System.Convert.ToInt32(ViewState["ID"].ToString()), strname,strUnit, strDesig, strDetails, filename, intSortOrder);
                ShowMessage(divSuccess, "Success", "Updated successfully!");
            }
            else
            {
                //Save images into folder
                fileuploadimages.SaveAs(Server.MapPath("~/Gallery/Management/" + filename));
                objEntities.INT_Insert_MGMTList(strname,strUnit, strDesig, strDetails, filename, Session["UserID"].ToString(), intSortOrder);
                ShowMessage(divSuccess, "Success", "Added successfully!");
            }            
            ListData();
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while adding data!" + ex.Message.ToString());
        }
    }


    private void ListData()
    {
        try
        {
            pnlList.Visible = true;
            List<INT_Get_All_MGMTList_Result> objListAll = objEntities.INT_Get_All_MGMTList().ToList();
            if (objListAll.Count > 0)
            {
                grid1.DataSource = objListAll;
                grid1.DataBind();
                grid1.Visible = true;
            }
            else
            {
                grid1.Visible = false;
            }
        }
        catch
        {
            ShowMessage(diverror, "Error", "Error while displaying data!");
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

    protected void grid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "cmEdit")
        {
            try
            {
                List<INT_Get_MGMTList_BYID_Result> objListAll = objEntities.INT_Get_MGMTList_BYID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
                if (objListAll.Count > 0)
                {
                    string Name = objListAll[0].MGMT_Name.ToString();
                    string Desig = objListAll[0].MGMT_Desig.ToString();
                    string Profile = objListAll[0].MGMT_Profile.ToString();
                    string SortOrder = objListAll[0].MGMT_SortOrder.ToString();
                    string strUnit = objListAll[0].MGMT_Unit.ToString();
                    ViewState["ID"] = e.CommandArgument.ToString();
                    ViewState["Image"] = objListAll[0].MGMT_Picture_Name.ToString();

                    txtDesig.Text = Desig;
                    txtDetails.Text = Profile;
                    txtName.Text = Name;
                    txtSortOrder.Text = SortOrder;
                    try
                    {
                        ddlUnit.SelectedItem.Value = strUnit;
                    }
                    catch { }
                    btnSubmit.Text = "Update Details";
                    txtName.Focus();
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
                objEntities.INT_Delete_MGMTList(System.Convert.ToInt32(e.CommandArgument.ToString()));
                ListData();
                ShowMessage(divSuccess, "Success", "Deleted successfully!");
            }
            catch
            {
                ShowMessage(diverror, "Error", "Error while deleting data!");
            }
        }
    }

    private string GetUniQueFileName(string Path, string FileName)
    {
        string NewFileName = "";
        try
        {
            if (File.Exists(Server.MapPath(Path + "/" + FileName)))
            {
                for (int i = 1; i <= 100; i++)
                {
                    NewFileName = "image" + i + "_" + FileName;
                    if (!File.Exists(Server.MapPath(Path + "/" + NewFileName)))
                    {
                        break;
                    }
                }
            }
            else
            {
                NewFileName = FileName;
            }
        }
        catch
        {
            NewFileName = FileName;
        }
        return NewFileName;
    }
 
}