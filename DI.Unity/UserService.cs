using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Unity
{
    public interface IUserService
    {
        void Display(string mes);
    }

    public class UserImpl : IUserService
    {
        /// <summary>
        /// 属性注入
        /// </summary>
        [Dependency]
        public IUserDao IUserDao {get;set;}


        ///// <summary>
        ///// 构造函数注入
        ///// </summary>
        ///// <param name="UserDao"></param>
        //public UserImpl(IUserDao UserDao)
        //{
        //    IUserDao = UserDao;
        //}

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="mes"></param>
        public void Display(string mes)
        {
            IUserDao.Display(mes);
        }
    }
}
