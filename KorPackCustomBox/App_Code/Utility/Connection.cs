using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Blumensoft.Utility
{
    /// <summary>
    /// Summary description for Connection
    /// </summary>
    public class Connection
    {
        public SqlConnection cnn = new SqlConnection();
        public Connection()
        {
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
            if (cnn.State.Equals(ConnectionState.Closed))
            {
                cnn.Open();
            }
        }


        //public void CloseConnection()
        //{
        //    if (cnn.State != ConnectionState.Closed)
        //        cnn.Close();
        //}
    }
}