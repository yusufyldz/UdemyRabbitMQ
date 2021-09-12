using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyRabbitMQ.Subscriber.udemysubs;

namespace UdemyRabbitMQ.Publisher.udemyPublisher
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqClientService _rabbitMqClientService;

        public RabbitMqPublisher(RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqClientService = rabbitMqClientService;
        }

        public void Publish(string data)
        {
            var chanel = _rabbitMqClientService.Connect();
            var bodybyte = Encoding.UTF8.GetBytes(data);
            var Properties = chanel.CreateBasicProperties();
            Properties.Persistent = true;
            chanel.BasicPublish(exchange: RabbitMqClientService.ExchaneName,routingKey: RabbitMqClientService.RoutingMongo,basicProperties: Properties, body:bodybyte);
         }
    }
}
