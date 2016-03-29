using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using Blumen;

/// <summary>
/// Summary description for BSVarietySync
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class BSVarietySync : System.Web.Services.WebService
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string strCompanyID = string.Empty;
    public BSVarietySync()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    
    

    

    

    
    

}
