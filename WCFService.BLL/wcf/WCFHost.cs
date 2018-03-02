using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFService;
namespace WCFService.BLL
{
    public class WCFHost
    {
        /// 定义一个服务对象
        /// </summary>
        private ServiceHost _myhost;
        public ServiceHost Myhost
        {
            get { return _myhost; }
        }

        public WCFHost()
        {
            CreateHelloServiceHost();
        }

        public string BaseAddress = "net.tcp://localhost:86";//基地址
        public const string HelloServiceAddress = "Hello";//可选地址


        //服务契约实现类型
        public static readonly Type ServiceType = typeof(MessageService);
        //服务契约接口
        public static readonly Type ContractType = typeof(IMessageService);

        protected void CreateHelloServiceHost()
        {
            try
            {
                //创建服务对象
                _myhost = new ServiceHost(ServiceType, new Uri[] { new Uri(BaseAddress) });
                //给当前宿主对象添加终结点
                _myhost.AddServiceEndpoint(ContractType, new NetTcpBinding(), HelloServiceAddress);
            }
            catch (Exception ex)
            {
                Log4netHelper.Debug("CreateHelloServiceHost", ex.Message);
            }
          
        }

        //打开服务
        public void open()
        {
            try
            {
                _myhost.Open();
                Log4netHelper.Debug(GetType().FullName, "WCF服务已启动");   
            }
            catch (Exception ex)
            {
                Log4netHelper.Debug("WCF Open", ex.Message);
            }
                
        }
        public void Dispose()
        {
            if (_myhost != null)
            {
                (_myhost as IDisposable).Dispose();
            }
        }
    }
}
