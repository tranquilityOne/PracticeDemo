using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e3net.Common
{
    /// <summary>
    /// 天气处理类
    /// </summary>
    public class WeatherHelper
    {
        public static Dictionary<int, string> dicWeather = new Dictionary<int, string>();

        /// <summary>
        /// 初始化数据
        /// </summary>
        private static void Init()
        {
            dicWeather.Add(0,"晴");
            dicWeather.Add(1,"多云");
            dicWeather.Add(2, "阴");
            dicWeather.Add(3, "阵雨");
            dicWeather.Add(4, "雷阵雨");
            dicWeather.Add(5, "雷阵雨并伴有冰雹");
            dicWeather.Add(6, "雨夹雪");
            dicWeather.Add(7, "小雨");
            dicWeather.Add(8, "中雨");
            dicWeather.Add(9, "大雨");
            dicWeather.Add(10, "暴雨");
            dicWeather.Add(11, "大暴雨");
            dicWeather.Add(12, "特大暴雨");
            dicWeather.Add(13, "阵雨");
            dicWeather.Add(14, "小雪");
            dicWeather.Add(15, "中雪");
            dicWeather.Add(16, "大雪");
            dicWeather.Add(17, "暴雪");
            dicWeather.Add(18, "雾");
            dicWeather.Add(19, "冻雨");
            dicWeather.Add(20, "沙尘暴");
            dicWeather.Add(21, "小雨-中雨");
            dicWeather.Add(22, "中雨-大雨");
            dicWeather.Add(23, "大雨-暴雨");
            dicWeather.Add(24, "暴雨-大暴雨");
            dicWeather.Add(25, "暴雨-特大暴雨");
            dicWeather.Add(26, "小雪-中雪");
            dicWeather.Add(27, "中雪-大雪");
            dicWeather.Add(28, "大雪-暴雪");
            dicWeather.Add(29,"浮尘");
            dicWeather.Add(30,"扬沙");
            dicWeather.Add(31,"强沙尘暴");
            dicWeather.Add(32,"飓风");
            dicWeather.Add(33,"龙卷风");
            dicWeather.Add(34, "高吹雪");
            dicWeather.Add(35,"轻雾");
            dicWeather.Add(36, "霾");
        }

        /// <summary>
        /// 获取自定义天气自定义代码
        /// </summary>
        /// <param name="weather">天气描述</param>
        /// <returns></returns>
        public static int GetWeatherCode(string weather)
        {
            if (dicWeather.Count == 0)
            {
                Init();
            }
            foreach (var item in dicWeather)
            {
                if (item.Value == weather.Trim())
                {
                    return item.Key;
                }
            }
            return -1;
        }
    }
}
