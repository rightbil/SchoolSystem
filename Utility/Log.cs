using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public sealed class Log : ILog
    {
        private Log() { }
        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());

        public static Log GetInstance
        {
            get { return instance.Value; }
        }

        public void SchoolSystemExceptions(string message)
        {
            var fileName = string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToShortDateString());
            var logFilePath = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("-------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (var writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}
