using RMQ.Main;
using RMQ.Main.ProxyConfig;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3net.Common;

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
            //publishUpgradeLog(rabbitMqProxy);
            //publishSendReply(rabbitMqProxy);

            publishUpgradeStatus(rabbitMqProxy);

            rabbitMqProxy.Dispose();
            Console.ReadKey();
          
        }

        #region 升级日志模拟发布
        static void publishUpgradeLog(RabbitMqService rabbitMqProxy)
        {
            var beingIMEIINumber = 500000000000000;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            try
            {
                sw.Start();
                Console.WriteLine("*****************消息发布开始******************");
                string imei = string.Empty;
                int terId = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (i % 2 == 0)
                    {
                        imei = "867282032356921";
                        terId = 433152;
                    }
                    else
                    {
                        imei = "867282031091891";
                        terId = 433408;
                    }
                    var log = new UpgradeLog
                    {
                        TerId = terId,
                        RequestRawData = GetUpgradeBytes(imei),
                        StatusCode = 0,
                        LogId = 100001,
                        IMEI = imei,
                        Request = "",
                        ComHost = "",
                        LocalEndPoint = ""
                    };
                    rabbitMqProxy.Publish(log);
                    System.Threading.Thread.Sleep(100);
                }
                sw.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("总耗时:" + sw.Elapsed.Milliseconds);
        }

        public static byte[] GetUpgradeBytes(string imei)
        {
            List<byte> listBytes = new List<byte>();
            var cmdHead = "*VK201{0},DU&VVKEL_MT6260D_1V0,0000_2015/01/15,VKEL_T7_20140115,0100";
            listBytes.AddRange(Encoding.ASCII.GetBytes(string.Format(cmdHead, imei)));
            //&B指令
            listBytes.AddRange(Encoding.ASCII.GetBytes("&B"));
            string hexB = "025B55AFC502085B55AFC50102";
            listBytes.AddRange(hexB.HexStringToByte());
            //&C
            listBytes.AddRange(Encoding.ASCII.GetBytes("&C1"));
            //&I 
            listBytes.AddRange(Encoding.ASCII.GetBytes("&I898602B3131650454831"));
            //&M
            listBytes.AddRange(Encoding.ASCII.GetBytes("&M368"));
            //&N
            listBytes.AddRange(Encoding.ASCII.GetBytes("&N36"));
            //&O
            listBytes.AddRange(Encoding.ASCII.GetBytes("&O4095"));
            //&P
            listBytes.AddRange(Encoding.ASCII.GetBytes("&P1"));
            //&Y
            listBytes.AddRange(Encoding.ASCII.GetBytes("&Y12333"));
            //&Z
            listBytes.AddRange(Encoding.ASCII.GetBytes("&Z01"));
            listBytes.AddRange(Encoding.ASCII.GetBytes("#"));
            return listBytes.ToArray<byte>();
        }

        #endregion

        #region 指令应答队列发布
        static void publishSendReply(RabbitMqService rabbitMqProxy)
        {
            var beingIMEIINumber = 500000000000000;
            int loop = 2;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            string exchangeName = "TerSendReply.exchange";
            string queueName = "TerSendReply";
            try
            {
                sw.Start();
                Console.WriteLine("*****************消息发布开始******************");
                string imei = string.Empty;
                int logId = 0;
                for (int i = 0; i < loop; i++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        logId++;
                        rabbitMqProxy.Publish(exchangeName, queueName,"",GetSendReplyStr((beingIMEIINumber+i).ToString(), logId));
                    }                    
                }
               
                System.Threading.Thread.Sleep(100);
                sw.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("总耗时:" + sw.Elapsed.Milliseconds);
        }

        public static byte[] GetSendReplyStr(string imei, int logId)
        {
            List<byte> listBytes = new List<byte>();
            listBytes.AddRange(Encoding.ASCII.GetBytes(string.Format("*VK200{0}YDF&T",imei)));
            listBytes.AddRange(logId.ToHexPadLeft(8).HexStringToByte());
            listBytes.AddRange(Encoding.ASCII.GetBytes("#"));         
            return listBytes.ToArray<byte>();
        }
        #endregion

        #region 升级日志状态
        static void publishUpgradeStatus(RabbitMqService rabbitMqProxy)
        {
            var beingIMEIINumber = 500000000000000;
            int loop = 2;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            string exchangeName = "upgradestatus.exchange";
            string queueName = "upgradestatus_queue";
            try
            {
                sw.Start();
                Console.WriteLine("*****************消息发布开始******************");
                //升级任务详情ID;升级状态;IMEI;固件ID
                rabbitMqProxy.Publish(exchangeName, queueName, "", $"4377892;2;500000000000000;3612");
                rabbitMqProxy.Publish(exchangeName, queueName, "", $"4377880;2;867282032269330;3612");
                //rabbitMqProxy.Publish(exchangeName, queueName, "", $"3567095;2;500000000000000");
                System.Threading.Thread.Sleep(100);
                sw.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("总耗时:" + sw.Elapsed.Milliseconds);
        }
        #endregion

        private static string Input()
        {
            Console.WriteLine("请输入信息：");
            var input = Console.ReadLine();
            return input;
        }
    }   
}
