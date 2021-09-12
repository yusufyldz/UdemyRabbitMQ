using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyOnlineLog.Model
{
    public class Employee
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public EmployeeContact Contact { get; set; }
        public ObjectId CityId { get; set; }


    }

    public class EmployeeContact
    {
        public string PhoneNumber { get; set; }
        public string  WorkPhoneNumber { get; set; }
    }
}
