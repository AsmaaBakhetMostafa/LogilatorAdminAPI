using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ActivationUrl { get; set; }
        public string Port { get; set; }
        public string EmailID { get; set; }
        public string EmailPassword { get; set; }
    }
}
