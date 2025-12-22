<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true"
    CodeFile="MGMTTalk.aspx.cs" Inherits="MGMTTalk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Management Talk</h3>
    <p>
        <asp:DataList ID="dlTalk" runat="server" RepeatColumns="1" CellPadding="5" CellSpacing="5">
            <ItemTemplate>
                <div style="text-align: left">
                    
                        <h4 style="color:Maroon"><b><%# DataBinder.Eval(Container.DataItem, "MGMT_Title")%></b></h4>
                </div>
                <div style="text-align: left">
                    <h4>Written By:
                        <%# DataBinder.Eval(Container.DataItem, "MGMT_Author")%>
                    </h4>
                </div>
                <div style="text-align: justify;">
                    <asp:Image ID="Image1" ImageUrl='<%# Bind("MGMT_Photo_Name", "~/Gallery/Management/{0}") %>'
                        runat="server" Height="130px" />
                    </a>
                </div>
                <div style="text-align: left">
                    <p>
                        <%# DataBinder.Eval(Container.DataItem, "MGMT_Text")%></p>
                </div>
                <div>
                    <hr style="height: 1px; color: Maroon" />
                </div>
            </ItemTemplate>
            <ItemStyle BorderColor="#FFFFFF" BorderStyle="solid" BorderWidth="0px" HorizontalAlign="left"
                VerticalAlign="top" />
        </asp:DataList>
    </p>
</asp:Content>
