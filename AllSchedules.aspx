<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllSchedules.aspx.cs" Inherits="AllSchedules" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Home</title>
    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/yamm.css" rel="stylesheet">
    <!-- custom css -->
    <link href="css/custom.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
		<![endif]-->
    <link href="Styles/popup.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div>
        <div id="calendar">
        </div>
    </div>
    <!-- menu-container -->
    <link href='FullCalendarJS/fullcalendar.css' rel='stylesheet' />
    <link href='FullCalendarJS/fullcalendar.print.css' rel='stylesheet' media='print' />
    <script src='FullCalendarJS/lib/moment.min.js'></script>
    <!--<script src='FullCalendarJS/lib/jquery.min.js'></script>-->
    <script src="js/jquery-1.11.3.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src='FullCalendarJS/fullcalendar.min.js'></script>
    <link rel="stylesheet" href="js/bootstrap-theme.min.css">
    <!--<script src="js/jquery.min.js"></script>-->
    <script src="js/bootstrap.min.js"></script>
    <script>
        function MeetingDetails() {
            window.open('http://www.google.com', '_blank', 'width=600,height=500,scrollbars=1');
        }
        var userID = '<%= UserID %>';
        var defaultdate = new Date();
        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: "{}",
            url: "schedule.asmx/GetXMLEventsFromEntity2?user=" + userID,
            dataType: "json",
            success: function (data) {
                $('div[id*=calendar]').fullCalendar({
                    //****************************************************
                    //disable 4 weeks old and 4 weeks future date
//                    viewRender: function (currentView) {
//                        var minDate = moment().add(-4, 'weeks'),
//		maxDate = moment().add(4, 'weeks');
//                        // Past
//                        if (minDate >= currentView.start && minDate <= currentView.end) {
//                            $(".fc-prev-button").prop('disabled', true);
//                            $(".fc-prev-button").addClass('fc-state-disabled');
//                        }
//                        else {
//                            $(".fc-prev-button").removeClass('fc-state-disabled');
//                            $(".fc-prev-button").prop('disabled', false);
//                        }
//                        // Future
//                        if (maxDate >= currentView.start && maxDate <= currentView.end) {
//                            $(".fc-next-button").prop('disabled', true);
//                            $(".fc-next-button").addClass('fc-state-disabled');
//                        } else {
//                            $(".fc-next-button").removeClass('fc-state-disabled');
//                            $(".fc-next-button").prop('disabled', false);
//                        }
//                    },
                    //****************************************************
                    //theme: true,
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'agendaWeek,agendaDay,month'
                    },
                    defaultView: 'agendaWeek',
                    //businessHours: true, // display business hours
                    editable: false,
                    eventLimit: true, // allow "more" link when too many events
                    defaultDate: defaultdate,
                    events: $.map(data.d, function (item, i) {
                        var event = new Object();
                        event.id = item.EventID;
                        event.start = new Date(item.StartDate);
                        event.end = new Date(item.EndDate);
                        event.title = item.EventName;
                        event.color = '#257e4a';
                        // event.url = 'Javascript:showViewMeetingPopup();';
                        event.url = 'Meeting.aspx?allsch=1&id=' + item.EventID;
                        return event;
                    })
                })
            }
        });        
    </script>
</body>
</html>
