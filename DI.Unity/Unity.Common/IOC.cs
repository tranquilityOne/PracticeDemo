using DI.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianNeng.Unity
{

    public static class IOC
    {
        private static UnityContainer container;
        static IOC()
        {
            container = new UnityContainer();
            DependencyRegisterType.Container_Sys(ref container);


            //默认的配置文件获取  
            //container = new UnityContainer();
            //UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //section.Configure(container, "myContainer");
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T R<T>()
        {
            return R<T>(null);
        }

        public static T R<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return container.Resolve<T>();
            return container.Resolve<T>(name);
        }
      //使用：  IUser iuser = IOC.R<IUser>(); 
    }
}
