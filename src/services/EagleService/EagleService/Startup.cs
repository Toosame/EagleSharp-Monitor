using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EagleService.HostedServices;
using EagleService.Hubs;
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
            services.AddHostedService<MockMonitorHostedService>();

            services.AddCors(setup =>
            {
                setup.AddDefaultPolicy(
                    new CorsPolicyBuilder()
                    .AllowAnyOrigin()
                    .Build());
            });

            services.AddSignalR();
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
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();

                    logger.LogInformation("Clinet: {ClientIp} start play video streaming...",
                        context.Connection.RemoteIpAddress);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "multipart/x-mixed-replace; boundary=frame";

                    var broadcast = context.RequestServices.GetRequiredService<VideoBroadcast>();

                    broadcast.Subscribe(context.Connection.Id, async frame =>
                    {
                        await context.Response.BodyWriter.WriteAsync(new ReadOnlyMemory<byte>(frame));
                    });

                    logger.LogInformation("Clinet: {ClientIp} subscribed video streaming.",
                        context.Connection.RemoteIpAddress);

                    context.RequestAborted.WaitHandle.WaitOne();

                    logger.LogInformation("Clinet: {ClientIp} was lost, ready unsubscribe...",
                        context.Connection.RemoteIpAddress);

                    broadcast.Unsubscribe(context.Connection.Id);
                }
                else
                    await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MonitorHub>("/monitor");
            });
        }
    }
}
