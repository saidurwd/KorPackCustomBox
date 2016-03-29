<%@ WebHandler Language="C#" Class="CompanyLogo" %>

using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

public class CompanyLogo : IHttpHandler
{
    /// <summary>
    /// Retriving picture from Database to show in Image Control
    /// </summary>
    /// <param name="ID"> ID to show picture and use in Query</param>
    public void ProcessRequest(HttpContext context)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;

        // Create SQL Command 

        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Select COMP_LOGO from dbo.TB_Company where COMPANY_ID = @ID";
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = con;

        SqlParameter ImageID = new SqlParameter("@ID", System.Data.SqlDbType.Int);
        ImageID.Value = context.Request.QueryString["ID"];
        cmd.Parameters.Add(ImageID);
        con.Open();
        SqlDataReader dReader = cmd.ExecuteReader();
        dReader.Read();
        if (dReader["COMP_LOGO"].ToString() != "")
        {
            context.Response.BinaryWrite((byte[])dReader["COMP_LOGO"]);
        }
        
        dReader.Close();
        con.Close();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}