using e3net.Common.Json;
using e3net.Common.NetWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace e3net.Common.Gps
{
    #region 参数类
    public class GoogleLBSWIFIRequestEntity
    {

        private string _radioType = "gsm";

        private string _carrier = "T-Mobile";
        public int homeMobileCountryCode { get; set; }
        public int homeMobileNetworkCode { get; set; }

        public string radioType
        {
            get { return _radioType; }
            set { _radioType = value; }
        }
        public string carrier
        {
            get { return _carrier; }
            set { _carrier = value; }
        }

        public List<GoogleCellTowers> cellTowers { get; set; }
        public List<GoogleWifiAccessPoints> wifiAccessPoints { get; set; }
    }


    public class GoogleWifiAccessPoints
    {

        public string macAddress { get; set; }
        public int signalStrength { get; set; }
        public int age { get; set; }
        public int signalToNoiseRatio { get; set; }
        public int channel { get; set; }

    }
    public class GoogleCellTowers
    {

        public int cellId { get; set; }
        public int locationAreaCode { get; set; }
        public int mobileCountryCode { get; set; }
        public int mobileNetworkCode { get; set; }
        public int age { get; set; }
        public int signalStrength { get; set; }
    }
    #endregion


    #region 结果类
    public class GoogleLatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }

    public class GoogleLoction
    {
        public GoogleLatLng location { get; set; }

        public double accuracy { get; set; }
    }


    public class GoogleAddress
    {
        public string status { get; set; }

        public List<address_components> results { get; set; }
    }

    public class Street
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<String> types { get; set; }
    }
    public class address_components
    {
        public string formatted_address { get; set; }
    }
    #endregion

    public class GoogleParse
    {
        /// <summary>
        /// WIFI,基站数据转google解析数据
        /// </summary>
        /// <param name="lbsList"></param>
        /// <param name="WifiList"></param>
        /// <returns></returns>
        public static GoogleLBSWIFIRequestEntity GetGoogleWifiAccessPointsFromWiFiItem(List<LBSDataItem> lbsList, List<WiFiItem> WifiList)
        {
           GoogleLBSWIFIRequestEntity ret = new GoogleLBSWIFIRequestEntity();
            ret.homeMobileCountryCode = Convert.ToInt32(lbsList[0].mcc);
            ret.homeMobileNetworkCode = Convert.ToInt32(lbsList[0].mnc);
            ret.cellTowers = new List<GoogleCellTowers>();
            for (int i = 0; i < lbsList.Count; i++)
            {
                var item = new GoogleCellTowers();
                item.cellId = lbsList[i].cellid;
                item.locationAreaCode = lbsList[i].lac;
                item.mobileCountryCode = Convert.ToInt32(lbsList[i].mcc.Trim());
                item.mobileNetworkCode = Convert.ToInt32(lbsList[i].mnc.Trim());
                item.signalStrength = -int.Parse(lbsList[i].signal.ToString());
                ret.cellTowers.Add(item);

            }
            if (WifiList != null)
            {
                List<GoogleWifiAccessPoints> wifi = new List<GoogleWifiAccessPoints>();
                for (int i = 0; i < WifiList.Count; i++)
                {
                    GoogleWifiAccessPoints point = new GoogleWifiAccessPoints();
                    point.macAddress = WifiList[i].Mac;
                    point.signalStrength = WifiList[i].SignalValue;
                    wifi.Add(point);
                }
                ret.wifiAccessPoints = wifi;
            }


            return ret;
        }

        /// <summary>
        /// 谷歌硬件解析
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static LocData GetLatLngFromGoogleLBSWIFIRequestEntity(GoogleLBSWIFIRequestEntity entity)
        {
            LocData locReturn = null;
            try
            {
                HttpUtil httpUtil = new HttpUtil();
                var postData = JsonHelper.ToJson(entity);
                string Key = ConfigurationManager.AppSettings["GoogleGeoKey"].ToString();
                string url = "https://www.googleapis.com/geolocation/v1/geolocate?key=" + Key;
                var responseData = httpUtil.PostRequest(url, postData);
                log4net.LogManager.GetLogger(typeof(GoogleParse).FullName).Info(responseData);
                GoogleLoction loca = JsonHelper.JSONToObject<GoogleLoction>(responseData);
                if (loca != null && loca.location != null)
                {
                    locReturn = new LocData();
                    locReturn.lat = loca.location.lat;
                    locReturn.lng = loca.location.lng;
                    locReturn.accuracy = loca.accuracy;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(GoogleParse).FullName).Error("GetLatLngFromGoogleLBSWIFIRequestEntity 操作失败:" + ex.Message,ex);
            }
            return locReturn;
        }

        /// <summary>
        /// 逆地理解析
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lng">经度</param>
        /// <returns></returns>
        public static string GetAddressFromGoogle(double lat, double lng,string language="zh-cn")
        {
            var data = "";
            try
            {
                string Key = ConfigurationManager.AppSettings["GoogleGeoKey"].ToString();
                String url = String.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}&language={3}", lat, lng, Key, language);
                HttpUtil httpUtil = new HttpUtil();
                string responseData = httpUtil.GetRequest(url);
                var entity = JsonHelper.JSONToObject<GoogleAddress>(responseData);
                if (entity != null && entity.status == "OK")
                {
                    data = entity.results[0].formatted_address;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(GoogleParse).FullName).Error("GetLatLngFromGoogle 操作失败:" + ex.Message + "  data:" + data,ex);
            }
            return data;
        }
    }
}
