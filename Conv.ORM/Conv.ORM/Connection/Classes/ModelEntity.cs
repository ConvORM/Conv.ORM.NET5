using System;
using System.Collections.Generic;
using System.Linq;

namespace Conv.ORM.Connection.Classes
{
    internal class ModelEntity
    {
        public string TableName { get; set; }
        public string ConnectionName { get; set; }
        public Type EntityType { get; set; }

        public List<ColumnModelEntity> ColumnsModelEntity { get; set; }

        internal IEnumerable<ColumnModelEntity> GetPrimaryFields()
        {
            return ColumnsModelEntity.Where(column => column.Primary).ToList();
        }

        internal int GetPrimaryFieldValue(string primaryFieldName)
        {
            return (int)ColumnsModelEntity.Where(column => column.ColumnName == primaryFieldName).FirstOrDefault().Value;
        }
    }
}
