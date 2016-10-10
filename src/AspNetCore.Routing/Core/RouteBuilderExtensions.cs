namespace AspNetCore.Routing.Core
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;

    public static class RouteBuilderExtensions
    {
        private static IInlineConstraintResolver GetRequiredService(this IRouteBuilder routeBuilder)
        {
            return routeBuilder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>();
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> for the given <paramref name="handler"/>, and
        /// <paramref name="template"/>.
        /// </summary>
        /// <param name="routeBuilder">The <see cref="IRouteBuilder"/> to add the route to.</param>
        /// <param name="handler">The <see cref="RequestDelegate"/> route handler.</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="template">The URL pattern of the route.</param>
        /// <param name="defaults">
        /// An object that contains default values for route parameters. The object's properties represent the names
        /// and values of the default values.
        /// </param>
        /// <param name="constraints">
        /// An object that contains constraints for the route. The object's properties represent the names and values
        /// of the constraints.
        /// </param>
        /// <param name="dataTokens">
        /// An object that contains data tokens for the route. The object's properties represent the names and values
        /// of the data tokens.
        /// </param>
        /// <returns>A reference to the <paramref name="routeBuilder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapRouteWithHandler(
            this IRouteBuilder routeBuilder,
            RequestDelegate handler,
            string name,
            string template,
            object defaults = null,
            object constraints = null,
            object dataTokens = null)
        {
            return routeBuilder.MapRouteWithHandler(new RouteHandler(handler), name, template, defaults, constraints, dataTokens);
        }

        /// <summary>
        /// Adds a route to the <see cref="IRouteBuilder"/> for the given <paramref name="target"/>, and
        /// <paramref name="template"/>.
        /// </summary>
        /// <param name="routeBuilder">The <see cref="IRouteBuilder"/> to add the route to.</param>
        /// <param name="target">The <see cref="IRouter"/> route handler.</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="template">The URL pattern of the route.</param>
        /// <param name="defaults">
        /// An object that contains default values for route parameters. The object's properties represent the names
        /// and values of the default values.
        /// </param>
        /// <param name="constraints">
        /// An object that contains constraints for the route. The object's properties represent the names and values
        /// of the constraints.
        /// </param>
        /// <param name="dataTokens">
        /// An object that contains data tokens for the route. The object's properties represent the names and values
        /// of the data tokens.
        /// </param>
        /// <returns>A reference to the <paramref name="routeBuilder"/> after this operation has completed.</returns>
        public static IRouteBuilder MapRouteWithHandler(
            this IRouteBuilder routeBuilder,
            IRouter target,
            string name,
            string template,
            object defaults = null,
            object constraints = null,
            object dataTokens = null)
        {
            routeBuilder.Routes.Add(new Route(
                target,
                name,
                template,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new RouteValueDictionary(dataTokens),
                routeBuilder.GetRequiredService()));

            return routeBuilder;
        }
    }
}