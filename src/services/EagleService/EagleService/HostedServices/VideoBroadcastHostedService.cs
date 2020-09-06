using EagleService.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EagleService.HostedServices
{
    public class VideoBroadcastHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public VideoBroadcastHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var frameBroadcast = _serviceProvider.GetRequiredService<VideoBroadcast>();

            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                    frameBroadcast.BroadcastFrame();

            }, stoppingToken);
        }
    }
}
