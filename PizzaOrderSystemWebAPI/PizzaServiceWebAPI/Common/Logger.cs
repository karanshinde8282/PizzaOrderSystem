using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrderSystemWebAPI.Common
{
    public class Logger
    {
        public const string LOG_FORMAT = "{1}Exception Type\t:\t{0}{1}Message\t\t:\t{3}";
        private const string LOG_DIRECTORY = "Logs";
        private const string DATE_FORMATE = "{0}_{1}_{2}.txt";
        private const string INFO = "\\Info_";
        private const string ERROR = "\\Error_";

        public Logger()
        {

        }

        public static void WriteLog(LogType type, Object exceptionObject)
        {
            try
            {
                string folderPath = AppContext.BaseDirectory;
                string logMessage = exceptionObject == null ? string.Empty : exceptionObject.ToString();
                string formatedMessage = string.Format(LOG_FORMAT,
                                         Enum.GetName(typeof(LogType), type),
                                         Environment.NewLine,
                                         DateTime.Now.ToString(),
                                         logMessage,
                                         "=======================================");

                string dateExtention = string.Format(DATE_FORMATE, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                if (!Directory.Exists(Path.Combine(folderPath, LOG_DIRECTORY)))
                {
                    Directory.CreateDirectory(Path.Combine(folderPath, LOG_DIRECTORY));
                }
                string logFilePath = folderPath + LOG_DIRECTORY + (type == 0 ? INFO : ERROR) + dateExtention;

                File.AppendAllText(logFilePath, formatedMessage);
            }
            catch
            {
            }
        }

    }

    public enum LogType
    {
        Info = 0,
        Error = 1,
        Exception = 2
    }
}
