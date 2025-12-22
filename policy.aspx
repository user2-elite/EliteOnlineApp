<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true" CodeFile="policy.aspx.cs" Inherits="policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <h3>Policy Documents</h3>
<p>
Click on the folder icon to view all documents.
</p>    
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
                                <a id="imageLink"  href='<%# Eval("CategoryGroupID","PolicyItem.aspx?id={0}") %>' title='<%#Eval("Category_Descr") %>' runat="server">
                                    <asp:Image ID="Image1" ImageUrl='~/Gallery/Images/3.jpg'
                                        runat="server"/>
                                </a>
                                <br />
                                <div style="text-align: center; font-size:13px; width:230px;">
									<h4>
                                        <%# DataBinder.Eval(Container.DataItem, "CategoryName")%></h4>                                
                                </div>
                                <div style="text-align: center; font-size:13px; ">
                               <%# DataBinder.Eval(Container.DataItem, "Category_Descr")%>
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
