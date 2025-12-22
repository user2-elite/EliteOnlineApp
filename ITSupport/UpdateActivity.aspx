<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateActivity.aspx.cs" Inherits="UpdateActivity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link rel="stylesheet" href="JQuery1.11.4/jquery-ui.css">
    <script src="JQuery1.11.4/jquery-1.10.2.js"></script>
    <script src="JQuery1.11.4/jquery-ui.js"></script>--%>
<%--    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txtExectionDate").datepicker();
        });
    </script>
--%>    <style type="text/css">
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
        
        .messagePos
        {
            position: absolute;
            top: 350px;
            left: 200px;
            width: 700px;
        }
    </style>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:Panel runat="server" ID="pnlAddNew" Visible="false">
        <table cellspacing="0" cellpadding="0" width="95%" align="center" id="one-column-emphasis">
            <tr>
                <td class="title" valign="top" colspan="2">
                    <h1>
                        Update Activity
                    </h1>
                </td>
            </tr>
            <tr>
                <td height="148" valign="top" colspan="2">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblError" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Asset name
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAssetName" runat="server" Width="273px" MaxLength="100" Enabled="false"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                System Type
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSystemType" runat="server" Width="273px" MaxLength="100" Enabled="false"></asp:TextBox>                                                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Asset Description
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAssetDescription" runat="server" Width="273px" TextMode="MultiLine" MaxLength="1000" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Latest Maintenance Activity Updated On
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLastActivityDate" runat="server"  Width="273px" MaxLength="100" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Planned Activity Date
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPlanneddate" runat="server"  Width="273px" MaxLength="100" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Actual Execution Date
                            </td>
                            <td align="left">

                            <asp:TextBox ID="txtExectionDate" runat="server" Width="273px"></asp:TextBox>
                                        <img src="images/cal.png" id="img12" />
                                        (MM/dd/yyyy)<asp:RequiredFieldValidator ID="rfv12" runat="server" ControlToValidate="txtExectionDate"
                                            ErrorMessage="Required"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="img12"
                                            TargetControlID="txtExectionDate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>                                      
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Execution Details
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExecutionDetails" runat="server" Width="273px" Columns="25" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExecutionDetails"
                                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            </td>
                            <td>
                                <asp:ImageButton ID="ibtnSubmit" runat="server" ImageUrl="images/b3.gif" OnClick="ibtnSubmit_Click" />                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align: left; font-weight: bold; position: relative; top: 10px;
            left: 10px;">
            <a href="MaintenanceActivities.aspx" class="KBAdd" runat="server"><b>Back</b></a>
        </div>
    </asp:Panel> 
</asp:Content>
