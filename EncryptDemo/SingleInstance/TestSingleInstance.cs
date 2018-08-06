using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDemo.SingleInstance
{
    /// <summary>
    /// 静态初始化(推荐使用)
    /// </summary>
    public sealed class TestSingleInstance
    {
        private static TestSingleInstance _Instance=null;

        static TestSingleInstance()
        {
            Console.WriteLine("Create Instance...");
            _Instance = new TestSingleInstance();
        }

        private static object locker = new object();
        public static TestSingleInstance Instance
        {
            get
            {
                return _Instance;
            }
        }

        private TestSingleInstance() { }
    }

    /// <summary>
    /// 线程安全,每次都要加锁,消耗性能
    /// </summary>
    public sealed class Singleton
    {
        static Singleton instance = null;
        private static readonly object padlock = new object();

        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }

                return instance;
            }
        }
    }

    /// <summary>
    /// 多线程安全, 线程不是每次都加锁，允许实例化延迟到第一次访问对象时发生
    /// </summary>
    public sealed class SingletonDoubleLock
    {
        static SingletonDoubleLock instance = null;
        private static readonly object padlock = new object();

        private SingletonDoubleLock()
        {
        }

        public static SingletonDoubleLock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonDoubleLock();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
