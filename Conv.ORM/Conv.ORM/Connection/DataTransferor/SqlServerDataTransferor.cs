using Conv.ORM.Connection.Classes;
using Conv.ORM.Connection.Classes.QueryBuilders;
using Conv.ORM.Connection.Enums;
using Conv.ORM.Repository;
using System;
using System.Collections;

namespace Conv.ORM.Connection.DataTransferor.Interfaces
{
    class SqlServerDataTransferor : IDataTransfer
    {
        private readonly ModelEntity _modelEntity;
        private readonly Connection _connection;

        public SqlServerDataTransferor(ModelEntity modelEntity, Connection connection)
        {
            _modelEntity = modelEntity;
            _connection = connection;
        }

        public Entity Delete()
        {
            throw new System.NotImplementedException();
        }

        public Entity Insert()
        {
            var commandBuilder = new CommandFactory(_modelEntity, EConnectionDriverTypes.ecdtSQLServer);

            if (_connection.ConnectionDriver()
                .ExecuteCommand(commandBuilder.GetSqlInsert(out var parametersValues), parametersValues) > 0)
            {
                var lastInsertedId = _connection.ConnectionDriver().GetLastInsertedId();
                var conditionsBuilder = new QueryConditionsBuilder();
                foreach (var column in _modelEntity.GetPrimaryFields())
                {

                    conditionsBuilder.AddQueryCondition(column.ColumnName, EConditionTypes.Equals,
                        new object[] { lastInsertedId });
                }

                return _connection.ConnectionDriver().ExecuteScalarQuery(commandBuilder.GetSqlSelect(conditionsBuilder),
                    _modelEntity.EntityType);
            }
            else
                return null;
        }

        public Entity SetDeleted()
        {
            throw new System.NotImplementedException();
        }

        public Entity Update()
        {
            var commandBuilder = new CommandFactory(_modelEntity, EConnectionDriverTypes.ecdtSQLServer);

            var conditionsBuilder = new QueryConditionsBuilder();
            foreach (var column in _modelEntity.GetPrimaryFields())
            {
                conditionsBuilder.AddQueryCondition(column.ColumnName, EConditionTypes.Equals,
                    new object[] { _modelEntity.GetPrimaryFieldValue(column.ColumnName) });
            }

            if (_connection.ConnectionDriver()
                .ExecuteCommand(commandBuilder.GetSqlUpdate(out var parametersValues, conditionsBuilder), parametersValues) > 0)
            {
                var lastInsertedId = _connection.ConnectionDriver().GetLastInsertedId();
                return Find(new int[] { lastInsertedId });
            }
            else
                return null;
        }

        public IList FindAll()
        {
            return _connection.ConnectionDriver()
                .ExecuteQuery(new CommandFactory(_modelEntity, EConnectionDriverTypes.ecdtSQLServer).GetSqlSelect(new QueryConditionsBuilder()),
                    _modelEntity.EntityType);
        }

        public IList Find(QueryConditionsBuilder conditionsBuilder)
        {
            return _connection.ConnectionDriver()
                .ExecuteQuery(new CommandFactory(_modelEntity, EConnectionDriverTypes.ecdtSQLServer).GetSqlSelect(conditionsBuilder),
                    _modelEntity.EntityType);
        }

        public Entity Find(int[] ids)
        {
            var conditionsBuilder = new QueryConditionsBuilder();
            int idsCount = 0;
            foreach (var column in _modelEntity.GetPrimaryFields())
            {
                conditionsBuilder.AddQueryCondition(column.ColumnName, EConditionTypes.Equals,
                    new object[] { ids[0] });

                idsCount++;
            }
            return _connection.ConnectionDriver()
                .ExecuteScalarQuery(new CommandFactory(_modelEntity, EConnectionDriverTypes.ecdtSQLServer).GetSqlSelect(conditionsBuilder),
                    _modelEntity.EntityType);
        }
    }
}
