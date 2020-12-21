using Conv.ORM.Connection.Enums;
using System.Xml.Serialization;

namespace Conv.ORM.Connection.Parameters
{
    public class ConnectionParameters
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("driver")]
        public EConnectionDriverTypes ConnectionDriverType { get; set; }
        [XmlElement("host")]
        public string Host { get; set; }
        [XmlElement("port")]
        public string Port { get; set; }
        [XmlElement("user")]
        public string User { get; set; }
        [XmlElement("password")]
        public string Password { get; set; }
        [XmlElement("database")]
        public string Database { get; set; }
        [XmlElement("userintegratedsecurity")]
        public bool UserIntegratedSecurity { get; set; }


        public ConnectionParameters(string name, EConnectionDriverTypes connectionDriverType, string host, string port, string database, string user, string password)
        {
            Name = name;
            ConnectionDriverType = connectionDriverType;
            Host = host;
            Port = port;
            Database = database;
            User = user;
            Password = password;
        }

        public ConnectionParameters(string name, EConnectionDriverTypes connectionDriverType, string host, string port, string database, bool userIntegratedSecurity, string user, string password)
        {
            Name = name;
            ConnectionDriverType = connectionDriverType;
            Host = host;
            Port = port;
            Database = database;
            UserIntegratedSecurity = userIntegratedSecurity;
            User = user;
            Password = password;
        }

        public ConnectionParameters()
        {
            Name = "Default";
            ConnectionDriverType = EConnectionDriverTypes.ecdtNone;
            Host = "";
            Port = "";
            User = "";
            Password = "";
            Database = "";
        }
    }
}
