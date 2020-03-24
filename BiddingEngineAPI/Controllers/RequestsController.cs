using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.ApiModels.Request;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Filters;
using BiddingEngineAPI.Helpers;
using BiddingEngineAPI.Mapping;
using BiddingEngineAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BiddingEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _service;
        private readonly IAutoMapper _mapper;

        public RequestsController(IRequestService service, IAutoMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public IQueryable<RequestModel> Get()
        {
            var result = _service.Get();
            var models = _mapper.Map<Request, RequestModel>(result);
            return models;
        }

        [HttpGet("{requestId}")]
        public RequestModel Get(int requestId)
        {
            var result = _service.Get(requestId);
            //var model = _mapper.Map<RequestModel>(result);
            return result;

        }

        [HttpGet("client/{clientId}")]
        public IQueryable<RequestListModel> GetAllRequestsByClient(int clientId)
        {
            var result = _service.GetByUserId(clientId);
            var models = _mapper.Map<Request, RequestListModel>(result);
            return models;
        }

        [HttpGet("Open")]
        public IQueryable<RequestListModel> GetAllOpenedRequests()
        {
            var result = _service.GetAllOpenedRequests();
            var models = _mapper.Map<Request, RequestListModel>(result);
            return models;
        }

        [HttpPost]
        public async Task<RequestModel> Post(CreateRequestModel model)
        {
            var result =  await _service.Create(model);
            var models = _mapper.Map<RequestModel>(result);
            return models;
        }

        [HttpPut("{requestId}")]
        public async Task<RequestModel> Put(int requestId, EditRequestModel model)
        {
            var result = await _service.Put(requestId, model);
            var models = _mapper.Map<RequestModel>(result);
            return models;
        }

    }
}