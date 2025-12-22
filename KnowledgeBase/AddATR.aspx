<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddATR.aspx.cs" Inherits="AddATR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="JQuery1.11.4/jquery-ui.css">
    <script src="JQuery1.11.4/jquery-1.10.2.js"></script>
    <script src="JQuery1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="jQuerydatetime/jquery.datetimepicker.css" />
    <script src="jQuerydatetime/jquery.datetimepicker.js"></script>
    <style type="text/css">
        .custom-date-style
        {
            background-color: red !important;
        }
    </style>
    <style type="text/css">
        .KBAdd
        {
            border: 2px solid #819BCB;
            -webkit-box-shadow: #A5B8DA 0px 21px 0px inset;
            -moz-box-shadow: #A5B8DA 0px 21px 0px inset;
            box-shadow: #A5B8DA 0px 21px 0px inset;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            padding: 10px 10px 10px 10px;
            text-decoration: none;
            display: inline-block;
            text-shadow: -1px -1px 0 rgba(0,0,0,0.3);
            font-weight: bold;
            color: #FFFFFF;
            background-color: #a5b8da;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#a5b8da), to(#7089b3));
            background-image: -webkit-linear-gradient(top, #a5b8da, #7089b3);
            background-image: -moz-linear-gradient(top, #a5b8da, #7089b3);
            background-image: -ms-linear-gradient(top, #a5b8da, #7089b3);
            background-image: -o-linear-gradient(top, #a5b8da, #7089b3);
            background-image: linear-gradient(to bottom, #a5b8da, #7089b3);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#a5b8da, endColorstr=#7089b3);
        }
        
        .KBAdd:hover
        {
            border: 2px solid #819BCB;
            background-color: #819bcb;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#819bcb), to(#536f9d));
            background-image: -webkit-linear-gradient(top, #819bcb, #536f9d);
            background-image: -moz-linear-gradient(top, #819bcb, #536f9d);
            background-image: -ms-linear-gradient(top, #819bcb, #536f9d);
            background-image: -o-linear-gradient(top, #819bcb, #536f9d);
            background-image: linear-gradient(to bottom, #819bcb, #536f9d);
            filter: progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#819bcb, endColorstr=#536f9d);
        }
        
        .messagePos
        {
            position: absolute;
            top: 350px;
            left: 200px;
            width: 700px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset>
                <asp:Panel runat="server" ID="pnlAddNew" Visible="false">
                    <div style="text-align: left; color: maroon; font-weight: bold; position: absolute;
                        top: 25px; right: 80px;">
                        <a href="AddATR.aspx?edit=1" class="KBAdd" runat="server" id="lnkViewDetails"><b>View/Edit
                            ATR</b></a>
                    </div>
                    <div style="border: 1px; background-color: #EEEEEE; width: 100%; padding: 5px;">
                        <div class="pure-form pure-form-stacked" style="border: 1px; width: 100%; margin: 10px;">
                            <h2>
                                Knowledge Management - Add New ATR</h2> 
                            <div class="pure-g">
                                <hr width="100%; height:1px" />
                                <br />
                                <div class="pure-u-1 pure-u-md-1-3">
                                    <asp:RadioButtonList runat="server" ID="rdCategory" TextAlign="Left" RepeatDirection="Horizontal"
                                        CellPadding="5" CellSpacing="5" Font-Bold="true">
                                        <asp:ListItem Text="Customer Complaint" Value="CC"></asp:ListItem>
                                        <asp:ListItem Text="Internal Complaint" Value="IC"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB1" runat="server" ControlToValidate="rdCategory"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                 <div class="pure-u-1 pure-u-md-1-3">
                                  <%--  SerialNumber<br />
                                    <asp:TextBox ID="txtSLNO" runat="server" Width="250px" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSLNO"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                </div>
                                   <div class="pure-u-1 pure-u-md-1-3">&nbsp;
                                   </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    GeneralCategory<br />
                                    <asp:TextBox ID="txtGeneralCategory" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
<%--                                    <asp:DropDownList ID="ddGeneralCategory" runat="server" Width="250px">
                                    </asp:DropDownList>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtGeneralCategory"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Poduction Unit<br />
                                    <asp:DropDownList ID="ddCompanyUnit" runat="server" Width="250px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddCompanyUnit"
                                        ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Product<br />
                                     <asp:TextBox ID="txtProdCategory" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtProdCategory"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    ComplaintDetails
                                    <asp:TextBox ID="txtComplaintDetails" runat="server" Width="580px" TextMode="MultiLine"
                                        MaxLength="250"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtComplaintDetails"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Root Cause
                                    <asp:TextBox ID="txtRootCause" runat="server" Width="580px" TextMode="MultiLine"
                                        MaxLength="500"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRootCause"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    C/A Short Data
                                    <asp:TextBox ID="txtCAShortData" runat="server" Width="250px" TextMode="MultiLine"
                                        MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCAShortData"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    CARNo
                                    <asp:TextBox ID="txtCARNo" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCARNo"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    CAR Details
                                    <asp:TextBox ID="txtCARDetails" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCARDetails"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    ATRNo
                                    <asp:TextBox ID="txtATRNo" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtATRNo"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    ATR Details
                                    <asp:TextBox ID="txtATRDetails" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtATRDetails"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Benefit Achieved
                                    <asp:TextBox ID="txtBenefitAchieved" runat="server" Width="580px" TextMode="MultiLine"
                                        MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBenefitAchieved"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Search Tags (Enter minimum 3 tags. Seperate each tag with a comma( , ).
                                    <asp:TextBox ID="txtSearchTags" runat="server" Width="920px" MaxLength="255"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSearchTags"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Prepared By
                                    <asp:TextBox ID="txtPreparedBy" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPreparedBy"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Prepared Date <span style="font-size: xx-small; color: Maroon">YYYY/MM/DD HH:MI(24h)</span>
                                    <asp:TextBox ID="txtPreparedDate" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPreparedDate"
                                        InitialValue="____/__/__ __:__" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Approved BY
                                    <asp:TextBox ID="txtApprovedBY" runat="server" Width="250px" MaxLength="230"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtApprovedBY"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <br />
                                    <div style="position: absolute; width: 500px; left: 200px;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="1"
                                            AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <img border="0" src="Images/progress1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <div class="alert-box success" id="divSuccess" runat="server">
                                        </div>
                                        <div class="alert-box warning" id="divAlert" runat="server">
                                        </div>
                                        <div class="alert-box notice" id="divNotice" runat="server">
                                        </div>
                                        <div class="alert-box error" id="diverror" runat="server">
                                        </div>
                                    </div>
                                    <hr style="width: 98%" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save Data" class="pure-button pure-button-primary"
                                        OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        class="pure-button pure-button-primary" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlList" Visible="false">
                    <br />
                    <div style="text-align: left; color: maroon; font-weight: bold; position: absolute;
                        top: 25px; right: 80px;">
                        <a href="AddATR.aspx" class="KBAdd"><b>Add New ATR</b></a>
                    </div>
                    <h2>
                        View Knowledge Management - ATR</h2>
                    <hr width="100%; height:1px" />
                    <div class="alert-box success" id="divListSuccess" runat="server">
                    </div>
                    <div class="alert-box error" id="divListError" runat="server">
                    </div>
                    <div class="pure-u-1">
                        <asp:DataGrid ID="dgKB" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None" OnItemCommand="dgKB_ItemCommand"
                            Width="100%">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#999999" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                            <ItemStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="GeneralCategory" HeaderText="GeneralCategory"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ComplaintDetails" HeaderText="ComplaintDetails"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Product" HeaderText="Product"></asp:BoundColumn>
                                <asp:BoundColumn DataField="UnitName" HeaderText="UnitName"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PreparedBy" HeaderText="PreparedBy"></asp:BoundColumn>
                                <asp:BoundColumn DataField="createdOn" HeaderText="created On"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="cmEdit"
                                            Text="View/Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SLNO") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmDelete"
                                            Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"SLNO") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <br />
                </asp:Panel>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>

        function pageLoad() {
            //            $(function () {
            //                $("#btnClick").click(function () {
            //                    alert("Alert: Hello from jQuery!");
            //                });
            //            });

            $('#ctl00_ContentPlaceHolder1_txtPreparedDate').datetimepicker({
                dayOfWeekStart: 1,
                lang: 'en',
                //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
                //startDate: '2010/01/05'
                mask: '9999/19/39 29:59'
            });
            // $('#ctl00_ContentPlaceHolder1_txtCallTakenTime').datetimepicker({ value: '2015/01/15 05:03', step: 10 });


            //$('#ctl00_ContentPlaceHolder1_txtContactedONAfterReview').datetimepicker({ value: '2015/01/15 05:03', step: 10 });
        } 
    </script>
</asp:Content>
