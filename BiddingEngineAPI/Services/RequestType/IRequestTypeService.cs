using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;
using BiddingEngineAPI.ApiModels;

namespace BiddingEngineAPI.Services
{
    public interface IRequestTypeService
    {
        IQueryable<RequestType> Get();
        IQueryable<RequestType> Get(int id);
    }
}
