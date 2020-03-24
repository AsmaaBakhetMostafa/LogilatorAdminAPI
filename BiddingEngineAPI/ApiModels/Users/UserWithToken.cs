using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class UserWithToken : UserModel
    { 
        public string Token { get; set; }
    }
}
