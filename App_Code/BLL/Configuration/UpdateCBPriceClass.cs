using System;
using System.Data;
using System.Data.SqlClient;

namespace KPCustomBox
{
    #region usp_UpdateCBPriceClass Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_UpdateCBPriceClass stored procedure.
    /// </summary>
    public class UpdateCBPriceClass
    {
        #region Member Variables

        protected string _connectionString = String.Empty;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected System.Int32 _returnValue;
        protected int? _pCAutoId;
        protected string _priceClassDesc;

        #endregion

        #region Constructors

        public UpdateCBPriceClass()
        {
        }

        public UpdateCBPriceClass(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public UpdateCBPriceClass(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public UpdateCBPriceClass(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The connection string to use when executing the usp_UpdateCBPriceClass stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_UpdateCBPriceClass stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_UpdateCBPriceClass stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_UpdateCBPriceClass stored procedure.
        /// </summary>
        public System.Int32 ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_UpdateCBPriceClass stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? PCAutoId
        {
            get { return _pCAutoId; }
            set
            {
                _pCAutoId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PriceClassDesc
        {
            get { return _priceClassDesc; }
            set
            {
                _priceClassDesc = value;
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
        /// This method calls the usp_UpdateCBPriceClass stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_UpdateCBPriceClass]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmPCAutoId = cmd.Parameters.Add("@PCAutoId", SqlDbType.Int);
                prmPCAutoId.Direction = ParameterDirection.Input;
                if (PCAutoId.HasValue)
                    prmPCAutoId.Value = PCAutoId.Value;
                else
                    prmPCAutoId.Value = DBNull.Value;


                SqlParameter prmPriceClassDesc = cmd.Parameters.Add("@PriceClassDesc", SqlDbType.VarChar);
                prmPriceClassDesc.Direction = ParameterDirection.Input;
                prmPriceClassDesc.Size = 250;
                if (!string.IsNullOrEmpty(PriceClassDesc))
                    prmPriceClassDesc.Value = PriceClassDesc;
                else
                    prmPriceClassDesc.Value = DBNull.Value;

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
        /// This method calls the usp_UpdateCBPriceClass stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="pCAutoId"></param>
        /// <param name="priceClassDesc"></param>
        public static void Execute(
        #region Parameters
                string connectionString,
                int? pCAutoId,
                string priceClassDesc
        #endregion
            )
        {
            UpdateCBPriceClass updateCBPriceClass = new UpdateCBPriceClass();

            #region Assign Property Values
            updateCBPriceClass.ConnectionString = connectionString;
            updateCBPriceClass.PCAutoId = pCAutoId;
            updateCBPriceClass.PriceClassDesc = priceClassDesc;
            #endregion

            updateCBPriceClass.Execute();

            #region Get Property Values

            #endregion
        }
        #endregion
    }
    #endregion
}

