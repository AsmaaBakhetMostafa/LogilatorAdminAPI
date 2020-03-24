using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BiddingEngineAPI.ApiModels.Users
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int UserType_Id { get; set; }
        public string UserType_Name { get; set; }
        public int Role_Id { get; set; }
        public bool Is_SProvider { get; set; }

    }
}
