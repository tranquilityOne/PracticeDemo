using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WCFService.BLL;

namespace WCFService
{
    partial class MyService : ServiceBase
    {
        public MyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TestService.Start();
            WCFHost host = new WCFHost();
            host.open();
            Log4netHelper.Debug("OnStart", "服务开始");
        }

        protected override void OnStop()
        {
            Log4netHelper.Debug("OnStop", "服务停止");
        }

        protected override void OnShutdown()
        {
            try
            {
                Log4netHelper.Debug(GetType().FullName, "OnShutdown");
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnPause()
        {
            try
            {
                Log4netHelper.Debug(GetType().FullName, "OnPause");
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnCustomCommand(int command)
        {
            try
            {
                Log4netHelper.Debug(GetType().FullName, "command=" + command);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
