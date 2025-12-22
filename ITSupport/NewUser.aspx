<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewUser.aspx.cs" Inherits="NewUser" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
               <%-- <table border="0" cellpadding="5" cellspacing="0" width="100%">
                    <tr>
                        <td width="95%" id="CssMenu1">
                            <asp:LinkButton ID="LogRequest" runat="server" CausesValidation="False" OnClick="LogRequest_Click">Create New HelpDesk Request</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LogOff_Click">Log off</asp:LinkButton>
                        </td>
                        <td align="right" style="width: 5%" id="CssMenu2">                            
                        </td>
                    </tr>
                </table>--%>
        <table cellspacing="0" cellpadding="0" width="95%" align="center" id="one-column-emphasis">     
            <tr>
                <td class="title" valign="top" colspan="2">
                   <h1> New User Form </h1></td>
            </tr>
            <tr>
                <td height="148" valign="top" colspan="2">
                   <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblError" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>                              
                <tr>
                    <td align="left">
                        Full name <span style="color: #FF0000">*</span></td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPrifix" runat="server">
                            <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                            <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                            <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>                          
                        </asp:DropDownList>
                        <asp:TextBox ID="txtFullName" runat="server" Width="226px" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                            ControlToValidate="txtFullName" ErrorMessage="Required" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                  <tr>
                    <td align="left">
                        User ID <span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUID" runat="server" Width="273px" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtUID" ErrorMessage="Required" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>                        
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Email <span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEmail" runat="server" Width="273px" MaxLength="100"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="txtEmail" ErrorMessage="Required" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>                        
                    </td>
                </tr>

<tr>
                    <td align="left">
                        EmpCode <span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEmpCode" runat="server" Width="273px" MaxLength="10"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtEmpCode" ErrorMessage="Required" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>                                                
                    </td>
                </tr>   
<tr>
                    <td align="left">
                        Phone <span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPhone" runat="server" Width="273px" MaxLength="20"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvPhone" runat="server" 
                            ControlToValidate="txtPhone" ErrorMessage="Required" 
                            SetFocusOnError="True"></asp:RequiredFieldValidator>                                                
                    </td>
                </tr>   
<tr>
                    <td align="left">
                        System Type <span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlStype" runat="server" Width="273px">
                         <asp:ListItem Text="Nil" Value="Nil"></asp:ListItem>
                         <asp:ListItem Text="LapTop" Value="LapTop"></asp:ListItem>
                         <asp:ListItem Text="DeskTop" Value="DeskTop"></asp:ListItem>
                         </asp:DropDownList> 
                    </td>
                </tr>
<tr>
                    <td align="left">
                        Asset Name<span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                     <asp:DropDownList ID="ddlAssetName" runat="server" Width="273px">
                         </asp:DropDownList> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlAssetName" ErrorMessage="Required" 
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>      
                          <!--<asp:TextBox ID="txtAsset" runat="server" Width="273px" MaxLength="100"></asp:TextBox>                            
                            -->
                    </td>
                </tr>       
                <tr>
                    <td align="left">
                        <span style="color: #FF0000"></span>
                    </td>
                    <td align="left">
                          <asp:CheckBox ID="isMouse" runat="server" Text="Mouse Allotted" />    
                          <asp:CheckBox ID="isExtD" runat="server" Text="Data Card Allotted" />    
                          <asp:CheckBox ID="isDatacard" runat="server" Text="External HardDisk Allotted" />    
                    </td>
                </tr> 
                
                
<tr>
                    <td align="left">
                        Location<span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlLocation" runat="server" Width="273px">
                         </asp:DropDownList> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="ddlLocation" ErrorMessage="Required" 
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>      
                    </td>
                </tr>
                
                <tr>
                    <td align="left">
                        Department<span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlDepartments" runat="server" Width="273px">
                         </asp:DropDownList> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlDepartments" ErrorMessage="Required" 
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>      
                    </td>
                </tr>
                   <tr>
                    <td align="left">
                        Area of Work<span style="color: #FF0000">*</span>
                    </td>
                    <td align="left">
                         <asp:DropDownList ID="ddlArea" runat="server" Width="273px">
                         </asp:DropDownList> 
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlArea" ErrorMessage="Required" 
                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>      
                    </td>
                </tr>
                
                                                                                            
                <tr>
                    <td align="left">
                    </td>
                     <td>
                        <asp:ImageButton ID="ibtnSubmit" runat="server" 
                            ImageUrl="images/b3.gif" onclick="ibtnSubmit_Click" />
                        &nbsp;<%--<asp:ImageButton ID="ibtnCancel" runat="server" CausesValidation="false"
                            ImageUrl="images/b2.gif" onclick="ibtnCancel_Click" />--%>
                    </td>
                </tr>
            </table>
                </td>
            </tr>
     
    </table>
</asp:Content>

