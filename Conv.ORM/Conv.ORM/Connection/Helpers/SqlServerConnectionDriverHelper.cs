using Conv.ORM.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv.ORM.Connection.Helpers
{
    class SqlServerConnectionDriverHelper
    {

        public static Entity ConvertReaderToEntity(MySqlDataReader reader, Type type)
        {
            var instance = Activator.CreateInstance(type);

            while (reader.Read())
            {
                foreach (var field in type.GetFields())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        if (field.Name != reader.GetName(i)) continue;
                        if (reader.GetValue(i).GetType() == field.FieldType)
                        {
                            field.SetValue(instance, reader.GetValue(i));
                            break;
                        }
                        else if (CompatibilityFormat(reader.GetValue(i), field.FieldType, out var convertedValue))
                        {
                            field.SetValue(instance, convertedValue);
                        }
                        else
                        {
#if DEBUG
                            Console.WriteLine(field.Name + " in query return if wrong type. Type returned in query result: " + reader.GetValue(i).GetType().ToString() + " Type of entity field: " + field.FieldType.ToString());
#endif
                        }
                    }
                }
            }

            return (Entity)instance;

        }

        private static bool CompatibilityFormat(object valueFromReader, Type typeOfEntityField, out object convertedValue)
        {
            convertedValue = null;
            if (valueFromReader.GetType().Name != "SByte") return convertedValue != null;
            if (typeOfEntityField.Name == "Boolean")
            {
                convertedValue = ((sbyte)valueFromReader) == 1;
            }

            return convertedValue != null;
        }

        public static IList ConvertReaderToCollectionOfEntity(MySqlDataReader reader, Type entityType)
        {

            var listType = typeof(List<>).MakeGenericType(entityType);
            var entities = (IList)Activator.CreateInstance(listType);


            while (reader.Read())
            {
                var instance = Activator.CreateInstance(entityType);

                foreach (var field in entityType.GetFields())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        if (field.Name != reader.GetName(i)) continue;
                        if (reader.GetValue(i).GetType() == field.FieldType)
                        {
                            field.SetValue(instance, reader.GetValue(i));
                            break;
                        }
                        else if (CompatibilityFormat(reader.GetValue(i), field.FieldType, out var convertedValue))
                        {
                            field.SetValue(instance, convertedValue);
                        }
                        else
                        {
#if DEBUG
                            Console.WriteLine(field.Name + " in query return if wrong type. Type returned in query result: " + reader.GetValue(i).GetType().ToString() + " Type of entity field: " + field.FieldType.ToString());
#endif
                        }
                    }
                }

                entities.Add((Entity)instance);
            }

            return entities;

        }

    }
}
