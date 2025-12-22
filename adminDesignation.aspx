<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminDesignation.aspx.cs" Inherits="AddDesignationName" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h3>Designation Name - Add New</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset>
                <div style="border: 1px; width: 90%; padding: 5px;">
                    <asp:Panel runat="server" ID="pnlAddNew">
                        <div class="pure-form pure-form-stacked" style="background-color: #EEEEEE; border: 1px;
                            width: 98%; margin: 10px; padding: 10px;">
                            <div class="pure-g">
                                <hr width="100%; height:1px" />
                                <br />
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Name<br />
                                    <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtName"
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
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" class="btn btn-success"
                                        OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlList">
                        <br />
                        <h4>
                            View Already existing Designations</h4>
                        <hr width="100%; height:1px" />
                        <div class="alert-box success" id="divListSuccess" runat="server">
                        </div>
                        <div class="alert-box error" id="divListError" runat="server">
                        </div>
                        <div class="pure-u-1">
                            <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None" OnItemCommand="grid1_ItemCommand"
                                Width="100%">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditItemStyle BackColor="#999999" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingItemStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
                                <ItemStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White" />
                                <Columns>
                                    <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AddedOn" HeaderText="Added On"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AddedBy" HeaderText="Added By"></asp:BoundColumn>
                                     <asp:TemplateColumn HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandName="cmEdit"
                                                Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" CommandName="cmDelete"
                                                Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' OnClientClick="javascript:return confDelete();"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                        <br />
                    </asp:Panel>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
