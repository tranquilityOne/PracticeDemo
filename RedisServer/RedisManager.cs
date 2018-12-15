using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using System.Collections.Concurrent;
using System.Configuration;

namespace RedisServer
{
    /// <summary>
    /// RedisManager类主要是创建链接池管理对象的
    /// </summary>
    public class RedisManager
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static string RedisPath;
        /// <summary>
        /// 所选库
        /// </summary>
        private static int DBIndex;
         //[ThreadStatic]
        private static PooledRedisClientManager _prcm;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["RedisConnectionString"]))
            {
                throw new Exception("Redis connection string is empty");
            }
            RedisPath = ConfigurationManager.AppSettings["RedisConnectionString"];
            Int32.TryParse(ConfigurationManager.AppSettings["RedisDBIndex"], out DBIndex);               
            _prcm = CreateManager(new string[] { RedisPath }, new string[] { RedisPath });
        }

        /// <summary>
        /// 链接池
        /// </summary>
        /// <param name="readWriteHosts">可写的Redis链接地址 password@ip:port </param>
        /// <param name="readOnlyHosts">可读的Redis链接地址 password@ip:port </param>
        /// <returns></returns>
        private static PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //WriteServerList：可写的Redis链接地址。
            //ReadServerList：可读的Redis链接地址。
            //MaxWritePoolSize：最大写链接数。
            //MaxReadPoolSize：最大读链接数。
            //AutoStart：自动重启。         

            // 支持读写分离，均衡负载 
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 1024, // “写”链接池链接数 
                MaxReadPoolSize = 1024, // “读”链接池链接数 
                AutoStart = true,
                DefaultDb = DBIndex,
            });
        }

        private static IEnumerable<string> SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static IRedisClient GetClient()
        {
            if (_prcm == null)
            {
                CreateManager();
            }
            return _prcm.GetClient();
        }

    }
}
