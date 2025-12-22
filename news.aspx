<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true"
    CodeFile="news.aspx.cs" Inherits="news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        News & Events</h3>
    <p>
        <asp:DataList ID="dlNews" runat="server" RepeatColumns="1" CellPadding="5" CellSpacing="5" Width="75%">
            <ItemTemplate>
                <div style="text-align: left">
                <b>
                        <%# DataBinder.Eval(Container.DataItem, "News_Title")%>
                    </b>
                </div>
                <div style="text-align: left">
                    <asp:Image ID="Image1" ImageUrl='<%# Bind("News_Picture_Name", "~/Gallery/News/{0}") %>'
                        runat="server" Height="200px" />
                    </a>
                </div>
                <div style="text-align: left">
                    <p>
                        <%# DataBinder.Eval(Container.DataItem, "News_Text")%></p>
                </div>
                <div style="text-align: left">
                    <a id="imageLink" href='<%# Eval("ID","eventItem.aspx?ID={0}") %>' title='' runat="server">
                        More Information</a>
                </div>
              <div>
                    <hr style="height: 1px; color: Maroon; width:100%;" />
                </div>
            </ItemTemplate>
            <ItemStyle BorderColor="#FFFFFF" BorderStyle="solid" BorderWidth="0px" HorizontalAlign="left"
                VerticalAlign="top" />
        </asp:DataList>
    </p>
    <br />
</asp:Content>
