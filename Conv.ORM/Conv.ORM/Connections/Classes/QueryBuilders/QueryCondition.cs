using Conv.ORM.Connections.Enums;

namespace Conv.ORM.Connections.Classes
{
    internal class QueryCondition
    {
        public string Field { get; set; }
        public EConditionTypes Type { get; set; }
        public object[] Values { get; set; }
        public ELogicalConditionTypes LogicalType { get; set; }
    }
}
