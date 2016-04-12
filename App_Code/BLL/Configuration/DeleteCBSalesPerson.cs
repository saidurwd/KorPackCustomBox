using System;
using System.Data;
using System.Data.SqlClient;

namespace KPCustomBox
{
    #region usp_DeleteCBSalesPerson Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_DeleteCBSalesPerson stored procedure.
    /// </summary>
    public class DeleteCBSalesPerson
    {
        #region Member Variables

        protected string _connectionString = String.Empty;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected System.Int32 _returnValue;
        protected int? _sPAutoId;

        #endregion

        #region Constructors

        public DeleteCBSalesPerson()
        {
        }

        public DeleteCBSalesPerson(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public DeleteCBSalesPerson(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public DeleteCBSalesPerson(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The connection string to use when executing the usp_DeleteCBSalesPerson stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_DeleteCBSalesPerson stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_DeleteCBSalesPerson stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_DeleteCBSalesPerson stored procedure.
        /// </summary>
        public System.Int32 ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_DeleteCBSalesPerson stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? SPAutoId
        {
            get { return _sPAutoId; }
            set
            {
                _sPAutoId = value;
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

            System.Diagnostics.Debug.Assert(this.ConnectionString.Length != 0, "You must first set the ConnectioString property before calling an Execute method.");
            return new SqlConnection(this.ConnectionString);
        }

        #endregion

        #region Execute Methods

        /// <summary>
        /// This method calls the usp_DeleteCBSalesPerson stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_DeleteCBSalesPerson]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmSPAutoId = cmd.Parameters.Add("@SPAutoId", SqlDbType.Int);
                prmSPAutoId.Direction = ParameterDirection.Input;
                if (SPAutoId.HasValue)
                    prmSPAutoId.Value = SPAutoId.Value;
                else
                    prmSPAutoId.Value = DBNull.Value;

                #endregion

                #region Execute Command
                if (cn.State != ConnectionState.Open) cn.Open();
                _recordsAffected = cmd.ExecuteNonQuery();
                #endregion

                #region Get Output Parameters
                if (prmReturnValue.Value != null && prmReturnValue.Value != DBNull.Value)
                {
                    _returnValue = (System.Int32)prmReturnValue.Value;
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
        /// This method calls the usp_DeleteCBSalesPerson stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="sPAutoId"></param>
        public static void Execute(
        #region Parameters
                string connectionString,
                int? sPAutoId
        #endregion
            )
        {
            DeleteCBSalesPerson deleteCBSalesPerson = new DeleteCBSalesPerson();

            #region Assign Property Values
            deleteCBSalesPerson.ConnectionString = connectionString;
            deleteCBSalesPerson.SPAutoId = sPAutoId;
            #endregion

            deleteCBSalesPerson.Execute();

            #region Get Property Values

            #endregion
        }
        #endregion
    }
    #endregion
}

