using EagleClient.Infrastructure.Services.Device;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EagleClient.Infrastructure.Services
{
    public interface IDeviceInfoService
    {
        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <returns></returns>
        Task<CPUInfo> GetCPUInfoAsync();

        /// <summary>
        /// 获取内存使用率
        /// </summary>
        /// <returns></returns>
        Task<MemoryInfo> GetMemoryInfoAsync();

        /// <summary>
        /// 获取磁盘信息
        /// </summary>
        /// <returns></returns>
        Task<DiskInfo> GetDiskInfoAsync();
    }
}
