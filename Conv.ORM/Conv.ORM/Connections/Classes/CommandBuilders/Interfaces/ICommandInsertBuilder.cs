using System.Collections.Generic;

namespace Conv.ORM.Connections.Classes.CommandBuilders.Interfaces
{
    interface ICommandInsertBuilder
    {
        string GetSqlInsert(out Dictionary<string, object> parametersValues);
    }
}
