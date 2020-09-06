using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EagleService.HostedServices;
using EagleService.Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EagleService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<VideoBroadcast>();

            services.AddHostedService<VideoBroadcastHostedService>();
            services.AddHostedService<VideoReceiveHostedService>();

            services.AddCors(setup =>
            {
                setup.AddDefaultPolicy(
                    new CorsPolicyBuilder()
                    .AllowAnyOrigin()
                    .Build());
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/videoStreaming"))
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "multipart/x-mixed-replace; boundary=frame";

                    var broadcast = context.RequestServices.GetRequiredService<VideoBroadcast>();

                    broadcast.Subscribe(context.Connection.Id, async frame =>
                    {
                        await context.Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(frame));
                    });

                    context.RequestAborted.WaitHandle.WaitOne();

                    broadcast.Unsubscribe(context.Connection.Id);
                }
                else
                    await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
