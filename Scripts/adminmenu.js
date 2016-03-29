function gwCall(method, argStr) {
    var gwProxy = window.document.getElementById('gwProxy');
    if (gwProxy) {
        gwProxy.setAttribute('gwMethod', method);
        gwProxy.setAttribute('gwArgStr', argStr);
        var e = document.createEvent('MouseEvents');
        e.initEvent('click', true, true);
        gwProxy.dispatchEvent(e);
    }
}

function jsCall() {
    var jsProxy = document.getElementById('jsProxy');
    if (jsProxy) {
        var jsCode = jsProxy.getAttribute('jsCode');
        eval(jsCode);
    }
}

function Gateway() {
    var _P4r4m5_ = {};
    this.addParam = function(name, value) {
        _P4r4m5_[escape(name)] = escape(value);
    }

    this.callName = function(callName) {
        var paramStr = '';
        for (name in _P4r4m5_) {
            paramStr = paramStr +
                          ((paramStr == '') ? '' : '&') +
                          name + '=' + _P4r4m5_[name];
        }
        gwCall(callName, paramStr);
    }
}

function showIFrame(name, url) {
    frames[name].location.href = url;
}

function createSrcScriptElement(srcPath) {
    var js = document.createElement('script');
    js.setAttribute('type', 'text/javascript');
    js.setAttribute('src', srcPath);
    document.getElementsByTagName('head')[0].appendChild(js);
}

function createInlineScriptElement(escapedJsCode) {
    try {
        var js;
        if (document.standardCreateElement)
            js = document.standardCreateElement('script');
        else
            js = document.createElement('script');
        js.setAttribute('type', 'text/javascript');
        js.text = unescape(escapedJsCode);
        document.getElementsByTagName('head')[0].appendChild(js);
    }
    catch (e) {
        //alert(document.createElement);                        
        //alert('ERROR: createInlineScriptElement(): '+e);      
    }
}

function invokeInGuiThread(callName, argPtr) {
    var gwObj = new Gateway();
    gwObj.addParam('argPtr', argPtr);
    gwObj.callName(callName);
}                           