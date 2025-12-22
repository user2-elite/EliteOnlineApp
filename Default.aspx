<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Elite Intraweb - Login</title>
    <!-- Bootstrap core CSS -->
    <link href="css/simplex/bootstrap.min.css" rel="stylesheet">
    <!-- custom css -->
    <link href="css/custom.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
		<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
		<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
		<![endif]-->
</head>
<body class="login-body">
    <div class="container login-container">
        <div class="row login-box">
            <div class="col-md-12 clear-padd">
                <div class="col-sm-12 col-md-7">
                    <h2 class="form-signin-heading">
                        <img src="img/logo.png" class="img-responsive center-block" alt="" title=""></h2>
                </div>
                <div class="col-sm-12 col-md-4 pull-right clear-padd">
                    <form id="Form1" runat="server" class="form-signin">
                    <h4>
                        Sign In to Your Account</h4>
                    <hr>                    
                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="User ID"
                        required autofocus></asp:TextBox>
                    <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" CssClass="form-control"
                        placeholder="Password" required></asp:TextBox>
                    <div class="checkbox">
                        <!--<label>
                            <input type="checkbox" value="remember-me">
                            Remember me                            
                        </label>
                        -->
                        <asp:Label ID="lblLoginError" runat="server" CssClass="Error-style"></asp:Label>
                    </div>
                    <asp:Button CssClass="btn btn-lg btn-success btn-block" runat="server" ToolTip="Sign in"
                        Text="Sign in" ID="btn1" OnClick="btn1_Click" />
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- /container -->
</body>
</html>
