﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by CodeSmith.
//
//     Date:    8/26/2012
//     Time:    3:17 PM
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
    #region usp_InsertTB_PrmUserRole Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_InsertTB_PrmUserRole stored procedure.
    /// </summary>
    public class InsertTBPrmUserRole
    {
        #region Member Variables
        protected string _connectionString = String.Empty;
        protected SqlConnection _connection = null;
        protected SqlTransaction _transaction = null;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected int _returnValue = 0;
        protected SqlString _roleDesc = SqlString.Null;
        protected bool _roleDescSet = false;
        protected SqlInt32 _createdBy = SqlInt32.Null;
        protected bool _createdBySet = false;
        protected SqlString _cOMPANY_ID = SqlString.Null;
        protected bool _cOMPANY_IDSet = false;
        protected SqlInt32 _roleID = SqlInt32.Null;
        protected bool _roleIDSet = false;
        #endregion

        #region Constructors
        public InsertTBPrmUserRole()
        {
        }

        public InsertTBPrmUserRole(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public InsertTBPrmUserRole(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public InsertTBPrmUserRole(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The connection string to use when executing the usp_InsertTB_PrmUserRole stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_InsertTB_PrmUserRole stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_InsertTB_PrmUserRole stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_InsertTB_PrmUserRole stored procedure.
        /// </summary>
        public int ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_InsertTB_PrmUserRole stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString RoleDesc
        {
            get { return _roleDesc; }
            set
            {
                _roleDesc = value;
                _roleDescSet = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlInt32 CreatedBy
        {
            get { return _createdBy; }
            set
            {
                _createdBy = value;
                _createdBySet = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlString COMPANY_ID
        {
            get { return _cOMPANY_ID; }
            set
            {
                _cOMPANY_ID = value;
                _cOMPANY_IDSet = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlInt32 RoleID
        {
            get { return _roleID; }
            set
            {
                _roleID = value;
                _roleIDSet = true;
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
        /// This method calls the usp_InsertTB_PrmUserRole stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_InsertTB_PrmUserRole]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmRoleDesc = cmd.Parameters.Add("@RoleDesc", SqlDbType.VarChar);
                prmRoleDesc.Direction = ParameterDirection.Input;
                prmRoleDesc.Size = 100;
                if (_roleDescSet == true || this.RoleDesc.IsNull == false)
                {
                    prmRoleDesc.Value = this.RoleDesc;
                }

                SqlParameter prmCreatedBy = cmd.Parameters.Add("@CreatedBy", SqlDbType.Int);
                prmCreatedBy.Direction = ParameterDirection.Input;
                if (_createdBySet == true || this.CreatedBy.IsNull == false)
                {
                    prmCreatedBy.Value = this.CreatedBy;
                }

                SqlParameter prmCOMPANY_ID = cmd.Parameters.Add("@COMPANY_ID", SqlDbType.Char);
                prmCOMPANY_ID.Direction = ParameterDirection.Input;
                prmCOMPANY_ID.Size = 4;
                if (_cOMPANY_IDSet == true || this.COMPANY_ID.IsNull == false)
                {
                    prmCOMPANY_ID.Value = this.COMPANY_ID;
                }

                SqlParameter prmRoleID = cmd.Parameters.Add("@RoleID", SqlDbType.Int);
                if (_roleIDSet == true)
                {
                    prmRoleID.Direction = ParameterDirection.InputOutput;
                }
                else
                {
                    prmRoleID.Direction = ParameterDirection.Output;
                }
                if (_roleIDSet == true || this.RoleID.IsNull == false)
                {
                    prmRoleID.Value = this.RoleID;
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

                if (prmRoleID != null && prmRoleID.Value != null)
                {
                    if (prmRoleID.Value is SqlInt32)
                    {
                        this.RoleID = (SqlInt32)prmRoleID.Value;
                    }
                    else
                    {
                        if (prmRoleID.Value != DBNull.Value)
                        {
                            this.RoleID = new SqlInt32((int)prmRoleID.Value);
                        }
                        else
                        {
                            this.RoleID = SqlInt32.Null;
                        }
                    }
                }
                else
                {
                    this.RoleID = SqlInt32.Null;
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
        /// This method calls the usp_InsertTB_PrmUserRole stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="roleDesc"></param>
        /// <param name="createdBy"></param>
        /// <param name="cOMPANY_ID"></param>
        /// <param name="roleID"></param>
        public static void Execute(
        #region Parameters
string connectionString,
                SqlString roleDesc,
                SqlInt32 createdBy,
                SqlString cOMPANY_ID,
                ref SqlInt32 roleID
        #endregion
)
        {
            InsertTBPrmUserRole insertTBPrmUserRole = new InsertTBPrmUserRole();

            #region Assign Property Values
            insertTBPrmUserRole.ConnectionString = connectionString;
            insertTBPrmUserRole.RoleDesc = roleDesc;
            insertTBPrmUserRole.CreatedBy = createdBy;
            insertTBPrmUserRole.COMPANY_ID = cOMPANY_ID;
            insertTBPrmUserRole.RoleID = roleID;
            #endregion

            insertTBPrmUserRole.Execute();

            #region Get Property Values
            roleID = insertTBPrmUserRole.RoleID;
            #endregion
        }
        #endregion
    }
    #endregion
}
