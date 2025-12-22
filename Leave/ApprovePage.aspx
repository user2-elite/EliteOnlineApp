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
        <h3>Waiting for Leave Approval</h3>
        <div style="max-height:400px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="SlNo" >
            <Columns>
                <asp:TemplateField HeaderText="ViewDetails">
                    <ItemTemplate>
                        <a href="#" onclick='openWindow("<%# Eval("SlNo") %>");'>View Details</a>
                    </ItemTemplate>
                </asp:TemplateField>
		<asp:BoundField DataField="EmpName" HeaderText="Employee Name" ReadOnly="True" SortExpression="EmpName" />
                <asp:BoundField DataField="AbsenseText" HeaderText="AbsenseText" ReadOnly="True" SortExpression="AbsenseText" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"/>
                <asp:BoundField DataField="LeaveDayType" HeaderText="LeaveDayType" SortExpression="LeaveDayType" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
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
        </div>
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
        <div style="max-height:400px; overflow:auto;">

        <asp:GridView ID="GridView3" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="SlNo" >
            <Columns>
                <asp:TemplateField HeaderText="ViewDetails">
                    <ItemTemplate>
                        <a href="#" onclick='openWindow("<%# Eval("SlNo") %>");'>View Details</a>
                    </ItemTemplate>
                </asp:TemplateField>
		<asp:BoundField DataField="EmpName" HeaderText="Employee Name" ReadOnly="True" SortExpression="EmpName" />
                <asp:BoundField DataField="AbsenseText" HeaderText="AbsenseText" ReadOnly="True" SortExpression="AbsenseText" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"/>
                <asp:BoundField DataField="LeaveDayType" HeaderText="LeaveDayType" SortExpression="LeaveDayType" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />                          
            </Columns>
        </asp:GridView>
        </div>
        <h4 id="CancelReqNoRecords" runat="server" visible="false">There are no data records to display.</h4>
        <br />
        <hr />
        <h3>Aproval History</h3> <h6>(Last 90 days records will be available here.)</h6>
		<div role="tabpanel" class="tab-pane" id="ViewPrint" style="max-height:400px; overflow:auto;">
        <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" 
            AutoGenerateColumns="False" DataKeyNames="SlNo"
            EmptyDataText="There are no data records to display.">
            <Columns>
		<asp:BoundField DataField="EmpName" HeaderText="Employee Name" ReadOnly="True" SortExpression="EmpName" />
                <asp:BoundField DataField="AbsenseText" HeaderText="AbsenseText" ReadOnly="True" SortExpression="AbsenseText" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate"/>
                <asp:BoundField DataField="LeaveDayType" HeaderText="LeaveDayType" SortExpression="LeaveDayType" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />                
            </Columns>
        </asp:GridView>
		</div>
<h4 id="previousReqNoRecords" runat="server" visible="false">There are no data records to display.</h4>
        <br />
        <hr />
        <!--
        <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>

        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Modal Header</h4>
                    </div>
                    <div class="modal-body">
                        <p>Some text in the modal.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        -->
    </form>
</body>
</html>
