﻿using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Services
{
    public interface IChatMessageService
    {
        List<ChatMessageViewModel> Get();
        ChatMessage Create(ChatMessageCreateModel model);
    }
}
