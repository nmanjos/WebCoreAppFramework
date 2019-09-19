using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreLogger
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var header = context.Request.Headers["X-CorrelationId"];
            string correlationId;
            if (header.Count > 0)
            {
                correlationId = header[0];
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
            }

            context.Items["X-CorrelationId"] = correlationId;

            var logger = context.RequestServices.GetRequiredService<ILogger<LogHeaderMiddleware>>();
            using (logger.BeginScope("{@SessionId}", correlationId))
            {
                await this._next(context);
            }
        }
    }
}
