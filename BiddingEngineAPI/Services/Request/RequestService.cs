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
using BiddingEngineAPI.ApiModels.Request;
using BiddingEngineAPI.EFCore.Enum;

namespace BiddingEngineAPI.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _uow;
        
        private readonly IAutoMapper _mapper;

        private readonly IFieldOptionService _fieldOptionService;

        public RequestService(IUnitOfWork uow,  IAutoMapper mapper, IFieldOptionService fieldOptionService)
        {
            _uow = uow;
            _mapper = mapper;
            _fieldOptionService = fieldOptionService;
        }

        public async Task<Request> Create(CreateRequestModel model)
        {
            for (int i = 0; i < model.RequestDetails.Count; i++)
            {
                if (model.RequestDetails[i].FieldTypeId == 10)
                {
                    model.RequestDetails[i].Value = UploadImageHelper.SaveImage(model.RequestDetails[i].Value, "Requests");
                }
                else if (model.RequestDetails[i].FieldTypeId == 8)
                {
                    model.RequestDetails[i].Value = UploadImageHelper.SavePdf(model.RequestDetails[i].Value, "Requests");
                }
            }
            
            var request = _mapper.Map<Request>(model);
            request.StatusId = (int) RequestStatus.Opened;
            _uow.Add(request);
            await _uow.CommitAsync();
            
            return request;
        }

        public async Task<Request> Put(int requestId, EditRequestModel model)
        {
            var requestModel = GetQuery().Include("RequestDetails").Include("RequestDetails.FormField").FirstOrDefault(x => x.Id == requestId);
            if (requestModel == null)
            {
                throw new NotFoundException();
            }

            var request = _mapper.Map<Request>(requestModel);

            for (int i = 0; i < request.RequestDetails.Count; i++)
            {
                var newValue = model.RequestDetails.Where(a => a.Id == request.RequestDetails[i].Id).FirstOrDefault();
                
                if (newValue != null && newValue.FormFieldId == 10 && request.RequestDetails[i].Value != newValue.Value)
                {
                    model.RequestDetails[i].Value = UploadImageHelper.SaveImage(model.RequestDetails[i].Value, "Requests");
                }
                else if (newValue != null && newValue.FormFieldId == 8 && request.RequestDetails[i].Value != newValue.Value)
                {
                    model.RequestDetails[i].Value = UploadImageHelper.SavePdf(model.RequestDetails[i].Value, "Requests");
                }
                else
                {
                    request.RequestDetails[i].Value = newValue == null ? "" : newValue.Value;
                }
            }

            var deletedItems = request.RequestDetails.Where(a=> !model.RequestDetails.Any(x=> a.Id == x.Id )).Select(a=>a.Id).ToArray();
            foreach (var item in deletedItems)
            {
                var reqDetail = request.RequestDetails.Where(a => a.Id == item).First();
                request.RequestDetails.Remove(reqDetail);
            }

            var addedItems = model.RequestDetails.Where(a => a.Id == 0);
            foreach (var item in addedItems)
            {
                request.RequestDetails.Add(new RequestDetail
                {
                    RequestId = requestId,
                    FormFieldId = item.FormFieldId,
                    Value = item.Value
                });
            }

            await _uow.CommitAsync();

            return request;
        }

        public IQueryable<Request> Get()
        {
            var query = GetQuery();

            return query;
        }
        private IQueryable<Request> GetQuery()
        {
            return _uow.Query<Request>();
        }

        public RequestModel Get(int id)
        {
            var Request = GetQuery().Include("RequestDetails").Include("RequestDetails.FormField").Include("Form").FirstOrDefault(x => x.Id == id);

            if (Request == null)
            {
                throw new NotFoundException("Request is not found");
            }
            var model = _mapper.Map<RequestModel>(Request);
            foreach (var item in model.RequestDetails)
            {
                if (item.FieldTypeId == (int)EFCore.Enum.FieldType.Dropdown ||
                item.FieldTypeId == (int) EFCore.Enum.FieldType.CheckBox ||
                item.FieldTypeId == (int) EFCore.Enum.FieldType.RadioButton)
                {

                    var option = _fieldOptionService.Get(Convert.ToInt32(string.IsNullOrEmpty(item.Value) ? "0" : item.Value));
                    item.ValueText = option == null ? "" : option.NameAr;
                }
            }
            return model;
        }

        public async Task<Request> ApproveRequest(int RequestId)
        {
            var Request = GetQuery().FirstOrDefault(x => x.Id == RequestId);

            if (Request == null)
            {
                throw new NotFoundException("Request not found");
            }

            await _uow.CommitAsync();
            return Request;
        }

        public IQueryable<Request> GetByUserId(int userId)
        {
            var Requests = GetQuery().Where(x => x.UserId == userId).OrderByDescending(a => a.CreateDate);

            return Requests;
        }

        public IQueryable<Request> GetAllOpenedRequests()
        {
            var Requests = GetQuery().Where(a=>a.StatusId == (int)RequestStatus.Opened).OrderByDescending(a => a.CreateDate);

            return Requests;
        }
    }
}
