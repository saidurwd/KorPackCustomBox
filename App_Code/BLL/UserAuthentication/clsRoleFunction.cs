//********************************************************
// Author : Faruk Ahmed
// Development Date : 5 November 2006
// Module : User Authentication
//********************************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using Blumen;

/// <summary>
/// Summary description for clsRoleFunction
/// </summary>
public class clsRoleFunction: DAL.BaseClass
{
    string strSQL;
	public clsRoleFunction(): base(ConfigurationManager.AppSettings["Cnn"])
	{
		//
		// TODO: Add constructor logic here
		//
        strSQL = "";
	}

    public object InsertData(string AssignedTo, string RoleID, string CreatedBy, string Splitter)
    {
        ArrayList arrParam = new ArrayList();
        arrParam.Add(new SqlParameter("@AssignedTo", AssignedTo));
        arrParam.Add(new SqlParameter("@RoleID", RoleID));
        arrParam.Add(new SqlParameter("@AssignedBy", CreatedBy));
        arrParam.Add(new SqlParameter("@Splitter", Splitter));

        return this.ExecuteStoredProcedureScalar("SP_TB_InsertRoleFunction", arrParam);

    }

   

    public object DeleteRec(string strFuncID, string strRoleID)
    {
        strSQL = "Delete from TB_PrmRoleFunction where RoleID='"+strRoleID+"' and FuncID IN ("+strFuncID+")";
        return this.ExecuteSQLStringScalar(strSQL);
    }


    public DataTable SelectData(string strSQL)
    {

        return this.ExecuteSQLStringDataTable(strSQL); 
    
    }

}
