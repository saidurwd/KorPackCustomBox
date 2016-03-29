Sys.Browser.WebKit = {}; //Safari 3 is considered WebKit
if (navigator.userAgent.indexOf('WebKit/') > -1) {
    Sys.Browser.agent = Sys.Browser.WebKit;
    Sys.Browser.version = parseFloat(
        navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
    Sys.Browser.name = 'WebKit';
}

Sys.Application.add_init(AppInit);

function AppInit(sender) {
// initiate begin and end request handlers
    if (Sys.WebForms != undefined) {
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    }
    
};

function BeginRequestHandler(sender, args) {
//alert('begin');
}

function EndRequestHandler(sender, args) {
    //alert('end');
    $('.cssTableAddSpecial input[type=text]').val("");
    $('.cssTableAddSpecial input[type=text]:eq(0)').focus();
//makeClear();
}