using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            MainService services = new MainService();
            Console.WriteLine("**********订阅服务开始**********");
            services.Start();
            Console.ReadKey();
        }
    }
}
