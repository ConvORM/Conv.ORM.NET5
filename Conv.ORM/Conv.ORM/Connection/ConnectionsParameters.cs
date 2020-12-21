using Conv.ORM.Connection.Parameters;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Conv.ORM.Connection
{
    [XmlRoot("connections")]
    public class ConnectionsParameters
    {
        [XmlElement("connection")]
        public List<ConnectionParameters> Connections { get; set; }
    }
}
