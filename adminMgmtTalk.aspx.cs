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

public partial class adminMgmtTalk : System.Web.UI.Page
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
            string strDetails = txtDetails.Text.Trim();
            string strTitle = txtTitle.Text.ToString();
            string Author = txtAuthor.Text.ToString();
            //Get Filename from fileupload control
            if (fileuploadimages.PostedFile.ContentLength > 30720)
            {
                ShowMessage(diverror, "Warning: ", "Image File size should be less than 30 KB.");
                return;
            }
            else if (fileuploadimages.PostedFile.ContentLength <= 0)
            {
                ShowMessage(diverror, "Warning: ", "Please upload an image file. File size should be within 30 KB size.");
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
            //ViewState["Image"] = objListAll[0].MGMT_Photo_Name.ToString();
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
                //Save images into SlideImages folder
                fileuploadimages.SaveAs(Server.MapPath("~/Gallery/Management/" + filename));
                objEntities.INT_Update_MGMT_Talk(System.Convert.ToInt32(ViewState["ID"].ToString()), strTitle, strDetails, Author, filename, Session["UserID"].ToString());
                ShowMessage(divSuccess, "Success", "Updated successfully!");
            }
            else
            {
                //Save images into SlideImages folder
                fileuploadimages.SaveAs(Server.MapPath("~/Gallery/Management/" + filename));
                objEntities.INT_Insert_MGMT_Talk(strTitle, strDetails, Author, filename, Session["UserID"].ToString());
                ShowMessage(divSuccess, "Success", "Added successfully!");
            }
            FillCache();            
            ListData();
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while adding data!");
        }
    }


    private void ListData()
    {
        pnlList.Visible = true;
         List<INT_Get_MGMT_Talk_Result> objListAll = objEntities.INT_Get_MGMT_Talk().ToList();
        if (objListAll.Count > 0)
        {
            grid1.DataSource = objListAll;
            grid1.DataBind();
            grid1.Visible = true;
        }
        else
        {
            if (Cache["MGMTTalk"] != null)
            {
                Cache.Remove("MGMTTalk");
            }
            grid1.Visible = false;
            //ShowMessage(divAlert, "Note", "No data found!");
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
                List<INT_Get_MGMT_Talk_BYID_Result> objListAll = objEntities.INT_Get_MGMT_Talk_BYID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
                if (objListAll.Count > 0)
                {
                    string Author = objListAll[0].MGMT_Author.ToString();
                    string Content = objListAll[0].MGMT_Text.ToString();
                    string Title = objListAll[0].MGMT_Title.ToString();
                    ViewState["ID"] = e.CommandArgument.ToString();
                    ViewState["Image"] = objListAll[0].MGMT_Photo_Name.ToString();
                    txtAuthor.Text = Author;
                    txtDetails.Text = Content;
                    txtTitle.Text = Title;
                    btnSubmit.Text = "Update Details";
                    txtTitle.Focus();
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
                objEntities.INT_Delete_MGMT_Talk(System.Convert.ToInt32(e.CommandArgument.ToString()));
                ListData();
                ShowMessage(divSuccess, "Success", "Deleted successfully!");
                FillCache();
            }
            catch
            {
                ShowMessage(diverror, "Error", "Error while deleting data!");
            }
        }
    }
    private void FillCache()
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["enterprise"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INT_Get_Latest_MGMT_Talk";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["MGMTTalk"] != null)
                            {
                                Cache.Remove("MGMTTalk");
                            }
                            Cache.Insert("MGMTTalk", dt);
                        }
                        //System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                    }
                    dt.Dispose();
                    conn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(diverror, "Error", "Error while storing data on Cache!");
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