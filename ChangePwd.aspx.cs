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

namespace StringEncodeDecode
{
    public partial class _ChangePwd : System.Web.UI.Page
    {
        string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
        string strLogID;
        string strComapnyID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                string myScript = "window.top.location=\"Logout.aspx\";";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClientScript", myScript, true);
                myScript = "";
            }
            else
            {
                this.hidMenuID.Value = "25";
                strLogID = Session["LoginID"].ToString();
                strComapnyID = Session["CompanyID"].ToString();
                this.txtLoginID.Text = strLogID;
                this.txtHidLoginID.Value = strLogID;
                if (!IsPostBack)
                {
                    mblnUserPermissionMaster();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string myScript = "";
            string strEnrPass = this.txtPassword.Text.Trim();
            string strChangedPass = this.txtConfNewPassword.Text.Trim();
            clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
            try
            {
                strEnrPass += this.txtLoginID.Text.Trim();
                strEnrPass = objEncodeString.getMd5Hash(strEnrPass);
                SqlConnection connection = new SqlConnection(cnnString);
                SelectLogInfo objDML = new SelectLogInfo(connection);

                try
                {
                    objDML.CompanyID = strComapnyID;
                    objDML.LoginID = this.txtLoginID.Text;
                    objDML.Password = strEnrPass;
                    objDML.Connection.Open();
                    objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                    DataTable dt = objDML.ExecuteDataSet().Tables[0];
                    

                    if (dt.Rows.Count > 0)
                    {
                        if (objDML.Connection.State == ConnectionState.Open)
                        {
                            objDML.Connection.Close();
                            objDML.Connection.Dispose();
                        }
                        strChangedPass += this.txtLoginID.Text.Trim();
                        strChangedPass = objEncodeString.getMd5Hash(strChangedPass);
                        connection = new SqlConnection(cnnString);
                        UpdateChangePassword objDML2 = new UpdateChangePassword(connection);

                        try
                        {
                            
                            objDML2.PreLoginID = this.txtHidLoginID.Value;
                            objDML2.LoginID = this.txtLoginID.Text;
                            objDML2.NewPassword = strChangedPass;
                            objDML2.Connection.Open();
                            objDML2.Transaction = objDML2.Connection.BeginTransaction("myTran");
                            objDML2.Execute();
                            if ((objDML2.ReturnValue == 0))
                            {
                                objDML2.Transaction.Commit();
                            }

                            
                            Session.Abandon();
                            string message = "fun_ShowPassChangeMessage();";
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", message, true);
                            myScript = "";
                            
                        }
                        catch (SqlException exp)
                        {
                            objDML2.Transaction.Rollback();
                            string myScript1 = "showError('" + Resources.Resource.DataBaseError + "');";
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
                            myScript1 = "";
                        }
                        catch (Exception exp)
                        {
                            //objDML.Transaction.Rollback();
                            string myScript1 = "showError('" + Resources.Resource.GeneralError + "');";
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
                            myScript1 = "";
                        }
                        finally
                        {
                            if (objDML2.Connection.State == ConnectionState.Open)
                            {
                                objDML2.Connection.Close();
                                objDML2.Connection.Dispose();
                            }

                        }
                    }
                    else
                    {
                        myScript = "alert('LoginID and old password is not correct');";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
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
            catch (SqlException exp)
            {
                myScript = "alert('Erron in database  ');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);

            }
            finally
            {
                objEncodeString.Dispose();
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

    }
}