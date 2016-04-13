using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Blumen;
using System.Text;

public partial class Default : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    
    string strCompanyID = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/Sales/CalculatePrice.aspx");

        //if (Session["UserID"] == null)
        //{
        //    Response.Redirect(ResolveUrl("~/Login.aspx"));
        //}
        //else
        //{
        //    Response.Redirect(ResolveUrl("~/Index.aspx"));
        //}

    }
    
}
