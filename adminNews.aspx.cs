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

public partial class adminNews : System.Web.UI.Page
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
            //string filename = Path.GetFileName(fileuploadimages.PostedFile.FileName);
            string filename = GetUniQueFileName("~/Gallery/News", Path.GetFileName(fileuploadimages.PostedFile.FileName));
            //Save images into SlideImages folder
            if (ViewState["ID"] != null)
            {
                try
                {
                    if (File.Exists(Server.MapPath("~/Gallery/News/" + ViewState["Image"].ToString())))
                    {
                        File.Delete(Server.MapPath("~/Gallery/News/" + ViewState["Image"].ToString()));
                    }
                }
                catch { }
                fileuploadimages.SaveAs(Server.MapPath("~/Gallery/News/" + filename));
                objEntities.INT_Update_News(System.Convert.ToInt32(ViewState["ID"].ToString()), strTitle, strDetails, filename);
                ShowMessage(divSuccess, "Success", "Updated successfully!");
            }
            else
            {
                fileuploadimages.SaveAs(Server.MapPath("~/Gallery/News/" + filename));
                objEntities.INT_Insert_News(strTitle, strDetails, filename, Session["UserID"].ToString());
                ShowMessage(divSuccess, "Success", "added successfully!");
            }
            FillCache();            
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
            List<INT_Get_All_News_Admin_Result> objListAll = objEntities.INT_Get_All_News_Admin().ToList();
            if (objListAll.Count > 0)
            {
                grid1.DataSource = objListAll;
                grid1.DataBind();
                grid1.Visible = true;
            }
            else
            {
                if (Cache["LatestNews"] != null)
                {
                    Cache.Remove("LatestNews");
                }
                grid1.Visible = false;
                //ShowMessage(divAlert, "Note", "No data found!");
            }
        }
        catch
        {
            ShowMessage(diverror, "Error", "Error while displaying news data!");
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
                List<INT_Get_News_BYID_Result> objListAll = objEntities.INT_Get_News_BYID(System.Convert.ToInt32(e.CommandArgument.ToString())).ToList();
                if (objListAll.Count > 0)
                {
                    string Content = objListAll[0].News_Text.ToString();
                    string Header = objListAll[0].News_Title.ToString();
                    ViewState["ID"] = e.CommandArgument.ToString();
                    ViewState["Image"] = objListAll[0].News_Picture_Name.ToString();
                    txtDetails.Text = Content;
                    txtTitle.Text = Header;
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
                objEntities.INT_Delete_News(System.Convert.ToInt32(e.CommandArgument.ToString()));
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
                    cmd.CommandText = "INT_Get_Latest_News";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (Cache["LatestNews"] != null)
                            {
                                Cache.Remove("LatestNews");
                            }
                            Cache.Insert("LatestNews", dt);
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