<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovePage.aspx.cs" Inherits="ApprovePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/moment.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" /
    <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />

     <script type="text/javascript">
         function openWindow(SlNo) {
             window.open('PopUpPage.aspx?Code=' + SlNo, 'open_window', ' width=640, height=480, left=0, top=0');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMessage" runat="server" Text=" "></asp:Label>
        <br />
        <h3>Waiting for Approval</h3>
        <asp:GridView ID="GridView1" runat="server" Width="80%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="SlNo" >
            <Columns>
                <asp:TemplateField HeaderText="ViewDetails">
                    <ItemTemplate>
                        <a href="#" onclick='openWindow("<%# Eval("SlNo") %>");'>View Details</a>
                    </ItemTemplate>
                </asp:TemplateField>
		<asp:BoundField DataField="EmpName" HeaderText="Employee Name" ReadOnly="True" SortExpression="EmpName" />
                <asp:BoundField DataField="AbsenseType" HeaderText="AbsenseType" ReadOnly="True" SortExpression="AbsenseType" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"/>
                <asp:BoundField DataField="ODDayType" HeaderText="ODDayType" SortExpression="ODDayType" />
                <asp:BoundField DataField="TravelAdvance" HeaderText="Travel Advance" SortExpression="TravelAdvance"/>
                <asp:BoundField DataField="Description" HeaderText="Description"/>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:TextBox ID="txtcomments" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>                 
            </Columns>
        </asp:GridView>
        <h4 id="ApproveNoRecords" runat="server" visible="false">There are no data records to display.</h4>
        <br />

        <div class="row">
            <div class="col-sm-4 col-md-4">
            </div>
            <div class="col-sm-3 col-md-2">
                <asp:Button ID="Button1" runat="server" Text="Approve" class="form-control btn-success" OnClick="Button1_Click" />
            </div>
            <div class="col-sm-3 col-md-2">

                <asp:Button ID="Button2" runat="server" Text="Reject" class="form-control" OnClick="Button2_Click" />
            </div>
        </div>
        <hr />
<h3>Waiting for Cancellation Approval</h3>
        <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="SlNo" >
            <Columns>
                <asp:TemplateField HeaderText="ViewDetails">
                    <ItemTemplate>
                        <a href="#" onclick='openWindow("<%# Eval("SlNo") %>");'>View Details</a>
                    </ItemTemplate>
                </asp:TemplateField>
		<asp:BoundField DataField="EmpName" HeaderText="Employee Name" ReadOnly="True" SortExpression="EmpName" />
                <asp:BoundField DataField="AbsenseType" HeaderText="AbsenseType" ReadOnly="True" SortExpression="AbsenseType" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"/>
                <asp:BoundField DataField="ODDayType" HeaderText="ODDayType" SortExpression="ODDayType" />
                <asp:BoundField DataField="TravelAdvance" HeaderText="TravelAdvance" SortExpression="TravelAdvance"/>
                <asp:BoundField DataField="Description" HeaderText="Description"/>                          
            </Columns>
        </asp:GridView>
        <h4 id="CancelReqNoRecords" runat="server" visible="false">There are no data records to display.</h4>
        <br />
        <hr />
        <h3>Aproval History</h3> <h6>(Last 90 days records will be available here.)</h6>
        <div role="tabpanel" class="tab-pane" id="ViewPrint" style="max-height:500px; overflow:auto;">
        <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
            AutoGenerateColumns="False" DataKeyNames="SlNo"
            EmptyDataText="There are no data records to display.">
            <Columns>
		<asp:BoundField DataField="EmpName" HeaderText="Employee Name" ReadOnly="True" SortExpression="EmpName" />
                <asp:BoundField DataField="AbsenseType" HeaderText="AbsenseType" ReadOnly="True" SortExpression="AbsenseType" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"/>
                <asp:BoundField DataField="ODDayType" HeaderText="ODDayType" SortExpression="ODDayType" />
                <asp:BoundField DataField="TravelAdvance" HeaderText="TravelAdvance" SortExpression="TravelAdvance"/>
                <asp:BoundField DataField="Description" HeaderText="Description" />                
            </Columns>
        </asp:GridView>
        </div>
<h4 id="previousReqNoRecords" runat="server" visible="false">There are no data records to display.</h4>
        <br />
        <hr />
    </form>
</body>
</html>
