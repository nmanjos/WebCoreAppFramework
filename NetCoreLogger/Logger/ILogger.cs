using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Logging
{ 
public interface ILogger
    {
        IDisposable BeginScope<TState>(TState state);
        bool IsEnabled(LogLevel logLevel);
        void Log<TState>(LogLevel logLevel, EventId eventId,
             TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}

