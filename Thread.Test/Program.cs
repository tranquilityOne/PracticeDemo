using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int workthreadnumber;
            int iothreadnumber;

            ThreadPool.GetMinThreads(out workthreadnumber, out iothreadnumber);
            Console.WriteLine("maxworkthreadnumber is " + workthreadnumber + "，maxworkthreadnumber is " + iothreadnumber + "");
            ThreadPool.GetMaxThreads(out workthreadnumber, out iothreadnumber);
            Console.WriteLine("minworkthreadnumber is " + workthreadnumber + "，miniothreadnumber is " + iothreadnumber + "");
            //ThreadPool.SetMaxThreads(1000, 1000);
            // 获得线程池中可用的线程，把获得的可用工作者线程数量赋给workthreadnumber变量
            // 获得的可用I/O线程数量给iothreadnumber变量
            ThreadPool.GetAvailableThreads(out workthreadnumber, out iothreadnumber);
            Console.WriteLine("workthreadnumber is " + workthreadnumber + "，iothreadnumber is " + iothreadnumber + "");
            for (int i = 0; i < 1200; i++)
            {
                ThreadApp app = new ThreadApp();
                app.TheadId = $"TheadId_{i}";
                Console.WriteLine("Create Thread is " + app.TheadId);
                //app.ThreadPoolStart();
                app.TaskStart();
            }
            Console.ReadKey();
        }

        static void Woker(object txt)
        {
            Thread.Sleep(10000);
            Console.WriteLine($"Message is from Woker{txt}"); 
        }
    }
}
