using Conv.ORM.Connections.Enums;
using Conv.ORM.Connections.Parameters;
using Conv.ORM.Logging;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Conv.ORM.Connections
{
    internal class ConnectionsParametersFile
    {
        private ConnectionsParameters Connections;
        private bool HasConnections;
        internal ConnectionsParametersFile()
        {
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            path += @"\connection.xml";

            //Log in debug mode;
            #if DEBUG
            LoggerKepper.Log(LoggerType.ltInformation, "ConnectionsParametersFile", $"File path: {path}");
            #endif

            StreamReader xmlFile;

           
            try
            {
                if (!File.Exists(path))
                {
                    HasConnections = false;
                    throw new Exception("Connection file not found.");
                }
                xmlFile = new StreamReader(path);
                LoadConnectionParametersFromFile(xmlFile);

            }
            catch (Exception e)
            {
                #if DEBUG
                LoggerKepper.Log(LoggerType.ltError, "ConnectionsParametersFile", $"Error in open the connection file: {e.Message}");
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
                HasConnections = (Connections is not null) && (Connections.Connections.Count > 0);
            }
            catch (InvalidOperationException ex)
            {
                #if DEBUG
                    LoggerKepper.Log(LoggerType.ltError, "ConnectionsParametersFile", $"Error in open the connection file: {ex.Message}");
                #endif
            }
            finally
            {
                xmlFile.Close();
            }
        }

        internal ConnectionParameters GetFirstConnectionParameter()
        {
            return HasConnections ? Connections.Connections[0] : null;
        }

        internal ConnectionParameters GetConnectionParameters(string name)
        {
            return HasConnections ? LocateConnectionParameters(name) : null;
        }

        internal ConnectionParameters GetConnectionParameters(EConnectionDriverTypes type)
        {
            return HasConnections ? LocateConnectionParameters(type) : null;
        }

        private ConnectionParameters LocateConnectionParameters(string name)
        {
            return HasConnections ? Connections.Connections.FirstOrDefault(parameters => parameters.Name == name) : null;
        }

        private ConnectionParameters LocateConnectionParameters(EConnectionDriverTypes type)
        {
            return HasConnections ? Connections.Connections.FirstOrDefault(parameters => parameters.ConnectionDriverType == type) : null;
        }
    }
}
