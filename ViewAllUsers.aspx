<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewAllUsers.aspx.cs" Inherits="ViewAllUsers" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Elite - Employee List</title>   
    <!-- custom css -->
    <link href="MessageBox/messagebox.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
		<![endif]-->
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/pure-min.css">
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/grids-responsive-min.css">
    <link href="Styles/layout.css" rel="Stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="left: 10px; top: 3px; margin-left: 10px;">
        <h2>
            View/Edit User Details</h2>
        <div class="alert-box success" id="divSuccess" runat="server" style="top: -3px;">
        </div>
        <div class="alert-box warning" id="divAlert" runat="server" style="top: -3x;">
        </div>
        <div class="alert-box notice" id="divNotice" runat="server" style="top: -3px;">
        </div>
        <div class="alert-box error" id="diverror" runat="server" style="top: -3px;">
        </div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 0px;
            border: 0px; background-color: #F0F0F0">
            <div class="pure-g">
                <div class="pure-u-1 pure-u-md-1-5">
                    Search By Employee Name
                    <asp:TextBox ID="txtEmpName" runat="server" Width="250px"></asp:TextBox></div>
                <div class="pure-u-1 pure-u-md-1-5">
                    <br />
                    <asp:Button ID="btnSearch" placeholder="Enter Employee name" runat="server" Text="Search"
                        class="pure-button pure-button-primary" Height="35px" OnClick="btnSearch_Click" />
                </div>                
                <div class="pure-u-1 pure-u-md-1-5">
                <br />
                    <asp:Button ID="btnExport2" OnClick="btnExport_Click" runat="server" Text="Export To Excel" class="pure-button pure-button-active" /></div>
                    <div class="pure-u-1 pure-u-md-1-5">
                    &nbsp;</div>
                <div class="pure-u-1 pure-u-md-1-5">
                    &nbsp;</div>
            </div>
        </div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 0px;
            border: 0px; background-color: #F0F0F0">
            <table width="100%" bgcolor="#E89F65" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                       <%-- <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID" DataSourceID="SqlDataSource1" EmptyDataText="No Users available"                            
                            EnableModelValidation="True" OnRowDataBound="gvUsers_RowDataBound" OnRowUpdating="gvUsers_RowUpdating"
                            Width="1400px" BackColor="White" BorderColor="#404040" BorderStyle="Solid" BorderWidth="1px" PageSize="50" CellPadding="5">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <RowStyle ForeColor="#000000" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle BackColor="maroon" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                            <Columns>
                                <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                                <asp:TemplateField HeaderText="Title">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="Title" runat="server" SelectedValue='<%# Bind("Title") %>'>
                                            <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                                            <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                                            <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpCode" HeaderText="EmpCode" SortExpression="EmpCode">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UID" HeaderText="UID" SortExpression="UID" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DOB" HeaderText="Date Of Birth(MM/DD/YYYY)" SortExpression="DOB">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="SystemType">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="SystemType" runat="server" SelectedValue='<%# Bind("SystemType") %>'>
                                            <asp:ListItem Text="Nil" Value="Nil"></asp:ListItem>
                                            <asp:ListItem Text="Laptop" Value="Laptop"></asp:ListItem>
                                            <asp:ListItem Text="Desktop" Value="Desktop"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hUID" runat="server" Value='<%# Bind("UID") %>'></asp:HiddenField>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Labelsystype" runat="server" Text='<%# Bind("SystemType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asset Name">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddAssetName" runat="server" DataSourceID="dsAssets" DataTextField="AssetName"
                                            DataValueField="AssetName" SelectedValue='<%# Bind("AssetName") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssetName" runat="server" Text='<%# Bind("AssetName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MouseAllotted">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="MouseAllotted" runat="server" SelectedValue='<%# Bind("MouseAllotted") %>'>
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMouse" runat="server" Text='<%# Bind("MouseAllotted") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ExtMemoryAllotted">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ExtMemoryAllotted" runat="server" SelectedValue='<%# Bind("ExtMemoryAllotted") %>'>
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMemory" runat="server" Text='<%# Bind("ExtMemoryAllotted") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DataCardAllotted">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DataCardAllotted" runat="server" SelectedValue='<%# Bind("DataCardAllotted") %>'>
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabeldataCard" runat="server" Text='<%# Bind("DataCardAllotted") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Area" HeaderText="Area">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlArea" runat="server" DataSourceID="dsArea" DataTextField="AreaName"
                                            DataValueField="AreaName" SelectedValue='<%# Bind("Area") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("Area") %>' ID="lblArea"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Department" HeaderText="Department">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" DataSourceID="dsDepartment" DataTextField="DepartmentName"
                                            DataValueField="DepartmentName" SelectedValue='<%# Bind("Department") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("Department") %>' ID="lblDepartment"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Location" HeaderText="Location">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlLocation" runat="server" DataSourceID="dsLocation" DataTextField="LocationDetails"
                                            DataValueField="LocationDetails" SelectedValue='<%# Bind("Location") %>'>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# Bind("Location") %>' ID="lblLocation"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Active?">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="Status" runat="server" SelectedValue='<%# Bind("Status") %>'>
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Createdon" HeaderText="Createdon" SortExpression="Createdon"
                                    ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <RowStyle ForeColor="#000000" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle BackColor="maroon" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRowStyle BackColor="WhiteSmoke" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="dsLocation" runat="server" ConnectionString="<%$ ConnectionStrings:Enterprise %>"
                            ProviderName="<%$ ConnectionStrings:Enterprise.ProviderName %>" SelectCommand="GetLocations"
                            SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="dsDepartment" runat="server" ConnectionString="<%$ ConnectionStrings:Enterprise %>"
                            ProviderName="<%$ ConnectionStrings:Enterprise.ProviderName %>" SelectCommand="GetDepartments"
                            SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="dsArea" runat="server" ConnectionString="<%$ ConnectionStrings:Enterprise %>"
                            ProviderName="<%$ ConnectionStrings:Enterprise.ProviderName %>" SelectCommand="GetAreas"
                            SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="dsAssets" runat="server" ConnectionString="<%$ ConnectionStrings:Enterprise %>"
                            ProviderName="<%$ ConnectionStrings:Enterprise.ProviderName %>" SelectCommand="IT_GetAssetNames"
                            SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Enterprise %>"
                            ProviderName="<%$ ConnectionStrings:Enterprise.ProviderName %>" SelectCommand="ViewAllUsers"
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="EmpName" SessionField="SelectedName"
                                    Type="String" />
                            </SelectParameters>                         
                        </asp:SqlDataSource>--%>

                           <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="True" Width="1400px" BackColor="White" BorderColor="#F0F0F0" BorderStyle="Outset" BorderWidth="1px" 
                           CellPadding="5" GridLines="Both" OnItemCommand="grid1_ItemCommand" OnItemDataBound="grid1_ItemDataBound" >
                                 <FooterStyle BackColor="White" ForeColor="#000066"  />
                            <ItemStyle ForeColor="#000000" HorizontalAlign="Left" BackColor="#F0F0F0" />                            
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle Font-Bold="true" ForeColor="#000000" HorizontalAlign="Left" BackColor="#E89F65" />
                            <AlternatingItemStyle BackColor="WhiteSmoke" />
                                <Columns>                                 
                                     <asp:TemplateColumn HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="pure-button pure-button-active" ForeColor=Red CausesValidation="false" CommandName="cmEdit"
                                                Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"UID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                                                       
                                </Columns>
                            </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
