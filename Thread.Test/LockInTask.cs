using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class LockInTask
    {
        static int TaskNum = 30;
        const int RUN_LOOP =100;
        static Task[] Tasks;

        static int result;
        static void Main()
        {
            //按住回车数秒
            while (true)
            {
                Tasks = new Task[TaskNum];

                for (int i = 0; i < TaskNum; i++)
                {
                    Tasks[i] = Task.Factory.StartNew((num) =>
                    {
                        var taskId = (int)num;
                        work(taskId);
                    }, i);
                }

                try
                {
                    Task.WaitAll(Tasks);
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("All Phase is completed");
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("result " + result);
                }
                catch (AggregateException aex)
                {
                    Console.WriteLine("Task failed And Canceled" + aex.ToString());
                }
                Console.ReadKey();
            }
        }

        static void work(int taskId)
        {
            int i = 0;
            
            try
            {
                //线程处理任务多数,会导致结果不正确
                //not use lock
                result++;

                //Interlocked.Increment 可实现原子操作
                //Interlocked.Increment(ref result);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                i++;
            }
            
           
        }
    }
}
