using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3net.MongodbServer;

namespace EncryptDemo
{
    public class TerLastInfo : BaseEntity
    {
        public int TerID { get; set; }
        public string IMEI { get; set; }
        public int AccStatus { get; set; }
        public int RunStatus { get; set; }
        public int LocateType { get; set; }
        public string GetLocateTime { get { return LocateTime.ToString("yyyy-MM-dd HH:mm:ss"); } }
        public DateTime LocateTime { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double MLongitude { get; set; }
        public double MLatitude { get; set; }
        public double BLongitude { get; set; }
        public double BLatitude { get; set; }
        public double Mileage { get; set; }
    }
}
