﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRPCDemo;
using Grpc.Core;

namespace gRPCServer
{
    class Program
    {
        const int Port = 9007;
        static void Main(string[] args)
        {
            try
            {
                Server server = new Server
                {
                    Services = { gRPC.BindService(new GRPCImple()) },
                    Ports = { new ServerPort("127.0.0.1", Port, ServerCredentials.Insecure) }
                };
                server.Start();

                Console.WriteLine("gRPC server listening on port " + Port);
                Console.WriteLine("任意键退出...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }                   
        }
    }

    class GRPCImple : gRPC.gRPCBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request,ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
