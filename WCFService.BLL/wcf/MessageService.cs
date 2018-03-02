using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCFService;
using System.Threading;

namespace WCFService.BLL
{
    public class MessageService : IMessageService
    {
        public string GetUserTokenInfor(string key)
        { 
             UserToken userEnity = new UserToken();
             TestService.UserDic.TryGetValue(key, out userEnity);
             if (userEnity != null)
             {
                 var callBack = OperationContext.Current.GetCallbackChannel<IDoCallBack>();
                 callBack.GetName("双工数据" + userEnity.UserName);
             }
             return "WCF服务_"+DateTime.Now.ToString("HHmmss:fff");
        }

        public string GetUserTokenCount(string key)
        {
            return TestService.UserDic.Count.ToString();
        }
    }

    public class DoCallBack : IDoCallBack
    {
        public void GetName(string name)
        {
            Console.WriteLine(name);
        }
    }

}
