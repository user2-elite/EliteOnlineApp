<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveQuotaView.aspx.cs" Inherits="LeaveQuotaView" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Elite - Quota List</title>   
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
            View Quota Details</h2>
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
            <div class="pure-g" style="width:900px">
                <div class="pure-u-1 pure-u-md-1-3" style="width:400px">
                    Search by: Employee Name (Leave blank for all employees)
                    <asp:TextBox ID="txtEmpName" runat="server" Width="250px"></asp:TextBox></div>

                <div class="pure-u-1 pure-u-md-1-3" style="width:200px">
                    <b>Quota Type</b><br />
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" Width="150px">
                        <asp:ListItem Text="--Choose--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Casual leave" Value="90"></asp:ListItem>
                        <asp:ListItem Text="Sick leave" Value="91"></asp:ListItem>
                        <asp:ListItem Text="ESI Leve" Value="92"></asp:ListItem>
                        <asp:ListItem Text="Earned Leave" Value="95"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="pure-u-1 pure-u-md-1-3">
                    <br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                        class="pure-button pure-button-primary" Height="35px" OnClick="btnSearch_Click" />
                </div>                
                <div class="pure-u-1 pure-u-md-1-1">
                <br />
                    <asp:Button ID="btnExport2" OnClick="btnExport_Click" runat="server" Text="Export To Excel" class="pure-button pure-button-active" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<a href="HRDashboards.aspx"><B><font color="blue"><u>Back To DashBoard</u></font></B></a>
                    </div>
                    
            </div>
        </div>

        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 0px;
            border: 0px; background-color: #F0F0F0">
            <table width="100%" bgcolor="#E89F65" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
             
                           <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#F0F0F0" BorderStyle="Outset" BorderWidth="1px" 
                           CellPadding="5" GridLines="Both">
                                 <FooterStyle BackColor="White" ForeColor="#000066"  />
                            <ItemStyle ForeColor="#000000" HorizontalAlign="Left" BackColor="#F0F0F0" />                            
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle Font-Bold="true" ForeColor="#000000" HorizontalAlign="Left" BackColor="#E89F65" />
                            <AlternatingItemStyle BackColor="WhiteSmoke" />
                                <Columns>                             
                                    <asp:BoundColumn DataField="Employee Id" HeaderText="Employee Id"></asp:BoundColumn>  
                                    <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AbsenseText" HeaderText="Type"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="StartDate" HeaderText="StartDate"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="EndDate" HeaderText="EndDate"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Quota" HeaderText="Quota"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Deduction" HeaderText="Deduction"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Balance" HeaderText="Balance"></asp:BoundColumn>                                                                     
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
