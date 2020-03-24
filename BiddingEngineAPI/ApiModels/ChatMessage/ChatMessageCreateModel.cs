using BiddingEngineAPI.EFCore.Enum;
using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public class ChatMessageCreateModel
    {
       

        [Required(ErrorMessage = "MessageText is required")]
        public string MessageText { get; set; }

        [Required(ErrorMessage = "MessageDate is required")]
        public DateTime MessageDate { get; set; }

        [Required(ErrorMessage = "FromUserId is required")]
        public int? FromUserId { get; set; }

        [Required(ErrorMessage = "ToUserId is required")]
        public int? ToUserId { get; set; }
       
        public bool? Isattached { get; set; }
        public int? attchedType { get; set; }
        public string type { get; set; }


    }
}
