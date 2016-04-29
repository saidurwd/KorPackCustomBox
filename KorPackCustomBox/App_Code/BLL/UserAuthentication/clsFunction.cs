//********************************************************
// Author : Faruk Ahmed
// Development Date : 1 November 2006
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

namespace Blumen
{
    public class clsFunction:DAL.BaseClass
    {
       // private string strSql;

        #region Constructor
        public clsFunction() : base(ConfigurationManager.AppSettings["Cnn"])
        {
         //   strSql = "";
        }
        #endregion

        #region Insert Data
        public object InsertRec( string FuncDesc,string ParentID,string TargetPage,string CreatedBy)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(new SqlParameter("@FuncDesc", FuncDesc));
            arrParams.Add(new SqlParameter("@ParentID",ParentID));
            arrParams.Add(new SqlParameter("@TargetPage", TargetPage));
            arrParams.Add(new SqlParameter("@CreatedBy", CreatedBy));

            return this.ExecuteStoredProcedureScalar("SP_InsertFunction", arrParams);
        }
        #endregion

        #region Update Data

        public object UpdateRec(string FuncID,string FuncDesc,string ParentID,string TargetPage)
        {
            ArrayList arrParams = new ArrayList();

            arrParams.Add(new SqlParameter("@FuncID", FuncID));
            arrParams.Add(new SqlParameter("@FuncDesc", FuncDesc));
            arrParams.Add(new SqlParameter("@ParentID", ParentID));
            arrParams.Add(new SqlParameter("@TargetPage",TargetPage));
            
            return this.ExecuteStoredProcedureScalar("SP_UpdateFunction", arrParams);

        }
        #endregion

        #region Delete Data

        public object DeleteRec(string strSQL)
        {
            return this.ExecuteSQLStringScalar(strSQL);
        }

        #endregion
        
        #region Select Data

        public DataTable SelectRec(string strSQL)
        {
            return this.ExecuteSQLStringDataTable(strSQL);
        }
        #endregion

    }
}
