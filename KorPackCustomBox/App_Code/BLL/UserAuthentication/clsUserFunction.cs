
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


public class clsUserFunction : DAL.BaseClass
{
    string strSQL;
    public clsUserFunction() : base(ConfigurationManager.AppSettings["Cnn"])
    {
        //
        strSQL = "";
        // TODO: Add constructor logic here
        //
    }

    public object InsertData(string AssignedTo, string UserID, string CreatedBy, string Splitter, string strCompanyID)
    {
        ArrayList arrParam = new ArrayList();
        arrParam.Add(new SqlParameter("@AssignedTo", AssignedTo));
        arrParam.Add(new SqlParameter("@UserID", UserID));
        arrParam.Add(new SqlParameter("@AssignedBy", CreatedBy));
        arrParam.Add(new SqlParameter("@Splitter", Splitter));
        arrParam.Add(new SqlParameter("@COMPANY_ID", strCompanyID));

        return this.ExecuteStoredProcedureScalar("SP_TB_InsertUserFunction", arrParam);


    }
    public object InsertUserPermission(string AssignedTo, string UserID, string CreatedBy, string Splitter, string strCompanyID)
    {
        ArrayList arrParam = new ArrayList();
        arrParam.Add(new SqlParameter("@AssignedTo", AssignedTo));
        arrParam.Add(new SqlParameter("@UserID", UserID));
        arrParam.Add(new SqlParameter("@AssignedBy", CreatedBy));
        arrParam.Add(new SqlParameter("@Splitter", Splitter));
        arrParam.Add(new SqlParameter("@COMPANY_ID", strCompanyID));

        return this.ExecuteStoredProcedureScalar("SP_TB_InsertUserPermission", arrParam);


    }

    public object DeleteRec(string strAutoID,string strUserID)
    {
        strSQL = "delete from TB_PrmUserFunction where UserID='" + strUserID + "'and FuncID IN(" + strAutoID + ")";
        return this.ExecuteSQLStringScalar(strSQL);
    }

    public DataTable SelectData(string strSQL)
    {
        return this.ExecuteSQLStringDataTable(strSQL);
    }

    public DataTable GetAssignedFunctionByUser(string strUserID,string strModule)
    {
        strSQL = @"SELECT * FROM(
                Select Distinct T.UserID,T.FuncID,T.FuncDesc,ISNULL(FA.FuncID,0) AssignedFuncID,ISNULL(FA.UserID,0) AssignedUserID,ISNULL(T.Project,0) Project,T.module,T.PageName From 
                (select u.userid,fn.funcid,fn.funcdesc,fn.Project,fn.module,fn.PageName from ASTINV_TB_Function FN,TB_PrmUserInfo u ) T --ORDER BY USERID
                Left Join ASTINV_TB_FunctionAuthentication FA ON T.USERID=FA.USERID and T.Funcid=FA.FuncID
                WHERE T.USERID="+strUserID+@" AND T.Module='"+strModule+@"'
                ) tblFunc WHERE tblFunc.AssignedFuncID>0 AND tblFunc.AssignedUserID>0 ORDER BY tblFunc.FuncDesc ASC";
        return this.ExecuteSQLStringDataTable(strSQL);
    }
}
