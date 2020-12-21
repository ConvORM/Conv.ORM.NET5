using System.Collections.Generic;

namespace Conv.ORM.Connection.Classes.CommandBuilders.Interfaces
{
    interface ICommandUpdateBuilder
    {
        string GetSqlUpdate(out Dictionary<string, object> parametersValues);
    }
}
