using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyOnlineLog.Model
{
    public class WepApiResultLog
    {
       [BsonId]
        public int RequestID { get; set; }
        public List<string> JsonData { get; set; }
        public List<Exception> Message { get; set; }

    }
    public class WepApiResultLogExample
    {
        [BsonId]
        public int RequestID { get; set; }
        public Exception Message { get; set; }

    }


    public class Exception
    {
        public string ServiceOperationPlace { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccessful { get; private set; }

    }
}
