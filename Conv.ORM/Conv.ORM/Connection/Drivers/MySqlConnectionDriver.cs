using Conv.ORM.Connection.Classes;
using Conv.ORM.Connection.Drivers.Interfaces;
using Conv.ORM.Connection.Helpers;
using Conv.ORM.Connection.Parameters;
using Conv.ORM.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Conv.ORM.Connection.Drivers
{
    internal class MySqlConnectionDriver : IConnectionDriver
    {
        private MySqlConnection _connection;
        private readonly MySqlConnectionDriverHelper _helper;

        public MySqlConnectionDriver()
        {
            _helper = new MySqlConnectionDriverHelper();
        }

        public bool Connect(ConnectionParameters parameters)
        {
            _connection = new MySqlConnection(GenerateConnectionString(parameters));
            try
            {
                _connection.Open();
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ConnectionHelper.HandlerMySqlException(ex);
            }
        }

        public int ExecuteCommand(string sql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sql, Dictionary<string, object> parameters)
        {
            MySqlCommand command = new MySqlCommand
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
            var command = new MySqlCommand
            {
                CommandText = sql
            };

#if DEBUG
            Console.WriteLine("Query: " + sql);
#endif

            command.Connection = _connection;

            try
            {
                _connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return MySqlConnectionDriverHelper.ConvertReaderToEntity(reader, entityType);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("Error: " + e.Message);
#endif
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public IList ExecuteQuery(string sql, Type entityType)
        {
            var command = new MySqlCommand
            {
                CommandText = sql
            };

#if DEBUG
            Console.WriteLine("Query: " + sql);
#endif

            command.Connection = _connection;

            try
            {
                _connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return MySqlConnectionDriverHelper.ConvertReaderToCollectionOfEntity(reader, entityType);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("Error: " + e.Message);
#endif
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public int GetLastInsertedId()
        {
            var lastId = new MySqlCommand { Connection = _connection, CommandText = ("SELECT LAST_INSERT_ID()") };

            try
            {
                _connection.Open();
                var lid = lastId.ExecuteReader();
                Console.WriteLine("Execute Last ID: OK");
                Console.WriteLine("Execute Last ID - Has Rows: " + (lid.HasRows ? "True" : "False"));
                lid.Read();
                return Convert.ToInt32((ulong)lid[0]);
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

        public Entity Insert(Entity entity)
        {
            Converter.EntityToModelEntity(entity);
            throw new NotImplementedException();
        }

        private static string GenerateConnectionString(ConnectionParameters parameters)
        {
            return "Server=" + parameters.Host + ";Port=" + parameters.Port + ";Database=" + parameters.Database + ";Uid=" + parameters.User + ";Pwd = " + parameters.Password + ";";
        }

    }
}
