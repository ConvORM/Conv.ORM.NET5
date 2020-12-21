using System.Collections.Generic;
using Conv.ORM.Connection.Enums;

namespace Conv.ORM.Connection.Classes.QueryBuilders
{
    public class QueryConditionsBuilder
    {
        internal readonly List<QueryCondition> QueryConditionList;
        
        public QueryConditionsBuilder()
        {
            QueryConditionList = new List<QueryCondition>();
        }

        public QueryConditionsBuilder AddQueryCondition(string field, EConditionTypes conditionTypes, object[] values, ELogicalConditionTypes logicalConditionTypes = ELogicalConditionTypes.And)
        {
            var queryCondition = new QueryCondition
            {
                Field = field, Type = conditionTypes, Values = values, LogicalType = logicalConditionTypes
            };

            QueryConditionList.Add(queryCondition);

            return this;
        }
    }
}
