using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e3net.MongodbServer
{
    /// <summary>
    /// 类的特性 描述特性(单体）
    /// </summary> 
    [AttributeUsage(AttributeTargets.Class)]
    public class MDbFactory : Attribute
    {
        public readonly string ConnectionName;

        public MDbFactory(string connectionName)
        {
            ConnectionName = connectionName;
        }
        /// <summary>
        /// 获取类的描述 值的第一个
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetKeyFrom(object target)
        {
            var objectType = target.GetType();
            var attributes = objectType.GetCustomAttributes(typeof(MDbFactory), true);
            if (attributes.Length > 0)
            {
                var attribute = (MDbFactory)attributes[0];
                return attribute.ConnectionName;
            }
            else {
                return "";
            }
        }
    }
}
