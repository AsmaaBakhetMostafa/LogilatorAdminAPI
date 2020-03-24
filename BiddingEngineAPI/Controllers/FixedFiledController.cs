using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FixedFiledController : ControllerBase
    {
        private readonly IFixedFiledService _service;
        private readonly IAutoMapper _mapper;
        private readonly IMailService _mailService;
        private readonly AppSettings _appSettings;


        public FixedFiledController(IFixedFiledService service, IMailService mailService, IAutoMapper mapper, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _mailService = mailService;
            _mapper = mapper;
            _service = service;
        }
        [HttpGet]
        public List<FixedFiledViewModel> Get()
        {
            var result = _service.Get();
            return result;
        }

    }
}