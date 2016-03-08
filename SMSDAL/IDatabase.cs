#region File Header
/********************************************** 
 File Name      - IDatabase.cs
 Class Name     - IDatabase
 Project Name   - HM.Data
 Purpose        - Interface for the database function
**********************************************/
#endregion

#region Namespace Imports
using System;
using System.Data.Common;
using System.Data;
#endregion

namespace SMSDAL
{
    /// <summary>
    /// Interface for the database function
    /// </summary>
    public interface IDatabase
    {

        /// <summary>
        /// Function to get a database connection
        /// </summary>
        /// <returns>IDBConnection object</returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Function to get a SQL string
        /// </summary>
        /// <param name="storedProcedureName">SQL string</param>
        /// <returns>Dbcommand object</returns>
        DbCommand GetSqlStringCommand(String sqlString);

        /// <summary>
        /// Function to get a stored procedure
        /// </summary>
        /// <param name="storedProcedureName">name of the stored procedure</param>
        /// <returns>Dbcommand object</returns>
        DbCommand GetStoredProcCommand(string storedProcedureName);

        /// <summary>
        /// Function to add an input parameter to a stored procedure
        /// </summary>
        /// <param name="objCommand">the dbcommand of that stored procedure</param>
        /// <param name="name">name of the parameter</param>
        /// <param name="dbType">the datatbase type of the parameter</param>
        /// <param name="value">the value of the parameter</param>
        void AddInParameter(DbCommand objCommand, string name, DbType dbType, object value);

        /// <summary>
        /// Function to get the parameter value.
        /// </summary>
        /// <param name="objCommand">db objCommand for a stored procedure</param>
        /// <param name="name">the name of the parameter</param>
        /// <returns>object(value of the output parameter)</returns>
        object GetParameterValue(DbCommand objCommand, String name);

        /// <summary>
        /// Function to add an Output parameter for a stored procedure
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <param name="name">the name of the parameter</param>
        /// <param name="dbType">the database type of the parameter</param>
        /// <param name="size">the size of the parameter based on the dbtype of parameter</param>
        void AddOutParameter(DbCommand objCommand, string name, DbType dbType, int size);

        /// <summary>
        /// Function to get select values from the database
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <returns>IDatareader object</returns>
        IDataReader ExecuteReader(DbCommand objCommand);

        /// <summary>
        /// Function to get select values from the database
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <param name="objTransaction">the transation object</param>
        /// <returns>IDatareader object</returns>
        IDataReader ExecuteReader(DbCommand objCommand, DbTransaction objTransaction);

        /// <summary>
        /// Function to get the value from the records from db for select statement
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <returns>datatset with all the values</returns>
        DataSet ExecuteDataSet(DbCommand objCommand);

        /// <summary>
        /// Function to get the records from the db
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <returns>datatable with all the values</returns>
        DataTable GetDataTable(DbCommand objCommand);

        /// <summary>
        /// Function to execute a query. 
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <returns>int</returns>
        int ExecuteNonQuery(DbCommand objCommand);

        /// <summary>
        /// Function to execute a query but with the objcommand and transaction passed
        /// as parameters
        /// </summary>
        /// <param name="objCommand">the dbcommand of the stored procedure</param>
        /// <param name="objTransaction">the transation object</param>
        /// <returns></returns>
        int ExecuteNonQuery(DbCommand objCommand, DbTransaction objTransaction);

        /// <summary>
        /// Function to get single value from the database
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <param name="objTransaction">the transation object</param>
        /// <returns>object with the value</returns>
        object ExecuteScalar(DbCommand objCommand, DbTransaction objTransaction);

        /// <summary>
        /// Function to get single value from the database with transaction object passed as parameter
        /// </summary>
        /// <param name="objCommand">dbcommand object of the stored procedure to be executed</param>
        /// <returns>object with the value</returns>
        object ExecuteScalar(DbCommand objCommand);

        
    }
}
