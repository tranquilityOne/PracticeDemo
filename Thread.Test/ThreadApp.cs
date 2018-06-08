using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class ThreadApp
    {
        /// <summary>
        /// 线程Id
        /// </summary>
       public string TheadId { get; set; }
          
        /// <summary>
        /// 线程
        /// </summary>
       private Thread AppThread { get; set; }

        private object locker = new object();

        /// <summary>
        /// Thread启动
        /// 每个thread耗内存,根据CPU能力,基本能保证每个工作项并行执行
        /// </summary>
        public void ThreadStart()
        {
            try
            {
                if (AppThread == null)
                {
                    lock (locker)
                    {
                        AppThread = new Thread(Woker);
                        AppThread.IsBackground = true;
                        AppThread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// 线程池启动
        /// 该方式不耗内存,基本所有工作项是按顺序来执行,影响速度
        /// </summary>
        public void ThreadPoolStart()
        {
            ThreadPool.QueueUserWorkItem(Woker);
        }

        /// <summary>
        /// 开启任务
        /// 和(ThreadPool.QueueUserWorkItem)执行类似
        /// </summary>
        public void TaskFactoryStart()
        {
            Task.Run(()=>{
                Woker("0");
            });
        }

        public void TaskStart()
        {
            Task task = new Task(n=>Woker(n),"0");
            task.Start();
        }


        private void Woker(object obj)
        {
            Thread.Sleep(20000);
            Console.WriteLine($"The ThreadId is {TheadId}");
        }
    }
}
