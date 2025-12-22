<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="KBView.aspx.cs" Inherits="KBView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
<style type="text/css">

.KBAdd{
border:2px solid #819BCB;-webkit-box-shadow: #A5B8DA 0px 21px 0px inset;-moz-box-shadow: #A5B8DA 0px 21px 0px inset; box-shadow: #A5B8DA 0px 21px 0px inset; -webkit-border-radius: 3px; -moz-border-radius: 3px;border-radius: 3px;font-size:12px;font-family:arial, helvetica, sans-serif; padding: 10px 10px 10px 10px; text-decoration:none; display:inline-block;text-shadow: -1px -1px 0 rgba(0,0,0,0.3);font-weight:bold; color: #FFFFFF;
 background-color: #a5b8da; background-image: -webkit-gradient(linear, left top, left bottom, from(#a5b8da), to(#7089b3));
 background-image: -webkit-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -moz-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -ms-linear-gradient(top, #a5b8da, #7089b3);
 background-image: -o-linear-gradient(top, #a5b8da, #7089b3);
 background-image: linear-gradient(to bottom, #a5b8da, #7089b3);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#a5b8da, endColorstr=#7089b3);
}

.KBAdd:hover{
 border:2px solid #819BCB;
 background-color: #819bcb; background-image: -webkit-gradient(linear, left top, left bottom, from(#819bcb), to(#536f9d));
 background-image: -webkit-linear-gradient(top, #819bcb, #536f9d);
 background-image: -moz-linear-gradient(top, #819bcb, #536f9d);
 background-image: -ms-linear-gradient(top, #819bcb, #536f9d);
 background-image: -o-linear-gradient(top, #819bcb, #536f9d);
 background-image: linear-gradient(to bottom, #819bcb, #536f9d);filter:progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr=#819bcb, endColorstr=#536f9d);
}
</style>
<div style="text-align:left;color:maroon; font-weight:bold; position:absolute;top:110px;right:18px;"><asp:HyperLink ID="lnkAddNew" runat="server" NavigateUrl="LogOff.aspx" CssClass="KBAdd" Text="Log Off"></asp:HyperLink></div>
<div style="border: 1px; background-color: #EEEEEE; width: 100%; padding:5px;">
 <div class="pure-form pure-form-stacked" style="border: 1px;width: 80%">
        <div class="pure-g">
  <div class="pure-u-1 pure-u-md-1-3">
                Product Item<br />
                <asp:DropDownList ID="ddProdCategory" runat="server" Width="250px">
                </asp:DropDownList>                
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Poduction Unit<br />
                <asp:DropDownList ID="ddCompanyUnit" runat="server" Width="250px">
                </asp:DropDownList>               
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Complaint Type<br />
                <asp:DropDownList ID="ddTypeOfComplaint" runat="server" Width="250px">
                </asp:DropDownList>
            </div>
            <div class="pure-u-1 pure-u-md-1-3" >
                Enter KeyWord
                <asp:TextBox ID="txtSearchText" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
            </div>
            <div class="pure-u-1 pure-u-md-1-3">
                Search Type
                <asp:DropDownList ID="ddlSearchType" runat="server" Width="170px">
                    <asp:ListItem Text="Contains" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Exactly" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Begin With" Value="2"></asp:ListItem>
                    <asp:ListItem Text="End With" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="pure-u-1 pure-u-md-1-3" style="position:absolute;right:220px; top:98px;">
            <br />
                <asp:Button ID="btnSearch" runat="server" Text="Show Results" OnClick="btnSearch_Click" class="pure-button pure-button-primary"
                    ValidationGroup="Submit" />
            </div>
        </div>
    </div>
    </div>
    <hr width="100%; height:1px" />

    <div>
    <b><U>Search Results(s)</U></b>
    <br /><br />
    </div>
    <div id="divSearchResults" runat="server" style="width:100%;" />
</asp:Content>

