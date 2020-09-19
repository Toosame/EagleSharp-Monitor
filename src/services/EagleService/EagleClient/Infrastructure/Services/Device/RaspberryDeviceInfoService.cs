using EagleClient.Infrastructure.Exceptions;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleClient.Infrastructure.Services.Device
{
    public class RaspberryDeviceInfoService : IDeviceInfoService
    {
        public async Task<CPUInfo> GetCPUInfoAsync()
        {
            IEnumerable<string> procStat = await File.ReadAllLinesAsync("/proc/stat");
            if (procStat == default || !procStat.Any())
                throw new ReadDeviceInfoException("Cannt get CPU info.");

            string temp = await File.ReadAllTextAsync("/sys/class/thermal/thermal_zone0/temp");
            if (string.IsNullOrWhiteSpace(temp))
                throw new ReadDeviceInfoException("Cannt get CPU temp.");

            (double user1, long nice1, long system1, long idle1, long iowait1, long irq1, long softirq1, long stealstolen1, long guest1) = await GetStatAsync();
            await Task.Delay(100);
            (double user2, long nice2, long system2, long idle2, long iowait2, long irq2, long softirq2, long stealstolen2, long guest2) = await GetStatAsync();

            double od = user1 + nice1 + system1 + idle1 + iowait1 + irq1 + softirq1 + stealstolen1 + guest1;
            double nd = user2 + nice2 + system2 + idle2 + iowait2 + irq2 + softirq2 + stealstolen2 + guest2;

            double total = nd - od;
            double idle = idle2 - idle1;

            return new CPUInfo
            {
                TempUnit = "°C",
                Temp = Convert.ToDouble(temp) / 1000.0,
                UsedUnit = "%",
                Used = Math.Round(100 * (total - idle) / total, 2)
            };
        }

        public Task<DiskInfo> GetDiskInfoAsync()
        {
            var rootInfo = DriveInfo.GetDrives().First(p => p.Name == "/");

            return Task.FromResult(new DiskInfo()
            {
                Free = rootInfo.AvailableFreeSpace / (1024d * 1024d * 1024d),
                Used = rootInfo.TotalSize / (1024d * 1024d * 1024d),
                Unit = "GB"
            });
        }

        public async Task<MemoryInfo> GetMemoryInfoAsync()
        {
            IEnumerable<string> procStat = await File.ReadAllLinesAsync("/proc/meminfo");
            if (procStat == default || !procStat.Any())
                throw new ReadDeviceInfoException("Cannt get memory info.");

            string[] memTotalStr = procStat.ElementAt(0).Split(':');
            double memTotal = Convert.ToInt64(memTotalStr[1].Substring(0, memTotalStr[1].IndexOf('k')).Trim()) / 1024;

            string[] memFreeStr = procStat.ElementAt(1).Split(':');
            double memFree = Convert.ToInt64(memFreeStr[1].Substring(0, memFreeStr[1].IndexOf('k')).Trim()) / 1024;

            string[] buffersStr = procStat.ElementAt(3).Split(':');
            double buffers = Convert.ToInt64(buffersStr[1].Substring(0, buffersStr[1].IndexOf('k')).Trim()) / 1024;

            string[] cachedStr = procStat.ElementAt(4).Split(':');
            double cached = Convert.ToInt64(cachedStr[1].Substring(0, cachedStr[1].IndexOf('k')).Trim()) / 1024;

            return new MemoryInfo()
            {
                Unit = "MB",
                Total = memTotal,
                Used = memTotal - memFree - buffers - cached
            };
        }

        private async Task<(double, long, long, long, long, long, long, long, long)> GetStatAsync()
        {
            IEnumerable<string> procStat = await File.ReadAllLinesAsync("/proc/stat");
            if (procStat == default || !procStat.Any())
                throw new ReadDeviceInfoException("Cannt get CPU info.");

            string cpu = procStat.First();
            string[] cpuCol = cpu.Split(' ');

            double user = Convert.ToDouble(cpuCol[2]);
            int nice = Convert.ToInt32(cpuCol[3]);
            int system = Convert.ToInt32(cpuCol[4]);
            int idle = Convert.ToInt32(cpuCol[5]);
            int iowait = Convert.ToInt32(cpuCol[6]);
            int irq = Convert.ToInt32(cpuCol[7]);
            int softirq = Convert.ToInt32(cpuCol[8]);
            int stealstolen = Convert.ToInt32(cpuCol[9]);
            int guest = Convert.ToInt32(cpuCol[10]);

            return (user, nice, system, idle, iowait, irq, softirq, stealstolen, guest);
        }
    }
}
