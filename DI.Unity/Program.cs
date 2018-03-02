using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DI.Unity
{
    class Program
    {
        [Dependency]
        public IUserService IUser { get; set; }

        static void Main(string[] args)
        {
            var container = new UnityContainer();
            DependencyRegisterType.Container_Sys(ref container);

            IUserService IUser = container.Resolve<UserImpl>();


            IUser.Display("SSS");
            Console.ReadLine();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));//mvc注入
            
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);//WebAPi注入
        }
    }
}
