using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using e3net.Common.Json;
using System.Configuration;

namespace RedisServer.Console
{
    class Program
    {
        static readonly int number = 100;//key总数=number*5
        static readonly int expireTime = 30;//过期时间10秒

        static int IMEI_NUM = 0;
       
        static long IMEI_PREX = 580000000000000;
        static long ORDERLOGIN_PREX_SET = 1000000;
        static long ORDERLOGIN_PREX_REMOVE = 1000000;
        static void Main(string[] args)
        {
            #region 异步读取数据
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread thread = new Thread(Woker);
            //    thread.Start(number);
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    Thread thread = new Thread(Pop);
            //    thread.Start(1000);
            //}
            #endregion

            #region 监听事件
            //Task.Run(() =>
            //{
            //    HashOperator.SubscribeExpireNot((channel, msg) => {
            //        var thisKey = msg.Substring(0, msg.IndexOf("$"));
            //        Ter terInfor = HashOperator.Get<Ter>(msg.Substring(0, msg.IndexOf("$")));
            //        if (terInfor!= null)
            //        {
            //            System.Console.WriteLine($"name:{terInfor.TerName},CretateTime:{terInfor.CreateTime},TimeStamp:{terInfor.ExpireTime},number:{terInfor.number}");
            //            if (terInfor.number > 2)
            //            {
            //                HashOperator.Remove(thisKey);
            //            }
            //            else
            //            {
            //                terInfor.number += 1;
            //                terInfor.CreateTime = DateTime.Now.ToString("yyyyMMdd HHmmss");
            //                terInfor.ExpireTime = DateTime.Now.AddSeconds(expireTime).ToString("yyyyMMdd HHmmss");
            //                HashOperator.Set<string>(msg, "", DateTime.Now.AddSeconds(expireTime));
            //                HashOperator.Set<Ter>(thisKey,terInfor);
            //            }                         
            //        }
            //    });
            //}); 
            #endregion

            #region 插入数据
            //Task.Run(() =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //        sw.Start();
            //        LoopAddList(number);

            //        //Push(number);
            //        //LoopAddKey(number);

            //        sw.Stop();
            //        TimeSpan ts2 = sw.Elapsed;
            //        System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
            //    }
            //    catch (Exception ex)
            //    {

            //        System.Console.WriteLine(ex.StackTrace);
            //    }
            //});
            #endregion

            #region 配置文件判断
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxIMEINumber"]))
            {
                throw new Exception("imei num config is empty");
            }
            IMEI_NUM = Convert.ToInt32(ConfigurationManager.AppSettings["MaxIMEINumber"]);
            #endregion

            System.Console.WriteLine("******************ts  设置key; tr 删除; tg 获取key ***************************");
            while (true)
            {
                string input = System.Console.ReadLine();
                if (input == "c")
                    System.Console.Clear();
                else if (input == "t")
                    RunTask();
                else if (input == "delete")
                    HashOperator.RemoveByPattern("list_3479*");
                else if (input == "ts")
                    TestBusinessMainSet();
                else if (input == "tg")
                    TestBusinessMainGet();
                else if (input == "tr")
                    TestBusinessMainRemove();
                else if (input == "td")
                    GetRedisData();
                else if (input == "tts")
                    LoopAddKey(number);
                else if (input == "ttg")
                    LoopAddKey(number);
                else if (input == "hash")
                    HashSet();
                else
                    System.Console.WriteLine("输入错误！");
            }
            System.Console.ReadKey();
        }

        #region 存储测试
        /// <summary>
        /// 读取数据
        /// </summary>
        static void Woker(object obj)
        {
            System.Console.WriteLine("woker thread begin");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            #region 查询所有
            for (int i = 0; i < number; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    System.Console.WriteLine(HashOperator.Get<string>(string.Format("key_{0}_{1}", i, k)));
                }
            }
            #endregion
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
        }

