<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="eventItem.aspx.cs" Inherits="eventItem" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:DataList ID="dlNews" runat="server" RepeatColumns="1" CellPadding="10">
                            <ItemTemplate>
                             <div style="text-align: left">
                                <h3>
                                <%# DataBinder.Eval(Container.DataItem, "News_Title")%>                                  
                                </h3>                                    
                                </div>
                             <div style="text-align: left">                        
                                    <asp:Image ID="Image1" ImageUrl='<%# Bind("News_Picture_Name", "~/Gallery/News/{0}") %>'
                                        runat="server" />
                                </div>
                                <br />                               
                                  <div style="text-align: left">
                                <P><%# DataBinder.Eval(Container.DataItem, "News_Text")%></P>
                                </div>
                            </ItemTemplate>
                            <ItemStyle BorderColor="#FFFFFF" BorderStyle="none" BorderWidth="0px" HorizontalAlign="left"
                                VerticalAlign="top" />
                        </asp:DataList>
</p>
<br />
        <div style="text-align:right; width:800px;">
           <a href="news.aspx" id="lnkBack" runat="server" class="btn btn-success"><span class="glyphicon glyphicon-arrow-left"
                                        aria-hidden="true"></span> Back</a>
                                        </div>
</asp:Content>