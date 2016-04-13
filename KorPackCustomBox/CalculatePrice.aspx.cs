using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Blumen;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

public partial class Sales_DirGroup : System.Web.UI.Page
{
    //string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string conString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string MaincnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    String path = HttpContext.Current.Request.PhysicalApplicationPath;
    string strCompanyID = String.Empty;
    string errorlog = "", errorlogDistro = "";
    private IList<Stream> m_streams;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("Logout.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                pageInitialize();
            }
        }

    }
    private void pageInitialize()
    {
        //string filters = "*.jpg;*.png;*.gif";
        //string Path = "~/Images/Style";

        //List<String> images = new List<string>();

        //foreach (string filter in filters.Split(';'))
        //{
        //    FileInfo[] fit = new DirectoryInfo(this.Server.MapPath(Path)).GetFiles(filter);
        //    foreach (FileInfo fi in fit)
        //    {
        //        images.Add(String.Format(Path + "/{0}", fi));
        //    }
        //}

        if (Session["CBHAutoId"] == null)
        {
            Session.Add("CBHAutoId", "0");
        }
        lblStyle.Text = "2";
        lblCustomer.Text = Session["CustomerId"].ToString();
        txtQty.Text = "800";//  System.DateTime.Now.ToShortDateString();

        txtLength.Text = "20";
        txtWidth.Text = "10";
        txtheight.Text = "5";
        txtTruck.Text = "2";
        txtOveLap.Text = "0";

        DataSet ds = getMasterData();
        ddlStyle.DataSource = ds.Tables[0];
        ddlStyle.DataTextField = "StyleCId";
        ddlStyle.DataValueField = "StyleId";
        ddlStyle.DataBind();

        ddlBroadGrade.DataSource = ds.Tables[1];
        ddlBroadGrade.DataTextField = "StrengthCId";
        ddlBroadGrade.DataValueField = "StrengthId";
        ddlBroadGrade.DataBind();

        ddlCustomer.DataSource = ds.Tables[2];
        ddlCustomer.DataTextField = "CustomersCId";
        ddlCustomer.DataValueField = "CustomersId";
        ddlCustomer.DataBind();

        ds = getData();
        FinalEachPrice.Text = ds.Tables[3].Rows[0]["Final Each Price"].ToString();
        TotalForThisQty.Text = ds.Tables[3].Rows[0]["Total For This Qty"].ToString();
        Session["CBHAutoId"] = ds.Tables[3].Rows[0]["CBHAutoId"].ToString();
        ds.Dispose();
        //RepeaterImages.DataSource = ds.Tables[0];
        //RepeaterImages.DataBind();
        //string myScript1 = "bindOnKeyUp();";
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
        //myScript1 = "";



    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {

            string myScript1 = "showInfo('Successfully Completed.');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
            myScript1 = "";
        }
        catch (Exception ex)
        {
            string myScript1 = "showInfo('" + Resources.Resource.ErrorMessage + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
            myScript1 = "";
        }
    }
    protected void btnProcessPO_Click(object sender, EventArgs e)
    {
        try
        {
            //rptViewer.DataBind();
            string CBHAutoId = Session["CBHAutoId"].ToString();
            DataTable dt = getOrderData(CBHAutoId);
            Sendmail("", "faaruk@yahoo.com", "Quotation-" + CBHAutoId + ".pdf", dt);

            //KPCustomBoxReports.Quote _rptOrderReport = new KPCustomBoxReports.Quote();
            //ExportToPdf(_rptOrderReport, CBHAutoId);


            //DataTable dt = getOrderData(CBHAutoId);
            //exportFIle(dt);

            string myScript1 = "showInfo('Operation Completed. Email Sent Successfully.');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
            myScript1 = "";
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            //string myScript1 = "showInfo('" + Resources.Resource.ErrorMessage + "');";
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
            //myScript1 = "";
        }
    }
    private void exportFIle(DataTable _dt)
    {
        var dataTable = _dt;

        var wb = new XLWorkbook();


        //// Add a DataTable as a worksheet
        wb.Worksheets.Add(dataTable);
        wb.SaveAs("d:/home/site/wwwroot/Sales/Sales.pdf");

        ////// Add a DataTable as a worksheet
        ////wb.Worksheets.Add(dataTable);
        ////wb.SaveAs("C:/Files/AddingDataTableAsWorksheet.xlsx");
        //////makeChart();

        //using (XLWorkbook wb = new XLWorkbook())
        //{

        //    wb.Worksheets.Add(dataTable);

        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=TrendAnalysis.xlsx");
        //    using (MemoryStream MyMemoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(MyMemoryStream);
        //        MyMemoryStream.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}

    }
    private DataSet getMasterData()
    {
        DataTable dt = null;
        string strSQL = "";
        strSQL = @"
        select [StyleId], [StyleCId] from [dbo].[CB_Style] order by [StyleCId];
        select [StrengthId], [StrengthCId] from [dbo].[CB_Strength] order by [StrengthCId] ;
        select [CustomersId], [CustomersCId] from [dbo].[CB_Customers] order by [CustomersCId];
        ";

        ExecuteSQL obAtten = new ExecuteSQL();
        DataSet ds = new DataSet();
        ds = obAtten.ExecuteQueryDataset(strSQL);
        return ds;
    }



    protected void btnReload_Click(object sender, EventArgs e)
    {

    }

    private DataSet getData()
    {


        DataSet ds = new DataSet("BoxPrice");
        using (SqlConnection conn = new SqlConnection(conString))
        {
            SqlCommand sqlComm = new SqlCommand("CalculateBoxPrice", conn);
            string _insertDate = System.DateTime.Now.ToString();

            sqlComm.Parameters.AddWithValue("@Qty", "800");
            sqlComm.Parameters.AddWithValue("@Length", "20");
            sqlComm.Parameters.AddWithValue("@Width", "10");
            sqlComm.Parameters.AddWithValue("@Height", "5");
            sqlComm.Parameters.AddWithValue("@StyleId", "2");
            sqlComm.Parameters.AddWithValue("@Strength", "2");
            sqlComm.Parameters.AddWithValue("@FlipCorrDir", "1");
            if (Session["CustomerId"].ToString() != "0")
            { sqlComm.Parameters.AddWithValue("@CustomersId", Session["CustomerId"].ToString()); }
            else { sqlComm.Parameters.AddWithValue("@CustomersId", "4"); }
            sqlComm.Parameters.AddWithValue("@OverLap", "0");
            sqlComm.Parameters.AddWithValue("@Truck", "2");
            sqlComm.Parameters.AddWithValue("@Perforated ", "1");
            sqlComm.Parameters.AddWithValue("@UserID ", Session["UserID"].ToString());
            sqlComm.Parameters.AddWithValue("@InsertDate", _insertDate);
            sqlComm.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;

            da.Fill(ds);

        }
        return ds;

    }
    private DataTable getOrderData(string orderNo)
    {
        DataTable dt = null;
        string strSQL = "";
        strSQL = @"SELECT        CBHAutoId, Qty, Length, Width, Height, S.StyleCId, B.StrengthCId,
         case when FlipCorrDir=1 then 'Yes'
	        else 'No' end FlipCorrDir,
         CC.CustomersCId,  OverLap, Truck, 
         case when Perforated='1' then 'Yes' else 'No' end Perforated,
         U.UserName , FinalEachPrice, TotalForThisQty, insertdate, U.Email, CC.CustomersDesc, 
         convert(varchar(10), convert(datetime,insertdate),101) QuoteDate,
         convert(varchar(10), DATEADD(dd, 30, convert(datetime,insertdate)),101) ExpireDate
        FROM CalculateBoxPrice_History C
        inner join[dbo].[CB_Style] S on S.StyleId = C.StyleId
        inner join[dbo].[CB_Strength]
            B on B.StrengthId=C.Strength
        left join[dbo].[CB_Customers]
            CC on CC.CustomersId=C.CustomersId
        inner join[dbo].[TB_PrmUserInfo]
            U on U.UserID=C.UserID
        where CBHAutoId=" + orderNo + @"";
        ExecuteSQL obAtten = new ExecuteSQL();
        DataSet ds = new DataSet();
        ds = obAtten.ExecuteQueryDataset(strSQL);
        dt = ds.Tables[0];
        return dt;
    }
    void ExportToPdf(Telerik.Reporting.Report reportToExport, string soid)
    {

        /* create pdf report of Sale order */
        string fileName = string.Empty;

        string path = string.Empty;
        string filePath = string.Empty;

        DataTable dtSales = getOrderData(soid);

        reportToExport.DataSource = dtSales;
        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
        Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportToExport, null);


        //fileName = result.DocumentName.Replace(result.DocumentName, "Quotation-" + soid) + "." + result.Extension;

        path = Server.MapPath(".");
        filePath = System.IO.Path.Combine(path, fileName);

        //string strFile = "d:/home/site/wwwroot/Sales/Quotation-" + soid + "." + result.Extension;
        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        {
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
        }
        //Sendmail("", dtSales.Rows[0]["Email"].ToString(), "Quotation-" + soid + ".pdf");

    }

    public void Sendmail(string distid, string semail, string sfilename, DataTable _dt)
    {

        try
        {
            String sBody;
            System.Net.Mail.MailMessage Mail = new System.Net.Mail.MailMessage("faaruk.ahmed@gmail.com", semail);
            Mail.Sender = new System.Net.Mail.MailAddress(semail);
            Mail.From = new System.Net.Mail.MailAddress("faaruk.ahmed@gmail.comt");
            Mail.To.Add(semail);
            Mail.CC.Add("nnovy@korpack.com");
            Mail.Subject = "Quotation";
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            client.Credentials = new System.Net.NetworkCredential("faaruk.ahmed@gmail.com", "secure123");
            client.EnableSsl = true;
            client.Port = 587;

            //string path = Server.MapPath(".");
            //string filePath = System.IO.Path.Combine(path, sfilename);
            //string filePath1 = System.IO.Path.Combine(path, sfilename2);
            //Mail.Attachments.Add(new System.Net.Mail.Attachment(filePath));
            //Mail.Attachments.Add(new System.Net.Mail.Attachment(filePath1));
            Mail.IsBodyHtml = true;

            sBody = "PDF";
            //Mail.Body = " Hello " + Convert.ToString(Session["UserName"]) + ",<br><br>" + "Attached, please find the Quotation.<br><br>";
            Mail.Body = emailbody(_dt);

            client.Send(Mail);
            Mail.Dispose();
            // Delete pdf file of so order
            //System.IO.File.Delete(filePath);
            //System.IO.File.Delete(filePath1);

        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }
    private string emailbody(DataTable _dt)
    {
        string body = @"<table>
        <tr>
            <td>
                <table style='width: 600px'>
                    <tr>
                        <td style='text-align: left; width: 400px'>Korpack<br />
                            1232 Hardt Circle<br />
                            Bartlett, IL, 60103<br />
                            Phone: 630-213-3600<br />
                            Web: www.korpack.com</td>
                        <td style='text-align: right; width: 200px'>
                            <div style='background-color: Navy; color: white; font-size: 20px; font-weight: 700; text-align: center'>Quote</div>
                            <br />
                            Customer: " + _dt.Rows[0]["CustomersCId"].ToString() + @"<br />
                            Quote Date: " + _dt.Rows[0]["QuoteDate"].ToString() + @"<br />
                            Expire Date: " + _dt.Rows[0]["ExpireDate"].ToString() + @"
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style='border: solid 1px black'>
                <table style='width: 600px'>
                    <tr>
                        <td style='background-color: cornflowerblue; color: white; font-size: 16px; font-weight: 600; text-align: center; width: 200px'>CUSTOMER P.O. NO</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 16px; font-weight: 600; text-align: center; width: 200px'>TERMS</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 16px; font-weight: 600; text-align: center; width: 200px'>CONTACT</td>
                    </tr>
                    <tr>
                        <td style='background-color: white; color: black; font-size: 16px; font-weight: 500; text-align: center; width: 200px'>TBD</td>
                        <td style='background-color: white; color: black; font-size: 16px; font-weight: 500; text-align: center; width: 200px'>Net due in 75 days</td>
                        <td style='background-color: white; color: black; font-size: 16px; font-weight: 500; text-align: center; width: 200px'>Novy, Nick</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style='border: solid 1px black'>
                <table style='width: 600px'>
                    <tr>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 50px;'>Quantity</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 50px;'>Length</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>Width</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>Height</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>Style</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>Board</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 100px;'>Final Each Price</td>
                        <td style='background-color: cornflowerblue; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 100px;'>Total For This Qty</td>
                    </tr>
                    <tr>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 50px;'>" + _dt.Rows[0]["Qty"].ToString() + @"</td>                        
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 50px;'>" + _dt.Rows[0]["Length"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>" + _dt.Rows[0]["Width"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>" + _dt.Rows[0]["Height"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>" + _dt.Rows[0]["StyleCId"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 75px;'>" + _dt.Rows[0]["StrengthCId"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 100px;'>" + _dt.Rows[0]["FinalEachPrice"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 100px;'>" + _dt.Rows[0]["TotalForThisQty"].ToString() + @"</td>
                    </tr>
                    <tr>
                        <td colspan='8'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan='8'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan='8'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan='8'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan='8'>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
             <td style='border: solid 1px black'>
                <table style='width: 600px'>
                    <tr>
                        <td colspan='2'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style='text-align: left; width: 400px'>&nbsp;</td>
                        <td style='text-align: right; width: 200px;font-size: 16px; font-weight: bold;'>
                            Quote Total: " + _dt.Rows[0]["TotalForThisQty"].ToString() + @"
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>&nbsp;</td>
                    </tr>
                        <tr>
                        <td colspan='2'>&nbsp;</td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>";
        return body;
    }
    protected void rptViewer_DataBinding(object sender, EventArgs e)
    {
        string deviceInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

        string s_rptType = "pdf";
        string bindDs_name = "DataSet1";
        DataTable dt = null;
        dt = new DataTable();
        string connectionString = string.Empty;
        string reportName = "";

        string strECDSmsg = "";
        strECDSmsg = "Printed from KP on " + DateTime.Now.ToString();

        DataTable dtCompany = null;
        dtCompany = new DataTable();

        rptViewer.LocalReport.DataSources.Clear();

        try
        {
            rptViewer.LocalReport.EnableExternalImages = true;
            reportName = "Quote.rdl";
            rptViewer.LocalReport.DisplayName = "Report";
            rptViewer.LocalReport.ReportPath = "Reports/" + reportName;

            string CBHAutoId = Session["CBHAutoId"].ToString();
            dt = getOrderData(CBHAutoId);
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource(bindDs_name.Trim(), dt));
            dt.Dispose();
        }// end of try
        catch (SqlException exp)
        {
            //exp.ToString();
            string myScript = "alert(' Exception Occured, Contect to System Admin '+ ');";
            ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", myScript, true);
            myScript = "";
        }
        finally
        {
            dt.Dispose();
            saveRptAs(s_rptType, deviceInfo);
        }
    }
    private void saveRptAs(String s_rptType, string deviceInfo)
    {
        String path = HttpContext.Current.Request.PhysicalApplicationPath;
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        m_streams = new List<Stream>();
        byte[] bytes;

        bytes = rptViewer.LocalReport.Render(
        s_rptType, deviceInfo, out mimeType, out encoding, out extension,
        out streamids, out warnings);
        File.Delete(path + @"TempReport\TempReport." + extension);


        FileStream stream = File.OpenWrite(path + @"TempReport\TempReport." + extension);
        stream.Write(bytes, 0, bytes.Length);
        m_streams.Add(stream);
        stream.Close();
    }

}
