﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRDashboards.aspx.cs" Inherits="HRDashboards" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elite - HR Reports</title>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
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
     <script src="OD/Scripts/jquery-1.9.1.js"></script>
    <script src="OD/Scripts/moment.js"></script>
    <script src="OD/Scripts/bootstrap.js"></script>
    <script src="OD/Scripts/bootstrap-datetimepicker.js"></script>
    <link href="OD/Content/bootstrap.css" rel="stylesheet" />
    <link href="OD/Content/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script>
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'DD-MMM-YYYY',
                ignoreReadonly: false
            });
        });
    </script>
</head>
<body style="margin:10px;">
    <form id="form2" runat="server">
    <div style="width: 100%; background-color: #EEEEEE">
        <asp:LinkButton ID="lnkLateComingDaily" runat="server" OnClick="lnkLateComingDaily_Click" CssClass="KBAdd">Late Coming - Daily</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkLateComingMonthly" runat="server" OnClick="lnkLateComingMonthly_Click"
            CssClass="KBAdd">Late Coming - Monthly</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkAttendence" runat="server" OnClick="lnkAttendence_Click" CssClass="KBAdd">Attendence</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkLeaveWithoutInform" runat="server" OnClick="lnkLeaveWithoutInform_Click"
            CssClass="KBAdd" Visible="false">Leave without Inform</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkLeaveRequests" runat="server" OnClick="lnkLeaveRequests_Click"
            CssClass="KBAdd">Leave Requests</asp:LinkButton>
&nbsp;
        <asp:LinkButton ID="lnkODRequests" runat="server" OnClick="lnkODRequests_Click"
            CssClass="KBAdd">OD Requests</asp:LinkButton>
        <asp:LinkButton ID="lnkODDailyView" runat="server" OnClick="lnkODDailyView_Click"  CssClass="KBAdd">OD/Leave Daily Report M1 above</asp:LinkButton>

            &nbsp;
            <asp:LinkButton ID="lnkQuotaView" runat="server" OnClick="lnkQuotaView_Click"
            CssClass="KBAdd" Visible="false">Leave Quota View</asp:LinkButton>
    </div>
    <b>
        <h2 id="divReportName" runat="server"></h2>
    </b>
        <h4 id="divInfo" style="color:blue" runat="server"></h4>
    <b>
        <h3 id="divError" style="color:red;" runat="server"></h3>
    </b>
    <div id="divPeriod1" runat="server" style="background-color: #F0F0F0; width: 100%;
        padding-top: 10px; padding-bottom: 10px;">
        <b>Year</b>:
        <asp:DropDownList ID="ddlSYear" runat="server">
        </asp:DropDownList>
        &nbsp;<b>Month</b>:
        <asp:DropDownList ID="ddlSMonth" runat="server">
        </asp:DropDownList>  
        &nbsp;<asp:Button ID="btnShowReport" runat="server" OnClick="btnShowReport_Click"
            Text="Show Report" ValidationGroup="g1" />
        <asp:Button ID="btnShowReportSAP" runat="server" OnClick="btnShowReportSAP_Click"
            Text="Show Report For SAP" ValidationGroup="g1" />
        <asp:Button ID="btnExport2" OnClick="btnExport_Click" runat="server" Text="Export To Excel" class="pure-button pure-button-active" />
    </div>

<div class="row" style="margin-top:10px;" id="divPeriod2" runat="server">
                                <div class="col-sm-1 col-md-1">
                                    <label for="userid">Date</label>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker1' runat="server">
                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" Style="background-color: white"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            <div class="col-sm-2 col-md-2">&nbsp;<asp:Button ID="btnShowReport2" runat="server" OnClick="btnShowReport_Click" Text="Show Report"  /></div>
                                <div class="col-sm-5 col-md-5"><asp:Button ID="btnExport3" OnClick="btnExport_Click" runat="server" Text="Export To Excel" class="pure-button pure-button-active" /></div>
                            </div>
<div style="width:90%; text-align:right; align:right" id="divPeriod3" runat="server"><asp:Button ID="btnExport1" OnClick="btnExport_Click" runat="server" Text="Export To Excel" class="pure-button pure-button-active" /></div>
    <table width="100%">
        <tr id="trReport1" runat="server">
            <td valign="top" align="left">
             <div><h3 id="reportCaption1" runat="server"></h3></div>
              <asp:DataGrid ID="grid1" runat="server"  AutoGenerateColumns="True" Width="1400px" BackColor="White" BorderColor="#F0F0F0" BorderStyle="Outset" BorderWidth="1px"
                           CellPadding="5" GridLines="Both" CellSpacing="5" >
                            <FooterStyle BackColor="White" ForeColor="#000066"  />
                            <ItemStyle ForeColor="#000000" HorizontalAlign="Left" BackColor="#F0F0F0" />                            
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle Font-Bold="true" ForeColor="#000000" HorizontalAlign="Left" BackColor="#E89F65" />
                            <AlternatingItemStyle BackColor="WhiteSmoke" />                                
                            </asp:DataGrid>
                <br /><h3 id="emptyRecords1" runat="server" visible="false">0 records found</h3>
            </td>         
        </tr>
        <tr id="trReport2" runat="server">
            <td valign="top" align="left">
                <div><h3 id="reportCaption2" runat="server"></h3></div>
              <asp:DataGrid ID="grid2" runat="server"  AutoGenerateColumns="True" Width="1400px" BackColor="White" BorderColor="#F0F0F0" BorderStyle="Outset" BorderWidth="1px"
                           CellPadding="5" GridLines="Both" CellSpacing="5" >
                            <FooterStyle BackColor="White" ForeColor="#000066"  />
                            <ItemStyle ForeColor="#000000" HorizontalAlign="Left" BackColor="#F0F0F0" />                            
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle Font-Bold="true" ForeColor="#000000" HorizontalAlign="Left" BackColor="#E89F65" />
                            <AlternatingItemStyle BackColor="WhiteSmoke" />                                
                            </asp:DataGrid>
                <br /><h3 id="emptyRecords2" runat="server" visible="false">0 records found</h3>
            </td>         
        </tr>        
    </table>
    </form>
</body>
</html>
