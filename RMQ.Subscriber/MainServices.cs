using FrameWork.Extension;
using RMQ.Main;
using RMQ.Main.ProxyConfig;
using RMQ.Model;
using System;
using System.Collections.Generic;
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

        public bool Start()
        {
            //_rabbitMqProxy.SubscribeBroadcastDefault();

            _rabbitMqProxy.SubscribeBroadCast<MessageModel>(msg =>
            {
                Console.WriteLine(msg.flag);
            });

            return true;
        }

        public bool Stop()
        {
            _rabbitMqProxy.Dispose();
            return true;
        }
    }
}
