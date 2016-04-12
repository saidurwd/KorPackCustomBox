
using System;
using System.Data;
using System.Data.SqlClient;

namespace KPCustomBox
{
    #region usp_InsertCBPriceClass Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_InsertCBPriceClass stored procedure.
    /// </summary>
    public class InsertCBPriceClass
    {
        #region Member Variables

        protected string _connectionString = String.Empty;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected System.Int32 _returnValue;
        protected string _priceClassDesc;
        protected int? _pCAutoId;

        #endregion

        #region Constructors

        public InsertCBPriceClass()
        {
        }

        public InsertCBPriceClass(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public InsertCBPriceClass(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public InsertCBPriceClass(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The connection string to use when executing the usp_InsertCBPriceClass stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_InsertCBPriceClass stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_InsertCBPriceClass stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_InsertCBPriceClass stored procedure.
        /// </summary>
        public System.Int32 ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_InsertCBPriceClass stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
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
        /// This method calls the usp_InsertCBPriceClass stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_InsertCBPriceClass]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmPriceClassDesc = cmd.Parameters.Add("@PriceClassDesc", SqlDbType.VarChar);
                prmPriceClassDesc.Direction = ParameterDirection.Input;
                prmPriceClassDesc.Size = 250;
                if (!string.IsNullOrEmpty(PriceClassDesc))
                    prmPriceClassDesc.Value = PriceClassDesc;
                else
                    prmPriceClassDesc.Value = DBNull.Value;


                SqlParameter prmPCAutoId = cmd.Parameters.Add("@PCAutoId", SqlDbType.Int);
                if (PCAutoId.HasValue)
                {
                    prmPCAutoId.Direction = ParameterDirection.InputOutput;
                }
                else
                {
                    prmPCAutoId.Direction = ParameterDirection.Output;
                }
                if (PCAutoId.HasValue)
                    prmPCAutoId.Value = PCAutoId.Value;
                else
                    prmPCAutoId.Value = DBNull.Value;

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

                if (prmPCAutoId != null && prmPCAutoId.Value != null)
                {
                    if (prmPCAutoId.Value is int?)
                    {
                        this.PCAutoId = (int?)prmPCAutoId.Value;
                    }
                    else
                    {
                        if (prmPCAutoId.Value != DBNull.Value)
                        {
                            this.PCAutoId = new int?((int)prmPCAutoId.Value);
                        }
                        else
                        {
                            this.PCAutoId = null;
                        }
                    }
                }
                else
                {
                    this.PCAutoId = null;
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
        /// This method calls the usp_InsertCBPriceClass stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="priceClassDesc"></param>
        /// <param name="pCAutoId"></param>
        public static void Execute(
        #region Parameters
                string connectionString,
                string priceClassDesc,
                ref int? pCAutoId
        #endregion
            )
        {
            InsertCBPriceClass insertCBPriceClass = new InsertCBPriceClass();

            #region Assign Property Values
            insertCBPriceClass.ConnectionString = connectionString;
            insertCBPriceClass.PriceClassDesc = priceClassDesc;
            insertCBPriceClass.PCAutoId = pCAutoId;
            #endregion

            insertCBPriceClass.Execute();

            #region Get Property Values
            pCAutoId = insertCBPriceClass.PCAutoId;
            #endregion
        }
        #endregion
    }
    #endregion
}

