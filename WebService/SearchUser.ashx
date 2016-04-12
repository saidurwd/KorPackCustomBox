<%@ WebHandler Language="C#" Class="SearchUser" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;


public class SearchUser : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    public void ProcessRequest(HttpContext context)
    {
       
        //-------------------- =Declaration Section ----------------------------
        context.Response.ContentType = "text/plain";
        string returnText = "";
        string defaultText = "[{\"id\":\"0\",\"label\":\"No Record Found\",\"value\":\"No Record Found\"}]";
        string prefixText = context.Request.QueryString["term"];
        
        DataTable dt = new DataTable();
        
        //-------------------- =Cache Checking ---------------------------------
        if (context.Cache["CacheUser"] != null)
        {
            dt = (DataTable)context.Cache["CacheUser"];

        }
        else
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = " Select convert(varchar,STOCKITEM_SERIAL)+'-'+STOCKITEM_BASEUNITS as StockID, SUBSTRING(STOCKITEM_NAME,5,50)+ '-'+isnull(substring(STOCKITEM_ALIAS,5,50),'') as NameLabel,  SUBSTRING(STOCKITEM_NAME,5,50)+ '-'+isnull(substring(STOCKITEM_ALIAS,5,50),'') as NameValue, Company_ID from dbo.ASTINV_STOCKITEM ";
                    cmd.CommandText = @" SELECT UserName AS StockID, 
                      LoginID AS NameValue, 
                        LoginID AS NameLabel,
                        LoginID AS CodeSearch,
                      LoginID AS NameSearch, LoginID as COMPANY_ID  
                        FROM dbo.TB_PrmUserInfo order by LoginID
                       ";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))                    
                    {
                           
                            sda.Fill(dt);
                            string tableName = "TB_PrmUserInfo";  
                            System.Web.Caching.SqlCacheDependency sqlDep = new System.Web.Caching.SqlCacheDependency("Cnn",tableName);
                            context.Cache.Insert("CacheUser", dt, sqlDep);
                    }
                    //dt.Dispose();
                    conn.Close();

                }
            }

        }

        //---------------- =It can be filled by cache or from database ---------------------
        if (dt.Rows.Count > 0)
        {
            string expression = String.Empty;

            DataTable dtnew = dt.Clone();
            dtnew.Columns.Remove("Company_ID");
            dtnew.Columns.Remove("CodeSearch");
            dtnew.Columns.Remove("NameSearch");
            dtnew.Clear();
            string strNameLabel = null;
            string strNameValue = null;
            expression = " NameSearch like '%" + prefixText + "%' ";  // = For Item Name Only 
            DataRow[] drArray = dt.Select(expression);
            if (drArray.Length > 0)
            {
                int i = 0;
                foreach (DataRow dr in drArray)
                {
                    DataRow drnewrow = dtnew.NewRow();
                    drnewrow["StockID"] = dr["StockID"];
                    if (String.IsNullOrEmpty(dr["NameLabel"].ToString()) == false)
                    {
                        strNameLabel = dr["NameLabel"].ToString().Split('-')[0]; // = For code only
                        strNameValue = dr["NameValue"].ToString().Split('-')[0]; // = For code only
                        drnewrow["NameLabel"] = strNameLabel;
                        drnewrow["NameValue"] = strNameValue;
                    }
                    dtnew.Rows.Add(drnewrow);
                    i++;
                    if (i == 30) break;

                }
                returnText = GetJSONString(dtnew);
            }
            else
                returnText = defaultText;
        }
        else
        {
            returnText = defaultText;
        }

        dt.Dispose();
        //------------------- =Sending data to caller ------------------
        context.Response.Write(returnText);

    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

  
    /// <summary>
    /// Convert a dataTable to json String
    /// </summary>
    /// <param name="Dt">Get DataTable</param>
    /// <returns>Return JSON string</returns>
    public static string GetJSONString(DataTable Dt)
    {

        string[] StrDc = new string[Dt.Columns.Count];

        string HeadStr = string.Empty;
        for (int i = 0; i < Dt.Columns.Count; i++)
        {

            StrDc[i] = Dt.Columns[i].Caption;
            HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";

        }

        //[ { "id": "Upupa epops", "label": "Eurasian Hoopoe", "value": "Eurasian Hoopoe" }, { "id": "Jynx torquilla", "label": "Eurasian Wryneck", "value": "Eurasian Wryneck" }, { "id": "Ficedula hypoleuca", "label":

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
        StringBuilder Sb = new StringBuilder();

        //Sb.Append("{\"" + Dt.TableName + "\" : [");
        Sb.Append("[");
        for (int i = 0; i < Dt.Rows.Count; i++)
        {

            string TempStr = HeadStr;

            Sb.Append("{");
            for (int j = 0; j < Dt.Columns.Count; j++)
            {

                TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾",HttpUtility.UrlEncode(Dt.Rows[i][j].ToString()));

            }
            Sb.Append(TempStr + "},");

        }

        Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

        Sb.Append("]");

        string jqText = Sb.ToString();
        jqText = jqText.Replace("StockID", "id");
        jqText = jqText.Replace("NameLabel", "label");
        jqText = jqText.Replace("NameValue", "value");

        return jqText;

    }


}