<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="text-align: left; width: 100%">
<div style="vertical-align:top; text-align:left; position:absolute; top:0px; left: 0px; font-weight:bold; color:#000000; font-size:14px; background-color:#fff8c4; width:100%; height:20px;"  runat="server" id="divWelcome"></div>
<br /><br />
        <div class="alert-box success" id="divSuccess" runat="server"></div>
        <div class="alert-box warning" id="divAlert" runat="server"></div>
        <div class="alert-box notice" id="divNotice" runat="server"></div>
        <div class="alert-box error" id="diverror" runat="server"></div>
        <br /><br /><br /><br /><br />

<asp:Panel ID="pnlMyWorlist" runat="server" GroupingText="My Worklist">
<asp:Label ID="lblNumRows" runat="server"></asp:Label>
 <asp:GridView ID="gvComplaint" runat="server" Width="99%" 
                    CellPadding="3" BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText=" ">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="Perform Action" Font-Bold="True" CommandName="EditRow" CommandArgument='<%#Eval("ComplaintID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#47AFAF" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                </asp:GridView>
</asp:Panel>
</div>

</asp:Content>

