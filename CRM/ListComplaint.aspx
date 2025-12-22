<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ListComplaint.aspx.cs" Inherits="ListComplaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span style="color: #000000; font-size: 12pt;"><strong>View All Complaints</strong></span>
    <br /><br />
    <div class="pure-form pure-form-stacked" style="border: 1px; background-color: #EEEEEE;
        width: 100%">
        <div class="pure-g">
            <div class="pure-u-1 pure-u-md-1-3">
                Complaint ID/Complainant Name
                <asp:TextBox ID="txtSearchText" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Complaint Status
                <asp:DropDownList ID="ddlStatusSearch" runat="server" Width="280px">
                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                    <asp:ListItem Text="Complaint Created" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pending with QA" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Pending With Production" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Pending With Materials" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Pending With Despatch" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Pending With Unit Head" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Pending With Legal" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Partially closed by Unit Head" Value="19"></asp:ListItem>
                    <asp:ListItem Text="Complaint Closed by CRM" Value="20"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
            <br />
                <asp:Button ID="btnSearch" runat="server" Text="Show Results" OnClick="btnSearch_Click" class="pure-button pure-button-primary"
                    ValidationGroup="Submit" />
            </div>
        </div>
    </div>
    <hr width="100%; height:1px" />
    <div style="width: 100%">
        <asp:GridView ID="gvComplaint" runat="server" AllowPaging="True" EmptyDataText="No Complaint request(s) are Available for the above search criteria."
            Width="99%" CellPadding="3" BackColor="White" BorderColor="#404040" BorderStyle="Solid"
            BorderWidth="1px" OnPageIndexChanging="gvComplaint_PageIndexChanging" PageSize="25" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="   ">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" Text="View Detail" Font-Bold="True" CommandName="EditRow"
                            CommandArgument='<%#Eval("Complaint_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#FF0000" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
            <PagerStyle CssClass="GridPageNum"/>
            <HeaderStyle BackColor="#E89F65" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="WhiteSmoke" />
        </asp:GridView>
    </div>
</asp:Content>
