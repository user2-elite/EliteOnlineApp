<%@ Import Namespace="System.Web.Mail"%>
<form method=get>
   <p>To: <input type="text" name="to"></p>
   <p>From: <input type="text" name="from"></p>
   <p>How many emails? <input type="text" name="loops" value="1"></p>
   <input type="submit" value="Submit">
</form>
<%
   dim MailTo
   dim MailFrom
   dim NumberOfMails
   dim i
   i = 1
   MailTo = request("to")
   MailFrom = request("from")
   NumberOfMails = Convert.ToInt16(request("loops"))
   if MailTo <> "" then
   For i = 1 to NumberOfMails
      Dim objMail As New System.Web.Mail.MailMessage()
      objMail.From = MailFrom
      objMail.To = MailTo
      objMail.Subject = "Test email " & i
      objMail.BodyFormat = MailFormat.Html 'MailFormat.Text to send plain text email
      objMail.Priority = MailPriority.High
      objMail.Body =   "This test email was sent at: " & Now()
      
      System.Web.Mail.SmtpMail.SmtpServer = "relay-hosting.secureserver.net"
      System.Web.Mail.SmtpMail.Send(objMail)
      objMail = Nothing
   Next i
   response.write("mail sent")
   end if
%>