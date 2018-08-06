using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest
{
    public class LockInThread
    {
        static Thread[] Threads;
        static int threadNum=20;

        static int result;
        static void Main()
        {
            while (true)
            {
                startThread();
                Console.ReadKey();
            }
        }

        static void startThread()
        {
            Threads = new Thread[threadNum];
            for (int i = 0; i < threadNum; i++)
            {
                Threads[i] = new Thread(Woker);
                Threads[i].IsBackground = true;
                Threads[i].Start();
            }
            Console.WriteLine("last result is " + result);
        }

        static void Woker()
        {
            Thread.Sleep(100);
            //result++;
            Interlocked.Increment(ref result);
        }
    }
}
