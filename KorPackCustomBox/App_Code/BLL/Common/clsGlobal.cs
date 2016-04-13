using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsGlobal
/// </summary>
public class clsGlobal
{
    public string PageTitle;

    private string strLogin;
    static string v="";
    private static string strClickedButton;

    public clsGlobal()
	{
		//
		// TODO: Add constructor logic here
		//
       // PageTitle = ":: Little Jewels Nursery Infant & Junior School ::";
        
	}

    public string  setTitle(string tt)
    {
        PageTitle = tt;
        return PageTitle;
    }

    public void setV(string t)
    {
        v =t;
     }
    public string getV()
    {
        return v;
    }

    public void setClickedV(string t)
    {
        strClickedButton = t;
    }
    public string getClickedV()
    {
        return strClickedButton;
    }


    public void setLogin(string t)
    {
        strLogin = t;
    }
    public string getLogin()
    {
        return strLogin;
    }
}
