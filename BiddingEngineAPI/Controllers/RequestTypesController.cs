using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.ApiModels;
using BiddingEngineAPI.ApiModels.Users;
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
    public class RequestTypesController : ControllerBase
    {
        private readonly IRequestTypeService _service;
        private readonly IAutoMapper _mapper;

        public RequestTypesController(IRequestTypeService service, IAutoMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        //[Authorize(Roles = "ServiceProvider")]
        [HttpGet]
        public IQueryable<RequestTypeModel> Get()
        {
            var result = _service.Get();
            var models = _mapper.Map<RequestType, RequestTypeModel>(result);
            return models;
        }

        //[Authorize(Roles = "Client,ServiceProvider")]
        [HttpGet("{id}")]
        public RequestTypeModel GetByID(int id)
        {
            var result = _service.Get(id);
            var models = _mapper.Map<RequestType, RequestTypeModel>(result);
            return models.FirstOrDefault();
        }
    }
}