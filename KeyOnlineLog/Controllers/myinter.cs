using KeyOnlineLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyOnlineLog.Controllers
{
    public interface myinter
    {
       WepApiResultLog OnGet();
       string PublishStringData(string data);
       string SubscribStringData();

    }
}
