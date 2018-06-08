using RMQ.Main.ProxyConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Model
{
    [RabbitMq("Lee.Rpc.QueueName", ExchangeName = "Lee.Rpc.ExchangeName", IsPersistence = false)]
    public class RpcMsgModel
    {
        public string Msg { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
