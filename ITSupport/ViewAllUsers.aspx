<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewAllUsers.aspx.cs" Inherits="ViewAllUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: absolute; left: 0px; top: 0px;">
        <table width="100%" bgcolor="#E89F65" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <strong><span style="font-size: 18px; color: Black;">View All Users</span></strong>
                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="ID" DataSourceID="SqlDataSource1" EmptyDataText="No Users available"
                        Width="1400px" BackColor="White" BorderColor="#404040" BorderStyle="Solid" BorderWidth="1px"
                        PageSize="20" CellPadding="3" EnableModelValidation="True" OnRowDataBound="gvUsers_RowDataBound"
                        OnRowUpdating="gvUsers_RowUpdating">
                        <Columns>
                            <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                            <asp:TemplateField HeaderText="Title">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="Title" runat="server" SelectedValue='<%# Bind("Title") %>'>
                                        <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                        <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                                        <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" SortExpression="EmpCode">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                                <HeaderStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UID" HeaderText="UID" SortExpression="UID" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="SystemType">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="SystemType" runat="server" SelectedValue='<%# Bind("SystemType") %>'>
                                        <asp:ListItem Text="Nil" Value="Nil"></asp:ListItem>
                                        <asp:ListItem Text="Laptop" Value="Laptop"></asp:ListItem>
                                        <asp:ListItem Text="Desktop" Value="Desktop"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hUID" runat="server" Value='<%# Bind("UID") %>'></asp:HiddenField>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Labelsystype" runat="server" Text='<%# Bind("SystemType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asset Name">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddAssetName" runat="server" DataSourceID="dsAssets" DataTextField="AssetName" DataValueField="AssetName" SelectedValue='<%# Bind("AssetName") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAssetName" runat="server" Text='<%# Bind("AssetName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MouseAllotted">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="MouseAllotted" runat="server" SelectedValue='<%# Bind("MouseAllotted") %>'>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelMouse" runat="server" Text='<%# Bind("MouseAllotted") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ExtMemoryAllotted">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ExtMemoryAllotted" runat="server" SelectedValue='<%# Bind("ExtMemoryAllotted") %>'>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelMemory" runat="server" Text='<%# Bind("ExtMemoryAllotted") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DataCardAllotted">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DataCardAllotted" runat="server" SelectedValue='<%# Bind("DataCardAllotted") %>'>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabeldataCard" runat="server" Text='<%# Bind("DataCardAllotted") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Area" HeaderText="Area">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlArea" runat="server" DataSourceID="dsArea" DataTextField="AreaName"
                                        DataValueField="AreaName" SelectedValue='<%# Bind("Area") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Area") %>' ID="lblArea"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Department" HeaderText="Department">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" DataSourceID="dsDepartment" DataTextField="DepartmentName"
                                        DataValueField="DepartmentName" SelectedValue='<%# Bind("Department") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Department") %>' ID="lblDepartment"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Location" HeaderText="Location">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlLocation" runat="server" DataSourceID="dsLocation" DataTextField="LocationDetails"
                                        DataValueField="LocationDetails" SelectedValue='<%# Bind("Location") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Location") %>' ID="lblLocation"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Active?">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="Status" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Createdon" HeaderText="Createdon" SortExpression="Createdon"
                                ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="#FF0000" Font-Bold="true" Font-Size="16px"
                            HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#E89F65" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="WhiteSmoke" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="dsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetLocations"
                        SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetDepartments"
                        SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="dsArea" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetAreas"
                        SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="dsAssets" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="IT_GetAssetNames"
                        SelectCommandType="StoredProcedure"></asp:SqlDataSource>                        
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="ViewAllUsers"
                        SelectCommandType="StoredProcedure">
                        <%-- <UpdateParameters>
                            <asp:SessionParameter DefaultValue="0" Name="UID" SessionField="EditUID" Type="String" />
                            <asp:Parameter Name="Title" Type="String" />
                            <asp:Parameter Name="EmpCode" Type="String" />
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="Email" Type="String" />
                            <asp:Parameter Name="Phone" Type="String" />
                            <asp:Parameter Name="Password" Type="String" />
                            <asp:Parameter Name="SystemType" Type="String" />
                            <asp:Parameter Name="AssetName" Type="String" />
                            <asp:Parameter Name="MouseAllotted" Type="String" />
                            <asp:Parameter Name="ExtMemoryAllotted" Type="String" />
                            <asp:Parameter Name="DataCardAllotted" Type="String" />
                            <asp:Parameter Name="Department" Type="String" />
                            <asp:Parameter Name="Location" Type="String" />
                            <asp:Parameter Name="Area" Type="String" />
                            <asp:Parameter Name="Status" Type="String" />
                        </UpdateParameters>--%>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
