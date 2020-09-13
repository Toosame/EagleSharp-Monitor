using EagleClient.Infrastructure.Services.Monitor;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Toosame.RaspberryIO.Peripherals;

namespace EagleClient.Workers
{
    public class RockerWorker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RockerWorker> _logger;
        private readonly PCA9685 _panTiltHat;

        private int rightToLeft = 95;
        private int upToDown = 120;

        public RockerWorker(IServiceProvider serviceProvider,
            ILogger<RockerWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _panTiltHat = new PCA9685();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _panTiltHat.SetRotationAngle(0, rightToLeft);
            _panTiltHat.SetRotationAngle(1, upToDown);

            var monitorServer = _serviceProvider
               .GetRequiredService<IMonitorServer>();

            monitorServer.OnUpdateRocker(OnRockerAction);
            cancellationToken.Register(() => monitorServer.Dispose());

            _logger.LogInformation("Start rocker worker");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop rocker worker");

            return Task.CompletedTask;
        }

        private void OnRockerAction(int key, int value)
        {
            if (key == 38)//ConsoleKey.UpArrow
            {
                int upToDownAfter = upToDown - value;
                if (upToDownAfter <= 180 && upToDownAfter >= 70)
                {
                    _panTiltHat.SetRotationAngle(1, upToDownAfter);
                    upToDown = upToDownAfter;
                }
            }
            else if (key == 40)//ConsoleKey.DownArrow
            {
                int upToDownAfter = upToDown + value;
                if (upToDownAfter <= 180 && upToDownAfter >= 70)
                {
                    _panTiltHat.SetRotationAngle(1, upToDownAfter);
                    upToDown = upToDownAfter;
                }
            }
            else if (key == 39)//ConsoleKey.RightArrow
            {
                int rightToLeftAfter = rightToLeft + value;
                if (rightToLeftAfter <= 180 && rightToLeftAfter >= 0)
                {
                    _panTiltHat.SetRotationAngle(0, rightToLeft);
                    rightToLeft = rightToLeftAfter;
                }
            }
            else if (key == 37)//ConsoleKey.LeftArrow
            {
                int rightToLeftAfter = rightToLeft - value;
                if (rightToLeftAfter <= 180 && rightToLeftAfter >= 0)
                {
                    _panTiltHat.SetRotationAngle(0, rightToLeft);
                    rightToLeft = rightToLeftAfter;
                }
            }
        }
    }
}
