<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewAllRequests.aspx.cs" Inherits="ViewAllRequests" Title="View All Request" ValidateRequest="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <style>
        .black_overlay1{
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index:1000;
            -moz-opacity: 0.8;
            opacity:.50;
            filter: alpha(opacity=50); shadow(color:#8f8f8f, strength:13, direction:135);
        }
        .black_overlay{
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index:99;
            -moz-opacity: 0.8;
            opacity:.50;
            filter: alpha(opacity=50);
        }
        .white_content {
            display: none;
            position: absolute;
             top: -25px;
            left: -5px;
            width: 100%;
            height: 100%;
            padding: 15px;
            z-index:100;
            border: 10px solid #B94629;
             background-color: #E89F65;
            overflow:hidden;
        }       
    </style>
<script language="Javascript">
    function OpenWindow() {
        document.getElementById('light').style.display = 'block';
        document.getElementById('fade').style.display = 'block';
    }

    function WindowClose() {
        document.getElementById('light').style.display = 'none';
        document.getElementById('fade').style.display = 'none';
        //document.location.reload(true);
        window.location.href = 'ViewAllRequests.aspx';
    }
</script>
<script language="javascript" type="text/javascript">
<!--

function Button1_onclick() {
    window.location.href = 'ViewAllRequests.aspx';
}

// -->
</script>

 <div id="fade" class="black_overlay"></div>    
      <div id="light" class="white_content">      
      <div style="width:100%; vertical-align:top; text-align:left; position:absolute; top:-40px; left: -20px;">
      <div style="width:100%; vertical-align:top; text-align:right;top:45px;right:30px; position:absolute;"><a href = "javascript:void(0)" onclick = "javascript:WindowClose();"><img src="Images/c1.gif" border="0" /></a></div> 
      <iframe width="102%" height="560px" id="newWin" runat="server" scrolling="auto" frameborder="0" style="padding:0px;"></iframe>
      </div>
       </div>                

       <div id="div1" style="position:absolute; top:1px;left:0px; font-size:14px; font-weight:bold; color:Maroon; width:100%; background-color:#F0F0F0; z-index:1;">
    <div id="divHelpDesk" runat="server" style="position:absolute; top:1px;left:0px; font-size:14px; font-weight:bold; color:Maroon; width:100%; background-color:#F0F0F0; z-index:1; visibility:hidden;"></div>
    <div style="position:absolute; top:1px;left:200px; z-index:10">
    <!--
    <input id="RefreshButton" type="button" value="Get Latest Requests" language="javascript" onclick="return Button1_onclick()" />-->
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="lnkOpenReq" runat="server" Text="View Open Requests" Font-Bold="True" OnClick="lnkOpenReq_Click"/>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="lnkClosedReq" runat="server" Text="View Closed Requests" Font-Bold="True" OnClick="lnkClosedReq_Click" />
    </div>
    </div>
    <table align="center" width="99%">
    <asp:Panel ID="pnlOpenReq" runat="server" Visible="false" >
        <tr>
            <td align="center">
<br />
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="RequestID,AssingedTo,ExpectedResolutionDtTime,RequestStatus" 
                    DataSourceID="SqlDataSource1" EmptyDataText="No requests available." 
                    Width="99%"
                    CellPadding="3" OnRowDataBound="GridView1_RowDataBound"
                    BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" PageSize="25" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="RequestID" HeaderText="Request ID" ReadOnly="True" SortExpression="RequestID" />
                        <asp:BoundField DataField="RegisteredBy" HeaderText="Requester UserID" SortExpression="RegisteredBy" />
                        <asp:BoundField DataField="EmpName" HeaderText="Requester Name" SortExpression="EmpName" />                        
                        <asp:BoundField DataField="NAME" HeaderText="Responded By" SortExpression="NAME" />
                        <asp:BoundField DataField="ExpectedResolutionDtTime" HeaderText="Expected Closure Date" SortExpression="ExpectedResolutionDtTime" />
                        <asp:BoundField DataField="RequestSubject" HeaderText="Request Subject" SortExpression="RequestSubject" />                        
                        <asp:BoundField DataField="StatusName" HeaderText="Request Status" SortExpression="StatusName" />                       
                        <asp:TemplateField HeaderText="Request Details">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="View" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("RequestID") %>' />
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
                <!--
                <asp:Label ID="RequestViolationColorLB" runat="server" BackColor="#BD3632"
                    ForeColor="#C9C299" Width="10px" Text=""></asp:Label><em><span><span style="color: #4C3327">* legend indicates, resolution time violated.</span></span></em>
                    -->
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" CancelSelectOnNullParameter="False" 
                     ></asp:SqlDataSource>
                <asp:HiddenField ID="RequestDisplayType" runat="server" />


                </td>
        </tr>
    </asp:Panel>
        <asp:Panel ID="pnlClosedReq" runat="server" Visible="false">
        <tr><td align="center">
        <br />
         <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="RequestID,AssingedTo,ExpectedResolutionDtTime,RequestStatus"
                    DataSourceID="SqlDataSource2" EmptyDataText="No requests available." 
                    Width="99%"
                    CellPadding="3" BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" PageSize="25" OnRowCommand="GridView2_RowCommand" OnRowDataBound="GridView2_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="RequestID" HeaderText="Request ID" ReadOnly="True" SortExpression="RequestID" />
                        <asp:BoundField DataField="RegisteredBy" HeaderText="Requester User ID" SortExpression="RegisteredBy" />
                        <asp:BoundField DataField="EmpName" HeaderText="Requester Name" SortExpression="EmpName" />                                                
                        <asp:BoundField DataField="RequestSubject" HeaderText="Subject" SortExpression="RequestSubject" /> 
                        <asp:BoundField DataField="ClosureDate" HeaderText="Closure Date" SortExpression="ClosureDate" />
                        <asp:BoundField DataField="NAME" HeaderText="Request Closd By" SortExpression="NAME" />                                                                        
                        <asp:BoundField DataField="StatusName" HeaderText="Request Status" SortExpression="StatusName" />                       
                        <asp:TemplateField HeaderText="Request Details">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="View" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("RequestID") %>' />
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
    <br />
</asp:Content>
