using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisServer.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(() =>
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                //AsyncAddLists(100000);
                Pop(100000);
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                System.Console.WriteLine("总耗时:" + ts2.TotalMilliseconds);
            });
            System.Console.ReadKey();
        }

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

        static void Push(int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                HashOperator.PushString("list_pop", i.ToString());
            }
        }

        static void Pop(int loops)
        {
            for (int i = 0; i < loops; i++)
            {
                HashOperator.PopString("list_pop");
            }
        }

    }
}
