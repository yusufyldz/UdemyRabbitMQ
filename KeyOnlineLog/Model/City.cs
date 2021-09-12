using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyOnlineLog.Model
{
    public class City
    {
        [BsonId]
        public int ID { get; set; }
        public string CityName { get; set; }

    }
}
