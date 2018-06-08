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
        static int TaskNum = 10;
        const int RUN_LOOP =100;
        static Task[] Tasks;

        static int result;
        static void Main()
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

        static void work(int taskId)
        {
            int i = 0;
            while (i < RUN_LOOP)
            {
                try
                {
                    //经测试不使用Interlocked.Increment,同样可实现原子操作,why?

                    //not use lock
                    //result++;

                    //use Interlocked
                    Interlocked.Increment(ref result);
                    Console.WriteLine("Task " + taskId + " working ... ..." + result);
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
}
