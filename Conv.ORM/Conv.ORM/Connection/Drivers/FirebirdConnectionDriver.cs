using Conv.ORM.Connection.Drivers.Interfaces;
using Conv.ORM.Connection.Parameters;
using Conv.ORM.Repository;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Conv.ORM.Connection.Drivers
{
    class FirebirdConnectionDriver : IConnectionDriver
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

        Entity IConnectionDriver.ExecuteScalarQuery(string sql, Type entityType)
        {
            throw new NotImplementedException();
        }
    }
}
