<%@ Page Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ViewOnBoardingDashboard.aspx.cs" Inherits="ViewOnBoardingDashboard" Title="Employee Onboarding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

         <asp:HyperLink runat="server" id="linkNew" href="NewUserOnBoarding.aspx" Text="Add New Onboarding User"></asp:HyperLink>
      &nbsp; | &nbsp; <asp:HyperLink runat="server" id="linkView" href="ViewOnBoardingDashboard.aspx" Text="Onboarding dashboard"></asp:HyperLink>
    
    <div style="left: 10px; top: 3px; margin-left: 10px;">
        <h2>
            View/Edit User Details</h2>
    </div>
        
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 0px;
            border: 0px; background-color: #F0F0F0">
            <div class="pure-g">
                <div class="pure-u-1 pure-u-md-1-4">
                    Search By Name <br />
                    <asp:TextBox ID="txtEmpName" runat="server" Width="250px"></asp:TextBox>&nbsp;
                </div>
                 <div class="pure-u-1 pure-u-md-1-4">
        Year<br />
        <asp:DropDownList ID="ddlSYear" runat="server">
        </asp:DropDownList>&nbsp;
                     </div>
                 <div class="pure-u-1 pure-u-md-1-4">
        Month<br />
        <asp:DropDownList ID="ddlSMonth" runat="server">
        </asp:DropDownList>&nbsp;</div>
                <div class="pure-u-1 pure-u-md-1-4">
                    <br />
                    <asp:Button ID="btnSearch" placeholder="Enter Name" runat="server" Text="Search"
                        class="pure-button pure-button-primary" Height="35px" OnClick="btnSearch_Click" />
                </div>                 
            </div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 0px;
            border: 0px; background-color: #F0F0F0">
            <table width="100%" bgcolor="#E89F65" cellpadding="3" cellspacing="3">
                <tr>
                    <td>
                           <asp:DataGrid ID="grid1" runat="server" AutoGenerateColumns="True" Width="1000px" BackColor="White" BorderColor="#F0F0F0" BorderStyle="Outset" BorderWidth="1px" 
                           CellPadding="10" CellSpacing="10" GridLines="Both" OnItemCommand="grid1_ItemCommand">
                                 <FooterStyle BackColor="White" ForeColor="#000066"  />
                            <ItemStyle ForeColor="#000000" HorizontalAlign="Left" BackColor="#F0F0F0" />                            
                            <PagerStyle CssClass="GridPageNum" />
                            <HeaderStyle Font-Bold="true" ForeColor="#000000" HorizontalAlign="Left" BackColor="#E89F65" />
                            <AlternatingItemStyle BackColor="WhiteSmoke" />
                                <Columns>                                 
                                     <asp:TemplateColumn HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="pure-button pure-button-active" ForeColor=Red CausesValidation="false" CommandName="cmEdit"
                                                Text="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>                                                                       
                                </Columns>
                            </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="alert-box success" id="divSuccess" runat="server" style="top: -3px;">
        </div>
        <div class="alert-box warning" id="divAlert" runat="server" style="top: -3x;">
        </div>
        <div class="alert-box notice" id="divNotice" runat="server" style="top: -3px;">
        </div>
        <div class="alert-box error" id="diverror" runat="server" style="top: -3px;">
        </div>
</asp:Content>
