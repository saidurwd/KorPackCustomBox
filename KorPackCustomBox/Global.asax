<%@ Application Language="C#" %>
<%--<%@ application CodeBehind="Global.asax.vb" Inherits="rdWeb.Global" %>--%>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["appCtr"] = 0;
        Application["noOfUsers"] = 0;
        //System.Data.SqlClient.SqlDependency.Start(ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString);
    }

    void Application_BeginRequest(Object Sender, EventArgs e)
    {

        // if ((int)Application["appCtr"] < 0)
        // {
        //Server.Transfer("testExcep.aspx");
        //Response.Redirect("testException.aspx");
        // clsCommon objThrowOut = new clsCommon();
        // objThrowOut.ThrowOutFroPage();         
        //}
        // else
        //{
        //Sales/SalesChallan.aspx?typeval=15
        string fullOrigionalpath = Request.Url.ToString();

        if (fullOrigionalpath.Contains("/testpage2"))
        {
            Context.RewritePath("testpage2.aspx?pid=$1");
        }
        else if (fullOrigionalpath.Contains("/Products/DVDs.aspx"))
        {
            Context.RewritePath("/Products.aspx?Category=DVDs");
        }

        Application.Lock();
        Application["appCtr"] = (int)Application["appCtr"] + 1;
        Application.UnLock();
        // }

        //if (Request.Cookies["culture"] == null) return;
        //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(Request.Cookies["culture"].Value);
        //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        //System.Threading.Thread.CurrentThread.CurrentUICulture = ci;


        //Reference http://www.csharphelp.com/archives/archive206.html


    }


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        //string strUser= Session["UserID"].ToString();
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        //Response.Redirect("testException.aspx");
        // Response.Write("Unexpected error occured ! <br> Please Report to Admin with Error Details. <br>Error Details:"+Server.GetLastError().TargetSite.Name +": " + Server.GetLastError().Message+"<br>"+DateTime.Now.ToString());        
        //Response.End();
        // Context.ClearError();


    }

    void Session_Start(object sender, EventArgs e)
    {
        Session.Timeout = 1440;
        //rdServer.rdSession.SessionStart();

        // Code that runs when a new session is started

    }


    void Session_End(object sender, EventArgs e)
    {
        //string strUser = Session["UserID"].ToString();
        Application["appCtr"] = -1;
        Session.RemoveAll();

        Response.Redirect("~/Logout.aspx");
        //Server.Transfer("testException.aspx");

        //Server.Transfer("Login.aspx");
        //Response.Redirect("Login.aspx");
        //Response.Write(Session["SID"].ToString());

        // Code that runs when a session ends. 
        //// Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
