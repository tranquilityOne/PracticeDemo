using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using EncryptDemo;
namespace SuperSocket.Client
{
    public class SSClient
    {
        private EasyClient client;

        public SSClient()
        {            
            client = new EasyClient();
            client.Initialize(new TestReceiveFilter(), (request) =>
            {
                Console.WriteLine(request.Key);
            });          
            client.Closed += client_Closed;
            client.Connected += client_Connected;
            client.Error += client_Error;

        }

       

        public void Send(string sendData)
        { 
             if(client.IsConnected)
                 client.Send(sendData.HexStringToByte());
        }

        public void Connect(IPEndPoint ipe)
        {
            client.BeginConnect(ipe);
        }

        void client_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
          
        }

        void client_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("连接成功");
        }

        void client_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("连接断开");
        }
    }
}
