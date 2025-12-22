<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="admin_AssignRole.aspx.cs" Inherits="admin_AssignRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       
   <table align="left" border="1" bordercolor="#E89F65" width="90%" 
        cellspacing="0" style="background-color:#F0F0F0" cellpadding="5">
        <tr>
            <td bgcolor="#E89F65" align="center" style="height: 22px">
                <span style="color: #ffffff; font-size: 12pt;"><strong>
                Add/Edit System Users</strong></span></td>
        </tr>
        <tr><td width="100%">
         <div class="alert-box success" id="divSuccess" runat="server"></div>
        <div class="alert-box warning" id="divAlert" runat="server"></div>
        <div class="alert-box notice" id="divNotice" runat="server"></div>
        <div class="alert-box error" id="diverror" runat="server"></div>
        <br /><br /><br /><br /><br /><br />
        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label>
        <br />


         <div class="pure-form pure-form-stacked">
                                <div class="pure-g">                                    
                                    <div class="pure-u-1 pure-u-md-1-1">
                                        Full Name<span style="color: #FF0000">*</span><br />
                        <asp:TextBox ID="txtFullName" runat="server" Width="273px" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                            ControlToValidate="txtFullName" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>


                                     <div class="pure-u-1 pure-u-md-1-3">
                                         User ID <span style="color: #FF0000">*</span>
                    <br />
                        <asp:TextBox ID="txtUID" runat="server" Width="273px" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtUID" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>       
                                    </div>

                                      <div class="pure-u-1 pure-u-md-1-3">
                                        Password <span style="color: #FF0000">*</span>
                    <br />
                        <asp:TextBox ID="txtPassword" runat="server" Width="273px" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>   
                                    </div>

                                      <div class="pure-u-1 pure-u-md-1-3">
                                        Email <span style="color: #FF0000">*</span>
                    <br />
                        <asp:TextBox ID="txtEmail" runat="server" Width="273px" MaxLength="100"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="txtEmail" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>  
                                    </div>

                                      <div class="pure-u-1 pure-u-md-1-3">
                                         Phone <span style="color: #FF0000">*</span>
                    <br />
                        <asp:TextBox ID="txtPhone" runat="server" Width="273px" MaxLength="20"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvPhone" runat="server" 
                            ControlToValidate="txtPhone" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>  
                                    </div>

                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Role Name <span style="color: #FF0000">*</span>
                 <br />
                         <asp:DropDownList ID="ddlRole" runat="server" Width="273px">
                         </asp:DropDownList> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="ddlRole" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>    
                                    </div>

                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Company Unit Name<span style="color: #FF0000">*</span>
                 <br />
                         <asp:DropDownList ID="ddlUnit" runat="server" Width="273px">
                         </asp:DropDownList> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="ddlUnit" ErrorMessage="Required" ValidationGroup="Group1"
                            SetFocusOnError="True"  InitialValue="0"></asp:RequiredFieldValidator>   
                                    </div>

                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Active <span style="color: #FF0000">*</span>
                 <br />
                         <asp:DropDownList ID="ddlActive" runat="server" Width="273px" Enabled="false">
                         <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                         <asp:ListItem Text="No" Value="0"></asp:ListItem>
                         </asp:DropDownList> 
                                    </div>

                                     <div class="pure-u-1 pure-u-md-1-1">
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add"
                                ValidationGroup="Group1" Width="57px" />
                                    </div>

                                    </div>
                                    </div>
   
   </td></tr>
        <tr>
            <td align="left">
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" BorderColor="Silver"
                    BorderStyle="Solid" BorderWidth="1pt" CellPadding="8" CellSpacing="0" EmptyDataText="No Users configured Yet."
                    ForeColor="#333333" GridLines="None" Font-Size="Medium" OnRowCommand="GridView1_RowCommand" >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" 
                                    CommandArgument='<%# Bind("ID") %>'
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

