using Conv.ORM.Connection.Enums;
using Conv.ORM.Connection.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace Conv.ORM.Connection
{
    public static class ConnectionFactory
    {
        private static Dictionary<string, Connection> _connections;
        private static ConnectionsParametersFile _connectionsParametersFile;

        internal static void AddConnection(Connection connection, string name)
        {
            _connections.Add(name, connection);
        }

        public static Connection GetConnection()
        {
            Initialize();
            var connection = LocateConnection();

            if (connection != null) return connection;
            var parameters = _connectionsParametersFile.GetFirstConnectionParameter();
            connection = GetNewConnection(parameters);

            return connection;
        }

        public static Connection GetConnection(ConnectionParameters parameters)
        {
            Initialize();
            var connection = LocateConnection(parameters);

            return connection ?? GetNewConnection(parameters);
        }

        public static Connection GetConnection(string name)
        {
            Initialize();
            if ((name == "") || (name == null))
            {
                return GetConnection();
            }
            else
            {
                var connection = LocateConnection(name);

                if (connection != null) return connection;
                var parameters = _connectionsParametersFile.GetConnectionParameters(name);
                connection = GetNewConnection(parameters);

                return connection;
            }
        }

        public static Connection GetConnection(EConnectionDriverTypes type)
        {
            {
                Initialize();
                var connection = LocateConnection(type);

                if (connection != null) return connection;
                var parameters = _connectionsParametersFile.GetConnectionParameters(type);
                connection = GetNewConnection(parameters);

                return connection;
            }
        }

        private static Connection GetNewConnection(ConnectionParameters parameters)
        {
            return new Connection(parameters).GetConnection();
        }

        private static Connection LocateConnection()
        {
            if (_connections.Count == 0)
                return null;
            else
            {
                var firstKey = _connections.Keys.First();
                return _connections[firstKey];
            }
        }

        private static Connection LocateConnection(ConnectionParameters parameters)
        {
            return _connections.Values.FirstOrDefault(connection => connection.Parameters == parameters);
        }

        private static Connection LocateConnection(string name)
        {
            return _connections.Values.FirstOrDefault(connection => connection.Parameters.Name == name);
        }

        private static Connection LocateConnection(EConnectionDriverTypes type)
        {
            return _connections.Values.FirstOrDefault(connection => connection.Parameters.ConnectionDriverType == type);
        }

        private static void Initialize()
        {
            if (_connections == null)
            {
                _connections = new Dictionary<string, Connection>();
            }

            if (_connectionsParametersFile == null)
                _connectionsParametersFile = new ConnectionsParametersFile();
        }
            
    }
}
