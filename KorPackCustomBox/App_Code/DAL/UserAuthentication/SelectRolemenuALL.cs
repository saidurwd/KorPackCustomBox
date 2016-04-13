﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by CodeSmith.
//
//     Date:    8/23/2012
//     Time:    8:53 PM
//     Version: 5.0.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace Blumen
{
    #region usp_SelectRole_menuALL Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_SelectRole_menuALL stored procedure.
    /// </summary>
    public class SelectRolemenuALL
    {
        #region Member Variables
        protected string _connectionString = String.Empty;
        protected SqlConnection _connection = null;
        protected SqlTransaction _transaction = null;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected int _returnValue = 0;
        protected SqlString _loginID = SqlString.Null;
        protected bool _loginIDSet = false;
        protected SqlString _companyID = SqlString.Null;
        protected bool _companyIDSet = false;
        #endregion

        #region Constructors
        public SelectRolemenuALL()
        {
        }

        public SelectRolemenuALL(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public SelectRolemenuALL(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public SelectRolemenuALL(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The connection string to use when executing the usp_SelectRole_menuALL stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_SelectRole_menuALL stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_SelectRole_menuALL stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_SelectRole_menuALL stored procedure.
        /// </summary>
        public int ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_SelectRole_menuALL stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString loginID
        {
            get { return _loginID; }
            set
            {
                _loginID = value;
                _loginIDSet = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString CompanyID
        {
            get { return _companyID; }
            set
            {
                _companyID = value;
                _companyIDSet = true;
            }
        }
        #endregion

        #region Helper Methods
        private SqlConnection GetConnection()
        {
            if (this.Connection != null)
            {
                _ownsConnection = false;
                return this.Connection;
            }
            else
            {
                System.Diagnostics.Debug.Assert(this.ConnectionString.Length != 0, "You must first set the ConnectioString property before calling an Execute method.");
                return new SqlConnection(this.ConnectionString);
            }
        }
        #endregion

        #region Execute Methods
        /// <summary>
        /// This method calls the usp_SelectRole_menuALL stored procedure and returns a SqlDataReader with the results.
        /// </summary>
        /// <returns>SqlDataReader</returns>
        public virtual SqlDataReader Execute()
        {
            SqlDataReader reader = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_SelectRole_menuALL]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmloginID = cmd.Parameters.Add("@loginID", SqlDbType.VarChar);
                prmloginID.Direction = ParameterDirection.Input;
                prmloginID.Size = 20;
                if (_loginIDSet == true || this.loginID.IsNull == false)
                {
                    prmloginID.Value = this.loginID;
                }

                SqlParameter prmCompanyID = cmd.Parameters.Add("@CompanyID", SqlDbType.VarChar);
                prmCompanyID.Direction = ParameterDirection.Input;
                prmCompanyID.Size = 10;
                if (_companyIDSet == true || this.CompanyID.IsNull == false)
                {
                    prmCompanyID.Value = this.CompanyID;
                }
                #endregion

                #region Execute Command
                if (cn.State != ConnectionState.Open) cn.Open();
                if (_ownsConnection)
                {
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    reader = cmd.ExecuteReader();
                }
                #endregion

                #region Get Output Parameters
                if (prmReturnValue.Value != null && prmReturnValue.Value != DBNull.Value)
                {
                    _returnValue = (int)prmReturnValue.Value;
                }

                #endregion
            }
            finally
            {
                cmd.Dispose();
            }

            return reader;
        }

        /// <summary>
        /// This method calls the usp_SelectRole_menuALL stored procedure and returns a DataSet with the results.
        /// </summary>
        /// <returns>DataSet</returns>
        public virtual DataSet ExecuteDataSet()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_SelectRole_menuALL]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmloginID = cmd.Parameters.Add("@loginID", SqlDbType.VarChar);
                prmloginID.Direction = ParameterDirection.Input;
                prmloginID.Size = 20;
                if (_loginIDSet == true || this.loginID.IsNull == false)
                {
                    prmloginID.Value = this.loginID;
                }

                SqlParameter prmCompanyID = cmd.Parameters.Add("@CompanyID", SqlDbType.VarChar);
                prmCompanyID.Direction = ParameterDirection.Input;
                prmCompanyID.Size = 10;
                if (_companyIDSet == true || this.CompanyID.IsNull == false)
                {
                    prmCompanyID.Value = this.CompanyID;
                }
                #endregion

                #region Execute Command
                if (cn.State != ConnectionState.Open) cn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                _recordsAffected = ds.Tables[0].Rows.Count;
                #endregion

                #region Get Output Parameters
                if (prmReturnValue.Value != null && prmReturnValue.Value != DBNull.Value)
                {
                    _returnValue = (int)prmReturnValue.Value;
                }

                #endregion
            }
            finally
            {
                if (_ownsConnection)
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    cn.Dispose();
                }
                cmd.Dispose();
            }

            return ds;
        }



        /// <summary>
        /// This method calls the usp_SelectRole_menuALL stored procedure and returns a DataSet with the results.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="loginID"></param>
        /// <param name="companyID"></param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSet(
        #region Parameters
string connectionString,
                SqlString loginID,
                SqlString companyID
        #endregion
)
        {
            SelectRolemenuALL selectRolemenuALL = new SelectRolemenuALL();

            #region Assign Property Values
            selectRolemenuALL.ConnectionString = connectionString;
            selectRolemenuALL.loginID = loginID;
            selectRolemenuALL.CompanyID = companyID;
            #endregion

            DataSet ds = selectRolemenuALL.ExecuteDataSet();

            #region Get Property Values

            #endregion

            return ds;
        }

        #endregion
    }
    #endregion
}