

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


/// <summary>
/// Summary description for clsLogin
/// </summary>
namespace Blumen
{
    public class clsPrmUserRole : DAL.BaseClass
    {
        string strSQL;
        public clsPrmUserRole()
            : base(ConfigurationManager.AppSettings["Cnn"])
        {
            strSQL = "";
        }
        public DataTable GetUserRole()
        {
            strSQL = @"SELECT RoleID as UserRoleCode,RoleDesc as UserRoleName
							FROM TB_PrmUserRole order by RoleID Desc";

            return this.ExecuteSQLStringDataTable(strSQL);
        }

        public Object InsertRec(string strUserRoleDesc)
        {
            ArrayList arrParams = new ArrayList();
            arrParams.Add(new SqlParameter("@RoleDesc", strUserRoleDesc));
            return this.ExecuteStoredProcedureScalar("SP_InsertPrmUserRole", arrParams);
        }

        public object UpdateRec(string strAutoID, string strRoleDesc)
        {
            strSQL = "UPDATE TB_PrmUserRole SET RoleDesc='" + strRoleDesc + "' WHERE RoleID=" + strAutoID;
            return this.ExecuteSQLStringScalar(strSQL);
        }

        public object DeleteRec(string strAutoID)
        {
            strSQL = "Delete from TB_PrmUserRole where RoleID IN (" + strAutoID + ")";
            return this.ExecuteSQLStringScalar(strSQL);
        }

    }
}
