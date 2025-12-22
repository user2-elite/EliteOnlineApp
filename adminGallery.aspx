<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminGallery.aspx.cs" Inherits="adminGallery" %>

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
        Gallery - Add New</h3>
    <p>
     Note: Maximum number of picture per each category shoud be 25.
    </p>
    <link rel="stylesheet" href="Gallery/lightbox.css" type="text/css" media="screen" />
    <script type="text/javascript" src="Gallery/prototype.js"></script>
    <script type="text/javascript" src="Gallery/scriptaculous.js?load=effects"></script>
    <script type="text/javascript" src="Gallery/lightbox.js"></script>
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
                        <asp:Button ID="btnBack" runat="server" Text="Back to Gallery" OnClick="btnBack_Click"
                            class="pure-button pure-button-primary" CausesValidation="false" />
                    </div>
                </div>
                <!-- Gallery items -->
                <div id="divgalleryItems" runat="server" visible="false">
                    <div class="pure-u-1 pure-u-md-1-3">
                        Gallery Category Name:
                        <asp:DropDownList ID="ddlGallery" runat="server" 
                            onselectedindexchanged="ddlGallery_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <b><asp:LinkButton ID="lnkaddCategory" runat="server" OnClick="lnkaddCategory_Click"
                            CausesValidation="false" >Create New Gallery Category</asp:LinkButton></b>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-3">
                        Picture Title (Maximum 50 Characters)<br />
                        <asp:TextBox ID="txtTitle" runat="server" Width="525px" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                        (Image size should be with in 500 KB. Recommended format will be in portrait to
                        fit best in the home page.)
                        <br/>
                        Upload Image
                        <br />
                        <asp:FileUpload ID="fileuploadimages" runat="server" />
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                    <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Upload Picture" OnClick="btnSubmit_Click"
                            class="pure-button pure-button-primary" />
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                    &nbsp;
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="3" CellPadding="10" OnEditCommand="Delete_Command">
                            <ItemTemplate>
                                <a id="imageLink" href='<%# Eval("ImageName","~/Gallery/SlideImages/{0}") %>' title='<%#Eval("Description") %>'
                                    rel="lightbox[Brussels]" runat="server">
                                    <asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Gallery/SlideImages/{0}") %>'
                                        runat="server" Width="200" Height="100" />
                                </a>
                                <br />
                                <div style="text-align: center">
                                    ID:
                                    <asp:Label ID="ItemLabel" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                        runat="server" />
                                    Title: <b>
                                        <%# DataBinder.Eval(Container.DataItem, "Title")%></b>
                                    <br />
                                    Description:
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                    <br />
                                </div>
                                <asp:LinkButton ID="EditButton" Text="Delete Image" CommandName="Edit" runat="server"
                                    CausesValidation="false"  OnClientClick="javascript:return confDelete();"/>
                            </ItemTemplate>
                            <%-- <EditItemTemplate>                              
                                <br />
                                Image:
                                <asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Gallery/SlideImages/{0}") %>'
                                    runat="server" Width="200" Height="100" />
                                <br />--%>
                            <%--<asp:LinkButton id="DeleteButton" 
                 Text="Delete" 
                 CommandName="Delete" 
                 runat="server" CausesValidation="false"/>

            <asp:LinkButton id="CancelButton" 
                 Text="Cancel" 
                 CommandName="Cancel" 
                 runat="server" CausesValidation="false"/>--%>
                            <%--</EditItemTemplate>--%>
                            <ItemStyle BorderColor="#F0F0F0" BorderStyle="dotted" BorderWidth="3px" HorizontalAlign="Center"
                                VerticalAlign="Bottom" />
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
