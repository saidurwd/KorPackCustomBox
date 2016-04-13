
//******************************************
// Author : Faruk Ahmed
// Development Date : 1st of June 2012
// Module : User Authentication
//******************************************

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using Blumen;

namespace StringEncodeDecode
{
    public partial class _Login : System.Web.UI.Page
    {

        string strCompanyID = String.Empty;
        string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyID"] == null)
            {
                Session.Add("CompanyID", "");
                Session.Add("BranchID", "");
            }
            Session["CompanyID"] = "01";
            Session["BranchID"] = "01";
            strCompanyID = "KORPACK";

            if (Request.Cookies["CompanyID"] == null)
            {
                HttpCookie CID = new HttpCookie("CompanyID");
                CID.Value = "01";
                CID.Expires = DateTime.Now.AddDays(7.0);
                Response.Cookies.Set(CID);
            }
            if (Session["UserID"] != null)
            {
                Response.Redirect("/Sales/CalculatePrice.aspx");
            }

            this.txtLoginID.Focus();
        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                Session["CompanyID"] = "01";
                

                if (Session["CompanyID"] != null)
                {
                    
                    strCompanyID = "01";
                    clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                    string textToEncode = this.txtPassword.Text.Trim() + this.txtLoginID.Text;
                    string strDecPass = objEncodeString.getMd5Hash(textToEncode);
                    SqlConnection connection = new SqlConnection(cnnString);
                    SelectLogInfo objDML = new SelectLogInfo(connection);

                    try
                    {
                        objDML.CompanyID = strCompanyID;
                        objDML.LoginID = this.txtLoginID.Text;
                        objDML.Password = strDecPass;
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
                            connection = new SqlConnection(cnnString);
                            InsertBlumenSoftUserLoginLog objDML2 = new InsertBlumenSoftUserLoginLog(connection);
                            try
                            {
                                objDML2.LoginDate = System.DateTime.Now;
                                objDML2.LogOutDate = System.DateTime.Now;
                                objDML2.UserID = Int32.Parse(dt.Rows[0][1].ToString());
                                objDML2.Connection.Open();
                                objDML2.Transaction = objDML2.Connection.BeginTransaction("myTran");
                                DataTable dt2 = objDML2.ExecuteDataSet().Tables[0];
                                objDML2.Transaction.Commit();
                                Session.Add("LogAutoID", dt2.Rows[0][0].ToString());
                            }
                            catch (SqlException exp)
                            {
                                objDML2.Transaction.Rollback();
                                Response.Write(exp.Message);
                            }
                            finally
                            {
                                if (objDML2.Connection.State == ConnectionState.Open)
                                {
                                    objDML2.Connection.Close();
                                    objDML2.Connection.Dispose();
                                }
                            }
                            string[] arrLogInfo = new string[25];
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    arrLogInfo[i++] = dr[dc].ToString();
                                }
                            }
                            dt.Dispose();

                            if ((i > 0) && (arrLogInfo[0].Length > 0))
                            {
                                Session.Add("LoginID", arrLogInfo[0]);
                                Session.Add("UserID", arrLogInfo[1]);
                                Session.Add("UserName", arrLogInfo[2]);
                                Session.Add("RoleID", arrLogInfo[10]);
                                Session.Add("CustomerId", arrLogInfo[12]);
                                clsGlobal gb = new clsGlobal();
                                gb.setLogin("1");
                                gb.setClickedV("");
                                Response.Redirect("/Sales/CalculatePrice.aspx", false);
                            }

                        }
                        else
                        {
                            this.lblLoginFail.Visible = true;

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
                else
                {
                    this.lblLoginFail.Visible = true;
                    this.lblLoginFail.Text = "Please Select Company";
                }
            }
            catch (SqlException exp)
            {
                this.lblLoginFail.Visible = true;
                this.lblLoginFail.Text = exp.Message;

            }
            finally
            {

            }
        }



    }

}