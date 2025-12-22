<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="mySalarySlip.aspx.cs" Inherits="mySalarySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        table
        {
            border-collapse: collapse;
            border-color: #CCCCCC;
        }
        
        table, th, td
        {
            border: 1px solid #CCCCCC;
            padding: 8px;
            margin: 1px;
            font-size: 12px;
        }
    </style>
    <h3>
        My Salary Slip</h3>
    <div>
        <div style="width: 100%; color: maroon">
            You may get up to last 3 months salary slip effective from October 2015. 
            If your search is with in the period and not find the result, please get in touch with portal administrator.
        </div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px;
            border: 0px; background-color: #EEEEEE">
            <div class="pure-g" style="width: 100%;">
                <div class="pure-u-1 pure-u-md-1-4">
                    Select Month<br />
                    <asp:DropDownList ID="ddlSalMonth" runat="server" CssClass="form-control" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="ddlSalMonth"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-4">
                    Select Year<br />
                    <asp:DropDownList ID="ddlSalYear" runat="server" CssClass="form-control" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalYear"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>                
                <div class="pure-u-1 pure-u-md-1-4">
                    <br />
                    <asp:Button ID="btnOpen" runat="server" Text="Open Salary Slip" OnClick="btnOpen_Click"
                        CssClass="btn btn-success" />
                </div>
                <div class="pure-u-1 pure-u-md-1-4">
                &nbsp;
                </div>
            </div>
        </div>
        <hr />

        <div class="alert-box success" id="divListSuccess" runat="server" style="width: 400px;">
    </div>
    <div class="alert-box error" id="divListError" runat="server" style="width: 550px;">
    </div>
    <br /><br /><br /><br /><br />    
    </div>
        
</asp:Content>

