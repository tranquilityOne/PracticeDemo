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
            //string key = "send_bl_1";
            //long cout = HashOperator.QueueCount(key);
            List<string> list = new List<string>();
            list.Add("宁浩");
            list.Add("大家好");
            sadd("list_1", list);
            System.Console.ReadKey();
        }

        public static void sadd(string key,List<string> list)
        {
            HashOperator.SAdd(key, list);
        }
    }
}
