<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ODPage.aspx.cs" Inherits="ODPage" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.js"></script>
    <script src="Scripts/moment.js"></script>
    <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/bootstrap-datetimepicker.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script>
        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'DD-MMM-YYYY',
                ignoreReadonly: false
            });
        });
        $(function () {
            $('#datetimepicker2').datetimepicker({
                format: 'DD-MMM-YYYY',

                ignoreReadonly: true,
            });
        });
    </script>

    <style>
        hr {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" id="pnlEmpty" runat="server">
            <div class="panel panel-default">
                <h3>Manager details not updated in system. To proceed OD request, please contact your HR/IT support team to update manager information.</h3>
        </div></div>
        <div class="container"  id="pnlRequest" runat="server">
            <div class="panel panel-default">
                <div class="panel-heading">On Duty/holiday working Details <h5 style="color:blue">If you are on official duty but not in office please log below:</h5></div>
                <div class="panel-body">

                    <div role="tabpanel">
                        <ul id="myTab" class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#Create" aria-controls="profile" role="tab" data-toggle="tab">Create</a></li>
                            <li role="presentation"><a href="#ViewPrint" aria-controls="profile" role="tab" data-toggle="tab">On Duty History </a></li>
                        </ul>
                    </div>
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="Create">
                                     <asp:Label ID="lblMessageHeader" runat="server" Text=" "></asp:Label>
                                        <br />
                            <asp:Label ID="Label2" runat="server" Text="OD Details" ForeColor="#000066" Font-Bold="True" Font-Underline="True"></asp:Label>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="userid">On Duty Type</label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" AppendDataBoundItems="true"
                                        DataTextField="AbsenseText" DataValueField="AbsenseText">
                                        <asp:ListItem Value="0"><--Select--></asp:ListItem>
                                    </asp:DropDownList>
                                </div><span style="color:red">*</span>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="userid">Time </label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Full Day</asp:ListItem>
                                        <asp:ListItem Value="2">First Half</asp:ListItem>
                                        <asp:ListItem Value="3">Second Half</asp:ListItem>
                                    </asp:DropDownList>
                                </div> <span style="color:red">*</span>                               
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="userid">Start Date </label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker1' runat="server">
                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" Style="background-color: white"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div><span style="color:red">*</span>
                            </div>
                            <div class="row" style="margin-top:10px;" id="divEndDate" runat="server">
                                <div class="col-sm-4 col-md-4" >
                                    <label for="userid">End Date </label>
                                </div>
                                <div class="col-sm-6 col-md-6" >
                                    <div class="form-group">
                                        <div class='input-group date' id='datetimepicker2' runat="server">
                                            <asp:TextBox ID="TextBox2" runat="server" class="form-control" Style="background-color: white"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div><span style="color:red">*</span>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="userid">Manager Name & Code</label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:Label ID="TextBox5" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="userid">Description </label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:TextBox ID="TextBox4" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="advRequired">Travel Advance Required?</label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:CheckBox ID="chkAdvRequired" runat="server" class="form-control" oncheckedchanged="chkAdvRequired_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                    <label for="advanceAmt">Advance Amount </label>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:TextBox ID="txtAdvanceAmount" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-sm-4 col-md-4">
                                </div>
                                <div class="col-sm-6 col-md-6">
                                     <asp:Label ID="lblMessage" runat="server" Text=" "></asp:Label>
                                        <br />
                                    <asp:Button ID="Button1" runat="server" Text="Send for Approval" class="form-control btn-success" OnClick="Button1_Click" />
                                </div>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="ViewPrint" style="max-height:500px; overflow:auto;">
                            <asp:Label ID="Label1" runat="server" Text="OD Request" ForeColor="#000066" Font-Bold="True" Font-Underline="True"></asp:Label>

                            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                OnRowDeleting="GridView1_RowDeleting" EnableModelValidation="True" OnDataBound="GridView1_DataBound"
                                 AutoGenerateColumns="False" DataKeyNames="SlNo"
                                EmptyDataText="There are no data records to display.">
                                <Columns>
                                    <asp:BoundField DataField="AbsenseType" HeaderText="AbsenseType" ReadOnly="True" SortExpression="AbsenseType" />
                                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                                    <asp:BoundField DataField="ODDayType" HeaderText="ODDayType" SortExpression="ODDayType" />
                                    <asp:BoundField DataField="TravelAdvance" HeaderText="TravelAdvance" SortExpression="TravelAdvance" />  
                                    <asp:BoundField DataField="Description" HeaderText="Description" />  
                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                     <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Delete">Cancel</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </form>
</body>
</html>
