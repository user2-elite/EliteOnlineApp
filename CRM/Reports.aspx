<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reports.aspx.cs" Inherits="reports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elite Complaint Log system</title>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" /> 
<style type="text/css">

.KBAdd{
border:2px solid #819BCB;-webkit-box-shadow: #A5B8DA 0px 21px 0px inset;-moz-box-shadow: #A5B8DA 0px 21px 0px inset; box-shadow: #A5B8DA 0px 21px 0px inset; -webkit-border-radius: 3px; -moz-border-radius: 3px;border-radius: 3px;font-size:12px;font-family:arial, helvetica, sans-serif; padding: 10px 10px 10px 10px; text-decoration:none; display:inline-block;text-shadow: -1px -1px 0 rgba(0,0,0,0.3);font-weight:bold; color: #FFFFFF;
 background-color: #a5b8da; background-image: -webkit-gradient(linear, left top, left bottom, from(#a5b8da), to(#7089b3));
 background-image: -webkit-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -moz-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -ms-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -o-linear-gradient(top, #a5b8da, #7089b3);
 background-image: linear-gradient(to bottom, #a5b8da, #7089b3);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#a5b8da, endColorstr=#7089b3);
}

.KBAdd:hover{
 border:2px solid #819BCB;
 background-color: #819bcb; background-image: -webkit-gradient(linear, left top, left bottom, from(#819bcb), to(#536f9d));
 background-image: -webkit-linear-gradient(top, #819bcb, #536f9d);
 background-image: -moz-linear-gradient(top, #819bcb, #536f9d);
 background-image: -ms-linear-gradient(top, #819bcb, #536f9d);
 background-image: -o-linear-gradient(top, #819bcb, #536f9d);
 background-image: linear-gradient(to bottom, #819bcb, #536f9d);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#819bcb, endColorstr=#536f9d);
}
</style>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
</head>
<body>
  
<form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
<table width="100%">

                        <tr>
                            <td valign="top" >
                            <br />
    <%--<asp:LinkButton ID="lnkDaily" runat="server" onclick="lnkDaily_Click" Font-Bold="true" Font-Size="14px">Datewise Report</asp:LinkButton>--%>
<!--&nbsp;|
    <asp:LinkButton ID="lnkWeekly" runat="server" onclick="lnkWeekly_Click" Font-Bold="true" Font-Size="14px">Weekly Report</asp:LinkButton>    
-->
   <b><div id="divReportName" runat="server" style="color:#FFFFFF; background-color:#a5b8da; margin:1px; padding:10px; font-size:16px;"></div></b>
        <div id="divDailyFilter" runat="server" visible="false" style="color:#FFFFFF; background-color:#a5b8da; margin:1px; padding:10px;">        
                                     <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                        <img src="images/cal.png" id="img1" />
                                        (mm/dd/yyyy)<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img1"
                                            TargetControlID="txtFromDate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>      

                                            <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                        <img src="images/cal.png" id="img2" />
                                        (mm/dd/yyyy)<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtToDate"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img2"
                                            TargetControlID="txtToDate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
    &nbsp;<asp:Button ID="btnDailyReport" runat="server" onclick="btnDailyReport_Click" Text="Show Report" />
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnExport1" OnClick="btnExport_Click" runat="server" Text="Export To Excel" Width="120px" Height="50px" />
    </div>
<!--
    <div id="divWeeklyFilter" runat="server" visible="false">
    <B>Year</B>: 
    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
    &nbsp;<B>Week</B>: 
    <asp:DropDownList ID="ddlWeek" runat="server"></asp:DropDownList>
    &nbsp;<asp:Button ID="btnWeeklyReport" runat="server" onclick="btnWeeklyReport_Click" Text="Show Report" />
    </div>
-->    
    <asp:GridView ID="gvReport" runat="server" AllowPaging="True" AutoGenerateColumns="True"
                                            EmptyDataText="No Data available" Width="1400px" BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" OnPageIndexChanging="gvReport_PageIndexChanging" PageSize="50"  CellPadding="3">
                    
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000000" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="GridPageNum"/>
                    <HeaderStyle BackColor="maroon" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />

                    </asp:GridView>
                     <div>
                <asp:Button ID="btnExport2" OnClick="btnExport_Click" runat="server" Text="Export To Excel" Width="120px" Height="50px" />
            </div>
</td>
</tr>
</table>
  </form>
</body>
</html>
