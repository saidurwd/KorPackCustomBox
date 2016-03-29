
//******************************************
// Author : Faruk Ahmed
// Development Date : 1st of June 2012
// Module : User Authentication
//******************************************

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Blumen;

public partial class UserAuthentication_PrmUserFunction : System.Web.UI.Page
{
    clsUserFunction objUser = new clsUserFunction();
    static int clickdtect = 2;
    string strUserID = "";
    string strCompanyID = null;
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    
    int intTreeSelectedNode;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../Logout.aspx");
        }
        else
        {
            this.hidMenuID.Value = "71";
            strUserID = Session["UserID"].ToString();
            clsGlobal gb = new clsGlobal();
            gb.setV(Request.QueryString["pageURL"]);
            strCompanyID = "01";
            if (!IsPostBack)
            {
                mblnUserPermissionMaster();
            }
        }
    }

    private void showHideControls(bool bshHide)
    {
        this.Panel1.Visible = bshHide;
        this.btnCheck.Visible = bshHide;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strFuncId = "", strParentId = "", strAdd = "", strEdit = "", strDelete = "";

        string strSelectedID = "";

        foreach (GridViewRow gr in grdUserFunction.Rows)
        {
            Label _lblFuncID = gr.FindControl("lblFuncID") as Label; ;
            CheckBox _chkRowAdd = gr.FindControl("chkRowAdd") as CheckBox;
            CheckBox _chkRowEdit = gr.FindControl("chkRowEdit") as CheckBox;
            CheckBox _chkRowDelete = gr.FindControl("chkRowDelete") as CheckBox;
            Label _lblParentID = gr.FindControl("lblParentID") as Label; ;

            strFuncId = _lblFuncID.Text.ToString();
            if (_chkRowAdd.Checked == true)
            {
                strAdd = "1";
            }
            else
            {
                strAdd = "0";
            }

            if (_chkRowEdit.Checked == true)
            {
                strEdit = "1";
            }
            else
            {
                strEdit = "0";
            }
            if (_chkRowDelete.Checked == true)
            {
                strDelete = "1";
            }
            else
            {
                strDelete = "0";
            }

            strParentId = _lblParentID.Text.ToString();

            strSelectedID = strSelectedID + strFuncId + '@' + strAdd + '@' + strEdit + '@' + strDelete + '@' + strParentId + '@' + '1' + '@' + '1' + '@' + '1' + '@';
        }
        SqlConnection connection = new SqlConnection(cnnString);
        if (strSelectedID.Length > 0)
        {
            SPTBInsertUserPermission objDML = new SPTBInsertUserPermission(connection);

            try
            {
                objDML.AssignedTo = strSelectedID;
                objDML.UserID = this.cboUserDesc.SelectedValue;
                objDML.AssignedBy = strUserID;
                objDML.Splitter = "@";
                objDML.COMPANY_ID = strCompanyID;
                objDML.Connection.Open();
                objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                objDML.Execute();

                if (objDML.ReturnValue == 0)
                {
                    objDML.Transaction.Commit();
                    string myScript = "";
                    myScript = "showInfo('" + Resources.Resource.MsgUpdateGeneral + "');";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
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
    }

    protected void cboUserDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        clickdtect = 2;
        if ((this.cboUserDesc.SelectedItem.Value != "0") && (this.ddlModule.SelectedItem.Value != "0"))
        {
            this.generateFunctionList(this.cboUserDesc.SelectedValue, this.ddlModule.SelectedValue);
            this.btnCheck.Text = "Select/Unselect All";
        }
        
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        clickdtect = 2;
        if ((this.cboUserDesc.SelectedItem.Value != "0") && (this.ddlModule.SelectedItem.Value != "0"))
        {
            this.generateFunctionList(this.cboUserDesc.SelectedValue, this.ddlModule.SelectedValue);
            this.btnCheck.Text = "Select/Unselect All";
        }

    }

    protected void generateFunctionList(string UserID, string module)
    {
        DataTable dtResult = new DataTable();
        try
        {
            string strSQL = @" 
            select FuncID,ParentID,FuncDesc,
            isnull((SELECT top 1 isnull(FuncDesc,'') FROM TB_PrmFunction WHERE COMPANY_ID='" + strCompanyID + @"' AND FuncID=A.ParentID),'') ParentDesc,
            Module,Serial,convert(bit,isnull(PerAdd,0)) PerAdd,convert(bit,isnull(PerEdit,0)) PerEdit,convert(bit,isnull(PerDelete,0)) PerDelete,MenuEnableDisable,MenuInUse,MenuInShow
            From
            (
            Select UF.FuncID,F.ParentID,F.FuncDesc,'' ParentDesc,F.Module,F.Serial,UF.PerAdd,UF.PerEdit,UF.PerDelete,F.MenuEnableDisable,F.MenuInUse,F.MenuInShow
                                         From TB_PrmUserFunction UF  
                                         left join TB_PrmFunction F ON UF.FuncID=F.FuncID  
                                         left Join TB_PrmFunction F1 ON F.ParentID=F1.FuncID 
                                         left Join TB_PrmUserInfo UI ON UF.UserID=UI.UserID 
                                         Where UI.company_id = '" + strCompanyID + @"'
             and UF.company_id = '" + strCompanyID + @"'
            and F1.company_id = '" + strCompanyID + @"'
            and F.company_id = '" + strCompanyID + @"' and UI.UserID=" + UserID + @" 
            and F1.MenuEnableDisable=1 and F1.MenuInUse=1 
            and F.MenuEnableDisable=1 and F.MenuInUse=1 
            --and F1.MenuInShow=1 and F.MenuInShow=1
            union
            select FuncID,ParentID,FuncDesc, '' ParentDesc,Module,Serial,0 PerAdd,0 PerEdit, 0 PerDelete,MenuEnableDisable,MenuInUse,MenuInShow
                                         From 
                                         TB_PrmFunction
                                         Where company_id = '" + strCompanyID + @"'
                                         and FuncID not in(
                                         Select UF.FuncID
                                         From TB_PrmUserFunction UF  
                                         left join TB_PrmFunction F ON UF.FuncID=F.FuncID  
                                         left Join TB_PrmFunction F1 ON F.ParentID=F1.FuncID 
                                         left Join TB_PrmUserInfo UI ON UF.UserID=UI.UserID 
                                         Where UI.company_id = '" + strCompanyID + @"' 
             and UF.company_id = '" + strCompanyID + @"' 
            and F1.company_id = '" + strCompanyID + @"'
            and F.company_id = '" + strCompanyID + @"' and UI.UserID=" + UserID + @"  
            and F1.MenuEnableDisable=1 and F1.MenuInUse=1 
            and F.MenuEnableDisable=1 and F.MenuInUse=1 
            --and F1.MenuInShow=1 and F.MenuInShow=1
            )
            ) A
            where Module='" + module + @"'
            and MenuEnableDisable=1 and MenuInUse=1 
            and MenuInShow=1
            ORDER BY Module asc ,ParentID asc ,Serial asc
            ";



            dtResult = objUser.SelectData(strSQL);
            
            if (dtResult.Rows.Count > 0)
            {
                this.Panel1.Visible = true;
                this.grdUserFunction.Visible = true;
                this.grdUserFunction.DataSource = dtResult;
                this.grdUserFunction.DataBind();
            }
            else
            {
                this.grdUserFunction.DataSource = null;
                this.grdUserFunction.DataBind();
            }
        }

        catch (SqlException exp)
        {
            Response.Write(exp.Message);
        }
        finally
        {
            UpdatePanel1.Update();
            dtResult.Dispose();
        }
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        int check = 2;
        if ((clickdtect % check) == 0)
        {
            foreach (GridViewRow gr in grdUserFunction.Rows)
            {
                CheckBox chkRow = gr.FindControl("chkRowAdd") as CheckBox;
                CheckBox chkRowEdit = gr.FindControl("chkRowEdit") as CheckBox;
                CheckBox chkRowDelete = gr.FindControl("chkRowDelete") as CheckBox;
                if (chkRow != null)
                {
                    chkRow.Checked = true;
                    chkRowEdit.Checked = true;
                    chkRowDelete.Checked = true;
                }
                else
                {
                    string myScript1 = "alert('Unable to select the data.');";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript1, true);

                }
            }
            this.btnCheck.Text = "Unselect all";
        }
        else
        {
            foreach (GridViewRow gr in grdUserFunction.Rows)
            {
                CheckBox chkRow = gr.FindControl("chkRowAdd") as CheckBox;
                CheckBox chkRowEdit = gr.FindControl("chkRowEdit") as CheckBox;
                CheckBox chkRowDelete = gr.FindControl("chkRowDelete") as CheckBox;
                if (chkRow != null)
                {
                    chkRow.Checked = false;
                    chkRowEdit.Checked = false;
                    chkRowDelete.Checked = false;
                }
                else
                {
                    string myScript1 = "alert('Unable to select the data.');";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript1, true);

                }
            }
            this.btnCheck.Text = "Select all";
        }
        clickdtect++;
    }

    protected void grdUserFunction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUserFunction.PageIndex = e.NewPageIndex;
        if ((this.cboUserDesc.SelectedItem.Value != "0") && (this.ddlModule.SelectedItem.Value != "0"))
        {
            this.generateFunctionList(this.cboUserDesc.SelectedValue, this.ddlModule.SelectedValue);
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        string myScript = "$('#loading').hide();";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
        myScript = "";
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

}
