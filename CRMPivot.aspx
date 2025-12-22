<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRMPivot.aspx.cs" Inherits="CRMPivot" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elite Complaint Log system - Pivot Charts</title>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <link href="styles/PivotStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .KBAdd
        {
            border: 2px solid #819BCB;
            -webkit-box-shadow: #A5B8DA 0px 21px 0px inset;
            -moz-box-shadow: #A5B8DA 0px 21px 0px inset;
            box-shadow: #A5B8DA 0px 21px 0px inset;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            padding: 10px 10px 10px 10px;
            text-decoration: none;
            display: inline-block;
            text-shadow: -1px -1px 0 rgba(0,0,0,0.3);
            font-weight: bold;
            color: #FFFFFF;
            background-color: #a5b8da;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#a5b8da), to(#7089b3));
            background-image: -webkit-linear-gradient(top, #a5b8da, #7089b3);
            background-image: -moz-linear-gradient(top, #a5b8da, #7089b3);
            background-image: -ms-linear-gradient(top, #a5b8da, #7089b3);
            background-image: -o-linear-gradient(top, #a5b8da, #7089b3);
            background-image: linear-gradient(to bottom, #a5b8da, #7089b3);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#a5b8da, endColorstr=#7089b3);
        }
        
        .KBAddSelected
        {
            border: 2px solid #CCCCCC;
            -webkit-box-shadow: #F0F0F0 0px 21px 0px inset;
            -moz-box-shadow: #DDDDDD 0px 21px 0px inset;
            box-shadow: #DDDDDD 0px 21px 0px inset;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            padding: 10px 10px 10px 10px;
            text-decoration: none;
            display: inline-block;
            text-shadow: -1px -1px 0 rgba(0,0,0,0.3);
            font-weight: bold;
            color: #f0f0f0;
            background-color: #EEEEEE;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#a5b8da), to(#7089b3));
            background-image: -webkit-linear-gradient(top, #F0F0F0, #CCCCCC);
            background-image: -moz-linear-gradient(top, #F0F0F0, #CCCCCC);
            background-image: -ms-linear-gradient(top, #F0F0F0, #CCCCCC);
            background-image: -o-linear-gradient(top, #F0F0F0, #CCCCCC);
            background-image: linear-gradient(to bottom, #F0F0F0, #CCCCCC);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=LIGHTBLUE, endColorstr=navy);
        }
        
        .KBAdd:hover
        {
            border: 2px solid #819BCB;
            background-color: #819bcb;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#819bcb), to(#536f9d));
            background-image: -webkit-linear-gradient(top, #819bcb, #536f9d);
            background-image: -moz-linear-gradient(top, #819bcb, #536f9d);
            background-image: -ms-linear-gradient(top, #819bcb, #536f9d);
            background-image: -o-linear-gradient(top, #819bcb, #536f9d);
            background-image: linear-gradient(to bottom, #819bcb, #536f9d);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#819bcb, endColorstr=#536f9d);
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div style="width: 100%; background-color: #EEEEEE">
        <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" CssClass="KBAdd">Back to Dashboards</asp:LinkButton>
        <asp:LinkButton ID="lnkTypeWise" runat="server" OnClick="lnkTypeWise_Click" CssClass="KBAdd">Defect Category Type/Unit Wise</asp:LinkButton>
        <asp:LinkButton ID="lnkRegionWise" runat="server" OnClick="lnkRegionWise_Click" CssClass="KBAdd">Defect Category Type/Region Wise</asp:LinkButton>
    </div>
    <div id="divPeriod1" runat="server" style="background-color: #F0F0F0; width: 100%;
        padding-top: 10px; padding-bottom: 10px;">
        <b>Start Year</b>:
        <asp:DropDownList ID="ddlSYear" runat="server">
        </asp:DropDownList>
        &nbsp;<b> Start Month</b>:
        <asp:DropDownList ID="ddlSMonth" runat="server">
        </asp:DropDownList>
        <b>End Year</b>:
        <asp:DropDownList ID="ddlEYear" runat="server">
        </asp:DropDownList>
        &nbsp;<b> End Month</b>:
        <asp:DropDownList ID="ddlEMonth" runat="server">
        </asp:DropDownList>
        &nbsp;<asp:Button ID="btnShowReport" runat="server" OnClick="btnShowReport_Click"
            Text="Show Report" ValidationGroup="g1" />     
    </div>
           <style>
        table
        {
            border-collapse: collapse;
            border-color: #ADD8E6;
            width:90%;
        }
        
        table, th, td
        {
            border: 1px solid #FFFFFF;
            padding: 10px;
            margin: 15px;
        }
    </style>
    <table width="100%">
        <tr>
            <td valign="top" align="left">
               <div id="div1" runat="server">
        </div>
            </td>       
        </tr>
          <tr>
            <td valign="top" align="left">
               <div id="div2" runat="server">
        </div>
            </td>       
        </tr>
    </table>
    </form>
</body>
</html>
