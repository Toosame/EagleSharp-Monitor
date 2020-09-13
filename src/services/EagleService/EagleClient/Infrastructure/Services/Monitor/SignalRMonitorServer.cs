using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EagleClient.Infrastructure.Services.Monitor
{
    public class SignalRMonitorServer : IMonitorServer
    {
        private object locker = new object();
        System.Threading.SemaphoreSlim semaphore;

        private readonly SignalROption _options;
        private readonly HubConnection _connection;

        private IDisposable onUpdateRocker;

        public SignalRMonitorServer(IOptions<SignalROption> options)
        {
            semaphore = new System.Threading.SemaphoreSlim(1);
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

            await _connection.SendAsync("UpdateCPUTemperature");
        }

        public async Task UpdateCPUUsedAsync(DateTime time, string unit, double value)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateCPUUsed");
        }

        public async Task UpdateDiskUsedAsync(string unit, double used, double free)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateDiskUsed");
        }

        public async Task UpdateMemoryUsedAsync(DateTime time, string unit, double value)
        {
            await EnsureConnectionStared();

            await _connection.SendAsync("UpdateMemoryUsed");
        }

        private async Task EnsureConnectionStared()
        {
            await semaphore.WaitAsync();

            if (_connection.State == HubConnectionState.Disconnected)
            {
                await _connection.StartAsync();

                semaphore.Release();
            }
        }

        public void Dispose()
        {
            onUpdateRocker?.Dispose();
        }
    }
}
