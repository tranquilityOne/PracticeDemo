using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest
{
   public  class AsyncTask
    {
        /// <summary>  
        /// 开始一个异步任务(用于界面UI)  
        /// </summary>  
        /// <param name="taskAction">异步任务执行委托</param>  
        /// <param name="taskEndAction">异步任务执行完毕后的委托（会跳转回UI线程）</param>  
        /// <param name="control">UI线程的控件</param>  
        public void StartAsyncTask(Action taskAction, Action taskEndAction, System.Windows.Forms.Control control)
        {
            if (control == null)
            {
                return;
            }

            Task task = new Task(() => {
                try
                {
                    taskAction();

                    //返回UI线程  
                    control.Invoke(new Action(() =>
                    {
                        taskEndAction();
                    }));
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            });
            task.Start();
        }
    }
}
