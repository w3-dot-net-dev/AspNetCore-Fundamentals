namespace AspNetCore.Routing
{
    using Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class Startup : IStartup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //services.AddRouting();

            services.AddMvc();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            IServiceProvider locator = app.ApplicationServices;

            IHostingEnvironment env = locator.GetService<IHostingEnvironment>();
            ILoggerFactory loggerFactory = locator.GetService<ILoggerFactory>();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routeBuilder =>
            {
                //routeBuilder.MapRouteWithHandler(
                //    handler: Handler,
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}",
                //    defaults: null,
                //    constraints: null,
                //    dataTokens: null
                //);

                //routeBuilder.MapRouteWithHandler(
                //    target: new CustomRouter(),
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}",
                //    defaults: null,
                //    constraints: null,
                //    dataTokens: null
                //);

                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        private Task Handler(HttpContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}
