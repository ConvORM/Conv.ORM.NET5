using Conv.ORM.Connections.Classes.CommandBuilders;
using Conv.ORM.Connections.Classes.CommandBuilders.Interfaces;
using Conv.ORM.Connections.Classes.CommandBuilders.SqlServer;
using Conv.ORM.Connections.Classes.QueryBuilders;
using Conv.ORM.Connections.Enums;
using System.Collections.Generic;

namespace Conv.ORM.Connections.Classes
{
    internal class CommandFactory
    {
        private readonly ModelEntity _modelEntity;
        private readonly EConnectionDriverTypes _eConnectionDriver;

        public CommandFactory(ModelEntity model, EConnectionDriverTypes eConnectionDriver)
        {
            _modelEntity = model;
            _eConnectionDriver = eConnectionDriver;
        }

        internal string GetSqlInsert(out Dictionary<string, object> parametersValues)
        {
            var commandInsertBuilder = GetCommandInsertBuilder();
            return commandInsertBuilder.GetSqlInsert(out parametersValues);
        }

        internal string GetSqlSelect(QueryConditionsBuilder queryConditionsBuilder)
        {
            var commandSelectBuilder = GetCommandSelectBuilder(queryConditionsBuilder);
            return commandSelectBuilder.GetSqlSelect();
        }

        internal string GetSqlUpdate(out Dictionary<string, object> parametersValues, QueryConditionsBuilder queryConditionsBuilder)
        {
            var commandUpdateBuilder = GetCommandUpdateBuilder(queryConditionsBuilder);
            return commandUpdateBuilder.GetSqlUpdate(out parametersValues);
        }

        private ICommandInsertBuilder GetCommandInsertBuilder()
        {
            switch (_eConnectionDriver)
            {
                case EConnectionDriverTypes.ecdtFirebird:
                    return null;
                case EConnectionDriverTypes.ecdtMySql:
                    return new MySqlCommandInsertBuilder(_modelEntity);
                case EConnectionDriverTypes.ecdtPostgreeSQL:
                    return null;
                case EConnectionDriverTypes.ecdtSQLServer:
                    return new SqlServerCommandInsertBuilder(_modelEntity);
                case EConnectionDriverTypes.ecdtNone:
                    return null;
                default:
                    return null;
            }
        }

        private ICommandSelectBuilder GetCommandSelectBuilder(QueryConditionsBuilder queryConditionsBuilder)
        {
            switch (_eConnectionDriver)
            {
                case EConnectionDriverTypes.ecdtFirebird:
                    return null;
                case EConnectionDriverTypes.ecdtMySql:
                    return new MySqlCommandSelectBuilder(_modelEntity, queryConditionsBuilder);
                case EConnectionDriverTypes.ecdtPostgreeSQL:
                    return null;
                case EConnectionDriverTypes.ecdtSQLServer:
                    return new SqlServerCommandSelectBuilder(_modelEntity, queryConditionsBuilder);
                case EConnectionDriverTypes.ecdtNone:
                    return null;
                default:
                    return null;
            }
        }

        private ICommandUpdateBuilder GetCommandUpdateBuilder(QueryConditionsBuilder queryConditionsBuilder)
        {
            switch (_eConnectionDriver)
            {
                case EConnectionDriverTypes.ecdtFirebird:
                    return null;
                case EConnectionDriverTypes.ecdtMySql:
                    return new MySqlCommandUpdateBuilder(_modelEntity, queryConditionsBuilder);
                case EConnectionDriverTypes.ecdtPostgreeSQL:
                    return null;
                case EConnectionDriverTypes.ecdtSQLServer:
                    return new SqlServerCommandUpdateBuilder(_modelEntity, queryConditionsBuilder);
                case EConnectionDriverTypes.ecdtNone:
                    return null;
                default:
                    return null;
            }
        }

    }
}
