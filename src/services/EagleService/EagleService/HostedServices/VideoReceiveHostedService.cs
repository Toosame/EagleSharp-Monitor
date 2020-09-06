using EagleService.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EagleService.HostedServices
{
    public class VideoReceiveHostedService : BackgroundService
    {
        private VideoReceiver videoReceiver;
        private VideoBroadcast videoBroadcast;

        private readonly IServiceProvider _serviceProvider;

        public VideoReceiveHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            videoBroadcast = _serviceProvider
                .GetRequiredService<VideoBroadcast>();
            videoReceiver = new VideoReceiver(5004,
                _serviceProvider.GetRequiredService<ILogger<VideoReceiver>>());

            videoReceiver.OnMessageReceived += VideoStreaming_OnMessageReceived;

            await videoReceiver.Start(stoppingToken);
        }

        private void VideoStreaming_OnMessageReceived(byte[] frame, IPEndPoint clientIpEndPoint)
        {
            videoBroadcast.ReveiveFrame(frame);
        }
    }
}
