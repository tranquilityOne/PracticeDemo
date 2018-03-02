using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3net.Common.Gps;

namespace EncryptDemo
{
    /// <summary>
    /// 定位过滤
    /// </summary>
    class LocationFilterTest
    {
        static void Main()
        {
            Location location1 = GetEnity(22.641766, 113.922681,DateTime.Now);
            Location location2 = GetEnity(22.641866, 113.922881,DateTime.Now.AddMinutes(1));
            Location location3 = GetEnity(22.642866, 113.925881, DateTime.Now.AddMinutes(2));
            Location location4 = GetEnity(22.642866, 113.922881, DateTime.Now.AddMinutes(3));
            Location location5 = GetEnity(22.641866, 113.922881, DateTime.Now.AddMinutes(4));
            Location location6 = GetEnity(22.642866, 113.932881, DateTime.Now.AddMinutes(6));
            List<Location> lists = new List<Location>();
            lists.Add(location1);
            lists.Add(location2);
            lists.Add(location3);
            lists.Add(location4);
            lists.Add(location5);
            lists.Add(location6);

            Location outLoc = null;
            List<int> listDistance = new List<int>();
            for (int i = 0; i < lists.Count; i++)
            {
                FilterLocation.Filter3Point(lists.Skip(i).Take(5).ToList(), out outLoc);
            }
            
        }

        static Location GetEnity(double lat,double lng,DateTime gpsTime)
        {
            Location location = new Location();
            location.Lat = lat;
            location.Lng = lng;
            location.GpsTime = gpsTime;
            return location;
        }
    }
}
