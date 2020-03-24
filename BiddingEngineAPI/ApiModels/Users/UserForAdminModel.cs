using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class UserForAdminModel
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Mobile_Number { get; set; }
        public DateTime Req_Date { get; set; }
        public int UserType_Id { get; set; }

    }
}
