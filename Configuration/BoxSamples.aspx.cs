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
using Blumen;

public partial class Configuration_AboutBlumenSoft : System.Web.UI.Page
{
    string cnnString = ConfigurationManager.ConnectionStrings["Cnn"].ConnectionString;
    string strCompanyID = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}