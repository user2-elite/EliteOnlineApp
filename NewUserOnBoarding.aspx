<%@ Page Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="NewUserOnBoarding.aspx.cs" Inherits="NewUserOnBoarding" Title="Employee Onboarding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<style type="text/css">
    .Initial
    {
      display: block;
      padding: 4px 18px 4px 18px;
      float: left;
      background: url("../Images/InitialImage.png") no-repeat right top;
      color: Black;
      font-weight: bold;
    }
    .Initial:hover
    {
      color: White;
      background: url("../Images/SelectedButton.png") no-repeat right top;
    }
    .Clicked
    {
      float: left;
      display: block;
      background: url("../Images/SelectedButton.png") no-repeat right top;
      padding: 4px 18px 4px 18px;
      color: Black;
      font-weight: bold;
      color: White;
    }

    /* Add a container div to wrap each section */
    .section-container {
      margin: 20px;
      padding: 20px;
      border: 1px solid #ddd;
      border-radius: 10px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }
    
    /* Add a header to each section */
    .section-header {
      background-color: #FFFFFF;
      padding: 10px;
      border-bottom: 1px solid #ddd;
    }
    
    
    /* Add a role-specific class to each section */
    .admin-section {
        padding: 10px;
        margin: 5px;
    }

    .HR-section {
        padding: 10px;
        margin: 5px;
    }

    .IT-section {
        padding: 10px;
        margin: 5px;
    }
    
    /* Add a role-specific icon to each section header */
    .admin-section .section-header:before {
      font-family: FontAwesome;
      content: "\f0e4"; 
      margin-right: 10px;
    }
    
    .HR-section .section-header:before {
      font-family: FontAwesome;
      content: "\f0e5"; 
      margin-right: 10px;
    }
    
    .IT-section .section-header:before {
      font-family: FontAwesome;
      content: "\f0e6"; 
      margin-right: 10px;
    }
    </style>
    <script language="javascript" type="text/javascript">
        function redirect() {
            window.document.location.href = "ViewOnBoardingUsers.aspx";
        }
    </script>
            <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px; border: 0px;">
            <div class="pure-g">
                <div class="pure-u-1 pure-u-md-1-1">
     <asp:HyperLink runat="server" id="linkNew" href="NewUserOnBoarding.aspx" Text="Add New Onboarding User"></asp:HyperLink>
      &nbsp; | &nbsp; <asp:HyperLink runat="server" id="linkViewPending" href="ViewOnBoardingPending.aspx" Text="Onboarding - Pending"></asp:HyperLink>
      &nbsp; | &nbsp; <asp:HyperLink runat="server" id="linkViewCompleted" href="ViewOnBoardingCompleted.aspx" Text="Onboarding - Completed"></asp:HyperLink>
    <h3 id="h3Title" runat="server">
        New Onboarding User</h3>
  </div>
                <div class="pure-u-1 pure-u-md-1-2"></div>
                <div class="pure-u-1 pure-u-md-1-2">
                    <div class="alert-box success" id="divSuccess" runat="server" >
                    </div>
                    <div class="alert-box warning" id="divAlert" runat="server">
                    </div>
                    <div class="alert-box notice" id="divNotice" runat="server">
                    </div>
                    <div class="alert-box error" id="diverror" runat="server">
                    </div>
                </div>
            </div>
        </div>
     <div class="pure-u-1 pure-u-md-1-1">
     <asp:Button runat="server" id="lnkHRAction1" Text="HR Initiation"  OnClick="lnkHRAction1_Click" BorderStyle="None" CssClass="Initial"></asp:Button>
    &nbsp; <asp:Button runat="server" id="lnkITAction2" Text="IT Action"  OnClick="lnkITAction2_Click" BorderStyle="None" CssClass="Initial"></asp:Button>
    &nbsp; <asp:Button runat="server" id="lnkHRFinalAction" Text="HR Final Action"  OnClick="lnkHRFinalAction_Click" BorderStyle="None" CssClass="Initial"></asp:Button>
  </div>
    <asp:MultiView ID="MainView" runat="server">
    <asp:View ID="View1" runat="server">
    <h2>Recruiter/HR Action</h2>
                <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px; border: 0px;">
                    <div class="pure-g">
                        <div class="section-container HR-section">
                            <asp:Panel ID="HRPanel" runat="server">
                                <!-- HR section content -->
                                <fieldset>
                                   
                                    <div class="pure-u-1 pure-u-md-1-1">
                                        Title
                                        <asp:DropDownList ID="ddlPrifix" runat="server" Width="80px">
                                            <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                            <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                                            <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        First Name
                                        <asp:TextBox ID="txtFirstName" runat="server" Width="263px" MaxLength="100"  ValidationGroup="primary"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="primary"></asp:RequiredFieldValidator>
                                       </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Middle Name
                                        <asp:TextBox ID="txtMiddleName" runat="server" Width="263px" MaxLength="100"></asp:TextBox>
                                       </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Last Name
                                        <asp:TextBox ID="txtLastName" runat="server" Width="263px" MaxLength="100"></asp:TextBox>                                        
                                       </div>
                                   
                                    <!-- Phone_Personal -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Phone (Personal):<br />
                                        <asp:TextBox ID="txtPhoneP" runat="server" Width="263px"></asp:TextBox>
                                    </div>
                                    <!-- Email_Personal -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Email (Personal):<br />
                                        <asp:TextBox ID="txtEmailP" runat="server" Width="263px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailP"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="primary"></asp:RequiredFieldValidator>
                                    </div>
                                      <!-- DateOfJoining -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Date of Joining:<br />
                                        <asp:TextBox ID="txtDOJ" runat="server" Width="263px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDOJ"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="primary"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Location
                                        <br />
                                        <asp:DropDownList ID="ddlLocation" runat="server" Width="263px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Department
                                        <br />
                                        <asp:DropDownList ID="ddlDepartments" runat="server" Width="263px">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Sub Department
                                        <br />
                                        <asp:DropDownList ID="ddlSubDepartments" runat="server" Width="273px">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Sales Location
                                        <br />
                                        <asp:TextBox ID="txtDivision" runat="server" Width="273px"></asp:TextBox> 

                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Designation
                                        <br />
                                        <asp:DropDownList ID="ddlDesignation" runat="server" Width="263px"  ValidationGroup="primary">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDesignation"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="primary"></asp:RequiredFieldValidator>
                                    </div>
                                   
                                    
                                      <div class="pure-u-1 pure-u-md-1-3">
                                        Payroll Area
                                        <br />
                                        <asp:DropDownList ID="ddlPayrollArea" runat="server" Width="263px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Band
                                        <br />
                                        <asp:DropDownList ID="ddlBand" runat="server" Width="263px">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Computer Required?
                                        <br />
                                        <asp:DropDownList ID="ddlComputerRequired" runat="server" Width="263px"  ValidationGroup="primary" >
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlStype"
                                        ErrorMessage="Required" SetFocusOnError="True"  ValidationGroup="primary"></asp:RequiredFieldValidator>

                                    </div>
                                      <div class="pure-u-1 pure-u-md-1-3">
                                        Company SIM Required:<br />
                                        <asp:DropDownList ID="ddlcompanySim" runat="server" Width="263px">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Official EmailId Required?:<br />
                                        <asp:DropDownList ID="ddlOfficialEmailRequired" runat="server" Width="263px">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        System Type
                                        <br />
                                        <asp:DropDownList ID="ddlStype" runat="server" Width="263px"  ValidationGroup="primary">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Nil" Value="Nil"></asp:ListItem>
                                            <asp:ListItem Text="LapTop" Value="LapTop"></asp:ListItem>
                                            <asp:ListItem Text="DeskTop" Value="DeskTop"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlStype"
                                        ErrorMessage="Required" SetFocusOnError="True"  ValidationGroup="primary"></asp:RequiredFieldValidator>

                                    </div>

                                <!--
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Company Number Required?:<br />
                                        <asp:DropDownList ID="ddlCompanyNumberRequired" runat="server" Width="273px">
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                               
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Alternate Mobile Number:<br />
                                            <asp:TextBox ID="txtAlternateMobile" runat="server" Width="150px"></asp:TextBox> 
                                    </div>
                                    -->
                                    <!-- NewOrReplacement Radio Button -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        New or Replacement:<br />
                                        <asp:DropDownList ID="ddlNewOrReplacement" runat="server" Width="263px"  ValidationGroup="primary">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                            <asp:ListItem Text="Replacement" Value="Replacement"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlNewOrReplacement"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="" ValidationGroup="primary"></asp:RequiredFieldValidator>
                                    </div>

                                    <!-- PayrollNameIfNew and PayrollNameIfReplacement -->
                                    <!--
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Payroll Name (if New):<br />
                                            <asp:TextBox ID="txtPayrollName" runat="server" Width="150px"></asp:TextBox> 

                                    </div>
                                    -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Payroll Name (if Replacement):<br />
                                         <asp:DropDownList ID="ddlPayrollReplacement" runat="server" Width="263px">
                                        </asp:DropDownList>
                                    </div>

                                    <!-- ReplacementFor -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Replacement For:<br />
                                          <asp:TextBox ID="txtReplacementForName" runat="server" Width="263px"></asp:TextBox>                                    </div>

                                    <!-- ReplacementEmpCode -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Replacement Emp Code:<br />
                                          <asp:TextBox ID="txtReplacementForEmpId" runat="server" Width="263px"></asp:TextBox>                                      </div>

                                    <!-- PhysicalLocation -->
                                    <!--
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Physical Location:<br />
                                          <asp:TextBox ID="txtPhysicalLocation" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                   -->
                                    <div class="pure-u-1 pure-u-md-1-1">
                                        Comments if any
                                        <asp:TextBox ID="txtHRNotes" runat="server" Width="310px" TextMode="MultiLine" Rows="5"></asp:TextBox>                                        
                                       </div>
                                    <p>
                                        &nbsp;
                                    </p>
                                    <div class="pure-u-1 pure-u-md-1-1">
                                        <br />
                                        <asp:Button ID="ibtnHRSave" runat="server" Text="Save Details" OnClick="ibtnHRSave_Click" class="btn btn-success" ValidationGroup="primary" />
                                        &nbsp; <asp:Button ID="btnProceedToIT" runat="server" Text="Proceed to IT" OnClick="btnProceedToIT_Click" class="btn btn-success" ValidationGroup="primary" />
                                        &nbsp;<asp:Button ID="ibtnRejectCandidate" runat="server" Text="Reject Candidate" OnClick="ibtnRejectCandidate_Click" class="btn btn-warning" />

                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px; border: 0px;">
                    <div class="pure-g">
                        <div class="section-container IT-section" id="">
                            <asp:Panel ID="pnlFileUpload" runat="server">
                                <!-- IT section content -->
                                <fieldset>
                                    <div class="pure-u-1 pure-u-md-1-3">
                    Browse Aadhar Card <i>(pdf/jpeg)</i><br />
                    <asp:FileUpload ID="uplAadhar" runat="server" CssClass="form-control" Width="300px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="uplAadhar"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" CssClass="btn btn-success" ValidationGroup="upload" />
                </div>
                                    <p>
                                        &nbsp;
                                    </p>
                                    
