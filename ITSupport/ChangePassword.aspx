<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs"
    Inherits="ChangePassword" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <asp:Panel id="PanelUser" runat="server" visible="false" >
                        <div style="right:3px; position:absolute;">
                        <%--<asp:HyperLink ID="LogRequest" runat="server" CausesValidation="False" NavigateUrl="~/Home.aspx">Create New HelpDesk Request</asp:HyperLink>--%>                        
                        </div>
            </asp:Panel>
    <div>
        <table width="100%" id="one-column-emphasis">
            <tr>
                <td>
                   <h1> Change Password</h1>
                </td>
            </tr>
            <tr>
                <td>
                   <h3><font color="maroon">Default password should be changed at the first time.</font></h1>
                </td>
            </tr>                        
            <tr>
                <td>
                    Old password&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                        ErrorMessage="(required)" ValidationGroup="grp1"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td >
                    New Password &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                        ErrorMessage="(required)" ValidationGroup="grp1"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td >
                    Repeat New Password&nbsp;&nbsp;
                    <asp:TextBox ID="txtRepeatPassword" runat="server" TextMode="Password"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRepeatPassword"
                        ErrorMessage="(required)" ValidationGroup="grp1"></asp:RequiredFieldValidator>
                    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                        ControlToValidate="txtRepeatPassword" ErrorMessage="(Incorrect Password)" ValidationGroup="grp1"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td >                    
                    <h3>
                    <asp:Button ID="btnSubmit" runat="server" Text="Change Password" OnClick="btnSubmit_Click"
                        ValidationGroup="grp1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LogOff_Click">Log off</asp:LinkButton>                                        
                    </h3>
                    
                </td>
            </tr>
            <tr>
                <asp:Label ID="lblError" runat="server"></asp:Label><td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
