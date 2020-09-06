using Microsoft.AspNetCore.SignalR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleService.Hubs
{
    public class MonitorHub : Hub<MonitorClient>
    {
        /// <summary>
        /// 更新CPU使用率
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCPUUsed(DateTime time, string unit, double value)
        {
            await Clients.All.CPUUsedReceived(time, unit, value);
        }

        public async Task UpdateCPUTemperature(DateTime time, string unit, double value)
        {
            await Clients.All.CPUTemperatureReceived(time, unit, value);
        }

        public async Task UpdateMemoryUsed(DateTime time, string unit, double value)
        {
            await Clients.All.MemoryUsedReceived(time, unit, value);
        }

        public async Task UpdateDiskUsed(string unit, double used, double free)
        {
            await Clients.All.DiskUsedReceived(unit, used, free);
        }

        /// <summary>
        /// 操作摇杆控制云台
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task Rocker(int key, int value)
        {
            await Clients.All.Rocker(key, value);
        }
    }
}
