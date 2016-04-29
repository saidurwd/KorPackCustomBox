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
/// Summary description for clsListData
namespace Blumen
{
    public class clsListData : DAL.BaseClass
    {
        String strSql, strWhere;
        public clsListData() : base(ConfigurationManager.AppSettings["Cnn"])
        {
            strSql = "";
            strWhere = "";
        }
        public void FillList( ListBox lstName, string tblName, string field1, string field2, string whereCond, string orderByField, string orderAscDesc)
        {
            strSql = "Select " + field1 + "," + field2 + " from " + tblName;
            if (whereCond.Length > 0)
            {
                strWhere = " where " + whereCond;
                strSql = strSql + strWhere;
            }
            if (orderByField.Length > 0)
            {
                strSql = strSql + " order by " + orderByField + " " + orderAscDesc;
            }
            DataTable dt = this.ExecuteSQLStringDataTable(strSql);
            lstName.DataSource = dt;
            lstName.DataTextField = field1;
            lstName.DataValueField = field2;
            lstName.DataBind();

        }
        public void FillListStringQry(ListBox lstName, string strSQL,string lstTextField,string lstTextValue)
        {
            DataTable dt = this.ExecuteSQLStringDataTable(strSQL);
            lstName.DataSource = dt;
            lstName.DataTextField = lstTextField;
            lstName.DataValueField = lstTextValue;
            lstName.DataBind();
        }
        public void FillDropDownListStringQry(DropDownList lstName, string strSQL, string lstTextField, string lstTextValue)
        {
            lstName.Items.Clear();
            DataTable dt = this.ExecuteSQLStringDataTable(strSQL);
            if (dt.Rows.Count > 0)
            {
                lstName.DataSource = dt;
                lstName.DataValueField = lstTextValue;
                lstName.DataTextField = lstTextField;
                lstName.DataBind();
            }
            else
            {
                ListItem li = new ListItem();
                li.Value = "0";
                li.Text = "----No Project Created----";
                lstName.Items.Add(li);
                lstName.DataBind();

            }
        }
    }
}
