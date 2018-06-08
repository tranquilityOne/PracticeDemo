using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wearable.Common;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using EncryptDemo.Common;
using e3net.Common.NetWork;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using e3net.Common;
using e3net.MongodbServer.Test;

namespace EncryptDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.Write(Int32.Parse("FFFF", System.Globalization.NumberStyles.HexNumber));

            //GroupByTest.GroupByEx1();
            //PostAlarm();
            //long thisStamp = StringHelper.GetTimeStamp(DateTime.Now);
            //Console.WriteLine(StringHelper.GetTimeByStamp(thisStamp.ToString()));
            //Console.WriteLine(thisStamp);
            //位运算
            //Console.WriteLine((0x00 | 0x01).ToHexPadLeft(2));
            //Console.WriteLine((0x08 | 0x10 | 0x01 | 0x02 | 0x20 | 0x40).ToHexPadLeft(2));

            //十进制和16进制互转
            //int num = 698885;
            //string hexStr = num.ToHexPadLeft(6);
            //int num1 = Int32.Parse(hexStr, System.Globalization.NumberStyles.HexNumber);
            //byte[] bytes = hexStr.HexStringToByte();
            //Console.WriteLine(bytes[0].ToHexPadLeft(2) + bytes[1].ToHexPadLeft(2) + bytes[2].ToHexPadLeft(2));
            //Console.WriteLine(num.ToHexPadLeft(6));


            //GetTime();
            //string text = "你的话费余额 66666 元！";
            //string dataUsage = "你的流量 100M";
            //string hex = "0F5C737C7151AB4E57006900460069005F004100300032004400000000000000";
            //byte[] bytes = hex.HexStringToByte();
            //byte[] bytes = Encoding.Unicode.GetBytes(text);
            //Console.WriteLine(Encoding.Unicode.GetString(bytes));
            //Console.WriteLine(Encoding.Unicode.GetBytes(text).ToHexString());
            //Console.WriteLine(Encoding.Unicode.GetBytes(dataUsage).ToHexString());
            //模拟api post 请求
            //GetCode();
            //时间格式转化
            //Console.WriteLine(DateTime.ParseExact("20170516113120", "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.AssumeUniversal).ToString());
            //YJ后台密码解密
            //Console.WriteLine(DESEncrypt.Decrypt("3217926385C69164", "B2ZV20"));
            //Console.WriteLine(GetFormatDate());
            //int hex1 = 0x0001;
            //int hex2 = 0x1001;
            //Console.WriteLine((0x0001+0x1000).ToHexPadLeft(4));
            //Console.Write(hex2 - hex1);

            //string hexString = "0001";
            //string hexString2 = "1" + hexString.Substring(1);            
            //Console.WriteLine(Convert.ToInt32(hexString2,16));

            //string maxHexString = "您好";
            //byte[] bytes = Encoding.Unicode.GetBytes(maxHexString);
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    Console.WriteLine(bytes[i]);
            //}
            ////字符转十六进制字符串
            //Console.WriteLine("**".ToHexString());
            //byte[] bytesss = new byte[] { 0x2A, 0x2A };
            ////字节转字符
            //Console.WriteLine(Encoding.Unicode.GetString(bytesss,0,1));
            //byte[] bytes = maxHexString.HexStringToByte();
            ////byte相加
            //Console.WriteLine((bytes[0] << 8) | bytes[1]);
            //string hexString = maxHexString.HexToString();

            //byte[] bytes = Encoding.Unicode.GetBytes("2A53533B3C23");
            //Console.WriteLine(Encoding.Unicode.GetString(bytes));

            //模拟webService调试
            // AjMobileService.AjMobileServiceSoapClient service = new AjMobileService.AjMobileServiceSoapClient();
            // AjMobileService.AppSearchOpention option = new AjMobileService.AppSearchOpention()
            // {
            //     PageSize = 10,
            //     MsgIndex = 0,
            //     LoginName = "86=18672583254",
            //     DeviceId = ""
            // };

            //var list = service.GetPushMessageList(option);

            //生成位置信息表名
            //string locationName = "tb_Location_" + Math.Abs(ToBKDRHash(("86203503205299")) % 10) + "_20170926";
            //Console.WriteLine(locationName);

            //Google地址解析
            //GoogleGetLatLang();

            ////地址匹配
            //Console.WriteLine(AdressConvert("深圳市百旺信科技大厦 附近约100米","en-us"));

            //模拟Http请求
            //HttpHelper.HttpGet("http://localhost:20209/OpenApi/AjMobileService.asmx/AjGetLastLocation?DeviceId=600288888065089");

            //设备ID解密
            //var deviceId = SrvLogic.CMDESEncrypt.Decrypt("Ide3edfcb72ed540aaac61037f4149e1be");
            //var deviceId = SrvLogic.CMDESEncrypt.Encrypt("862016120100766");
            //Console.WriteLine(deviceId);

            //判断GPS坐标是否在国内
            //Console.WriteLine(PositionJudgeHelper.IsInsideChina(22.4176660000, 114.1022079621));
            //Console.WriteLine(PositionJudgeHelper.IsInsideChina(22.4364176008, 114.1149902344));
            //Console.WriteLine(PositionJudgeHelper.IsInsideChina(19.5875641243,109.8151892424));
            //Console.WriteLine(PositionJudgeHelper.IsInsideChina(11.0059044597, 106.6937255859));

            //YJ密码解密
            //var pwd = Wearable.Common.DESEncrypt.Decrypt("2E06F712A29B8095856164863990EEAB", "4N0LF8");
            //Console.WriteLine(pwd);

            //距离测量
            //AppClient appClient = new AppClient();
            //DataSet ds=appClient.GetNearAppClients(22.6362975106478, 113.944965783566);

            //gps84_To_BD09
            //GpsItem item = PositionUtil.gps84_To_BD09(22.6645916666667, 113.995248333333);
            //Console.WriteLine("Blat:" + item.Lat + ",Glat:" + item.Lng + "");

            //gcj02_To_Gps84
            //GpsItem item = PositionUtil.gcj02_To_Gps84(10.7873453, 106.6476601);
            //Console.WriteLine("Blat:" + item.Lat + ",Glat:" + item.Lng + "");

            //高德逆地理解析
            //Console.WriteLine(LocationDemo.GetAddrFromGaode(10.750185,106.675117));

            //模拟上传文件
            //PostFile();

            //模拟读取txt文档数据
            //ReadFromText();

            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
            //Console.WriteLine(Convert.ToInt32("11110000", 2));

            //Console.WriteLine(Guid.NewGuid());
            //PostAlarmForLowBattery();

            //Console.WriteLine((600).ToHexPadLeft(4));
            //Console.WriteLine(("6,8").ToHexString());
            //Console.WriteLine(0x02 | 0x04);
            //Console.WriteLine(Encoding.Unicode.GetString(("387238723100").HexStringToByte()));
            //Console.Write(Encoding.Unicode.GetBytes("11").ToHexString());
            //Console.WriteLine(Encoding.ASCII.GetBytes("39 02 81 F0 EF 6C CD").ToHexString());

            //var bytes = Encoding.Unicode.GetBytes("万瑞博测试12");
            //string addHex = "";
            //if(bytes.ToHexString().Length <=32)
            //{
            //    var len = bytes.ToHexString().Length;
            //    addHex = addHex.PadLeft(32 - bytes.ToHexString().Length, '0');
            //    Console.WriteLine(bytes.ToHexString() + addHex);
            //    Console.WriteLine(bytes.ToHexString());
            //}
            //Console.WriteLine("13880888088".ToHexString());

            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd 11:10:00"));

            //PostAlarmForFrid();

            //byte[] bytes = Encoding.ASCII.GetBytes("http://119.23.136.169:8080/600000000000008/alarm/20171031/alarm_15014131606.jpg"); 
            //Console.WriteLine(Encoding.ASCII.GetString(bytes));
            //Console.WriteLine(Convert.ToBase64String(bytes));

            //HttpUtil http = new HttpUtil();
            //IDictionary<string,string> dic = new Dictionary<string, string>();
            //Console.WriteLine(Convert.ToBoolean(1));
            //Console.Write(http.PostRequest("http://s1.zjrt9999.com:8090/Common/GetHttpInfor?deviceId=600000000000006", dic, false));
            //Console.Write(http.GetRequest("http://localhost:9001/WeChatDevice/GetChatRecordByQueryDate?deviceId=600000000000018&queryDate=1509784200000", dic));
            //Console.WriteLine(100 << 16 | (255 << 8 | (255 & 0xff)));
            //Console.WriteLine((16777215).ToHexPadLeft(4));

            //Console.WriteLine(PostRepairDataTest());
            //Console.WriteLine((201711230905222).ToHexPadLeft(6));
            //Console.WriteLine(TestWeChat());
            //SaveImage();

            //TimerTest test1 = new TimerTest(10, "test1");
            //TimerTest test2 = new TimerTest(8, "test2");
            //test1.tickCompleted = new TickCompletedEventHandler(TickCompletedHandle);
            //test1.StartTick();
            //test2.StartTick();

            //Console.WriteLine(Encoding.Unicode.GetString(("0020").HexStringToByte()));
            //Console.WriteLine(Encoding.Unicode.GetBytes("我").ToHexString());
            //File.Move(System.Environment.CurrentDirectory + @"\doc\chat_1.amr", System.Environment.CurrentDirectory + @"\doc\chat_2.amr");

            //byte[] bytes = { 255, 255, 255 };
            //Console.WriteLine(bytes.ByteToInt32(0, 3));
            //Console.WriteLine(0x02 | 0x04 | 0x08 | 0x10 | 0x20 | 0x40);
            //HttpUtil hu = new HttpUtil();
            //for (int i = 0; i < 1000; i++)
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine("i="+i+",return："+hu.GetRequest("http://localhost:9991"));
            //}


            //Console.WriteLine(PostUploadPhoto());

            //byte[] byts = ("EFEFEFEFEFEF").HexStringToByte();
            //string base64Str = Convert.ToBase64String(byts);
            //Console.WriteLine(base64Str);
            //Console.WriteLine(TimeHelper.GetDateTimeFrom1970Ticks(1514457144));

            //Console.WriteLine(DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")));

            //Console.WriteLine(DateTime.Now.Ticks);

            //Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

            //ModelChatGroupLink();

            //var strs = "0123456789附近约xx";
            //Console.WriteLine(strs.Substring(0, strs.LastIndexOf("附近约") + 3));

            //Console.WriteLine(PostGetSyncData());
            //AddChatGroup();
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine(PostUploadPhoto());
            //}

            Console.WriteLine(9 / 100);

            #region 计时器测试(连续开始与暂停后,统计耗时)
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Restart();
            Thread.Sleep(2000);
            sw.Stop();
            Console.WriteLine($"耗时:{sw.ElapsedMilliseconds} ");
            sw.Restart();
            Thread.Sleep(2000);
            Console.WriteLine($"耗时:{sw.ElapsedMilliseconds} ");
            sw.Stop();
            #endregion




            #region 取模测试
            //int aNum = 0, bNum = 0, cNum = 0;
            //for (int i = 0; i < 100; i++)
            //{
            //    if (i % 3 == 0)
            //        aNum++;
            //    else if (i % 3 == 1)
            //        bNum++;
            //    else if (i % 3 == 2)
            //        cNum++;
            //    Console.WriteLine($"aNum:{aNum},bNum:{bNum},cNum:{cNum}");
            //}
            #endregion

            //MongodbInsert();
            //DeliveryConsole();
            Console.ReadKey();
        }


        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        private static DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        /// <summary>
        /// 数据插入
        /// </summary>
        static void MongodbInsert()
        {
            try
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                List<UserInfo> lists = new List<UserInfo>();
                for (int i = 1000000; i < 2000000; i++)
                {
                    lists.Add(new UserInfo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Age = i,
                        UserName = "Name_" + i
                    });
                }
                UserInfoBLL.Instance.InsertBatch(lists);               
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
           
        }

        #region 取模运算
        /// <summary>
        /// 取模运算
        /// </summary>
        static void DeliveryConsole()
        {
            for (int i = 1000; i < 9999; i++)
            {
                Console.WriteLine(i % 1000);
            }
        }


        public static int ToBKDRHash(string key)
        {
            int hash = 0;
            var arr = key.ToCharArray();
            foreach (var item in arr)
            {
                hash = hash * 131 + item;
            }
            return hash % 10;
        } 
        #endregion

        public static void TickCompletedHandle(string name)
        {
            Console.WriteLine(string.Format("{0}执行完毕！",name));
        }


        /// <summary>
        /// 谷歌地址解析
        /// </summary>
        public static void GoogleGetLatLang()
        {
            String googleKey = "AIzaSyCXazzAXAU8pPU4U6sq0aorPJD9Hxd_C5I";
            string data1 = "{\"homeMobileCountryCode\":452,\"homeMobileNetworkCode\":2,\"radioType\":\"gsm\",\"carrier\":\"T-Mobile\",\"cellTowers\":[{\"cellId\":42903,\"locationAreaCode\":242,\"mobileCountryCode\":452,\"mobileNetworkCode\":2,\"age\":0,\"signalStrength\":63},{\"cellId\":42901,\"locationAreaCode\":242,\"mobileCountryCode\":452,\"mobileNetworkCode\":2,\"age\":0,\"signalStrength\":58},{\"cellId\":42902,\"locationAreaCode\":242,\"mobileCountryCode\":452,\"mobileNetworkCode\":2,\"age\":0,\"signalStrength\":80},{\"cellId\":42213,\"locationAreaCode\":242,\"mobileCountryCode\":452,\"mobileNetworkCode\":2,\"age\":0,\"signalStrength\":90},{\"cellId\":42266,\"locationAreaCode\":242,\"mobileCountryCode\":452,\"mobileNetworkCode\":2,\"age\":0,\"signalStrength\":92}],\"wifiAccessPoints\":[{\"macAddress\":\"00:00:00:00:00:00\",\"signalStrength\":0,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0},{\"macAddress\":\"A8:58:40:FE:4A:A4\",\"signalStrength\":-82,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0},{\"macAddress\":\"C4:E9:84:77:08:5A\",\"signalStrength\":-86,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0},{\"macAddress\":\"C4:E9:84:56:68:14\",\"signalStrength\":-92,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0},{\"macAddress\":\"24:9E:AB:E7:CF:60\",\"signalStrength\":-90,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0},{\"macAddress\":\"60:E3:27:6D:F8:E0\",\"signalStrength\":-86,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0},{\"macAddress\":\"64:70:02:E9:66:FC\",\"signalStrength\":-81,\"age\":0,\"signalToNoiseRatio\":0,\"channel\":0}]}";
            string data = "{\"homeMobileCountryCode\": 310,\"homeMobileNetworkCode\": 260,\"radioType\": \"gsm\",\"carrier\": \"T-Mobile\",\"cellTowers\": [{\"cellId\": 39627456,\"locationAreaCode\": 40495,\"mobileCountryCode\": 310,\"mobileNetworkCode\": 260,\"age\": 0,\"signalStrength\": -95}],\"wifiAccessPoints\": [{\"macAddress\": \"01:23:45:67:89:AB\", \"signalStrength\": 8, \"age\": 0,\"signalToNoiseRatio\": -65, \"channel\": 8},{\"macAddress\": \"01:23:45:67:89:AC\",\"signalStrength\": 4,\"age\": 0}]}";
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/geolocation/v1/geolocate?key=" + googleKey);
            req.Method = "POST";
            req.ContentType = "application/json";
            Stream reqstream = req.GetRequestStream();
            byte[] b = Encoding.ASCII.GetBytes(data1);
            reqstream.Write(b, 0, b.Length);
            StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream(), System.Text.Encoding.Default);
            String JsonStr = responseReader.ReadToEnd();
            Console.WriteLine(JsonStr);
        }

        /// <summary>
        /// api test
        /// </summary>
        public static void  GetCode()
        {
            try
            {
                HttpRequestMode mode = new HttpRequestMode();
                mode.token = "123";
                mode.data = "{'phoneCode':'86'}";
                string jsonStr = JsonHelper.ToJson(mode);
                string loginData = "{\"loginName\":\"15014131606\",\"password\":\"123\",\"phoneCode\":\"86\"}";
                string userData = "{\"token\";\"269a5c6ccaaaced8cebfc2304425d2a23c051a0c\",\"userId\":\"53ec01c7-2835-4952-9c28-10bb00151a81\"}";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://192.168.11.46:8090/User/EditUserPhoto");
                req.Method = "POST";
                req.ContentType = "application/json";
                Stream reqstream = req.GetRequestStream();
                byte[] b = Encoding.Default.GetBytes(loginData);
                reqstream.Write(b, 0, b.Length);
                StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
                String JsonStr = responseReader.ReadToEnd();
                Console.WriteLine(JsonStr);
            }
            catch (Exception ex)
            {
                
            }
          
        }

        #region 进制转换

        public static string StrConvert(string str)
        {
            return str.Replace("\"", "\\\"");
        }

        /// <summary>
        /// 汉字转二进制
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ChineseToBinary(string s)
        {
            byte[] data = Encoding.Unicode.GetBytes(s);
            StringBuilder result = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }

        /// <summary>
        /// 二进制转汉字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string BinaryToChinese(string input)
        {
            StringBuilder sb = new StringBuilder();
            int numOfBytes = input.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(input.Substring(8 * i, 8), 2);
            }
            return System.Text.Encoding.Unicode.GetString(bytes);

        }

        /// <summary>
        /// 十六进制字符串转byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] HexStringToByte(String value)
        {

            byte[] b = new byte[value.Length / 2];
            for (int i = 0; i < value.Length / 2; i++)
            {
                String tem = value.Substring(i * 2, 2);
                b[i] = byte.Parse(tem, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return b;
        }

        /// <summary>
        /// 获取16进制格式日期数据现在时间为2015/01/05 02:35:48，对应数据为年2015-2000:0x0F,月：0x01,日0x05,时0x02, 分0x23, 秒0x30
        /// </summary>
        /// <returns></returns>
        public static string GetFormatDate()
        {
            string dateStr = DateTime.Now.ToString("yyMMddHHmmss");
            string hexDate = string.Empty;
            for (int i = 0; i < dateStr.Length; i = i + 2)
            {
                int item = Convert.ToInt32(dateStr.Substring(i, 2));
                hexDate += Convert.ToInt32(item).ToHexPadLeft(2);
            }
            return hexDate;
        } 
        #endregion

        #region 模拟发送学生卡报警信息
        /// <summary>
        /// 模拟发送学生卡报警信息
        /// </summary>
        /// <returns></returns>
        public static void PostAlarm()
        {

            string DeviceId = "86556030000343";
            string strType = "SOS";
            //string fenceData = "22.650143,113.927942,围栏1,广东省深圳市宝安区石岩街道应人石社区西南方向50米";
            string sosData = "22.650143,113.927942,广东省深圳市宝安区石岩街道应人石社区西南方向50米";
            string fridData = "2";
            string paramsUrl = "DeviceId=" + DeviceId + "&Type=" + strType + "&DateTime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "&Data=" + sosData + "";
            string url = "http://localhost:8002/tools/alarm_message_new.aspx?" + paramsUrl;
            HttpHelper.HttpGet(url);
        }


        /// <summary>
        /// 模拟发送学生卡报警信息
        /// </summary>
        /// <returns></returns>
        public static void PostAlarmForLowBattery()
        {

            string DeviceId = "86556030000863";
            string strType = "Battery_20";
            string Data = "";
            string paramsUrl = "DeviceId=" + DeviceId + "&Type=" + strType + "&DateTime=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "&Data=" + Data + "";
            string url = "http://localhost:8002/tools/alarm_message_new.aspx?" + paramsUrl;
            HttpHelper.HttpGet(url);
        }


        /// <summary>
        /// 模拟发送考勤报警信息
        /// </summary>
        /// <returns></returns>
        public static void PostAlarmForFrid()
        {
            string DeviceId = "86556030000863";
            string strType = "RFID";
            string Data = "2";
            string DateTime = "2017/10/18 07:59:23";
            string paramsUrl = "DeviceId=" + DeviceId + "&Type=" + strType + "&DateTime=" + DateTime + "&Data=" + Data + "";
            string url = "http://localhost:8002/tools/alarm_message_new.aspx?" + paramsUrl;
            HttpHelper.HttpGet(url);
        }
        #endregion

        #region 模拟上传文件
        /// <summary>
        /// 模拟上传文件
        /// </summary>
        public static void PostFile()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"doc\\微信图片.jpg";
            var url = "http://localhost:8090/User/EditUserPhoto";
            string fileKeyName = "test";
            NameValueCollection collection = new NameValueCollection();
            collection.Add("test1", "123");
            HttpHelper.HttpPostData(url, 60000, fileKeyName, filePath, collection);
        }
        #endregion

        #region 逐个从txt文档 读出数据
        /// <summary>
        /// 逐个从txt文档 读出数据
        /// </summary>
        public static void ReadFromText()
        {
            string baseRoot = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\11.txt";
            StreamReader sr = new StreamReader(baseRoot, Encoding.UTF8);
            string strLine = null;
            while ((strLine = sr.ReadLine()) != null)
            {
                Console.WriteLine(strLine);
            }
        } 
        #endregion

        #region 模拟请求webservices
        /// <summary>
        /// 模拟请求webservices
        /// </summary>
        public static void PostWebServices()
        {
            HttpUtil httpHandler = new HttpUtil();
            string url = "http://localhost:20212/OpenApi/OpenService.asmx/AjGetHeartDataRecord";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("DeviceId", "60028888806498");
            dic.Add("MsgIndex", "1");
            dic.Add("PageSize", "10");
            dic.Add("LoginName", "sss");
            httpHandler.PostRequest(url, dic, false);

        }

        public static void PostWebServices1()
        {
            try
            {
                string data = "{'options':{'DeviceId':'60028888806498','AlarmType':1,'UserType':1}}";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:20212/OpenApi/OpenService.asmx/GetAlarmInfor");
                req.Method = "POST";
                req.ContentType = "application/json;charset=utf-8";
                Stream reqstream = req.GetRequestStream();
                byte[] b = Encoding.UTF8.GetBytes(data);
                reqstream.Write(b, 0, b.Length);
                StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.UTF8);
                String JsonStr = responseReader.ReadToEnd();
                Console.WriteLine(JsonStr);
            }
            catch (Exception ex)
            {

            }

        } 
        #endregion

        #region ref 传值测试

        public static void ShowRefStudent(ref Student entity)
        {
            entity.Name = "ref name";
            entity.Weight = 60;
            ShowRefStudentAgain(ref entity);
        }


        public static void ShowRefStudentAgain(ref Student entity)
        {
            entity.Name = "ref name again";
            entity.Weight = 70;
        }


        public static void ShowUnRefStudent(Student entity)
        {
            entity.Name = "ref name";
            entity.Weight = 60;
            ShowUnRefStudentAgain(entity);
        }


        public static void ShowUnRefStudentAgain(Student entity)
        {
            entity.Name = "ref name again";
            entity.Weight = 70;
        }

        public static void RefValue(ref int value)
        {
            value += 20;
        }

        public static void UnRefValue(int value)
        {
            value += 20;
        }
        #endregion

        #region 模拟服务接口
        public static string PostVoiceDataTest()
        {
            string result = "success";
            try
            {
                List<FormItemModel> lists = new List<FormItemModel>();
                lists.Add(new FormItemModel()
                {
                    Key = "from_x",
                    Value = "116.230640"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "from_y",
                    Value = "39.906952"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "from_address",
                    Value = "浙江省杭州市江干区"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "from_tel",
                    Value = "13333333333"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "from_checkinfo",
                    Value = "MoPNGHnhcjsj"
                });
                var amrFile = @"E:\浙江瑞投\svnsource\WoAng.Web.Api\UploadFiles\new.amr";
                lists.Add(new FormItemModel()
                {
                    Key = "from_content",
                    Value = "",
                    FileName = "测试语音.amr",
                    FileContent = File.OpenRead(amrFile)
                });
                string url = "http://sapp.zjrt9999.com/home/Record/apply_seek";
                string url1 = "http://s1.zjrt9999.com:8090/ServiceCenter/UploadVoiceData?deviceId=600000000000008&locations=39.906952,116.230640";
                result = e3net.Common.NetWork.HttpUtil.PostForm(url1, lists);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string PostRepairDataTest()
        {
            string result = "success";
            try
            {
                //string url = "http://sapp.zjrt9999.com/home/Record/apply_seek";
                string url1 = "http://s1.zjrt9999.com:8090/ServiceCenter/UploadRepairData?deviceId=600000000000008&locations=30.179725,120.258633";
                result = e3net.Common.NetWork.HttpUtil.PostForm(url1, null);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string PostFeedBackTest()
        {
            string result = "success";
            try
            {
                List<FormItemModel> lists = new List<FormItemModel>();
                lists.Add(new FormItemModel()
                {
                    Key = "content",
                    Value = "我的建议啊啊是的放松的发生的防守对方"
                });
                lists.Add(new FormItemModel());
                string appFile = System.Environment.CurrentDirectory + @"\doc\微信图片.jpg";
                lists.Add(new FormItemModel()
                {
                    Key = "img1",
                    Value = "",
                    FileName = "微信图片.jpg",
                    FileContent = File.OpenRead(appFile)
                });
                lists.Add(new FormItemModel()
                {
                    Key = "img2",
                    Value = "",
                    FileName = "微信图片.jpg",
                    FileContent = File.OpenRead(appFile)
                });
                lists.Add(new FormItemModel()
                {
                    Key = "img3",
                    Value = "",
                    FileName = "微信图片.jpg",
                    FileContent = File.OpenRead(appFile)
                });
                string url1 = "http://localhost:9001/User/SubmitFeedBack?userId=11122334455";
                result = e3net.Common.NetWork.HttpUtil.PostForm(url1, lists);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string PostUploadPhoto()
        {
            string result = "success";
            try
            {
                List<FormItemModel> lists = new List<FormItemModel>();
                string appFile = System.Environment.CurrentDirectory + @"\doc\微信图片.jpg";
                lists.Add(new FormItemModel()
                {
                    Key = "img1",
                    Value = "",
                    FileName = "862035010000431.jpg",
                    FileContent = File.OpenRead(appFile)
                });
                string url1 = "http://s1.zjrt9999.com:8090/Document/UploadPhoto?deviceId=862035010000431&serieNumber=1";
                result = e3net.Common.NetWork.HttpUtil.PostForm(url1, lists);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static string PostGetSyncData()
        {
            string result = "success";
            try
            {
                List<FormItemModel> lists = new List<FormItemModel>();
                lists.Add(new FormItemModel()
                {
                    Key = "deviceId",
                    Value = "862035000002736"
                });
                string url1 = "http://s1.zjrt9999.com:8090/Device/GetSyncData";
                result = e3net.Common.NetWork.HttpUtil.PostForm(url1, lists);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        #endregion

        #region 微聊测试
        public static string TestWeChat()
        {
            string result = "success";
            try
            {
                //string url = "http://sapp.zjrt9999.com/home/Record/apply_seek";
                string deviceId = "865560300008650";
                string appFile = System.Environment.CurrentDirectory + @"\doc\chat_1.amr";
                List<FormItemModel> lists = new List<FormItemModel>();
                lists.Add(new FormItemModel()
                {
                    Key = "deviceId",
                    Value = deviceId
                });
                lists.Add(new FormItemModel()
                {
                    Key = "sendUser",
                    Value = "2066d2cb-5bc9-4f60-b88f-3884714918fa"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "acceptUser",
                    Value = "6dc770f2-4adb-40b6-8fb5-af2a0cf3016c"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "speakerType",
                    Value = "1"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "duration",
                    Value = "5"
                });
                lists.Add(new FormItemModel()
                {
                    Key = "from_content",
                    Value = deviceId,
                    FileName = "chat_1.amr",
                    FileContent = File.OpenRead(appFile)
                });
                string url1 = "http://localhost:9001/WeChat/UploadChatSingle";
                result = e3net.Common.NetWork.HttpUtil.PostForm(url1, lists);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        } 
        #endregion

        public static void SaveImage()
        {
            string appFile = System.Environment.CurrentDirectory + @"\doc\微信图片.jpg";
            Image img = Image.FromFile(appFile);
            //截图画板
            Bitmap bm = new Bitmap(400, 400);
            using (Graphics g = Graphics.FromImage(bm))
            {
                //设置高质量插值法
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //创建截图路径（类似Ps里的路径）
                GraphicsPath gpath = new GraphicsPath();
                gpath.AddEllipse(100, 100, 200, 200);//圆形

                //设置画板的截图路径
                g.SetClip(gpath);
                //对图片进行截图
                g.DrawImage(img, 0,0);
                //保存截好的图
            }  
            bm.Save(System.Environment.CurrentDirectory + @"\doc\压缩图片.jpg", ImageFormat.Png);
        }

        #region 模拟添加群聊数据(新数据结构)

        /// <summary>
        /// 模拟添加设备关联人员
        /// </summary>
        /// <returns></returns>
        public static void ModelChatGroupLink()
        {
            e3net.Common.NetWork.HttpUtil http = new e3net.Common.NetWork.HttpUtil();
            for (int i = 0; i < 10000; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    GhatGroupUserInfo userInfor = new GhatGroupUserInfo();
                    userInfor.deviceId = "device_" + i;
                    userInfor.nickName = string.Format("name_{0}_{1}", i, k);
                    userInfor.userId = string.Format("id_{0}_{1}", i, k);

                    string jsonData = JsonHelper.ObjectToJSON(userInfor);                   
                    string url = "http://localhost:29105/WeChat/AddGroupChatLink";
                    http.PostRequest(url, jsonData);
                }               
            }
        }

        /// <summary>
        /// 添加群聊数据
        /// </summary>
        static void AddChatGroup()
        {
            e3net.Common.NetWork.HttpUtil http = new e3net.Common.NetWork.HttpUtil();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 10000; k++)
                {
                    string deviceId = "device_" + k;
                    for (int i = 0; i < 3; i++)
                    {
                        PostChatGroupData postEntity = new PostChatGroupData();
                        postEntity.deviceId = deviceId;
                        postEntity.speakerId = string.Format("id_{0}_{1}", k, i);
                        postEntity.timePeriod = 3;

                        string jsonData = JsonHelper.ObjectToJSON(postEntity);
                        string url = "http://localhost:29105/WeChat/AddChatGroup";
                        http.PostRequest(url, jsonData);
                    }
                } 
            }
           
            sw.Stop();
            Console.WriteLine("总耗时："+sw.Elapsed.TotalMilliseconds);
        }

        #endregion
    }


    #region 聊天辅助类
    /// <summary>
    /// 群聊用户信息
    /// </summary>
    public class GhatGroupUserInfo
    {
        public string deviceId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickName { get; set; }
    }

    /// <summary>
    /// 提交群聊请求数据
    /// </summary>
    public class PostChatGroupData
    {
        public string deviceId { get; set; }

        public string speakerId { get; set; }

        public int timePeriod { get; set; }
    }
    #endregion

    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        /// <summary>
        /// true 男 false 女
        /// </summary>
        public bool Sex { get; set; }
    }
}
