using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Services
{
    public interface IFixedFiledDetailsService
    {
        List<FixedFiledDetails> GetAllPredefinedOptions(int FixedfieldID);
    }
}
