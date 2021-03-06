using Conv.ORM.Connections.DataTransferor.Interfaces;
using Conv.ORM.Connections.Enums;
using System;
using Conv.ORM.Connections.Classes;

namespace Conv.ORM.Connections.DataTransferor
{
    internal static class DataTransferorFactory
    {
        internal static IDataTransfer GetDataTransferor(ModelEntity modelEntity)
        {
            var connection = ConnectionFactory.GetConnection(modelEntity.ConnectionName);
            switch (connection.Parameters.ConnectionDriverType)
            {
                case EConnectionDriverTypes.ecdtFirebird:
                    return null;
                case EConnectionDriverTypes.ecdtMySql:
                    return  new MySqlDataTransferor(modelEntity, connection);
                case EConnectionDriverTypes.ecdtPostgreeSQL:
                    return null;
                case EConnectionDriverTypes.ecdtSQLServer:
                    return new SqlServerDataTransferor(modelEntity, connection);
                case EConnectionDriverTypes.ecdtNone:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
