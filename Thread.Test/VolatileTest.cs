using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest
{
    public class VolatileTest
    {
        static void Main()
        {
            VolatileWorker wokerObject = new VolatileWorker();
            Thread  workerThread = new Thread(wokerObject.Woker);
            workerThread.Start();
            Console.WriteLine("Main Thread: Start Work Thread ...");

            while (!workerThread.IsAlive) 
                Thread.Sleep(1);

            wokerObject.StopWork();

            Thread.Sleep(2000);

            workerThread.Join();

            Console.WriteLine("Main thread: worker thread has terminated.");
            Console.ReadKey();
        }
    }

    public class VolatileWorker
    {
        //感觉加不加volatile 没有什么区别？？
        private volatile bool _shouldStop;

        public void Woker()
        {
            while (!_shouldStop)
            {
                Console.WriteLine("Work Thread Working...");
            }
        }

        public void StopWork()
        {
            _shouldStop = true;
        }
    }
}
