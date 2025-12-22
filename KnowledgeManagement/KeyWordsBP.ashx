<%@ WebHandler Language="C#" Class="KeyWordsBP" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
public class KeyWordsBP : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string returnText = "";
        string prefixText = context.Request.QueryString["term"];
        DataTable dt = new DataTable();
        /*
         * https://ashfaqasp.wordpress.com/2010/04/23/using-jquery-ui-autocomplete-with-asp-net-ashx-file/
         * */
        //-------------------- =Cache Checking ---------------------------------
        if (context.Cache["KeyWordsBP"] != null)
        {
            dt = (DataTable)context.Cache["KeyWordsBP"];
        }
        else
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["CRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "SELECT UnitName as NameVal, UnitName as NameLabel " +
                    //                    " FROM CRMtbl_UnitMaster";
                    cmd.CommandText = "CRM_KB_GetBestPracticeKeyWords";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    //StringBuilder sb = new StringBuilder();

                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency(cmd);
                        context.Cache.Insert("KeyWordsBP", dt, sqlDep);
                    }
                    //dt.Dispose();
                    conn.Close();
                }
            }
        }

        //---------------- =It can be filled by cache or from database ---------------------
        if (dt.Rows.Count > 0)
        {
            DataTable dtnew = dt.Clone();
            dtnew.Clear();
            //string expression="NameVal ="
            DataRow[] drArray = dt.Select(" JSONKeyWords like '" + prefixText + "%'");
            foreach (DataRow dr in drArray)
            {
                DataRow drnewrow = dtnew.NewRow();
                drnewrow["JSONID"] = dr["JSONID"];
                drnewrow["JSONKeyWords"] = dr["JSONKeyWords"];
                dtnew.Rows.Add(drnewrow);
            }
            returnText = GetJSONString(dtnew);
        }
        else
        {
            returnText = "";
        }

        dt.Dispose();
        //------------------- =Sending data to caller ------------------
        context.Response.Write(returnText);
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


    public static string GetJSONString(DataTable Dt)
    {

        string[] StrDc = new string[Dt.Columns.Count];

        string HeadStr = string.Empty;
        for (int i = 0; i < Dt.Columns.Count; i++)
        {

            StrDc[i] = Dt.Columns[i].Caption;
            HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";

        }

        //[ { "id": "Upupa epops", "label": "Eurasian Hoopoe", "value": "Eurasian Hoopoe" }, { "id": "Jynx torquilla", "label": "Eurasian Wryneck", "value": "Eurasian Wryneck" }, { "id": "Ficedula hypoleuca", "label":

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
        StringBuilder Sb = new StringBuilder();

        //Sb.Append("{\"" + Dt.TableName + "\" : [");
        Sb.Append("[");
        for (int i = 0; i < Dt.Rows.Count; i++)
        {

            string TempStr = HeadStr;

            Sb.Append("{");
            for (int j = 0; j < Dt.Columns.Count; j++)
            {

                TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());

            }
            Sb.Append(TempStr + "},");

        }

        Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

        Sb.Append("]");

        string jqText = Sb.ToString();
        jqText = jqText.Replace("JSONKeyWords", "label");
        jqText = jqText.Replace("JSONID", "value");

        return jqText;

    }

}