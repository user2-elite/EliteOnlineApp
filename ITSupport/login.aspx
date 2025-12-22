<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" EnableViewState="true"
    Inherits="login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script language="javascript">
function OpenPage() {
    window.open('policy.aspx', 'mywin', 'width=750,height=600,resizable=no,scrollbars=no,menu=no,location=no,status=no,left=1,top=1');
}
</script>
    <table cellspacing="0" cellpadding="0" width="100%" align="center">
        <tbody>
            <tr>
                <td class="title" valign="top">
                    <h1>
                        Login</h1>
                </td>
            </tr>
            <tr>
            <td><asp:Label ID="lblMessageFor" Text="" runat="server"></asp:Label></td>
            </tr>

            <asp:Panel ID="pnlUserLogin" runat="server">
                <tr>
                    <td align="left" valign="top">
                        <asp:Panel ID="pnlMessage" runat="server">

                            <h3 style="color:Maroon">Please enter your User ID and Password
                                    to login below.</h3>
                        </asp:Panel>
                    </td>
                </tr>
                
                <tr id="trLogin" runat="server">
                    <td align="center" valign="top">
                        <fieldset style="width: 350px;">
                            <legend style="font-weight:bold">HelpDesk Login</legend>
                            <table width="100%" cellpadding="5" cellspacing="3">
                                <tr>
                                    <td colspan="2" align="left" valign="top">
                                        <br />
                                        <span style="color: #FF0000">
                                            <asp:Label ID="lblLoginError" runat="server"></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="25%" align="left" valign="top">
                                        User ID <span style="color: #FF0000">*</span>
                                    </td>
                                    <td align="left" valign="top" width="75%">
                                        <asp:TextBox ID="txtUserID" runat="server" Width="180px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserID1" runat="server" ControlToValidate="txtUserID"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top">
                                        Password <span style="color: #FF0000">*</span>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="180px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                            <br />
                                            First time users, <a href="javascript:OpenPage()">click here to Read & Agree IT Security Policy</a>
                                    </td>
                                </tr>                               
                                <tr>
                                <td></td>
                                    <td align="left">
                                        <asp:ImageButton ID="ibtnLogin" runat="server" ImageUrl="images/b1.gif" OnClick="ibtnLogin_Click"
                                            ValidationGroup="Login" />
                                        &nbsp;<asp:ImageButton ID="ibtnCancel" runat="server" CausesValidation="false" ImageUrl="images/b2.gif"
                                            OnClick="ibtnCancel_Click" />
                                       
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </asp:Panel>         
        </tbody>
    </table>
   
</asp:Content>
