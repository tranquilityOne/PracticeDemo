using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.BLL
{
    /// <summary>
    /// 单例模式实现
    /// </summary>
    public class WCFProxySingleTan : ClientBase<IMessageService>
    {
        //绑定协议
        public static readonly Binding helloWorldBinding = new NetTcpBinding();

        /// <summary>
        /// 通信地址
        /// </summary>
        public static EndpointAddress HelloAddress
        {
            get
            {
                var addr = "net.tcp://localhost:86/Hello";
                return new EndpointAddress(new Uri(addr));
            }
        }

        private static WCFProxySingleTan singleInstance;

        private WCFProxySingleTan() : base(helloWorldBinding, HelloAddress)
        { 
            
        }

        public static WCFProxySingleTan GetInstance()
        {
            if (singleInstance == null)
                singleInstance = new WCFProxySingleTan();
            return singleInstance;
        }
        
          /// <summary>
        /// 实现调用
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetUserTokenInfor(string key)
        {

            //双工模式
            InstanceContext instanceContext = new InstanceContext(new DoCallBack());
            using (ChannelFactory<IMessageService> factory = new DuplexChannelFactory<IMessageService>(instanceContext, helloWorldBinding, HelloAddress))
            {
                IMessageService proxy = factory.CreateChannel();
                return proxy.GetUserTokenInfor(key);
            }
            
            //非双工模式
            //return Channel.GetUserTokenInfor(key);
        }
    }
}
