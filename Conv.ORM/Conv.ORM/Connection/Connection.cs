using Conv.ORM.Connection.Drivers;
using Conv.ORM.Connection.Drivers.Interfaces;
using Conv.ORM.Connection.Enums;
using Conv.ORM.Connection.Parameters;

namespace Conv.ORM.Connection
{
    public class Connection
    {
        public ConnectionParameters Parameters { get; private set; }
        public bool Connected { get; set; }

        private IConnectionDriver _connectionDriver;

        public Connection(ConnectionParameters parameters)
        {
            Parameters = parameters;
        }

        internal IConnectionDriver ConnectionDriver()
        {
            return _connectionDriver;
        }

        public Connection GetConnection()
        {
            if (_connectionDriver == null)
            {
                LoadConnectionDriver();
            }

            Connected = _connectionDriver.Connect(Parameters);

            if (Connected)
                ConnectionFactory.AddConnection(this, "");

            return this;

        }

        private void LoadConnectionDriver()
        {
            switch (Parameters.ConnectionDriverType)
            {
                case EConnectionDriverTypes.ecdtFirebird:
                    _connectionDriver = new FirebirdConnectionDriver();
                    break;
                case EConnectionDriverTypes.ecdtMySql:
                    _connectionDriver = new MySqlConnectionDriver();
                    break;
                case EConnectionDriverTypes.ecdtPostgreeSQL:
                    _connectionDriver = new PostgreeSqlConnectionDriver();
                    break;
                case EConnectionDriverTypes.ecdtSQLServer:
                    _connectionDriver = new SqlServerConnectionDriver();
                    break;
                default:
                    _connectionDriver = null;
                    break;
            }
        }

    }
}
