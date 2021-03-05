using System.Diagnostics;

namespace Conv.ORM.Logging
{
    class LoggerKepper
    { 
        public static void Log(LoggerType loggerType, string className, string text)
        {
            string type = loggerType switch
            {
                LoggerType.ltInformation => "Info",
                LoggerType.ltError => "Error",
                LoggerType.ltWarning => "Warning",
                LoggerType.ltDebug => "Debug",
                _ => "Log",
            };
            Debug.WriteLine($"{className}: {type} --> {text}");
        }
    }
}
