<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Elite Intraweb - Home</title>
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
    <style>
        .popover
        {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 1010;
            display: none;
            max-width: 800px;
            padding: 1px;
            text-align: left;
            white-space: normal;
            background-color: #ffffff;
            border: 1px solid #ccc;
            border: 1px solid rgba(0, 0, 0, 0.2);
            -webkit-border-radius: 6px;
            -moz-border-radius: 6px;
            border-radius: 6px;
            -webkit-box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            -moz-box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            -webkit-background-clip: padding-box;
            -moz-background-clip: padding;
            background-clip: padding-box;
        }
        .popover-text
        {
            font-size: 12px;
            color: #000000;
        }
    </style>
    <link href="MessageBox/messagebox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="container">
        <header class="row">
			<div class="col-xs-4 col-sm-4 col-md-2">
				<span class="col-sm-8 col-md-10"><img src="img/logonew.png" class="img-responsive center-block-logo" alt="" title=""/></span>
			</div>
			<div class="col-xs-8 col-md-6 pull-right top-right">
				<div class="col-sm-5">
           <!-- <div id="imaginary_container"> 
                <div class="input-group stylish-input-group">
                    <input type="text" class="form-control red-box" placeholder="Search" >
                    <span class="input-group-addon">
                        <button type="submit">
                            <span class="glyphicon glyphicon-search"></span>
                        </button>  
                    </span>
                </div>
            </div>-->
        </div>        
  <!--
        <div class="col-md-3">
              <span class="red count">10</span><span class="glyphicon glyphicon-bell notify" aria-hidden="true"></span>
          </div>-->          
        <div class="col-md-4 pull-right">
        	<div class="dropdown pull-right">
			  <button class="btn dropdown-toggle user-settings" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
			  	<i class="glyphicon glyphicon-user user" aria-hidden="true"></i>
			   <span style="padding-top: -10px"><asp:Label runat="server" id="lblLogonuser"></asp:Label></span> 
			    <span class="caret"></span>
			  </button>
			  <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">                            
			    <li role="presentation" class="popover-options1"><a role="menuitem" tabindex="-1" style="color:#FFFFFF; font-size:14px;"
                title="<h2>My Information</h2>"  data-container="body" data-toggle="popover" data-html="true" data-content="<div class='popover-text'><%=Session["UserText"]%></div>">
                My Details</a></li>
                <li role="presentation"><a role="menuitem" tabindex="-1" href="ChangePassword.aspx" style="color:#FFFFFF; font-size:14px;">Change Password</a></li>
                <li role="presentation" id="liSettings" runat="server"><a role="menuitem" tabindex="-1" href="adminHome.aspx" style="color:#FFFFFF; font-size:14px;">Settings</a></li>
			    <li role="presentation"><a role="menuitem" tabindex="-1" href="LogOff.aspx" style="color:#FFFFFF; font-size:14px;">Logout</a></li>			   
			  </ul>
			</div>
        </div>
			</div>
		</header>
    </div>
    <div class="container-fluid clear-padd nav-container">
        <div class="container">
            <div class="navbar navbar-default yamm">
                <div class="navbar-header">
                    <button type="button" data-toggle="collapse" data-target="#navbar-collapse-grid"
                        class="navbar-toggle">
                        <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar">
                        </span>
                    </button>
                </div>
                <div id="navbar-collapse-grid" class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a href="home.aspx">Home</a></li>
                        <li><a href="company.aspx">Company</a></li>
                        <!--<li><a href="management.aspx">Management Team</a></li>-->
                        <!-- Grid 12 Menu -->
                        <li class="dropdown yamm-fw"><a href="#" data-toggle="dropdown" class="dropdown-toggle">
                            My Applications<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li class="grid-demo">
                                    <div class="row">
                                        <div class="col-sm-5">
                                            <a role="menuitem" tabindex="-1" href="ITSupport/Home.aspx" style="color: #FFFFFF; font-size: 15px; font-weight: bold;" target="_blank">IT Support Request</a>
                                            <br />
                                            <a role="menuitem" tabindex="-1" href="KnowledgeManagement/km.aspx?id=1" style="color: #FFFFFF;font-size: 15px; font-weight: bold;" target="_blank">Knowledge Management - CRM, ATR & Best Practice</a>
                                            <br />
                                            <a role="menuitem" tabindex="-1" href="CRM/login.aspx" style="color: #FFFFFF; font-size: 15px; font-weight: bold;" target="_blank">CRM Portal Login</a>
                                            <br />
                                           <a role="menuitem" tabindex="-1" href="MySalarySlip.aspx" style="color:#FFFFFF; font-size:15px;font-weight:bold;">My Salary Slip</a>
                                           <br />
                                           <a role="menuitem" tabindex="-1" href="leave/Leave.aspx" style="color:#FFFFFF; font-size:15px;font-weight:bold;">Leave Request</a>
