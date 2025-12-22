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

public partial class Page : System.Web.UI.Page
{
    INTModelContainer objEntities = new INTModelContainer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            string ID = Request.QueryString["ID"].ToString();
            DisplayData(ID);
        }
    }

    private void  DisplayData(string ID)
    {
        List<INT_Get_ExtraLinks_ByID_Result> objListAll = objEntities.INT_Get_ExtraLinks_ByID(System.Convert.ToInt32(ID)).ToList();
        if (objListAll.Count > 0)
        {
            string Content = objListAll[0].Page_Content.ToString();
            string Header = objListAll[0].Page_Header.ToString();

            divHeader.InnerHtml = Header;
            divContent.InnerHtml = Content;
        }
        else
        {
            divHeader.InnerHtml = "Page Header";
            divContent.InnerHtml = "Currently no data available.";
        }
    }
}