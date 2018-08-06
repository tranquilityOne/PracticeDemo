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

        public int flag { get; set; }
    }

    [RabbitMq("TER_MsgParse", ExchangeName = "MsgParse.exchange", IsPersistence = false)]
    public class MsgProcessorModel
    {
        public string Msg { get; set; }

        public DateTime CreateDateTime { get; set; }
    }


    /// <summary>
    /// 升级日志类
    /// </summary>
    [RabbitMq("TerUpgradeLog", ExchangeName = "TerUpgradeLog.exchange", IsPersistence = false)]
    public class UpgradeLog
    {
        /// <summary>
        /// 日志Id
        /// </summary>
        public long LogId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        public string IMEI { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public int TerId { get; set; }
        /// <summary>
        /// 请求原始字节数组
        /// </summary>
        public byte[] RequestRawData { get; set; }

        /// <summary>
        /// 请求报文
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// 应答报文
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// 应答状态码。
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 升级服务器外网IP和端口
        /// </summary>
        public string ComHost { get; set; }

        /// <summary>
        /// 升级服务器内网IP和端口
        /// </summary>
        public string LocalEndPoint { get; set; }

        /// <summary>
        /// 升级任务ID
        /// </summary>
        public int TaskId { get; set; }
    }
}
