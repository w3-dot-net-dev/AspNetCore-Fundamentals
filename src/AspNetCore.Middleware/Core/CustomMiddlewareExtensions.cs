namespace AspNetCore.Middleware.Core
{
    using Microsoft.AspNetCore.Builder;

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddleware>();
        }
    }
}
