
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WCFService
{
    public class Log4netHelper
    {
        static log4net.Appender.RollingFileAppender appender = new log4net.Appender.RollingFileAppender();
        /// <summary>
        ///  自定义 花样日志
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Message"></param>
        /// <param name="DeviceId"></param>
        public static void Info(String Name, String Message, string DeviceId)
        {
            appender.File = string.Format("D:/logs/{0}_{1}.log", DeviceId, DateTime.Now.ToString("yyyyMMdd"));
            appender.ActivateOptions();
            appender.AppendToFile = true;
            appender.Writer.WriteLine(String.Format("{0},{1}-\r\n{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Name, Message));
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Message">信息</param>
        public static void Error(String Name, String Message)
        {
            log4net.LogManager.GetLogger(Name).Error(Message);
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="Message">详情</param>
        /// <param name="ex">异常</param>
        public static void Error(String Name, String Message, Exception ex)
        {
            log4net.LogManager.GetLogger(Name).Error(Message, ex);
        }
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Message"></param>
        public static void Info(String Name, String Message)
        {
            log4net.LogManager.GetLogger(Name).Info(Message);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Message"></param>
        public static void Debug(String Name, String Message)
        {    
              log4net.LogManager.GetLogger(Name).Debug(Message);            
        }


        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Message"></param>
        public static void Warn(String Name, String Message)
        {
            log4net.LogManager.GetLogger(Name).Warn(Message);
        }
    }
}
