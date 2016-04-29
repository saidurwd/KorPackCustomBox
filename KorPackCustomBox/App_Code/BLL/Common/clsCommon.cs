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
/// Summary description for clsCommon
/// </summary>
public class clsCommon : DAL.BaseClass
{
    static string strBtn;
    string strSQL;
	public clsCommon()
        : base(ConfigurationManager.AppSettings["Cnn"])
    {
        strSQL = "";
    }
    public void set(string strVal)
    {
        strBtn = strVal;
    }
    public string get()
    {
        return strBtn;
    }

    public object CreateDatabaseBackup(string dataBaseName, string backUpFileName, string backUpDescription)
    {
        strSQL = @" BACKUP DATABASE " + dataBaseName + @" 
	                TO  DISK = '" + backUpFileName + @"'
                    WITH 
	                NOFORMAT, 
	                NOINIT,  
	                NAME = N'" + backUpDescription + @"', 
	                SKIP, 
	                STATS = 10; ";
        return this.ExecuteSQLStringScalar(strSQL);
    }
}
