<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITDashboard.aspx.cs" Inherits="_ITDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elite ITSupport - Dashboard</title>
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
</head>
<body>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <form id="form2" runat="server">
    <table width="100%">
        <tr>
            <td valign="top">
                <br />
                <asp:LinkButton ID="lnkClosedRequests" runat="server" OnClick="lnkClosedRequests_Click"
                    CssClass="KBAdd">Request Status</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkUtilization" runat="server" OnClick="lnkUtilization_Click"
                    CssClass="KBAdd">Utilization Summary</asp:LinkButton>
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
                    BackSecondaryColor="AliceBlue" BackGradientStyle="TopBottom" BorderColor="DarkBlue" Palette="Grayscale">
                    <Titles>
                        <asp:Title Font="Calibiri, 12pt, style=Bold, Italic" >
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="Green" Legend="Legend1">
                        </asp:Series>
                        <asp:Series Name="Series2" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="Blue" Legend="Legend2">
                        </asp:Series>
                         <asp:Series Name="Series3" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="Red" Legend="Legend3">                        
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BackColor="#FFFFFF" Area3DStyle-Enable3D="true" Area3DStyle-IsClustered="true" Area3DStyle-LightStyle="Realistic" Area3DStyle-WallWidth="25" 
                            BorderColor="DarkBlue">
                        </asp:ChartArea>
                    </ChartAreas>
                      <Legends>
                        <asp:Legend Name="Legend1" Title="Closed" BackColor="Green" Docking="right" Alignment=Near LegendStyle=Row >
                        </asp:Legend>
                          <asp:Legend Name="Legend2" Title="Open" BackColor="Blue" Docking="right" Alignment=Near LegendStyle=Row >
                        </asp:Legend>
                         <asp:Legend Name="Legend3" Title="Violated" BackColor="Red" Docking="right" Alignment=Near LegendStyle=Row >
                        </asp:Legend>
                    </Legends>     
                </asp:Chart>
                  <asp:Chart ID="cDashboardsCharts2" runat="server" Width="1200" Height="450" BackColor="LightSalmon"  
                    BackSecondaryColor="AliceBlue" BackGradientStyle="TopBottom" BorderColor="DarkBlue" Palette="Grayscale">
                    <Titles>
                        <asp:Title Font="Calibiri, 12pt, style=Bold, Italic" >
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series Name="Series1" YValueType="Int32" IsValueShownAsLabel="true"  ChartArea="ChartArea1" ChartType="Column" Color="Green">
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
