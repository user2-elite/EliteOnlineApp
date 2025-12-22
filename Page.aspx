<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="Page.aspx.cs" Inherits="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
.table
{
	padding:3px;
	border-color: #CCCCCC;
	border-width:1px;
}
</style>
<h3><div id="divHeader" runat="server"></div></h3>
<p><div id="divContent" runat="server"></div></p>
<br />
        <div style="text-align:right; width:800px;">
           <a href="Javascript:history.back(-1);" id="lnkBack" runat="server" class="btn btn-success"><span class="glyphicon glyphicon-arrow-left"
                                        aria-hidden="true"></span> Back</a>
                                        </div>
</asp:Content>

