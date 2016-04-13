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
using KPCustomBox;

public partial class Configuration_AboutBlumenSoft : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string strCompanyID = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        SqlConnection connection = new SqlConnection(cnnString);
        InsertCBContactUS objDML = new InsertCBContactUS(connection);
        try
        {

            objDML.Name = this.txtName.Text;
            objDML.Company = txtCompany.Text;
            objDML.Street = txtStreet.Text;
            objDML.City = txtCity.Text;
            objDML.State= txtState.Text;
            objDML.ZipCode = txtZipCode.Text;
            objDML.Phone = txtPhone.Text;
            objDML.Email = txtEmail.Text;
            objDML.Subject = txtSubject.Text;
            objDML.Message = txtMessage.InnerText;
            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
            objDML.Execute();

            
            if (objDML.ReturnValue == 0)
            {
                objDML.Transaction.Commit();
                sendemailoffice365Sales();
                sendemailoffice365ToThePerson();
                this.txtName.Text = "";
                this.txtCompany.Text = "";
                this.txtStreet.Text = "";
                this.txtCity.Text = "";
                this.txtState.Text = "";
                this.txtZipCode.Text = "";
                this.txtPhone.Text = "";
                this.txtEmail.Text = "";
                this.txtSubject.Text = "";
                this.txtMessage.InnerText = "";
                string myScript1 = "showInfo('Email Sent Successfully. Thank you for contacting us!');";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
                myScript1 = "";
            }
            
        }
        catch (Exception ex)
        {

            string myScript1 = "showInfo('" + Resources.Resource.ErrorMessage + "');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClientScript", myScript1, true);
            myScript1 = "";
        }
    }
    private void sendemailoffice365ToThePerson()
    {
        
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("quotes@korpack.com", "KorPack");
        msg.To.Add(new System.Net.Mail.MailAddress(this.txtEmail.Text, txtName.Text));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("nnovy@korpack.com", "Nick Novy"));
        msg.Subject = "Thank you from Korpack";
        msg.Body ="Hello " + this.txtName.Text + ",<br><br>Thank you for contacting us! We will get back to you soon. .<br><br> Best Regards, <br>Korpack Team";
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
    private void sendemailoffice365Sales()
    {

        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("quotes@korpack.com", "KorPack");
        msg.To.Add(new System.Net.Mail.MailAddress("sales@korpack.com ", "KorPack Sales"));
        msg.Bcc.Add(new System.Net.Mail.MailAddress("nnovy@korpack.com", "Nick Novy"));
        msg.Subject = "New Contact us for QuicKor: " + this.txtSubject.Text;
        msg.Body = this.txtMessage.InnerText;
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
}