<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SearchKM.aspx.cs" Inherits="SearchKM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="JQuery1.11.4/jquery-ui.css">
    <script src="JQuery1.11.4/jquery-1.10.2.js"></script>
    <script src="JQuery1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#tabs").tabs();
        });
    </script>
    <style>
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
        #tabs-1
        {
            font-size: 14px;
        }
        #tabs-2
        {
            font-size: 14px;
        }
        #tabs-3
        {
            font-size: 14px;
        }
        .custom-date-style
        {
            background-color: red !important;
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
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">CRM Search</a></li>
            <li><a href="#tabs-2">Best Practice Search</a></li>
            <li><a href="#tabs-3">ATR Search</a></li>
        </ul>
        <div id="tabs-1">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <p>
                            <div class="pure-form pure-form-stacked">
                                <div class="pure-g">
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Product Item<br />
                                        <asp:DropDownList ID="ddProdCategory" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Poduction Unit<br />
                                        <asp:DropDownList ID="ddCompanyUnit" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Complaint Type<br />
                                        <asp:DropDownList ID="ddTypeOfComplaint" runat="server" Width="250px">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        Enter Search Text
                                        <asp:TextBox ID="txtSearch1" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                                    </div>
                                    <%-- <div class="pure-u-1 pure-u-md-1-3">
                                    Search Type
                                    <asp:DropDownList ID="ddlSearchType" runat="server" Width="170px">
                                        <asp:ListItem Text="Contains" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Exactly" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Begin With" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="End With" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                        <br />
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="0"
                                            AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <img border="0" src="Images/progress1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:Button ID="Button1" runat="server" Text="Find Results" OnClick="btnSubmit1_Click"
                                            class="pure-button pure-button-primary" ValidationGroup="Submit" />
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3">
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div id="divSearchResultsCRM" runat="server">
                            </div>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="tabs-2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <p>
                            <script>
                                $(function () {
                                    initializer1();
                                });
                                var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
                                prmInstance.add_endRequest(function () {
                                    //you need to re-bind your jquery events here
                                    initializer1();
                                });
                                function initializer1() {
                                    $(function () {
                                        $("#ctl00_ContentPlaceHolder1_txtSearch2").autocomplete({
                                            // TODO doesn't work when loaded from /demos/#autocomplete|remote
                                            source: "KeyWordsBP.ashx",
                                            minLength: 1
                                        });
                                    });
                                }
                            </script>
                            <div class="pure-form pure-form-stacked">
                                <div class="pure-g">
                                    <br />
                                    <div class="pure-u-1 pure-u-md-1-3" style="font-weight: bold;">
                                        Enter Key Word
                                        <asp:TextBox ID="txtSearch2" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3" style="font-weight: bold;">
                                        <br />
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="0"
                                            AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <img border="0" src="Images/progress1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:Button ID="btnSubmit2" runat="server" Text="Find Results" OnClick="btnSubmit2_Click"
                                            class="pure-button pure-button-primary" ValidationGroup="Submit" />
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3" style="font-weight: bold;">
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div id="divSearchResultsBP" runat="server">
                            </div>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="tabs-3">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <p>
                            <script>
                                $(function () {
                                    initializer2();
                                });
                                var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
                                prmInstance.add_endRequest(function () {
                                    //you need to re-bind your jquery events here
                                    initializer2();
                                });
                                function initializer2() {
                                    $(function () {
                                        $("#ctl00_ContentPlaceHolder1_txtSearch3").autocomplete({
                                            // TODO doesn't work when loaded from /demos/#autocomplete|remote
                                            source: "KeyWordsATR.ashx",
                                            minLength: 1
                                        });
                                    });
                                }
                            </script>
                            <div class="pure-form pure-form-stacked">
                                <div class="pure-g">
                                    <br />
                                    <div class="pure-u-1 pure-u-md-1-3" style="font-weight: bold;">
                                        Enter Key Word
                                        <div class="ui-widget">
                                            <asp:TextBox ID="txtSearch3" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3" style="font-weight: bold;">
                                        <br />
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" DynamicLayout="true" DisplayAfter="0"
                                            AssociatedUpdatePanelID="UpdatePanel3">
                                            <ProgressTemplate>
                                                <img border="0" src="Images/progress1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:Button ID="btnSearch3" runat="server" Text="Find Results" OnClick="btnSubmit3_Click"
                                            class="pure-button pure-button-primary" ValidationGroup="Submit" />
                                    </div>
                                    <div class="pure-u-1 pure-u-md-1-3" style="font-weight: bold;">
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div id="divSearchResultsATR" runat="server">
                            </div>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
