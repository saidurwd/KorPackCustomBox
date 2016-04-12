

//=====================Global Detection of Script========================
var url = document.location.toString(); //url
var e_url = ''; //edited url
var p = 0; //position
var p2 = 0; //position 2
var p3 = 0; //position 3
p = url.indexOf("//");
e_url = url.substring(p + 2);
p2 = e_url.indexOf("/");
newe_Url = e_url.substring(p2 + 1);
p3 = newe_Url.indexOf("/");
var root_url = url.substring(0, p + p2 + p3 + 4);
root_url = root_url + "Scripts/";

function requireCSS(jspath) {
    //alert(jspath);

}
function require(jspath) {
    jspath = root_url + jspath;                                
    //alert(jspath);
    document.write('<script type="text/javascript" src="' + jspath + '"><\/script>');
}

//requireCSS('so');
require("jquery-1.4.2.min.js");
require("jquery-ui-1.8.custom.min.js");
//require("jquery.cookies.2.0.1.min.js");
require("thickbox-compressed.js");
//require("thickbox.js");
require("jquery.corners.min.js");
require("jquery.watermark.min.js");
require("CommonJS.js");
require("jquery.quicksearch.js");


    





