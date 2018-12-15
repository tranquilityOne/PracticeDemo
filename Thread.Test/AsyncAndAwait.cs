using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadTest
{
    /// <summary>
    /// await and async
    /// </summary>
    public class AsyncAndAwait
    {
        static void Main()
        {
            int result = TaskOfTResult_MethodAsync2().Result;
            Console.WriteLine("result is ", result);
            Console.ReadKey();
        }

        /// <summary>
        /// 异步方法1
        /// </summary>
        /// <returns></returns>
        static async Task<int> TaskOfTResult_MethodAsync()
        {
            Task<int> returnTask = Task.Run<int>(() =>
            {
                Thread.Sleep(2000);
                return 0;
            });
            return await returnTask;
        }

        /// <summary>
        /// 异步方法2
        /// </summary>
        /// <returns></returns>
        static async Task<int> TaskOfTResult_MethodAsync1()
        {
            Task<int> taskResult= TaskOfTResult_MethodAsync();
            Console.WriteLine("the method is TaskOfTResult_MethodAsync1");
            return await taskResult;
        }
        /// <summary>
        /// 异步方法2
        /// </summary>
        /// <returns></returns>
        static async Task<int> TaskOfTResult_MethodAsync2()
        {
            Task<int> taskResult = TaskOfTResult_MethodAsync1();
            Console.WriteLine("the method is TaskOfTResult_MethodAsync2...");


            int result = await taskResult;
            Console.WriteLine("the method is TaskOfTResult_MethodAsync2");
            return result;
        }

    }
}
