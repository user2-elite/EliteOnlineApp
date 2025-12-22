<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddKB.aspx.cs" Inherits="AddKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<style type="text/css">

.KBAdd{
border:2px solid #819BCB;-webkit-box-shadow: #A5B8DA 0px 21px 0px inset;-moz-box-shadow: #A5B8DA 0px 21px 0px inset; box-shadow: #A5B8DA 0px 21px 0px inset; -webkit-border-radius: 3px; -moz-border-radius: 3px;border-radius: 3px;font-size:12px;font-family:arial, helvetica, sans-serif; padding: 10px 10px 10px 10px; text-decoration:none; display:inline-block;text-shadow: -1px -1px 0 rgba(0,0,0,0.3);font-weight:bold; color: #FFFFFF;
 background-color: #a5b8da; background-image: -webkit-gradient(linear, left top, left bottom, from(#a5b8da), to(#7089b3));
 background-image: -webkit-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -moz-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -ms-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -o-linear-gradient(top, #a5b8da, #7089b3);
 background-image: linear-gradient(to bottom, #a5b8da, #7089b3);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#a5b8da, endColorstr=#7089b3);
}

.KBAdd:hover{
 border:2px solid #819BCB;
 background-color: #819bcb; background-image: -webkit-gradient(linear, left top, left bottom, from(#819bcb), to(#536f9d));
 background-image: -webkit-linear-gradient(top, #819bcb, #536f9d);
 background-image: -moz-linear-gradient(top, #819bcb, #536f9d);
 background-image: -ms-linear-gradient(top, #819bcb, #536f9d);
 background-image: -o-linear-gradient(top, #819bcb, #536f9d);
 background-image: linear-gradient(to bottom, #819bcb, #536f9d);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#819bcb, endColorstr=#536f9d); 
}

.messagePos
{
	position:absolute;top:350px;left:200px; width:700px;
}
</style>
<div style="text-align:left;color:maroon; font-weight:bold; position:absolute;top:50px;right:20px;"><a href="KBView.aspx" Class="KBAdd" ><b><< Back</b></a></div>
    <div class="pure-form pure-form-stacked">
        <div class="pure-g">
            <asp:Label ID="Labelkb1" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="Knowledge Management"
                runat="server"></asp:Label>
            <hr width="100%; height:1px" />
            <br />
            <asp:Label ID="Labelkb3" class="pure-u-1 pure-u-md-1-1" Text="If you want to share your knowledge/experiance, enter below"
                runat="server"></asp:Label>
            <br />
            <br />
            <div class="pure-u-1 pure-u-md-1-3">
                Product Item<br />
                <asp:DropDownList ID="ddProdCategory" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB1" runat="server" ControlToValidate="ddProdCategory"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Poduction Unit<br />
                <asp:DropDownList ID="ddCompanyUnit" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB2" runat="server" ControlToValidate="ddCompanyUnit"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Complaint Type<br />
                <asp:DropDownList ID="ddTypeOfComplaint" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB3" runat="server" ControlToValidate="ddTypeOfComplaint"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-2">
                Complaint Description As per CRM<br />
                <asp:TextBox ID="txtKBDetails" runat="server" Width="450px" TextMode="MultiLine"
                    Height="80px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB5" runat="server" ControlToValidate="txtKBDetails"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-2">
                Root Cause Analysis<br />
                <asp:TextBox ID="txtRootCauseAnalysis" runat="server" Width="450px" TextMode="MultiLine"
                    Height="80px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB4" runat="server" ControlToValidate="txtRootCauseAnalysis"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-1">
                Corrective Action Taken<br />
                <asp:TextBox ID="txtKBSolution" runat="server" Width="750px" TextMode="MultiLine"
                    Height="60px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorKB6" runat="server" ControlToValidate="txtKBSolution"
                    ErrorMessage="Required" SetFocusOnError="True" ValidationGroup="KB"></asp:RequiredFieldValidator>
            </div>
            <div class="pure-u-1 pure-u-md-1-1">
                <asp:Button ID="btnSaveKB" runat="server" Text="Save" class="pure-button pure-button-primary"
                    ValidationGroup="KB" OnClick="btnSaveKB_Click" />
            </div>
            <div class="pure-u-1 pure-u-md-1-3 messagePos">
                <asp:Label ID="lblMessageText5" runat="server"></asp:Label>
            </div>
            <br />
            <asp:Label ID="LabelKB4" class="pure-u-1 pure-u-md-1-1" Font-Bold="true" Text="View Knowledge Management"
                runat="server"></asp:Label>
            <hr width="100%; height:1px" />
            <div class="pure-u-1">
                <asp:DataGrid ID="dgKB" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyField="ID" Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None"
                    OnItemCommand="dgKB_ItemCommand" Width="100%">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" Font-Size="8pt" ForeColor="#284775" />
                    <ItemStyle BackColor="#F7F6F3" Font-Size="8pt" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <Columns>
                        <asp:BoundColumn DataField="ProductItem" HeaderText="Product Item"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PoductionUnit" HeaderText="Poduction Unit"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ComplaintNature" HeaderText="Complaint Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="KB_Description" HeaderText="Description"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RootCauseAnalysis" HeaderText="Root Cause Analysis"></asp:BoundColumn>
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
</asp:Content>
