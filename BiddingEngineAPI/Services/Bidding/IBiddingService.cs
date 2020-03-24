using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;
using BiddingEngineAPI.ApiModels.Bid;

namespace BiddingEngineAPI.Services
{
    public interface IBiddingService
    {
        IQueryable<Bid> Get();
        IQueryable<Bid> GetByUserId(int userId);
        BidModel Get(int id);
        Task<Bid> Create(CreateBidModel model);
        Task<Bid> Put(int bidId, EditBidModel model);
        Task<Bid> ApproveBid(int requestId);
        bool IsUserAddedBidding(int userId, int requestId);
        bool IsUserApproveBidding(int userTypeId, int requestId);
        IQueryable<BidModel> GetByRequestId(int requestId, int userTypeId, int userId);
        //IQueryable<Bid> GetAllOpenedBids();

    }
}
