using System;
using System.Collections.Generic;
using System.Text;

namespace EagleClient.Infrastructure.Services.Device
{
    public class DiskInfo
    {
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 已使用
        /// </summary>
        public double Used { get; set; }

        /// <summary>
        /// 可使用空间
        /// </summary>
        public double Free { get; set; }
    }
}
