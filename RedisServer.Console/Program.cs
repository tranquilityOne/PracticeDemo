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
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(Woker);
                thread.Start();
            }

            //Task.Run(() =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //        sw.Start();
            //        AsyncAddLists(100000);
            //        //Pop(100000);
            //        sw.Stop();
            //        TimeSpan ts2 = sw.Elapsed;
            //        System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Console.WriteLine(ex.StackTrace);
            //    }
              
            //});
            System.Console.ReadKey();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        static void Woker()
        {
            System.Console.WriteLine("woker thread begin");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    HashOperator.Get<string>(string.Format("key_{0}_{1}", i, k));
                }
            }
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
                    HashOperator.Set<string>(string.Format("key_{0}_{1}",i,k),"hello redis");
                }
            }           
        }


        /// <summary>
        /// 队列弹入数据
        /// </summary>
        /// <param name="loops"></param>
        static void Push(int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                HashOperator.PushString("list_pop", i.ToString());
            }
        }

        /// <summary>
        /// 队列弹出数据
        /// </summary>
        /// <param name="loops"></param>
        static void Pop(int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                HashOperator.PopString("list_pop");
            }
        }

    }
}
