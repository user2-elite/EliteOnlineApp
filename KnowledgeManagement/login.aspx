<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="login.aspx.cs" EnableViewState="true" Inherits="login1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: left; width: 100%">
        <h2>
            Knowledge Management Login</h2>
            <br />
        <div id="diverror" runat="server"></div>       
        <br />
    <div class="login-box login" >
                    <div class="pure-g">
                        <div class="pure-u-1 pure-u-md-1-1">
                            User ID<br /><asp:TextBox ID="txtUserID" runat="server" Width="180px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvUserID1" runat="server" ControlToValidate="txtUserID"
                                ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        </div>
                        <br />
                        <div class="pure-u-1 pure-u-md-1-1">
                            Password
                            <br />
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="180px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        </div>
                        <div class="pure-u-1 pure-u-md-1-1">
                            <asp:Button ID="ibtnLogin" runat="server" class="pure-button pure-button-primary"
                                OnClick="ibtnLogin_Click" ValidationGroup="Login" Text="Login" Width="80px" Height="35px" />
                        </div>
                    </div>
                </div>
</asp:Content>
