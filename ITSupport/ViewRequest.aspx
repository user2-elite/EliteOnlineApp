<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRequest.aspx.cs" Inherits="ViewRequest"
    Title="HelpDesk" ValidateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HelpDesk</title>
    <link href="styles/layout.css" rel="stylesheet" type="text/css" />
    <style>
    
    .body
    {
    	font-size:9px;
    }
    #ReqHeaderItemBG
    {
    	background-color:#FFFFFF;    	
    }        
    
    #ReqHeaderItemBG TR
    {
    	background-color:#FFCC99;
    }
    .style1
    {
        width: 100%;
    }
        
    #TableContent
    {
    	background-color:#FFFFFF;
    	border: 1px; color:#FFFFFF;
    	font-size:8px;
    }        
    
    #TableContent TR
    {
    	background-color:#4C3327;
    }
    .style1
    {
        width: 100%;
    }  
    </style>
    <script language="JavaScript" type="text/javascript">
        var nameCtrl = new ActiveXObject("Name.NameCtrl");

    </script>
    <script language="javascript" type="text/javascript">
    <!--
        function HideWindows() {
            //    document.getElementById('Requesthistorydiv').style.visibility = 'hidden'; 
            document.getElementById('ViewattachedTable').style.visibility = 'hidden';
        }

        function AttachedfilesShow() {
            document.getElementById('ViewattachedTable').style.visibility = 'visible';
        }

        function btnClosefilewindow() {
            document.getElementById('ViewattachedTable').style.visibility = 'hidden';
        }

        function WindowClose_onclick() {
            window.close();
            parent.opener.location.reload();
        }
// -->
    </script>