<div class="pure-u-1 pure-u-md-1-1">


<asp:GridView ID="ImagesGrid" runat="server" AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No files found"
                                        ForeColor="#333333" GridLines="None" OnRowCommand="ImagesGrid_RowCommand"
                                        PageSize="2" BorderColor="#D0D0D0" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1"
                                        Width="95%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="View">
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

                                </fieldset>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
    </asp:View>
    <asp:View ID="View2" runat="server">
    <h2>IT Action</h2>
                <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px; border: 0px;">
                    <div class="pure-g">
                        <div class="section-container IT-section" id="">
                            <asp:Panel ID="ITPanel" runat="server">
                                <!-- IT section content -->
                                <fieldset>
                                   
 <!-- ESIC -->
                                  
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Mobile Number:<br />
                                          <asp:TextBox ID="txtMobileNumber" runat="server" Width="150px"></asp:TextBox>                                      </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Email 
                                        <br />
                                        <asp:TextBox ID="txtEmail" runat="server" Width="273px" MaxLength="100" ValidationGroup="ITSection"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                                    validationgroup="ITSection" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <!-- MailIdCommonOrIndividual Radio Button -->
                                    <!--<div class="pure-u-1 pure-u-md-1-3">
                                        Mail ID Common or Individual:<br />
                                          <asp:DropDownList ID="ddlcommonOrIndividual" runat="server" Width="273px">
                                              <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Common" Value="Common"></asp:ListItem>
                                            <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlcommonOrIndividual"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="" ValidationGroup="ITSection"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Phone
                                        <br />
                                        <asp:TextBox ID="txtPhone" runat="server" Width="273px" MaxLength="20"></asp:TextBox>
                                       </div>
                                        -->
                                      <div class="pure-u-1 pure-u-md-1-3">
                                        System Purchase:<br />
                                          <asp:TextBox ID="txtSystemPurchase" runat="server" Width="150px"></asp:TextBox>                                      </div>
                                    <!-- Gadgets Radio Button -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Gadgets:<br />
                                        <asp:DropDownList ID="ddlGadgets" runat="server" Width="273px">
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator InitialValue="" ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlGadgets"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="ITSection"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Asset Name
                                        <br />
                                        <asp:DropDownList ID="ddlAssetName" runat="server" Width="273px" ValidationGroup="ITSection">
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAssetName"
                    ErrorMessage="Required" SetFocusOnError="True" InitialValue="" ValidationGroup="ITSection"></asp:RequiredFieldValidator>
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
                                         <div class="pure-u-1 pure-u-md-1-3">
                                            <asp:CheckBox ID="isMobile" runat="server" Text="Mobile Allotted" TextAlign="Left" />
                                        </div>
                                         <div class="pure-u-1 pure-u-md-1-3">
                                            <asp:CheckBox ID="isHeadSet" runat="server" Text="Headset Allotted" TextAlign="Left" />
                                        </div>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Capex Number:<br />
                                        <asp:TextBox ID="txtCapexNumber" runat="server" Width="263px"></asp:TextBox>                                        
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        PO Number:<br />
                                        <asp:TextBox ID="txtPONumber" runat="server" Width="263px"></asp:TextBox>                                        
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        Amount:<br />
                                        <asp:TextBox ID="txtAmount" runat="server" Width="263px"></asp:TextBox>                                        
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-1">
                                        Comments if any
                                        <asp:TextBox ID="txtITNotes" runat="server" Width="310px" TextMode="MultiLine" Rows="5"></asp:TextBox>                                        
                                       </div>

