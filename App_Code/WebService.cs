using System;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Web.Script.Services;
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class WebService : System.Web.Services.WebService
{
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public ServiceOutput GetCalculations(string Qty, string Length, string Width, string Height, string StyleId, string Strength, string FlipCorrDir,
        string CustomersId, string OverLap, string Truck, string Perforated, string callFrom, string PCAutoId)
    {
        //@callFrom
        //@PCAutoId
        //Debug.Assert(false, "This is text that has been send--" + prefixText);
        string UserID = "";

        string returnString = String.Empty;
        string connectionString = string.Empty;
        DataTable dt = null;
        SqlConnection con = null;

        dt = new DataTable();
        DataSet ds = new DataSet("BoxPrice");
        if (Session["UserID"] != null)
        {
            UserID = Session["UserID"].ToString();
            connectionString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
            con = new SqlConnection(connectionString);
            try
            {
                string _insertDate = System.DateTime.Now.ToString();

                SqlCommand sqlComm = new SqlCommand("CalculateBoxPrice", con);

                sqlComm.Parameters.AddWithValue("@Qty", Qty);
                sqlComm.Parameters.AddWithValue("@Length ", Length);
                sqlComm.Parameters.AddWithValue("@Width", Width);
                sqlComm.Parameters.AddWithValue("@Height ", Height);
                sqlComm.Parameters.AddWithValue("@StyleId", StyleId);
                sqlComm.Parameters.AddWithValue("@Strength ", Strength);
                sqlComm.Parameters.AddWithValue("@FlipCorrDir", FlipCorrDir);
                sqlComm.Parameters.AddWithValue("@CustomersId", CustomersId);
                sqlComm.Parameters.AddWithValue("@OverLap", OverLap);
                sqlComm.Parameters.AddWithValue("@Truck", Truck);
                sqlComm.Parameters.AddWithValue("@Perforated", Perforated);
                sqlComm.Parameters.AddWithValue("@UserID", UserID);
                sqlComm.Parameters.AddWithValue("@InsertDate", _insertDate);
                sqlComm.Parameters.AddWithValue("@callFrom", callFrom);
                sqlComm.Parameters.AddWithValue("@PCAutoId", PCAutoId);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

                dt = ds.Tables[3];

                //return dt;
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
                dt.Dispose();

            }
        }
        ServiceOutput objSO = new ServiceOutput();
        if (dt.Rows.Count == 0)
        {
            objSO.FinalEachPrice = "0";
            objSO.TotalForThisQty = "0";
            objSO.CBHAutoId = "0";
        }
        else
        {
            objSO.FinalEachPrice = dt.Rows[0][22].ToString();
            objSO.TotalForThisQty = dt.Rows[0][23].ToString();
            objSO.CBHAutoId = dt.Rows[0][24].ToString();
            if (Session["CBHAutoId"] == null)
            {
                Session.Add("CBHAutoId", "0");
            }
            Session["CBHAutoId"] = dt.Rows[0][24].ToString();
        }
        return objSO;
        //return new JavaScriptSerializer().Serialize(objSO);

        //List<string> itemList = new List<string>(dt.Rows.Count);

        //if (dt.Rows.Count == 0)
        //{
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //    itemList.Add("0");
        //}
        //else
        //{

        //    itemList.Add(dt.Rows[0][0].ToString());
        //    itemList.Add(dt.Rows[0][1].ToString());
        //    itemList.Add(dt.Rows[0][2].ToString());
        //    itemList.Add(dt.Rows[0][3].ToString());
        //    itemList.Add(dt.Rows[0][4].ToString());
        //    itemList.Add(dt.Rows[0][5].ToString());
        //    itemList.Add(dt.Rows[0][6].ToString());
        //    itemList.Add(dt.Rows[0][7].ToString());
        //    itemList.Add(dt.Rows[0][8].ToString());
        //    itemList.Add(dt.Rows[0][9].ToString());
        //    itemList.Add(dt.Rows[0][10].ToString());
        //    itemList.Add(dt.Rows[0][11].ToString());
        //    itemList.Add(dt.Rows[0][12].ToString());
        //    itemList.Add(dt.Rows[0][13].ToString());
        //    itemList.Add(dt.Rows[0][14].ToString());
        //    itemList.Add(dt.Rows[0][15].ToString());
        //    itemList.Add(dt.Rows[0][16].ToString());
        //    itemList.Add(dt.Rows[0][17].ToString());
        //    itemList.Add(dt.Rows[0][18].ToString());
        //    itemList.Add(dt.Rows[0][19].ToString());
        //    itemList.Add(dt.Rows[0][20].ToString());
        //    itemList.Add(dt.Rows[0][21].ToString());
        //    itemList.Add(dt.Rows[0][22].ToString());
        //    itemList.Add(dt.Rows[0][23].ToString());
        //    itemList.Add(dt.Rows[0][24].ToString());
        //    if (Session["CBHAutoId"] == null)
        //    {
        //        Session.Add("CBHAutoId", "0");
        //    }
        //    Session["CBHAutoId"] = dt.Rows[0][24].ToString();



        //}
        //return itemList.ToArray();




    }

}
public class ServiceOutput
{
    public string FinalEachPrice { get; set; }
    public string TotalForThisQty { get; set; }
    public string CBHAutoId { get; set; }
}