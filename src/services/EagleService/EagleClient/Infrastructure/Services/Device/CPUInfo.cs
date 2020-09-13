using System;
using System.Collections.Generic;
using System.Text;

namespace EagleClient.Infrastructure.Services.Device
{
    public class CPUInfo
    {
        /// <summary>
        /// CPU使用率单位
        /// </summary>
        public string UsedUnit { get; set; }

        /// <summary>
        /// CPU使用率
        /// </summary>
        public double Used { get; set; }

        /// <summary>
        /// CPU温度单位
        /// </summary>
        public string TempUnit { get; set; }

        /// <summary>
        /// CPU温度
        /// </summary>
        public double Temp { get; set; }
    }
}
