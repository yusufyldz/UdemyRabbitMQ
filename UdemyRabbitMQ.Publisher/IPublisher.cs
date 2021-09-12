using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyRabbitMQ.Publisher
{
    public interface IPublisher
    {
        Task<string> Publish(string StringJsonData);

    }
}
