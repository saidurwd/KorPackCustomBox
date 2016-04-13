using System;
using System.Web.UI;
using System.Configuration;
using System.Data;
using Blumen;
using KPCustomBox;

public partial class Sales_DirGroup : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        int recCount = 0;
        clsUserInfo objUserInfo = new clsUserInfo();
        string strLogin = "";
        strLogin = this.txtEmail.Text;
        DataTable dt = null;
        if ((strLogin.Length > 0) && (strLogin != "0"))
        {
            dt = objUserInfo.GetUserInfoByEmailID(strLogin);

            recCount = dt.Rows.Count;
            if (recCount < 1)
            {
                //string myScript = "showInfo('Duplicate email address. Please provide a valid one.');";
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
                //myScript = "";

                string myScript = "alert('Invalid email address. Please check and try again.');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript, true);
            }
            else
            {
                sendemailoffice365(dt.Rows[0]["Email"].ToString(), dt.Rows[0]["UserName"].ToString());
                string myScript = "alert('Password reset successful. Please check your inbox for instructions.');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myKdy", myScript, true);
            }
        }
    }
    private void sendemailoffice365(string semail, string username)
    {
        string EncryptuserId = Extensions.Encrypt(semail);


        string myString = "Korpack: Password Reset";
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
                <img src='" + ConfigurationManager.AppSettings["RootPath"] + @"Images/quickor-logo-3_transparent54.jpg' alt='' /><br /><br />
                <div style = 'border-top:3px solid #22BCE5'>&nbsp;</div>
                <span style = 'font-family:Arial;font-size:10pt'>
                Dear <b>" + username + @"</b>,<br /><br />
                <p>Your Korpack account password reset was successfull. Please click the link below to set new password.</p><br />
                <a style = 'color:#22BCE5' href='" + ConfigurationManager.AppSettings["RootPath"] + "ResetPassword.aspx?act_r=" + EncryptuserId + @"'>Set new password</a>
                <br /><br />
                Thank you<br />
                Korpack Team<br />
                630-213-3600
                </span>
                </body>";
        return body;
    }
}
