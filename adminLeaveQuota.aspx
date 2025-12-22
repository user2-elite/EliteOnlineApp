<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminLeaveQuota.aspx.cs" Inherits="adminLeaveQuota" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        Leave Quota - Upload</h3>
    <div class="alert-box success" id="divListSuccess" runat="server" style="position: absolute;
        left: -10; top: 1; width: 400px;">
    </div>
    <div class="alert-box error" id="divListError" runat="server" style="position: absolute;
        top: 1px; left: 300px; top: -25; width: 550px;">
    </div>
    <br />
    <div>
        <div style="width: 100%; color: maroon">
            <b><u>Instructions:</u></b> 1) Excel data format : <a href="Gallery/templateLeaveQuota.xls"><b>
                <i>[Template Format]</i></b></a><br />
                Maximum number of records per excel sheet should be 1500.
                <br />
                2) Both Employee Code &  Quota Value should be mandatory in excel sheet. Extra spaces, line gaps, blank lines should be removed before the upload.<br />
            3) Data should be in the first sheet and the excel sheet name should contain the word "quota".<br />
            4) Bulk Upload can be done when new Leave Quota starts. (Note: Previous year quota will be moved to backup table and will not be available in the portal)<br />
            5) Insert New / Quota Correction can be done at any time, however the changes will affect for the active Quota Year records.
        </div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px;
            border: 0px; background-color: #EEEEEE">
            <div class="pure-g" style="width: 100%;">
            <div class="pure-g" style="width: 85%;">
             <div class="pure-u-1 pure-u-md-1-4" style="width:350px">
                    <b>Upload Type</b><br />
                    <asp:RadioButtonList ID="rdUplType" runat="server" RepeatDirection="Horizontal" Width="300px" onselectedindexchanged="rdUplType_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Bulk Upload" Value="bulk"></asp:ListItem>
                        <asp:ListItem Text="Insert New / Quota Correction" Value="single"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="rdUplType"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
             <div class="pure-u-1 pure-u-md-1-4">
                    <b>Quota Year</b><br />
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Width="190px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-4">
                    <b>Quota Type</b><br />
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" Width="150px">
                        <asp:ListItem Text="--Choose--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Casual leave" Value="90"></asp:ListItem>
                        <asp:ListItem Text="Sick leave" Value="91"></asp:ListItem>
                        <asp:ListItem Text="ESI Leve" Value="92"></asp:ListItem>
                        <asp:ListItem Text="Earned Leave" Value="95"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlType"
                        ErrorMessage="Required" SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                </div>
               
                </div>
               <div style="width:100%">
                <div class="pure-u-1 pure-u-md-1-2">
                    <b>Browse Excel Data <i>(.xls)</i></b><br />
                    <asp:FileUpload ID="upldExcelData" runat="server" CssClass="form-control" Width="400px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="upldExcelData"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-2">
                    <br />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click"
                        CssClass="btn btn-success" />                        
                </div>
                </div>
        </div>
        <hr />
    </div>
</asp:Content>
