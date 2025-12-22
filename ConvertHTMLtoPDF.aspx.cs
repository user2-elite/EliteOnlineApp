using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class ConvertHTMLtoPDF : System.Web.UI.Page
{
    protected void btnCreatePDF_Click(object sender, EventArgs e)
    {
        // Create a Document object
        var document = new Document(PageSize.A4, 50, 50, 25, 25);
        string Month = System.DateTime.Now.Month.ToString();
        string Year = System.DateTime.Now.Year.ToString(); 
        // Create a new PdfWrite object, writing the output to a MemoryStream
        var output = new MemoryStream();
        var writer = PdfWriter.GetInstance(document, output);

        // Open the Document for writing
        document.Open();

        // Read in the contents of the Receipt.htm HTML template file
        string contents = File.ReadAllText(Server.MapPath("template.htm"));

        // Replace the placeholders with the user-specified text
        //contents = contents.Replace("[ORDERID]", "");
        //contents = contents.Replace("[TOTALPRICE]", "");
        //contents = contents.Replace("[ORDERDATE]", "");        
        //contents = contents.Replace("[ITEMS]", "");

        var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents), null);
        foreach (var htmlElement in parsedHtmlElements)
            document.Add(htmlElement as IElement);


        // You can add additional elements to the document. Let's add an image in the upper right corner
        //var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/logo.jpg"));
        //logo.SetAbsolutePosition(440, 800);
        //document.Add(logo);

        document.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=payslip-{0}.pdf", Month+""+Year));
        Response.BinaryWrite(output.ToArray());
    }
}