using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDemo
{

    /// <summary>
    /// 经纬度
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// 维度
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
    }

    public class CoordinateHelper
    {
        public static double PI = 3.14159265;
        public static double TWOPI = 2 * PI; //圆弧度长

        /// <summary>
        /// 根据经纬度计算2点之间角度(方位角度,与x轴的角度,基本上用不着)
        /// </summary>
        /// <param name="x1">经度1</param>
        /// <param name="y1">纬度1</param>
        /// <param name="x2">经度2</param>
        /// <param name="y2">纬度2</param>
        /// <returns></returns>
        public static double GetAngle(decimal x1, decimal y1, decimal x2, decimal y2)
        {
            double dRotateAngle = Math.Atan2(double.Parse(Math.Abs(x1 - x2).ToString()), double.Parse(Math.Abs(y1 - y2).ToString()));
            if (x2 >= x1)
            {
                if (y2 >= y1)
                {
                }
                else
                {
                    dRotateAngle = Math.PI - dRotateAngle;
                }
            }
            else
            {
                if (y2 >= y1)
                {
                    dRotateAngle = 2 * Math.PI - dRotateAngle;
                }
                else
                {
                    dRotateAngle = Math.PI + dRotateAngle;
                }
            }
            dRotateAngle = dRotateAngle * 180 / Math.PI;
            return dRotateAngle;
        }

        /// <summary>
        /// 计算亮点之间方位角之差
        /// </summary>
        /// <param name="y1">纬度1</param>
        /// <param name="x1">经度1</param>
        /// <param name="y2">纬度2</param>
        /// <param name="x2">经度2</param>
        /// <returns></returns>
        public static double Angle2D(double y1, double x1, double y2, double x2)
        {
            double dtheta, theta1, theta2;
            //返回从x 轴到点 (x,y) 之间的角度(方位角)
            theta1 = Math.Atan2(y1, x1);
            theta2 = Math.Atan2(y2, x2);
            dtheta = theta2 - theta1;
            while (dtheta > PI)
                dtheta -= TWOPI;
            while (dtheta < -PI)
                dtheta += TWOPI;
            return dtheta;
        }

        /// <summary>
        /// 判断点是否在多边形内(球面)
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lng"><经度/param>
        /// <param name="polygons">多变形区域</param>
        /// <returns></returns>
        public static bool CoordinateInPolygons(double lat, double lng, List<Coordinate> polygons)
        {
            double angle = 0;
            double point1_lat;
            double point1_lng;
            double point2_lat;
            double point2_lng;
            int len = polygons.Count();
            //逐点比较
            for (int i = 0; i < len; i++)
            {
                point1_lat = polygons[i].Lat- lat;
                point1_lng = polygons[i].Lng- lng;
                point2_lat = polygons[(i + 1) % len].Lat - lat;
                point2_lng = polygons[(i + 1) % len].Lng - lng;
                angle += Angle2D(point1_lat, point1_lng, point2_lat, point2_lng);
            }
            if (Math.Abs(angle) < PI)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 是否有效坐标
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static bool IsValidGpsCoordinate(double latitude,double longitude)
        {
            if (latitude > -90 && latitude < 90 &&
                    longitude > -180 && longitude < 180)
            {
                return true;
            }
            return false;
        }
    }
}
