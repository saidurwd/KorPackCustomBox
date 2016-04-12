
using System;
using System.Data;
using System.Data.SqlClient;

namespace KPCustomBox
{
    #region usp_InsertUserInfoSignUp Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_InsertUserInfoSignUp stored procedure.
    /// </summary>
    public class InsertUserInfoSignUp
    {
        #region Member Variables

        protected string _connectionString = String.Empty;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected System.Int32 _returnValue;
        protected string _userName;
        protected string _email;
        protected string _password;
        protected string _companyName;
        protected byte? _isSignUpRequest;

        #endregion

        #region Constructors

        public InsertUserInfoSignUp()
        {
        }

        public InsertUserInfoSignUp(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public InsertUserInfoSignUp(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public InsertUserInfoSignUp(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The connection string to use when executing the usp_InsertUserInfoSignUp stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_InsertUserInfoSignUp stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_InsertUserInfoSignUp stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_InsertUserInfoSignUp stored procedure.
        /// </summary>
        public System.Int32 ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_InsertUserInfoSignUp stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte? IsSignUpRequest
        {
            get { return _isSignUpRequest; }
            set
            {
                _isSignUpRequest = value;
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
        /// This method calls the usp_InsertUserInfoSignUp stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_InsertUserInfoSignUp]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmUserName = cmd.Parameters.Add("@UserName", SqlDbType.VarChar);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Size = 150;
                if (!string.IsNullOrEmpty(UserName))
                    prmUserName.Value = UserName;
                else
                    prmUserName.Value = DBNull.Value;


                SqlParameter prmEmail = cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                prmEmail.Direction = ParameterDirection.Input;
                prmEmail.Size = 150;
                if (!string.IsNullOrEmpty(Email))
                    prmEmail.Value = Email;
                else
                    prmEmail.Value = DBNull.Value;


                SqlParameter prmPassword = cmd.Parameters.Add("@Password", SqlDbType.VarChar);
                prmPassword.Direction = ParameterDirection.Input;
                prmPassword.Size = 50;
                if (!string.IsNullOrEmpty(Password))
                    prmPassword.Value = Password;
                else
                    prmPassword.Value = DBNull.Value;


                SqlParameter prmCompanyName = cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar);
                prmCompanyName.Direction = ParameterDirection.Input;
                prmCompanyName.Size = 100;
                if (!string.IsNullOrEmpty(CompanyName))
                    prmCompanyName.Value = CompanyName;
                else
                    prmCompanyName.Value = DBNull.Value;


                SqlParameter prmIsSignUpRequest = cmd.Parameters.Add("@IsSignUpRequest", SqlDbType.TinyInt);
                prmIsSignUpRequest.Direction = ParameterDirection.Input;
                if (IsSignUpRequest.HasValue)
                    prmIsSignUpRequest.Value = IsSignUpRequest.Value;
                else
                    prmIsSignUpRequest.Value = DBNull.Value;

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
        /// This method calls the usp_InsertUserInfoSignUp stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="companyName"></param>
        /// <param name="isSignUpRequest"></param>
        public static void Execute(
        #region Parameters
                string connectionString,
                string userName,
                string email,
                string password,
                string companyName,
                byte? isSignUpRequest
        #endregion
            )
        {
            InsertUserInfoSignUp insertUserInfoSignUp = new InsertUserInfoSignUp();

            #region Assign Property Values
            insertUserInfoSignUp.ConnectionString = connectionString;
            insertUserInfoSignUp.UserName = userName;
            insertUserInfoSignUp.Email = email;
            insertUserInfoSignUp.Password = password;
            insertUserInfoSignUp.CompanyName = companyName;
            insertUserInfoSignUp.IsSignUpRequest = isSignUpRequest;
            #endregion

            insertUserInfoSignUp.Execute();

            #region Get Property Values

            #endregion
        }
        #endregion
    }
    #endregion
}
