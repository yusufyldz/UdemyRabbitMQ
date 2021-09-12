using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyRabbitMQ.Subscriber.udemysubs
{
    public class RabbitMqClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private  IConnection _connection;
        private  IModel _chanel;
        public static string ExchaneName = "MongoDbDirectExchange";
        public static string RoutingMongo = "Mongo-route-log";
        public static string QueueName = "starting-log";

        public RabbitMqClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_chanel is { IsOpen: true })
            {
                return _chanel;
            }

            _chanel = _connection.CreateModel();
            _chanel.ExchangeDeclare(ExchaneName, type: "direct", true, false);
            _chanel.QueueDeclare(QueueName,true,false,false,null);
            _chanel.QueueBind(exchange:ExchaneName,queue:QueueName,routingKey:RoutingMongo);
            return _chanel; 
            
        }

        public void Dispose()
        {
            _chanel?.Close();
            _chanel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
