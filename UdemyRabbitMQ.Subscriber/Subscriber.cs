using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdemyRabbitMQ.Subscriber.udemysubs;

namespace UdemyRabbitMQ.Subscriber
{
    public class Subscriber : BackgroundService
    {
        private readonly RabbitMqClientService _rabbitMqClientService;
        private IModel _channel;
       
       
        public Subscriber(RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqClientService = rabbitMqClientService;
        }
       

        public string SubscriberNow()
        {
            try
            {
                _channel = _rabbitMqClientService.Connect();
                _channel.BasicQos(0, 1, false);

                var Consumer = new EventingBasicConsumer(_channel);

                _channel.BasicConsume(RabbitMqClientService.QueueName, true, Consumer);
                var message = string.Empty;

                Consumer.Received += (sender, @event) => {
                   message =  Encoding.UTF8.GetString(@event.Body.ToArray());
                   
                };

                return message;

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());

            return Task.CompletedTask;
           
        }
    }
}
