function openBirthdayDialog() {
	$('#overlay').fadeIn('fast', function() {
	    $('#boxpopup').css('display', 'block');
	    
        $('#boxpopup').animate({'left':'30%'},500);
    });
}


function closeOffersDialog(prospectElementID) {
	$(function($) {
		$(document).ready(function() {
		    $('#' + prospectElementID).css('position', 'absolute');
		    $('#' + prospectElementID).css('opacity', '0.5');
			$('#' + prospectElementID).animate({'left':'-100%'}, 500, function() {
				$('#' + prospectElementID).css('position','fixed');
				$('#' + prospectElementID).css('left','100%');
				$('#overlay').fadeOut('fast');
			});
		});
	});
}

function openAnniversaryDialog() {
    $('#overlay').fadeIn('fast', function () {
        $('#boxpopup1').css('display', 'block');

        $('#boxpopup1').animate({ 'left': '30%' }, 500);
    });
}


function closeAnniversaryDialog(prospectElementID) {
    $(function ($) {
        $(document).ready(function () {
            $('#' + prospectElementID).css('position', 'absolute');
            $('#' + prospectElementID).css('opacity', '0.5');
            $('#' + prospectElementID).animate({ 'left': '-100%' }, 500, function () {
                $('#' + prospectElementID).css('position', 'fixed');
                $('#' + prospectElementID).css('left', '100%');
                $('#overlay').fadeOut('fast');
            });
        });
    });
}

