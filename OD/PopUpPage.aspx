<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpPage.aspx.cs" Inherits="PopUpPage" %>

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
    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        th, td {
            padding: 5px;
            text-align: left;
        }
    </style>
</head>
<body topmargin="5" leftmargin="5">
    <form id="form1" runat="server">
        <div class="container">
            <div class="panel panel-default">
                <div class="panel-heading">Request Approval</div>
                <div class="panel-body">
<div class="row">
                                <div class="col-sm-10 col-md-10">
        <table style="width: 100%">
            <tr>
                <th>Type of Absence</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Full/Half Day</th>
                <th>Travel Advance</th>
                <th>Description</th>
            </tr>
            <tr>
                <td>
                    <label id="TypeofAbsence" runat="server"></label>
                </td>
                <td>
                    <label id="StartDate" runat="server"></label>
                </td>
                <td>
                    <label id="EndDate" runat="server"></label>
                </td>
                <td>
                    <label id="Time" runat="server"></label>
                </td>
                 <td>
                    <label id="TravelAdvance" runat="server"></label>
                </td>
                <td>
                    <label id="Description" runat="server"></label>
                </td>
              
            </tr>
        </table>
        <br />
        Description: 
          <asp:TextBox ID="txtDescription" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
        <br />
         <asp:Label ID="lblMessage" runat="server" Text=" "></asp:Label>
        <br />
                                    <div class="row">
        <div class="col-md-3"><asp:Button ID="Approve" runat="server" Text="Approve"  OnClick="Approve_Click" class="form-control btn-success" /></div>
        <div class="col-md-3"><asp:Button ID="Reject" runat="server" Text="Reject"  OnClick="Reject_Click" class="form-control" /></div>               
                                        </div>
        <%-- <div class="row">
            <div class="col-sm-4 col-md-4">
            </div>
            <div class="col-sm-1 col-md-1">
                  
            </div>
            <div class="col-sm-1 col-md-1">
              
            </div>
        </div>--%>
</div></div>       
</div></div></div>        
    </form>
</body>
</html>
