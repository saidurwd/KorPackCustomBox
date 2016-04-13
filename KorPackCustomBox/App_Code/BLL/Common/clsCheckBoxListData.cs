using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ComboDate
/// </summary>
namespace Blumen
{
    public class clsCheckBoxListData : DAL.BaseClass
    {
        String strSql,strWhere;
        public clsCheckBoxListData(): base(ConfigurationManager.AppSettings["Cnn"])
        {
            strSql = "";
            strWhere = "";
        }
        public void FillCheckBoxList(CheckBoxList chkName,string tblName,string field1,string field2,string whereCond,string orderByField,string orderAscDesc)
        {
            strSql = "Select " + field1 + "," + field2 + " from " + tblName ;
            if (whereCond.Length > 0)
            {
                strWhere = " where "+whereCond;
                strSql = strSql + strWhere;
            }
            if(orderByField.Length > 0)
            {
                strSql = strSql + " order by " + orderByField + " " + orderAscDesc;
            }
            DataTable dt= this.ExecuteSQLStringDataTable(strSql);            
            chkName.DataSource = dt;
            chkName.DataTextField = field1;
            chkName.DataValueField = field2;
            chkName.DataBind();

        }
    }
}
