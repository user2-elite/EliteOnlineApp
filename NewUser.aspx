<%@ Page Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="NewUser.aspx.cs" Inherits="NewUser" Title="Employee Details Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function redirect() {
            window.document.location.href = "ViewAllusers.aspx";
        }
    </script>
    <h3 id="h3Title" runat="server">
        Add New Employee</h3>
    <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px;
        border: 0px;">
        <div class="pure-g">
            <div class="pure-u-1 pure-u-md-1-1">
                <div class="alert-box success" id="divSuccess" runat="server" style="top: -3px;">
                </div>
                <div class="alert-box warning" id="divAlert" runat="server" style="top: -3x;">
                </div>
                <div class="alert-box notice" id="divNotice" runat="server" style="top: -3px;">
                </div>
                <div class="alert-box error" id="diverror" runat="server" style="top: -3px;">
                </div>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Full name <span style="color: #FF0000">*</span>
                <asp:DropDownList ID="ddlPrifix" runat="server" Width="200px">
                    <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                    <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                    <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                <br />
                <asp:TextBox ID="txtFullName" runat="server" Width="273px" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFullName"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="primary"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                User ID <span style="color: #FF0000">*</span>
                <br />
                <asp:TextBox ID="txtUID" runat="server" Width="273px" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUID"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-3" id="divPassword" runat="server">
                Password <span style="color: #FF0000">*</span>
                <br />
                <asp:TextBox ID="txtPassword" runat="server" Width="273px" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPWD1" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Email <span style="color: #FF0000">*</span>
                <br />
                <asp:TextBox ID="txtEmail" runat="server" Width="273px" MaxLength="100"></asp:TextBox>
                <!--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                EmpCode <span style="color: #FF0000">*</span>
                <br />
                <asp:TextBox ID="txtEmpCode" runat="server" Width="273px" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmpCode"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Phone <span style="color: #FF0000">*</span>
                <br />
                <asp:TextBox ID="txtPhone" runat="server" Width="273px" MaxLength="20"></asp:TextBox>
               <!-- <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>-->
            </div>
            <!-- Phone_Personal -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Phone (Personal):<br />
                                        <asp:TextBox ID="txtPhoneP" runat="server" Width="150px"></asp:TextBox>
                                    </div>
                                    <!-- Email_Personal -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Email (Personal):<br />
                                        <asp:TextBox ID="txtEmailP" runat="server" Width="150px"></asp:TextBox>                                        
                                    </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Date Of Birth *<br />
                <asp:TextBox ID="txtDOB" runat="server" Width="150px"></asp:TextBox>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDOB"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                System Type <span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlStype" runat="server" Width="273px">
                    <asp:ListItem Text="Nil" Value="Nil"></asp:ListItem>
                    <asp:ListItem Text="LapTop" Value="LapTop"></asp:ListItem>
                    <asp:ListItem Text="DeskTop" Value="DeskTop"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Asset Name<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlAssetName" runat="server" Width="273px">
                </asp:DropDownList>
               <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAssetName"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>-->
                <!--<asp:TextBox ID="txtAsset" runat="server" Width="273px" MaxLength="100"></asp:TextBox>                            
                            -->
            </div>
            <div style="width: 100%; background-color: #F0F0F0">
                <div class="pure-u-1 pure-u-md-1-3">
                    <asp:CheckBox ID="isMouse" runat="server" Text="Mouse Allotted" TextAlign="Left" />
                </div>
                <div class="pure-u-1 pure-u-md-1-3">
                    <asp:CheckBox ID="isDatacard" runat="server" Text="Data Card Allotted" TextAlign="Left" />
                </div>
                <div class="pure-u-1 pure-u-md-1-3">
                    <asp:CheckBox ID="isExtD" runat="server" Text="External HardDisk Allotted" TextAlign="Left" />
                </div>
            </div>
            <p>
                &nbsp;</p>
            <div class="pure-u-1 pure-u-md-1-3">
                Location<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlLocation" runat="server" Width="273px">
                </asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLocation"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Department<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlDepartments" runat="server" Width="273px">
                </asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartments"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Area of Work<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlArea" runat="server" Width="273px">
                </asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlArea"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Reporting Manager<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlManager" runat="server" Width="273px">
                </asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlManager"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Administartive Reporting Manager<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlAdminManager" runat="server" Width="273px">
                </asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAdminManager"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Payroll HR<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlpayrolHR" runat="server" Width="273px">
                </asp:DropDownList>
               <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlpayrolHR"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Location HR<span style="color: #FF0000">*</span>
                <br />
                <asp:DropDownList ID="ddlLocationHR" runat="server" Width="273px">
                </asp:DropDownList>
               <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlLocationHR"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Payroll Area <span style="color: #FF0000">*</span>
                <br />
               <asp:DropDownList ID="ddlPayrollArea" runat="server" Width="273px">
                </asp:DropDownList>
                <!--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPayrollArea"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>
             <div class="pure-u-1 pure-u-md-1-3">
                Band <span style="color: #FF0000">*</span>
                <br />
               <asp:DropDownList ID="ddlBand" runat="server" Width="273px">
                </asp:DropDownList>
               <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBand"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>
             <div class="pure-u-1 pure-u-md-1-3">
                Designation <span style="color: #FF0000">*</span>
                <br />
               <asp:DropDownList ID="ddlDesignation" runat="server" Width="273px">
                </asp:DropDownList>
               <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlDesignation"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="---"></asp:RequiredFieldValidator>-->
            </div>

                                    <!-- DateOfJoining -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Date of Joining:<br />
                                        <asp:TextBox ID="txtDOJ" runat="server" Width="150px"></asp:TextBox>
                                      <!--  <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDOJ"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="primary"></asp:RequiredFieldValidator>-->
                                    </div>

                                    <!-- VendorCode -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Vendor Code:<br />
                                        <asp:TextBox ID="txtVendor" runat="server" Width="150px"></asp:TextBox>
                                    </div>



                                    <!-- CompanySIM Radio Button -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Company SIM:<br />
                                        <asp:DropDownList ID="ddlcompanySim" runat="server" Width="273px">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- NewOrReplacement Radio Button -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        New or Replacement:<br />
                                        <asp:DropDownList ID="ddlNewOrReplacement" runat="server" Width="273px">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                            <asp:ListItem Text="Replacement" Value="Replacement"></asp:ListItem>
                                        </asp:DropDownList>
                                       <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlNewOrReplacement"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="" ValidationGroup="primary"></asp:RequiredFieldValidator>-->
                                    </div>

                                    <!-- PayrollNameIfNew and PayrollNameIfReplacement -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Payroll Name (if New):<br />
                                            <asp:TextBox ID="txtPayrollName" runat="server" Width="150px"></asp:TextBox>                                    </div>

                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Payroll Name (if Replacement):<br />
                                        <asp:TextBox ID="txtPayrollReplacement" runat="server" Width="150px"></asp:TextBox>
                                    </div>

                                    <!-- ReplacementFor -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Replacement For:<br />
                                          <asp:TextBox ID="txtReplacementForName" runat="server" Width="150px"></asp:TextBox>                                    </div>

                                    <!-- ReplacementEmpCode -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Replacement Emp Code:<br />
                                          <asp:TextBox ID="txtReplacementForEmpId" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- Shift_Details -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Shift Details:<br />
                                          <asp:TextBox ID="txtShiftDetails" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- Casual_Leave -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Casual Leave:<br />
                                          <asp:TextBox ID="txtCasualLeave" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- Sick_Leave -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Sick Leave:<br />
                                          <asp:TextBox ID="txtSickLeave" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- PermanentAddress -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Permanent Address:<br />
                                          <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" Width="250px" Columns="5"></asp:TextBox>                                      </div>

                                    <!-- AppraiserInAppraisal -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Appraiser in Appraisal:<br />
                                          <asp:TextBox ID="txtAppraiser" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- ReviewerInAppraisal -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Reviewer in Appraisal:<br />
                                          <asp:TextBox ID="txtReviewer" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- BankName -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Bank Name:<br />
                                          <asp:TextBox ID="txtBank" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- AccountNumber -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Account Number:<br />
                                          <asp:TextBox ID="txtAccount" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- IFSC -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        IFSC:<br />
                                          <asp:TextBox ID="txtIFSC" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- PANNumber -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        PAN Number:<br />
                                          <asp:TextBox ID="txtPAN" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- PhysicalLocation -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Physical Location:<br />
                                          <asp:TextBox ID="txtPhysicalLocation" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <!-- ESIC -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        ESIC:<br />
                                          <asp:TextBox ID="txtESIC" runat="server" Width="150px"></asp:TextBox> </div>

              <!-- Gadgets Radio Button -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Gadgets:<br />
                                        <asp:DropDownList ID="ddlGadgets" runat="server" Width="273px">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                        <!-- <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlGadgets"
                    ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>-->
                                    </div>
                                    <!-- MailIdCommonOrIndividual Radio Button -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Mail ID Common or Individual:<br />
                                          <asp:DropDownList ID="ddlcommonOrIndividual" runat="server" Width="273px">
                                              <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Common" Value="Common"></asp:ListItem>
                                            <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                        </asp:DropDownList>
                                        <!-- <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlcommonOrIndividual"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>-->
                                    </div>

            <div class="pure-u-1 pure-u-md-1-3">
                Roll Name
                <br />
                <asp:DropDownList ID="ddlRole" runat="server" Width="200px">
                    <asp:ListItem Text="-No Role-" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Super Admin" Value="1"></asp:ListItem>
                    <asp:ListItem Text="HR Team" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Senior Executive Member" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Travel Admin" Value="2"></asp:ListItem>
                    <asp:ListItem Text="General Admin (Quota upload)" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="pure-u-1 pure-u-md-1-3" id="divStatus" runat="server">
                Is Active? <span style="color: #FF0000">*</span>
                <asp:DropDownList ID="ddlStatus" runat="server" Width="273px">
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </asp:DropDownList>
            </div>

