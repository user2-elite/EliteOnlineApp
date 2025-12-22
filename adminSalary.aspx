<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminSalary.aspx.cs" Inherits="adminSalary" %>

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
        Salary Data - Upload</h3>
    <div class="alert-box success" id="divListSuccess" runat="server" style="position: absolute;
        left: -10; top: 1; width: 400px;">
    </div>
    <div class="alert-box error" id="divListError" runat="server" style="position: absolute;
        top: 1px; left: 300px; top: -25; width: 550px;">
    </div>
    <br />
    <div>
        <div style="width: 100%; color: maroon">
            <b><u>Instructions:</u></b> 1) Excel data format: <a href="Gallery/template.xls"><b>
                <i>[Template Format]</i></b></a> 2) Mandatory Fields in the excel sheet: Employee
            Code, Employee Name, Designation, Basic, HRA, PF Deductions, Earning Total, Deduction
            Total, Net Pay amount<br />
            4) Data should be in the first sheet and the excel sheet name should contain the
            word "salary".
        </div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 3px;
            border: 0px; background-color: #EEEEEE">
            <div class="pure-g" style="width: 100%;">
                <div class="pure-u-1 pure-u-md-1-6">
                    Company Unit Name<br />
                    <asp:DropDownList ID="ddCompanyUnit" runat="server" CssClass="form-control" Width="150px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddCompanyUnit"
                        ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-6">
                    Salary Month<br />
                    <asp:DropDownList ID="ddlSalMonth" runat="server" CssClass="form-control" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="ddlSalMonth"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-6">
                    Salary Year<br />
                    <asp:DropDownList ID="ddlSalYear" runat="server" CssClass="form-control" Width="120px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalYear"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-6">
                    Browse Excel Data <i>(.xls)</i><br />
                    <asp:FileUpload ID="upldExcelData" runat="server" CssClass="form-control" Width="300px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="upldExcelData"
                        ErrorMessage="Required" InitialValue="" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-6">
                    &nbsp;
                </div>
                <div class="pure-u-1 pure-u-md-1-6">
                    <br />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click"
                        CssClass="btn btn-success" />                        
                </div>
            </div>
        </div>
        <hr />
        <div>
            <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                DataKeyNames="Doc_ID" CellPadding="5" CellSpacing="5" Width="100%" Font-Bold="False"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Doc_ID" HeaderText="File ID" Visible="false" />
                    <asp:BoundField DataField="Doc_FileName" HeaderText="File Name" />
                    <asp:BoundField DataField="Attached_Date" HeaderText="Added On" />
                    <asp:BoundField DataField="Doc_AttachedBy" HeaderText="Attached By" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
            </asp:GridView>
        </div>
        <div id="divFinalData" runat="server" visible="false">
            <hr />
                        <asp:Button ID="btnUpdate1" runat="server" Text="Publish Selected Rows" OnClick="btnUpdate_Click"
                CssClass="btn btn-success" CausesValidation="false" />
            <asp:GridView ID="gvFinalData" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                DataKeyNames="Doc_ID" CellPadding="4" Font-Bold="False" ForeColor="#333333" GridLines="Both"
                OnRowCommand="gvFinalData_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox Checked="true" ID="chkSelection" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPreview" runat="server" CausesValidation="false" CommandName="cmPreview"
                                Text="Preview" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="Trans ID" />
                    <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                    <asp:BoundField DataField="Bank" HeaderText="Bank" />
                    <asp:BoundField DataField="AccNo" HeaderText="AccNo" />
                    <asp:BoundField DataField="Basic" HeaderText="Basic" />
                    <asp:BoundField DataField="HRA" HeaderText="HRA" />
                    <asp:BoundField DataField="TA" HeaderText="TA" />
                    <asp:BoundField DataField="TS" HeaderText="TS" />
                    <asp:BoundField DataField="SpecialAllowance" HeaderText="SpecialAllowance" />
                    <asp:BoundField DataField="Arrears" HeaderText="Arrears" />
                    <asp:BoundField DataField="PFDeduction" HeaderText="PFDeduction" />
                    <asp:BoundField DataField="ESI" HeaderText="ESI" />
                    <asp:BoundField DataField="FestivalAdvance" HeaderText="FestivalAdvance" />
                    <asp:BoundField DataField="SalaryAdvance" HeaderText="SalaryAdvance" />
                    <asp:BoundField DataField="ProfessionalTax" HeaderText="ProfessionalTax" />
                    <asp:BoundField DataField="TozhilaliKshemaNidhi" HeaderText="TozhilaliKshemaNidhi" />
                    <asp:BoundField DataField="LWF" HeaderText="LWF" />
                    <asp:BoundField DataField="TDS" HeaderText="TDS" />
                    <asp:BoundField DataField="LIC" HeaderText="LIC" />
                    <asp:BoundField DataField="LWF1" HeaderText="LWF1" />
                    <asp:BoundField DataField="EarningTotal" HeaderText="EarningTotal" />
                    <asp:BoundField DataField="DeductionTotal" HeaderText="DeductionTotal" />
                    <asp:BoundField DataField="Claims" HeaderText="Status" />
                    <asp:BoundField DataField="NetPay" HeaderText="NetPay" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
            </asp:GridView>
            <asp:Button ID="btnUpdate" runat="server" Text="Publish Selected Rows" OnClick="btnUpdate_Click"
                CssClass="btn btn-success" CausesValidation="false" />
        </div>

        <h3>View Published Details</h3>
        <asp:Button ID="btnGetPublishedData" runat="server" Text="Get Published Details" OnClick="btnGetPublishedData_Click"
                        CssClass="btn btn-info" CausesValidation="false" />
        <div >
        <div id="divPublishedTitle" runat="server"></div>
            <asp:GridView ID="gvPublishedData" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                DataKeyNames="Doc_ID" CellPadding="4" Font-Bold="False" ForeColor="#333333" GridLines="Both"
                OnRowCommand="gvPublishedData_RowCommand" EmptyDataRowStyle-ForeColor="Red" EmptyDataText="No data found for the selected Month, Year & Unit.">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPreview" runat="server" CausesValidation="false" CommandName="cmPreview"
                                Text="Preview" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmDelete"
                                Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ID" HeaderText="Trans ID" />
                    <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" />
                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                    <asp:BoundField DataField="Bank" HeaderText="Bank" />
                    <asp:BoundField DataField="AccNo" HeaderText="AccNo" />
                    <asp:BoundField DataField="Basic" HeaderText="Basic" />
                    <asp:BoundField DataField="HRA" HeaderText="HRA" />
                    <asp:BoundField DataField="TA" HeaderText="TA" />
                    <asp:BoundField DataField="TS" HeaderText="TS" />
                    <asp:BoundField DataField="SpecialAllowance" HeaderText="SpecialAllowance" />
                    <asp:BoundField DataField="Arrears" HeaderText="Arrears" />
                    <asp:BoundField DataField="PFDeduction" HeaderText="PFDeduction" />
                    <asp:BoundField DataField="ESI" HeaderText="ESI" />
                    <asp:BoundField DataField="FestivalAdvance" HeaderText="FestivalAdvance" />
                    <asp:BoundField DataField="SalaryAdvance" HeaderText="SalaryAdvance" />
                    <asp:BoundField DataField="ProfessionalTax" HeaderText="ProfessionalTax" />
                    <asp:BoundField DataField="TozhilaliKshemaNidhi" HeaderText="TozhilaliKshemaNidhi" />
                    <asp:BoundField DataField="LWF" HeaderText="LWF" />
                    <asp:BoundField DataField="TDS" HeaderText="TDS" />
                    <asp:BoundField DataField="LIC" HeaderText="LIC" />
                    <asp:BoundField DataField="LWF1" HeaderText="LWF1" />
                    <asp:BoundField DataField="EarningTotal" HeaderText="EarningTotal" />
                    <asp:BoundField DataField="DeductionTotal" HeaderText="DeductionTotal" />
                    <asp:BoundField DataField="Claims" HeaderText="Status" />
                    <asp:BoundField DataField="NetPay" HeaderText="NetPay" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
            </asp:GridView>            
        </div>
    </div>
</asp:Content>
