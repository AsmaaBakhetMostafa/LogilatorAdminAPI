using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;
using BiddingEngineAPI.ApiModels.Request;

namespace BiddingEngineAPI.Services
{
    public interface IRequestService
    {
        IQueryable<Request> Get();
        IQueryable<Request> GetByUserId(int userId);
        RequestModel Get(int id);
        Task<Request> Create(CreateRequestModel model);
        Task<Request> Put(int requestId, EditRequestModel model);
        Task<Request> ApproveRequest(int requestId);
        IQueryable<Request> GetAllOpenedRequests();

    }
}
