
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
using BiddingEngineAPI.ApiModels.Bid;
using BiddingEngineAPI.EFCore.Enum;

namespace BiddingEngineAPI.Services
{
    public class BiddingService : IBiddingService
    {
        private readonly IUnitOfWork _uow;
        
        private readonly IAutoMapper _mapper;

        private readonly IRequestService _requestService;

        private readonly IFieldOptionService _fieldOptionService;

        public BiddingService(IUnitOfWork uow,  IAutoMapper mapper, IFieldOptionService fieldOptionService, IRequestService requestService)
        {
            _uow = uow;
            _mapper = mapper;
            _fieldOptionService = fieldOptionService;
            _requestService = requestService;
        }

        public async Task<Bid> Create(CreateBidModel model)
        {
            for (int i = 0; i < model.BidDetails.Count; i++)
            {
                if (model.BidDetails[i].FieldTypeId == 10)
                {
                    model.BidDetails[i].Value = UploadImageHelper.SaveImage(model.BidDetails[i].Value, "Bids");
                }
                else if (model.BidDetails[i].FieldTypeId == 8)
                {
                    model.BidDetails[i].Value = UploadImageHelper.SavePdf(model.BidDetails[i].Value, "Bids");
                }
            }

            var request = _mapper.Map<Bid>(model);
            _uow.Add(request);
            await _uow.CommitAsync();
            
            return request;
        }

        public async Task<Bid> Put(int bidId, EditBidModel model)
        {
            var bidModel = GetQuery().Include("BidDetails").Include("BidDetails.FormField").FirstOrDefault(x => x.Id == bidId);
            if (bidModel == null)
            {
                throw new NotFoundException();
            }

            var bid = _mapper.Map<Bid>(bidModel);

            for (int i = 0; i < bid.BidDetails.Count; i++)
            {
                var newValue = model.BidDetails.Where(a => a.Id == bid.BidDetails[i].Id).FirstOrDefault();

                if (newValue != null && newValue.FormFieldId == 10 && bid.BidDetails[i].Value != newValue.Value)
                {
                    model.BidDetails[i].Value = UploadImageHelper.SaveImage(model.BidDetails[i].Value, "Bids");
                }
                else if (newValue != null && newValue.FormFieldId == 8 && bid.BidDetails[i].Value != newValue.Value)
                {
                    model.BidDetails[i].Value = UploadImageHelper.SavePdf(model.BidDetails[i].Value, "Bids");
                }
                else
                {
                    bid.BidDetails[i].Value = newValue == null ? "" : newValue.Value;
                }
            }

            var deletedItems = bid.BidDetails.Where(a => !model.BidDetails.Any(x => a.Id == x.Id)).Select(a => a.Id).ToArray();
            foreach (var item in deletedItems)
            {
                var reqDetail = bid.BidDetails.Where(a => a.Id == item).First();
                bid.BidDetails.Remove(reqDetail);
            }

            var addedItems = model.BidDetails.Where(a => a.Id == 0);
            foreach (var item in addedItems)
            {
                bid.BidDetails.Add(new  BidDetail
                {
                    BidId = bidId,
                    FormFieldId = item.FormFieldId,
                    Value = item.Value
                });
            }

            await _uow.CommitAsync();

            return bid;
        }

        public IQueryable<Bid> Get()
        {
            var query = GetQuery();

            return query;
        }
        private IQueryable<Bid> GetQuery()
        {
            return _uow.Query<Bid>();
        }

        public BidModel Get(int id)
        {
            var Bid = GetQuery().Include("BidDetails").Include("BidDetails.FormField").FirstOrDefault(x => x.Id == id);

            if (Bid == null)
            {
                throw new NotFoundException("Bid is not found");
            }
            var model = _mapper.Map<BidModel>(Bid);
            foreach (var item in model.BidDetails)
            {
                if (item.FieldTypeId == (int)EFCore.Enum.FieldType.Dropdown ||
                item.FieldTypeId == (int) EFCore.Enum.FieldType.CheckBox ||
                item.FieldTypeId == (int) EFCore.Enum.FieldType.RadioButton)
                {

                    var option = _fieldOptionService.Get(Convert.ToInt32(item.Value));
                    item.ValueText = option == null ? "" : option.NameAr;
                }
            }
            return model;
        }
        public bool IsUserAddedBidding(int userId,int requestId)
        {
            return GetQuery().Where(a => a.RequestId == requestId && a.UserId == userId).Count() > 0;
        }

        public bool IsUserApproveBidding(int userTypeId, int requestId)
        {
            return GetQuery().Where(a => a.RequestId == requestId && a.UserTypeId == userTypeId && a.IsApproved == true).Count() > 0;
        }
        public async Task<Bid> ApproveBid(int BidId)
        {
            var Bid = GetQuery().FirstOrDefault(x => x.Id == BidId);
            Bid.IsApproved = true;
            
            var request = _uow.Query<Request>().FirstOrDefault(a => a.Id == Bid.RequestId);
            request.StatusId = (int) RequestStatus.Completed;

            if (Bid == null)
            {
                throw new NotFoundException("Bid not found");
            }

            await _uow.CommitAsync();
            return Bid;
        }

        public IQueryable<Bid> GetByUserId(int userId)
        {
            var Bids = GetQuery().Where(x => x.UserId == userId).OrderByDescending(a => a.CreateDate);

            return Bids;
        }

        public IQueryable<BidModel> GetByRequestId(int requestId,int userTypeId,int userId)
        {
            var Bids = GetQuery().Include("BidDetails").Include("BidDetails.FormField")
                .Where(x => x.RequestId == requestId && x.UserTypeId == userTypeId);

            if (Bids == null)
            {
                throw new NotFoundException("Bid is not found");
            }

            var temp = Bids.Where(a=>a.UserId == userId).ToList();
            var otherBids = Bids.Where(a => a.UserId != userId).ToList();
            temp.AddRange(otherBids);

            var model = _mapper.Map<Bid,BidModel>(temp);

            foreach (var bid in model)
            {
                foreach (var item in bid.BidDetails)
                {
                    if (item.FieldTypeId == (int)EFCore.Enum.FieldType.Dropdown ||
                    item.FieldTypeId == (int)EFCore.Enum.FieldType.CheckBox ||
                    item.FieldTypeId == (int)EFCore.Enum.FieldType.RadioButton)
                    {
                        var option = _fieldOptionService.Get(Convert.ToInt32(item.Value));
                        item.ValueText = option == null ? "" : option.NameAr;
                    }
                }
            }
            
            return model.AsQueryable();
        }

        //public IQueryable<Bid> GetAllOpenedBids()
        //{
        //    var Bids = GetQuery().Where(a=>a.StatusId == (int)BidStatus.Open).OrderByDescending(a => a.CreateDate);

        //    return Bids;
        //}
    }
}
