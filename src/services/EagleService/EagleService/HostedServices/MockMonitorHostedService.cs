using EagleService.Hubs;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EagleService.HostedServices
{
    public class MockMonitorHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MockMonitorHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var hub = _serviceProvider.GetRequiredService<IHubContext<MonitorHub>>();

            while (!stoppingToken.IsCancellationRequested)
            {
                Random random = new Random();

                await hub.Clients.All.SendAsync(nameof(MonitorClient.CPUUsedReceived), DateTime.UtcNow, "%", random.Next(1, 100));

                await hub.Clients.All.SendAsync(nameof(MonitorClient.CPUTemperatureReceived), DateTime.UtcNow, "°C", random.Next(20, 60));

                await hub.Clients.All.SendAsync(nameof(MonitorClient.MemoryUsedReceived), DateTime.UtcNow, "%", random.Next(1, 80));

                int used = random.Next(5, 60);

                await hub.Clients.All.SendAsync(nameof(MonitorClient.DiskUsedReceived), "%", used, 100 - used);

                await Task.Delay(1000);
            }
        }
    }
}
