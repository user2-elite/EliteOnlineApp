<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elite ITSupport</title>
</head>
<body>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="ssCalendar.css" type="text/css">
	<script language="javascript" src="ssCalendar.js"></script>
<script language="javascript">
	function check_date(field, blnAllowBlank)
	{
	
		var checkstr = "0123456789";
		var DateField = field;
		var Datevalue = "";
		var DateTemp = "";
		var seperator = "-";
		var day;
		var month;
		var year;
		var leap = 0;
		var err = 0;
		var i;
		err = 0;
		DateValue = DateField.value;
		
		/* Delete all chars except 0..9 */
		for (i = 0; i < DateValue.length; i++) {
			if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
				DateTemp = DateTemp + DateValue.substr(i,1);
			}
		}
		DateValue = DateTemp;
		/* Always change date to 8 digits - string*/
		/* if year is entered as 2-digit / always assume 20xx */
		
		if (DateValue.length == 6)
		{
			DateValue = '20' + DateValue
		}
		
		if (DateValue.length != 8) 
		{
			if (blnAllowBlank == true && DateField.value.length == 0)
			{
				//let a blank value pass
				err = 10;
			}
			else
			{
				//not a date
				err = 20;
			}
		}
		else
		{
			/* year is wrong if year = 0000 */
			year = DateValue.substr(0,4);
			if (year <= 1899) {
				err = 21;
			}
			/* Validation of month*/
			month = DateValue.substr(4,2);
			if ((month < 1) || (month > 12)) {
				//not a month
				err = 22;
			}
			/* Validation of day*/
			day = DateValue.substr(6,2);
			if (day < 1) {
				err = 23;
			}
			/* Validation leap-year / february / day */
			if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
				leap = 1;
			}
			if ((month == 2) && (leap == 1) && (day > 29)) {
				err = 24;
			}
			if ((month == 2) && (leap != 1) && (day > 28)) {
				err = 25;
			}
			/* Validation of other months */
			if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
				err = 26;
			}
			if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
				err = 27;
			}
			/* if 00 is entered, no error, deleting the entry */
			if ((day == 0) && (month == 0) && (year == 00)) {
				err = 0; day = ""; month = ""; year = ""; seperator = "";
			}
		}
		/* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
		if (err == 0) 
		{
			DateField.value = year + seperator + month + seperator + day;
		}
		/* Error-message if err != 0 */
		else 
		{
			/*if (err == 10)
			{
				DateField.value = "";
				DateField.select();
				DateField.focus();
				alert("You have to submit an expiration date.");
				return false;
			}
			else*/
			/*if (err != 10)
			{

				DateField.select();
				DateField.focus();
				alert("Date is incorrect, use the format dd/mm/yyyy.");
				return false;
			}*/
		}
	}	
	
		// ---------- Start Customizable data for Calendar ( for internationalization/ Curent Date ) ----------
	var months = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
	var daysOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
	var todayCaption = "Today";	
	var d = new Date();
	var currYear = d.getFullYear();
	var currMonth = d.getMonth();
	currMonth = parseInt(currMonth) + 1
	var currDate = d.getDate(); // Assign client date. To use server date, set the server date here using server script.
	var weekStartDay = 1; // To Start from - Sunday : 0, Monday : 1 etc 
	var nonWorkingDays = "6,0"; // Saturdays and Sundays are non-working days
	-->
</script>	

<form id="form1" runat="server">
<table width="100%">

                        <tr>
                            <td valign="top" >
                            <br />
    <asp:LinkButton ID="lnkDaily" runat="server" onclick="lnkDaily_Click" Font-Bold="true" Font-Size="14px">Daily Report</asp:LinkButton>
&nbsp;|
    <asp:LinkButton ID="lnkWeekly" runat="server" onclick="lnkWeekly_Click" Font-Bold="true" Font-Size="14px">Weekly Report</asp:LinkButton>    
     <br />
   <b><div id="divReportName" runat="server"></div></b>
        <div id="divDailyFilter" runat="server" visible="false">
                                    <asp:TextBox CssClass="slo-TextBox" ID="txtFromDate" runat="server" onblur="return check_date(this, true);" onkeyDown="javascript:return checkDateManual()"></asp:TextBox>
                                <a id="ancStartDate"><img border="0" src='Images/cal.png' alt="Choose Calendar"  style="cursor:hand"/></a>
                                <div id="dtDivStartDate" style="border-style:outset;border-width:1px;position:absolute;width:180px;visibility:hidden;z-index:-1;display:block;"></div>

                                            <asp:TextBox CssClass="slo-TextBox" ID="txtToDate" runat="server" onblur="return check_date(this, true);" onkeyDown="javascript:return checkDateManual()"></asp:TextBox>
                                <a id="ancEndDate"><img border="0" src='Images/cal.png' alt="Choose Calendar"  style="cursor:hand"/></a>
                                <div id="dtDivEndDate" style="border-style:outset;border-width:1px;position:absolute;width:180px;visibility:hidden;z-index:-1;display:block;"></div>
    &nbsp;<asp:Button ID="btnDailyReport" runat="server" onclick="btnDailyReport_Click" Text="Show Report" />
    </div>
    <div id="divWeeklyFilter" runat="server" visible="false">
    <B>Year</B>: 
    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
    &nbsp;<B>Week</B>: 
    <asp:DropDownList ID="ddlWeek" runat="server"></asp:DropDownList>
    &nbsp;<asp:Button ID="btnWeeklyReport" runat="server" onclick="btnWeeklyReport_Click" Text="Show Report" />
    </div> 
    <asp:Button ID="btnExport1" OnClick="btnExport_Click" runat="server" Text="Export To Excel" />
    <asp:GridView ID="gvReport" runat="server" AllowPaging="True" AutoGenerateColumns="True"
                                            EmptyDataText="No Data available" Width="1400px" BackColor="White" BorderColor="#404040" BorderStyle="Solid" 
                    BorderWidth="1px" PageSize="50" CellPadding="3">
                    
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#E89F65" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="#FF0000" Font-Bold="true" Font-Size="16px" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="maroon" Font-Bold="False" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="WhiteSmoke" />

                    </asp:GridView>
                     <div>
                <asp:Button ID="btnExport2" OnClick="btnExport_Click" runat="server" Text="Export To Excel" />
            </div>
</td>
</tr>
</table>
  </form>
   <script language="javascript"><!--  
       var dptxtFromDate = new DatePicker();
       var d = new Date();
       dptxtFromDate.id = "txtFromDate";
       var currMonth = d.getMonth();
       currMonth = parseInt(currMonth) + 1
       dptxtFromDate.month = currMonth;
       dptxtFromDate.year = d.getFullYear();
       dptxtFromDate.canvas = "dtDivStartDate";
       dptxtFromDate.format = "mm/dd/yyyy";
       dptxtFromDate.anchor = "ancStartDate";
       dptxtFromDate.initialize();

       var dptxtToDate = new DatePicker();
       var d = new Date();
       dptxtToDate.id = "txtToDate";
       var currMonth = d.getMonth();
       currMonth = parseInt(currMonth) + 1
       dptxtToDate.month = currMonth;
       dptxtToDate.year = d.getFullYear();
       dptxtToDate.canvas = "dtDivEndDate";
       dptxtToDate.format = "mm/dd/yyyy";
       dptxtToDate.anchor = "ancEndDate";
       dptxtToDate.initialize();
-->
</script>
</body>
</html>

