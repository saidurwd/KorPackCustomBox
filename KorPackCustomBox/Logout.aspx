<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
protected void Page_Load(object sender, EventArgs e)
{
    
    Session.Clear();
    Cache.Remove("CacheVarietyAdd");    
    if (Session["LogAutoID"] != null)
    {
        string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(cnnString);
        Blumen.UpdateBlumenSoftUserLoginLog objDML = new Blumen.UpdateBlumenSoftUserLoginLog(connection);

        try
        {
            objDML.LogAutoID = Int64.Parse(Session["LogAutoID"].ToString());
            objDML.LogOutDate = System.DateTime.Now;
            objDML.Connection.Open();
            objDML.Transaction = objDML.Connection.BeginTransaction("myTran");
            objDML.Execute();
            objDML.Transaction.Commit();

        }
        catch (System.Data.SqlClient.SqlException exp)
        {
            objDML.Transaction.Rollback();
            Response.Write(exp.Message);
        }
        finally
        {
            if (objDML.Connection.State == System.Data.ConnectionState.Open)
            {
                objDML.Connection.Close();
                objDML.Connection.Dispose();
            }
        }
    }
    //Response.Cookies.Clear();
    Session.Remove("dashBoardMenuTable");
    Session.Clear();
    Response.Redirect(ResolveUrl("~/Login.aspx"));
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
