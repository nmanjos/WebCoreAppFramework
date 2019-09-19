using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLogger
{
    public class LogEntry
    {
        public LogEntry()
        {
            TimeStampUtc = DateTime.UtcNow;
            UserName = Environment.UserName;
        }

        
        static public readonly string StaticHostName = System.Net.Dns.GetHostName();
        public string CorrelationId { get; set; }
        public string UserName { get; private set; }
        public string HostName { get { return StaticHostName; } }
        public DateTime TimeStampUtc { get; private set; }
        public string Category { get; set; }
        public LogLevel Level { get; set; }
        public string Text { get; set; }
        public Exception Exception { get; set; }
        public EventId EventId { get; set; }
        public object State { get; set; }
        public string StateText { get; set; }
        public Dictionary<string, object> StateProperties { get; set; }
        public List<LogScopeInfo> Scopes { get; set; }
    }
}
