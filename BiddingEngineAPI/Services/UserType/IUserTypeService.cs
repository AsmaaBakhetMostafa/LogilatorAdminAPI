using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;
using BiddingEngineAPI.ApiModels;

namespace BiddingEngineAPI.Services
{
    public interface IUserTypeService
    {
        IQueryable<UserType> Get();
        IQueryable<UserType> Get(int id);
    }
}
