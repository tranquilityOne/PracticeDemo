using RMQ.Main;
using RMQ.Main.ProxyConfig;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e3net.Common;
using FrameWork.Extension;
using RabbitMQ.Client;
using System.Configuration;

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
                Password = "guest",

            });
            System.Console.WriteLine("u 升级日志队列;r 设备应答队列;s 升级状态队列;");
            while (true)
            {
                var input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "r":
                        publishSendReply(rabbitMqProxy);
                        break;
                    case "s":
                        publishUpgradeStatus(rabbitMqProxy);
                        break;
                    case "u":
                        publishUpgradeLogSchedule(rabbitMqProxy);
                        break;
                    case "q":
                        return;
                }               
            }          
        }

        static void publishUpgradeLogSchedule(RabbitMqService rabbitMqProxy)
        {
            int num =1;
            Task[] tasks = new Task[num];
            for (int i = 0; i < num; i++)
            {
                tasks[i] = Task.Factory.StartNew((n) =>
                {
                    //publishDefault($"upgradelog.exchange_192.168.30.204:20260_{n}", $"upgradelog_queue_192.168.30.204:20260_{n}");
                    publishDefault(ConfigurationManager.AppSettings["rabbitMq_ExchangeName"], ConfigurationManager.AppSettings["rabbitMq_QueueName"]);
                }, i);
            }
            Task.WaitAll(tasks);
        }

        #region 升级日志模拟发布
        static void publishUpgradeLog(string exchangeName,string queueName)
        {
            var rabbitMqProxyLog = new RabbitMqService(new MqConfig
            {
                AutomaticRecoveryEnabled = true,
                HeartBeat = 60,
                NetworkRecoveryInterval = new TimeSpan(60),
                Host = "localhost",
                UserName = "guest",
                Password = "guest"
            });
            var beingIMEIINumber = 100000000000000;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            try
            {
                sw.Start();
                Console.WriteLine($"*****************{queueName}消息发布开始******************");
                string imei = string.Empty;
                int terId = 0;

                try
                {
                    while (true)
                    {
                        for (int i = 0; i < 50000; i++)
                        {
                            imei = (beingIMEIINumber + i).ToString();

                            var log = new UpgradeLog
                            {
                                TerId = 0,
                                RequestRawData = GetUpgradeBytes(imei),
                                StatusCode = 0,
                                LogId = 100001,
                                IMEI = imei,
                                Request = "",
                                ComHost = "",
                                LocalEndPoint = "192.168.0.38:8886",
                                CreatedTime = DateTime.Now,
                                FirewareType = 0,
                                HardwareVer = "WK_MT6260D_T808_1V0",
                                SoftwareVer= "9300_2016/10/11,VKEL_T808_20161011",
                                AppVer = "9315"
                            };
                            rabbitMqProxyLog.Publish(exchangeName, queueName, "", log.ToJson());
                            //rabbitMqProxyLog.Publish(log);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                
                sw.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("总耗时:" + sw.Elapsed.Milliseconds);
        }

        static void publishDefault(string exchangeName,string queueName)
        {
            var factory = new ConnectionFactory() {
                HostName = ConfigurationManager.AppSettings["rabbitMq_HostName"]                                      ,
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["rabbitMq_Port"])                                   ,
                UserName = ConfigurationManager.AppSettings["rabbitMq_UserName"]                                      ,
                Password = ConfigurationManager.AppSettings["rabbitMq_Password"]};

            using (IConnection conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchangeName, type: ConfigurationManager.AppSettings["rabbitMq_Type"], durable:true);
                    //指定队列
                    channel.QueueBind(queue: queueName,
                                 exchange: exchangeName,
                                 routingKey: "");
                    try
                    {
                        var beingIMEIINumber = 100000000000000;                       
                        string imei = "";

                        //1秒=10000000ticks
                        //1毫秒=10000tikcs

                        //1ticks = 100毫微秒=0.1微秒=0.00001毫秒

                        #region 循环批量
                        while (true)
                        {
                            for (int i = 60000; i < 70000; i++)
                            {
                                imei = (beingIMEIINumber + i).ToString();

                                var log = new UpgradeLog
                                {
                                    TerId = 0,
                                    RequestRawData = GetUpgradeBytes(imei),
                                    StatusCode = 0,
                                    LogId = 100001,
                                    IMEI = imei,
                                    Request = "",
                                    CreatedTime = DateTime.Now,
                                    LocalEndPoint = "192.168.30.204:20260",
                                    FirewareType = 0,
                                    HardwareVer = "WK_MT6260D_T808_1V0",
                                    SoftwareVer = "9300_2016/10/11,VKEL_T808_20161011",
                                    AppVer = "9315"
                                };
                                var message = Encoding.ASCII.GetBytes(log.ToJson());
                                channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: message);

                            }
                            //System.Threading.Thread.Sleep(2000);
                        }
                        #endregion

                        #region 单个测试
                        //var log = new UpgradeLog
                        //{
                        //    TerId = 0,
                        //    RequestRawData = GetUpgradeBytes("868500023849383"),
                        //    StatusCode = 0,
                        //    LogId = 100001,
                        //    IMEI = "868500023849383",
                        //    Request = "",
                        //    CreatedTime = DateTime.Now,
                        //    LocalEndPoint = "192.168.30.204:20260",
                        //    FirewareType = 0,
                        //    Scheme= "VKEL_MT2503D",//方案号只去下划线前两位
                        //    HardwareVer = "VKEL_MT2503D_S28_1V0",
                        //    SoftwareVer = "BP00_2018/07/14,VKEL_S107_20180822",
                        //    AppVer = "BP24"
                        //};
                        //var message = Encoding.ASCII.GetBytes(log.ToJson());
                        //channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: message); 
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public static byte[] GetUpgradeBytes(string imei)
        {
            List<byte> listBytes = new List<byte>();
            var cmdHead = "*VK201{0},DU&VVKEL_MT2503D_S28_1V0,BP00_2018/07/14,VKEL_S107_20180822,BP24";
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
            int loop = 100;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            string exchangeName = "terreply.exchange";
            string queueName = "terreply_queue";
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
                        //rabbitMqProxy.Publish(exchangeName, queueName,"",GetSendReplyStr((beingIMEIINumber+i).ToString(), logId));
                        rabbitMqProxy.Publish(exchangeName, queueName, "", GetSendReplyStr((beingIMEIINumber + i).ToString(), logId));
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

        public static byte[] GetSendReplyBytes(string imei, int logId)
        {
            List<byte> listBytes = new List<byte>();
            listBytes.AddRange(Encoding.ASCII.GetBytes(string.Format("*VK200{0}YDF&T",imei)));
            listBytes.AddRange(logId.ToHexPadLeft(8).HexStringToByte());
            listBytes.AddRange(Encoding.ASCII.GetBytes("#"));         
            return listBytes.ToArray<byte>();
        }

        public static string GetSendReplyStr(string imei, int logId)
        {
            return string.Format("*VK200{0}YDF&T", imei)+ logId.ToHexPadLeft(8)+ "#";
          
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
                rabbitMqProxy.Publish(exchangeName, queueName, "", $"4377892;2;500000000000000;3612;0;VKEL_MT2503D_T100_1V0;FE00_2017/11/29;FE13");
                rabbitMqProxy.Publish(exchangeName, queueName, "", $"4377880;2;867282032269330;3612;0;VKEL_MT2503D_T100_1V0;FE00_2017/11/29;FE13");
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
