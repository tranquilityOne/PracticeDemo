using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EncryptDemo
{
    public class LocationDemo
    {
        private static string aliKey = "2b1e7e4323cdb8a5572669fc908b155d";
        /// <summary>
        /// 谷歌地图逆地理解析
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static string GetAddrFromGaode(double lat, double lng)
        {
            string addr = "";
            string url = String.Format("http://restapi.amap.com/v3/geocode/regeo?output=xml&location={0},{1}&key={2}&radius=300&extensions=all", lng, lat, aliKey);
            XmlDocument document = new XmlDocument();
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                var responeStream = request.GetResponse();
                StreamReader reader = new StreamReader(responeStream.GetResponseStream());
                string xml = reader.ReadToEnd();

                document.LoadXml(xml);              
                if (document.GetElementsByTagName("status")[0].InnerText == "1" && document.GetElementsByTagName("info")[0].InnerText.ToLower() == "ok")
                {
                    XmlNode node = document.GetElementsByTagName("regeocode")[0];
                    var formatted_address = node.SelectSingleNode("formatted_address").InnerText;
                    var node_pois = node.SelectNodes("pois")[0];
                    var name = node_pois.SelectSingleNode("poi/name").InnerText;
                    var direction = node_pois.SelectSingleNode("poi/direction").InnerText;
                    var distance = node_pois.SelectSingleNode("poi/distance").InnerText;
                    addr = string.Format("{0} 附近约{1}米", formatted_address, distance);
                }
            }
            catch (Exception ex)
            {      
         
            }
            return addr;
        }
    }
}
