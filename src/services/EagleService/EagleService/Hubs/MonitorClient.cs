using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleService.Hubs
{
    public interface MonitorClient
    {
        Task CPUUsedReceived(DateTime time, string unit, double value);

        Task CPUTemperatureReceived(DateTime time, string unit, double value);

        Task MemoryUsedReceived(DateTime time, string unit, double value);

        Task DiskUsedReceived(string unit, double used, double free);

        Task Rocker(int key, int value);
    }
}
