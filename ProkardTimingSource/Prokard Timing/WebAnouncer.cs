using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace Rentix
{
    class WebAnouncer
    {
        public void action(Webanounserdata data)
        {
            string serialized = JsonConvert.SerializeObject(data);
            MultiServer.SocketServer.WebSocketServices.Broadcast(serialized);
        }
    }

    class Webanounserdata
    {
       public string method;
       public object data;
    }
}
