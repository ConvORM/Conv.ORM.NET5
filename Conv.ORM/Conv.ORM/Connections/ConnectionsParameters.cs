using Conv.ORM.Connections.Parameters;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Conv.ORM.Connections
{
    [XmlRoot("connections")]
    public class ConnectionsParameters
    {
        [XmlElement("connection")]
        public List<ConnectionParameters> Connections { get; set; }
    }
}
