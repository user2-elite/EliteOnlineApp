<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaintenanceActivities.aspx.cs" Inherits="MaintenanceActivities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

 <div style="position:absolute; top:1px;left:200px; z-index:10; font-size:14px;">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkAllOpen" runat="server" Text="View Upcoming/Pending Activities" Font-Bold="True" OnClick="lnkOpenReq_Click"/>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="lnkClosedReq" runat="server" Text="View Closed Activities" Font-Bold="True" OnClick="lnkClosedReq_Click" />
    &nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="lnkAllPendingActivities" runat="server" Text="View All Activities" Font-Bold="True" OnClick="lnkAllReq_Click" />
    </div>
    <table align="center" width="99%">
    <asp:Panel ID="pnlOpenReq" runat="server" Visible="false" >
        <tr>
            <td align="center">
<br />
<h3>View upcoming maintenance activities for the next 30 Days.</h3>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="NextActivityInDays" 
                    DataSourceID="SqlDataSource1" EmptyDataText="No maintenance activity details available." 
                    Width="99%"
                    CellPadding="3" OnRowDataBound="GridView1_RowDataBound"
                    BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                         <asp:BoundField DataField="AssetName" HeaderText="Asset Name" ReadOnly="True" />
                        <asp:BoundField DataField="AssetDescription" HeaderText="Asset Description"/>
                        <asp:BoundField DataField="AssetType" HeaderText="Asset Type" />                        
                        <asp:BoundField DataField="NAME" HeaderText="Activity Responsible To" />
                        <asp:BoundField DataField="LastMntActivityUpdatedOn" HeaderText="Last Activity Updated On"/>
                        <asp:BoundField DataField="NextActivityInDays" HeaderText="Next Activity In Days" />                        
                        <asp:BoundField DataField="NextActivityDate" HeaderText="Next Activity Date" />                       
                        <asp:TemplateField HeaderText="Activity Details Update">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="Update" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#47AFAF" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" CancelSelectOnNullParameter="False" ></asp:SqlDataSource>
                </td>
        </tr>
    </asp:Panel>


 <asp:Panel ID="pnlAllReq" runat="server" Visible="false" >

 <tr><td>
         <div id="divDailyFilter" runat="server" style="color:#FFFFFF; background-color:#a5b8da; margin:1px; padding:10px;">        
  <asp:TextBox ID="txtFromDate3" runat="server" ValidationGroup="g1"></asp:TextBox>
                                        <img src="images/cal.png" id="img1" />
                                        (MM/dd/yyyy)<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFromDate3"
                                            ErrorMessage="Required"  ValidationGroup="g1"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="img1"
                                            TargetControlID="txtFromDate3" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>      

                                            <asp:TextBox ID="txtToDate3" runat="server" ValidationGroup="g1"></asp:TextBox>
                                        <img src="images/cal.png" id="img2" />
                                        (MM/dd/yyyy)<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtToDate3"
                                            ErrorMessage="Required" ValidationGroup="g1"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="img2"
                                            TargetControlID="txtToDate3" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
    &nbsp;<asp:Button ID="btnShowResult3" runat="server" onclick="btnShowResult3_Click" Text="Show Results"  ValidationGroup="g1"/>
    </div>
 </td></tr>
        <tr>
            <td align="center">
<br />
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="NextActivityInDays" 
                    DataSourceID="SqlDataSource3" EmptyDataText="No details available for the selected date range. Please select From & To date incase not selected." 
                    Width="99%" 
                    CellPadding="3" OnRowDataBound="GridView3_RowDataBound"
                    BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" OnRowCommand="GridView3_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="AssetName" HeaderText="Asset Name" ReadOnly="True" SortExpression="AssetName" />
                        <asp:BoundField DataField="AssetDescription" HeaderText="Asset Description" SortExpression="AssetDescription" />
                        <asp:BoundField DataField="AssetType" HeaderText="Asset Type" SortExpression="AssetType" />                        
                        <asp:BoundField DataField="NAME" HeaderText="Activity Responsible To" SortExpression="NAME" />
                        <asp:BoundField DataField="LastMntActivityUpdatedOn" HeaderText="Last Activity Updated On" SortExpression="LastMntActivityUpdatedOn" />
                        <asp:BoundField DataField="NextActivityInDays" HeaderText="Next Activity In Days" SortExpression="NextActivityInDays" />                        
                        <asp:BoundField DataField="NextActivityDate" HeaderText="Next Activity Date" SortExpression="NextActivityDate" />                       
                        <asp:TemplateField HeaderText="Activity Details Update">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="Update" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#47AFAF" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                </asp:GridView>                
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
                </td>
        </tr>
    </asp:Panel>
        <asp:Panel ID="pnlClosedReq" runat="server" Visible="false">

        <tr><td>
                 <div id="divDailyFilter2" runat="server" style="color:#FFFFFF; background-color:#a5b8da; margin:1px; padding:10px;">        
  <asp:TextBox ID="txtFromDate2" runat="server" ValidationGroup="g2"></asp:TextBox>
                                        <img src="images/cal.png" id="img12" />
                                        (MM/dd/yyyy)<asp:RequiredFieldValidator ID="rfv12" runat="server" ControlToValidate="txtFromDate2"
                                            ErrorMessage="Required" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="img12"
                                            TargetControlID="txtFromDate2" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>      

                                            <asp:TextBox ID="txtToDate2" ValidationGroup="g2" runat="server"></asp:TextBox>
                                        <img src="images/cal.png" id="img22" />
                                        (MM/dd/yyyy)<asp:RequiredFieldValidator ID="rfv22" runat="server" ControlToValidate="txtToDate2"
                                            ErrorMessage="Required"  ValidationGroup="g2"></asp:RequiredFieldValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender22" runat="server" PopupButtonID="img22"
                                            TargetControlID="txtToDate2" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
    &nbsp;<asp:Button ID="btnShowResult2" runat="server" onclick="btnShowResult2_Click" Text="Show Results"  ValidationGroup="g2"/>
    </div>
        </td></tr>
        <tr><td align="center">
        <br />
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="VolationDays"
                    DataSourceID="SqlDataSource2" EmptyDataText="No details available for the selected date range. Please select From & To date incase not selected." 
                    Width="99%" 
                    CellPadding="3" BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound">
                    <Columns>
                         <asp:BoundField DataField="AssetName" HeaderText="Asset Name" ReadOnly="True" SortExpression="AssetName" />
                        <asp:BoundField DataField="PreviousExecutionDate" HeaderText="Previous Execution Date" SortExpression="PreviousExecutionDate" />
                        <asp:BoundField DataField="PlannedExectionDate" HeaderText="Planned Exection Date" SortExpression="PlannedExectionDate" />                        
                        <asp:BoundField DataField="ActualExecutionDate" HeaderText="Actual Execution Date" SortExpression="ActualExecutionDate" />                       
                        <asp:BoundField DataField="ExecutionDescription" HeaderText="Execution Description" SortExpression="ExecutionDescription" />                        
                        <asp:BoundField DataField="NAME" HeaderText="Activity Executed By" SortExpression="NAME" />
                        <asp:TemplateField HeaderText="Activity Details Update" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="Update" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#47AFAF" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#E89F65" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" CancelSelectOnNullParameter="False" ></asp:SqlDataSource>
        </td></tr>
        </asp:Panel>
    </table>
</asp:Content>

