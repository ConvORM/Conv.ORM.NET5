using System;
using System.Collections;
using System.Data;

namespace Conv.ORM.Connection.Helpers
{
    public class ConvORMHelper
    {
        public static DataTable ConvertListToDataTable(IList list)
        {
            if (list.Count == 0)
            {
#if DEBUG
                Console.Write("Empty List");
#endif
                return null;
            }

            var dataTable = new DataTable();
            var typeObjectIntoList = list[0].GetType();
            var fields = typeObjectIntoList.GetFields();

            foreach (var field in fields)   
            {
                dataTable.Columns.Add(field.Name, field.FieldType);
            }

            foreach (var entity in list)
            {
                var values = new object[fields.Length];

                var i = 0;

                foreach (var field in fields)
                {
                    values.SetValue(field.GetValue(entity),i);
                    i++;
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;

        }
    }
}
