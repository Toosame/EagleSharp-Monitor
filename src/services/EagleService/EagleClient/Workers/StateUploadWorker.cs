using EagleClient.Infrastructure.Services;
using EagleClient.Infrastructure.Services.Monitor;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Swan;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EagleClient.Workers
{
    public class StateUploadWorker : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StateUploadWorker> _logger;

        private Timer timer;

        public StateUploadWorker(IServiceProvider serviceProvider, ILogger<StateUploadWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("State Upload Hosted Service running.");

            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var monitorServer = _serviceProvider
                .GetRequiredService<IMonitorServer>();

            var deviceInfoService = _serviceProvider
                .GetRequiredService<IDeviceInfoService>();

            var time = DateTime.UtcNow;
            //Get CPU info
            var cpuInfo = await deviceInfoService.GetCPUInfoAsync();

            await monitorServer.UpdateCPUUsedAsync(time, cpuInfo.UsedUnit, cpuInfo.Used);
            await monitorServer.UpdateCPUTemperatureAsync(time, cpuInfo.TempUnit, cpuInfo.Temp);

            //_logger.LogInformation("Got cpu used info: {CPUUsed}{CPUUsedUnit}",
            //   cpuInfo.Used, cpuInfo.UsedUnit);

            //Get memory info
            var memInfo = await deviceInfoService.GetMemoryInfoAsync();
            await monitorServer.UpdateMemoryUsedAsync(time, "%", Math.Round((memInfo.Used / memInfo.Total) * 100, 2));

            //_logger.LogInformation("Got mem info: {MemUsed}{MemTotalUnit} / {MemTotal}{MemTotalUnit}",
            //   memInfo.Used, memInfo.Unit, memInfo.Total, memInfo.Unit);

            //Get disk info
            if (time.Minute % 20 == 0)
            {
                var diskInfo = await deviceInfoService.GetDiskInfoAsync();
                await monitorServer.UpdateDiskUsedAsync(diskInfo.Unit, diskInfo.Used, diskInfo.Free);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("State Upload Hosted Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
