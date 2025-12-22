<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AssetMaster.aspx.cs" Inherits="AssetMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <link rel="stylesheet" href="JQuery1.11.4/jquery-ui.css">
    <script src="JQuery1.11.4/jquery-1.10.2.js"></script>
    <script src="JQuery1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txtLastActivityDate").datepicker();
        });
    </script>--%>
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
                        Add New Assets
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
                                Asset name<span style="color: #FF0000">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAssetName" runat="server" Width="273px" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAssetName" runat="server" ControlToValidate="txtAssetName"
                                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                System Type <span style="color: #FF0000">*</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAssetType" runat="server" Width="273px">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Nil" Value="Nil"></asp:ListItem>
                                    <asp:ListItem Text="LapTop" Value="LapTop"></asp:ListItem>
                                    <asp:ListItem Text="DeskTop" Value="DeskTop"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAssetType"
                                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Asset Description<span style="color: #FF0000">*</span>
                            </td> 
                            <td align="left">
                                <asp:TextBox ID="txtAssetDescription" runat="server" Width="273px" Rows="3" Columns="25" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAssetDescription"
                                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Scheduled Maintenance Responsible To<span style="color: #FF0000">*</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlResponsible" runat="server" Width="273px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlResponsible"
                                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Latest Maintenance Activity Updated On<span style="color: #FF0000">*</span>
                            </td>
                            <td align="left">

                            <asp:TextBox ID="txtLastActivityDate" runat="server" Width="273px"></asp:TextBox>
                                        <img src="images/cal.png" id="img12" />
                                        (MM/dd/yyyy)<asp:RequiredFieldValidator ID="rfv12" runat="server" ControlToValidate="txtLastActivityDate"
                                            ErrorMessage="Required" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="img12"
                                            TargetControlID="txtLastActivityDate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>                                      
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Maintenance Activity Frequency (In Days)<span style="color: #FF0000">*</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtActivityFrequency" runat="server" Width="80px" MaxLength="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtActivityFrequency"
                                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <br />
                                <font color="blue">(Note: Reminder Mail trigger for the asset maintenance activity will
                                    be based on this duration)</font>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            </td>
                            <td>
                                <asp:ImageButton ID="ibtnSubmit" runat="server" ImageUrl="images/b3.gif" OnClick="ibtnSubmit_Click" />
                                &nbsp;<%--<asp:ImageButton ID="ibtnCancel" runat="server" CausesValidation="false"
                            ImageUrl="images/b2.gif" onclick="ibtnCancel_Click" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align: left; font-weight: bold; position: relative; top: 100px;
            left: 10px;">
            <a href="AssetMaster.aspx?edit=1" class="KBAdd" runat="server" id="lnkViewDetails"><b>
                View/Edit Asset List</b></a>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlList" Visible="false">
        <div style="text-align: left; color: maroon; font-weight: bold; position: relative;
            width: 90%; text-align: right;">
            <a href="AssetMaster.aspx" class="KBAdd"><b>Add New Assets</b></a>
        </div>
        <h1>
            View Assets</h1>
        <hr width="100%; height:1px" />
        <div>
            <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyField="ID" Font-Bold="False" Font-Size="Medium" ForeColor="#333333" GridLines="None"
                OnItemCommand="grid1_ItemCommand" Width="100%">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                <ItemStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="AssetName" HeaderText="Asset Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="AssetDescription" HeaderText="Asset Description"></asp:BoundColumn>
                    <asp:BoundColumn DataField="AssetType" HeaderText="Asset Type"></asp:BoundColumn>
                    <asp:BoundColumn DataField="LastMntActivityUpdatedOn" HeaderText="Latest Activity Updated On">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NextActivityFrequencyInDays" HeaderText="Activity Frequency (In Days)">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NAME" HeaderText="Activity Responsible To"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="cmEdit"
                                Text="View/Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <br />
    </asp:Panel>
</asp:Content>
