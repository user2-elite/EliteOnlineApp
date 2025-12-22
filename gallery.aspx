<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <h3>Gallery</h3>
<p>
Click on gallery folder to view photos.
</p>    
    <link rel="stylesheet" href="Gallery/lightbox.css" type="text/css" media="screen" />
    <script type="text/javascript" src="Gallery/prototype.js"></script>
    <script type="text/javascript" src="Gallery/scriptaculous.js?load=effects"></script>
    <script type="text/javascript" src="Gallery/lightbox.js"></script>
    <style>
    tr.tableLineFirst td {font-size: 11px; color: black; background-color: #F0F0F0; font-weight: normal; padding: 5px;  }
tr.tableLineLast td {font-size: 11px; color: black; background-color: #E4E5E3; font-weight: normal; padding: 5px; }


    </style>
    <div>
        <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
            border: 0px;">
            <div class="pure-g">
                   <div class="pure-u-1 pure-u-md-1-1">
                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="4" CellPadding="10" CellSpacing="10" TabIndex="10"
                        CssClass="tableLineLast" ItemStyle-CssClass="tableLineFirst">
                            <ItemTemplate>
                            <div>
                                <a id="imageLink"  href='<%# Eval("GalleryGroupID","GalleryItem.aspx?id={0}") %>' title='<%#Eval("Gallery_Descr") %>' runat="server">
                                    <asp:Image ID="Image1" ImageUrl='~/Gallery/Images/3.jpg'
                                        runat="server"/>
                                </a>
                                <br />
                                <div style="text-align: center; font-size:13px; width:230px;">
									<h4>
                                        <%# DataBinder.Eval(Container.DataItem, "GalleryName")%></h4>                                
                                </div>
                                <div style="text-align: center; font-size:13px; ">
                               <%# DataBinder.Eval(Container.DataItem, "Gallery_Descr")%>
                                </div>
                                </div>
                            </ItemTemplate>                                  
                            <ItemStyle BorderColor="#F0F0F0" BorderStyle="solid"  BorderWidth="1px" HorizontalAlign="Center"
                                VerticalAlign="Bottom" />
                        </asp:DataList>
                    </div>
            </div>
        </div>
      <br />       
    </div>
   
</asp:Content>
