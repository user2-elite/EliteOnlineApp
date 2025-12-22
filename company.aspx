<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3>My Company</h3>
<div>
<div style="text-align: left;">
<h4>(Watch Elite Corporate Video)</h4>
<iframe width="100%" height="550" src="https://www.youtube.com/embed/mQLsE7fAhLI" frameborder="0" allowfullscreen></iframe>
</div>
</div>
<p></p>
<p>
<object data="CorporatePresentation.pdf" type="application/pdf" width="100%" height="500" title="Corporate Presentation">
<p><a href="CorporatePresentation.pdf">Download Corporate Presentation</a></p>
</object>
</p>
</asp:Content>

