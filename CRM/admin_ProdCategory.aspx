<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="admin_ProdCategory.aspx.cs" Inherits="admin_ProdCategory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table align="center" border="1" bordercolor="#E89F65" width="85%" 
        cellspacing="0" style="background-color:#F0F0F0">
        <tr>
            <td bgcolor="#E89F65" align="center" style="height: 22px">
                <span style="color: #ffffff; font-size: 12pt;"><strong>
                Add/Edit Product Category Name</strong></span></td>
        </tr>
        <tr><td width="100%">
        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
   <table width="90%" align="center" cellspacing="0">
                    <tr>
                        <td align="left" colspan="2">
                            Category Name:
                            <asp:TextBox ID="txtProductCategory" runat="server" MaxLength="100" Width="230px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductCategory"
                                ErrorMessage="Required" ValidationGroup="Group1"></asp:RequiredFieldValidator></td>                        
                    </tr>                                                     
                    <tr>
                        <td align="left" colspan="4" style="height: 19px; text-align: center;">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add"
                                ValidationGroup="Group1" Width="57px" />
                            </td>
                    </tr>
                </table>
</td></tr>
        <tr>
            <td align="left">
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" BorderColor="Silver"
                    BorderStyle="Solid" BorderWidth="1pt" CellPadding="8" CellSpacing="0" EmptyDataText="No Categories configured Yet."
                    ForeColor="#333333" GridLines="None" Font-Size="Medium" OnRowCommand="GridView1_RowCommand" >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" 
                                    CommandArgument='<%# Bind("PID") %>'
                                    Text="View/Edit" CommandName="EditRow"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="Medium" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle ForeColor="#333333" Font-Size="Medium" />
                    <EditRowStyle BackColor="#999999" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#284775" />
                </asp:GridView>
                </td>
        </tr>  
    </table>
</asp:Content>

