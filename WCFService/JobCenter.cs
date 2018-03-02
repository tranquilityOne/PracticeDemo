using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService.BLL;

namespace WCFService
{
    public class JobCenter
    {
        public static void Start()
        {
            TestService.Start();
            WCFHost host = new WCFHost();
            host.open();
        }
    }
}
