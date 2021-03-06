using Conv.ORM.Connection.Classes.CommandBuilders.Interfaces;
using Conv.ORM.Connection.Classes.QueryBuilders;
using Conv.ORM.Connection.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conv.ORM.Connection.Classes.CommandBuilders.SqlServer
{
    internal class SqlServerCommandSelectBuilder : ICommandSelectBuilder
    {
        private readonly ModelEntity _modelEntity;
        private readonly QueryConditionsBuilder _queryConditionsBuilder;

        public SqlServerCommandSelectBuilder(ModelEntity model, QueryConditionsBuilder queryConditions)
        {
            _modelEntity = model;
            _queryConditionsBuilder = queryConditions;
        }

        public string GetSqlSelect()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT ");

            sql.Append(GetSqlFields());

            sql.Append(" FROM ");
            sql.Append("[");
            sql.Append(_modelEntity.TableName);
            sql.Append("]");

            if (!HasWhere()) return sql.ToString();

            sql.Append(" WHERE ");
            sql.Append(GetWhere());

            return sql.ToString();
        }

        private string GetSqlFields()
        {
            var sqlFields = new StringBuilder();

            foreach (var columnModelEntity in _modelEntity.ColumnsModelEntity)
            {
                sqlFields.Append("[");
                sqlFields.Append(_modelEntity.TableName);
                sqlFields.Append("]");
                sqlFields.Append(".");
                sqlFields.Append("[");
                sqlFields.Append(columnModelEntity.ColumnName);
                sqlFields.Append("]");
                sqlFields.Append(",");
            }

            sqlFields.Remove(sqlFields.Length - 1, 1);

            return sqlFields.ToString();
        }

        private string GetWhere()
        {
            var sqlWhere = new StringBuilder();
            foreach (var condition in _queryConditionsBuilder.QueryConditionList)
            {
                sqlWhere.Append(condition.Field);

                switch (condition.Type)
                {
                    case EConditionTypes.In:
                        sqlWhere.Append(" IN ");
                        sqlWhere.Append(GetSqlIn(condition.Values));
                        break;
                    case EConditionTypes.Between:
                        sqlWhere.Append(" BETWEEN ");
                        sqlWhere.Append(GetSqlBetween(condition.Values));
                        break;
                    case EConditionTypes.Like:
                        sqlWhere.Append(" LIKE ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.LessThan:
                        sqlWhere.Append(" < ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.LessThanOrEquals:
                        sqlWhere.Append(" <= ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.MoreThan:
                        sqlWhere.Append(" > ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.MoreThanOrEquals:
                        sqlWhere.Append(" >= ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.Equals:
                        sqlWhere.Append(" = ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.Not:
                        sqlWhere.Append(" NOT ");
                        sqlWhere.Append(ConvertValue(condition.Values[0]));
                        break;
                    case EConditionTypes.IsNull:
                        sqlWhere.Append(" IS null ");
                        break;
                    case EConditionTypes.IsNotNull:
                        sqlWhere.Append(" IS NOT null ");
                        break;
                    default:
                        break;
                }
                if (_queryConditionsBuilder.QueryConditionList.IndexOf(condition) < (_queryConditionsBuilder.QueryConditionList.Count - 1))
                {
                    sqlWhere.Append(" ");
                    sqlWhere.Append(condition.LogicalType == ELogicalConditionTypes.And ? "AND" : "OR");
                    sqlWhere.Append(" ");
                }

            }

            return sqlWhere.ToString();

        }

        private string GetSqlBetween(object[] conditionValues)
        {
            if (conditionValues.Length != 2) throw new Exception("The condition of type BETWEEN require two values");

            var sqlBetween = new StringBuilder();
            sqlBetween.Append(" ");
            sqlBetween.Append(ConvertValue(conditionValues[0]));
            sqlBetween.Append(" AND ");
            sqlBetween.Append(ConvertValue(conditionValues[1]));
            sqlBetween.Append(" ");
            return sqlBetween.ToString();
        }

        private bool HasWhere()
        {
            return _queryConditionsBuilder.QueryConditionList.Count > 0;
        }

        private string GetSqlIn(object[] conditionValues)
        {
            if (!(conditionValues.Length > 0)) throw new Exception("The condition of type IN require one or more values");

            var sqlIn = new StringBuilder();
            sqlIn.Append("(");
            foreach (var conditionValue in conditionValues)
            {
                switch (conditionValue)
                {
                    case List<string> list:
                        sqlIn.Append("'");
                        sqlIn.Append(string.Join("','", list));
                        sqlIn.Append("'");
                        break;
                    case List<int> _:
                        if (sqlIn.Length > 1) sqlIn.Append(",");
                        sqlIn.Append(string.Join(",", (List<string>)conditionValue ?? throw new InvalidOperationException()));
                        break;
                    default:
                        if (sqlIn.Length > 1) sqlIn.Append(",");
                        sqlIn.Append(ConvertValue(conditionValue));
                        break;
                }
            }

            sqlIn.Append(") ");
            return sqlIn.ToString();
        }

        private static string ConvertValue(object value)
        {

            switch (value)
            {
                case int i:
                    return i.ToString();
                case string s:
                    return "'" + s + "'";
                case DateTime time:
                    return "'" + time.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                default:
                    return "'" + value.ToString() + "'";
            }
        }
    }
}
