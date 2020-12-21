using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Conv.ORM.Connection.Drivers.Interfaces;
using Conv.ORM.Connection.Helpers;
using Conv.ORM.Connection.Parameters;
using Conv.ORM.Repository;

namespace Conv.ORM.Connection.Drivers
{
    class SqlServerConnectionDriver : IConnectionDriver
    {
        private SqlConnection _connection;

        private static string GenerateConnectionString(ConnectionParameters parameters)
        {
            if (parameters.UserIntegratedSecurity)
            {
                return "Server=" + parameters.Host + "," + parameters.Port + ";Database=" + parameters.Database + ";Trusted_Connection=True;";

            }
            else
            {
                return "Server=" + parameters.Host + "," + parameters.Port + ";Database=" + parameters.Database + ";User Id=" + parameters.User + ";Password = " + parameters.Password + ";";
            }
        }

        public bool Connect(ConnectionParameters parameters)
        {
            _connection = new SqlConnection(GenerateConnectionString(parameters));
            try
            {
                _connection.Open();
                _connection.Close();
                return true;
            }
            catch (SqlException ex)
            {

                throw ConnectionHelper.SQLconnectionException(ex);
            }

        }

        public int ExecuteCommand(string sql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sql, Dictionary<string, object> parameters)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = sql
            };

#if DEBUG
            Console.WriteLine("Query: " + sql);
#endif

            foreach (var key in parameters.Keys)
            {
                command.Parameters.AddWithValue(key, parameters[key]);
            }

            command.Connection = _connection;

            try
            {
                _connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("Execute Non Query: OK");
                Console.WriteLine("Number of rows affected: " + rowsAffected.ToString());
                return rowsAffected;
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("Error: " + e.Message);
#endif
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }

        public Entity ExecuteScalarQuery(string sql, Type entityType)
        {
            throw new NotImplementedException();
        }

        public IList ExecuteQuery(string sql, Type entityType)
        {
            throw new NotImplementedException();
        }

        public int GetLastInsertedId()
        {
            throw new NotImplementedException();
        }
    }
}
