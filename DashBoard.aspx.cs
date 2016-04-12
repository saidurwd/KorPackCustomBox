using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Elmah;
using Blumen;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

public partial class DashBoard : System.Web.UI.Page
{

    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string strCompanyID = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            form1.Target = "_parent";
            strCompanyID = Session["CompanyID"].ToString();
            if (!IsPostBack)
            {
                FillAllWidgets();
                mLoadShortCutIcons();
            }
        }

    }
    protected void FillAllWidgets()
    {
        ExecuteSQL obAtten = new ExecuteSQL();
        DataTable dt = null;
        dt = new DataTable();
        //saleschart.Visible = false;
        string myHideDisplayStyle;
        string myShowDisplayStyle;
        myHideDisplayStyle = "none";
        myShowDisplayStyle = "block";
        AttributeCollection _widgetAttribute;

        _widgetAttribute = saleschart.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        _widgetAttribute = intro.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        _widgetAttribute = widget2.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        _widgetAttribute = widget3.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        _widgetAttribute = widget4.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        _widgetAttribute = widget5.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        _widgetAttribute = widget6.Attributes;
        _widgetAttribute.CssStyle.Add("display", myHideDisplayStyle);

        string struserID = "", strSQL = "";
        struserID = Session["UserID"].ToString();

        strSQL = @"SELECT    TP.DItemID, TD.DItemName
        FROM         dbo.TB_DashboardItems AS TD INNER JOIN
                              dbo.TB_Dashboard_Permission AS TP ON TD.DItemID = TP.DItemID
        WHERE     (TP.COMPANY_ID =" + strCompanyID + @") AND (TP.UserID = " + struserID + @")
        order by TP.DItemID";

        dt = obAtten.SearchReportRecordFromSQLQuery(strSQL);
        if (dt.Rows.Count == 0)
        {
            return;
        }
        SqlConnection connection = new SqlConnection(cnnString);
        dashboard objDML = new dashboard(connection);
        try
        {
            //Parameter Section
            objDML.company_id = Session["CompanyID"].ToString();
            objDML.whereConditionID = String.Empty;
            objDML.COMP_VOUCHER_DATE = Convert.ToDateTime(Request.Cookies.Get("DDate").Value);

            //Connection Section 
            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");

            //Excute Section

            DataSet ds = objDML.ExecuteDataSet();
            //objDML.Execute();

            if (objDML.ReturnValue == 0)
            {
                objDML.Transaction.Commit();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["DItemID"].ToString() == "1")
                    {
                        _widgetAttribute = saleschart.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        //MakeGraph(1);
                    }

                    if (dt.Rows[i]["DItemID"].ToString() == "2")
                    {
                        _widgetAttribute = intro.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        this.grdDocControl.DataSource = ds.Tables[0];
                        this.grdDocControl.DataBind();

                        
                    }
                    if (dt.Rows[i]["DItemID"].ToString() == "3")
                    {
                        _widgetAttribute = widget2.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        this.grdSales.DataSource = ds.Tables[1];
                        this.grdSales.DataBind();
                    }
                    if (dt.Rows[i]["DItemID"].ToString() == "4")
                    {
                        _widgetAttribute = widget3.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        this.grdProcurement.DataSource = ds.Tables[2];
                        this.grdProcurement.DataBind();
                    }
                    if (dt.Rows[i]["DItemID"].ToString() == "5")
                    {
                        _widgetAttribute = widget4.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        this.grdInventory.DataSource = ds.Tables[3];
                        this.grdInventory.DataBind();
                    }
                    if (dt.Rows[i]["DItemID"].ToString() == "6")
                    {
                        _widgetAttribute = widget5.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        this.grdAccounts.DataSource = ds.Tables[4];
                        this.grdAccounts.DataBind();
                    }
                    if (dt.Rows[i]["DItemID"].ToString() == "7")
                    {
                        _widgetAttribute = widget6.Attributes;
                        _widgetAttribute.CssStyle.Add("display", myShowDisplayStyle);
                        this.grdHR.DataSource = ds.Tables[5];
                        this.grdHR.DataBind();
                    }
                }
                //ErrorSignal.FromCurrentContext().Raise(exp);

                // this.intro.Visible = false; // = We can give permission like this :), I think it will be easy

            }
        }
        catch (SqlException exp)
        {

            objDML.Transaction.Rollback();

            string myScript = "showError('" + Resources.Resource.DataBaseError + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";

            ErrorSignal.FromCurrentContext().Raise(exp);


        }
        catch (Exception exp)
        {

            //objDML.Transaction.Rollback();
            string myScript = "showError('" + Resources.Resource.GeneralError + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";

        }
        finally
        {
            if (objDML.Connection.State == ConnectionState.Open)
            {
                objDML.Connection.Close();
                objDML.Connection.Dispose();

            }

        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       //if (e.Row.RowType == DataControlRowType.DataRow)
       // {
       //     HyperLink _hlinkRefNo = e.Row.FindControl("hlinkRefNo") as HyperLink;
       //     ImageButton _hlReqHistory = e.Row.FindControl("hlReqHistory") as ImageButton;
       //     //ImageButton _hlQuotationHistory = e.Row.FindControl("hlQuotationHistory") as ImageButton;
       //     ImageButton _hlOrderHistory = e.Row.FindControl("hlOrderHistory") as ImageButton;
       //     ImageButton _hlInstructionHistory = e.Row.FindControl("hlInstructionHistory") as ImageButton;
       //     ImageButton _hlChallanHistory = e.Row.FindControl("hlChallanHistory") as ImageButton;
       //     ImageButton _itemLedger = e.Row.FindControl("itemLedger") as ImageButton;
       // }
        //hlinkRefNo
    }
    protected void mLoadShortCutIcons()
    {
        this.SqlDataSource1.SelectCommand = @"
            select 
            [dbo].[fn_getFuncDesc](1,@UserID,@CompanyID) FuncDesc1, 
            [dbo].[fn_getFuncTarget](1,@UserID,@CompanyID) AS TargetPage1,
 
            [dbo].[fn_getFuncDesc](2,@UserID,@CompanyID) AS FuncDesc2, 
            [dbo].[fn_getFuncTarget](2,@UserID,@CompanyID) AS TargetPage2,

            [dbo].[fn_getFuncDesc](3,@UserID,@CompanyID) AS FuncDesc3, 
            [dbo].[fn_getFuncTarget](3,@UserID,@CompanyID) AS TargetPage3,

            [dbo].[fn_getFuncDesc](4,@UserID,@CompanyID) AS FuncDesc4, 
            [dbo].[fn_getFuncTarget](4,@UserID,@CompanyID) AS TargetPage4,

            [dbo].[fn_getFuncDesc](5,@UserID,@CompanyID) AS FuncDesc5, 
            [dbo].[fn_getFuncTarget](5,@UserID,@CompanyID) AS TargetPage5
        ";

        this.SqlDataSource1.SelectParameters.Clear();
        this.SqlDataSource1.SelectParameters.Add("CompanyID", Session["CompanyID"].ToString());
        this.SqlDataSource1.SelectParameters.Add("UserID", Session["UserID"].ToString()); // == ddlSearchIn contains Requisition, quotation
        this.GridView1.DataBind();
    }
}