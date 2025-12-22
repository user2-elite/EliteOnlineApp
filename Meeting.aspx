<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Meeting.aspx.cs" Inherits="Meeting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="MessageBox/messagebox.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/pure-min.css">
    <!--[if lte IE 8]>
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/grids-responsive-old-ie-min.css">
    <![endif]-->
    <!--[if gt IE 8]><!-->
    <link rel="stylesheet" href="JQuery_Pure/pure-release-0.5.0/grids-responsive-min.css">
    <!--<![endif]-->
    <link rel="stylesheet" href="JQuery1.11.4/jquery-ui.css">
    <script src="JQuery1.11.4/jquery-1.10.2.js"></script>
    <script src="JQuery1.11.4/jquery-ui.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <script language="javascript">
        function confCancel() {
            if (window.confirm("This will cancel the meeting request. Email notification will send to all members if the 'Notify participants' checkbox is ticked. Click on 'Ok' button to confirm cancellation")) {
                return true;
            }
            else {
                return false;
            }
        }
        function MoveItem(ctrlSource, ctrlTarget) {
            var Source = document.getElementById(ctrlSource);
            var Target = document.getElementById(ctrlTarget);
            document.getElementById('<%=hdnListTexts.ClientID %>').value = "";
            document.getElementById('<%=hdnListValues.ClientID %>').value = "";
            if ((Source != null) && (Target != null)) {
                while (Source.options.selectedIndex >= 0) {
                    var newOption = new Option(); // Create a new instance of ListItem
                    newOption.text = Source.options[Source.options.selectedIndex].text;
                    newOption.value = Source.options[Source.options.selectedIndex].value;

                    Target.options[Target.length] = newOption; //Append the item in Target
                    Source.remove(Source.options.selectedIndex);  //Remove the item from Source
                }
            }
            var SelectedList = document.getElementById('lstparticipantsSelected');
            for (i = 0; i < SelectedList.length; i++) {
                document.getElementById('<%=hdnListTexts.ClientID %>').value += SelectedList.options[i].text + ",";
                document.getElementById('<%=hdnListValues.ClientID %>').value += SelectedList.options[i].value + ",";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="width: 100%; background-color: #EEEEEE; margin: 0px; padding: 0px; border: 0px;">
        <asp:Panel runat="server" ID="pnlAddNew">
            <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
                border: 0px;">
                <div class="pure-g">
                    <div class="pure-u-1 pure-u-md-1-2">
                        Meeting Title/Subject (Max character length:100)<br />
                        <asp:TextBox ID="txtTitle" runat="server" Width="525px" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-2" runat="server" id="divBack">
                        <a href="" id="lnkBack" runat="server" class="btn btn-info"><span class="glyphicon glyphicon-arrow-left"
                            aria-hidden="true"></span> Back</a>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        Agenda of the Meeting<br />
                        <asp:TextBox ID="txtAgenda" runat="server" Width="525px" TextMode="MultiLine" Rows="3"
                            cols="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAgenda"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        Meeting Date<br />
                        <asp:TextBox ID="txtMeetingDate" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMeetingDate"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        Start Time<br />
                        <asp:TextBox ID="txtStartTime" runat="server" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStartTime"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        End Time<br />
                        <asp:TextBox ID="txtEndTime" runat="server" Width="80px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndTime"
                            ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        &nbsp;
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        &nbsp;
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    <b>Location Details</b> Unit Name<br />
                                    <asp:DropDownList ID="ddCompanyUnit" runat="server" Width="250px" OnSelectedIndexChanged="ddCompanyUnit_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddCompanyUnit"
                                        ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    Conference Room Name<br />
                                    <asp:DropDownList ID="ddConfrooms" runat="server" Width="250px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddConfrooms"
                                        ErrorMessage="Required" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:LinkButton ID="lnkCheckAvailability" runat="server" OnClick="lnkCheckAvailability_Click"
                                        CausesValidation="false"><font color="blue"><B>Check availability</B></font></asp:LinkButton>
                                    <div id="divAvailability" runat="server" style="width: 450px;" class="alert-box warning">
                                    </div>
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DynamicLayout="true" DisplayAfter="0"
                                        AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <img border="0" src="Images/progress1.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="pure-u-1 pure-u-md-1-1">
                            <B>Choose Participants</B>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5 alert-danger" style="width:250px;">
                        &nbsp;&nbsp;<B>All Available Members</B>
                        <asp:ListBox ID="lstparticipants" runat="server" Height="190px" Width="250px" SelectionMode="Multiple" class="form-control">
                        </asp:ListBox>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        <p style="height: 30px; width: 100px; text-align: center">
                        </p>
                        <p style="width: 230px; text-align: center">
                            <a href="Javascript:MoveItem('lstparticipants', 'lstparticipantsSelected');" class="btn btn-info">
                                <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span></a>
                        </p>
                        <p style="width: 230px; text-align: center">
                            <a href="Javascript:MoveItem('lstparticipantsSelected', 'lstparticipants');" class="btn btn-info">
                                <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"></span></a>
                        </p>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5 alert-success" style="width:250px;">
                        &nbsp;&nbsp;<B>All Selected Members</B>
                        <asp:ListBox ID="lstparticipantsSelected" runat="server" Height="190px" Width="250px"
                            SelectionMode="Multiple" class="form-control"></asp:ListBox>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        &nbsp;
                    </div>
                    <div class="pure-u-1 pure-u-md-1-5">
                        &nbsp;
                    </div>
                    <%--<div class="pure-u-1 pure-u-md-1-3" style="vertical-align: bottom">
                                    <asp:Button ID="btnAdd" Text="Add To List" runat="server" class="pure-button pure-button-primary"
                                        CausesValidation="false" OnClick="btnAdd_Click" />
                                </div>
                                <div class="pure-u-1 pure-u-md-1-3">
                                </div> 
                                <div class="pure-u-1 pure-u-md-1-1">
                                    Participants List<br />
                                    <asp:TextBox ID="txtParticipants" runat="server" Width="525px" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtParticipants"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>--%>
                    <div class="pure-u-1 pure-u-md-1-1">
                        Outcome of the Meeting (To be updated after the meeting)<br />
                        <asp:TextBox ID="txtOutCome" runat="server" Width="525px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtOutCome"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="pure-u-1 pure-u-md-1-1">
                        <br />
                        Please Specify whether an email notification should be sent to all participants<br />
                        <asp:CheckBox runat="server" ID="chkNotify" />
                        Notify Participants
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <div class="pure-u-1 pure-u-md-1-1">
                                    <br />
                                    <div style="position: absolute; width: 800px; left: 200px;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="0"
                                            AssociatedUpdatePanelID="UpdatePanel2">
                                            <ProgressTemplate>
                                                <img border="0" src="Images/progress1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <div class="alert-box success" id="divSuccess" runat="server">
                                        </div>
                                        <div class="alert-box warning" id="divAlert" runat="server">
                                        </div>
                                        <div class="alert-box notice" id="divNotice" runat="server">
                                        </div>
                                        <div class="alert-box error" id="diverror" runat="server">
                                        </div>
                                    </div>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Schedule A Meeting" class="btn btn-success"
                                        OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel Meeting" class="btn btn-danger"
                                        OnClick="btnCancel_Click" OnClientClick="javascript:return confCancel();" />
                                </div>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
    </div>
    <link rel="stylesheet" type="text/css" href="jQuerydatetime/jquery.datetimepicker.css" />
    <script src="jQuerydatetime/jquery.datetimepicker.js"></script>
    <script>

        $('#txtMeetingDate').datetimepicker({
            dayOfWeekStart: 1,
            lang: 'en',
            datepicker: true,
            timepicker: false,
            format: 'm/d/Y',
            formatDate: 'Y/m/d'
            //disabledDates: ['2010/01/08', '2010/01/09', '2010/01/10'],
            //startDate: '2010/01/05'
            //mask: '9999/19/39 29:59'
        });

        $('#txtStartTime').datetimepicker({
            datepicker: false,
            timepicker: true,
            format: 'H:i',
            allowTimes: ['7:00', '7:30', '8:00', '8:30', '9:00', '9:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00', '13:30', '14:00', '14:30', '15:00', '15:30'
                , '16:00', '16:30', '17:00', '17:30', '18:00', '18:30', '19:00', '19:30', '20:00', '20:30'],
            step: 5
        });

        $('#txtEndTime').datetimepicker({
            datepicker: false,
            timepicker: true,
            format: 'H:i',
            allowTimes: ['7:00', '7:30', '8:00', '8:30', '9:00', '9:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00', '13:30', '14:00', '14:30', '15:00', '15:30'
                , '16:00', '16:30', '17:00', '17:30', '18:00', '18:30', '19:00', '19:30', '20:00', '20:30'],
            step: 5
        });
    </script>
    <asp:HiddenField runat="server" ID="hdnListValues" />
    <asp:HiddenField runat="server" ID="hdnListTexts" />
    </form>
</body>
</html>
