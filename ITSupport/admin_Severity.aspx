<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="admin_Severity.aspx.cs" Inherits="admin_Severity" Title="admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div id="divHelpDesk" runat="server" style="position:absolute; top:1px;left:0px; font-size:14px; font-weight:bold; color:Maroon; width:100%; background-color:#F0F0F0; visibility:hidden;"></div>
    <table align="center" id="ResolverTBl" runat="server" width="85%"
        border="1" bordercolor="#E89F65" cellspacing="0">
        <tr>
            <td align="center" bgcolor="#E89F65">
                <span style="color: #ffffff; font-size: 12pt;"><strong>Severity Master</strong></span></td>
        </tr>
        <tr>
            <td align="center">
                <span></span><strong><span style="color: #000099"></span></strong>&nbsp;
                <br />
                <table id="addtbl" runat="server" width="95%" cellspacing="0">
                    <tr>
                        <td valign="top" align="left">
                            Severity Name&nbsp;</td>
                        <td style="width: 902px" align="left">
                            <asp:TextBox ID="SeverityCode" runat="server" MaxLength="25" Width="92px"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="SeverityCode"
                                ErrorMessage="*" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SeverityCode"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        <td style="width: 1077px; text-align: left">
                            Response Time&nbsp;(in Minutes)</td>
                        <td align="left">
                            <asp:TextBox ID="txtResponseTime" runat="server" Width="67px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtResponseTime"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtResponseTime"
                                ErrorMessage="*" MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" width="20%">
                            Status</td>
                        <td style="width: 902px" align="left">
                            <asp:DropDownList ID="Status" runat="server">
                                <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 1077px" align="left">
                            <p class="MsoNormal" style="margin: 0in 0in 0pt">
                                Resolution Time (in Minutes)</p>
                        </td>
                        <td style="width: 452px; text-align: left">
                            <asp:TextBox ID="txtResolutionTime" runat="server" Width="67px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtResolutionTime"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtResolutionTime"
                                ErrorMessage="*" MaximumValue="15000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtResponseTime"
                                ControlToValidate="txtResolutionTime" ErrorMessage="*" Operator="GreaterThan"
                                Type="Integer"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="left" valign="top" width="20%">
                            Category</td>
                        <td align="left" style="width: 902px">
                            <asp:DropDownList ID="GroupDD" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCat" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="GroupErrorLb" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td></tr>
                                <tr>
                        <td colspan="2" style="text-align: left">
                            &nbsp;&nbsp;
                            <asp:Button ID="SeveritySubmit" runat="server" OnClick="SeveritySubmit_Click" Text="Add" />&nbsp;&nbsp;
                            <asp:Button ID="ClearForm" runat="server" OnClick="ClearForm_Click" Text="Clear"
                                ValidationGroup="Group5" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" valign="top">
                            <%--<span style="font-size: 8pt; color: #ff6666"><em>NB: Response time should be included
                                in the Resolution time. eg. If total SLA is 60 min and if response time is 10 min,
                                resolution time will be 60 min.</em></span>--%></td>
                    </tr>
                </table>
                &nbsp;<br />
                <p>Severity1, Severity2, Severity3 can be used for request received from Board Members, Depots, Billing Division, Vertical/Department Heads etc. or for any other high priority requests.</p>
                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="SeverityID,Status" DataSourceID="SqlDataSource3" EmptyDataText="There are no data records to display."
                    Width="95%" OnRowCommand="GridView3_RowCommand" BackColor="White" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Smaller" AllowSorting="True"
                    PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="SeverityID" HeaderText="ID" SortExpression="SeverityID"
                            Visible="False" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category Name" SortExpression="CategoryName">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="Severity" HeaderText="Name" SortExpression="Severity">
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SResponseTime" HeaderText="Response Time" SortExpression="SResponseTime">
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SResolutionTime" HeaderText="Resolution Time" SortExpression="SResolutionTime">
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                         
                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ResolverUpdate" runat="server" CausesValidation="false" CommandArgument='<%# Bind("SeverityID") %>' 
                                    Text="View/Edit" CommandName="editdetails"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20%" />
                            <HeaderStyle Font-Size="Small" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" Font-Size="Small" />
                    <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetAllSeverities"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="HelpDeskID" SessionField="HelpDeskID"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
