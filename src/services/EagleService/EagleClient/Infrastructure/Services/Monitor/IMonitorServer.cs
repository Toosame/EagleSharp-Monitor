using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EagleClient.Infrastructure.Services.Monitor
{
    public interface IMonitorServer : IDisposable
    {
        /// <summary>
        /// 更新CPU使用率
        /// </summary>
        /// <param name="time"></param>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task UpdateCPUUsedAsync(DateTime time, string unit, double value);

        /// <summary>
        /// 更新CPU温度
        /// </summary>
        /// <param name="time"></param>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task UpdateCPUTemperatureAsync(DateTime time, string unit, double value);

        /// <summary>
        /// 更新内存使用率
        /// </summary>
        /// <param name="time"></param>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task UpdateMemoryUsedAsync(DateTime time, string unit, double value);

        /// <summary>
        /// 更新磁盘使用率
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="used"></param>
        /// <param name="free"></param>
        /// <returns></returns>
        Task UpdateDiskUsedAsync(string unit, double used, double free);

        /// <summary>
        /// 当更新摇杆时调用
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        Task OnUpdateRocker(Action<int, int> callback);
    }
}
