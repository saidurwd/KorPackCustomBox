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
    public class clsComboData : DAL.BaseClass
    {
        String strSql,strWhere;
        public clsComboData() : base(ConfigurationManager.AppSettings["Cnn"])
        {
            strSql = "";
            strWhere = "";
        }
        public void FillCombo(DropDownList cboName,string tblName,string field1,string field2,string whereCond,string orderByField,string orderAscDesc)
        {
            strSql = "Select Distinct convert(varchar," + field1 + ") as "+ field1+"," + field2 + " from " + tblName ;

            cboName.Items.Clear();
            if (whereCond.Length > 0)
            {
                strWhere = " where " + whereCond;
                strSql = strSql + strWhere;
            }
            //string unionCond = " UNION Select '<Select>',convert(varchar,0) from " + tblName;
            string unionCond = "";

            if (orderByField.Length > 0)
            {
                strSql = strSql + unionCond + " order by " + orderByField + " " + orderAscDesc;
            }
            else
            {
                strSql = strSql + unionCond;
            }

            DataTable dt= this.ExecuteSQLStringDataTable(strSql);            
            cboName.DataSource = dt;
            cboName.DataTextField = field1;
            cboName.DataValueField = field2;
            cboName.DataBind();


        }

        public void FillComboAB(DropDownList cboName, string tblName, string field1, string field2, string whereCond, string orderByField, string orderAscDesc)
        {
            strSql = "Select " + field1 + " A," + field2 + " B from " + tblName;

            cboName.Items.Clear();
            if (whereCond.Length > 0)
            {
                strWhere = " where " + whereCond;
                strSql = strSql + strWhere;
            }
           // string unionCond = " UNION Select ' ',convert(uniqueidentifier,0) from " + tblName;

            if (orderByField.Length > 0)
            {
                strSql = strSql + " order by " + orderByField + " " + orderAscDesc;
            }
            
            DataTable dt = this.ExecuteSQLStringDataTable(strSql);
            cboName.DataSource = dt;
            cboName.DataTextField = "A";
            cboName.DataValueField = "B";
            cboName.DataBind();

        }

        public void FillComboWithBlankRowCaption(DropDownList cboName, string tblName, string field1, string field2, string whereCond, string orderByField, string orderAscDesc,string strCaption)
        {
            strSql = "Select Distinct convert(varchar," + field1 + ") as " + field1 + "," + field2 + " from " + tblName;

            cboName.Items.Clear();
            if (whereCond.Length > 0)
            {
                strWhere = " where " + whereCond;
                strSql = strSql + strWhere;
            }

            string unionCond = " UNION Select '" + strCaption + "',convert(varchar,0)";

            if (orderByField.Length > 0)
            {
                strSql = strSql + unionCond + " order by " + orderByField + " " + orderAscDesc;
            }
            else
            {
                strSql = strSql + unionCond;
            }

            DataTable dt = this.ExecuteSQLStringDataTable(strSql);
            cboName.DataSource = dt;
            cboName.DataTextField = field1;
            cboName.DataValueField = field2;
            cboName.DataBind();

        }


        //---- EXAMPLE CALL
        //obCombo.FillComboByCondition(this.ddlSection, "STD_TB_PrmClass", "STD_TB_PrmSection", "SectionName"
        //,"AutoSectionID", "AutoClassID", "STD_TB_PrmSection.AutoClassID="+ClassID);

        public void FillComboByCondition(DropDownList cboName, string tblParent, string tblChild
            , string field1, string field2,string relationField, string whereCond,bool BlankRowYesNo,string BlankRowCaption)
        {
            strSql = @"Select Distinct convert(varchar," + tblChild + "." + field1 + ") as " + field1 + "," + tblChild + "." + field2 + " as " + field2
                + " FROM " + tblChild
                + " LEFT JOIN " + tblParent + " ON " + tblChild + "." + relationField + "=" + tblParent + "." + relationField;

            cboName.Items.Clear();

            if (whereCond.Length > 0)
            {
                strWhere = " where " + whereCond;
                strSql = strSql + strWhere;
            }

            if (BlankRowYesNo == true)
            {
                strSql = strSql + " UNION Select '" + BlankRowCaption + "',0";
            }

            strSql = @"SELECT * FROM ( " + strSql + ") TBL ORDER BY TBL." + field1;

            DataTable dt = this.ExecuteSQLStringDataTable(strSql);
            cboName.DataSource = dt;
            cboName.DataTextField = field1;
            cboName.DataValueField = field2;
            cboName.DataBind();

        }

        public DataTable GetEmpFullNameByDesg(string DesgID, string strCompanyID)
        {
            if (DesgID != "0")
            {
                strSql = @"SELECT ISNULL(LastName,'')+' '+ISNULL(FirstName,'')+' '+ISNULL(MiddleName,'') EmpName,EmpAutoID FROM HRM_TB_EmployeeInfo WHERE COMPANY_ID='" + strCompanyID + "' and FirstName!='Super Admin' AND DesgID=" + DesgID + @" ORDER BY LastName,FirstName,MiddleName";
            }
            else
            {
                //strSql = @"SELECT ISNULL(LastName,'')+' '+ISNULL(FirstName,'')+' '+ISNULL(MiddleName,'') EmpName,EmpAutoID FROM HRM_TB_EmployeeInfo WHERE FirstName!='Super Admin' ORDER BY LastName,FirstName,MiddleName";
                strSql = @"SELECT dbo.fn_getEmpName(EmpAutoID) EmpName ,EmpAutoID FROM HRM_TB_EmployeeInfo WHERE COMPANY_ID='" + strCompanyID + "' and FirstName!='Super Admin' ORDER BY FirstName,MiddleName,LastName";
            }
            return this.ExecuteSQLStringDataTable(strSql);
        }

        public DataTable GetDonarInfo()
        {
            strSql = @"SELECT ISNULL(D.BCompany,'') DonarName,D.DirID DonarID
                        FROM Dir_Info D
                        LEFT JOIN DIR_Category C ON D.CatID=C.CatID
                        WHERE C.CatCode='donar' ORDER BY D.BCompany";
            return this.ExecuteSQLStringDataTable(strSql);
        }

        
    }

    
}
