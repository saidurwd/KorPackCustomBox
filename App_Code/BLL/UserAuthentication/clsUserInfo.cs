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
    public class clsUserInfo : DAL.BaseClass
    {
        private string strSQL;

        #region Construsctor
        public clsUserInfo()
            : base(ConfigurationManager.AppSettings["Cnn"])
        {
            strSQL = "";
        }
        #endregion

        #region Insert Data
        public object InsertRec(string strLoginID, string strPass, string strUserRole, string strEmployee, string strCreatedBy, string strcompanyID)
        {
            ArrayList arrParams = new ArrayList();

            arrParams.Add(new SqlParameter("@LoginID", strLoginID));
            arrParams.Add(new SqlParameter("@Password", strPass));
            arrParams.Add(new SqlParameter("@RoleID", strUserRole));
            arrParams.Add(new SqlParameter("@EmpAutoID", strEmployee));
            arrParams.Add(new SqlParameter("@CreatedBy", strCreatedBy));
            arrParams.Add(new SqlParameter("@COMPANY_ID", strcompanyID));

            return this.ExecuteStoredProcedureScalar("SP_InsertUserInfo", arrParams);
        }
        #endregion

        #region Update Data

        public object UpdateRec(string strLoginID, string strPass, string strUserRole, string strEmployee, string strUserID, string strCreatedBy, string strUserActive,string strCompanyID)
        {
            strSQL = "UPDATE  TB_PrmUserInfo " +
                    " SET LoginID='" + strLoginID + "', Password='" + strPass + "', RoleID=" + strUserRole + ", EmpAutoID=" + strEmployee + ",CreatedBy=" + strCreatedBy + ",Active='" + strUserActive + "'" + " , Company_ID = '" +strCompanyID+"'" + 
                    " Where UserID=" + strUserID + " and Company_ID = " + strCompanyID;

            return this.ExecuteSQLStringScalar(strSQL);

            //return this.ExecuteStoredProcedureScalar("SP_UpdateUserInfo", arrParams);

        }

        public object SetPassword(string strLoginID, string strPass)
        {
            strSQL = @"UPDATE TB_PrmUserInfo 
                     SET Password='" + strPass + "' Where LoginID='" + strLoginID + "'";

            return this.ExecuteSQLStringScalar(strSQL);

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

        public DataTable GetUserInfoByEmailID(string LoginID)
        {
            strSQL = @"SELECT UserName, Email FROM tb_PrmUserInfo WHERE LoginID='" + LoginID + "'";
            return this.ExecuteSQLStringDataTable(strSQL);
        }
        public DataTable PopulateUserInfo()
        {
            strSQL = @"select * from(
                    Select isnull(RoleDesc,'') RoleDesc,convert(varchar,RoleID) RoleID 
                    from TB_PrmUserRole R
                    UNION Select '---All---',0 from TB_PrmUserRole 
                    ) tbl
                    order by RoleDesc";
            return this.ExecuteSQLStringDataTable(strSQL);
        }

        public DataTable PopulateEmpByDesc(string DesgID)
        {
            strSQL = @"SELECT ISNULL(E.FIRSTNAME,'')+' '+ISNULL(E.LASTNAME,'')+' '+ISNULL(E.MIDDLENAME,'') EmpName
                    ,CONVERT(VARCHAR,U.USERID)+'ô'+U.LOGINID ULogin
                    FROM TB_PrmUserInfo U
                    LEFT JOIN HRM_TB_EmployeeInfo E ON U.EmpAutoID=E.EmpAutoID
                    WHERE E.DESGID=" + DesgID +
                    @" UNION SELECT '--select employee name--' EmpName,'0' ULogin";
            return this.ExecuteSQLStringDataTable(strSQL);
        }

        #endregion
        #region Check Duplicate

        public object CheckDuplicate(string LoginID)
        {
            strSQL = @"SELECT COUNT(*) FROM tb_PrmUserInfo WHERE LoginID='" + LoginID + "'";
            return this.ExecuteSQLStringScalar(strSQL);
        }
        #endregion

    }
}
