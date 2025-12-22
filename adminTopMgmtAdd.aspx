<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminTopMgmtAdd.aspx.cs" Inherits="adminTopMgmtAdd" ValidateRequest="false"
    Trace="false" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .custom-date-style
        {
            background-color: red !important;
        }
    </style>
    <script language="javascript">
        function confDelete() {
            if (window.confirm("Click on 'Ok' button to confirm delete")) {
                return true;
            }
            else {
                return false;
            }
        }    
    </script>
    <h3>
        Top Management List - Update</h3>
    <p>
        Update Top Management list here.</p>
    <div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 10px;
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
                 <div class="pure-u-1 pure-u-md-1-1">
                    Company Unit <br />
                   <asp:DropDownList ID="ddlUnit" runat="server">
                   <asp:ListItem Text="Select" Value=""></asp:ListItem>
                   <asp:ListItem Text="Elite Corporate" Value="Elite Corporate"></asp:ListItem>
                   <asp:ListItem Text="Food Processing" Value="Elite FMCG"></asp:ListItem>
                   <asp:ListItem Text="Jewel Country" Value="JewelCountry"></asp:ListItem>
                   <asp:ListItem Text="Real Estate" Value="Real Estate"></asp:ListItem>
                   <asp:ListItem Text="Solgen" Value="Solgen"></asp:ListItem>
                   <asp:ListItem Text="Organic and Export" Value="Organic and Export"></asp:ListItem>
                   <asp:ListItem Text="International Trade" Value="InternationalTrade"></asp:ListItem>
                   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlUnit"
                        ErrorMessage="Required" SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    Name (Maximum 50 Characters)<br />
                    <asp:TextBox ID="txtName" runat="server" Width="525px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    Designation/Title (Maximum 50 Characters)<br />
                    <asp:TextBox ID="txtDesig" runat="server" Width="525px" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesig"
                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    (Image size should be with in 100 KB. Please ensure the width and Height should
                    be 140 X 140 Pixels)
                    <br />
                    Upload Image
                    <br />
                    <asp:FileUpload ID="fileuploadimages" runat="server" />
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    Profile Text
                    <FTB:FreeTextBox ID="txtDetails" ToolbarLayout="Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|BulletedList,NumberedList|Cut,Copy,Paste,Delete;Undo,Redo,Print|InsertTable"
                        runat="Server" EnableHtmlMode="false" BackColor="#FFFFFF" Height="350" Width="700" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDetails"
                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    Sort Order position (Enter Number like 1,2,3,4,5 etc)<br />
                    <asp:TextBox ID="txtSortOrder" runat="server" Width="100px" MaxLength="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSortOrder"
                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    <asp:Button ID="btnSubmit" runat="server" Text="Save Details" class="btn btn-success"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlList">
            <br />
            <h4>
                View Already Saved Data</h4>
            <hr width="100%; height:1px" />
            <div class="pure-u-1">
                <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None" OnItemCommand="grid1_ItemCommand"
                    Width="100%">
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                    <ItemStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
                    <Columns>
                        <asp:BoundColumn DataField="MGMT_Name" HeaderText="Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MGMT_Unit" HeaderText="Unit Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MGMT_Desig" HeaderText="Designation"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MGMT_Profile" HeaderText="Profile"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Created_On" HeaderText="Added On"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Created_By" HeaderText="Added By"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="cmEdit"
                                    Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmDelete"
                                    Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                    OnClientClick="javascript:return confDelete();"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