<br />
                                           <a role="menuitem" tabindex="-1" href="OD/Request.aspx" style="color:#FFFFFF; font-size:15px;font-weight:bold;">OD Request</a>

                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <li><a href="policy.aspx">Policies & Guidelines</a></li>
                        <li><a href="news.aspx">News & Events</a></li>
                        <li><a href="gallery.aspx">Gallery</a></li>
                        <li id="liDashboard" runat="server" class="dropdown yamm-fw"><a href="#" data-toggle="dropdown" class="dropdown-toggle">
                            Dashboard<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li class="grid-demo">
                                    <div class="row">
                                    <div class="col-sm-8">
                                    &nbsp;
                                    </div>
                                        <div class="col-sm-3">
                                            <a role="menuitem" tabindex="-1" href="ITDashboard.aspx" style="color: #FFFFFF;
                                                font-size: 15px; font-weight: bold;" target="_blank">IT Support Dashboards</a>
                                            <br />
                                            <a role="menuitem" tabindex="-1" href="CRMDashboards.aspx" style="color: #FFFFFF;
                                                font-size: 15px; font-weight: bold;" target="_blank">CRM Dashboards</a>
                                            <br />
                                            <a role="menuitem" tabindex="-1" href="HRDashboards.aspx" style="color: #FFFFFF;
                                                font-size: 15px; font-weight: bold;" target="_blank">HR Dashboards</a>
                                                                                        
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row content-part">
            <div class="col-sm-12 col-md-2 qucik-menu">
                <h4 class="quick-menuheader text-right">
                    Extra Links</h4>
                <%--   <ul class="list-unstyled text-right">
                    <li style="padding: 3px;"><a href="page.aspx?id=1">Top Management</a></li>
                    <li style="padding: 3px;"><a href="page.aspx?id=2">Holiday List</a></li>
                    <li style="padding: 3px;"><a href="page.aspx?id=3">CXO Messages</a></li>
                    <li style="padding: 3px;"><a href="page.aspx?id=4">Birthday Calendar</a></li>
                    <li style="padding: 3px;"><a href="page.aspx?id=5">New Joinees</a></li>
                    <li style="padding: 3px;"><a href="page.aspx?id=6">Training List</a></li>
                    <li style="padding: 3px;"><a href="page.aspx?id=7">Up Coming Events</a></li>
                </ul>--%>
                <div runat="server" id="divLeftLinks">
                </div>
            </div>
            <div class="col-sm-12 col-md-6">
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                        <li data-target="#myCarousel" data-slide-to="1"></li>
                        <li data-target="#myCarousel" data-slide-to="2"></li>
                    </ol>
                    <div class="carousel-inner" role="listbox">
                        <div class="item active">
                            <img class="first-slide" alt="Gallery Pictures" id="imgFirstSlide" runat="server" />
                            <div class="container">
                                <div class="carousel-caption">
                                    <!--<h1>
                                        headline 1.</h1>
                                    <p>
                                        Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text
                                        Sample Text Sample Text Sample Text Sample Text Sample Text.</p>
                                    <br />
                                    <br />
                                    -->
                                    <p>
                                        <a class="btn btn-lg btn-primary" role="button" id="lnkFirstSlideImage" runat="server">
                                            Browse gallery</a></p>
                                </div>
                            </div>
                        </div>
                        <div class="item">
                            <img class="second-slide" alt="Gallery Pictures" id="imgSecondSlide" runat="server" />
                            <div class="container">
                                <div class="carousel-caption">
                                    <!--<h1>
                                        headline 2.</h1>
                                    <p>
                                        Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text
                                        Sample Text Sample Text Sample Text Sample Text Sample Text.</p>
                                    <br />
                                    <br />
                                    -->
                                    <p>
                                        <a class="btn btn-lg btn-primary" role="button" id="lnkSecondSlideImage" runat="server">
                                            Browse gallery</a></p>
                                </div>
                            </div>
                        </div>
                        <div class="item">
                            <img class="third-slide" alt="Gallery Pictures" id="imgThirdSlide" runat="server" />
                            <div class="container">
                                <div class="carousel-caption">
                                    <!--<h1>
                                        headline 3.</h1>
                                    <p>
                                        Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text Sample Text
                                        Sample Text Sample Text Sample Text Sample Text Sample Text.</p>
                                    <br />
                                    <br />
                                    -->
                                    <p>
                                        <a class="btn btn-lg btn-primary" role="button" id="lnkThirdSlideImage" runat="server">
                                            Browse gallery</a></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span><span class="sr-only">
                            Previous</span> </a><a class="right carousel-control" href="#myCarousel" role="button"
                                data-slide="next"><span class="glyphicon glyphicon-chevron-right" aria-hidden="true">
                                </span><span class="sr-only">Next</span> </a>
                </div>
                <!-- /.carousel -->
            </div>
            <div class="col-sm-12 col-md-4">
                <div class="row" style="margin-top: 5px">
                    <div class="col-xs-12 col-sm-12 col-md-12 right-info clear-padd">
                        <div class="col-xs-6 col-sm-6 col-md-6 clear-padd">
                            <img runat="server" id="imgNews" src="img/user-left.png" class="img-responsive" alt=""
                                title="" height="130" /></div>
                        <div class="col-xs-6 col-sm-6 col-md-6 pull-right right-info">
                            <h4 class="text-center">
                                News & Events
                            </h4>
                            <p class="text-center">
                                <div runat="server" id="divNewsText">
                                </div>
                                <br>
                                <a runat="server" id="lnkNewsID">Read more</a></p>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 5px">
                    <div class="col-xs-12 col-sm-12 col-md-12 left-info clear-padd">
                        <div class="col-xs-6 col-sm-6 col-md-6 pull-left clear-padd">
                            <h4 class="text-center">
                                Management Talks</h4>
                            <p class="text-center" style="text-align: center">
                                <div runat="server" id="divMGMTTalk" style="text-align: center">
                                </div>
                                <br>
                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <a runat="server" id="lnkMGMTTalk">
                                    Read more</a></p>
                        </div>
                        <div class="col-xs-6 col-sm-6 col-md-6 clear-padd">
                            <img runat="server" id="imgMGMTTalk" class="img-responsive pull-right" title="" height="130" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 pull-right">
                <div class="col-xs-12 col-sm-12 col-md-12 popover-options2">
                    <a herf="#" class="btn btn-info" title="<h2>Scheduled Travels</h2>" data-container="body"
                        data-toggle="popover" data-html="true" data-content="<div class='popover-text'><%= TravelInfo %></div>"
                        <span class="glyphicon glyphicon-plane" aria-hidden="true"></span>Scheduled Travels</a>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-12">
                    <h3>
                        &nbsp;&nbsp;Quote for the Day</h3>
                    <blockquote>
                        <div id="divQuote" runat="server">
                        </div>
                    </blockquote>
                </div>
            </div>
            <div class="col-xs-12 col-sm-10 col-md-6 pull-right">
                <div class="">
                    <a href="javascript:showAddMeetingPopup();" class="btn btn-danger"><span class="glyphicon glyphicon-plus"
                        aria-hidden="true"></span>Add New Meeting</a> <a href="Javascript:showMySchedules();"
                            class="btn btn-success"><span class="glyphicon glyphicon-calendar" aria-hidden="true">
                            </span>My Schedules</a>
                </div>
                <hr>
                <div id="calendar">
                </div>
                <br>
            </div>
        </div>
    </div>
    <div class="container-fluid clear-padd footer-container">
        <div class="container">
            <br>
            <p class="text-center">
                Elite 2015, All rights reserved</p>
            <br>
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
            <link href="Styles/bdaystyle.css" rel="stylesheet" type="text/css" />
    <script src="js/popup.js" type="text/javascript"></script>

    <script>
        var options1 = {
            placement: function (context, source) {
                var position = $(source).position();
                return "bottom";
            }
    , trigger: "click"
        };
        $(".popover-options1 a").popover(options1);


        var options2 = {
            placement: function (context, source) {
                var position = $(source).position();

                if (position.left > 515) {
                    return "left";
                }

                if (position.right > 515) {
                    return "right";
                }

                if (position.top > 300) {
                    return "top";
                }

                return "bottom";
            }
    , trigger: "click"
        };
        $(".popover-options2 a").popover(options2);
        //$(function () { $('.popover-show').popover('show');});
        //$(function () { $(".popover-options a").popover({html : true });});
    </script>
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
            url: "schedule.asmx/GetXMLEventsFromEntity1?user=" + userID,
            dataType: "json",
            success: function (data) {
                $('div[id*=calendar]').fullCalendar({
                    //****************************************************
                    //disable 4 weeks old and 4 weeks future date
                    viewRender: function (currentView) {
                        var minDate = moment().add(-4, 'weeks'),
		maxDate = moment().add(4, 'weeks');
                        // Past
                        if (minDate >= currentView.start && minDate <= currentView.end) {
                            $(".fc-prev-button").prop('disabled', true);
                            $(".fc-prev-button").addClass('fc-state-disabled');
                        }
                        else {
                            $(".fc-prev-button").removeClass('fc-state-disabled');
                            $(".fc-prev-button").prop('disabled', false);
                        }
                        // Future
                        if (maxDate >= currentView.start && maxDate <= currentView.end) {
                            $(".fc-next-button").prop('disabled', true);
                            $(".fc-next-button").addClass('fc-state-disabled');
                        } else {
                            $(".fc-next-button").removeClass('fc-state-disabled');
                            $(".fc-next-button").prop('disabled', false);
                        }
                    },
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
                        event.url = 'Meeting.aspx?id=' + item.EventID;
                        return event;
                    })
                })
            }
        });        
    </script>
    <script>
        $(function () {
            $(document).on('click', '.yamm .dropdown-menu', function (e) {
                e.stopPropagation();
            });
        });           
    </script>
    	<!-- Files For mRova Feedback Form [Dependency: jQuery] -->
	<script src="Feedback/mrova-feedback-form.js" type="text/javascript"></script>
	<link rel="stylesheet" href="Feedback/mrova-feedback-form.css" type="text/css"/>
	<!-- END -->
    <div id="AddMeeting" class="popup-wrapper">
        <div class="popup-content">
            <div class="popup-title">
                <button id="close1" type="button" class="popup-close">
                    &times;</button>
                <h4>
                    Add New Meeting</h4>
            </div>
            <div class="popup-body">
                <p>
                    <iframe width="100%" height="475px" id="newWin" src="Meeting.aspx" runat="server"
                        scrolling="auto" frameborder="0" style="padding: 0px; border: 0px; background-color: #EEEEEE">
                    </iframe>
                </p>
            </div>
        </div>
    </div>
    <div id="ViewMeeting" class="popup-wrapper">
        <div class="popup-content">
            <div class="popup-title">
                <button id="close2" type="button" class="popup-close">
                    &times;</button>
                <h4>
                    View/Update Meeting</h4>
            </div>
            <div class="popup-body">
                <p>
                    <iframe width="100%" height="475px" id="Iframe1" src="Meeting.aspx" runat="server"
                        scrolling="auto" frameborder="0" style="padding: 0px; border: 0px; background-color: #EEEEEE">
                    </iframe>
                </p>
            </div>
        </div>
    </div>
    <div id="ViewSchedule" class="popup-wrapper">
        <div class="popup-content">
            <div class="popup-title">
                <button id="close3" type="button" class="popup-close">
                    &times;</button>
                <h4>
                    View My schedules</h4>
            </div>
            <div class="popup-body">
                <p>
                    <iframe width="100%" height="500px" id="Iframe2" src="AllSchedules.aspx" runat="server"
                        scrolling="auto" frameborder="0" style="padding: 1px; border: 0px; background-color: #EEEEEE">
                    </iframe>
                </p>
            </div>
        </div>
    </div>
    <script src="simple-popup/simple-popup.js"></script>
    <script language="javascript">
        HidePopup();
        function showAddMeetingPopup() {
            HidePopup();
            document.getElementById("AddMeeting").style.visibility = "visible";
            if (detectmob()) {
                document.getElementById("AddMeeting").style.width = "100%";
                document.getElementById("AddMeeting").style.height = "550px";
            }
            else {
                document.getElementById("AddMeeting").style.width = "83%";
                document.getElementById("AddMeeting").style.height = "570px";
            }
            window.location.hash = '#AddMeeting';
        }
        function showViewMeetingPopup() {
            HidePopup();
            document.getElementById("ViewMeeting").style.visibility = "visible";
            document.getElementById("ViewMeeting").style.width = "70%";
            document.getElementById("ViewMeeting").style.height = "550px";
            window.location.hash = '#ViewMeeting';
        }
        function showMySchedules() {
            HidePopup();
            document.getElementById("ViewSchedule").style.visibility = "visible";
            if (detectmob()) {
                document.getElementById("ViewSchedule").style.width = "100%";
                document.getElementById("ViewSchedule").style.height = "550px";
            }
            else {
                document.getElementById("ViewSchedule").style.width = "80%";
                document.getElementById("ViewSchedule").style.height = "550px";
            }

            window.location.hash = '#ViewSchedule';
        }
        function HidePopup() {
            document.getElementById("AddMeeting").style.visibility = "hidden";
            document.getElementById("ViewMeeting").style.visibility = "hidden";
            document.getElementById("ViewSchedule").style.visibility = "hidden";
        }

        //var LnkNewMeeting = document.getElementById('LnkNewMeeting');
        //        var LnkViewMeeting = document.getElementById('LnkViewMeeting');
        var close1 = document.getElementById('close1');
        var close2 = document.getElementById('close2');
        var close3 = document.getElementById('close3');

        //var close = document.getElementById('close');
        close1.onclick = function () {
            HidePopup();
            location.reload();
        };
        close2.onclick = function () {
            HidePopup();
            location.reload();
        };
        close3.onclick = function () {
            HidePopup();
            location.reload();
        };

        function detectmob() {
            if (window.innerWidth <= 800 || window.innerHeight <= 500) {
                return true;
            } else {
                return false;
            }
        }        
    </script>
			

            <!--bday popup -->
            
       <div id="divPopups" runat="server"> 
 <div id="overlay" class="overlay" ></div>
