using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;



namespace EncryptDemo
{

    public delegate void TickCompletedEventHandler(string name);

    /// <summary>
    /// 定时操作
    /// </summary>
    public class TimerTest
    {
        /// <summary>
        /// 总秒数
        /// </summary>
        private int totalSeconds=0;
        private string timerName = string.Empty;
        private Timer timer = null;
        /// <summary>
        /// 完成后委托
        /// </summary>
        public TickCompletedEventHandler tickCompleted;
        public TimerTest(int seconds,string name)
        {
            totalSeconds = seconds;
            timerName = name;
            timer = new Timer(1000);
        }

        public void StartTick()
        {
              Console.WriteLine("name:" + timerName + " begin!");
              timer.Elapsed+=timer_Elapsed;

              
              timer.Start();
        }

        private  void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (totalSeconds > 0)
            {
                Console.WriteLine("name :" + timerName + " second:" + totalSeconds + "");
                totalSeconds--;
              
            }
            else
            {
                Console.WriteLine("name :" + timerName + " second:" + totalSeconds + "");
                if (tickCompleted != null)
                    tickCompleted(timerName);
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}
