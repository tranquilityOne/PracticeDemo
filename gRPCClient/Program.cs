using Grpc.Core;
using GRPCDemo;
using System;

namespace gRPCClient
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Channel channel = new Channel("127.0.0.1:9007", ChannelCredentials.Insecure);

            try
            {
                var client = new gRPC.gRPCClient(channel);
                var reply = client.SayHello(new HelloRequest { Name = "Test" });
                Console.WriteLine("reply message：" + reply.Message);  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            channel.ShutdownAsync().Wait();
            Console.WriteLine("任意键退出...");
            Console.ReadKey();
        }
    }
}
