using Conv.ORM.Connection.Enums;

namespace Conv.ORM.Connection.Classes
{
    internal class QueryCondition
    {
        public string Field { get; set; }
        public EConditionTypes Type { get; set; }
        public object[] Values { get; set; }
        public ELogicalConditionTypes LogicalType { get; set; }
    }
}
