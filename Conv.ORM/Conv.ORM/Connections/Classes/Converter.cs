using Conv.ORM.Connections.Helpers;
using Conv.ORM.Repositories;

namespace Conv.ORM.Connections.Classes
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
