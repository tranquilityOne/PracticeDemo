using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.BLL
{
    /// <summary>
    /// 双工模式
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IDoCallBack))]
    public interface IMessageService
    {
        /// <summary>
        /// 请求与答复模式(默认)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [OperationContract]
        string GetUserTokenInfor(string key);

        /// <summary>
        /// 单向模式(不需要等待)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetUserTokenCount(string key);
    }

    /// <summary>
    /// 回调接口
    /// </summary>
    public interface IDoCallBack
    {
        [OperationContract(IsOneWay = true)]
        void GetName(string name);
    }
}
