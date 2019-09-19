using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreLogger
{
    public class DefaultCorrelationIdAccessor : ICorrelationIdAccessor
    {
        private readonly ILogger<DefaultCorrelationIdAccessor> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultCorrelationIdAccessor(ILogger<DefaultCorrelationIdAccessor> logger, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessor;
        }

        public string GetCorrelationId()
        {
            try
            {
                var context = this._httpContextAccessor.HttpContext;
                var result = context?.Items["X-CorrelationId"] as string;

                return result;
            }
            catch (Exception exception)
            {
                this._logger.LogWarning(exception, "Unable to get original session id header");
            }

            return string.Empty;
        }
    }
}