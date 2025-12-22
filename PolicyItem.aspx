<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="PolicyItem.aspx.cs" Inherits="PolicyItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Policy Documents</h3>
        <p>
Click on the documents to view.
</p>   	 
    <div>
        <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
            border: 0px;">
            <div class="pure-g">
                <div id="divPolicyItems">                  
                    <div class="pure-u-1 pure-u-md-1-1">
                          <%--  <div style="text-align:right; width:800px;">
           <a href="policy.aspx" id="A1" runat="server" class="btn btn-success"><span class="glyphicon glyphicon-arrow-left"
                                        aria-hidden="true"></span> Back</a>
                                        </div>--%>
                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="1" CellPadding="10">
                            <ItemTemplate>
                            <div style="padding:5px; margin:5px;Width:100%">
                            <img src="Gallery/word.jpg" border="0" />
                                <a id="imageLink" href='<%# Eval("FileName","~/Gallery/PolicyDocs/{0}") %>' title='<%#Eval("Title") %>' runat="server" target="_blank">
                                    <%# DataBinder.Eval(Container.DataItem, "Title")%>
                                </a>
                                <br />
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </div>    								
                                <HR />
                            </ItemTemplate>                                                                              
                            <ItemStyle BorderColor="#FFFFFF" BorderStyle="solid"  BorderWidth="0px" HorizontalAlign="left"
                                VerticalAlign="Bottom" Width="100%" />
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div style="text-align:right; width:800px;">
           <a href="Javascript:history.back(-1);" id="lnkBack" runat="server" class="btn btn-success"><span class="glyphicon glyphicon-arrow-left"
                                        aria-hidden="true"></span> Back</a>
                                        </div>
    </div>
</asp:Content>
