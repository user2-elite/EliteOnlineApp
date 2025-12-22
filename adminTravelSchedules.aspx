<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true"
    CodeFile="adminTravelSchedules.aspx.cs" Inherits="adminTravelSchedules" ValidateRequest="false" Trace="false"%>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .custom-date-style
        {
            background-color: red !important;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h3>Travel Schedule</h3>
    <p>Update Top Management Travel schedules here. Page always update the latest travel schedules, hence history will not be maintained.</p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset>
    <div>
        <div class="pure-form pure-form-stacked" style="width: 100%; margin: 0px; padding: 10px;
            border: 0px;">
            <div class="pure-g">                
                  <div class="pure-u-1 pure-u-md-1-1">
                    Enter Details
<%--
 <FTB:FreeTextBox ID="txtTravelPurpose" runat="server"  Height="100px"                                                                
                                                                Width="100%">
                                                            </FTB:FreeTextBox>--%>
                    <FTB:FreeTextBox id="txtDetails"
			toolbarlayout="Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|BulletedList,NumberedList|Cut,Copy,Paste,Delete;Undo,Redo,Print|InsertTable" 
            runat="Server" EnableHtmlMode="false" BackColor="#FFFFFF" Height="350" Width="700" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDetails"
                                        ErrorMessage="Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                  </div>

                    <div class="pure-u-1 pure-u-md-1-1">
                                    <br />
                                    <div style="position: absolute; width: 800px; left: 200px;">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="1"
                                            AssociatedUpdatePanelID="UpdatePanel1">
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
                                    <asp:Button ID="btnSubmit" runat="server" Text="Save Details" class="btn btn-success"
                                        OnClick="btnSubmit_Click" />
                                </div>
            </div>
        </div>
    </div>
      </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
