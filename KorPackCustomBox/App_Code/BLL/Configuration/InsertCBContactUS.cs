
using System;
using System.Data;
using System.Data.SqlClient;

namespace KPCustomBox
{
    #region usp_InsertCBContactUS Wrapper
    /// <summary>
    /// This class is a wrapper for the usp_InsertCBContactUS stored procedure.
    /// </summary>
    public class InsertCBContactUS
    {
        #region Member Variables

        protected string _connectionString = String.Empty;
        protected SqlConnection _connection;
        protected SqlTransaction _transaction;
        protected bool _ownsConnection = true;
        protected int _recordsAffected = -1;
        protected System.Int32 _returnValue;
        protected string _name;
        protected string _company;
        protected string _street;
        protected string _city;
        protected string _state;
        protected string _zipCode;
        protected string _phone;
        protected string _email;
        protected string _subject;
        protected string _message;
        protected int? _cUAutoId;

        #endregion

        #region Constructors

        public InsertCBContactUS()
        {
        }

        public InsertCBContactUS(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public InsertCBContactUS(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public InsertCBContactUS(SqlConnection connection, SqlTransaction transaction)
        {
            this.Connection = connection;
            this.Transaction = transaction;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The connection string to use when executing the usp_InsertCBContactUS stored procedure.
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// The connection to use when executing the usp_InsertCBContactUS stored procedure.
        /// If this is not null, it will be used instead of creating a new connection.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// The transaction to use when executing the usp_InsertCBContactUS stored procedure.
        /// If this is not null, the stored procedure will be executing within the transaction.
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        /// <summary>
        /// Gets the return value from the usp_InsertCBContactUS stored procedure.
        /// </summary>
        public System.Int32 ReturnValue
        {
            get { return _returnValue; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the usp_InsertCBContactUS stored procedure.
        /// </summary>
        public int RecordsAffected
        {
            get { return _recordsAffected; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
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
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? CUAutoId
        {
            get { return _cUAutoId; }
            set
            {
                _cUAutoId = value;
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
        /// This method calls the usp_InsertCBContactUS stored procedure.
        /// </summary>
        public virtual void Execute()
        {
            SqlCommand cmd = new SqlCommand();

            SqlConnection cn = this.GetConnection();

            try
            {
                cmd.Connection = cn;
                cmd.Transaction = this.Transaction;
                cmd.CommandText = "[dbo].[usp_InsertCBContactUS]";
                cmd.CommandType = CommandType.StoredProcedure;

                #region Populate Parameters
                SqlParameter prmReturnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.ReturnValue;

                SqlParameter prmName = cmd.Parameters.Add("@Name", SqlDbType.VarChar);
                prmName.Direction = ParameterDirection.Input;
                prmName.Size = 100;
                if (!string.IsNullOrEmpty(Name))
                    prmName.Value = Name;
                else
                    prmName.Value = DBNull.Value;


                SqlParameter prmCompany = cmd.Parameters.Add("@Company", SqlDbType.VarChar);
                prmCompany.Direction = ParameterDirection.Input;
                prmCompany.Size = 100;
                if (!string.IsNullOrEmpty(Company))
                    prmCompany.Value = Company;
                else
                    prmCompany.Value = DBNull.Value;


                SqlParameter prmStreet = cmd.Parameters.Add("@Street", SqlDbType.VarChar);
                prmStreet.Direction = ParameterDirection.Input;
                prmStreet.Size = 50;
                if (!string.IsNullOrEmpty(Street))
                    prmStreet.Value = Street;
                else
                    prmStreet.Value = DBNull.Value;


                SqlParameter prmCity = cmd.Parameters.Add("@City", SqlDbType.VarChar);
                prmCity.Direction = ParameterDirection.Input;
                prmCity.Size = 50;
                if (!string.IsNullOrEmpty(City))
                    prmCity.Value = City;
                else
                    prmCity.Value = DBNull.Value;


                SqlParameter prmState = cmd.Parameters.Add("@State", SqlDbType.VarChar);
                prmState.Direction = ParameterDirection.Input;
                prmState.Size = 50;
                if (!string.IsNullOrEmpty(State))
                    prmState.Value = State;
                else
                    prmState.Value = DBNull.Value;


                SqlParameter prmZipCode = cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar);
                prmZipCode.Direction = ParameterDirection.Input;
                prmZipCode.Size = 50;
                if (!string.IsNullOrEmpty(ZipCode))
                    prmZipCode.Value = ZipCode;
                else
                    prmZipCode.Value = DBNull.Value;


                SqlParameter prmPhone = cmd.Parameters.Add("@Phone", SqlDbType.VarChar);
                prmPhone.Direction = ParameterDirection.Input;
                prmPhone.Size = 50;
                if (!string.IsNullOrEmpty(Phone))
                    prmPhone.Value = Phone;
                else
                    prmPhone.Value = DBNull.Value;


                SqlParameter prmEmail = cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                prmEmail.Direction = ParameterDirection.Input;
                prmEmail.Size = 50;
                if (!string.IsNullOrEmpty(Email))
                    prmEmail.Value = Email;
                else
                    prmEmail.Value = DBNull.Value;


                SqlParameter prmSubject = cmd.Parameters.Add("@Subject", SqlDbType.VarChar);
                prmSubject.Direction = ParameterDirection.Input;
                prmSubject.Size = 50;
                if (!string.IsNullOrEmpty(Subject))
                    prmSubject.Value = Subject;
                else
                    prmSubject.Value = DBNull.Value;


                SqlParameter prmMessage = cmd.Parameters.Add("@Message", SqlDbType.NVarChar);
                prmMessage.Direction = ParameterDirection.Input;
                prmMessage.Size = 500;
                if (!string.IsNullOrEmpty(Message))
                    prmMessage.Value = Message;
                else
                    prmMessage.Value = DBNull.Value;


                SqlParameter prmCUAutoId = cmd.Parameters.Add("@CUAutoId", SqlDbType.Int);
                if (CUAutoId.HasValue)
                {
                    prmCUAutoId.Direction = ParameterDirection.InputOutput;
                }
                else
                {
                    prmCUAutoId.Direction = ParameterDirection.Output;
                }
                if (CUAutoId.HasValue)
                    prmCUAutoId.Value = CUAutoId.Value;
                else
                    prmCUAutoId.Value = DBNull.Value;

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

                if (prmCUAutoId != null && prmCUAutoId.Value != null)
                {
                    if (prmCUAutoId.Value is int?)
                    {
                        this.CUAutoId = (int?)prmCUAutoId.Value;
                    }
                    else
                    {
                        if (prmCUAutoId.Value != DBNull.Value)
                        {
                            this.CUAutoId = new int?((int)prmCUAutoId.Value);
                        }
                        else
                        {
                            this.CUAutoId = null;
                        }
                    }
                }
                else
                {
                    this.CUAutoId = null;
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
        /// This method calls the usp_InsertCBContactUS stored procedure.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="name"></param>
        /// <param name="company"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zipCode"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="cUAutoId"></param>
        public static void Execute(
        #region Parameters
                string connectionString,
                string name,
                string company,
                string street,
                string city,
                string state,
                string zipCode,
                string phone,
                string email,
                string subject,
                string message,
                ref int? cUAutoId
        #endregion
            )
        {
            InsertCBContactUS insertCBContactUS = new InsertCBContactUS();

            #region Assign Property Values
            insertCBContactUS.ConnectionString = connectionString;
            insertCBContactUS.Name = name;
            insertCBContactUS.Company = company;
            insertCBContactUS.Street = street;
            insertCBContactUS.City = city;
            insertCBContactUS.State = state;
            insertCBContactUS.ZipCode = zipCode;
            insertCBContactUS.Phone = phone;
            insertCBContactUS.Email = email;
            insertCBContactUS.Subject = subject;
            insertCBContactUS.Message = message;
            insertCBContactUS.CUAutoId = cUAutoId;
            #endregion

            insertCBContactUS.Execute();

            #region Get Property Values
            cUAutoId = insertCBContactUS.CUAutoId;
            #endregion
        }
        #endregion
    }
    #endregion
}

