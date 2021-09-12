using KeyOnlineLog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyRabbitMQ.Publisher;
using UdemyRabbitMQ.Publisher.udemyPublisher;
using UdemyRabbitMQ.Subscriber;
using KeyOnlineLog.Model;
namespace KeyOnlineLog.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class WeatherForecastController : ControllerBase, myinter
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPublisher _publisher;
        private readonly UdemyRabbitMQ.Subscriber.Subscriber _subscriber;
        private RabbitMqPublisher _RabbitMqPublisher;

     
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPublisher publisher, Subscriber subs, RabbitMqPublisher rabbitMqPublisher)
        {
            _logger = logger;
            _publisher = publisher;
            _subscriber = subs;
            _RabbitMqPublisher = rabbitMqPublisher;
        }



        [HttpGet]
        public Model.WepApiResultLog OnGet()
        {
          var subscribe=  _subscriber.SubscriberNow();
            var a = System.Text.Json.JsonSerializer.Deserialize<WepApiResultLogExample>(subscribe);
            var client = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
            var database = client.GetDatabase("NoSqlData");
            //var CityCollection = database.GetCollection<City>("City");
            //CityCollection.InsertOne(a);

            var wepApiCollection = database.GetCollection<Model.WepApiResultLog>("WepApiResultLog");

            var filter = Builders<Model.WepApiResultLog>.Filter.Eq("RequestID", a.RequestID);
           
            var akl = wepApiCollection.Find(p => p.RequestID == a.RequestID).FirstOrDefault();

            akl.Message.Add(a.Message);
            var newdata = new Model.WepApiResultLog()
            {
                RequestID = a.RequestID,
                JsonData =akl.JsonData,
                Message = akl.Message
            };
            wepApiCollection.ReplaceOne(filter,newdata);




            return akl;


        }


        [HttpGet]
        public string PublishStringData(string data)
        {
            _RabbitMqPublisher.Publish(data);
            return "send publish dataa";
        }

        [HttpGet]
        public string SubscribStringData()
        {
            return _subscriber.SubscriberNow();
        }

     
    }
}
