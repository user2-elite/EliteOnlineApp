<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="adminGalleryAdd.aspx.cs" Inherits="adminGalleryAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Gallery</title>
   <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <link href="MessageBox/messagebox.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/pure-min.css">
    <!--[if lte IE 8]>
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/grids-responsive-old-ie-min.css">
    <![endif]-->
    <!--[if gt IE 8]><!-->
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/grids-responsive-min.css">
    <!--<![endif]-->
<link rel="stylesheet" href="Gallery/lightbox.css" type="text/css" media="screen" />
<script type="text/javascript" src="Gallery/prototype.js"></script>
<script type="text/javascript" src="Gallery/scriptaculous.js?load=effects"></script>
<script type="text/javascript" src="Gallery/lightbox.js"></script>
</head>
<body>
<form id="form1" runat="server">
<div>
 <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
                            border: 0px;">
                            <div class="pure-g">

<div id="divCategory" runat="server" visible="false">

<div class="pure-u-1 pure-u-md-1-1">
Category name (Maximum 50 Characters)<br />
 <asp:TextBox ID="txtCatTitle" runat="server" Width="525px" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCatTitle"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
</div>
 <div class="pure-u-1 pure-u-md-1-1">
                                    Description (Maximum 250 Characters)<br />
                                    <asp:TextBox ID="txtCatDescr" runat="server" Width="525px" TextMode="MultiLine" Rows="3"
                                        cols="25"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCatDescr"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
<asp:Button ID="btnAddCategory" runat="server" Text="Save" onclick="btnAddCategory_Click" class="pure-button pure-button-primary" Enabled="false"/>
<asp:Button ID="btnBack" runat="server" Text="Back to Gallery" onclick="btnBack_Click" class="pure-button pure-button-primary"/>
</div>

</div>

<!-- Gallery items -->
<div id="divgalleryItems" runat="server" visible="false">
<div class="pure-u-1 pure-u-md-1-1">
Gallery Category Name: <asp:DropDownList ID="ddlGallery" runat="server"><asp:ListItem Text="Gal 1" Value="1"></asp:ListItem></asp:DropDownList>

    <asp:LinkButton ID="lnkaddCategory" runat="server" 
        onclick="lnkaddCategory_Click" CausesValidation="false">Create New Gallery Category</asp:LinkButton>
    <br />

</div>
<div class="pure-u-1 pure-u-md-1-1">
Picture Title (Maximum 50 Characters)<br />
 <asp:TextBox ID="txtTitle" runat="server" Width="525px" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtTitle"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
</div>
 <div class="pure-u-1 pure-u-md-1-1">
                                    Description (Maximum 250 Characters)<br />
                                    <asp:TextBox ID="txtDesc" runat="server" Width="525px" TextMode="MultiLine" Rows="3"
                                        cols="25"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesc"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-2">
Upload Image<br /><asp:FileUpload ID="fileuploadimages" runat="server" />
</div>
<div class="pure-u-1 pure-u-md-1-2">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" class="pure-button pure-button-primary"/>
</div>
<div class="pure-u-1 pure-u-md-1-2">

<asp:DataList ID="dlImages" runat="server" RepeatColumns="3" CellPadding="10">
<ItemTemplate>
<a id="imageLink" href='<%# Eval("ImageName","~/Gallery/SlideImages/{0}") %>' title='<%#Eval("Description") %>' rel="lightbox[Brussels]" runat="server" >
<asp:Image ID="Image1" ImageUrl='<%# Bind("ImageName", "~/Gallery/SlideImages/{0}") %>' runat="server" Width="200" Height="100" />
</a>
<div style="text-align:left">
<br />
<B>Text here</B>
<br />
Description here
<br />
 </div>
</ItemTemplate>
<ItemStyle BorderColor="Brown" BorderStyle="dotted" BorderWidth="3px" HorizontalAlign="Center"
VerticalAlign="Bottom" />
</asp:DataList>

</div>

</div>
</div>
</div>
<br />
</div>
</form>
</body>
</html>

