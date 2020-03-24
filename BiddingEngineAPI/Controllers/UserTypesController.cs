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
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypeService _service;
        private readonly IAutoMapper _mapper;

        public UserTypesController(IUserTypeService service, IAutoMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        //[Authorize(Roles = "ServiceProvider")]
        [HttpGet]
        public IQueryable<UserTypeModel> Get()
        {
            var result = _service.Get();
            var models = _mapper.Map<UserType, UserTypeModel>(result);
            return models;
        }

        //[Authorize(Roles = "Client,ServiceProvider")]
        [HttpGet("{id}")]
        public UserTypeModel GetByID(int id)
        {
            var result = _service.Get(id);
            var models = _mapper.Map<UserType, UserTypeModel>(result);
            return models.FirstOrDefault();
        }
    }
}