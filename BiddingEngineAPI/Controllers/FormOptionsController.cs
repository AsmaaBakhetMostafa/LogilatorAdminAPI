using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Mapping;
using BiddingEngineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiddingEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormOptionsController : ControllerBase
    {
        private readonly IFieldOptionService _service;
        private readonly IAutoMapper _mapper;


        public FormOptionsController(IFieldOptionService service, IAutoMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("{ParentId}")]
        public IQueryable<FieldOptionViewModel> GetByParentId(int ParentId)
        {
            var result = _service.GetOptionsByParentId(ParentId);
            var models = _mapper.Map< FieldOption, FieldOptionViewModel >(result);
            return models;

        }
    }
}