</head>
<body onload="HideWindows()" bgcolor="#E89F65" style="text-align: center" id="PopupcontentBG" topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <table border="0" width="100%" align="center" cellspacing="2" cellpadding="1" id="ReqHeaderItemBG">                
                <tr>
                    <td align="left" colspan="4" style="height: 17px">
                        <table cellpadding="3" cellspacing="1" id="tableContent" width="100%">
                            <tr>
                                <td colspan="2" style="background-color:#E89F65">
                                    <strong>Employeee Details</strong></td>
                                <td colspan="2" style="background-color:#E89F65">
                                    <strong>Request Status</strong></td>                             
                            </tr>
                            <tr>
                                <td>
                                    <strong>Name :</strong>&nbsp;<asp:Label ID="EmpNameLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>EmpCode :</strong>&nbsp;<asp:Label ID="EmpNoLB" runat="server"></asp:Label>
                                    &nbsp;&nbsp;</td>
                                <td>
                                    <strong>Status :</strong>&nbsp;<asp:Label ID="StatusLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>Severity :</strong>&nbsp;<asp:Label ID="SeverityLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                               
                            </tr>
                            <tr>
                                <td>
                                    <strong>Phone :</strong>&nbsp;<asp:Label ID="ExtnLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>Email : </strong>
                                    <asp:Label ID="EmailLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>Created Date :</strong>&nbsp;<asp:Label ID="CreateDateLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>Last Modified On:</strong>&nbsp;
                                    <asp:Label ID="LBLastupdatedDate" runat="server"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td>
                                    <strong>Department :</strong>
                                    <asp:Label ID="DepartmentLB" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <strong>Location :</strong>
                                    <asp:Label ID="LocationLB" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <strong>Responded By :</strong>&nbsp;<asp:Label ID="AttendedByLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>Responded Date : </strong>
                                    <asp:Label ID="AttendedDateLB" runat="server"></asp:Label>
                                    <strong>&nbsp;</strong>&nbsp;</td>
                              
                            </tr>
                            <tr>
                                <td>
                                    <strong>Area : </strong>
                                    <asp:Label ID="AreaLB" runat="server"></asp:Label>
                                    <strong>&nbsp;</strong>&nbsp;
                                </td>
                                <td>
                                    <asp:LinkButton ID="LNTranscationHistory" runat="server" 
                                        CausesValidation="False" Font-Bold="True" ForeColor="MidnightBlue" 
                                        OnClick="LNTranscationHistory_Click">Transaction History</asp:LinkButton></td>
                                <td>
                                    <strong>Support Staff :</strong>&nbsp;<asp:Label ID="AssignedToLB" runat="server"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    <strong>Assigned Date&nbsp;: </strong>
                                    <asp:Label ID="AssignedDateLB" runat="server"></asp:Label>
                                    &nbsp;&nbsp;</td>
                               
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblTimeLeftForResolution" runat="server" Font-Bold="True" 
                                        Text="Time left for resolution :"></asp:Label>
                                    &nbsp;<asp:Label ID="TimeLeftForResolution" runat="server"></asp:Label>
                                    &nbsp;<asp:Label ID="lblHrs" runat="server" Text="Hrs"></asp:Label>
                                </td>
                                <td>
                                    <strong>Response Violated :</strong>&nbsp;<asp:Label ID="Responsetimeviolationlb" 
                                        runat="server"></asp:Label>
                                </td>
                                <td>
                                    <strong>Resolution Violated :</strong>&nbsp;&nbsp;<asp:Label 
                                        ID="Resolutiontimeviolationlb" runat="server"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <strong>Exp. Response :</strong>&nbsp;<asp:Label ID="ResponseBy" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <strong>Exp. Resolution :</strong>&nbsp;<asp:Label ID="ResolutionBy" runat="server"></asp:Label>
                                </td>
                             
                            </tr>                           
                            <tr>
                                <td colspan="4" style="background-color:#E89F65">
                                    <strong>Request Details</strong></td>
                              
                            </tr>
                            <tr>
                                <td colspan="4">                                    
                                    <strong>Request Number: <asp:Label ID="RequestIDLB" runat="server"></asp:Label></strong>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <strong>Issue/Request Title :</strong>&nbsp;<asp:Label ID="SubjectLB" runat="server"></asp:Label>
                                </td>
                               
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Issue/Request Description</strong>: <div id="divRequestDesc" runat="server" style="width:800px;"></div>
                                    <asp:Label ID="ViewAttachedFiles" runat="server" Font-Bold="True" 
                                        Font-Underline="True" ForeColor="MidnightBlue" 
                                        onclick="javascript:AttachedfilesShow()" Text="View/upload attachments"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="ErrorLabel1" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                                </td>
                              
                            </tr>                                                    
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <div id="Requesthistorydiv" runat="server" visible="false">
                        <asp:Panel ID="pnlth" runat="server" GroupingText="Transaction History" Font-Bold="true">
                            <table width="100%" border="0" style="border-color:#FFFFFF; background-color:#FFFFFF" cellspacing="0">
                                <tr>
                                    <td align="right" style="height: 17px">
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>">
                                        </asp:SqlDataSource>
                                        <asp:ImageButton ID="CloseHistory" runat="server" ImageUrl="~/images/c1.gif"
                                            Width="15px" Height="15px" OnClick="CloseHistory_Click" CausesValidation="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4"
                                            EmptyDataText="No records." BorderColor="#400040" BorderStyle="Solid"
                                            BorderWidth="1px" CellPadding="4" ForeColor="#FFFFFF" GridLines="None" Width="99%"
                                            PageSize="4" CellSpacing="1" AllowPaging="True" OnPageIndexChanging="GridView4_PageIndexChanging"
                                            OnRowDataBound="GridView4_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" ItemStyle-Font-Bold="false"/>
                                                <asp:BoundField DataField="AssignedTo" HeaderText="Assigned To" SortExpression="AssignedTo" ItemStyle-Font-Bold="false"/>
                                                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" ItemStyle-Font-Bold="false"/>
                                                <asp:BoundField DataField="Severity" HeaderText="Severity" SortExpression="Severity" ItemStyle-Font-Bold="false"/>                                                
                                                <asp:BoundField DataField="HDate" HeaderText="Date" SortExpression="HDate" ItemStyle-Font-Bold="false"/>            
                                                <asp:BoundField DataField="RequestMessage" HeaderText="Message" SortExpression="RequestMessage" ItemStyle-Font-Bold="false" />                                                                                   
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#C7A317" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                            <AlternatingRowStyle BackColor="WhiteSmoke" BorderColor="#E89F65" BorderStyle="Solid"
                                                BorderWidth="1px" />
                                            <PagerSettings PageButtonCount="4" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                         </asp:Panel>
                        </div>
                        <div>
                        <table width="400px" align="left" cellspacing="1" cellpadding="1" border="0">
                            <tr>
                                <td align="left">
                                    <strong>Resolution Group</strong>
                                    <br />
                                    <asp:DropDownList ID="DDGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <strong>Category</strong>
                                <br />
                                    <asp:DropDownList ID="RequestType1DD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RequestType1DD_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <!--
                                <td align="left">
                                    <strong>Sub Category</strong>
                                <br />
                                    <asp:DropDownList ID="RequestType2DD" runat="server">
                                    </asp:DropDownList>
                                </td>
                                -->
                                <td align="left">
                                    <strong>Severity</strong>
                                <br />
                                    <asp:DropDownList ID="SeverityDD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SeverityDD_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            <td align="left">
                                    <strong>Request Type</strong>
                                <br />
                                    <asp:DropDownList ID="RequestCategory" runat="server">
                                        <asp:ListItem Value="0">---Select----</asp:ListItem>
                                        <asp:ListItem Value="1">Incident</asp:ListItem>
                                        <asp:ListItem Value="2">Request</asp:ListItem>
                                    </asp:DropDownList>
                                </td>   
                                 <td align="left">
                                    <strong>Status</strong>
                                <br />
                                    <asp:DropDownList ID="RequestStatusDD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RequestStatusDD_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>  
                                 <td align="left">
                                    <strong>Assign To</strong>
                                <br />
                                    <asp:DropDownList ID="AssignToDD" runat="server">
                                    </asp:DropDownList>
                                </td>           
                            </tr>
                            <tr>                                                                                                                                                                          
                                <td align="left" colspan="7">
                                    <asp:CheckBox ID="CBsendMessage" runat="server" AutoPostBack="True" Text="Send Message to user"
                                        Font-Bold="True" OnCheckedChanged="CBsendMessage_CheckedChanged" />
                                </td>
                            </tr>
                            <tr id="Responseviolationtr" runat="server">
                                <td align="left" colspan="7">
                                    <strong>Response Violation Reasons</strong>
                                <br />
                                    <asp:TextBox ID="ResponseViolationText" runat="server" Height="55px" TextMode="MultiLine"
                                        Width="550px" AutoCompleteType="Disabled" ValidationGroup="SaveRequest"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ResponseViolationText"
                                        ErrorMessage="Required" ValidationGroup="SaveRequest" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="Resolutionviolationtr" runat="server">
                                <td align="left" colspan="7">
                                    <strong>Resolution Violation Reasons</strong>
