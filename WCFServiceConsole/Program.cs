using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService;
using WCFService.BLL;
using System.Threading;

namespace WCFServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxNum = 100000;
            for (int i = 1; i <= maxNum; i++)
            {               
                //WCFProxy proxy = new WCFProxy();
                Console.WriteLine( WCFProxySingleTan.GetInstance().GetUserTokenInfor(string.Format("Name_{0}", i)));
            }
        
            //Console.WriteLine(TestService.UserDic.Keys.Count);
            Console.ReadKey();
        }
    }


   
}
