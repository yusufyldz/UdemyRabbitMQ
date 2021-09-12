using KeyOnlineLog.Controllers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Example.Subscriber
{
    public class Program
    {
        private readonly myinter _inter;
        public Program(myinter myinter)
        {
            _inter = myinter;
        }
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://mirmqflp:tu7ij7jBl6gd7Flbogzxf7C6gwsS4_dK@rat.rmq2.cloudamqp.com/mirmqflp");
            using var connection = factory.CreateConnection();
            var chanel = connection.CreateModel();
            chanel.QueueDeclare("MongoDb-Log", true, false, false);
            var consumer = new EventingBasicConsumer(chanel);
            chanel.BasicConsume("MongoDb-Log", false, consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
               var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine(message);
                

            };
            Console.ReadLine();
          
        }
    }
}
