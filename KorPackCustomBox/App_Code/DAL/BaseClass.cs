//******************************************
// Author : Faruk Ahmed
// Development Date : 30th of May 2012
// Module : User Authentication
//************

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DAL
{
	public class BaseClass : IDisposable
	{
		#region Field
		private SqlDataAdapter _sqldaAdapter;
		#endregion

		#region Constructor
		public BaseClass(string strConnectionString)
		{
			_sqldaAdapter = new SqlDataAdapter();
			_sqldaAdapter.SelectCommand = new SqlCommand();
			_sqldaAdapter.SelectCommand.Connection = new SqlConnection(strConnectionString);
            if (_sqldaAdapter.SelectCommand.Connection.State.Equals(ConnectionState.Closed))
            {
                
                    _sqldaAdapter.SelectCommand.Connection.Open();
            }
		} // end constructor
		#endregion

		#region Methods
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(true); // as a service to those who might inherit from us
		} // end Dispose

        #region Dispose Method
        protected virtual void Dispose(bool disposing)
		{
			if (! disposing)
				return; // we're being collected, so let the GC take care of this object

			if (_sqldaAdapter != null )
			{
				if (_sqldaAdapter.SelectCommand != null)
				{
					if(_sqldaAdapter.SelectCommand.Connection != null)
					{
						_sqldaAdapter.SelectCommand.Connection.Dispose();
					} // end nested-nested if
					
					_sqldaAdapter.SelectCommand.Dispose();
				} // end nested if
				
				_sqldaAdapter.Dispose();
				_sqldaAdapter = null;
			} // end if
		} // end Dispose
        #endregion

        #region Initialize
        private void Initialize()
		{
			_sqldaAdapter.SelectCommand.Parameters.Clear();
		} // end Initialize

        #endregion

        #region Execute Stored Procedure DataTable
        protected DataTable ExecuteStoredProcedureDataTable(string strProcedureName, ArrayList arlParams)
		{
			if (_sqldaAdapter == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			} // end if

			Initialize();

			SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
			dbCommand.CommandText = strProcedureName;
			dbCommand.CommandType = CommandType.StoredProcedure;

			if (arlParams != null)
			{
				for (int i = 0; i < arlParams.Count; i++)
				{
					dbCommand.Parameters.Add(arlParams[i]);
				} // end for
			} // end if

			
			DataTable dtResult = new DataTable();
			_sqldaAdapter.Fill(dtResult);
			return dtResult;
		} // end ExecuteStoredProcedureDataTable
        #endregion

        #region Execute Non Query Stored Procedure
        protected int ExecuteNonQueryStoredProcedure(string strProcedureName, ArrayList arlParams)
		{
			try
			{
				if (_sqldaAdapter == null)
				{
					throw new System.ObjectDisposedException( GetType().FullName);
				} // end if

				Initialize();

				int intResult = 0;

				SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
				dbCommand.CommandText = strProcedureName;
				dbCommand.CommandType = CommandType.StoredProcedure;

				if (arlParams != null)
				{
					for (int i = 0; i < arlParams.Count; i++)
					{
						dbCommand.Parameters.Add(arlParams[i]);
					} // end for
				} // end if
			
				intResult = dbCommand.ExecuteNonQuery();
			
				return intResult;
			}
			catch(SqlException ex)
			{
				return ex.Number;
			}
		} // end ExecuteNonQueryStoredProcedure
        #endregion

        #region Execute Stored Procedure Scaler
        protected object ExecuteStoredProcedureScalar(string strProcedureName, ArrayList arlParams)
		{
			if (_sqldaAdapter == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			} // end if

			Initialize();

			object objResult = null;

			SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
			dbCommand.CommandText = strProcedureName;
			dbCommand.CommandType = CommandType.StoredProcedure;

			if (arlParams != null)
			{
				for (int i = 0; i < arlParams.Count; i++)
				{
					dbCommand.Parameters.Add(arlParams[i]);
				} // end for
			} // end if

			objResult = dbCommand.ExecuteScalar();


			return objResult;
		} // end ExecuteStoredProcedureScalar
        #endregion

        #region Execute SQL String Scalar
        protected object ExecuteSQLStringScalar(string strSQL)
		{
			if (_sqldaAdapter == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			} // end if

			Initialize();

			object objResult = null;

			SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
			dbCommand.CommandText = strSQL;
			dbCommand.CommandType = CommandType.Text;

			objResult = dbCommand.ExecuteScalar();

			return objResult;
		} // end ExecuteSQLStringScalar
        #endregion

        #region ExecuteSQLStringDataTable
        protected DataTable ExecuteSQLStringDataTable(string strSQL)
		{
			if (_sqldaAdapter == null)
			{
				throw new System.ObjectDisposedException( GetType().FullName);
			} // end if

			Initialize();

			SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
			dbCommand.CommandText = strSQL;
			dbCommand.CommandType = CommandType.Text;
					
			DataTable dtResult = new DataTable();
			_sqldaAdapter.Fill(dtResult);

			return dtResult;
		} // end ExecuteSQLStringDataTable
        #endregion
        
        #region ExecuteSQLStringDataReader
        protected SqlDataReader ExecuteSQLStringDataReader(string strSQL)
        {
            if (_sqldaAdapter == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            } // end if

            Initialize();

            SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
            dbCommand.CommandText = strSQL;
            dbCommand.CommandType = CommandType.Text;

            SqlDataReader drResult;
            drResult = dbCommand.ExecuteReader();
            

            return drResult;
        } // end ExecuteSQLStringDataTable
        #endregion

        //==========

        #region ExecuteSQLStringDataSet
        protected DataSet ExecuteSQLStringDataSet(string strSQL)
        {
            if (_sqldaAdapter == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            } // end if

            Initialize();

            SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
            dbCommand.CommandText = strSQL;
            dbCommand.CommandType = CommandType.Text;

            DataSet dsResult = new DataSet();
            _sqldaAdapter.Fill(dsResult);

            return dsResult;
        } // end ExecuteSQLStringDataTable
        #endregion

        //==============
        #region ExecuteSPDataSet
        protected DataSet ExecuteSPDataSet(string strProcedureName, ArrayList arlParams)
        {
            if (_sqldaAdapter == null)
            {
                throw new System.ObjectDisposedException(GetType().FullName);
            } // end if
            Initialize();

            

            SqlCommand dbCommand = _sqldaAdapter.SelectCommand;
            dbCommand.CommandText = strProcedureName;
            dbCommand.CommandType = CommandType.StoredProcedure;

            if (arlParams != null)
            {
                for (int i = 0; i < arlParams.Count; i++)
                {
                    dbCommand.Parameters.Add(arlParams[i]);
                } // end for
            } // end if


            DataSet dsResult = new DataSet();
            _sqldaAdapter.Fill(dsResult);

            return dsResult;
        } // end ExecuteSPDataSet
        #endregion
        #endregion
    } // end class BaseClass
}