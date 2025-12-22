<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ConvertHTMLtoPDF.aspx.cs" Inherits="ConvertHTMLtoPDF" %>
<html><head>
<title></title>
</head>
<body>
<form id="form1" runat="server">
    <h2>Convert HTML to PDF</h2>
    <p>
        This demo shows how to use iTextSharp to convert an HTML document (<code>~/HTMLTemplate/Receipt.htm</code>) into a PDF.
        Enter the values for the receipt in the user interface below and then click "Create Receipt." iTextSharp will then create
        a PDF based on the HTML defined in the HTML template file (<code>~/HTMLTemplate/Receipt.htm</code>) using the values you
        entered below. (Of course, in a real-world application the values for the receipt would come from a database...)
    </p>
   
    <p>
        <asp:Button ID="btnCreatePDF" runat="server" Text="Create Receipt" 
            onclick="btnCreatePDF_Click" />
    </p>
    </form>
</body>
</html>
