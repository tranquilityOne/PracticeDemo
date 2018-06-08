using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string exchangeName = "test.exchange";
                string queueName = "test1.queue";
                string otherQueueName = "test2.queue";

                #region 绑定
                ConnectionFactory factory = new ConnectionFactory();
                // "guest"/"guest" by default, limited to localhost connections
                factory.UserName = "lihui";
                factory.Password = "123456";
                factory.VirtualHost = "/";
                factory.HostName = "localhost";

                using (IConnection conn = factory.CreateConnection())
                {
                    using (IModel channel = conn.CreateModel())
                    {
                        #region 绑定
                        //2 定义一个exchange
                        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                        //4 定义两个queue
                        channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                        channel.QueueDeclare(otherQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                        //3 定义exchange到queue的binding
                        channel.QueueBind(queueName, exchangeName, routingKey: queueName);
                        channel.QueueBind(otherQueueName, exchangeName, routingKey: otherQueueName);
                        #endregion

                        #region 发送消息
                        var props = channel.CreateBasicProperties();
                        props.Persistent = true;
                        var msgBody = Encoding.UTF8.GetBytes("Hello, World!");
                        //1. 发送消息到exchange ，但加上routingkey
                        channel.BasicPublish(exchangeName, routingKey: queueName, basicProperties: props, body: msgBody);
                        channel.BasicPublish(exchangeName, routingKey: otherQueueName, basicProperties: props, body: msgBody);
                        #endregion

                        //5. 从test1.queue 队列获取消息
                        BasicGetResult msgResponse = channel.BasicGet(queueName, autoAck: true);
                        string acceptBody = Encoding.UTF8.GetString(msgResponse.Body);
                        Console.WriteLine(acceptBody);
                        //5. 从test2.queue 队列获取消息
                        msgResponse = channel.BasicGet(otherQueueName, autoAck: true);
                        acceptBody = Encoding.UTF8.GetString(msgResponse.Body);
                        Console.WriteLine(acceptBody);
                    }
                }
                #endregion           
                Console.ReadKey();
            }
            catch (Exception)
            {

                throw;
            }
                  
        }

      
    }
}
