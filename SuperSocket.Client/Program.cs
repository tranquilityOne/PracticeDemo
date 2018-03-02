using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using SuperSocket.ClientEngine;
using System.Net;

namespace SuperSocket.Client
{
    
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"),9001);
            SSClient client = new SSClient();
            client.Connect(ipe);

            string sendData = string.Empty;
            if (Console.ReadLine() == "s")
                sendData = Console.ReadLine();
            client.Send(sendData);
            Console.ReadKey();
        }    
    }
}