<br />
                                    <asp:TextBox ID="ResolutionViolationText" runat="server" AutoCompleteType="Disabled"
                                        Height="55px" TextMode="MultiLine" Width="550px"></asp:TextBox>
                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ResolutionViolationText"
                                        ErrorMessage="Required" ValidationGroup="SaveRequest" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr id="Requestclosuredestr" runat="server">
                               <td align="left" colspan="3">
                                    <strong>Resolution&nbsp; Details</strong>
                                <br />
                                    <asp:TextBox ID="ClosureDesc" runat="server" Height="55px" TextMode="MultiLine" Width="550px"
                                        ValidationGroup="SaveRequest"></asp:TextBox>&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ClosureDesc"
                                        ErrorMessage="Required" ValidationGroup="SaveRequest" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" colspan="4">
                                        <strong>Actual Hours/Minuits taken to complete the request</strong>
                                <br />Hour&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                Minuits<br />
                                <asp:TextBox MaxLength="3" ID="txtHours" Width="40px" runat="server">0</asp:TextBox>
                                <asp:DropDownList ID="ddlMins" runat="server">
                                <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                </asp:DropDownList>
                                </td>
                            </tr>
                                                                                 
                            <tr runat="server" id="MessageTr">
                                <td align="left" colspan="7">
                                    <table width="100%">
                                        <tr id="UI_TRID_RequestAcceptance" runat="server" visible="false">
                                            <td align="right" valign="bottom">
                                                <b>Resulotion is</b>
                                            </td>
                                            <td align="left" valign="bottom">
                                                <asp:RadioButtonList ID="AcceptRejectRB" runat="server" RepeatDirection="Horizontal"
                                                    Style="margin-left: 1px">
                                                    <asp:ListItem Value="Accepted">Accepted </asp:ListItem>
                                                    <asp:ListItem Value="Rejected">Rejected </asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="LbAcceptReject" runat="server" EnableViewState="False" ForeColor="Red"
                                                    Text="Select Accpet/Reject Options" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="UI_TRID_RequestMessages" runat="server">
                                            <td align="right">
                                                <strong>
                                                    <asp:Label ID="MessageLabel" runat="server" Width="175px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 40px" align="left">
                                                <asp:TextBox ID="MessageText" runat="server" AutoCompleteType="Disabled" Height="55px"
                                                    MaxLength="350" TextMode="MultiLine" Width="500px" ValidationGroup="GroupMessage"></asp:TextBox>
                                                &nbsp;<asp:Button ID="MessageButton" runat="server" Text="Button" OnClick="MessageButton_Click"
                                                    ValidationGroup="GroupMessage" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="MessageRequiredvalidator" runat="server" ControlToValidate="MessageText"
                                        ErrorMessage="Required" ValidationGroup="GroupMessage" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                                    <br />
                                </td>
                            </tr>

                            <tr runat="server" id="RateRequestTR" visible="false">
                                <td align="left" colspan="7">
                                <table>
                                        <tr>
                                            <td valign="middle" style="width: 52px">
                                                <strong><span style="font-size: 8pt">Rating :</span></strong>
                                            </td>
                                            <td valign="middle">
                                                <asp:RadioButtonList ID="UserSatisfaction" runat="server" RepeatDirection="Horizontal"
                                                    ValidationGroup="GraoupRating">
                                                    <asp:ListItem Value="1">Not Satisfied</asp:ListItem>
                                                    <asp:ListItem Value="2">Satisfied</asp:ListItem>
                                                    <asp:ListItem Value="3">Extremely Satisfied</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td valign="middle">
                                                <asp:Label ID="LBRatingError" runat="server" EnableViewState="False" ForeColor="Red"
                                                    Text="*" Visible="False"></asp:Label>
                                            </td>
                                            <td valign="middle">
                                                <span style="font-size: 8pt"><strong>Feedback :</strong></span>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="FeedbackText" runat="server" TextMode="MultiLine" Width="289px"
                                                    ValidationGroup="GraoupRating" Height="54px"></asp:TextBox>
                                            </td>
                                            <td valign="middle">
                                                <asp:Button ID="UserRating" runat="server" CausesValidation="False" Text="Rate this Request"
                                                    OnClick="UserRating_Click" ValidationGroup="GraoupRating" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                          
                                <tr>
                                <td align="left" colspan="7">
                                    <asp:Button ID="SaveRequest" runat="server" OnClick="SaveRequest_Click" Text="Save" Width="100px" ValidationGroup="SaveRequest" />
                                    <br /><br />
                                </td>
                            </tr>
                                            <tr>
                                <td align="left" colspan="7">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" CancelSelectOnNullParameter="False"
                                        ConnectionString="<%$ ConnectionStrings:HelpDesk %>" DataSourceMode="DataReader">
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
               
            </table>
        <div style="position:absolute; left:100px;top:200px;">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                    DisplayAfter="1" DynamicLayout="true">
                    <ProgressTemplate>
                        <img src="Images/progress1.gif" border="0" alt="Progressing"/>
                        <span class="style10">Please wait...! </span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <table id="ViewattachedTable" runat="server" border="1" bordercolor="#FFFFFF" bgcolor="#F2EFE4"
                            cellpadding="5"
                            width="40%" cellspacing="0" class="style55">
                            <tr>
                                <td align="center" width="90%">
                                    <strong><span style="font-size: 8pt">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                                        <asp:Button ID="UploadFile" runat="server" CausesValidation="False" Font-Size="8pt"
                                            OnClick="UploadFile_Click" Text="Upload" /><asp:SqlDataSource ID="SqlDataSource2"
                                                runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>" ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>">
                                            </asp:SqlDataSource>
                                    </span></strong>
                                </td>
                                <td align="right" valign="top">
                                    <asp:Image ID="Attachfilestableclose" runat="server" ImageUrl="~/images/c1.gif"
                                        OnClick="javascript:btnClosefilewindow()" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:GridView ID="ImagesGrid" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        DataKeyNames="RequestFileNo" DataSourceID="SqlDataSource5" EmptyDataText="No files found"
                                        ForeColor="#333333" GridLines="None" OnRowCommand="ImagesGrid_RowCommand"
                                        PageSize="2" BorderColor="#D0D0D0" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1"
                                        Width="95%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn2" runat="server" CommandArgument='<%#Eval("FileID")%>' CommandName="Viewfile"
                                                        CausesValidation="false" Text='<%#Eval("FileName1")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" Font-Bold="True" />
                                        <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#C7A317" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <AlternatingRowStyle BackColor="WhiteSmoke" BorderColor="WhiteSmoke" BorderStyle="Solid"
                                            BorderWidth="1px" />
                                        <PagerSettings PageButtonCount="5" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                                        ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetFilesByRequestID"
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="RequestIDLB" DefaultValue="0" Name="RequestID" PropertyName="Text"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
    </form>
</body>
</html>
