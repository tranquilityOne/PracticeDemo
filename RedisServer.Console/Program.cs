using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using e3net.Common.Json;
namespace RedisServer.Console
{
    class Program
    {
        static readonly int number = 100000;
        static readonly int expireTime = 30;//过期时间10秒
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

            while (true)
            {
                string input = System.Console.ReadLine();
                if (input == "c")
                    System.Console.Clear();
                else if (input == "t")
                    RunTask();
                else if (input == "delete")
                    HashOperator.RemoveByPattern("list_3479*");
                else
                    LoopGetKey(number);
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
                    HashOperator.Set<string>(string.Format("key_{0}_{1}$Expire", i, k),"",DateTime.Now.AddSeconds(expireTime));
                }
            }
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

        static void LoopGetKey(int num)
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
}
