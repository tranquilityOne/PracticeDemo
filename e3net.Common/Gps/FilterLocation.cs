using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e3net.Common.Gps
{
    public class Location
    {
        /// 原始纬度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 原始经度
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// 百度纬度
        /// </summary>
        public double BLat { get; set; }
        /// <summary>
        /// 百度经度
        /// </summary>
        public double BLng { get; set; }
        /// <summary>
        /// 高德纬度
        /// </summary>
        public double GLat { get; set; }
        /// <summary>
        /// 高德经度
        /// </summary>
        public double GLng { get; set; }

        /// <summary>
        /// 定位类型
        /// </summary>
        public int LocationType { get; set; }

        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime GpsTime { get; set; }
    }


   public class FilterLocation
   {

        /// <summary>
        /// 三点坐标过滤法  第2个点 （到1的距离+到3的距离）如果大于xx 陪 （1到3的距离） 则过滤
        /// </summary>
        public static bool Filter3Point(List<Location> listp, out Location filterLocation)
        {
            filterLocation = null;//过滤点
            double xx = 2.5;//十分钟异常参数
            double xxl = 3;//30分钟异常参数
            bool isFilt = false;
            if (listp != null && listp.Count >= 5 && listp[1].LocationType != 2)
            {
                double times = Math.Abs((listp[0].GpsTime - listp[2].GpsTime).TotalSeconds);//3点内的时间间隔

                bool isMove = false;
                #region 是否在运动中前进 前四个点形成的钜形 高度小于一定值，证明在运动中
                double qq = 300;//距离300米以上，计算
                double xh = 0.25;//合法高度参数
                double d25 = PositionUtil.Distance(listp[1].Lat, listp[1].Lng, listp[4].Lat, listp[4].Lng);//2和5的距离

                double d23 = PositionUtil.Distance(listp[1].Lat, listp[1].Lng, listp[2].Lat, listp[2].Lng);//2和3的距离
                double d35 = PositionUtil.Distance(listp[2].Lat, listp[2].Lng, listp[4].Lat, listp[4].Lng);//3和5的距离
                double h3 = ExtendHelper.TriangleHeiht(d25, d23, d35);//点3 的高

                double d24 = PositionUtil.Distance(listp[1].Lat, listp[1].Lng, listp[3].Lat, listp[3].Lng);//2和4的距离
                double d45 = PositionUtil.Distance(listp[3].Lat, listp[3].Lng, listp[4].Lat, listp[4].Lng);//4和5的距离
                double h4 = ExtendHelper.TriangleHeiht(d25, d24, d45);//点4 的高

                if (d25 > qq && h3 < (d25 * xh) && h4 < (d25 * xh))
                {
                    isMove = true;
                }
                #endregion

                if (!isMove && times < 1800)//三十分钟以内
                {
                    double c = PositionUtil.Distance(listp[0].Lat, listp[0].Lng, listp[2].Lat, listp[2].Lng);
                    double c1 = PositionUtil.Distance(listp[0].Lat, listp[0].Lng, listp[1].Lat, listp[1].Lng);
                    double c2 = PositionUtil.Distance(listp[1].Lat, listp[1].Lng, listp[2].Lat, listp[2].Lng);

                    if (times < 600)
                    {
                        if ((c1 + c2) > c * xx)//距离大于 xx陪过滤
                        {
                            isFilt = true;
                        }
                    }
                    else if (times < 1830)
                    {
                        if ((c1 + c2) > c * xxl)//距离大于 xxl陪过滤
                        {
                            isFilt = true;
                        }
                    }
                    if (isFilt)
                    {
                        filterLocation = listp[1];
                    }
                }
            }
            return isFilt;
        }
    }
}
