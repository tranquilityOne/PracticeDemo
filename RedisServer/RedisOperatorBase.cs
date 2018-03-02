using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace RedisServer
{
    /// <summary>
    /// RedisOperatorBase类，是redis操作的基类，继承自IDisposable接口，主要用于释放内存
    /// </summary>
    public abstract class RedisOperatorBase : IDisposable
    {
        /// <summary>
        /// 操作对象
        /// </summary>
        //protected IRedisClient Redis { get; private set; }
        private bool _disposed = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisClient">操作对象 为空为默认连接</param>
        protected RedisOperatorBase()
        {

            //Redis = RedisManager.GetClient();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    //Redis.Dispose();
                    //Redis = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
