using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e3net.MongodbServer
{
    /// <summary>
    /// mongodb数据上下文 工厂
    /// </summary>
    public interface IMDBContextFactory
    {
        /// <summary>
        /// 从上下文获取一个操作对像
        /// </summary>
        /// <returns></returns>
         IMongoDatabase GetIDbContext();


        /// <summary>
        /// 从上下文获取一个操作对像
        /// </summary>
        /// <param name="ConnectName">配置文件连接名</param>
        /// <returns></returns>
         IMongoDatabase GetIDbContext(string ConnectName);

        /// <summary>
        /// 从上下文获取一个操作对像
        /// </summary>
        /// <param name="DBName">数据库名</param>
        /// <param name="User">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="Host">ip</param>
        /// <param name="Port">端口</param>
        /// <returns></returns>
         IMongoDatabase GetIDbContext(string DBName, string User, string pwd, string Host, int Port);

    }
}
