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
using System.Text;
using Blumen;
using KPCustomBox;

public partial class UA_ManageRole : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string strCompanyID = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserID"] != null)
        {
            this.hidMenuID.Value = "232";
            strCompanyID = Session["CompanyID"].ToString();
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            mblnUserPermissionMaster();
        }
        
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Check Permission
        SqlConnection connectionUA = new SqlConnection(cnnString);
        SelectUserPermission objDMLUA = new SelectUserPermission(connectionUA);
        string strAction = "1";

        if (this.HidCID.Value.Length == 0)
        {
            strAction = "1";
        }
        else
        {
            strAction = "2";
        }
        objDMLUA.companyID = Session["CompanyID"].ToString();
        objDMLUA.strAction = strAction;
        objDMLUA.strMenuID = this.hidMenuID.Value.ToString();
        objDMLUA.UserID = Session["UserID"].ToString();

        objDMLUA.Connection.Open();
        objDMLUA.Transaction = objDMLUA.Connection.BeginTransaction("myTran");
        DataTable dt = objDMLUA.ExecuteDataSet().Tables[0];

        if ((objDMLUA.ReturnValue == 0) && (dt.Rows.Count > 0))
        {
            objDMLUA.Transaction.Commit();
            string strpermission;
            strpermission = dt.Rows[0]["val"].ToString();
            if (strpermission == "0")
            {
                string myScriptUA = "showError('" + Resources.Resource.UserPermission + "');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScriptUA, true);
                myScriptUA = "";
                return;
            }
        }
        else
        {
            objDMLUA.Transaction.Rollback();
        }
        #endregion Check Permission


        if (this.HidCID.Value.Length == 0)
        {
            mSave();
        }
        else
        {
            mUpdate();
        }

    }

    protected void mSave()
    {

        SqlConnection connection = new SqlConnection(cnnString);
        InsertCBPriceClass objDML = new InsertCBPriceClass(connection);
        try
        {        
            objDML.PriceClassDesc = this.txtDescription.Text;
            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
            objDML.Execute();

            if (objDML.ReturnValue == 0)
            {
                objDML.Transaction.Commit();

                string myScript = "showInfo('" + Resources.Resource.MsgSaveGeneral + "');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                myScript = "";
                this.txtDescription.Text = "";

                this.GridView1.DataSourceID = "SqlDataSource1";
                this.GridView1.DataBind();
            }
        }
        catch (SqlException exp)
        {

            objDML.Transaction.Rollback();
            string myScript = "showError('" + Resources.Resource.DataBaseError + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";

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

    protected void mUpdate()
    {

        SqlConnection connection = new SqlConnection(cnnString);
        UpdateCBPriceClass objDML = new UpdateCBPriceClass(connection);
        try
        {
            objDML.PCAutoId = Int32.Parse(this.HidCID.Value.ToString());
            objDML.PriceClassDesc = this.txtDescription.Text;
            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
            objDML.Execute();

            if (objDML.ReturnValue == 0)
            {
                objDML.Transaction.Commit();

                string myScript = "showInfo('" + Resources.Resource.MsgUpdateGeneral + "');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                myScript = "";
                this.txtDescription.Text = "";
                this.HidCID.Value = "";

                this.GridView1.DataSourceID = "SqlDataSource1";
                this.GridView1.DataBind();
            }
        }
        catch (SqlException exp)
        {

            objDML.Transaction.Rollback();
            string myScript = "showError('" + Resources.Resource.DataBaseError + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";

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

    private void mblnUserPermissionMaster()
    {
        SqlConnection connectionUA = new SqlConnection(cnnString);
        SelectUserPermissionMaster objDMLUA = new SelectUserPermissionMaster(connectionUA);
        objDMLUA.companyID = Session["CompanyID"].ToString();
        objDMLUA.strMenuID = this.hidMenuID.Value.ToString();
        objDMLUA.UserID = Session["UserID"].ToString();

        objDMLUA.Connection.Open();
        objDMLUA.Transaction = objDMLUA.Connection.BeginTransaction("myTran");
        DataTable dt = objDMLUA.ExecuteDataSet().Tables[0];

        if ((objDMLUA.ReturnValue == 0) && (dt.Rows.Count > 0))
        {
            objDMLUA.Transaction.Commit();
            string strpermission;
            strpermission = dt.Rows[0]["val"].ToString();
            if (strpermission == "0")
            {
                Response.Redirect("~/Login.aspx");

            }
        }
        else
        {
            objDMLUA.Transaction.Rollback();
        }
    }
    private string CreateListOfItem2()
    {
        StringBuilder strServiceList = new StringBuilder();
        foreach (GridViewRow gr in GridView1.Rows)
        {

            CheckBox _chkRow = gr.FindControl("chkRow") as CheckBox;
            if (_chkRow.Checked == true)
            {
                TextBox _txtRefNo = gr.FindControl("txtKeyField") as TextBox;
                strServiceList.Append("" + _txtRefNo.Text + "");
                strServiceList.Append(",");
            }
        }
        string strText = strServiceList.ToString();
        if (strText.Length > 0)
        {
            strText = strText.Substring(0, strText.Length - 1);
        }
        return strText;

    }
    protected void cmdDelete_Click(object sender, EventArgs e)
    {
        string completeString = CreateListOfItem2();
        string myScript = "";
        if (completeString.Length == 0)
        {
            myScript = "showInfo('Please select a Role to delete.');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            myScript = "";
            return;
        }

        ExecuteSQL obAtten = new ExecuteSQL();
        DataTable dt = null;
        dt = new DataTable();
        string strSQL = "";

        strSQL = @"SELECT COUNT(PCAutoId) CVAL FROM CB_Customers WHERE PCAutoId IN (" + completeString + ")";
        dt = obAtten.SearchReportRecordFromSQLQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CVAL"].ToString() != "0")
            {
                myScript = "showInfo('" + Resources.Resource.DepedentDataExists + "');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                myScript = "";
                return;
            }
        }

        strSQL = @"DELETE FROM [dbo].[CB_PriceClass] WHERE PCAutoId IN (" + completeString + "); ";
        object obj = null;
        obj = obAtten.ExecuteQuery(strSQL);

        this.GridView1.DataBind();
        GridView1.UseAccessibleHeader = true;
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

        myScript = "showInfo('" + Resources.Resource.MsgDeleteGeneral + "');";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
        myScript = "";

    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        string myScript = "$('#loading').hide();";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
        myScript = "";
    }
    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        if (this.GridView1.Rows.Count > 0)
        {
            //Gets or sets a value indicating whether a System.Web.UI.WebControls.GridView
            //control renders its header in an accessible format. This property is provided
            //to make the control more accessible to users of assistive technology devices.
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

        }


    }
}
