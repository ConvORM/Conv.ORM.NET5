using Conv.ORM.Connection.Parameters;
using Conv.ORM.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Conv.ORM.Connection.Drivers.Interfaces
{
    internal interface IConnectionDriver
    {
        /// <summary>
        /// Connect to database
        /// </summary>
        /// <param name="parameters">Parameters connection</param>
        /// <returns>Returns if the connection was successful</returns>
        bool Connect(ConnectionParameters parameters);
        int ExecuteCommand(string sql);
        /// <summary>
        /// Executes a command against a given database over a connection
        /// </summary>
        /// <param name="sql">Sql script to run</param>
        /// <param name="parameters">Parameters to be added to the command</param>
        /// <returns>The number of rows affected</returns>
        int ExecuteCommand(string sql, Dictionary<string, object> parameters);
        /// <summary>
        /// Executes a query command against a given database over a connection
        /// </summary>
        /// <param name="sql">Sql script to run</param>
        /// <param name="entityType">Type of entity to be returned</param>
        /// <returns>A entity get into a result of query runs</returns>
        Entity ExecuteScalarQuery(string sql, Type entityType);
        /// <summary>
        /// Executes a query command against a given database to be returned
        /// </summary>
        /// <param name="sql">Sql script to run</param>
        /// <param name="entityType">Type of entity to be returned</param>
        /// <returns></returns>
        IList ExecuteQuery(string sql, Type entityType);
        /// <summary>
        /// Get the last inserted id
        /// </summary>
        /// <returns>The last inserted id</returns>
        int GetLastInsertedId();
    }
}