        /// <summary>
        ///插入数据 
        /// </summary>
        /// <param name="num"></param>
        static void LoopAddKey(int num)
        {
            Random r = new Random();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < num; i++)
            {             
                for (int k = 0; k < 5; k++)
                {
                    HashOperator.Set<Ter>(string.Format("key_{0}_{1}", i, k), new Ter()
                    {
                        TerId = i,
                        TerName = $"Name_{i}",
                        IMEI = i.ToString(),
                        IsOnLine = true,
                        Lat = 22.35643434m,
                        Lng = 112.54754875m,
                        TerTypeCode = "测试设备",
                        CreateTime = DateTime.Now.ToString("yyyyMMdd HHmmss"),
                        ExpireTime = DateTime.Now.AddSeconds(expireTime).ToString("yyyyMMdd HHmmss"),
                        number = 1                        
                    });
                }
            }
            sw.Stop();
            System.Console.WriteLine("总耗时:" + sw.ElapsedMilliseconds);
        }

        /// <summary>
        ///插入数据 
        /// </summary>
        /// <param name="num"></param>
        static void LoopGetKey(int num)
        {
            Random r = new Random();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < num; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    HashOperator.Get<Ter>(string.Format("key_{0}_{1}", i, k));
                }
            }
            sw.Stop();
            System.Console.WriteLine("总耗时:" + sw.ElapsedMilliseconds);
        }

        static void LoopAddList(int num)
        {
            try
            {
                List<Ter> listTer = new List<Ter>();
                for (int i = 0; i < num; i++)
                {
                    for (int k = 0; k < 1; k++)
                    {
                        listTer.Add(new Ter()
                        {
                            TerId = i,
                            TerName = $"Name_{i}_{k}",
                            IMEI = i.ToString(),
                            IsOnLine = true,
                            Lat = 22.35643434m,
                            Lng = 112.54754875m,
                            TerTypeCode = "测试设备",
                            data = Encoding.UTF8.GetBytes($"Name_{i}_{k}")
                        });                       
                    }
                    HashOperator.Set<List<Ter>>($"list_{i}", listTer);
                    listTer.Clear();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }          
        }

        static void LoopGetKeyList(int num)
        {
            try
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                for (int i = 0; i < num; i++)
                {
                    HashOperator.Get<List<Ter>>($"list_{i}");
                }
                //测试byte数据返回
                System.Console.WriteLine(Encoding.UTF8.GetString(HashOperator.Get<List<Ter>>($"list_1")[0].data));
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 队列测试

        static void AsyncPushData(int num)
        {
            string key = "server_send_1";
            for (int i = 0; i < num; i++)
            {
                HashOperator.PushString(key, JsonHelper.ToJson(new Ter()
                {
                    TerId = i,
                    TerName = $"Name_{i}",
                    IMEI = i.ToString(),
                    IsOnLine = true,
                    Lat = 22.35643434m,
                    Lng = 112.54754875m,
                    TerTypeCode = "测试设备"
                    
                }));
            }
        }

        /// <summary>
        /// 队列弹入数据
        /// </summary>
        /// <param name="loops"></param>
        static void Push(int loops)
        {
            string jsonStr = "{\"Msg\":\"ss\",\"CreateDateTime\":\"2018-05-15 15:20:30\"}";
            for (int i = 0; i < loops; i++)
            {
                HashOperator.PushString("list_pop", jsonStr);
            }
        }

        /// <summary>
        /// 队列弹出数据
        /// </summary>
        /// <param name="loops"></param>
        static void Pop(object loops)
        {
            int num = Convert.ToInt32(loops);
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < num; i++)
            {
                HashOperator.PopString("list_pop");
            }
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
        }
        #endregion

        #region 线程安全测试
        /// <summary>
        /// 总结:同时操作一个key,是不安全的
        /// </summary>
        static void RunTask()
        {
            int taskNums = 20;
            Task[] tasksArray = new Task[taskNums];
            for (int i = 0; i < taskNums; i++)
            {
                tasksArray[i] = Task.Factory.StartNew(() =>
                {
                    List<Ter> listTer=HashOperator.Get<List<Ter>>($"list_1");
                    if (listTer.Count > 0)
                        listTer[0].number++;
                   System.Console.WriteLine(listTer[0].number);
                   HashOperator.Set<List<Ter>>($"list_1", listTer);
                });
            }
            Task.WaitAll(tasksArray);
        }
        #endregion

        #region  主业务测试
        /// <summary>
        /// 测试主业务Set耗时(6/ms)
        /// </summary>
        static void TestBusinessMainSet()
        {
            
            string sourceData_Login = "*VK2015{0},AB#";//登陆包
            string sourceData_Heartbeat = "*VK2015{0},AH#";//心跳包
            System.Console.WriteLine("****Set执行中......****");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swTotal = new System.Diagnostics.Stopwatch();
            swTotal.Start();
            try
            {
                for (int i = 0; i < IMEI_NUM; i++)
                {
                    TerResultEntiy entity = new TerResultEntiy((IMEI_PREX + i).ToString(),
                                                    Encoding.ASCII.GetBytes(sourceData_Login),
                                                    ORDERLOGIN_PREX_SET + i,
                                                    1,
                                                    0,
                                                    1,
                                                    DateTime.Now.Ticks)
                    { };
                    //sw.Restart();
                    SendDataCache.SetSendData(entity);
                    //sw.Stop();
                    //System.Console.WriteLine($"IMEI:{(IMEI_PREX + i).ToString()} takes {sw.Elapsed} ms");
                }
                swTotal.Stop();
                System.Console.WriteLine($"All IMEIS takes {swTotal.ElapsedMilliseconds} ms");
                ORDERLOGIN_PREX_SET = ORDERLOGIN_PREX_SET + 1000000;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("error:" + ex.Message);
            }
            
        }

        /// <summary>
        /// 测试主业务Get耗时(6/ms)
        /// </summary>
        static void TestBusinessMainGet()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swTotal = new System.Diagnostics.Stopwatch();
            System.Console.WriteLine("****Set执行中......****");
            swTotal.Start();
            for (int i = 0; i < IMEI_NUM; i++)
            {
                //sw.Restart();
                SendDataCache.GetSendData((IMEI_PREX + i).ToString());
                //sw.Stop();
                //System.Console.WriteLine($"IMEI:{(IMEI_PREX + i).ToString()} takes {sw.Elapsed} ms");
            }
            swTotal.Stop();
            System.Console.WriteLine($"All IMEIS takes {swTotal.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// 测试主业务Removet耗时(6/ms)
        /// </summary>
        static void TestBusinessMainRemove()
        {
            string sourceData_Login = "*VK2015{0},AB#";//登陆包
            string sourceData_Heartbeat = "*VK2015{0},AH#";//心跳包
            System.Console.WriteLine("****Set执行中......****");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swTotal = new System.Diagnostics.Stopwatch();
            swTotal.Start();
            try
            {
                for (int i = 0; i < IMEI_NUM; i++)
                {
                    TerResultEntiy entity = new TerResultEntiy((IMEI_PREX + i).ToString(),
                                                   Encoding.ASCII.GetBytes(sourceData_Login),
                                                   ORDERLOGIN_PREX_REMOVE + i,
                                                   1,
                                                   0,
                                                   1,
                                                   DateTime.Now.Ticks)
                    { };
                    //sw.Restart();
                    SendDataCache.RemoveSendData(entity);
                    //sw.Stop();
                    //System.Console.WriteLine($"IMEI:{(IMEI_PREX + i).ToString()} takes {sw.Elapsed} ms");
                }
                swTotal.Stop();
                System.Console.WriteLine($"All IMEIS takes {swTotal.ElapsedMilliseconds} ms");
                ORDERLOGIN_PREX_REMOVE = ORDERLOGIN_PREX_REMOVE + 1000000;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
           
        }


        /// <summary>
        /// 获取主库数据
        /// </summary>
        static void GetRedisData()
        {
            System.Console.WriteLine("****指令数统计中****");
            long totalNum = 0;
            long totalOne = 0;
            long totalTwo = 0;
            long totalThree = 0;
            long totalDelelte = 0;
            List<TerResultEntiy> listReturn = new List<TerResultEntiy>();
            for (int i = 1; i < 50001; i++)
            {
                listReturn=SendDataCache.GetSendData((IMEI_PREX + i).ToString());
                if(listReturn != null)
                     totalNum += listReturn.Count;
                //totalOne += listReturn.Where(p => p.ReadCount == 3 && p.ErrorCode>0).ToList().Count;
                //totalTwo += listReturn.Where(p => p.ReadCount == 2 && p.ErrorCode > 0).ToList().Count;
                //totalThree += listReturn.Where(p => p.ReadCount == 1 && p.ErrorCode > 0).ToList().Count;
                //totalDelelte+= listReturn.Where(p => p.ReadCount == 0).ToList().Count;
            }
            System.Console.WriteLine($"重发1次：{totalOne}\r\n");
            System.Console.WriteLine($"重发2次：{totalTwo}\r\n");
            System.Console.WriteLine($"重发3次：{totalThree}\r\n");
            System.Console.WriteLine($"重发成功数：{totalDelelte}\r\n");
            System.Console.WriteLine($"总数：{totalNum}\r\n");
        }

        #endregion

        #region hash测试
        /// <summary>
        /// HashSet
        /// </summary>
        public static void HashSet()
        {
            HashOperator.Set("hash_1", "name", "xxx");
            HashOperator.Set("hash_1", "age", "20");
            HashOperator.Set("hash_1", "set", "男");

            List<string> hashListValue = HashOperator.GetHashValues("hash_1");
            for (int i = 0; i < hashListValue.Count; i++)
            {
                System.Console.WriteLine(hashListValue[i]);
            }
            System.Console.WriteLine(HashOperator.GetHashValues("hash_2").Count);
        } 
        #endregion

        public static long GetTimestamp()
        {
            //ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            //精确到毫秒
            return (long)ts.TotalMilliseconds; 
        }
    }

    public class Ter
    {
        public int TerId { get; set; }

        public string IMEI { get; set; }

        public string TerName { get; set; }


        public string TerTypeCode { get; set; }

        public bool IsOnLine { get; set; }

        public decimal Lat { get; set; }

        public decimal Lng { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string ExpireTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 累计次数
        /// </summary>
        public int number { get; set; }

        public byte[] data { get; set; }
    }


    /// <summary>
    /// 返回结果实例
    /// </summary>
    public class TerResultEntiy
    {
        public byte[] Data { get; private set; }  //下发已经组好包的数据
        public long OrderLogID { get; private set; }  //消息唯一标识
        public string IMEI { get; private set; }  //IMEI号
        public byte ProtoType { get; private set; }  //协议类型
        public int ReadCount { get; private set; }  //读取次数
        public byte ErrorCode { get; private set; }  //错误码

        public long SendTime { get; private set; }  //发送时间
        public long ReusltTime { get; private set; }  //应答时间（包括超时，MG的发送成功，发送失败，设备应答的时间）

        /// <summary>
        ///
        /// </summary>
        /// <param name="imei"></param>
        /// <param name="Msg"></param>
        /// <param name="orderLogID"></param>
        /// <param name="protoType"></param>
        /// <param name="readCount"></param>
        /// <param name="errorCode"></param>
        public TerResultEntiy(string imei, byte[] Msg, long orderLogID, byte protoType, int readCount, byte errorCode, long sendTime)
        {
            IMEI = imei;
            OrderLogID = orderLogID;
            ProtoType = protoType;
            Data = Msg;
            ReadCount = readCount;
            ErrorCode = errorCode;
            SendTime = sendTime;
            ReusltTime = DateTime.Now.Ticks;
        }
    }
}
