using Conv.ORM.Connection.Helpers;
using Conv.ORM.Repository;

namespace Conv.ORM.Connection.Classes
{
    internal static class Converter
    {
        internal static ModelEntity EntityToModelEntity(Entity entity)
        {
            var helper = new ConverterModelEntityHelper(entity);
            var model = new ModelEntity
            {
                TableName = helper.GetTableName(), ColumnsModelEntity = helper.GetColumnsModelEntity(), EntityType = entity.GetType()
            };
            return model;
        }
    }
}