<fieldset>
                                    <div class="pure-u-1 pure-u-md-1-3">
                    Browse Invoice <i>(pdf/jpeg)</i><br />
                    <asp:FileUpload ID="uplPO" runat="server" CssClass="form-control" Width="300px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="uplPO"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True" ValidationGroup="upload"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnUploadPO" runat="server" Text="Upload File" OnClick="btnUploadPO_Click" CssClass="btn btn-success" ValidationGroup="upload" />
                </div>
                                    <p>
                                        &nbsp;
                                    </p>
                                    
<div class="pure-u-1 pure-u-md-1-1">


<asp:GridView ID="gvPO" runat="server" AutoGenerateColumns="False" CellPadding="4" EmptyDataText="No files found"
                                        ForeColor="#333333" GridLines="None" OnRowCommand="ImagesGridPO_RowCommand"
                                        PageSize="2" BorderColor="#D0D0D0" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1"
                                        Width="95%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPO" runat="server" CommandArgument='<%#Eval("FileID")%>' CommandName="ViewPOfile"
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

                                </fieldset>

                                    <p>
                                        &nbsp;
                                    </p>
                                    <div class="pure-u-1 pure-u-md-1-1">
                                        <br />
                                        <asp:Button ID="ibtnITSave" Visible="false" runat="server" Text="Save Details" OnClick="ibtnITSave_Click" ValidationGroup="ITSection" class="btn btn-success"  Enabled="false"/>
                                        <asp:Button ID="ibtnProceedToHR" runat="server" Text="Proceed to HR" OnClick="ibtnProceedToHR_Click" validationgroup="ITSection" class="btn btn-success" />

                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

    </asp:View>
    <asp:View ID="View3" runat="server">
    <h2>Convert To Employee</h2>
                <div class="section-container admin-section">
                    <asp:Panel ID="AdminPanel" runat="server">
                        <!-- admin section content -->
                        <fieldset>
                            <div class="pure-form pure-form-stacked" style="width: 800px; margin: 0px; padding: 3px; border: 0px;">
                                <div class="pure-g">
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        EmpCode /SAP No
                                        <br />
                                        <asp:TextBox ID="txtEmpCode" runat="server" Width="273px" MaxLength="10" validationgroup="FinalInfoGroup"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmpCode"
                                         validationgroup="FinalInfoGroup" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Reporting Manager
                                        <br />
                                        <asp:DropDownList ID="ddlManager" runat="server" Width="273px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Administartive Reporting Manager
                                        <br />
                                        <asp:DropDownList ID="ddlAdminManager" runat="server" Width="273px">
                                        </asp:DropDownList>
                                    </div>
                                    <!-- VendorCode -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Vendor Code:<br />
                                        <asp:TextBox ID="txtVendor" runat="server" Width="150px"></asp:TextBox>
                                    </div>

                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Date Of Birth *<br />
                                        <asp:TextBox ID="txtDOB" runat="server" Width="150px" validationgroup="FinalInfoGroup"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDOB"
                    ErrorMessage="Required" SetFocusOnError="True" validationgroup="FinalInfoGroup"></asp:RequiredFieldValidator>
                                        </div>                                   
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Payroll HR
                                        <br />
                                        <asp:DropDownList ID="ddlpayrolHR" runat="server" Width="273px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Location HR
                                        <br />
                                        <asp:DropDownList ID="ddlLocationHR" runat="server" Width="273px">
                                        </asp:DropDownList>
                                    </div>
                                    <!-- Shift_Details -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Shift Details:<br />
                                          
                                    <asp:DropDownList ID="ddlShiftDetail" runat="server" Width="200px">
                                            <asp:ListItem Text="Day" Value="Day"></asp:ListItem>
                                            <asp:ListItem Text="Night" Value="Night"></asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
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
                                    <!--
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Appraiser in Appraisal:<br />
                                          <asp:TextBox ID="txtAppraiser" runat="server" Width="150px"></asp:TextBox>                                      </div>
                                        -->
                                    <!-- ReviewerInAppraisal -->
                                    <!--
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Reviewer in Appraisal:<br />
                                          <asp:TextBox ID="txtReviewer" runat="server" Width="150px"></asp:TextBox>                                      </div>
                                    -->
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

 <!-- ESIC -->
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        ESIC:<br />
                                          <asp:TextBox ID="txtESIC" runat="server" Width="150px"></asp:TextBox>                                      </div>

                                    <div class="pure-u-1 pure-u-md-1-3">
                                        User ID 
                                        <br />
                                        <asp:TextBox ID="txtUID" runat="server" Width="273px" MaxLength="30" validationgroup="FinalInfoGroup"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUID"
                                                                    validationgroup="FinalInfoGroup" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-3">
                                        PF Number:<br />
                                        <asp:TextBox ID="txtPFNumber" runat="server" Width="263px"></asp:TextBox>                                        
                                    </div>
                                    <!--
                                    <div class="pure-u-1 pure-u-md-1-3" id="divPassword" runat="server">
                                        Password 
                                        <br />
                                        <asp:TextBox ID="txtPassword" runat="server" Width="273px" MaxLength="30"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPWD1" runat="server" ControlToValidate="txtPassword"
                                                                    validationgroup="FinalInfoGroup" ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    -->
                                  

                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Roll Name
                                        <br />
                                        <asp:DropDownList ID="ddlRole" runat="server" Width="200px">
                                            <asp:ListItem Text="-No Role-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Super Admin" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="HR Team" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="IT Team" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Senior Executive Member" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Travel Admin" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="General Admin (Quota upload)" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="pure-u-1 pure-u-md-1-1">
                                        Comments if any
                                        <asp:TextBox ID="txtHRNotesFinal" runat="server" Width="310px" TextMode="MultiLine" Rows="5"></asp:TextBox>                                        
                                       </div>
                                </div>
                            </div>
                            <p>
                                &nbsp;
                            </p>
                            <div class="pure-u-1 pure-u-md-1-1">
                                <br />
                                <asp:Button ID="ibtnSaveFinal" Visible="false" runat="server" Text="Save Details" OnClick="ibtnSaveFinal_Click" validationgroup="FinalInfoGroup" class="btn btn-success" Enabled="false" />
                                <asp:Button ID="ibtnSubmitFinal" runat="server" Text="Create Employee" OnClick="ibtnSubmitFinal_Click" validationgroup="FinalInfoGroup" class="btn btn-success" />
                                <asp:Button ID="ibtnHold" runat="server" Text="Hold Candidate" OnClick="ibtnHold_Click" validationgroup="FinalInfoGroup" class="btn btn-success" Enabled="false" />
                            </div>
                        </fieldset>
                    </asp:Panel>
                </div>
    </asp:View>
          </asp:MultiView>
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
