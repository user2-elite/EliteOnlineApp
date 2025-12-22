<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="CRMapping.aspx.cs" Inherits="CRMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .custom-date-style
        {
            background-color: red !important;
        }
    </style>
    <div style="position: relative; left: 100px; width: 800px;">
        <div style="position: absolute; left: 250px; top: 1px; width: 400px;">
            <div class="alert-box success" id="divSuccess" runat="server">
            </div>
            <div class="alert-box warning" id="divAlert" runat="server">
            </div>
            <div class="alert-box notice" id="divNotice" runat="server">
            </div>
            <div class="alert-box error" id="diverror" runat="server">
            </div>
        </div>
        <div class="pure-form pure-form-stacked">
            <h2>
                Conference Room Add</h2>
            <div class="pure-g">
                <div class="pure-u-1 pure-u-md-1-1">
                    Unit Name<br />
                    <asp:DropDownList ID="ddlFacName" runat="server" Width="250px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFacName" runat="server" ControlToValidate="ddlFacName"
                        ErrorMessage="Select the Unit Name" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-1">
                    Conference Room Name
                    <br />
                    <asp:TextBox ID="txtConfRoom" runat="server" Width="250px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfRoomId" runat="server" ErrorMessage="Enter Conf Room"
                        ControlToValidate="txtConfRoom"></asp:RequiredFieldValidator>
                </div>
                <div class="pure-u-1 pure-u-md-1-3">
                    <asp:Button ID="btnMap" runat="server" Text="Add Conference Room" OnClick="btnMap_Click"
                        CausesValidation="true" class="pure-button pure-button-primary" />
                </div>
                <br />
            </div>
        </div>
        <br />
        <asp:GridView ID="gvCRMapping" runat="server" AutoGenerateColumns="False" DataKeyNames="cr_id,unitID,crName"
            CellPadding="4" Font-Bold="False" Font-Size="Small" ForeColor="#333333" GridLines="None"
            Width="100%" HorizontalAlign="left">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <AlternatingRowStyle BackColor="White" Font-Size="11pt" ForeColor="#284775" />
            <RowStyle BackColor="#F7F6F3" Font-Size="11pt" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="11pt" ForeColor="White"
                HorizontalAlign="Left" />
            <Columns>
                <asp:BoundField DataField="cr_id" Visible="false" />
                <%--  <asp:BoundField DataField="fs_id" Visible="false" />--%>
                <asp:TemplateField HeaderText="Unit Name">
                    <ItemTemplate>
                        <asp:Label ID="lblFacName" runat="server" Text='<%#Eval("UnitName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Conference Room">
                    <ItemTemplate>
                        <asp:Label ID="lblcrName" runat="server" Text='<%#Eval("crName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
