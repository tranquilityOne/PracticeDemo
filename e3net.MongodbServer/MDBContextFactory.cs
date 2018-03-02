using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using MongoDB.Driver.Linq;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using e3net.Common;
namespace e3net.MongodbServer
{
    /// <summary>
    /// mongodb数据 工厂
    /// </summary>
    public class MDBContextFactory : IMDBContextFactory
    {
        #region 创建 EF上下文 对象，在线程中共享 一个 上下文对象 + DbContext GetDbContext()
        /// <summary>
        /// 从上下文获取一个操作对像
        /// </summary>
        /// <returns></returns>
        public IMongoDatabase GetIDbContext()
        {

            string[] DBAhou = SysConfig.MDB_User_Pwd_Host_Port;
            return GetIDbContext(DBAhou[0], DBAhou[1], DBAhou[2], DBAhou[3], int.Parse(DBAhou[4]));

        }


        /// <summary>
        /// 从上下文获取一个操作对像
        /// </summary>
        /// <param name="ConnectName">配置文件连接名</param>
        /// <returns></returns>
        public IMongoDatabase GetIDbContext(string ConnectName)
        {
            string[] DBAhou = ConnectName.ToAppSetting().Split(':');
            return GetIDbContext(DBAhou[0], DBAhou[1], DBAhou[2], DBAhou[3], int.Parse(DBAhou[4]));
        }


        /// <summary>
        /// 从上下文获取一个操作对像
        /// </summary>
        /// <param name="DBName">数据库名</param>
        /// <param name="User">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="Host">ip</param>
        /// <param name="Port">端口</param>
        /// <returns></returns>
        public IMongoDatabase GetIDbContext(string DBName, string User, string pwd, string Host, int Port)
        {
            //从当前线程中 获取 EF上下文对象
            IMongoDatabase dbContext = CallContext.GetData("I" + DBName) as IMongoDatabase;
            if (dbContext == null)
            {
                MongoCredential cr = MongoCredential.CreateCredential(DBName, User, pwd);
                var settings = new MongoClientSettings
                {
                    Credentials = new[] { cr },
                    Server = new MongoServerAddress(Host, Port)
                };
                var mongoClient = new MongoClient(settings);
                dbContext = mongoClient.GetDatabase(DBName);
                //将新创建的 ef上下文对象 存入上下文
                CallContext.SetData("I" + DBName, dbContext);
            }
            return dbContext;
        }

        #endregion




    }
}
