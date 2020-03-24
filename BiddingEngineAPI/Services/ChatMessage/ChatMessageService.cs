using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.DAL;
using BiddingEngineAPI.Enum;
using BiddingEngineAPI.EFCore.Helper;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Exceptions;
using BiddingEngineAPI.Helpers;
using BiddingEngineAPI.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BiddingEngineAPI.EFCore.Enum;

namespace BiddingEngineAPI.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IUnitOfWork _uniteOfwork;

        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;

        public ChatMessageService(IUnitOfWork uniteOfwork, IOptions<AppSettings> appSettings, IAutoMapper mapper)
        {
            _uniteOfwork = uniteOfwork;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        public List<ChatMessageViewModel> Get()
        {
            var ChatMessages = _uniteOfwork.Query<ChatMessage>()
                                .Include("ToUser")
                                .Include("FromUser")
                                .ToList();
            // .Where(x => x.IsActive);


            return ChatMessages.Select(x => x.ToModel(_mapper)).ToList();
        }
        public ChatMessage Create(ChatMessageCreateModel model)
        {
            AttachedType? Newvariabe = new AttachedType();
            if (model.attchedType != null)
                Newvariabe = (AttachedType)model.attchedType;
            var MssgText = "";
            if (model.Isattached.HasValue)
            {
                if (model.Isattached.Value)
                {
                    if(Newvariabe== AttachedType.Image)
                    MssgText = UploadImageHelper.SaveImage( model.MessageText, "Chats");
                    else
                   MssgText = UploadImageHelper.SaveFile(model.type, model.MessageText, "Chats");


                }
            }
            else
            {
                MssgText = model.MessageText;
            }
            var chatMessage = new ChatMessage
            {
                MessageDate = DateTime.Now,
                FromUserId = model.FromUserId,
                ToUserId = model.ToUserId,
                Isattached = model.Isattached,
                attchedType = Newvariabe,
                MessageText = MssgText,
                

            };

            _uniteOfwork.Add(chatMessage);
            _uniteOfwork.Commit();

            return chatMessage;
        }

    }
}
