
//******************************************
// Author : Faruk Ahmed
// Development Date : 30th of May 2012
// Module : User Authentication
//******************************************

using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Blumen;

namespace StringEncodeDecode
{
    public partial class UserAuthentication_PrmUserInfo : System.Web.UI.Page
    {
        string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
        private string strLoginUserID = null;
        private string myScript = "";
        private string strcompanyID = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("../Logout.aspx");
            }
            else
            {
                this.hidMenuID.Value = "70";
                strcompanyID ="01";
                strLoginUserID = Session["UserID"].ToString();
                if (!IsPostBack)
                {
                    mblnUserPermissionMaster();
                    
                    FillUsers();
                }
                SqlDataSource1.SelectCommand = @"SELECT RoleID, RoleDesc FROM (SELECT RoleID, RoleDesc,company_id FROM TB_PrmUserRole where company_id='" + strcompanyID + "') AS TBL ORDER BY RoleDesc";
                SqlDataSource1.DataBind();
             
            }
        }
       

        protected void FillUsers()
        {
            SqlConnection connection = new SqlConnection(cnnString);
            SelectAllUsers objDML = new SelectAllUsers(connection);

            try
            {
                objDML.CompanyID = strcompanyID;
                objDML.wherecondition = string.Empty;
                objDML.OrderByExpression = string.Empty;
                objDML.Connection.Open();
                objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                DataTable dt = objDML.ExecuteDataSet().Tables[0];

                if ((objDML.ReturnValue == 0) && (dt.Rows.Count > 0))
                {
                    objDML.Transaction.Commit();
                    grdEmpInfo.DataSource = dt;
                    grdEmpInfo.DataBind();
                    if (this.grdEmpInfo.Rows.Count > 0)
                    {
                        //Gets or sets a value indicating whether a System.Web.UI.WebControls.GridView
                        //control renders its header in an accessible format. This property is provided
                        //to make the control more accessible to users of assistive technology devices.
                        grdEmpInfo.UseAccessibleHeader = true;
                        grdEmpInfo.HeaderRow.TableSection = TableRowSection.TableHeader;

                    }
                }
                else
                {
                    grdEmpInfo.DataSource = null;
                    grdEmpInfo.DataBind();
                }
            }
            catch (SqlException exp)
            {
                Response.Write(exp.Message);
            }
            finally
            {
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            FillUsers();
        }

        protected void grdEmpInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                //string lblUserStatus = ((Label)e.Row.FindControl("lblUserStatus")).Text;
                TextBox txtGrdPass = (TextBox)e.Row.FindControl("txtGrdPass");
                TextBox txtGrdConfPass = (TextBox)e.Row.FindControl("txtGrdConfPass");
                Label lblUserActive = (Label)e.Row.FindControl("lblUserActive");
                Label _lblgrdActiveDeactive = (Label)e.Row.FindControl("lblgrdActiveDeactive");

                if (txtGrdPass != null)
                {
                    txtGrdPass.Text = "000";
                }

                if (lblUserActive != null)
                {
                    if (lblUserActive.Text == "False")
                    {
                        lblUserActive.Text = "No";
                        _lblgrdActiveDeactive.Text = "Inactive";
                    }
                    else
                    {
                        lblUserActive.Text = "Yes";
                        _lblgrdActiveDeactive.Text = "Active";
                    }
                }
               
            }

        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            try
            {
                FillUsers();
            }


            catch (SqlException exp)
            {
                myScript = "alert('" + exp.Message + "');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                myScript = "";
            }
        }

        protected void grdEmpInfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdEmpInfo.EditIndex = e.NewEditIndex;
            FillUsers();
            ((TextBox)grdEmpInfo.Rows[e.NewEditIndex].FindControl("txtGrdLoginID")).Focus();
        }

        protected void grdEmpInfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdEmpInfo.EditIndex = -1;
            FillUsers();
            
        }

        protected void grdEmpInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection connection = new SqlConnection(cnnString);
            string strUserName = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGridFullName")).Text;
            string strEmail = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGrdEditEmail")).Text;
            DropDownList cboGrdCustomer = (DropDownList)this.grdEmpInfo.Rows[e.RowIndex].FindControl("ddlGrdEditCustomer");
            string strCustomerId = cboGrdCustomer.SelectedValue.ToString();

            string strUserID = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtUserID")).Text;
            DropDownList cboGrdRole = (DropDownList)this.grdEmpInfo.Rows[e.RowIndex].FindControl("cboGrdRole");
            string strRoleID = cboGrdRole.SelectedValue.ToString();

            string strLoginID = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGrdLoginID")).Text;
            string lblPrvLoginID = ((Label)this.grdEmpInfo.Rows[e.RowIndex].FindControl("lblPrvLoginID")).Text;

            string strPass = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGrdPass")).Text;
            string strConfPass = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGrdConfPass")).Text;
            DropDownList cboUserActive = (DropDownList)this.grdEmpInfo.Rows[e.RowIndex].FindControl("cboUserActive");
            string strDecPass = "";
            try
            {
                if ((strPass.Trim().Length < 1) || (strConfPass.Trim().Length < 1))
                {
                    strDecPass = "";
                    UpdateUserInfo objDML = new UpdateUserInfo(connection);
                    objDML.Connection.Open();
                    objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                    objDML.UserID = strUserID;
                    objDML.LoginID = strLoginID;
                    objDML.Password = strDecPass;
                    objDML.RoleID = strRoleID;
                    objDML.CreatedBy = strLoginUserID;
                    objDML.UserActive = cboUserActive.SelectedValue.ToString();
                    objDML.COMPANY_ID = strcompanyID;
                    objDML.NameOfUser = strUserName;
                    objDML.EMail = strEmail;
                    objDML.CustomerId = strCustomerId;
                    objDML.Execute();
                    if ((objDML.ReturnValue == 0))
                    {
                        objDML.Transaction.Commit();
                        grdEmpInfo.EditIndex = -1;
                        FillUsers();
                    }

                    //myScript = "alert('Password strength is Weak. Password can not be accepted');";
                    ////Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                    //grdEmpInfo.EditIndex = -1;
                    //FillUsers();
                    //return;
                }
                else
                {

                    if (strPass == strConfPass)
                    {
                        //StringManipulation obStrMan = new StringManipulation();
                        

                        if ((strRoleID.Length > 0) && (strRoleID != "0"))
                        {

                            int recCount = 0;
                            clsUserInfo objUserInfo = new clsUserInfo();

                            if ((strUserID.Length > 0) && (strUserID != "0"))
                            {

                                string strGrdPassOLD = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGrdPassOLD")).Text;
                                string strGrdConfPassOLD = ((TextBox)this.grdEmpInfo.Rows[e.RowIndex].FindControl("txtGrdConfPassOLD")).Text;


                                if (strLoginID != lblPrvLoginID)
                                {
                                    object objRecCount1 = objUserInfo.CheckDuplicate(strLoginID);

                                    recCount = int.Parse(objRecCount1.ToString());
                                    if (recCount > 0)
                                    {
                                        myScript = "alert('Duplicate login id');";
                                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
                                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                                    }

                                    else
                                    {
                                        if (strGrdPassOLD == strPass)  // means password from database is same
                                        {
                                            strDecPass = strPass; //means already encrypted
                                        }
                                        else
                                        {
                                            //strDecPass = obStrMan.EncryptString(strPass);
                                            clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                                            strPass += strLoginID;
                                            strDecPass = objEncodeString.getMd5Hash(strPass);

                                        }
                                        UpdateUserInfo objDML = new UpdateUserInfo(connection);
                                        objDML.Connection.Open();
                                        objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                                        objDML.UserID = strUserID;
                                        objDML.LoginID = strLoginID;
                                        objDML.Password = strDecPass;
                                        objDML.RoleID = strRoleID;
                                        objDML.CreatedBy = strLoginUserID;
                                        objDML.UserActive = cboUserActive.SelectedValue.ToString();
                                        objDML.COMPANY_ID = strcompanyID;
                                        objDML.NameOfUser = strUserName;
                                        objDML.EMail = strEmail;
                                        objDML.CustomerId = strCustomerId;
                                        
                                        objDML.Execute();
                                        if ((objDML.ReturnValue == 0))
                                        {
                                            objDML.Transaction.Commit();
                                            grdEmpInfo.EditIndex = -1;
                                            FillUsers();
                                        }
                                        //object objRec = objUserInfo.UpdateRec(strLoginID, strDecPass, strRoleID, strEmpAutoID, strUserID, strLoginUserID, cboUserActive.SelectedValue.ToString(), strcompanyID);
                                        //grdEmpInfo.EditIndex = -1;
                                        //FillUsers();
                                    }
                                }
                                else
                                {
                                    if (strGrdPassOLD == strPass)  // means password from database is same
                                    {
                                        strDecPass = strPass; //means already encrypted
                                    }
                                    else
                                    {
                                        //strDecPass = obStrMan.EncryptString(strPass);
                                        clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                                        strPass += strLoginID;
                                        strDecPass = objEncodeString.getMd5Hash(strPass);
                                    }
                                    UpdateUserInfo objDML = new UpdateUserInfo(connection);
                                    objDML.Connection.Open();
                                    objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                                    objDML.UserID = strUserID;
                                    objDML.LoginID = strLoginID;
                                    objDML.Password = strDecPass;
                                    objDML.RoleID = strRoleID;
                                    objDML.CreatedBy = strLoginUserID;
                                    objDML.UserActive = cboUserActive.SelectedValue.ToString();
                                    objDML.COMPANY_ID = strcompanyID;
                                    objDML.NameOfUser = strUserName;
                                    objDML.EMail = strEmail;
                                    objDML.CustomerId = strCustomerId;
                                    objDML.Execute();
                                    if ((objDML.ReturnValue == 0))
                                    {
                                        objDML.Transaction.Commit();
                                        grdEmpInfo.EditIndex = -1;
                                        FillUsers();
                                    }
                                    //object objRecAdd = objUserInfo.UpdateRec(strLoginID, strDecPass, strRoleID, strEmpAutoID, strUserID, strLoginUserID, cboUserActive.SelectedValue.ToString(), strcompanyID);
                                    //grdEmpInfo.EditIndex = -1;
                                    //FillUsers();
                                }
                            }
                            else
                            {
                                object objRecCount2 = objUserInfo.CheckDuplicate(strLoginID);

                                recCount = int.Parse(objRecCount2.ToString());
                                if (recCount > 0)
                                {
                                    myScript = "alert('Duplicate login id');";
                                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
                                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                                }
                                else
                                {
                                    //strDecPass = obStrMan.EncryptString(strPass);
                                    clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                                    strPass += strLoginID;
                                    strDecPass = objEncodeString.getMd5Hash(strPass);
                                    
                                    //InsertUserInfo objDML = new InsertUserInfo(connection);
                                    //object objRec = objUserInfo.InsertRec(strLoginID, strDecPass, strRoleID, strEmpAutoID, strLoginUserID, strcompanyID);
                                    //grdEmpInfo.EditIndex = -1;
                                    //FillUsers();
                                }
                            }
                        }
                        else
                        {
                            myScript = "alert('Please, select a role for this user')";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript, true);
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                        }
                    }

                    else
                    {
                        myScript = "alert('Password and confirm password does not match')";
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", myScript, true);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                    }
                }

            }
            catch (SqlException exp)
            {
                Response.Write(exp.ToString());

            }

        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            
            string strPass = this.txtPassword.Text;
            string strConfPass = this.txtRetypePassword.Text;
            if ((strPass.Trim().Length < 5) || (strConfPass.Trim().Length < 5))
            {
                myScript = "alert('Password strength is Weak. Password can not be accepted');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            }
            if (strPass == strConfPass)
            {
                string strRoleID = cboLoginRole.SelectedValue.ToString();

                if ((strRoleID.Length > 0) && (strRoleID != "0"))
                {

                    int recCount = 0;
                    clsUserInfo objUserInfo = new clsUserInfo();
                    string strLogin = "", strDecPass="";
                    strLogin = this.Login.Text;
                    if ((strLogin.Length > 0) && (strLogin != "0"))
                    {
                        object objRecCount2 = objUserInfo.CheckDuplicate(strLogin);

                        recCount = int.Parse(objRecCount2.ToString());
                        if (recCount > 0)
                        {
                            myScript = "alert('Duplicate login id');";
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                        }
                        else
                        {
                            //strDecPass = obStrMan.EncryptString(strPass);
                            clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                            strPass += strLogin;
                            strDecPass = objEncodeString.getMd5Hash(strPass);
                            SqlConnection connection = new SqlConnection(cnnString);
                            InsertUserInfo objDML = new InsertUserInfo(connection);

                            try
                            {
                                objDML.UserName = this.NameofUser.Text;
                                objDML.LoginID = this.Login.Text;
                                objDML.Email = this.txtEmail.Text;
                                objDML.Password = strDecPass;
                                objDML.CreatedBy = this.strLoginUserID;
                                objDML.RoleID = strRoleID;
                                objDML.COMPANY_ID = this.strcompanyID;
                                objDML.Connection.Open();
                                objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                                objDML.Execute();

                                objDML.Transaction.Commit();
                                string myScript = "showInfo('" + Resources.Resource.MsgSaveGeneral + "');";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                                myScript = "";
                                FillUsers();

                            }
                            catch (SqlException exp)
                            {

                                objDML.Transaction.Rollback();

                                //Response.Write("=======" + exp.ToString());

                                string myScript = "showError('" + Resources.Resource.DataBaseError + "');";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                                myScript = "";

                            }
                            catch (Exception exp)
                            {

                                objDML.Transaction.Rollback();
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
                }
            }
        }
        public void Page_LoadComplete()
        {
            //if (Request.Browser.Browser == "Firefox") Response.Cache.SetNoStore();
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
            objDMLUA.companyID = "01";
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
}