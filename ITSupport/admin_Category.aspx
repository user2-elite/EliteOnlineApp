<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="admin_Category.aspx.cs" Inherits="admin_Category" Title="Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div id="divHelpDesk" runat="server" style="position:absolute; top:1px;left:0px; font-size:14px; font-weight:bold; color:Maroon; width:100%; background-color:#F0F0F0; visibility:hidden;"></div>
    <table align="center" border="1" bordercolor="#E89F65" width="85%" 
        cellspacing="0" style="background-color:#F0F0F0">
        <tr>
            <td bgcolor="#E89F65" align="center" style="height: 22px">
                <span style="color: #ffffff; font-size: 12pt;"><strong>
                Manage Group</strong></span></td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="Silver"
                    BorderStyle="Solid" BorderWidth="1pt" CellPadding="4" DataKeyNames="GroupID"
                    DataSourceID="SqlDataSource1" EmptyDataText="No Categry configured Yet."
                    ForeColor="#333333" GridLines="None" Font-Size="Smaller" OnRowCommand="GridView1_RowCommand"
                    AllowSorting="True">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="GroupID" HeaderText="Group ID" ReadOnly="True" SortExpression="GroupID" />
                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NoofWorkingDays" HeaderText="No of Working Days" SortExpression="NoofWorkingDays" />
                        <asp:BoundField DataField="Startinghour" HeaderText="Starting Hour" SortExpression="Startinghour" />
                        <asp:BoundField DataField="Endinghour" HeaderText="Ending Hour" SortExpression="Endinghour" />
                        <asp:BoundField DataField="HelpdeskID" HeaderText="HelpdeskID" SortExpression="HelpdeskID"
                            Visible="False" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="UpdateGroup" runat="server" CausesValidation="false" CommandArgument='<%# Bind("GroupID") %>'
                                    Text="View/Edit" CommandName="editdetails"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Font-Size="Small" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle ForeColor="#333333" Font-Size="Small" />
                    <EditRowStyle BackColor="#999999" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" ForeColor="#284775" />
                </asp:GridView>
                <br />
                <table width="90%" align="center" cellspacing="0">
                    <tr>
                        <td align="left" colspan="2">
                            Group Name :
                            <asp:TextBox ID="GroupName" runat="server" MaxLength="25" Width="230px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="GroupName"
                                ErrorMessage="*" ValidationGroup="Group1"></asp:RequiredFieldValidator></td>
                        <td align="left" colspan="2">
                            Group Email :                             
                            <asp:TextBox ID="txtemailesaclation" runat="server" MaxLength="49" 
                                Width="293px"></asp:TextBox>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="txtemailesaclation" ErrorMessage="*" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 272px">
                            No. of Working Days
                            <asp:Label ID="LBerrornoofdays" runat="server" EnableViewState="False" Font-Names="Calibiri, Helvetica, sans-serif"
                                ForeColor="Red" Text="*" Visible="False"></asp:Label></td>
                        <td align="left" style="width: 246px">
                            <asp:RadioButtonList ID="Noofworkingdays" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                        <td align="left" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 272px">
                            Starting Hour :
                            <asp:DropDownList ID="StartingHour" runat="server">
                                <asp:ListItem>0.00</asp:ListItem>
                                <asp:ListItem>0.50</asp:ListItem>
                                <asp:ListItem>1.00</asp:ListItem>
                                <asp:ListItem>1.50</asp:ListItem>
                                <asp:ListItem>2.00</asp:ListItem>
                                <asp:ListItem>2.50</asp:ListItem>
                                <asp:ListItem>3.00</asp:ListItem>
                                <asp:ListItem>3.50</asp:ListItem>
                                <asp:ListItem>4.00</asp:ListItem>
                                <asp:ListItem>4.50</asp:ListItem>
                                <asp:ListItem>5.00</asp:ListItem>
                                <asp:ListItem>5.50</asp:ListItem>
                                <asp:ListItem>6.00</asp:ListItem>
                                <asp:ListItem>6.50</asp:ListItem>
                                <asp:ListItem>7.00</asp:ListItem>
                                <asp:ListItem>7.50</asp:ListItem>
                                <asp:ListItem>8.00</asp:ListItem>
                                <asp:ListItem>8.50</asp:ListItem>
                                <asp:ListItem>9.00</asp:ListItem>
                                <asp:ListItem>9.50</asp:ListItem>
                                <asp:ListItem>10.00</asp:ListItem>
                                <asp:ListItem>10.50</asp:ListItem>
                                <asp:ListItem>11.00</asp:ListItem>
                                <asp:ListItem>11.50</asp:ListItem>
                                <asp:ListItem>12.00</asp:ListItem>
                                <asp:ListItem>12.50</asp:ListItem>
                                <asp:ListItem>13.00</asp:ListItem>
                                <asp:ListItem>13.50</asp:ListItem>
                                <asp:ListItem>14.00</asp:ListItem>
                                <asp:ListItem>14.50</asp:ListItem>
                                <asp:ListItem>15.00</asp:ListItem>
                                <asp:ListItem>15.50</asp:ListItem>
                                <asp:ListItem>16.00</asp:ListItem>
                                <asp:ListItem>16.50</asp:ListItem>
                                <asp:ListItem>17.00</asp:ListItem>
                                <asp:ListItem>17.50</asp:ListItem>
                                <asp:ListItem>18.00</asp:ListItem>
                                <asp:ListItem>18.50</asp:ListItem>
                                <asp:ListItem>19.00</asp:ListItem>
                                <asp:ListItem>19.50</asp:ListItem>
                                <asp:ListItem>20.00</asp:ListItem>
                                <asp:ListItem>20.50</asp:ListItem>
                                <asp:ListItem>21.00</asp:ListItem>
                                <asp:ListItem>21.50</asp:ListItem>
                                <asp:ListItem>22.00</asp:ListItem>
                                <asp:ListItem>22.50</asp:ListItem>
                                <asp:ListItem>23.00</asp:ListItem>
                                <asp:ListItem>23.50</asp:ListItem>
                                <asp:ListItem>24.00</asp:ListItem>
                            </asp:DropDownList></td>
                        <td align="left" style="width: 246px">
                            Ending Hour :
                            <asp:DropDownList ID="EndingHour" runat="server">
                                <asp:ListItem>0.00</asp:ListItem>
                                <asp:ListItem>0.50</asp:ListItem>
                                <asp:ListItem>1.00</asp:ListItem>
                                <asp:ListItem>1.50</asp:ListItem>
                                <asp:ListItem>2.00</asp:ListItem>
                                <asp:ListItem>2.50</asp:ListItem>
                                <asp:ListItem>3.00</asp:ListItem>
                                <asp:ListItem>3.50</asp:ListItem>
                                <asp:ListItem>4.00</asp:ListItem>
                                <asp:ListItem>4.50</asp:ListItem>
                                <asp:ListItem>5.00</asp:ListItem>
                                <asp:ListItem>5.50</asp:ListItem>
                                <asp:ListItem>6.00</asp:ListItem>
                                <asp:ListItem>6.50</asp:ListItem>
                                <asp:ListItem>7.00</asp:ListItem>
                                <asp:ListItem>7.50</asp:ListItem>
                                <asp:ListItem>8.00</asp:ListItem>
                                <asp:ListItem>8.50</asp:ListItem>
                                <asp:ListItem>9.00</asp:ListItem>
                                <asp:ListItem>9.50</asp:ListItem>
                                <asp:ListItem>10.00</asp:ListItem>
                                <asp:ListItem>10.50</asp:ListItem>
                                <asp:ListItem>11.00</asp:ListItem>
                                <asp:ListItem>11.50</asp:ListItem>
                                <asp:ListItem>12.00</asp:ListItem>
                                <asp:ListItem>12.50</asp:ListItem>
                                <asp:ListItem>13.00</asp:ListItem>
                                <asp:ListItem>13.50</asp:ListItem>
                                <asp:ListItem>14.00</asp:ListItem>
                                <asp:ListItem>14.50</asp:ListItem>
                                <asp:ListItem>15.00</asp:ListItem>
                                <asp:ListItem>15.50</asp:ListItem>
                                <asp:ListItem>16.00</asp:ListItem>
                                <asp:ListItem>16.50</asp:ListItem>
                                <asp:ListItem>17.00</asp:ListItem>
                                <asp:ListItem>17.50</asp:ListItem>
                                <asp:ListItem>18.00</asp:ListItem>
                                <asp:ListItem>18.50</asp:ListItem>
                                <asp:ListItem>19.00</asp:ListItem>
                                <asp:ListItem>19.50</asp:ListItem>
                                <asp:ListItem>20.00</asp:ListItem>
                                <asp:ListItem>20.50</asp:ListItem>
                                <asp:ListItem>21.00</asp:ListItem>
                                <asp:ListItem>21.50</asp:ListItem>
                                <asp:ListItem>22.00</asp:ListItem>
                                <asp:ListItem>22.50</asp:ListItem>
                                <asp:ListItem>23.00</asp:ListItem>
                                <asp:ListItem>23.50</asp:ListItem>
                                <asp:ListItem>24.00</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="StartingHour"
                                ControlToValidate="EndingHour" ErrorMessage="Should be Grater" Operator="GreaterThan"
                                Type="Currency" ValidationGroup="Group1" Font-Size="Smaller"></asp:CompareValidator>
                        </td>
                        <td align="left" colspan="2" >
                            &nbsp;Status:
                            <asp:DropDownList ID="Status" runat="server" Enabled="False">
                                <asp:ListItem>Active</asp:ListItem>
                                <asp:ListItem>Inactive</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3">
