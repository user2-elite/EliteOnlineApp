<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="_Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elite ITSupport - Dashboard</title>
</head>
<body>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <form id="form2" runat="server">
    <table width="100%">
        <tr>
            <td valign="top">
                <br />
                <asp:LinkButton ID="lnkClosedRequests" runat="server" OnClick="lnkClosedRequests_Click"
                    Font-Bold="true" Font-Size="14px">Request Status</asp:LinkButton>
                &nbsp;|
                <asp:LinkButton ID="lnkUtilization" runat="server" OnClick="lnkUtilization_Click"
                    Font-Bold="true" Font-Size="14px">Utilization Summary</asp:LinkButton>
                <br />
                <b>
                    <div id="divReportName" runat="server">
                    </div>
                </b>
                <div id="divPeriod" runat="server" style="background-color:#F0F0F0;Width:1200; padding-top:10px; padding-bottom:10px;">                
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
                        Text="Show Report" />
                </div>
                <asp:Chart ID="cDashboardsCharts" runat="server" Width="1200" Height="450" BackColor="LightSalmon"  
                    BackSecondaryColor="AliceBlue" BackGradientStyle="TopBottom" BorderColor="DarkBlue">
                    <Titles>
                        <asp:Title Font="Calibiri, 12pt, style=Bold, Italic" >
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="Green">                        
                        </asp:Series>
                        <asp:Series Name="Series2" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="Blue">                        
                        </asp:Series>
                         <asp:Series Name="Series3" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="OrangeRed">                        
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BackColor="#FFFFFF" Area3DStyle-Enable3D="true" Area3DStyle-IsClustered="true" Area3DStyle-LightStyle="Realistic" Area3DStyle-WallWidth="25" 
                            BorderColor="DarkBlue">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
