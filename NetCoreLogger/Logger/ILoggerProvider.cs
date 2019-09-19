using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging
{
    public interface ILoggerProvider : IDisposable
    {
        ILogger CreateLogger(string categoryName);
    }
}
