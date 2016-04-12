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

    public class ExecuteSQL : DAL.BaseClass
    {
        string strSQL
               ;
        public ExecuteSQL()
            : base(ConfigurationManager.AppSettings["Cnn"])
        {
            strSQL = "";
        }
        #region Report Execution
        public DataTable SearchReportRecordFromSQLQuery(string strSQL)
        {


            //DataTable dt = this.ExecuteSQLStringDataTable(strSQL);


            return this.ExecuteSQLStringDataTable(strSQL);


        }
        public object ExecuteQuery(string strSQL)
        {
            return this.ExecuteSQLStringScalar(strSQL);
        }
        public DataSet ExecuteQueryDataset(string strSQL)
        {
            return this.ExecuteSQLStringDataSet(strSQL);
        }
        public DataSet RecordFromStoredProcedure1Param(string Param1Name, string Param1, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));

            return this.ExecuteSPDataSet(strStoredProcedurename, arrParam);

        }
        public DataTable RecordFromStoredProcedure3Param(string Param1Name, string Param2Name, string Param3Name, string Param1, string Param2, string Param3, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));

            return this.ExecuteStoredProcedureDataTable(strStoredProcedurename, arrParam);

        }
        public DataTable RecordFromStoredProcedure4Param(string Param1Name, string Param2Name, string Param3Name, string Param4Name, string Param1, string Param2, string Param3, string Param4, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));
            arrParam.Add(new SqlParameter(Param4Name, Param4));

            return this.ExecuteStoredProcedureDataTable(strStoredProcedurename, arrParam);

        }
        public DataTable RecordFromStoredProcedure5Param(string Param1Name, string Param2Name, string Param3Name, string Param4Name, string Param5Name, string Param1, string Param2, string Param3, string Param4, string Param5, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));
            arrParam.Add(new SqlParameter(Param4Name, Param4));
            arrParam.Add(new SqlParameter(Param5Name, Param5));

            return this.ExecuteStoredProcedureDataTable(strStoredProcedurename, arrParam);

        }
        public DataTable RecordFromStoredProcedure6Param(string Param1Name, string Param2Name, string Param3Name, string Param4Name, string Param5Name, string Param6Name, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));
            arrParam.Add(new SqlParameter(Param4Name, Param4));
            arrParam.Add(new SqlParameter(Param5Name, Param5));
            arrParam.Add(new SqlParameter(Param6Name, Param6));

            return this.ExecuteStoredProcedureDataTable(strStoredProcedurename, arrParam);

        }


        public DataSet RecordFromStoredProcedure5ParamDataSet(string Param1Name, string Param2Name, string Param3Name, string Param4Name, string Param5Name, string Param1, string Param2, string Param3, string Param4, string Param5, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));
            arrParam.Add(new SqlParameter(Param4Name, Param4));
            arrParam.Add(new SqlParameter(Param5Name, Param5));

            return this.ExecuteSPDataSet(strStoredProcedurename, arrParam);

        }
        public DataSet RecordFromStoredProcedure6ParamDataSet(string Param1Name, string Param2Name, string Param3Name, string Param4Name, string Param5Name, string Param6Name, string Param1, string Param2, string Param3, string Param4, string Param5, string Param6, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));
            arrParam.Add(new SqlParameter(Param4Name, Param4));
            arrParam.Add(new SqlParameter(Param5Name, Param5));
            arrParam.Add(new SqlParameter(Param6Name, Param6));

            return this.ExecuteSPDataSet(strStoredProcedurename, arrParam);

        }
        public DataSet RecordFromStoredProcedure4ParamDataSet(string Param1Name, string Param2Name, string Param3Name, string Param4Name, string Param1, string Param2, string Param3, string Param4, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));
            arrParam.Add(new SqlParameter(Param4Name, Param4));


            return this.ExecuteSPDataSet(strStoredProcedurename, arrParam);

        }
        public DataSet RecordFromStoredProcedure3ParamDataSet(string Param1Name, string Param2Name, string Param3Name, string Param1, string Param2, string Param3, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));
            arrParam.Add(new SqlParameter(Param3Name, Param3));

            return this.ExecuteSPDataSet(strStoredProcedurename, arrParam);

        }
        public DataSet RecordFromStoredProcedure2ParamDataSet(string Param1Name, string Param2Name, string Param1, string Param2, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));

            return this.ExecuteSPDataSet(strStoredProcedurename, arrParam);

        }
        public DataTable RecordFromStoredProcedure2Param(string Param1Name, string Param2Name, string Param1, string Param2, string strStoredProcedurename)
        {

            ArrayList arrParam = new ArrayList();
            arrParam.Add(new SqlParameter(Param1Name, Param1));
            arrParam.Add(new SqlParameter(Param2Name, Param2));

            return this.ExecuteStoredProcedureDataTable(strStoredProcedurename, arrParam);

        }

        public string GetMonthName(int month, bool abbrev)
        {
            DateTime date = new DateTime(1900, month, 1);
            if (abbrev) return date.ToString("MMM");
            return date.ToString("MMMM");
        }
        public int getMonthNumber(string monthName)
        {
            switch (monthName.ToLower())
            {
                case "january":
                    return 1;
                case "february":
                    return 2;
                case "march":
                    return 3;
                case "april":
                    return 4;
                case "may":
                    return 5;
                case "june":
                    return 6;
                case "july":
                    return 7;
                case "august":
                    return 8;
                case "september":
                    return 9;
                case "october":
                    return 10;
                case "november":
                    return 11;
                case "december":
                    return 12;
                default:
                    return 0;
            }
        }
        #endregion
    }
}
