namespace AspNetCore.Middleware
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;

    public class Startup : IStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            IHostingEnvironment env = app.ApplicationServices.GetService<IHostingEnvironment>();
            ILoggerFactory loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(context =>
                {
                    return context.Request.Query.ContainsKey("branch");
                },
                branchApp =>
                {
                    branchApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("branch");
                    });
                }
            );

            app.Map(new PathString("/level1"), level1App =>
            {
                level1App.Map(new PathString("/base"), baseApp =>
                {
                    level1App.Use(async (context, next) =>
                    {
                        await context.Response.WriteAsync("/level1/base");
                        await next.Invoke();
                    });
                });

                level1App.Map(new PathString("/level2a"), level2aApp =>
                {
                    level2aApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("/level1/level2a");
                    });
                });

                level1App.Map(new PathString("/level2b"), level2bApp =>
                {
                    level2bApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("/level1/level2b");
                    });
                });

                level1App.Map(new PathString("/level2c"), level2cApp =>
                {
                    level2cApp.Run(async context =>
                    {
                        await context.Response.WriteAsync("/level1/level2c");
                    });
                });

            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
