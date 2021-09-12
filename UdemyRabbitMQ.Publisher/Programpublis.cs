using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyRabbitMQ.Publisher
{

    public enum LogsName
    {
        Critical = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
   public class Programpublis : IPublisher
    {
      
        
        public async Task<string> Publish(string StringJsonData)
        {


            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://mirmqflp:tu7ij7jBl6gd7Flbogzxf7C6gwsS4_dK@rat.rmq2.cloudamqp.com/mirmqflp");
            using var connection = factory.CreateConnection();
            var chanel = connection.CreateModel();
            chanel.QueueDeclare("MongoDb-Log", true, false, false);
            string Message = StringJsonData;
            var Messagebody = Encoding.UTF8.GetBytes(Message);
            //chanel.ExchangeDeclare("yusuflogs-direct", durable: true, type: ExchangeType.Direct);
            chanel.BasicPublish(string.Empty, "MongoDb-Log",null,Messagebody);
            //Enum.GetNames(typeof(LogsName)).ToList().ForEach(x =>
            //{
            //    var queueName = $"direct-queue{x}";
            //    chanel.QueueDeclare(queueName, true, false, false);
            //    var routeKey = $"route-{x}";
            //    chanel.QueueBind(queueName, "yusuflogs-direct", routeKey, null);
            //});



            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            // {
            //     LogsName log = (LogsName)new Random().Next(1, 4);
            //     var Log = $"Excel oluşturma-{log}";
            //     var routeKey = $"route-{log}";
            //     var messagebody = Encoding.UTF8.GetBytes(Log);
            //     chanel.BasicPublish("yusuflogs-direct", routeKey, null, messagebody);
            //     Console.WriteLine($"Mesaj gönderilmiştir => {Log}");

            // });
            return "mesaj gönderildi";
            
        }
           


            
        
    }
}