&nbsp;</td>
                        <td align="left" style="font-size: 12pt">
                        &nbsp;</td>
                    </tr>
                    <tr >
                        <td align="left" colspan="2">
                            
                            &nbsp;</td>
                        <td align="left" colspan="2">
                            &nbsp;</td>
                    </tr>                    
                    <tr>
                        <td align="left" colspan="4" style="height: 19px; text-align: center;">
                            <asp:Button ID="AddWorkSchedule" runat="server" OnClick="AddWorkSchedule_Click" Text="Add"
                                ValidationGroup="Group1" Width="57px" />
                            </td>
                    </tr>
                </table>
                <asp:HiddenField ID="HiddenGroupID" runat="server" />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" 
        SelectCommand="GETWorkSchedulebyHelpDeskID" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="HelpDeskID" SessionField="HelpDeskID"
                Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </td>
        </tr>
        <!--
        <tr>
            <td align="left">
                <span style="font-size: 8pt; color: #ff3366"><em>
                Note : - While calculating the SLA, the
                    hours outside the starting and ending hour will be excluded. Also if No. of working
                    days is 5, Saturday and Sunday will be excluded and if it is 6 days Sunday will
                    be excludedd<br />
                <br />
                Only one email id should be put in Escalation email column. Make sure that mail 
                id is entered correctly.</em></span></td>
        </tr>
        -->
    </table>
    <br />
    <br />
    &nbsp; &nbsp; &nbsp;<br />
    <br />
</asp:Content>
