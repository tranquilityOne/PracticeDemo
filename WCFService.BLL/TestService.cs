using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace WCFService.BLL
{
    public class TestService
    {
        public static ConcurrentDictionary<string, UserToken> UserDic=new ConcurrentDictionary<string,UserToken>();

        public static int maxNum = 100000;
        public static void Start()
        {
            try
            {
                for (int i = 0; i < maxNum; i++)
                {
                    Set(i);
                }  
            }
            catch (Exception ex)
            {
                Log4netHelper.Error("TestService.Start", ex.Message, ex);
            }           
        }

        public static void Set(int i)
        {
            UserToken token = new UserToken()
            {
                Id = string.Format("key_{0}", i.ToString()),
                UserName = string.Format("Name_{0}", i.ToString()),
                Age = i,
            };
            UserDic.TryAdd(token.UserName,token);
        }
    }

    public class UserToken
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public long Age { get; set; }
    }
}
