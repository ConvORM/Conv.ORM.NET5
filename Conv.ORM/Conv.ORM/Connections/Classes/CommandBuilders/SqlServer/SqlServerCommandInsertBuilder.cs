using Conv.ORM.Connections.Classes.CommandBuilders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv.ORM.Connections.Classes.CommandBuilders
{
    internal class SqlServerCommandInsertBuilder : ICommandInsertBuilder
    {

        private readonly ModelEntity _modelEntity;

        public SqlServerCommandInsertBuilder(ModelEntity model)
        {
            _modelEntity = model;
        }

        private void GetSqlFieldsAndParameters(out string fields, out string values, out Dictionary<string, object> parametersValues)
        {
            parametersValues = new Dictionary<string, object>();

            var sqlFields = new StringBuilder();
            var sqlValues = new StringBuilder();

            sqlFields.Append(" (");
            sqlValues.Append(" (");
            foreach (var columnModelEntity in _modelEntity.ColumnsModelEntity)
            {
                sqlFields.Append(columnModelEntity.ColumnName);
                sqlFields.Append(",");

                var parameter = "@" + columnModelEntity.ColumnName;

                sqlValues.Append(parameter);
                sqlValues.Append(",");

                parametersValues.Add(parameter, columnModelEntity.Value);

            }

            sqlFields.Remove(sqlFields.Length - 1, 1);
            sqlFields.Append(") ");

            sqlValues.Remove(sqlValues.Length - 1, 1);
            sqlValues.Append(") ");

            fields = sqlFields.ToString();
            values = sqlValues.ToString();
        }

        string ICommandInsertBuilder.GetSqlInsert(out Dictionary<string, object> parametersValues)
        {
            var sql = new StringBuilder();

            sql.Append("INSERT INTO ");
            sql.Append(_modelEntity.TableName);

            GetSqlFieldsAndParameters(out var fields,
                                      out var values,
                                      out parametersValues);

            sql.Append(fields);

            sql.Append(" VALUES ");

            sql.Append(values);

            return sql.ToString();
        }
    }
}