<div id="boxpopup" class="box">
	<a onclick="closeOffersDialog('boxpopup');" class="boxclose"></a>
	
	<div style="width:100%;min-height:198px;float:left;position:relative;">
            <div style="width:100%;float:left;position:relative;padding:0px 3px;">
            <img src="Images/birthday.jpg" class="img-responsive;>
            </div>
            <div style="width:100%;float:left;position:relative;margin:1px 0px;"><p style="color:Maroon; font-size:14px; font-weight:bold;">Below employees are celebrating their birthday today. Wish them all the best.</p>
            <p></p>
            <div runat="server" id="divEmpBday" style="color:Orange; font-size:13px; font-weight:bold;"></div>
            </div>
            </div>
	
</div>
</div>
            <!-- Bday popup end-->
	<!--Feedback Form HTML START -->   
	<div id="mrova-feedback">		
		<div id="mrova-form">
			<form id="mrovacontactform" method="post" runat="server">
            <div>
                  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset>
             <div class="alert-box success" id="divSuccess" runat="server" style="width:370px; left:1px;">Thank you.  We'hv received your message.</div>
<div class="alert-box error" id="divError" runat="server" style="width:370px; left:1px;">Your suggestion/feedback could not send to its recepients. Please try later.</div>

            <h3>Suggestion/Feedback</h3>
						<label>Your Name*</label><asp:TextBox ID="txtEmpName" runat="server"  CssClass="form-control" Width="370px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="fld" runat="server" ControlToValidate="txtEmpName" ErrorMessage="Enter Your Name" ></asp:RequiredFieldValidator>
