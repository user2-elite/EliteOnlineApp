<%@ Page Language="C#" MasterPageFile="~/Masterpage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="_Home" Title="HelpDesk"
    ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
     <style>
        .black_overlay1{
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index:1000;
            -moz-opacity: 0.8;
            opacity:.50;
            filter: alpha(opacity=50); shadow(color:#8f8f8f, strength:13, direction:135);
        }
        .black_overlay{
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index:99;
            -moz-opacity: 0.8;
            opacity:.50;
            filter: alpha(opacity=50);
        }
        .white_content {
            display: none;
            position: absolute;
             top: -25px;
            left: -5px;
            width: 100%;
            height: 100%;
            padding: 15px;
            z-index:100;
            border: 10px solid #B94629;
            background-color: #E89F65;
            overflow:hidden;
        }       
    </style>
<script language="Javascript">
    function OpenWindow() {
        document.getElementById('light').style.display = 'block';
        document.getElementById('fade').style.display = 'block';
    }

    function WindowClose() {
        document.getElementById('light').style.display = 'none';
        document.getElementById('fade').style.display = 'none';
        //document.location.reload(true);
        //document.location.redirect('home.aspx');
    }
</script>
    <asp:ScriptManager ID="mng1" runat="server">
    </asp:ScriptManager>

    <div id="fade" class="black_overlay"></div>    
      <div id="light" class="white_content">      
      <div style="width:100%; vertical-align:top; text-align:left; position:absolute; top:-40px; left: -20px;">
      <div style="width:100%; vertical-align:top; text-align:right;top:45px;right:30px; position:absolute;"><a href = "javascript:void(0)" onclick = "javascript:WindowClose();"><img src="Images/c1.gif" border="0" /></a></div> 
      <iframe width="102%" height="550px" id="newWin" runat="server" scrolling="auto" frameborder="0" style="padding:0px;"></iframe>
      </div>
       </div>          
                        <asp:Panel id="PanelUser" runat="server" visible="false" >
                        <div id="CssMenu1">
                                <asp:LinkButton ID="LogRequest" runat="server" CausesValidation="False" OnClick="LogRequest_Click">Create New HelpDesk Request</asp:LinkButton>
                            <asp:LinkButton ID="ViewHistory" runat="server" CausesValidation="False" OnClick="ViewHistory_Click">View Request History</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClick="LogOff_Click">Log off</asp:LinkButton>
                        </div>
                        <div style="vertical-align:top; text-align:left; position:absolute; top:0px; left: 0px; font-weight:bold; color:#000000; font-size:13px; background-color:#E3DEC1; width:100%; height:20px;"  runat="server" id="divWelcome"></div>
            </asp:Panel>

<asp:Panel ID="admChnageHelpdesk" runat="server" Visible="false">
<div style="vertical-align:top; text-align:left; position:absolute; top:-10px; left: 10px;" runat="server" id="divChangehdesk">
                            <br /><asp:Label ID="Label1" Text="Change HelpDesk" runat="server" Font-Bold="true"></asp:Label>
    <br />
 <asp:DropDownList ID="ddChangeHelpDesk" runat="server" OnSelectedIndexChanged="ddChangeHelpDesk_SelectedIndexChanged" AutoPostBack="true">                                                                                
 </asp:DropDownList>
</div>
 </asp:Panel>                           

<br /><br /><br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <table align="center" id="OpenRequeststbl" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="top">
                <table width="100%" align="center" cellpadding="0" cellspacing="0">
                    <tr>    
                        <td align="left" colspan="3" valign="top">
                            <table width="100%" bgcolor="#E89F65"  cellpadding="3" cellsapcing="3">
                            <tr><td>
                            <strong><span style="font-size:17px ; color: Black;">New Request</span></strong>
                            </td></tr>
                            <div id="divHelpdeskArea" runat="server" >
                                <tr>
                                    <td width="20%">
                                        Help Desk Area
                                    </td>
                                    <td width="80%">
                                        <asp:DropDownList ID="ddHelpDesk" runat="server" ValidationGroup="G1">                                        
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="ddHelpDesk" ErrorMessage="Required" InitialValue="0"  ValidationGroup="G1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </div>
                                <tr runat="server" id="trWorkGrp" visible="false">
                                    <td>
                                        Select Category
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWorkGroupCategory" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblWGCat" runat="server" EnableViewState="False" ForeColor="Red" Text="*"
                                            Visible="False"></asp:Label>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <h3>Request/Issue Title</h3>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Subject" runat="server" Width="470px" MaxLength="60" ValidationGroup="G1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="Subject" ErrorMessage="Required" ValidationGroup="G1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <h3>Problem Description</h3>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Probelm" runat="server" Height="150px" TextMode="MultiLine" Width="470px" ValidationGroup="G1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="Probelm" ErrorMessage="Required" ValidationGroup="G1"></asp:RequiredFieldValidator><br />
                                        <asp:LinkButton ID="lnkAttachFiles" runat="server" CausesValidation="False" OnClick="lnkAttachFiles_Click"
                                            Font-Size="Medium" ><B>Upload if any supporting files</B></asp:LinkButton><br /><br />                         
                                        <asp:ImageButton ID="Submit" runat="server" ImageUrl="~/Images/cr.gif" OnClick="Submit_Click" ValidationGroup="G1" />
                                    </td>
                                </tr>                                
                            </table>                           
                        </td>                       
                    </tr>
                </table>
                <table id="imageviewtbl" runat="server" width="50%" border="1" style="z-index: 100;
                    left: 180px; position: absolute; top: 180px; height:100px; background-color:ThreeDFace;" cellpadding="8">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:Button ID="UploadFile" runat="server" OnClick="UploadFile_Click" Text="Upload File"
                                CausesValidation="False"/>
                            &nbsp;&nbsp;<asp:ImageButton ID="btnClosefilewindow" runat="server" ImageUrl="images/c1.gif"
                                OnClick="btnClosefilewindow_Click" AlternateText="Close Window" CausesValidation="False" />

                        <br /><br />
                            &nbsp;<asp:GridView ID="GridforDataTable" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="No records available." BackColor="White" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="99%"
                                OnRowCommand="GridforDataTable_RowCommand">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="FileID" HeaderText="File ID" ReadOnly="True" SortExpression="FileID">
                                        <ItemStyle Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FileName1" HeaderText="File Name" SortExpression="FileName1">
                                        <ItemStyle Width="70%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn1" runat="server" CommandArgument='<%#Eval("FileID")%>' CommandName="Deletefile"
                                                CausesValidation="False" Text="Delete" />
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#B94629" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <AlternatingRowStyle BorderColor="#CCCCCC" BorderStyle="Solid"
                                    BorderWidth="1px" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                                ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>"></asp:SqlDataSource>
                            &nbsp;
                        </td>
                    </tr>
                </table>
               </td>
                </tr> 
               </table>
<table width="100%" bgcolor="#E89F65">
<tr>
                                    <td colspan="2">
                                        <strong><span style="font-size:16px ; color: Black;">Pending Requests</span></strong>
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataKeyNames="RequestID" DataSourceID="SqlDataSource1" EmptyDataText="No Pending Requests"
                                            Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                                            CellPadding="3" PageSize="5" EnableModelValidation="True" OnRowCommand="GridView1_RowCommand">
                                            <Columns>                                                
                                                <asp:BoundField DataField="RequestID" HeaderText="RequestID" SortExpression="RequestID">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="10%" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="RequestSubject" HeaderText="Request Title" SortExpression="RequestSubject">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="55%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="StatusName" HeaderText="Request Status" SortExpression="StatusName">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="25%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="View Request">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" Text="View" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("RequestID") %>' />
                                                </ItemTemplate>
                                           </asp:TemplateField>
                                            </Columns>
                                            <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#B94629" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <AlternatingRowStyle BorderColor="#FF0000" BorderStyle="Solid"
                                    BorderWidth="1px" />

                                        </asp:GridView>                                        
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                                                ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetActiveRequestsByEmployee"
                                                SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="RegisteredBy" SessionField="UserID"
                                                        Type="string" />
                                                    <asp:ControlParameter ControlID="ddHelpDesk" DefaultValue="0" Name="SelectedHID"
                                                        PropertyName="SelectedValue" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </span>
                                    </td>
                                </tr>

</table>

    <table align="center" id="RequestHistorytbl" runat="server" style="width: 81%;">
        <tr>
            <td align="center" style="text-align: left; height: 18px;">
                <strong><span style="color: black; font-size: 10pt;">&nbsp;</span><span style="color: #666666;
                    font-size: 10pt;">Closed Requests</span></strong>
            </td>
        </tr>
        <tr>
            <td align="left">
                <strong><span style="color: darkblue"></span></strong>
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="RequestID" DataSourceID="SqlDataSource2" EmptyDataText="No records available."
                    Width="99%" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" OnRowCommand="GridView2_RowCommand" AllowSorting="True"
                    PageSize="15" EnableModelValidation="True">
                    <Columns>                       
                        <asp:BoundField DataField="RequestID" HeaderText="RequestID" SortExpression="RequestID">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RequestSubject" HeaderText="Subject" SortExpression="RequestSubject">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="60%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="View Request">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" runat="server" Text="View" Font-Bold="True" CommandName="View" CommandArgument='<%#Eval("RequestID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                                <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <HeaderStyle BackColor="#B94629" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <AlternatingRowStyle BorderColor="#CCCCCC" BorderStyle="Solid"
                                    BorderWidth="1px" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HelpDesk %>"
                    ProviderName="<%$ ConnectionStrings:HelpDesk.ProviderName %>" SelectCommand="GetALLClosedRequestsByEmployee"
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="RegisteredBy" SessionField="UserID"
                            Type="string" />
                        <asp:ControlParameter ControlID="ddHelpDesk" DefaultValue="0" Name="SelectedHID"
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>   
    <br />
</asp:Content>
