using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e3net.Common.Entity
{
    /// <summary>
    /// 天气数据
    /// </summary>
    public class GaoDeWeatherModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 天气描述
        /// </summary>
        public string Weather { get; set; }
        /// <summary>
        /// 风向
        /// </summary>
        public string WindDirection { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public string Temperature { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 空气湿度
        /// </summary>
        public string Humidity { get; set; }

        /// <summary>
        /// 气候代码
        /// </summary>
        public int Code { get; set; }
    }
}
