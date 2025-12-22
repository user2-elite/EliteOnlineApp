using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class policy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["IAgree"] = null;
    }    

     protected void chkvalidate_CheckedChanged(object sender, EventArgs e)
     {
         try
         {
             if (chkvalidate.Checked)
             {
                 Session["IAgree"] = "1";
             }
         }
         catch
         {

         }
     }
}