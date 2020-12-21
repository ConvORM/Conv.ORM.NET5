using Conv.ORM.Connection.Classes.CommandBuilders.Interfaces;
using Conv.ORM.Connection.Classes.QueryBuilders;
using Conv.ORM.Connection.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conv.ORM.Connection.Classes.CommandBuilders
{
    class MySqlCommandUpdateBuilder : ICommandUpdateBuilder
    {
        private readonly ModelEntity _modelEntity;
        private readonly QueryConditionsBuilder _queryConditionsBuilder;

        public MySqlCommandUpdateBuilder(ModelEntity model, QueryConditionsBuilder queryConditionsBuilder)
        {
            _modelEntity = model;
            _queryConditionsBuilder = queryConditionsBuilder;
        }

        public string GetSqlUpdate(out Dictionary<string, object> parametersValues)
        {
            var sql = new StringBuilder();

            sql.Append("UPDATE ");
            sql.Append(_modelEntity.TableName);

            sql.Append(" SET ");

            GetSqlFieldsAndParameters(out var fieldAndValues,
                                      out parametersValues);

            sql.Append(fieldAndValues);

            sql.Append(" WHERE ");
            sql.Append(GetWhere());

            return sql.ToString();
        }

        private void GetSqlFieldsAndParameters(out string fieldAndValues, out Dictionary<string, object> parametersValues)
        {
            parametersValues = new Dictionary<string, object>();

            var sqlFieldAndValues = new StringBuilder();

            foreach (var columnModelEntity in _modelEntity.ColumnsModelEntity)
            {
                if (columnModelEntity.Primary)
                    continue;
                sqlFieldAndValues.Append(columnModelEntity.ColumnName);

                sqlFieldAndValues.Append(" = ");

                var parameter = "?" + columnModelEntity.ColumnName;

                sqlFieldAndValues.Append(parameter);
                sqlFieldAndValues.Append(",");

                parametersValues.Add(parameter, columnModelEntity.Value);

            }

            sqlFieldAndValues.Remove(sqlFieldAndValues .Length - 1, 1);

            fieldAndValues = sqlFieldAndValues.ToString();
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

