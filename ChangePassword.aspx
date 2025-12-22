<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
        border: 0px;">
        <div class="pure-g">
         <div class="pure-u-1 pure-u-md-1-1" >
                <div class="alert-box success" id="divSuccess" runat="server" style="width:300px">
                </div>
                <div class="alert-box error" id="diverror" runat="server" style="width:300px">
                </div>
            </div> 
            <div class="pure-u-1 pure-u-md-1-1">
                <h4>
                    Change Password</h4>
                <h5>
                    <font color="maroon">Default password should be changed at the first time.</font></h5>
            </div>            
            <div class="pure-u-1 pure-u-md-1-1">
                Old password<br />
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                    ErrorMessage="(required)" ValidationGroup="grp1"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-1">
                New Password
                <br />
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                    ErrorMessage="(required)" ValidationGroup="grp1"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-1">
                Repeat New Password<br />
                <asp:TextBox ID="txtRepeatPassword" runat="server" TextMode="Password"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRepeatPassword"
                    ErrorMessage="(required)" ValidationGroup="grp1"></asp:RequiredFieldValidator>
                &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                    ControlToValidate="txtRepeatPassword" ErrorMessage="(Incorrect Password)" ValidationGroup="grp1"></asp:CompareValidator>
            </div>      
            
            <div class="pure-u-1 pure-u-md-1-1">
                <asp:Button ID="btnSubmit" runat="server" Text="Change Password" OnClick="btnSubmit_Click"
                    ValidationGroup="grp1" class="pure-button pure-button-primary" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LogOff_Click"
                    class="pure-button alert-danger">Log off</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
