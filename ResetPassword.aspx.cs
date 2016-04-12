using System;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using Blumen;
using KPCustomBox;

public partial class Sales_DirGroup : System.Web.UI.Page
{
    //string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginid = KPCustomBox.Extensions.Decrypt(Request.QueryString["act_r"]);
            txtEmail.Text = loginid;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //if (txtVerificationCode.Text.ToLower() != Session["verify"].ToString())
        //{
        //    string myScript = "showInfo('Verification code is incorrect. Please enter it again.');";
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
        //    myScript = "";
        //    return;
        //}
        string myScript;
        string strPass = this.txtPassword.Text;
        string strConfPass = this.txtRetypePassword.Text;
        if ((strPass.Trim().Length < 5) || (strConfPass.Trim().Length < 5))
        {
            myScript = "alert('Password strength is Weak. Password can not be accepted');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
        }
        if (strPass == strConfPass)
        {

            clsUserInfo objUserInfo = new clsUserInfo();
            string strLogin = "", strDecPass = "";
            strLogin = this.txtEmail.Text;
            if ((strLogin.Length > 0) && (strLogin != "0"))
            {

                //strDecPass = obStrMan.EncryptString(strPass);
                clsMD5StringEncode objEncodeString = new clsMD5StringEncode();
                strPass += strLogin;
                strDecPass = objEncodeString.getMd5Hash(strPass);
                SqlConnection connection = new SqlConnection(cnnString);
                InsertUserInfoSignUp objDML = new InsertUserInfoSignUp(connection);

                try
                {
                    string logOutPage = ResolveUrl("~/Logout.aspx");
                    objUserInfo.SetPassword(strLogin, strDecPass);
                    //myScript = "showInfo('Set new password successful.');self.parent.window.top.location='" + logOutPage + "'";
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                    //myScript = "";


                    //myScript = "self.parent.window.top.location='" + logOutPage + "';";
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ClientScript", myScript, true);

                    string message = "fun_ShowPassChangeMessage();";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", message, true);

                }
                catch (SqlException exp)
                {
                    myScript = "showError('" + Resources.Resource.DataBaseError + "');";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                    myScript = "";

                }
                catch (Exception exp)
                {
                    myScript = "showError('" + Resources.Resource.GeneralError + "');";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                    myScript = "";
                }
                finally
                {
                    objUserInfo.Dispose();

                }


            }
        }
    }
}
