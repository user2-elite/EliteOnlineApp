<%@ Page Language="C#" AutoEventWireup="true" CodeFile="policy.aspx.cs" Inherits="policy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <iframe src="IT_Security_Policy.mht" width="700px" height="500px"></iframe>
    </div>
    <div>
        <asp:CheckBox runat="server" ID="chkvalidate" Text="I Agree IT Security Policy" 
            Font-Bold="true" oncheckedchanged="chkvalidate_CheckedChanged" AutoPostBack="true" />  
            <br /><br />
            <a href="javascript:window.close()">Click here to close the window  </a>            
    </div>
    </form>
</body>
</html>
