using Conv.ORM.Connections.Classes;
using Conv.ORM.Repositories;
using System.Collections;
using Conv.ORM.Connections.Classes.QueryBuilders;

namespace Conv.ORM.Connections.DataTransferor.Interfaces
{
    public interface IDataTransfer
    {
        Entity Delete();
        Entity Insert();
        Entity SetDeleted();
        Entity Update();

        IList FindAll();
        IList Find(QueryConditionsBuilder conditionsBuilder);
        Entity Find(int[] ids);
    }
}