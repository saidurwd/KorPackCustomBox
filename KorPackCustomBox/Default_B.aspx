<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_B.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=ConfigurationManager.AppSettings["PageTitle"]%></title>
    
    <script language="JavaScript"><!--
function full(url) {
    if (document.all) {
        window.open(url,'windowName','fullscreen=yes');
        return false;
    }
    return true;
}

function fullScreen()
{
    var site = window.open('Login.aspx','homepage','fullscreen=yes');
		
		if (site==null || typeof(site)=="undefined") 
		{
			alert("It seems you have a popup blocker which is preventing this site from opening. Please check the top of the browser, whether there is a message saying the site has been blocked. You need to allow it.");
		} 

		if( site != null ) site.focus();
}
//--></script>

</head>
<body > 
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td valign="top">
                <iframe  runat="server"  height="605" width="100%" id="iFrame1" src="Login.aspx" scrolling="no">
                </iframe>
            </td>
        </tr>
        <tr>
            <td valign="top" style="font-weight: bold; font-size: xx-small; color: white; font-family: Verdana; background-color: darkblue" align="center">
             All Rights Reserved (c) Aphix Inc.
            </td>
        </tr>
        
    </table>
    </form>
</body>
</html>
