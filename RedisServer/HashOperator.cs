using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Text;
using ServiceStack.Redis;

namespace RedisServer
{
    /// <summary>
    /// HashOperator类，是操作哈希表类。继承自RedisOperatorBase类
    /// </summary>
    public class HashOperator : RedisOperatorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisClient">操作对象 为空为默认连接</param>
        public HashOperator()
            : base()
        {

        }

        #region 基础 单类型
        #region add
        /// <summary>
        /// 添加指定类型 设置过期的具体时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键 </param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">在什么时候 过期</param>
        /// <returns></returns>
        public static bool Add<T>(string key, T value, DateTime expiresAt)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Add(key, value, expiresAt);
            }

        }
        /// <summary>
        /// 添加指定类型 设置 多久后过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn">时间差</param>
        /// <returns></returns>
        public static bool Add<T>(string key, T value, TimeSpan expiresIn)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Add(key, value, expiresIn);
            }
        }

        /// <summary>
        /// 添加指定类型 无期限
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Add<T>(string key, T t)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Add(key, t);
            }
        }

        #endregion

        #region set

        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加 设置过期的具体时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键 </param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">在什么时候 过期</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T value, DateTime expiresAt)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Set(key, value, expiresAt);
            }

        }
        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加 多久后过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn">时间差</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Set(key, value, expiresIn);
            }
        }

        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加 无期限
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Set(key, t);
            }
        }

        /// <summary>
        /// 设置某个缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public static void SetExpire(string key, DateTime datetime)
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.ExpireEntryAt(key, datetime);
            }
        }

        #endregion

        /// <summary>
        /// 获取指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Get<T>(key);
            }
        }

        /// <summary>
        /// 移除整个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.Remove(key);
            }
        }

        public static void RemoveByPattern(string pattern)
        {
            using (var redis = RedisManager.GetClient())
            {
                //通过lua脚本批量删除
                var deleteLua = $"for i, name in ipairs(redis.call('keys', '{pattern}')) do redis.call('del', name); end";
                redis.ExecLua(deleteLua, new string[] {});
            }
            
        }

        /// <summary>
        /// 是否存在 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.ContainsKey(key);
            }
        }      
        #endregion

        #region   hash对像转成字符串来存

        /// <summary>
        /// 存储数据到hash表 如果存在则覆盖
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Set<T>(string hashId, string key, T t)
        {
            using (var Redis = RedisManager.GetClient())
            {
                var value = JsonSerializer.SerializeToString<T>(t);
                return Redis.SetEntryInHash(hashId, key, value);
            }
        }


        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string hashId, string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                string value = Redis.GetValueFromHash(hashId, key);
                return JsonSerializer.DeserializeFromString<T>(value);
            }
        }

        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<T> GetAll<T>(string hashId)
        {
            using (var Redis = RedisManager.GetClient())
            {
                var result = new List<T>();
                var list = Redis.GetHashValues(hashId);
                if (list != null && list.Count > 0)
                {
                    list.ForEach(x =>
                    {
                        var value = JsonSerializer.DeserializeFromString<T>(x);
                        result.Add(value);
                    });
                }
                return result;
            }
        }


        #endregion

        #region   hash
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string hashId, string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.HashContainsEntry(hashId, key);
            }
        }
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string hashId, string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.RemoveEntryFromHash(hashId, key);
            }
        }
        /// <summary>
        /// 存储数据到hash表 如果存在则覆盖
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Set(string hashId, string key, string value)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.SetEntryInHash(hashId, key, value);
            }
        }


        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string hashId, string key)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.GetValueFromHash(hashId, key);
            }
        }

        /// <summary>
        /// 获取所有的hash 值
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        public static List<string> GetHashValues(string hashId)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.GetHashValues(hashId);
            }
        }

        #endregion

        #region   队列

        /// <summary>
        /// 压入头部 （将一个元素存入指定ListId的List的头部）
        /// </summary>
        /// <param name="listId">组别名</param>
        /// <param name="value">值</param>
        public static void PushString(string listId, string value)
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.EnqueueItemOnList(listId, value);
            }
        }

        /// <summary>
        /// 弹出从尾部 （将指定ListId的末尾的那个元素出列，返回出列元素）
        /// </summary>
        /// <param name="listId">组别名</param>
        /// <returns></returns>
        public static string PopString(string listId)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.BlockingDequeueItemFromList(listId, null);//将指定ListId的List<T>末尾的那个元素出列，区别是：会阻塞该List<T>，支持超时时间，返回出列元素
            }
        }

        /// <summary>
        /// 链表中数据容量
        /// </summary>
        /// <param name="listId">主键</param>
        /// <returns></returns>
        public static long QueueCount(string listId)
        {
            using (var Redis = RedisManager.GetClient())
            {
                long cout = Redis.GetListCount(listId);
                return cout;
            }
        }



        /// <summary>
        ///压入头部  （将一个元素存入指定ListId的List的头部）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <param name="t"></param>
        public static void PushObject<T>(string listId, T t)
        {
            using (var Redis = RedisManager.GetClient())
            {
                var value = JsonSerializer.SerializeToString<T>(t);
                Redis.EnqueueItemOnList(listId, value);
            }
        }
        /// <summary>
        ///弹出从尾部   （将指定ListId的List<T>末尾的那个元素出列，返回出列元素）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static T PopObject<T>(string listId)
        {
            using (var Redis = RedisManager.GetClient())
            {
                string value = Redis.BlockingDequeueItemFromList(listId,null);
                if (!string.IsNullOrEmpty(value))
                {
                    return JsonSerializer.DeserializeFromString<T>(value);
                }
                else
                {
                    return default(T);
                }
            }
        }
        #endregion

        #region 无序集合
        /// <summary>
        /// 无序集合
        /// </summary>
        public static void SAdd(string key, List<string> list)
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.AddRangeToSet(key, list);
            }
        }

        /// <summary>
        /// 往集合中添加单个值
        /// </summary>
        public static void SAdd(string key, string value)
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.AddItemToSet(key, value);
            }
        }

        /// <summary>
        /// 删除指定集合
        /// </summary>
        public static void SRemove(string key, string value)
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.RemoveItemFromSet(key, value);
            }
        }

        /// <summary>
        /// 获取所有集合
        /// </summary>
        public HashSet<string> SMember(string key, string value)
        {
            using (var Redis = RedisManager.GetClient())
            {
                return Redis.GetAllItemsFromSet(key);
            }
        }
        #endregion

        #region 保存数据文件
        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public static void Save()
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.Save();
            }
        }
        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public static void SaveAsync()
        {
            using (var Redis = RedisManager.GetClient())
            {
                Redis.SaveAsync();
            }
        }
        #endregion

        #region 过期事件订阅
        public static void SubscribeExpireNot(Action<string, string> action)
        {
            try
            {
                using (var Redis = RedisManager.GetClient())
                {
                    using (var subscription = Redis.CreateSubscription())
                    {
                        var channelName = "__keyevent@0__:expired";
                        subscription.OnSubscribe = channel => Console.WriteLine("订阅消息频道：" + channelName);
                        subscription.OnUnSubscribe = channel => Console.WriteLine("取消订阅消息频道：" + channelName);

                        subscription.OnMessage = action;

                        subscription.SubscribeToChannels(channelName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        } 
        #endregion
    }
}
