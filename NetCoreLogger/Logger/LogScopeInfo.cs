using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLogger
{
    public class LogScopeInfo
    {
        public LogScopeInfo()
        {
        }

        public string Text { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }
}
