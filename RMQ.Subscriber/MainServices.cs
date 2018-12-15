using FrameWork.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RMQ.Main;
using RMQ.Main.ProxyConfig;
using RMQ.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Subscriber
{
    public class MainService
    {
        private readonly RabbitMqService _rabbitMqProxy;
        public MainService()
        {
            _rabbitMqProxy = new RabbitMqService(new MqConfig
            {
                AutomaticRecoveryEnabled = true,
                HeartBeat = 60,
                NetworkRecoveryInterval = new TimeSpan(60),
                Host = "localhost",
                UserName = "guest",
                Password = "guest"
            });
        }

        public void Start()
        {
            //_rabbitMqProxy.SubscribeBroadcastDefault();

            #region 原生方法          
            try
            {
                var factory = new ConnectionFactory() { HostName = ConfigurationManager.AppSettings["rabbitMq_HostName"] 
                                                        ,Port= Convert.ToInt32(ConfigurationManager.AppSettings["rabbitMq_Port"])
                                                        ,UserName = ConfigurationManager.AppSettings["rabbitMq_UserName"]
                                                        , Password = ConfigurationManager.AppSettings["rabbitMq_Password"]
                };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            var exchangeName = ConfigurationManager.AppSettings["rabbitMq_ExchangeName"];
                            channel.ExchangeDeclare(exchange: exchangeName, type: ConfigurationManager.AppSettings["rabbitMq_Type"], durable: true);
                            //默认exchange
                            //channel.ExchangeDeclare(exchange:"/", type: "direct", durable: true);

                            //获取默认队列
                            //var queueName = channel.QueueDeclare().QueueName; 
                            var queueName = ConfigurationManager.AppSettings["rabbitMq_QueueName"];
                            channel.QueueBind(queue: queueName,
                                  exchange: exchangeName,
                                  routingKey: "");
                            var consumer = new EventingBasicConsumer(channel);
                            consumer.Received += (model, ea) =>
                            {
                                var body = ea.Body;
                                var message = Encoding.UTF8.GetString(body);
                                var routingKey = ea.RoutingKey;  
                            };
                            channel.BasicConsume(queue: queueName,
                                                 autoAck: true,
                                                 consumer: consumer);
                        }
                        Console.WriteLine(" Press [enter] to exit.");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            #endregion

            #region 封装方法
            //for (int i = 0; i < 5; i++)
            //{
            //    _rabbitMqProxy.Subscribe($"upgradelog_queue_" + i, true, () => { }, true);
            //} 
            #endregion
        }

        public bool Stop()
        {
            _rabbitMqProxy.Dispose();
            return true;
        }
    }
}
