//SETTING UP OUR POPUP
//0 means disabled; 1 means enabled;
var popupStatus = 0;
var popupName = '';

//loading popup with jQuery magic!
function loadPopup(currentpopup) {
    //loads popup only if it is disabled
    $("#backgroundPopup").css({
        "opacity": "0.7"
    });
    //$("#backgroundPopup").fadeIn("slow");
    $("#backgroundPopup").fadeIn();
    $(currentpopup).css({"z-index": "100" });
    //$(currentpopup).fadeIn("slow");
   $(currentpopup).show(); 
}

//disabling popup with jQuery magic!
function disablePopup(currentpopup) {
    //disables popup only if it is enabled
    $(currentpopup).hide();
    //$("#backgroundPopup").fadeOut("slow");
    $("#backgroundPopup").hide();
    
}

//centering popup
function centerPopup(currentpopup) {
    //request data for centering
    var windowWidth = document.documentElement.clientWidth;
    var windowHeight = document.documentElement.clientHeight;
    var popupHeight = $(currentpopup).height();
    var popupWidth = $(currentpopup).width();
    //centering
    $(currentpopup).css({
        "position": "absolute",
        "top": windowHeight / 2 - popupHeight / 2,
        "left": windowWidth / 2 - popupWidth / 2
    });
    //only need force for IE6

    $("#backgroundPopup").css({
        "height": windowHeight
    });
}