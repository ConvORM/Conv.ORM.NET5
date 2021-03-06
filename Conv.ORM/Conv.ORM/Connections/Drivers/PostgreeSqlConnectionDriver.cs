using Conv.ORM.Connections.Drivers.Interfaces;
using Conv.ORM.Connections.Parameters;
using Conv.ORM.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Conv.ORM.Connections.Drivers
{
    class PostgreeSqlConnectionDriver : IConnectionDriver
    {
        public bool Connect(ConnectionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sql, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public IList ExecuteQuery(string sql, Type entityType)
        {
            throw new NotImplementedException();
        }

        public Entity ExecuteScalarQuery(string sql, Type entityType)
        {
            throw new NotImplementedException();
        }

        public int GetLastInsertedId()
        {
            throw new NotImplementedException();
        }
    }
}
