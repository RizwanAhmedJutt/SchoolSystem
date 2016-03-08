#region File Header
/********************************************** 
 File Name      - SQLDatabase.cs
 Class Name     - SQLDatabase
 Project Name   - HM.Data
 Purpose        - Class for the database function inheriting the
 *                  IDatabase interface
**********************************************/
#endregion

#region namespace imports
using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SMSDAL;

#endregion

namespace SMSDAL
{
    /// <summary>
    /// Database function class
    /// </summary>
    public class SqlDatabase : IDatabase
    {
        /// <summary>
        /// the global database object
        /// </summary>
        private Microsoft.Practices.EnterpriseLibrary.Data.Database gObjDatabase;

        /// <summary>
        /// initialise the gObjDatabase object
        /// </summary>
        public SqlDatabase()
        {
            //gObjDatabase = DatabaseFactory.CreateDatabase("DefaultConnection");

            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            gObjDatabase = factory.Create("DefaultConnection");

        }

        #region GetConnection()
        /// <summary>
        /// Function to get a database connection
        /// </summary>
        /// <returns>IDBConnection object</returns>
        public IDbConnection GetConnection()
        {
            IDbConnection objConnection = gObjDatabase.CreateConnection();
            return objConnection;
        }
        #endregion

        /// <summary>
        /// Function to get a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">SQL string</param>
        /// <returns>Dbcommand object</returns>
        public DbCommand GetSqlStringCommand(String sqlString)
        {
            return gObjDatabase.GetSqlStringCommand(sqlString);
        }

        #region Function for Stored Procedures
        /// <summary>
        /// Function to get a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">name of the stored procedure</param>
        /// <returns>Dbcommand object</returns>
        public DbCommand GetStoredProcCommand(String storedProcedureName)
        {
            return gObjDatabase.GetStoredProcCommand(storedProcedureName);
        }

        /// <summary>
        /// Function to add an input parameter to a stored procedure
        /// </summary>
        /// <param name="objCommand">the dbcommand of that stored procedure</param>
        /// <param name="name">name of the parameter</param>
        /// <param name="dbType">the datatbase type of the parameter</param>
        /// <param name="value">the value of the parameter</param>
        public void AddInParameter(DbCommand objCommand, String name, DbType dbType, object value)
        {
            gObjDatabase.AddInParameter(objCommand, name, dbType, value);
        }

        /// <summary>
        /// Function to add an Output parameter for a stored procedure
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <param name="name">the name of the parameter</param>
        /// <param name="dbType">the database type of the parameter</param>
        /// <param name="size">the size of the parameter based on the dbtype of parameter</param>
        public void AddOutParameter(DbCommand objCommand, String name, DbType dbType, int size)
        {
            gObjDatabase.AddOutParameter(objCommand, name, dbType, size);
        }

        /// <summary>
        /// Function to get the parameter value.
        /// </summary>
        /// <param name="objCommand">db objCommand for a stored procedure</param>
        /// <param name="name">the name of the parameter</param>
        /// <returns>object(value of the output parameter)</returns>
        public object GetParameterValue(DbCommand objCommand, String name)
        {
            return gObjDatabase.GetParameterValue(objCommand, name);
        }
        #endregion

        #region Select Statement Functions
        /// <summary>
        /// Function to get select values from the database
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <returns>IDatareader object</returns>
        public IDataReader ExecuteReader(DbCommand objCommand)
        {
            try
            {
                return gObjDatabase.ExecuteReader(objCommand);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }

        /// <summary>
        /// Function to get select values from the database
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <param name="objTransaction">the transation object</param>
        /// <returns>IDatareader object</returns>
        public IDataReader ExecuteReader(DbCommand objCommand, DbTransaction objTransaction)
        {
            try
            {
                return gObjDatabase.ExecuteReader(objCommand, objTransaction);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }

        /// <summary>
        /// Function to get the value from the records from db for select statement
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <returns>datatset with all the values</returns>
        public DataSet ExecuteDataSet(DbCommand objCommand)
        {
            try
            {
                return gObjDatabase.ExecuteDataSet(objCommand);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }

        /// <summary>
        /// Function to get the values as a datatable
        /// </summary>
        /// <param name="objCommand">dbcommand object</param>
        /// <returns>Datatable with all the values</returns>
        public DataTable GetDataTable(DbCommand objCommand)
        {
            try
            {
                DataTable dtReturnTable = new DataTable();
                DataSet dataset = new DataSet();
                dataset = gObjDatabase.ExecuteDataSet(objCommand);
                if (dataset != null)
                {
                    dtReturnTable = dataset.Tables[0];
                }
                return dtReturnTable;
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }
        #endregion

        #region Dataa Manipulation Functions
        /// <summary>
        /// Function to execute a query. 
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <returns>int</returns>
        public int ExecuteNonQuery(DbCommand objCommand)
        {
            try
            {
                return gObjDatabase.ExecuteNonQuery(objCommand);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }

        /// <summary>
        /// Function to execute a query but with the objcommand and transaction passed
        /// as parameters
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <param name="objTransaction">the transation object</param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbCommand objCommand, DbTransaction objTransaction)
        {
            try
            {
                return gObjDatabase.ExecuteNonQuery(objCommand, objTransaction);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }

        /// <summary>
        /// Function to get single value from the database
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <param name="objTransaction">the transation object</param>
        /// <returns>object with the value</returns>
        public object ExecuteScalar(DbCommand objCommand, DbTransaction objTransaction)
        {
            try
            {
                return gObjDatabase.ExecuteScalar(objCommand, objTransaction);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }

        /// <summary>
        /// Function to get single value from the database with transaction object passed as parameter
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <returns>object with the value</returns>
        public object ExecuteScalar(DbCommand objCommand)
        {
            try
            {
                return gObjDatabase.ExecuteScalar(objCommand);
            }
            catch (Exception ex)
            {
                ex.Data.Clear();
                ex.Data.Add("SP_Name", objCommand.CommandText);
                foreach (DbParameter param in objCommand.Parameters)
                {
                    ex.Data.Add(param.ParameterName, DBUtility.ObjectToString(param.Value));
                }
                throw ex;
            }
        }
        #endregion
    }
}
