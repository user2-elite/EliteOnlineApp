<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="galleryItem.aspx.cs" Inherits="galleryItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="Gallery/lightbox.css" type="text/css" media="screen" />
    <script type="text/javascript" src="Gallery/prototype.js"></script>
    <script type="text/javascript" src="Gallery/scriptaculous.js?load=effects"></script>
    <script type="text/javascript" src="Gallery/lightbox.js"></script>
    <h3>
        Gallery - Photos</h3>
        <p>
Click on the photos to get its original size.
</p>   	   
    <div>
        <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
            border: 0px;">
            <div class="pure-g">
                <div id="divgalleryItems">                  
                    <div class="pure-u-1 pure-u-md-1-1">
                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="4" CellPadding="10">
                            <ItemTemplate>
                            <div style="padding:5px; margin:5px;">
                                <a id="imageLink" href='<%# Eval("ImageName","~/Gallery/SlideImages/{0}") %>' title='<%#Eval("Title") %>'
                                    rel="lightbox[Brussels]" runat="server">
                                    <asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Gallery/SlideImages/{0}") %>'
                                        runat="server" Width="250" Height="150" />
                                </a>
                                </div>
                                <div style="text-align: center; font-size:13px; width:230px;">
										<b>
                                        <%# DataBinder.Eval(Container.DataItem, "Title")%>
										</b>
                                </div>   
								<div style="text-align: center; font-size:13px; ">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </div>    								
                            </ItemTemplate>                                                  
                            <ItemStyle BorderColor="#CCCCCC" BorderStyle="solid"  BorderWidth="1px" HorizontalAlign="Center"
                                VerticalAlign="Bottom" />
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
