using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMQ.Main.ProxyConfig;

namespace RMQ.Model
{
    [RabbitMq("Lee.QueueName", ExchangeName = "Lee.ExchangeName", IsPersistence = false)]
    public class MessageModel
    {
        public string Msg { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
