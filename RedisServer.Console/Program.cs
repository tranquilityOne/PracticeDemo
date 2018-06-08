using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RedisServer.Console
{
    class Program
    {
        static readonly int number = 100000;
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

            #region 插入数据
            Task.Run(() =>
            {
                try
                {
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    //AsyncAddLists(number);
                    //Push(number);
                    Pop(number);
                    sw.Stop();
                    TimeSpan ts2 = sw.Elapsed;
                    System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.StackTrace);
                }
            });
            #endregion
            System.Console.ReadKey();
        }

        #region 存储测试
        /// <summary>
        /// 读取数据
        /// </summary>
        static void Woker(object obj)
        {
            int number = (Int32)obj;
            System.Console.WriteLine("woker thread begin");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            #region 查询所有
            //for (int i = 0; i < number; i++)
            //{
            //    for (int k = 0; k < 10; k++)
            //    {
            //        HashOperator.Get<string>(string.Format("key_{0}_{1}", i, k));
            //    }
            //}
            HashOperator.Get<string>(string.Format("key_{0}_{1}", 1, 1));
            #endregion
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
        }

        /// <summary>
        ///插入数据 
        /// </summary>
        /// <param name="num"></param>
        static void AsyncAddLists(int num)
        {
            for (int i = 0; i < num; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    HashOperator.Set<Ter>(string.Format("key_{0}_{1}", i, k), new Ter()
                    {
                        TerId = i,
                        TerName = $"Name_{i}",
                        IMEI = i.ToString(),
                        IsOnLine = true,
                        Lat = 22.35643434m,
                        Lng = 112.54754875m,
                        TerTypeCode = "测试设备"
                    });
                }
            }
        }
        #endregion

        #region 队列测试
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
    }
}
