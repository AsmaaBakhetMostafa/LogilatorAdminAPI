using BiddingEngineAPI.EFCore.Enum;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BiddingEngineAPI.ApiModels.Users
{
    public static class ChatMessageExtensions
    {
        private static IAutoMapper _mapper;
        public static ChatMessageViewModel ToModel(this ChatMessage target, IAutoMapper mapper)
        {
            _mapper = mapper;
            var model = new ChatMessageViewModel();  
            if (target != null)
            {
                var strMssg = "";
                if (target.Isattached.HasValue)
                {
                    if (target.Isattached.Value)
                    {
                        strMssg = target.MessageText.Replace("\\", "/");
                       
                    }
                }
                else
                    strMssg = target.MessageText;
                model.Id = target.Id;
                model.MessageText = strMssg;
                model.MessageDate = target.MessageDate;
                model.FromUserId = target.FromUserId;
                model.FromUser = target.FromUser;
                model.ToUserId = target.ToUserId;
                model.ToUser = target.ToUser;
                model.attchedType = target.attchedType;
                model.Isattached = target.Isattached;
             
            }

            //Return Result
            return model;
        }
    }
    public class UserChatViewModel
    { 
        public virtual User User { get; set; }
        public List<ChatMessageViewModel> ChatMessages { get; set; } = new List<ChatMessageViewModel>();

    }
    public class ChatMessageViewModel
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public DateTime MessageDate { get; set; }

        public int? FromUserId { get; set; }
        public virtual User FromUser { get; set; }

        public int? ToUserId { get; set; }
        public virtual User ToUser { get; set; }
        public bool? Isattached { get; set; }
        public AttachedType? attchedType { get; set; }

    }
}
