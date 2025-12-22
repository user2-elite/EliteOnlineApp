<%@ Page Language="C#" MasterPageFile="~/Masterpage.master" AutoEventWireup="true"
    CodeFile="admin_SubCategory.aspx.cs" Inherits="admin_SubCategory" Title="Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div id="divHelpDesk" runat="server" style="position:absolute; top:1px;left:0px; font-size:14px; font-weight:bold; color:Maroon; width:100%; background-color:#F0F0F0; visibility:hidden;"></div>
    <table align="center" border="1" width="95%" bordercolor="#E89F65" cellspacing="0">
        <tr>
            <td align="center" bgcolor="#E89F65" style="height: 23px">
                <strong><span style="color: #ffffff; font-size: 12pt;">Category Master</span></strong></td>
        </tr>
        <tr>
            <td align="center" style="height: 240px">
                <table align="center" width="99%" cellspacing="0">                    
                    <tr>
                        <td valign="top" align="left" style="height: 199px">
                            <table width="100%">
                            <tr>
                                    <td valign="top" align="left" width="15%">
                                        Group Name:</td><td valign="top" align="left" width="75%">
                                            <asp:DropDownList ID="DDGroup" runat="server">
                                            </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDGroup" InitialValue="0" ErrorMessage="*" ValidationGroup="Group1"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left">
                                        Category Name:</td><td valign="top" align="left">
                                        <asp:TextBox ID="CateogryTxt" runat="server" MaxLength="49" Width="85px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CateogryTxt" ErrorMessage="*" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="2" valign="top" align="left"><asp:Button ID="AddnewCat" runat="server" Text="Add New Category" OnClick="AddnewCat_Click" ValidationGroup="Group1" /></td>
                                </tr>
                            </table>
                            &nbsp;&nbsp;<br />
                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                DataKeyNames="RequestType1ID" DataSourceID="SqlDataSource1" EmptyDataText="No Categories found."
                                Width="80%">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:TemplateField HeaderText="Category " SortExpression="RequestType1">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" MaxLength="49" Text='<%# Bind("RequestType1") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("RequestType1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Group Name" DataField="GroupName" />
                                    <asp:TemplateField HeaderText="SubCategories" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton5" runat="server" CausesValidation="False" CommandName="Select"
                                                Text="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Size="Small" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle HorizontalAlign="Left" />
                                <SelectedRowStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" BackColor="#B94629" ForeColor="White" />
                                <AlternatingRowStyle CssClass="GridAlternateRow" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                                DeleteCommand="DELETE FROM [RequestType1] WHERE [RequestType1ID] = @RequestType1ID" ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>"
                                UpdateCommand="UpdateRequestType1" UpdateCommandType="StoredProcedure" SelectCommand="GetRequestType1"
                                SelectCommandType="StoredProcedure">
                                <UpdateParameters>
                                    <asp:Parameter Name="RequestType1ID" Type="int32" />
                                    <asp:Parameter Name="RequestType1" Type="String" />
                                    <asp:Parameter Name="GroupName" Type="String" />
                                    <asp:SessionParameter DefaultValue="0" Name="CreatedBy" SessionField="LoggedInUser" Type="String" />
                                    <asp:SessionParameter DefaultValue="0" Name="HelpDeskID" SessionField="HelpDeskID" Type="Int32" />                                    
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="HelpDeskID" SessionField="HelpDeskID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>                       
                    <%--    <td valign="top" align="left">
                            <table width="100">
                                <tr>
                                    <td valign="top">
                                        Sub Category :
                                        <asp:TextBox ID="SubCatTxt" runat="server" MaxLength="49" EnableViewState="False"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="SubCatTxt"
                                            ErrorMessage="*" ValidationGroup="Group2"></asp:RequiredFieldValidator></td>
                                    <td valign="top">
                                        <asp:Button ID="AddSubCategory" runat="server" Text="Add" OnClick="AddSubCategory_Click"
                                            ValidationGroup="Group2" /></td>
                                </tr>
                            </table>
                            <span style="font-size: 8pt"></span><strong><span style="font-size: 10pt"></span></strong>
                            <asp:Label ID="MainCategory" runat="server" Font-Italic="False" Font-Size="10pt"
                                Font-Bold="True" ForeColor="Black"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                DataKeyNames="RequestType2ID" DataSourceID="SqlDataSource2" EmptyDataText="There are no data records to display."
                                Width="80%" OnPreRender="GridView2_PreRender" OnRowEditing="GridView2_RowEditing"
                                OnRowUpdating="GridView2_RowUpdating">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:BoundField DataField="RequestType2" HeaderText="RequestType2" SortExpression="RequestType2" />
                                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="StatusDDL" runat="server">
                                                <asp:ListItem>Active</asp:ListItem>
                                                <asp:ListItem>Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridHeader" ForeColor="White" />
                                <AlternatingRowStyle CssClass="GridAlternateRow" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                                ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" UpdateCommand="UpdateRequestType2"
                                UpdateCommandType="StoredProcedure" SelectCommand="GetRequestType2" SelectCommandType="StoredProcedure">
                                <UpdateParameters>
                                    <asp:Parameter Name="RequestType2ID" Type="Int32" />
                                    <asp:Parameter Name="RequestType2" Type="String" />                                      
                                    <asp:ControlParameter ControlID="GridView1" Name="RequestType1ID" PropertyName="SelectedValue"
                                        Type="Int32" DefaultValue="0" />
                                    <asp:SessionParameter DefaultValue="0" Name="CreatedBy" SessionField="LoggedInUser"
                                        Type="String" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="GridView1" Name="RequestType1ID" PropertyName="SelectedValue"
                                        Type="Int32" DefaultValue="0" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            &nbsp;
                        </td>--%>

                    </tr>
                </table></td>
        </tr>
    </table>
</asp:Content>
