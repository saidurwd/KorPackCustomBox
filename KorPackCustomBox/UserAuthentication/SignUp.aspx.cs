
//******************************************
// Author : Faruk Ahmed
// Development Date : 30th of May 2012
// Module : User Authentication
//******************************************

using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Data.SqlClient;
using Blumen;
using KPCustomBox;

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

            this.hidMenuID.Value = "70";
            strcompanyID = "01";
            if (!IsPostBack)
            {
                FillCapctha();
            }

        }
        void FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);
                Session["captcha"] = captcha.ToString();
                imgCaptcha.ImageUrl = "captcha.aspx?" + DateTime.Now.Ticks.ToString();
            }
            catch
            {


                throw;
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
            if (txtVerificationCode.Text.ToLower() != Session["verify"].ToString())
            {
                string myScript = "showInfo('Verification code is incorrect. Please enter it again.');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                myScript = "";
                return;
            }

            string strPass = this.txtPassword.Text;
            string strConfPass = this.txtRetypePassword.Text;
            if ((strPass.Trim().Length < 5) || (strConfPass.Trim().Length < 5))
            {
                myScript = "alert('Password strength is Weak. Password can not be accepted');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            }
            if (strPass == strConfPass)
            {
                int recCount = 0;
                clsUserInfo objUserInfo = new clsUserInfo();
                string strLogin = "", strDecPass = "";
                strLogin = this.txtEmail.Text;
                if ((strLogin.Length > 0) && (strLogin != "0"))
                {
                    object objRecCount2 = objUserInfo.CheckDuplicate(strLogin);

                    recCount = int.Parse(objRecCount2.ToString());
                    if (recCount > 0)
                    {
                        string myScript = "showInfo('Duplicate email address. Please provide a valid one.');";
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                        myScript = "";

                        //myScript = "alert('Duplicate login id');";
                        ////Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                    }
                    else
                    {
                        //strDecPass = obStrMan.EncryptString(strPass);
                        clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                        strPass += strLogin;
                        strDecPass = objEncodeString.getMd5Hash(strPass);
                        SqlConnection connection = new SqlConnection(cnnString);
                        InsertUserInfoSignUp objDML = new InsertUserInfoSignUp(connection);

                        try
                        {
                            objDML.UserName = this.NameofUser.Text;
                            objDML.CompanyName = this.txtCompanyName.Text;
                            objDML.Email = this.txtEmail.Text;
                            objDML.Password = strDecPass;
                            objDML.IsSignUpRequest = 1;
                            objDML.Connection.Open();
                            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
                            objDML.Execute();
                            if (objDML.ReturnValue == 0)
                            {
                                objDML.Transaction.Commit();
                                string myScript = "showInfo('Sign up Successfull. Please check your inbox. You will get an email. Please follow the link to activate your account. Thank you.');";
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                                myScript = "";
                                sendemailoffice365(this.txtEmail.Text, this.NameofUser.Text);
                            }
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
        private void sendemailoffice365(string semail, string username)
        {
            string EncryptuserId= Extensions.Encrypt(semail);

            
            string myString = "Korpack: Account Activation";
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress("quotes@korpack.com", "KorPack");
            msg.To.Add(new System.Net.Mail.MailAddress(semail, username));
            msg.Bcc.Add(new System.Net.Mail.MailAddress("nnovy@korpack.com", "Nick Novy"));
            //msg.Bcc.Add(new System.Net.Mail.MailAddress("sales@korpack.com", "Korpack Sales"));
            msg.Bcc.Add(new System.Net.Mail.MailAddress("faaruk@yahoo.com", "Faruk Ahmed"));

            msg.Subject = myString;
            msg.Body = emailbody(username, EncryptuserId);
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
        private string emailbody(string username, string EncryptuserId)
        {
            string body = @"<html xmlns='http://www.w3.org/1999/xhtml'>
                <head><title></title></head>
                <body>
                <img src='"+ ConfigurationManager.AppSettings["RootPath"] + @"Images/quickor-logo-3_transparent54.jpg' alt='' /><br /><br />
                <div style = 'border-top:3px solid #22BCE5'>&nbsp;</div>
                <span style = 'font-family:Arial;font-size:10pt'>
                Dear <b>" + username + @"</b>,<br /><br />
                <p>Your Korpack account has been created successfully. Please click the link below to login and get started.</p><br />
                <a style = 'color:#22BCE5' href='" + ConfigurationManager.AppSettings["RootPath"] + "ActivateAccount.aspx?act_r="+ EncryptuserId + @"'>Activate Account</a>
                <br /><br />
                Thank you<br />
                Korpack Team<br />
                630-213-3600
                </span>
                </body>";
            return body;
        }
    }
}