<%@ Page Title="" Language="C#" MasterPageFile="~/InnerPage.master" AutoEventWireup="true"
    CodeFile="management.aspx.cs" Inherits="management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Management Team</h3>
     <div>
       <iframe width="900px" height="700px" frameborder="0" scrolling="no" src="ProfileView.aspx"></iframe>
    </div>


   <%-- <div>
        <div class="pure-form pure-form-stacked" style="width: 90%; margin: 0px; padding: 10px;
            border: 0px;">
            <div class="pure-g" runat="server" id="divList">
            </div>
        </div>
        <br />       
    </div>--%>
 <%--   <script language="javascript">
        var ControlCount = <%=ProfileCount %>;                 

         $('.e0').click(function () {
            $('.exp0').slideToggle('slow');
        });

        $('.e1').click(function () {
            $('.exp1').slideToggle('slow');
        });

        $('.e2').click(function () {
            $('.exp2').slideToggle('slow');
        });

         $('.e3').click(function () {
            $('.exp3').slideToggle('slow');
        });

         $('.e4').click(function () {
            $('.exp4').slideToggle('slow');
        });

         $('.e5').click(function () {
            $('.exp5').slideToggle('slow');
        });

         $('.e6').click(function () {
            $('.exp6').slideToggle('slow');
        });

         $('.e7').click(function () {
            $('.exp7').slideToggle('slow');
        });

         $('.e8').click(function () {
            $('.exp8').slideToggle('slow');
        });

         $('.e9').click(function () {
            $('.exp9').slideToggle('slow');
        });

         $('.e10').click(function () {
            $('.exp10').slideToggle('slow');
        });

           for(count = 0; count < ControlCount; count++)
         {              
//            $('.e'+ count).click(function () 
//            {
//                $('.exp'+ count).slideToggle('slow');
//            });
            $('.exp'+ count).slideToggle('slow');
         }

    </script>--%>
</asp:Content>
