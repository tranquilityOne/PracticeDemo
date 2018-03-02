using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Unity
{
    public interface IUserDao
    {
        void Display(string mes);
    }

    /// <summary>
    /// 实现类
    /// </summary>
    public class UserDaoImpl : IUserDao
    {       
        public void Display(string mes)
        {
            Console.WriteLine(mes);
        }
    }
}