<div class="pure-u-1 pure-u-md-1-1">


<asp:GridView ID="ImagesGrid" runat="server" AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No files found"
                                        ForeColor="#333333" GridLines="None" OnRowCommand="ImagesGrid_RowCommand"
                                        PageSize="2" BorderColor="#D0D0D0" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1"
                                        Width="95%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="DOWNLOAD AADHAR">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn2" runat="server" CommandArgument='<%#Eval("FileID")%>' CommandName="Viewfile"
                                                        CausesValidation="false" Text='<%#Eval("FileName1")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" Font-Bold="True" />
                                        <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#C7A317" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <AlternatingRowStyle BackColor="WhiteSmoke" BorderColor="WhiteSmoke" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                        <PagerSettings PageButtonCount="5" />
                                    </asp:GridView>
        </div>
            <div class="pure-u-1 pure-u-md-1-3">
            <br />
                <asp:Button ID="ibtnSubmit" runat="server" Text="Add New" OnClick="ibtnSubmit_Click"
                    class="btn btn-success" />
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
            <br />
                <a id="ibtnBack" runat="server" href="Javascript:redirect();" class="btn btn-info"><span class="glyphicon glyphicon-circle-arrow-up">
                </span> Back to Employee list</a>
            </div>
        </div>
    </div>
    <link rel="stylesheet" type="text/css" href="jQuerydatetime/jquery.datetimepicker.css" />
    <script src="jQuerydatetime/jquery.datetimepicker.js"></script>
    <script>

        $('#ctl00_ContentPlaceHolder1_txtDOB').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en',
            datepicker: true,
            timepicker: false,
            format: 'm/d/Y',
            formatDate: 'Y/m/d'
            //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
            //startDate: '2010/01/05'
            //mask: '9999/19/39 29:59'
        });

        $('#ctl00_ContentPlaceHolder1_txtDOJ').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en',
            datepicker: true,
            timepicker: false,
            format: 'm/d/Y',
            formatDate: 'Y/m/d'
            //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
            //startDate: '2010/01/05'
            //mask: '9999/19/39 29:59'
        });

    </script>
</asp:Content>
