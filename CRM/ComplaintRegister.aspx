<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ComplaintRegister.aspx.cs" Inherits="ComplaintRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="JQueryTab/jquery-ui.css" rel="stylesheet">
    <script src="JQueryTab/jquery-1.10.2.js"></script>
    <script src="JQueryTab/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="jQuerydatetime/jquery.datetimepicker.css" />
    <script src="jQuerydatetime/jquery.datetimepicker.js"></script>
    <style type="text/css">
        .custom-date-style
        {
            background-color: red !important;
        }
    </style>
    <script>
        $(function () {
            $("#tabs-1").tabs();
        });
    </script>
    <style>
        #tabs-1
        {
            font-size: 15px;
        }
        #tabs-2
        {
            font-size: 13px;
        }
        #tabs-3
        {
            font-size: 13px;
        }
        #tabs-4
        {
            font-size: 13px;
        }
        #tabs-5
        {
            font-size: 13px;
        }
        #tabs-6
        {
            font-size: 13px;
        }
        #tabs-7
        {
            font-size: 13px;
        }
        .ui-widget-header
        {
            background: #FF6347;
            border: 1px solid #FF0000;
            color: #FFFFFF;
            font-weight: bold;
            font-family: Calibri;
        }
        
        .messagePos
        {
            position: absolute;
            top: 370px;
            left: 200px;
            width: 700px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="alert-box success" id="divSuccess" runat="server">
    </div>
    <div class="alert-box warning" id="divAlert" runat="server">
    </div>
    <div class="alert-box notice" id="divNotice" runat="server">
    </div>
    <div class="alert-box error" id="diverror" runat="server">
    </div>
    <div id="tabs-1">
        <ul>
            <li><a href="#tabs-2">Create/View Request</a></li>
            <li><a href="#tabs-3">Corrective Actions</a></li>
            <li><a href="#tabs-4">Documents</a></li>
            <li><a href="#tabs-5">Feedback</a></li>
            <li><a href="#tabs-6">Knowledge Management</a></li>
            <li><a href="#tabs-7">Dispute Resolution Form</a></li>
        </ul>
        <div id="tabs-2">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="pure-form pure-form-stacked">
                            <div class="pure-g">
                                <asp:Label ID="lblHeader1" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Header Info"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                    Complaint ID
                                    <br />
                                    <asp:Label ID="lblcomplaintID" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                    Created Date
                                    <br />
                                    <asp:Label ID="lblCreatedOn" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                    Current Status
                                    <br />
                                    <asp:Label ID="lblCurrentStatus" runat="server" Text="NA"></asp:Label>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                    Expected Closure Date
                                    <br />
                                    <asp:Label ID="lblExpClosure" runat="server" Text="NA"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="pure-form pure-form-stacked">
                                <div class="pure-g">
                                    <asp:Label ID="lblCustomerDetails" class="pure-u-1 pure-u-md-1-1" Font-Bold="true"
                                        Text="Customer Details" runat="server"></asp:Label>
                                    <hr width="100%; height:1px" />
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Complainant Name<br />
                                        <asp:TextBox ID="txtComplainantName" runat="server" Width="280px" MaxLength="45"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvUserID1" runat="server" ControlToValidate="txtComplainantName"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Customer Telephone<br />
                                        <asp:TextBox ID="txtCustomerTelephone" runat="server" Width="280px" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCustomerTelephone"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Customer Mobile Number<br />
                                        <asp:TextBox ID="txtCustomerMobile" runat="server" Width="280px" MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCustomerMobile"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Complainant Place<br />
                                        <asp:TextBox ID="txtComplaintPlace" runat="server" Width="280px" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtComplaintPlace"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Area<br />
                                        <asp:DropDownList ID="ddlArea" runat="server" Width="280px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="0" runat="server"
                                            ControlToValidate="ddlArea" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Complainant Type<br />
                                        <asp:DropDownList ID="cstType" runat="server" Width="280px">
                                            <asp:ListItem Text="Choose One" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                            <asp:ListItem Text="Retailer" Value="Retailer"></asp:ListItem>
                                            <asp:ListItem Text="Depo" Value="Depo"></asp:ListItem>
                                            <asp:ListItem Text="Distributor" Value="Distributor"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" InitialValue="0" runat="server"
                                            ControlToValidate="cstType" ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Distributor Name<br />
                                        <asp:TextBox ID="txtDistributor" runat="server" Width="280px" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtDistributor"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Customer Address<br />
                                        <asp:TextBox ID="txtCustomerAddress" runat="server" Width="450px" TextMode="MultiLine"
                                            MaxLength="250"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCustomerAddress"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="pure-form pure-form-stacked">
                                <div class="pure-g">
                                    <asp:Label ID="lblComplaintDetails" class="pure-u-1 pure-u-md-1-1" Font-Bold="true"
                                        Text="Complaint Details" runat="server"></asp:Label>
                                    <hr width="100%; height:1px" />
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Nature of Complaint<br />
                                        <asp:DropDownList ID="ddNatureOfComplaint" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" InitialValue="0" runat="server"
                                            ControlToValidate="ddNatureOfComplaint" ErrorMessage="Required" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Type of complaint<br />
                                        <asp:DropDownList ID="ddTypeOfComplaint" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" InitialValue="0" runat="server"
                                            ControlToValidate="ddTypeOfComplaint" ErrorMessage="Required" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Complaint Received Date & time <span style="font-size: xx-small; color: Maroon">YYYY/MM/DD
                                            HH:MI(24h)</span><br />
                                        <asp:TextBox ID="txtCallTakenTime" runat="server" Width="253px" MaxLength="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCallTakenTime"
                                            InitialValue="____/__/__ __:__" ErrorMessage="Required" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Product Category<br />
                                        <asp:DropDownList ID="ddCategory" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" InitialValue="0" runat="server"
                                            ControlToValidate="ddCategory" ErrorMessage="Required" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Product Description<br />
                                        <asp:TextBox ID="txtProductDetails" runat="server" Width="253px" MaxLength="255"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtProductDetails"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Quantity
                                        <asp:TextBox ID="txtQuantity" runat="server" Width="150px" MaxLength="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQuantity"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Call Taken by (Name)<br />
                                        <asp:TextBox ID="txtContactedByName" runat="server" Width="253px" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtContactedByName"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Invoice NO/Batch NO<br />
                                        <asp:TextBox ID="txtBatchNO" runat="server" Width="101px" MaxLength="30"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBatchNO"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-1">
                                        Complaint Description<br />
                                        <asp:TextBox ID="txtCompDetails" runat="server" Width="750px" Columns="8" TextMode="MultiLine"
                                            MaxLength="800"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtCompDetails"
                                            ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div style="position: absolute;">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="1"
                                    AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <img border="0" src="Images/progress1.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div class="pure-u-1 pure-u-md-1-2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Save New Complaint" OnClick="btnSubmit_Click"
                                    class="pure-button pure-button-primary" ValidationGroup="Submit" />
                                <asp:Button ID="btnUpdate" runat="server" Text="Update Complaint" OnClick="btnUpdate_Click"
                                    class="pure-button pure-button-primary" Enabled="false" ValidationGroup="Submit" />
                            </div>
                            <div class="pure-u-1 pure-u-md-1-2" style="position: absolute; top: 700px; left: 300px;">
                                <asp:Label ID="lblMessageText1" runat="server" Width="500px"></asp:Label>
                            </div>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="tabs-3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="pure-form pure-form-stacked">
                            <asp:Label ID="Label6" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Corrective Actions Taken By Team"
                                runat="server"></asp:Label>
                            <hr width="100%; height:1px" />
                            <br />
                            <div class="pure-g">
                                <div style="position: absolute;">
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server" DynamicLayout="true" DisplayAfter="1"
                                        AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <img border="0" src="Images/progress1.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Company Unit
                                    <asp:DropDownList ID="ddComplaintForwardedTO" runat="server" OnSelectedIndexChanged="ddComplaintForwardedTO_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator30" InitialValue="0" runat="server"
                                        ControlToValidate="ddComplaintForwardedTO" ErrorMessage="Required" SetFocusOnError="True"
                                        ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div runat="server" class="pure-u-1 pure-u-md-1-3" id="divSubDivision" visible="false">
                                    Sub Unit
                                    <asp:DropDownList ID="ddSubDivision1" runat="server" Visible="false" Width="200px">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="CPS" Value="CPS"></asp:ListItem>
                                        <asp:ListItem Text="Bulk" Value="Bulk"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddSubDivision2" runat="server" Visible="false" Width="200px">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Cake Division" Value="Cake Division"></asp:ListItem>
                                        <asp:ListItem Text="Toast Division" Value="Toast Division"></asp:ListItem>
                                        <asp:ListItem Text="STAPLES Division" Value="STAPLES Division"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator30" InitialValue="0" runat="server"
                                        ControlToValidate="ddSubDivision" ErrorMessage="Required" SetFocusOnError="True"
                                        ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Expected Closure Date <span style="font-size: xx-small; color: Maroon">YYYY/MM/DD HH:MI(24h)</span>
                                    <asp:TextBox ID="txtExpectedClosureDate" runat="server" Width="200px" MaxLength="16"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtExpectedClosureDate"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Actual Closure Date & Time <span style="font-size: xx-small; color: Maroon">YYYY/MM/DD
                                        HH:MI(24h)</span>
                                    <asp:TextBox ID="txtActualClosureDate" runat="server" Width="200px" MaxLength="16"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtActualClosureDate"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Customer Contacted After Review <span style="font-size: xx-small; color: Maroon">YYYY/MM/DD
                                        HH:MI(24h)</span>
                                    <asp:TextBox ID="txtContactedONAfterReview" runat="server" Width="200px" MaxLength="16"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtContactedONAfterReview"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Sample Recieved Date & Time <span style="font-size: xx-small; color: Maroon">YYYY/MM/DD
                                        HH:MI(24h)</span>
                                    <asp:TextBox ID="txtSampleReceivedTime" runat="server" Width="200px" MaxLength="16"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtSampleReceivedTime"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Total Cost
                                    <asp:TextBox ID="txtCost" runat="server" Width="200px" MaxLength="8"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtCost"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <asp:Label ID="lblclosureNotes" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Closure Details"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By CRM
                                    <asp:TextBox ID="txtActionUpdatedbyCRM" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtActionUpdatedbyCRM"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By QA
                                    <asp:TextBox ID="txtActionUpdatedbyQA" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtActionUpdatedbyQA"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By Unit Head
                                    <asp:TextBox ID="txtActionUpdatedbyUH" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtActionUpdatedbyUH"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By Production
                                    <asp:TextBox ID="txtActionUpdatedbyProd" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtActionUpdatedbyProd"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By Materials
                                    <asp:TextBox ID="txtActionUpdatedbyMat" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtActionUpdatedbyMat"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By Despatch
                                    <asp:TextBox ID="txtActionUpdatedbyDespatch" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtActionUpdatedbyDespatch"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By Maintenance
                                    <asp:TextBox ID="txtActionUpdatedbyMaintenance" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtActionUpdatedbyMat"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action Updated By Purchase
                                    <asp:TextBox ID="txtActionUpdatedbyPurchase" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtActionUpdatedbyDespatch"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Action Updated By NPD
                                    <asp:TextBox ID="txtActionUpdatedbyNPD" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtActionUpdatedbyDespatch"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Outcome Of Complaint (To be filled by Unit Head/QA/CRM)
                                    <asp:TextBox ID="txtComplaintOutCome" runat="server" Width="800px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtComplaintOutCome"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Forward"></asp:RequiredFieldValidator>--%>
                                </div>
                                <hr width="100%; height:1px" />
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <%--<asp:Button ID="Button1" runat="server" Text="Test Save" OnClick="btnSave1_Click"
                                        class="pure-button pure-button-primary" />--%>
                                    <span style="font-size: small; color: Maroon; width: 900px;">
                                        <asp:Label runat="server" ID="lbl1" Width="900px" Text="Click on 'SAVE DATA' button to save details. You may have to 'FORWARD' the details to next level inorder to complete the CRM process."></asp:Label>
                                    </span>
                                </div>
                                <div style="position: absolute;">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="1"
                                        AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <img border="0" src="Images/progress1.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div style="text-align: left" class="pure-u-1 pure-u-md-1-1">
                                    <asp:Button ID="btnSave" runat="server" Text="Save Data" OnClick="btnSave_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <asp:Button ID="btnForwardToQA" runat="server" Text="Forward to QA" OnClick="btnForwardToQA_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <asp:Button ID="btnForwardToUnitHead" runat="server" Text="Forward to Unit Head"
                                        OnClick="btnForwardToUnitHead_Click" class="pure-button pure-button-primary"
                                        ValidationGroup="Forward" />
                                    <asp:Button ID="btnForwardToLegal" runat="server" Text="Forward to Legal" OnClick="btnForwardToLegal_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <asp:Button ID="btnForwardToCRM" runat="server" Text="Forward to CRM" OnClick="btnForwardToCRM_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <br />
                                </div>
                                <div style="text-align: left" class="pure-u-1 pure-u-md-1-1">
                                    <hr width="100%; height:1px" />
                                </div>
                                <div style="text-align: left" class="pure-u-1 pure-u-md-1-1">
                                    <asp:Button ID="btnForwardToProduction" runat="server" Text="Forward to Production"
                                        OnClick="btnForwardToProduction_Click" class="pure-button pure-button-primary"
                                        ValidationGroup="Forward" />
                                    <asp:Button ID="btnForwardToMaterials" runat="server" Text="Forward to Materials"
                                        OnClick="btnForwardToMaterials_Click" class="pure-button pure-button-primary"
                                        ValidationGroup="Forward" />
                                    <asp:Button ID="btnForwardToDespatch" runat="server" Text="Forward to Despatch" OnClick="btnForwardToDespatch_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <asp:Button ID="btnMaintenance" runat="server" Text="Forward to Maintenance" OnClick="btnForwardToMaintenance_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <asp:Button ID="btnPurchase" runat="server" Text="Forward to Purchase" OnClick="btnForwardToPurchase_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <asp:Button ID="btnNPD" runat="server" Text="Forward to NPD" OnClick="btnForwardToNPD_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                    <br />
                                </div>
                                <div style="text-align: left" class="pure-u-1 pure-u-md-1-1">
                                    <hr width="100%; height:1px" />
                                </div>
                                <div>
                                    <span style="font-size: small; color: Maroon; width: 900px;">
                                        <asp:Label ID="Label8" class="pure-u-1" Width="900px" Text="CRM can do the 'FINAL CLOSURE' once the corrective actions are done. Feedback form is mandatory for all 'Major' Complaints."
                                            runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div style="text-align: left" class="pure-u-1 pure-u-md-1-4">
                                    <asp:Button ID="btnComplaintClosure" runat="server" Text="Final Closure" OnClick="btnComplaintClosure_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="Forward" />
                                </div>
                                 <div style="text-align: left" class="pure-u-1 pure-u-md-1-4">
                                    <asp:Label ID="lblMessageText2" runat="server"></asp:Label>
                                </div>
                                <div style="text-align: left" class="pure-u-1 pure-u-md-1-4">
                                </div>
                                  <div style="text-align: left" class="pure-u-1 pure-u-md-1-4">
                                </div>
                                <br />
                            </div>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- 
         ********************************************************************************************************************************
         Documents
         ********************************************************************************************************************************         
         -->
        <div id="tabs-4">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="pure-form pure-form-stacked">
                            <div class="pure-g">
                                <asp:Label ID="Label1" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Upload/View Documents related to this complaint"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <br />
                                <asp:Label ID="Label3" class="pure-u-1 pure-u-md-1-1" Text="If any documents, please upload below"
                                    runat="server"></asp:Label>
                                <br />
                                <br />
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Attach a document (Max Size: 1 MB/ Each File)<br />
                                    <asp:FileUpload runat="server" ID="fileDocs" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="fileDocs"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Documents"></asp:RequiredFieldValidator>
                                    <br />
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Document Type<br />
                                    <asp:DropDownList ID="ddlDocType" runat="server" CssClass="pure-input-1-3" Width="250px">
                                        <asp:ListItem Text="Choose One" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Customer Email" Value="Customer Email"></asp:ListItem>
                                        <asp:ListItem Text="Files Sent by Complainant" Value="Files Sent by Complainant"></asp:ListItem>
                                        <asp:ListItem Text="CRM Team Documents" Value="CRM Team Documents"></asp:ListItem>
                                        <asp:ListItem Text="QA Documents" Value="QA Documents"></asp:ListItem>
                                        <asp:ListItem Text="Unit Head Documents" Value="Unit Head Documents"></asp:ListItem>
                                        <asp:ListItem Text="Production team documents" Value="Production team documents"></asp:ListItem>
                                        <asp:ListItem Text="Materials Team Documents" Value="Materials Team Documents"></asp:ListItem>
                                        <asp:ListItem Text="Despatch Team Documents" Value="Despatch Team Documents"></asp:ListItem>
                                        <asp:ListItem Text="Legal Documents" Value="Legal Documents"></asp:ListItem>
                                        <asp:ListItem Text="Other Documents" Value="Other Documents"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" InitialValue="0" runat="server"
                                        ControlToValidate="ddlDocType" ErrorMessage="Required" SetFocusOnError="True"
                                        ValidationGroup="Documents"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Document Details<br />
                                    <asp:TextBox ID="txtDocDetails" runat="server" Width="400px" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtDocDetails"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Documents"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <asp:Button ID="btnSaveDocs" runat="server" Text="Upload" class="pure-button pure-button-primary"
                                        ValidationGroup="Documents" OnClick="btnSaveDocs_Click" />
                                </div>
                                <br />
                                <br />
                                <asp:Label ID="Label4" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Download Documents"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <div class="pure-u-1">
                                    <asp:DataGrid ID="dgDocuments" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        DataKeyField="ID" Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None"
                                        OnItemCommand="dgDocuments_ItemCommand" Width="900px">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingItemStyle BackColor="White" Font-Size="8pt" ForeColor="#284775" />
                                        <ItemStyle BackColor="#F7F6F3" Font-Size="8pt" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Attached Document(s)">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDoc" runat="server" CausesValidation="false" CommandName="Click"
                                                        Text='<%# DataBinder.Eval(Container.DataItem,"File_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Document_Type" HeaderText="Document Type"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Document_Details" HeaderText="Document Details"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Uploaded_By" HeaderText="Uploaded By"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="RoleName" HeaderText="Uploaded Role Name"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Uploaded_On" HeaderText="Uploaded Date"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" DynamicLayout="true" DisplayAfter="1"
                                AssociatedUpdatePanelID="UpdatePanel3">
                                <ProgressTemplate>
                                    <img border="0" src="Images/progress1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </fieldset>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveDocs" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div id="tabs-5">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="pure-form pure-form-stacked">
                            <div class="pure-g">
                                <asp:Label ID="lblFeedback" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Customer Feedback"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <br />
                                <asp:Label ID="Label5" class="pure-u-1 pure-u-md-1-1" Text="This is mandatory to fill before the final closure of a complaint"
                                    runat="server"></asp:Label>
                                <br />
                                <br />
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Satisfied with the&nbsp; Complaint handling
                                    <asp:DropDownList ID="ddFeedbackSatisfaction" runat="server">
                                        <asp:ListItem Text="Excellent" Value="Excellent"></asp:ListItem>
                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                        <asp:ListItem Text="Not Satisfactory" Value="Not Satisfactory"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" InitialValue="0" runat="server"
                                        ControlToValidate="ddFeedbackSatisfaction" ErrorMessage="Required" SetFocusOnError="True"
                                        ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    If not please give the details
                                    <asp:TextBox ID="txtComplaintHandlingComment" runat="server" Width="351px" MaxLength="150"></asp:TextBox>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Behavior of our employees towards solving your complaint
                                    <asp:DropDownList ID="ddEmployeeBehavior" runat="server">
                                        <asp:ListItem Text="Excellent" Value="Excellent"></asp:ListItem>
                                        <asp:ListItem Text="Good" Value="Good"></asp:ListItem>
                                        <asp:ListItem Text="Satisfactory" Value="Satisfactory"></asp:ListItem>
                                        <asp:ListItem Text="Not Satisfactory" Value="Not Satisfactory"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" InitialValue="0" runat="server"
                                        ControlToValidate="ddEmployeeBehavior" ErrorMessage="Required" SetFocusOnError="True"
                                        ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Timeliness of response and action on your complaint
                                    <asp:TextBox ID="txtComplaintTimelinessofResp" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtComplaintTimelinessofResp"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Whether ELITE Products meet your requirement
                                    <br />
                                    <asp:RadioButtonList runat="server" ID="RBRequirementMet" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="RBRequirementMet"
                                        InitialValue="" ErrorMessage="Required" ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    If NO state your requirement
                                    <asp:TextBox ID="txtComplaintReqNotMetComment" runat="server" Width="451px" MaxLength="150"></asp:TextBox>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Remarks/Suggestions/Improvements
                                    <asp:TextBox ID="txtFeedback" runat="server" Height="80px" TextMode="MultiLine" Width="451px"
                                        MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFeedback"
                                        ErrorMessage="Please enter the feedback" SetFocusOnError="True" ValidationGroup="Feedback"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    <asp:Button ID="btnFeedback" runat="server" OnClick="btnFeedback_Click" Text="Submit Feedback"
                                        class="pure-button pure-button-primary" ValidationGroup="Feedback" />
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    <asp:Label ID="lblMessageText3" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" DynamicLayout="true" DisplayAfter="1"
                                AssociatedUpdatePanelID="UpdatePanel4">
                                <ProgressTemplate>
                                    <img border="0" src="Images/progress1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- 
        ************************************************************
        KB
        *****************************************************************
        -->
        <div id="tabs-6">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="pure-form pure-form-stacked">
                            <div class="pure-g">
                                <asp:Label ID="Labelkb1" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Knowledge Management"
                                    runat="server"></asp:Label><asp:Label ID="Labelkb3" class="pure-u-1 pure-u-md-1-1"
                                        Text="If you want to share your knowledge/experiance regarding this complaint, enter below"
                                        runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <div class="pure-g" style="background-color: #FFFFFF; border: 1px; border-color: #CCCCCC;">
                                    <br />
                                    <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                        Complaint ID
                                        <br />
                                        <asp:Label ID="lblKBcomplaintID" runat="server" Text="NA"></asp:Label>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                        Unit
                                        <br />
                                        <asp:Label ID="lblKBUnit" runat="server" Text="NA"></asp:Label>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                        Product Item
                                        <br />
                                        <asp:Label ID="lblKBProdItem" runat="server" Text="NA"></asp:Label>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-4" style="font-weight: bold; color: maroon">
                                        Complaint Type
                                        <br />
                                        <asp:Label ID="lblKBCompType" runat="server" Text="NA"></asp:Label>
                                    </div>
                                    <br />
                                    <br />
                                </div>
                                <br />
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Complaint Description As per CRM<br />
                                    <asp:TextBox ID="txtKBDetails" runat="server" Width="450px" TextMode="MultiLine"
                                        Height="70px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB5" runat="server" ControlToValidate="txtKBDetails"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Root Cause Analysis<br />
                                    <asp:TextBox ID="txtRootCauseAnalysis" runat="server" Width="450px" TextMode="MultiLine"
                                        Height="70px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB4" runat="server" ControlToValidate="txtRootCauseAnalysis"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Corrective Action Taken<br />
                                    <asp:TextBox ID="txtKBSolution" runat="server" Width="650px" TextMode="MultiLine"
                                        Height="60px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB6" runat="server" ControlToValidate="txtKBSolution"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3 messagePos">
                                    <asp:Label ID="lblMessageText5" runat="server"></asp:Label>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <asp:Button ID="btnSaveKB" runat="server" Text="Save" class="pure-button pure-button-primary"
                                        ValidationGroup="KB" OnClick="btnSaveKB_Click" />
                                </div>
                                <br />
                                <asp:Label ID="LabelKB4" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="View Knowledge Management"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <div class="pure-u-1">
                                    <asp:DataGrid ID="dgKB" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        DataKeyField="ID" Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None"
                                        OnItemCommand="dgKB_ItemCommand" Width="900px">
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <AlternatingItemStyle BackColor="White" Font-Size="8pt" ForeColor="#284775" />
                                        <ItemStyle BackColor="#F7F6F3" Font-Size="8pt" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ProductItem" HeaderText="Product Item"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ComplaintNature" HeaderText="Complaint Type"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="KB_Description" HeaderText="Description"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="RootCauseAnalysis" HeaderText="Root Cause Analysis">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="KB_Solution" HeaderText="Solution"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="createdBy" HeaderText="Created BY"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="createdOn" HeaderText="Created On"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="Click"
                                                        Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" DynamicLayout="true" DisplayAfter="1"
                                AssociatedUpdatePanelID="UpdatePanel5">
                                <ProgressTemplate>
                                    <img border="0" src="Images/progress1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- 
        ************************************************************
        Legal dispute
        *****************************************************************
        -->
        <div id="tabs-7">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <div class="pure-form pure-form-stacked">
                            <div class="pure-g">
                                <asp:Label ID="Labellg1" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Complaint Dispute Resolution Form"
                                    runat="server"></asp:Label>
                                <asp:Label ID="Labellg3" class="pure-u-1 pure-u-md-1-1" Text="(To be filled by legal team)"
                                    runat="server"></asp:Label>
                                <hr width="100%; height:1px" />
                                <br />
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Dispute Points<br />
                                    <asp:TextBox ID="txtlgDisputePoints" runat="server" Width="350px" TextMode="MultiLine"
                                        Height="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlg1" runat="server" ControlToValidate="txtlgDisputePoints"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="lg"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Points In Checklist<br />
                                    <asp:TextBox ID="txtlgPointsInChecklist" runat="server" Width="350px" TextMode="MultiLine"
                                        Height="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlg4" runat="server" ControlToValidate="txtlgPointsInChecklist"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="lg"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Action InitiatedThrough<br />
                                    <asp:DropDownList ID="ddllgActionInitiatedThrough" runat="server">
                                        <asp:ListItem Text="Mediator" Value="Mediator"></asp:ListItem>
                                        <asp:ListItem Text="Arbitrator" Value="Arbitrator"></asp:ListItem>
                                        <asp:ListItem Text="Court" Value="Court"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlg2" runat="server" ControlToValidate="ddllgActionInitiatedThrough"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="lg" InitialValue=""></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
                                    Dispute Updated On<br />
                                    <asp:TextBox ID="txtlgDisputeCompletedOn" runat="server" Width="200px" MaxLength="18"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorlg3" runat="server" ControlToValidate="txtlgDisputeCompletedOn"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="lg"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Action Updated By Legal
                                    <asp:TextBox ID="txtActionUpdatedbyLegal" runat="server" Width="450px" Height="80px"
                                        TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtActionUpdatedbyLegal"
                                        ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="lg"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <asp:Label ID="lblMessageLG" runat="server"></asp:Label>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <asp:Button ID="btnSavelg" runat="server" Text="Save" class="pure-button pure-button-primary"
                                        ValidationGroup="lg" OnClick="btnSavelg_Click" />
                                    <asp:Button ID="btnForwardedByLegal" runat="server" Text="Forward to Unit Head" OnClick="btnForwardedByLegal_Click"
                                        class="pure-button pure-button-primary" ValidationGroup="lg" />
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" DynamicLayout="true" DisplayAfter="1"
                                AssociatedUpdatePanelID="UpdatePanel6">
                                <ProgressTemplate>
                                    <img border="0" src="Images/progress1.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script>

        function pageLoad() {
            //            $(function () {
            //                $("#btnClick").click(function () {
            //                    alert("Alert: Hello from jQuery!");
            //                });
            //            });

            $('#ctl00_ContentPlaceHolder1_txtCallTakenTime').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });
            // $('#ctl00_ContentPlaceHolder1_txtCallTakenTime').datetimepicker({ value: '2015/01/15 05:03', step: 10 });

            $('#ctl00_ContentPlaceHolder1_txtExpectedClosureDate').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });
            //$('#ctl00_ContentPlaceHolder1_txtExpectedClosureDate').datetimepicker({ value: '2015/01/15 05:03', step: 10 });

            $('#ctl00_ContentPlaceHolder1_txtActualClosureDate').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });
            //$('#ctl00_ContentPlaceHolder1_txtActualClosureDate').datetimepicker({ value: '2015/01/15 05:03', step: 10 });

            $('#ctl00_ContentPlaceHolder1_txtSampleReceivedTime').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });
            //$('#ctl00_ContentPlaceHolder1_txtSampleReceivedTime').datetimepicker({ value: '2015/01/15 05:03', step: 10 });

            $('#ctl00_ContentPlaceHolder1_txtContactedONAfterReview').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });

            $('#ctl00_ContentPlaceHolder1_txtlgDisputeCompletedOn').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });

            //$('#ctl00_ContentPlaceHolder1_txtContactedONAfterReview').datetimepicker({ value: '2015/01/15 05:03', step: 10 });
        } 
    </script>
</asp:Content>