<br />
						<label>Suggestion/Feedback On*</label> 
                        <asp:DropDownList runat="server" ID="ddlSuggestionType" CssClass="form-control" Width="370px">
                        <asp:ListItem Text="--Choose--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                        <asp:ListItem Text="HR Policies" Value="HR Policies"></asp:ListItem>                        
						<asp:ListItem Text="Culture & Events" Value="Culture & Events"></asp:ListItem>                        
						<asp:ListItem Text="Rewards & Recognitions" Value="Rewards & Recognitions"></asp:ListItem>                        						
						<asp:ListItem Text="Corporate Ethics" Value="Corporate Ethics"></asp:ListItem>                        						
						<asp:ListItem Text="Products or Services" Value="Products or Services"></asp:ListItem>                        						
						<asp:ListItem Text="Training Programmes" Value="Training Programmes"></asp:ListItem>   
									<asp:ListItem Text="Cafe Services" Value="Cafe Services"></asp:ListItem>   
									<asp:ListItem Text="Transportations" Value="Transportations"></asp:ListItem>   
									<asp:ListItem Text="Work–life balance" Value="Work–life balance"></asp:ListItem>   
									<asp:ListItem Text="HR Programmes" Value="HR Programmes"></asp:ListItem>   
						
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSuggestionType" ErrorMessage="Choose a Type" InitialValue=""></asp:RequiredFieldValidator>
					<br />
						<label>Your Suggestion/Feedback*</label>
                        <asp:TextBox ID="txtFeedback" runat="server" TextMode="MultiLine" Rows="8" Columns="30"  CssClass="form-control" Width="370px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFeedback" ErrorMessage="Enter Detailed description"></asp:RequiredFieldValidator>
					<br />
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="1"
                                            AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <img border="0" src="Images/progress1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                <asp:Button ID="btnSendFeedback" runat="server" Text="Send" 
                    class="btn btn-success"  onclick="btnSendFeedback_Click" Width="200px" />
                     </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>    
			</form>
		</div>
		<div id="mrova-img-control"></div>
	</div>     
	<!-- Feedback Form HTML END -->

    <script type="text/javascript">
        $('.dropdown-toggle').dropdown();
        $(".navbar-toggle").on('click', function () { $('#navbar-collapse-grid').toggle(); });
    </script>
</body>
</html>
