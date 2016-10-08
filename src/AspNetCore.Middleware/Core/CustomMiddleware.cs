namespace AspNetCore.Middleware.Core
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        private ILogger _logger;

        public CustomMiddleware(RequestDelegate next, IServiceProvider locator)
        {
            _next = next;
            _logger = locator.GetService<ILogger>();
        }

        public async Task Invoke(HttpContext context)
        {
            //_logger.LogInformation("Handling request: " + context.Request.Path);
            await context.Response.WriteAsync("Custom Middleware");
            await _next.Invoke(context);
            //_logger.LogInformation("Finished handling request.");
        }
    }
}
