using System;
using System.Web.UI;
using System.Configuration;
using System.Data;
using Blumen;

public partial class Sales_DirGroup : System.Web.UI.Page
{
    //string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginid = KPCustomBox.Extensions.Decrypt(Request.QueryString["act_r"]);
            Activate(loginid);
        }
    }
    
    private void Activate(string userid)
    {
        
        string strSQL = "";
        strSQL = @"update TB_PrmUserInfo set Active=1
        where LoginID='" + userid + @"'";
        ExecuteSQL obAtten = new ExecuteSQL();
        DataSet ds = new DataSet();
        obAtten.ExecuteQuery(strSQL);
        string logOutPage = ResolveUrl("~/Logout.aspx");
        string myScript = "self.parent.window.top.location='" + logOutPage + "';";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ClientScript", myScript, true);

    }


}
