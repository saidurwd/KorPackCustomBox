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

public partial class Index : System.Web.UI.Page
{
    string strUserID = "",login;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/Sales/CalculatePrice.aspx");
      //  if (Session["UserID"] == null)
      //  {
      //      //Server.Transfer("~/Login.aspx");
      //      Response.Redirect("~/Login.aspx");
      //  }
      //  else
      //  {
      //      strUserID = Session["UserID"].ToString();
      //  }
       
      //  //this.Page.Title = ConfigurationManager.AppSettings["PageTitle"];

      //clsCommon objCom = new clsCommon();
      //objCom.set("./Buttons/HomeC.png");
    }
    
      
      
}
