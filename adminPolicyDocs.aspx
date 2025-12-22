<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminPolicyDocs.aspx.cs" Inherits="adminPolicyDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        Policy Documents - Add New</h3>
    <p>
        Note: Maximum number of documents per each category should be 15.
    </p>
    <div style="width: 200px; left: 300px;">
        <div class="alert-box success" id="divSuccess" runat="server">
        </div>
        <div class="alert-box warning" id="divAlert" runat="server">
        </div>
        <div class="alert-box notice" id="divNotice" runat="server">
        </div>
        <div class="alert-box error" id="diverror" runat="server">
        </div>
    </div>
    <br />
    <div>
        <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
            border: 0px;">
            <div class="pure-g">
                <div id="divCategory" runat="server" visible="false">
                    <div class="pure-u-1 pure-u-md-1-1">
                        Category name (Maximum 50 Characters)<br />
                        <asp:TextBox ID="txtCatTitle" runat="server" Width="525px" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCatTitle"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        Description (Maximum 250 Characters)<br />
                        <asp:TextBox ID="txtCatDescr" runat="server" Width="525px" TextMode="MultiLine" Rows="3"
                            cols="25"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCatDescr"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        <br />
                        <asp:Button ID="btnAddCategory" runat="server" Text="Save" OnClick="btnAddCategory_Click"
                            class="pure-button pure-button-primary" />
                        <asp:Button ID="btnBack" runat="server" Text="Back to Category" OnClick="btnBack_Click"
                            class="pure-button pure-button-primary" CausesValidation="false" />
                    </div>
                </div>
                <!-- Category items -->
                <div id="divCategoryItems" runat="server" visible="false">
                    <div class="pure-u-1 pure-u-md-1-3">
                        Document Category Name:
                        <asp:DropDownList ID="ddlCategory" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <b>
                            <asp:LinkButton ID="lnkaddCategory" runat="server" OnClick="lnkaddCategory_Click"
                                CausesValidation="false">Create New Document Category</asp:LinkButton></b>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-3">
                        Document Title (Optional; If empty, File name will be the title of the document<br />
                        <asp:TextBox ID="txtTitle" runat="server" Width="525px" MaxLength="50"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-3">
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        Description (Maximum 250 Characters)<br />
                        <asp:TextBox ID="txtDesc" runat="server" Width="525px" TextMode="MultiLine" Rows="3"
                            cols="25"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        (File size should be with in 500 KB. Recommended formats(.doc/.docx/.pdf/.xls/.xlsx)
                        <br />
                        Upload File
                        <br />
                        <asp:FileUpload ID="fileuploadimages" runat="server" />
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Upload Document" OnClick="btnSubmit_Click"
                            class="pure-button pure-button-primary" />
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        &nbsp;
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="1" CellPadding="10" OnEditCommand="Delete_Command">
                            <ItemTemplate>
                                ID:
                                <asp:Label ID="ItemLabel" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                    runat="server" />
                                <a href='<%# Eval("FileName","~/Gallery/PolicyDocs/{0}") %>' title='<%#Eval("Description") %>'
                                    runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Title") %>
                                </a>
                                <br />
                                Description:
                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                <br />
                                <asp:LinkButton ID="EditButton" Text="Delete Document" CommandName="Edit" runat="server"
                                    CausesValidation="false" OnClientClick="javascript:return confDelete();" />
                                    <br /><br />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#F0F0F0" BorderStyle="dotted" BorderWidth="3px" HorizontalAlign="left"
                                VerticalAlign="Bottom" Width="100%" />
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
