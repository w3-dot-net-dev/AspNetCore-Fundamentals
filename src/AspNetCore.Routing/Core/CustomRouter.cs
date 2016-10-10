namespace AspNetCore.Routing.Core
{
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Routing;
    using System.Threading.Tasks;

    public class CustomRouter : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public Task RouteAsync(RouteContext context)
        {
            return TaskCache.CompletedTask;
        }
    }
}
