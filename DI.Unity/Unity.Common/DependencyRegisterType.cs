using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Practices.Unity;



namespace DI.Unity
{
    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            //container.RegisterType<ISysSampleBLL, SysSampleBLL>();//样例
            container.RegisterType<IUserDao, UserDaoImpl>();

            container.RegisterType<IUserService, UserImpl>();

        }
    }
}