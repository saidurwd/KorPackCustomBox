
//******************************************
// Author : Faruk Ahmed
// Development Date : 30th of May 2012
// Module : User Authentication
//******************************************

using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Data.SqlClient;
using Blumen;

namespace StringEncodeDecode
{
    public partial class UserAuthentication_NewUser : System.Web.UI.Page
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
                }
                SqlDataSource1.SelectCommand = @"SELECT RoleID, RoleDesc FROM (SELECT RoleID, RoleDesc,company_id FROM TB_PrmUserRole where company_id='" + strcompanyID + "') AS TBL ORDER BY RoleDesc";
                SqlDataSource1.DataBind();

            }
        }
        protected override void InitializeCulture()
        {
            if (Request.Cookies["culture"] == null) return;
            Page.Culture = Request.Cookies["culture"].Value;
            Page.UICulture = Request.Cookies["culture"].Value;
            
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
                    string strLogin = "", strDecPass = "";
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
                                objDML.CustomerID = ddlCustomer.SelectedValue.ToString();
                                objDML.COMPANY_ID = this.strcompanyID;
                                objDML.Connection.Open();
                                objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                                objDML.Execute();

                                objDML.Transaction.Commit();
                                string myScript = "showInfo('" + Resources.Resource.MsgSaveGeneral + "');";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                                myScript = "";
                                

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