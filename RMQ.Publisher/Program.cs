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

            //rabbitMqProxy.PublishBraodcastDefault();

            #region 封装逻辑
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            try
            {
                sw.Start();
                Console.WriteLine("*****************消息发布开始******************");
                for (int i = 0; i < 10000; i++)
                {
                    var log = new MessageModel
                    {
                        CreateDateTime = DateTime.Now,
                        Msg = $"hello world {i}",
                        flag = i
                    };
                    rabbitMqProxy.PublishBroadCast(log);
                    System.Threading.Thread.Sleep(200);
                }
                sw.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("总耗时:" + sw.Elapsed.Milliseconds);
            rabbitMqProxy.Dispose();
            #endregion
        }

        private static string Input()
        {
            Console.WriteLine("请输入信息：");
            var input = Console.ReadLine();
            return input;
        }
    }   
}
