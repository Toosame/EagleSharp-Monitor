using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EagleClient.Infrastructure.Services.Monitor
{
    public class SignalRMonitorServer : IMonitorServer
    {
        private readonly System.Threading.SemaphoreSlim _semaphore;
        private readonly ILogger<SignalRMonitorServer> _logger;
        private readonly SignalROption _options;
        private readonly HubConnection _connection;

        private IDisposable onUpdateRocker;

        public SignalRMonitorServer(IOptions<SignalROption> options,
            ILogger<SignalRMonitorServer> logger)
        {
            _semaphore = new System.Threading.SemaphoreSlim(1);
            _logger = logger;
            _options = options.Value;
            _connection = new HubConnectionBuilder()
                .WithUrl(_options.ServerUrl)
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task OnUpdateRocker(Action<int, int> callback)
        {
            await EnsureConnectionStared();

            onUpdateRocker = _connection.On("Rocker", callback);
        }

        public async Task UpdateCPUTemperatureAsync(DateTime time, string unit, double value)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateCPUTemperature", time, unit, value);
        }

        public async Task UpdateCPUUsedAsync(DateTime time, string unit, double value)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateCPUUsed", time, unit, value);
        }

        public async Task UpdateDiskUsedAsync(string unit, double used, double free)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateDiskUsed", unit, used, free);
        }

        public async Task UpdateMemoryUsedAsync(DateTime time, string unit, double value)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateMemoryUsed", time, unit, value);
        }

        private async Task EnsureConnectionStared()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _semaphore.WaitAsync();

                _logger.LogInformation("Ready connection to {ServerUrl}.", _options.ServerUrl);

                await _connection.StartAsync();

                _logger.LogInformation("Connectioned to EagleService.");

                _semaphore.Release();
            }
        }

        public void Dispose()
        {
            onUpdateRocker?.Dispose();
        }
    }
}
