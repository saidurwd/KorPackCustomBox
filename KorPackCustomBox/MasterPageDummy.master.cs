using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageDummy : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {        
            //Response.Redirect(ResolveUrl("~/Logout.aspx"));
             string logOutPage = ResolveUrl("~/Logout.aspx");
             string myScript = "self.parent.window.top.location='" + logOutPage + "';window.close();";             
             ScriptManager.RegisterStartupScript(this.Page,this.GetType(), "ClientScript", myScript, true);
        }
    }
}
