using Conv.ORM.Connection.Classes;
using Conv.ORM.Repository;
using System.Collections;
using Conv.ORM.Connection.Classes.QueryBuilders;

namespace Conv.ORM.Connection.DataTransferor.Interfaces
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