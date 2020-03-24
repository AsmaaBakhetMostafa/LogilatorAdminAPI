using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.ApiModels.Bid;
using BiddingEngineAPI.Mapping;
using BiddingEngineAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiddingEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingController : ControllerBase
    {
        private readonly IBiddingService _service;
        private readonly IAutoMapper _mapper;

        public BiddingController(IBiddingService service, IAutoMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("{biddingId}")]
        public BidModel Get(int biddingId)
        {
            var result = _service.Get(biddingId);
            //var model = _mapper.Map<BidModel>(result);
            return result;

        }

        [HttpGet("DidUserAddBidding/Request/{requestId}/User/{userId}")]
        public bool GetIsUserAddBidding(int userId,int requestId)
        {
            var result = _service.IsUserAddedBidding(userId, requestId);
            return result;
        }

        [HttpGet("UserAddBidding/Request/{requestId}/userType/{userTypeId}/user/{userId}")]
        public IQueryable<BidModel> GetBiddingsByRequestId(int userTypeId, int requestId,int userId)
        {
            var result = _service.GetByRequestId(requestId, userTypeId, userId);
            return result;
        }

        [HttpGet("DidUserApproveBidding/Request/{requestId}/userType/{userTypeId}")]
        public bool GetIsUserApproveBidding(int userTypeId, int requestId)
        {
            var result = _service.IsUserApproveBidding(userTypeId, requestId);
            return result;
        }

        [HttpPost]
        public async Task<BidModel> Post(CreateBidModel model)
        {
            var result = await _service.Create(model);
            var models = _mapper.Map<BidModel>(result);
            return models;

        }
        [HttpPut("{bidId}")]
        public async Task<BidModel> Put(int bidId, EditBidModel model)
        {
            var result = await _service.Put(bidId, model);
            var models = _mapper.Map<BidModel>(result);
            return models;
        }
        [HttpPut("AcceptBid/{bidId}")]
        public async Task<bool> ApproveBid(int bidId)
        {
            var item = await _service.ApproveBid(bidId);
            if (item != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}