using RabbitMQ.Client;
using System;
using System.Text;

namespace Example.Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://mirmqflp:tu7ij7jBl6gd7Flbogzxf7C6gwsS4_dK@rat.rmq2.cloudamqp.com/mirmqflp");
            using var connection = factory.CreateConnection();
            var chanel = connection.CreateModel();
            chanel.QueueDeclare("MongoDb-Log", true, false, false);
            string Message = "Hayırlı işler";
            var Messagebody = Encoding.UTF8.GetBytes(Message);
            //chanel.ExchangeDeclare("yusuflogs-direct", durable: true, type: ExchangeType.Direct);
            chanel.BasicPublish(string.Empty, "MongoDb-Log", null, Messagebody);
            Console.WriteLine(Message);
            Console.ReadLine();
        }
    }
}
