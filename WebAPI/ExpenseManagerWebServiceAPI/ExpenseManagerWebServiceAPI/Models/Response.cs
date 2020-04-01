using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Models
{
    public class Response
    {
        public bool status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string data { get; set; }

        public Response() { }
    }
}
