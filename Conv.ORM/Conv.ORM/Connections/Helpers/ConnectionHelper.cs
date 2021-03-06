using MySql.Data.MySqlClient;
using Conv.ORM.Exceptions;
using System.Data.SqlClient;
using System;

namespace Conv.ORM.Connections.Helpers
{
    internal static class ConnectionHelper
    {
        private const string ConnectionInitCode = "A";
        internal static ConnectionException HandlerMySqlException(MySqlException myEx)
        {
            if (myEx.Message.Contains("Unable to connect to any of the specified MySQL hosts"))
            {
                //Exception host and/or port invalids
                return new ConnectionException(
                    ConnectionInitCode + "001",
                    "Unable to connect to the specified host.",
                    "Check if:" + Environment.NewLine +
                    "- Host and/or Port are correct;" + Environment.NewLine +
                    "- The service of your database is running;");

            }
            else if (myEx.Message.Contains("Access denied for user"))
            {
                //Exception user and/or password invalids
                return new ConnectionException(
                    ConnectionInitCode + "002",
                    "Access denied",
                    "Check if:" + Environment.NewLine +
                    "- User and/or Password are correct;");
            }
            else if (myEx.Message.Contains("Unknown database"))
            {
                //Exception database invalid
                return new ConnectionException(
                    ConnectionInitCode + "003",
                    "Unknown database",
                    "Check if:" + Environment.NewLine +
                    "- Database name was spelled correctly;");
            }
            else if (myEx.Message.Contains("Reading from the stream has failed"))
            {
                //Old version
                return new ConnectionException(
                    ConnectionInitCode + "004",
                    "Reading from the stream has failed" + Environment.NewLine +
                    "  This error may be generated on try to connect in older or incompatibility version of the database engine",
                    "Check if:" + Environment.NewLine +
                    "- Your database engine version is listed as the compatible in our website;");
            }
            else
            {
                return new ConnectionException(Convert.ToString(myEx.ErrorCode), myEx.Message);
            }
        }

        internal static ConnectionException SQLconnectionException(SqlException myEx)
        {
            if (myEx.Message.Contains("Login failed"))
            {
                //Exception user and/or password invalids                
                return new ConnectionException(
                    ConnectionInitCode + "001",
                    "Access denied",
                    "- User and/or Password are correct;");
            }
            else
            {
                return new ConnectionException(Convert.ToString(myEx.ErrorCode), myEx.Message);
            }
        }
    }
}
