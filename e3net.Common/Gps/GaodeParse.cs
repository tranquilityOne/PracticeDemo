using e3net.Common.Json;
using e3net.Common.NetWork;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3net.Common.Entity;

namespace e3net.Common.Gps
{
    public class GaodeParse
    {

        static string gaode_key = System.Configuration.ConfigurationManager.AppSettings["gaode_key"];

      /// <summary>
        /// 高德天气  说明 http://lbs.amap.com/api/webservice/guide/api/weatherinfo/#scene
      /// </summary>
        /// <param name="city">如:深圳 或编号 440300</param>
      /// <returns></returns>
        public static JObject getweather(string city)
        {
            String url = "http://restapi.amap.com/v3/weather/weatherInfo?key=" + gaode_key + "&city=" + city;
            HttpUtil request = new HttpUtil();
            JObject obj = JsonHelper.FromJson(request.GetRequest(url));
            if (obj!=null)
            {
                if(obj["status"]!=null && obj["status"].ToString().Equals("1"))
                {
                      JArray res = JArray.Parse(obj["lives"].ToString());
                      return JObject.Parse(res[0].ToString());
                }
                return null;
              
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 逆地理编码  说明 http://lbs.amap.com/api/webservice/guide/api/georegeo/#regeo
        /// </summary>
        /// <param name="location">经度 城市名称如:北京，经纬度格式为lng,lat坐标如: location=116.305145,39.982368;</param>
        /// <returns></returns>
        public static JObject geocode(string location)
        {
            String url = " http://restapi.amap.com/v3/geocode/regeo?key=" + gaode_key + "&location=" + location;
            HttpUtil request = new HttpUtil();
            JObject obj = JsonHelper.FromJson(request.GetRequest(url));

            if (obj != null && obj["status"] != null&&obj["status"].ToString().Equals("1"))
            {
                return JObject.Parse(obj["regeocode"].ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 经纬度 获取 天气，先地理解析，再获取天气，共两次http请求
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static GaoDeWeatherModel getweather(double lng, double lat)
        {
            string location = lng + "," + lat;
            JObject geo = geocode(location);//先获取城市
            if (geo!=null)
            {
               string adcode=geo["addressComponent"]["adcode"].ToString();
               JObject weter=getweather(adcode);
               GaoDeWeatherModel model = new GaoDeWeatherModel();
               if (weter != null)
               {
                   model.Province = weter["province"].ToString();//省份名
                   model.City = weter["city"].ToString();//城市名
                   model.Weather = weter["weather"].ToString();//天气现象，天气现象对应描述
                   model.Temperature = weter["temperature"].ToString();//时时气温
                   model.WindDirection = weter["winddirection"].ToString();//风向
                   model.Humidity = weter["humidity"].ToString();//湿度
                   model.Date = weter["reporttime"].ToString();//报道日期
                   return model;
               }
               return null;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取地理位置
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static string GetAddress(double lng, double lat)
        {
            string location = lng + "," + lat;
            //先请求位置信息
            JObject geo = geocode(location);
            string address = string.Empty;
            if (geo != null)
            {
                address = geo["formatted_address"].ToString();
            }
            return address;
        }

    }
}
