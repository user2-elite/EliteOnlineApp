<%@ Page Title="" Language="C#"  AutoEventWireup="true"
    CodeFile="ProfileView.aspx.cs" Inherits="ProfileView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">    
    <title></title>
   		<link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.css">
<script src="http://code.jquery.com/jquery-1.11.3.min.js"></script>
<script src="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
</head>
<body>
<div data-role="page" id="pageone">
  <div data-role="main" class="ui-content">
    <ul data-role="listview" data-inset="true">
      <li data-role="divider">Major Entities - Click on the arrow to view management team of each sector</li>
      <li>
        <a href="#" style="height:80px;">
        <img src="images/corporate.jpg" class="img-rounded">
        <h2>Elite Corporate</h2>
        <p>www.eliteindia.com</p>
        </a>
         <a href="#myPopup1" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>
      <li>
        <a href="#" style="height:80px;">
        <img src="images/fmcg.jpg" class="img-rounded" />
        <h2>Food Processing</h2>
        <p>www.eliteindia.com</p>
        </a>
         <a href="#myPopup2" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>
       <li>
        <a href="#" style="height:80px;">
        <img src="Images/itrade.jpg" class="img-rounded" />
        <h2>International Trade</h2>
        <p>www.elitegreen.in</p>
        </a>
         <a href="#myPopup7" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>
       <li>
        <a href="#" style="height:80px;">
        <img src="Images/jewel.jpg" class="img-rounded" />
        <h2>Jewel Country</h2>
        <p>www.jewelcountry.in</p>
        </a>
         <a href="#myPopup3" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>

       <li style="text-align:center">
        <a href="#" style="height:80px;">
        <img src="Images/re.jpg" class="img-rounded" />
        <h2>Real Estate</h2>    
        <p>www.elitedevelopers.co.in</p>    
        </a>
         <a href="#myPopup4" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>

       <li>
        <a href="#" style="height:80px;">
        <img src="images/solgen.jpg" class="img-rounded" />
        <h2>Solar Energy</h2>        
        <p>www.solgenindia.com</p>
        </a>
         <a href="#myPopup5" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>

       <li>
        <a href="#" style="height:80px;">
        <img src="Images/org.jpg">
        <h2>Organic</h2>
        <p>www.elitegreen.in</p>
        </a>
         <a href="#myPopup6" data-rel="popup" class="ui-btn ui-btn-inline ui-corner-all">Show Management Team</a>
      </li>
    </ul>
 

      <link href="css/bootstrap.min.css" rel="stylesheet">
  <!-- Popup Section -->
 <div data-role="popup" id="myPopup1" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup1" runat="server"></div>
    </div>
  
 <div data-role="popup" id="myPopup2" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup2" runat="server"></div>
    </div>

 <div data-role="popup" id="myPopup3" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup3" runat="server"></div>
    </div>

 <div data-role="popup" id="myPopup4" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup4" runat="server"></div>
    </div>

 <div data-role="popup" id="myPopup5" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup5" runat="server"></div>
    </div>

 <div data-role="popup" id="myPopup6" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup6" runat="server"></div>
    </div>
  </div>

  
 <div data-role="popup" id="myPopup7" class="ui-content" data-dismissible="false" style="width:700px;max-width:850px;margin-top:3px; margin-right:500px;">
      <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn ui-icon-delete ui-btn-icon-notext ui-btn-right">Close</a>
      <div id="divmyPopup7" runat="server"></div>
    </div>
  </div>
  <!-- Popup Section end-->
</div> 

</body>
</html>

