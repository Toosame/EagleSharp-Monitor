using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EagleClient.Infrastructure.Services;
using EagleClient.Infrastructure.Services.Device;
using EagleClient.Infrastructure.Services.Monitor;
using EagleClient.Workers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EagleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions<SignalROption>()
                    .Bind(hostContext.Configuration.GetSection("EagleServer"));

                    services.AddSingleton<IMonitorServer, SignalRMonitorServer>();
                    services.AddTransient<IDeviceInfoService, RaspberryDeviceInfoService>();

                    services.AddHostedService<RockerWorker>();
                    services.AddHostedService<StateUploadWorker>();
                    //services.AddHostedService<VideoStreamingWorker>();
                });
    }
}
