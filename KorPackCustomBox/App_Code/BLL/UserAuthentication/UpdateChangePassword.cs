﻿//------------------------------------------------------------------------------
// <autogenerated>
//     Author : Faruk Ahmed
//
//     Date:    6/1/2012
//     Time:    8:43 PM
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
    #region usp_UpdateChangePassword Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_UpdateChangePassword stored procedure.
    /// </summary>
    public class UpdateChangePassword
    {
        #region Member Variables
        protected string _connectionString = String.Empty;
        protected SqlConnection _connection = null;
        protected SqlTransaction _transaction = null;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected int _returnValue = 0;
        protected SqlString _preLoginID = SqlString.Null;
        protected bool _preLoginIDSet = false;
        protected SqlString _loginID = SqlString.Null;
        protected bool _loginIDSet = false;
        protected SqlString _newPassword = SqlString.Null;
        protected bool _newPasswordSet = false;
        #endregion

        #region Constructors
        public UpdateChangePassword()
        {
        }

        public UpdateChangePassword(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public UpdateChangePassword(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public UpdateChangePassword(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The connection string to use when executing the usp_UpdateChangePassword stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_UpdateChangePassword stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_UpdateChangePassword stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_UpdateChangePassword stored procedure.
        /// </summary>
        public int ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_UpdateChangePassword stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString PreLoginID
        {
            get { return _preLoginID; }
            set
            {
                _preLoginID = value;
                _preLoginIDSet = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString LoginID
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
        public SqlString NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                _newPasswordSet = true;
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
        /// This method calls the usp_UpdateChangePassword stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_UpdateChangePassword]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmPreLoginID = cmd.Parameters.Add("@PreLoginID", SqlDbType.VarChar);
                prmPreLoginID.Direction = ParameterDirection.Input;
                prmPreLoginID.Size = 50;
                if (_preLoginIDSet == true || this.PreLoginID.IsNull == false)
                {
                    prmPreLoginID.Value = this.PreLoginID;
                }

                SqlParameter prmLoginID = cmd.Parameters.Add("@LoginID", SqlDbType.VarChar);
                prmLoginID.Direction = ParameterDirection.Input;
                prmLoginID.Size = 50;
                if (_loginIDSet == true || this.LoginID.IsNull == false)
                {
                    prmLoginID.Value = this.LoginID;
                }

                SqlParameter prmNewPassword = cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar);
                prmNewPassword.Direction = ParameterDirection.Input;
                prmNewPassword.Size = -1;
                if (_newPasswordSet == true || this.NewPassword.IsNull == false)
                {
                    prmNewPassword.Value = this.NewPassword;
                }
                #endregion

                #region Execute Command
                if (cn.State != ConnectionState.Open) cn.Open();
                _recordsAffected = cmd.ExecuteNonQuery();
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
        }

        /// <summary>
        /// This method calls the usp_UpdateChangePassword stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="preLoginID"></param>
        /// <param name="loginID"></param>
        /// <param name="newPassword"></param>
        public static void Execute(
        #region Parameters
string connectionString,
                SqlString preLoginID,
                SqlString loginID,
                SqlString newPassword
        #endregion
)
        {
            UpdateChangePassword updateChangePassword = new UpdateChangePassword();

            #region Assign Property Values
            updateChangePassword.ConnectionString = connectionString;
            updateChangePassword.PreLoginID = preLoginID;
            updateChangePassword.LoginID = loginID;
            updateChangePassword.NewPassword = newPassword;
            #endregion

            updateChangePassword.Execute();

            #region Get Property Values

            #endregion
        }
        #endregion
    }
    #endregion
}