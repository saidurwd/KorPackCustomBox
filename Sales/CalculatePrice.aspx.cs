using System;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Blumen;

public partial class Sales_DirGroup : System.Web.UI.Page
{
    string conString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserID"] == null)
        //{
        //    Response.Redirect("../Logout.aspx");
        //}
        //else
        //{
        if (!IsPostBack)
        {
            pageInitialize();
        }
        //}
    }
    private void pageInitialize()
    {
        if (Session["CBHAutoId"] == null)
        {
            Session.Add("CBHAutoId", "0");
        }
        lblStyle.Text = "1";
        if (Session["CustomerId"] != null)
        {
            lblCustomer.Text = Session["CustomerId"].ToString();
        }
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
        string RoleID = "0";
        if (Session["RoleID"] != null)
        {
            RoleID = Session["RoleID"].ToString();
        }
        if ((RoleID == "18") || (RoleID == "4"))
        {
            ddlPriceClass.DataSource = ds.Tables[2];
            ddlPriceClass.DataTextField = "PriceClassDesc";
            ddlPriceClass.DataValueField = "PCAutoId";
            ddlPriceClass.DataBind();
            rwPriceClass.Visible = true;
        }
        else
        { rwPriceClass.Visible = false; }
        if (Session["UserID"] != null)
        {
            ds = getData();
            FinalEachPrice.Text = ds.Tables[3].Rows[0]["Final Each Price"].ToString();
            TotalForThisQty.Text = ds.Tables[3].Rows[0]["Total For This Qty"].ToString();
            Session["CBHAutoId"] = ds.Tables[3].Rows[0]["CBHAutoId"].ToString();
        }
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
            //Sendmail("", "faaruk@yahoo.com", "Quotation-" + CBHAutoId + ".pdf", dt);
            sendemailoffice365(dt.Rows[0]["Email"].ToString(), dt);
            QuoteSent(CBHAutoId);
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
    protected void btnProcessPORFQ_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = getEmailID();
            string requestedByEmail = dt.Rows[0]["Email"].ToString();
            sendemailoffice365ForRFQ(requestedByEmail);
            sendemailoffice365ForRFQTo(requestedByEmail);

            string myScript1 = "showInfo('Operation Completed. Email Sent Successfully.');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
            myScript1 = "";
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    //private void exportFIle(DataTable _dt)
    //{
    //    var dataTable = _dt;

    //    var wb = new XLWorkbook();


    //    //// Add a DataTable as a worksheet
    //    wb.Worksheets.Add(dataTable);
    //    wb.SaveAs("d:/home/site/wwwroot/Sales/Sales.pdf");

    //    ////// Add a DataTable as a worksheet
    //    ////wb.Worksheets.Add(dataTable);
    //    ////wb.SaveAs("C:/Files/AddingDataTableAsWorksheet.xlsx");
    //    //////makeChart();

    //    //using (XLWorkbook wb = new XLWorkbook())
    //    //{

    //    //    wb.Worksheets.Add(dataTable);

    //    //    Response.Clear();
    //    //    Response.Buffer = true;
    //    //    Response.Charset = "";
    //    //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //    //    Response.AddHeader("content-disposition", "attachment;filename=TrendAnalysis.xlsx");
    //    //    using (MemoryStream MyMemoryStream = new MemoryStream())
    //    //    {
    //    //        wb.SaveAs(MyMemoryStream);
    //    //        MyMemoryStream.WriteTo(Response.OutputStream);
    //    //        Response.Flush();
    //    //        Response.End();
    //    //    }
    //    //}

    //}
    private DataSet getMasterData()
    {
        
        string strSQL = "";
        strSQL = @"
        select [StyleId], [StyleCId] from [dbo].[CB_Style] order by [StyleCId];
        select [StrengthId], [StrengthCId] from [dbo].[CB_Strength] order by [StrengthId] ;
        SELECT PCAutoId, PriceClassDesc FROM [dbo].[CB_PriceClass] ORDER BY PriceClassDesc ;
        ";

        ExecuteSQL obAtten = new ExecuteSQL();
        DataSet ds = new DataSet();
        ds = obAtten.ExecuteQueryDataset(strSQL);
        obAtten.Dispose();
        return ds;
    }
    private void QuoteSent(string CBHAutoId)
    {

        string strSQL = "";
        strSQL = @"UPDATE CalculateBoxPrice_History SET QuoteSent=1 WHERE CBHAutoId=" + CBHAutoId + @"";
        ExecuteSQL obAtten = new ExecuteSQL();
        obAtten.ExecuteQuery(strSQL);
        obAtten.Dispose();
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
            sqlComm.Parameters.AddWithValue("@StyleId", "1");
            sqlComm.Parameters.AddWithValue("@Strength", "1");
            sqlComm.Parameters.AddWithValue("@FlipCorrDir", "0");
            if (Session["CustomerId"] != null)
            {
                if (Session["CustomerId"].ToString() != "0")
                {
                    sqlComm.Parameters.AddWithValue("@CustomersId", Session["CustomerId"].ToString());
                }
                else { sqlComm.Parameters.AddWithValue("@CustomersId", "4"); }
            }
            else { sqlComm.Parameters.AddWithValue("@CustomersId", "4"); }
            sqlComm.Parameters.AddWithValue("@OverLap", "0");
            sqlComm.Parameters.AddWithValue("@Truck", "2");
            sqlComm.Parameters.AddWithValue("@Perforated ", "1");
            sqlComm.Parameters.AddWithValue("@UserID ", Session["UserID"].ToString());
            sqlComm.Parameters.AddWithValue("@InsertDate", _insertDate);
            sqlComm.Parameters.AddWithValue("@callFrom", "0");
            sqlComm.Parameters.AddWithValue("@PCAutoId", "0");
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
    private DataTable getEmailID()
    {
        DataTable dt = null;
        string strUserId = Session["UserID"].ToString();

        string strSQL = "";
        strSQL = @"SELECT U.Email
        FROM [dbo].[TB_PrmUserInfo] U 
        where U.UserID=" + strUserId + @"";
        ExecuteSQL obAtten = new ExecuteSQL();
        DataSet ds = new DataSet();
        ds = obAtten.ExecuteQueryDataset(strSQL);
        dt = ds.Tables[0];
        return dt;
    }
    //void ExportToPdf(Telerik.Reporting.Report reportToExport, string soid)
    //{

    //    /* create pdf report of Sale order */
    //    string fileName = string.Empty;

    //    string path = string.Empty;
    //    string filePath = string.Empty;

    //    DataTable dtSales = getOrderData(soid);

    //    reportToExport.DataSource = dtSales;
    //    Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
    //    Telerik.Reporting.Processing.RenderingResult result = reportProcessor.RenderReport("PDF", reportToExport, null);


    //    //fileName = result.DocumentName.Replace(result.DocumentName, "Quotation-" + soid) + "." + result.Extension;

    //    path = Server.MapPath(".");
    //    filePath = System.IO.Path.Combine(path, fileName);

    //    //string strFile = "d:/home/site/wwwroot/Sales/Quotation-" + soid + "." + result.Extension;
    //    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
    //    {
    //        fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
    //    }
    //    //Sendmail("", dtSales.Rows[0]["Email"].ToString(), "Quotation-" + soid + ".pdf");

    //}
    private void sendemailoffice365(string semail, DataTable _dt)
    {
        string CBHAutoId = _dt.Rows[0]["CBHAutoId"].ToString();
        string myString = Convert.ToInt32(CBHAutoId).ToString("00000");
        myString = "Quote# 16-" + myString;
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("quotes@korpack.com", "KorPack");
        msg.To.Add(new System.Net.Mail.MailAddress(semail, semail));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("nnovy@korpack.com", "Nick Novy"));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("sales@korpack.com", "Korpack Sales"));

        msg.Subject = "Quotation from Korpack - " + myString;
        msg.Body = emailbody(myString, _dt);
        msg.IsBodyHtml = true;

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("quotes@korpack.com", "Dato0401");
        client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        client.Send(msg);
        msg.Dispose();

    }
    private void sendemailoffice365ForRFQ(string semail)
    {
        
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("quotes@korpack.com", "KorPack");
        msg.To.Add(new System.Net.Mail.MailAddress(semail, semail));
        msg.To.Add(new System.Net.Mail.MailAddress("quotes@korpack.com", "Korpack Quote"));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("nnovy@korpack.com", "Nick Novy"));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("faaruk@yahoo.com", "Korpack Quote"));

        msg.Subject = "RFQ by " + Session["UserName"].ToString();
        msg.Body = Session["UserName"].ToString() + " has requested for RFQ. Please reply as soon as possible.";
        msg.IsBodyHtml = true;

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("quotes@korpack.com", "Dato0401");
        client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        client.Send(msg);
        msg.Dispose();

    }
    private void sendemailoffice365ForRFQTo(string semail)
    {
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("quotes@korpack.com", "KorPack");
        msg.To.Add(new System.Net.Mail.MailAddress(semail, semail));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("nnovy@korpack.com", "Nick Novy"));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("faaruk@yahoo.com", "Korpack Quote"));

        msg.Subject = "Thank you from Korpack for contacting us! ";
        msg.Body ="Hello " +Session["UserName"].ToString() + ", <br/><br/> Thank you for contacting us. We will get back to you soon.<br/><br/><br/> Best Regards,<br/>Team Korpack";
        msg.IsBodyHtml = true;

        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("quotes@korpack.com", "Dato0401");
        client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
        client.Host = "smtp.office365.com";
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        client.EnableSsl = true;

        client.Send(msg);
        msg.Dispose();

    }
    private string emailbody(string quotenumber, DataTable _dt)
    {
        string body = @"<table>
        <tr>
            <td>
                <table style='width: 700px'>
                    <tr>
                        <td style='text-align: left; width: 450px'>Korpack<br />
                            1232 Hardt Circle<br />
                            Bartlett, IL, 60103<br />
                            Phone: 630-213-3600<br />
                            www.korpack.com<br />
                            www.QuicKorCustomBoxes.com
                            </td>
                        <td style='text-align: right; width: 350px'>
                            <div style='background-color: Navy; color: white; font-size: 20px; font-weight: 700; text-align: center'>Quote</div>
                            <br />
                            " + quotenumber + @"<br />
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
                <table style='width: 700px'>
                    <tr>
                        <td style='background-color: Navy; color: white; font-size: 16px; font-weight: 600; text-align: center; width: 233px'>CUSTOMER P.O. NO</td>
                        <td style='background-color: Navy; color: white; font-size: 16px; font-weight: 600; text-align: center; width: 233px'>TERMS</td>
                        <td style='background-color: Navy; color: white; font-size: 16px; font-weight: 600; text-align: center; width: 234px'>CONTACT</td>
                    </tr>
                    <tr>
                        <td style='background-color: white; color: black; font-size: 16px; font-weight: 500; text-align: center; width: 233px'>TBD</td>
                        <td style='background-color: white; color: black; font-size: 16px; font-weight: 500; text-align: center; width: 233px'>Net due in 75 days</td>
                        <td style='background-color: white; color: black; font-size: 16px; font-weight: 500; text-align: center; width: 234px'>Novy, Nick</td>
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
                <table style='width: 700px'>
                    <tr>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Quantity</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Length</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Width</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Height</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Style</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Board</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>Price (Ea)</td>
                        <td style='background-color: Navy; color: white; font-size: 14px; font-weight: bold; text-align: center; width: 97px;'>Total</td>
                    </tr>
                    <tr>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["Qty"].ToString() + @"</td>                        
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["Length"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["Width"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["Height"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["StyleCId"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["StrengthCId"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 87px;'>" + _dt.Rows[0]["FinalEachPrice"].ToString() + @"</td>
                        <td style='font-size: 14px; font-weight: bold; text-align: center; width: 91px;'>" + _dt.Rows[0]["TotalForThisQty"].ToString() + @"</td>
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
                <table style='width: 700px'>
                    <tr>
                        <td colspan='2'>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style='text-align: left; width: 450px'>&nbsp;</td>
                        <td style='text-align: right; width: 350px;font-size: 16px; font-weight: bold;'>
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
    //protected void rptViewer_DataBinding(object sender, EventArgs e)
    //{
    //    string deviceInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

    //    string s_rptType = "pdf";
    //    string bindDs_name = "DataSet1";
    //    DataTable dt = null;
    //    dt = new DataTable();
    //    string connectionString = string.Empty;
    //    string reportName = "";

    //    string strECDSmsg = "";
    //    strECDSmsg = "Printed from KP on " + DateTime.Now.ToString();

    //    DataTable dtCompany = null;
    //    dtCompany = new DataTable();

    //    rptViewer.LocalReport.DataSources.Clear();

    //    try
    //    {
    //        rptViewer.LocalReport.EnableExternalImages = true;
    //        reportName = "Quote.rdl";
    //        rptViewer.LocalReport.DisplayName = "Report";
    //        rptViewer.LocalReport.ReportPath = "Reports/" + reportName;

    //        string CBHAutoId = Session["CBHAutoId"].ToString();
    //        dt = getOrderData(CBHAutoId);
    //        rptViewer.LocalReport.DataSources.Add(new ReportDataSource(bindDs_name.Trim(), dt));
    //        dt.Dispose();
    //    }// end of try
    //    catch (SqlException exp)
    //    {
    //        //exp.ToString();
    //        string myScript = "alert(' Exception Occured, Contect to System Admin '+ ');";
    //        ClientScript.RegisterStartupScript(this.GetType(), "ClientScript", myScript, true);
    //        myScript = "";
    //    }
    //    finally
    //    {
    //        dt.Dispose();
    //        saveRptAs(s_rptType, deviceInfo);
    //    }
    //}
    //private void saveRptAs(String s_rptType, string deviceInfo)
    //{
    //    String path = HttpContext.Current.Request.PhysicalApplicationPath;
    //    Warning[] warnings;
    //    string[] streamids;
    //    string mimeType;
    //    string encoding;
    //    string extension;
    //    m_streams = new List<Stream>();
    //    byte[] bytes;

    //    bytes = rptViewer.LocalReport.Render(
    //    s_rptType, deviceInfo, out mimeType, out encoding, out extension,
    //    out streamids, out warnings);
    //    File.Delete(path + @"TempReport\TempReport." + extension);


    //    FileStream stream = File.OpenWrite(path + @"TempReport\TempReport." + extension);
    //    stream.Write(bytes, 0, bytes.Length);
    //    m_streams.Add(stream);
    //    stream.Close();
    //}

}
