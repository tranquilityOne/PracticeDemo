using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                if (args[0] == "i" || args[0] == "-i")
                {
                    InstallService();
                }
                else if (args[0] == "debug")
                {
                    JobCenter.Start();
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new MyService() };
                ServiceBase.Run(ServicesToRun);
            }
            Console.ReadKey();
        }

        static void InstallService()
        {
            var command = "sc stop  " + GetServiceName();
            command += "\r\n sc delete " + GetServiceName();
            command += "\r\n sc create " + GetServiceName() + " binpath= \"" + Environment.CurrentDirectory + "\\WCFService.Service.exe\" start= \"auto\" displayname= \"" + GetServiceName() + "\"";
            command += "\r\n net start " + GetServiceName();
            command += "\r\n pause";
            command += "\r\n";
            RunCommandFile(command);
        }
        private static string GetServiceName()
        {
            int defaultPort = 9000;
            return "WCFService_" + defaultPort.ToString();
        }
        static void RunCommandFile(string cmd)
        {
            try
            {
                string cmdfile = "service.cmd";
                File.WriteAllText(cmdfile, cmd);
                Process.Start(cmdfile);
                Log4netHelper.Debug("RunCommandFile", "服务安装成功");
            }
            catch (Exception ex)
            {
                Log4netHelper.Error("RunCommandFile", ex.Message,ex);
            }
        }
    }
}
