using System;
using System.Collections.Generic;
using System.Text;

namespace EagleClient.Infrastructure.Services.Device
{
    public class MemoryInfo
    {
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 已使用内存
        /// </summary>
        public double Used { get; set; }

        /// <summary>
        /// 总使用内存
        /// </summary>
        public double Total { get; set; }
    }
}
