using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreLogger
{
    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
    }
}
