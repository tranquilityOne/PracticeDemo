using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using DownloadSample;

namespace WindowsFormsApplication1
{
    //定义委托，异步调用
    delegate void ShowProgressDelegate(int totalStep, int currentStep);
    public partial class Form1 : Form
    {
        private Downloader dl;
        private bool IsPause = false;
        public Form1()
        {
            InitializeComponent();

            this.tbUrl.Text = "http://192.168.30.38/group1/M00/00/34/wKgeJltRVF2ASTveAADHGvcwvgo399.jpg";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            StartDownload();
        }
        /// <summary>
        /// 开始下载
        /// </summary>
        private void StartDownload()
        {
            if (dl == null)
            {
                string directory = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Files");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                dl = new Downloader(this.tbUrl.Text.Trim(), directory);
                dl.Step = 500;
            }

            if (IsPause)
            {
                IsPause = false;
            }

            ParameterizedThreadStart start = new ParameterizedThreadStart(SetProgress);
            Thread progressThread = new Thread(start);
            progressThread.IsBackground = true;//标记为后台进程，在窗口退出时，正常退出
            progressThread.Start();
        }

        /// <summary>
        /// 设置当前进度
        /// </summary>
        /// <param name="state"></param>
        void SetProgress(object state)
        {            
            object[] objs = new object[] { 100, 0 };
            while (!dl.IsFinished && !IsPause)
            {
                dl.Download();

                Thread.Sleep(100);
                objs[1] = (int)dl.CurrentProgress;
                //异步调用
                this.Invoke(new ShowProgressDelegate(ShowProgress), objs);
            }
        }
        /// <summary>
        /// 刷新进度条
        /// </summary>
        /// <param name="totalStep"></param>
        /// <param name="currentStep"></param>
        void ShowProgress(int totalStep, int currentStep)
        {
            this.progressBar1.Maximum = totalStep;
            this.progressBar1.Value = currentStep;

            this.lbCurrent.Text = this.progressBar1.Value * 100 / progressBar1.Maximum + "%";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            this.IsPause = true;
        }
    }
}
