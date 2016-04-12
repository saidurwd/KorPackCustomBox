using System;
using System.Data;
using System.Data.SqlClient;

namespace KPCustomBox
{
    #region usp_InsertCB_Customer Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_InsertCB_Customer stored procedure.
    /// </summary>
    public class InsertCBCustomer
    {
        #region Member Variables

        protected string _connectionString = String.Empty;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected System.Int32 _returnValue;
        protected string _customersCId;
        protected string _customersDesc;
        protected decimal? _amount;
        protected int? _sPAutoId;
        protected int? _pCAutoId;
        protected string _terms;

        #endregion

        #region Constructors

        public InsertCBCustomer()
        {
        }

        public InsertCBCustomer(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public InsertCBCustomer(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public InsertCBCustomer(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The connection string to use when executing the usp_InsertCB_Customer stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_InsertCB_Customer stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_InsertCB_Customer stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_InsertCB_Customer stored procedure.
        /// </summary>
        public System.Int32 ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_InsertCB_Customer stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustomersCId
        {
            get { return _customersCId; }
            set
            {
                _customersCId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustomersDesc
        {
            get { return _customersDesc; }
            set
            {
                _customersDesc = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
            }
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
        public string Terms
        {
            get { return _terms; }
            set
            {
                _terms = value;
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
        /// This method calls the usp_InsertCB_Customer stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_InsertCB_Customer]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmCustomersCId = cmd.Parameters.Add("@CustomersCId", SqlDbType.VarChar);
                prmCustomersCId.Direction = ParameterDirection.Input;
                prmCustomersCId.Size = 50;
                if (!string.IsNullOrEmpty(CustomersCId))
                    prmCustomersCId.Value = CustomersCId;
                else
                    prmCustomersCId.Value = DBNull.Value;


                SqlParameter prmCustomersDesc = cmd.Parameters.Add("@CustomersDesc", SqlDbType.NVarChar);
                prmCustomersDesc.Direction = ParameterDirection.Input;
                prmCustomersDesc.Size = 500;
                if (!string.IsNullOrEmpty(CustomersDesc))
                    prmCustomersDesc.Value = CustomersDesc;
                else
                    prmCustomersDesc.Value = DBNull.Value;


                SqlParameter prmAmount = cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                prmAmount.Direction = ParameterDirection.Input;
                prmAmount.Precision = 9;
                prmAmount.Scale = 2;
                if (Amount.HasValue)
                    prmAmount.Value = Amount.Value;
                else
                    prmAmount.Value = DBNull.Value;


                SqlParameter prmSPAutoId = cmd.Parameters.Add("@SPAutoId", SqlDbType.Int);
                prmSPAutoId.Direction = ParameterDirection.Input;
                if (SPAutoId.HasValue)
                    prmSPAutoId.Value = SPAutoId.Value;
                else
                    prmSPAutoId.Value = DBNull.Value;


                SqlParameter prmPCAutoId = cmd.Parameters.Add("@PCAutoId", SqlDbType.Int);
                prmPCAutoId.Direction = ParameterDirection.Input;
                if (PCAutoId.HasValue)
                    prmPCAutoId.Value = PCAutoId.Value;
                else
                    prmPCAutoId.Value = DBNull.Value;


                SqlParameter prmTerms = cmd.Parameters.Add("@Terms", SqlDbType.NVarChar);
                prmTerms.Direction = ParameterDirection.Input;
                prmTerms.Size = 500;
                if (!string.IsNullOrEmpty(Terms))
                    prmTerms.Value = Terms;
                else
                    prmTerms.Value = DBNull.Value;

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
        /// This method calls the usp_InsertCB_Customer stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="customersCId"></param>
        /// <param name="customersDesc"></param>
        /// <param name="amount"></param>
        /// <param name="sPAutoId"></param>
        /// <param name="pCAutoId"></param>
        /// <param name="terms"></param>
        public static void Execute(
        #region Parameters
                string connectionString,
                string customersCId,
                string customersDesc,
                decimal? amount,
                int? sPAutoId,
                int? pCAutoId,
                string terms
        #endregion
            )
        {
            InsertCBCustomer insertCBCustomer = new InsertCBCustomer();

            #region Assign Property Values
            insertCBCustomer.ConnectionString = connectionString;
            insertCBCustomer.CustomersCId = customersCId;
            insertCBCustomer.CustomersDesc = customersDesc;
            insertCBCustomer.Amount = amount;
            insertCBCustomer.SPAutoId = sPAutoId;
            insertCBCustomer.PCAutoId = pCAutoId;
            insertCBCustomer.Terms = terms;
            #endregion

            insertCBCustomer.Execute();

            #region Get Property Values

            #endregion
        }
        #endregion
    }
    #endregion
}

