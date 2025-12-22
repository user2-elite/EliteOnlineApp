<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="admin_AssignRole.aspx.cs" Inherits="admin_AssignRole" Title="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div id="divHelpDesk" runat="server" style="position:absolute; top:1px;left:0px; font-size:14px; font-weight:bold; color:Maroon; width:100%; background-color:#F0F0F0; visibility:hidden;"></div>
    <table align="center" id="ResolverTBl" runat="server" style="font-size: 12pt" width="95%"
        border="1" bordercolor="#E89F65" cellspacing="0">
        <tr>
            <td align="center" bgcolor="#E89F65" style="height: 22px">
                <strong><span style="color: #ffffff">Admin / Resolvers / Coordinators Master</span></strong></td>
        </tr>
        <tr>
            <td align="center">
                <table id="addtbl" runat="server" width="100%" cellspacing="0" style="background-color:#F0F0F0">
                    <tr>
                        <td valign="top" width="10%" align="left">
                            <span style="font-size: 10pt">
                            User ID&nbsp; </span>
                        </td>
                        <td width="90%" align="left">
                            <asp:TextBox ID="ResolverTxt" runat="server" MaxLength="25" Width="92px"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ResolverTxt"
                                ErrorMessage="*" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ResolverTxt"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>                     
                    </tr>
                    <tr>
                        <td valign="top" align="left" >
                            <span style="font-size: 10pt">
                            Group</span>&nbsp;
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="GroupDD" runat="server">
                            </asp:DropDownList>
                            <asp:Label ID="GroupErrorLb" runat="server" ForeColor="Red" Text="*" Visible="False"></asp:Label></td>                      
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            <span style="font-size: 10pt">
                            Status</span></td>
                        <td align="left">
                            <asp:DropDownList ID="Status" runat="server">
                                <asp:ListItem Selected="True" Value="Active">Active</asp:ListItem>
                                <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </td>                       
                    </tr>
                    <tr>
                     <td style="text-align: left">
                            <span style="font-size: 10pt">
                            Role</span>&nbsp;</td>
                        <td align="left">
                            <asp:DropDownList ID="RoleDD" runat="server">
                                <asp:ListItem Value="1">Admin</asp:ListItem>
                                <asp:ListItem Value="2">Coordinator</asp:ListItem>
                                <asp:ListItem Selected="True" Value="3">Resolver</asp:ListItem>
                            </asp:DropDownList><span style="font-size: 10pt; color: maroon;">Once the user mapped to Admin/Coordinator, he can view all requests in all groups.</span></td>                   
                    </tr>
                    <tr>
                     <td colspan="2" align="left">
                            <asp:Button ID="ResolverSubmit" runat="server" OnClick="ResolverSubmit_Click" Text="Add" /></td>
                    </tr>
                </table>
                &nbsp;<br />
                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="ResolverID,ResolverStatus,Admin" DataSourceID="SqlDataSource3"
                    EmptyDataText="There are no data records to display." Width="99%" OnRowCommand="GridView3_RowCommand" BorderColor="#CCCCCC"
                    BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="Smaller" AllowSorting="True"
                    PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="ResolverID" HeaderText="ID" InsertVisible="False" SortExpression="ResolverID"
                            Visible="False" />
                        <asp:BoundField DataField="Resolver" HeaderText="User ID" ReadOnly="True" SortExpression="Resolver" />
                        <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResolverStatus" HeaderText="Status" SortExpression="ResolverStatus" />
                        <asp:BoundField DataField="GroupName" HeaderText="Group" SortExpression="GroupName" />                        
                         <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />                            
                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ResolverUpdate" runat="server" CausesValidation="false" CommandArgument='<%# Bind("ResolverID") %>'
                                    Text="View/Edit" CommandName="editdetails"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetALLResolvers"
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
