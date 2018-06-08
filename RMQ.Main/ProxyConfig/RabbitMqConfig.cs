using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQ.Main.ProxyConfig
{
    /// <summary>
    /// Mq配置信息
    /// </summary>
    public class MqConfig
    {
        public string Host { get; set; }
        public ushort HeartBeat { get; set; }
        public bool AutomaticRecoveryEnabled { get; set; }
        public TimeSpan NetworkRecoveryInterval { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
