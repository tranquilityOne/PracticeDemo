using RMQ.Main;
using RMQ.Main.ProxyConfig;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitMqProxy = new RabbitMqService(new MqConfig
            {
                AutomaticRecoveryEnabled = true,
                HeartBeat = 60,
                NetworkRecoveryInterval = new TimeSpan(60),
                Host = "localhost",
                UserName = "guest",
                Password = "guest"
            });

            var input = Input();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var log = new MessageModel
            {
                CreateDateTime = DateTime.Now,
                Msg = input
            };
            for (int i = 0; i < 500000; i++)
            {
                rabbitMqProxy.Publish(log);
            }
            sw.Stop();
            Console.WriteLine("总耗时:"+sw.Elapsed.Milliseconds);

            #region 单个数据
            //while (input != "exit")
            //{
            //    var log = new MessageModel
            //    {
            //        CreateDateTime = DateTime.Now,
            //        Msg = input
            //    };
            //    rabbitMqProxy.Publish(log);
            //    input = Input();
            //} 
            #endregion
            rabbitMqProxy.Dispose();
        }

        private static string Input()
        {
            Console.WriteLine("请输入信息：");
            var input = Console.ReadLine();
            return input;
        }
    }   
}
