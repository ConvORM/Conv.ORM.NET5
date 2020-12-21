using Conv.ORM.Connection.Enums;
using Conv.ORM.Connection.Parameters;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Conv.ORM.Connection
{
    internal class ConnectionsParametersFile
    {
        private ConnectionsParameters Connections;
        internal ConnectionsParametersFile()
        {
            var path = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName).FullName;
            path += @"\connection.xml";

            //Log in debug mode;
            #if DEBUG
                Console.WriteLine("File path: " + path);
            #endif

            StreamReader xmlFile;

            try
            {
                xmlFile = new StreamReader(path);
                LoadConnectionParametersFromFile(xmlFile);

            }
            catch (Exception e)
            {
                #if DEBUG
                    Console.WriteLine("Error in open the connection file: " + e.Message);
                #endif

                xmlFile = null;
            }
        }

        private void LoadConnectionParametersFromFile(StreamReader xmlFile)
        {
            var serializer = new XmlSerializer(typeof(ConnectionsParameters));

            try
            {
                Connections = serializer.Deserialize(xmlFile) as ConnectionsParameters;
            }
            catch (InvalidOperationException ex)
            {
                #if DEBUG
                    Console.WriteLine("Error in open the connection file: " + ex.Message);
                #endif
            }
            finally
            {
                xmlFile.Close();
            }
        }

        internal ConnectionParameters GetFirstConnectionParameter()
        {
            return Connections.Connections.Count == 0 ? null : Connections.Connections[0];
        }

        internal ConnectionParameters GetConnectionParameters(string name)
        {
            return Connections.Connections.Count == 0 ? null : LocateConnectionParameters(name);
        }

        internal ConnectionParameters GetConnectionParameters(EConnectionDriverTypes type)
        {
            return Connections.Connections.Count == 0 ? null : LocateConnectionParameters(type);
        }

        private ConnectionParameters LocateConnectionParameters(string name)
        {
            return Connections.Connections.FirstOrDefault(parameters => parameters.Name == name);
        }

        private ConnectionParameters LocateConnectionParameters(EConnectionDriverTypes type)
        {
            return Connections.Connections.FirstOrDefault(parameters => parameters.ConnectionDriverType == type);
        }
    }
}
