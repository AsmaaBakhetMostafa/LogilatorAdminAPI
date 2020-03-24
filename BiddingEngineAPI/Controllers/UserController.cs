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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IAutoMapper _mapper;
        private readonly IMailService _mailService;
        private readonly AppSettings _appSettings;


        public UsersController(IUserService service, IMailService mailService, IAutoMapper mapper, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _mailService = mailService;
            _mapper = mapper;
            _service = service;
        }

        //[Authorize]
        [HttpGet]
        public IQueryable<UserModel> Get()
        {
            var result = _service.Get();
            var models = _mapper.Map<User, UserModel>(result);
            return models;

        }
        [HttpGet("usertype/GetServiceProviderUser/{CurrentUserID}")]
        public List<UserChatViewModel> GetServiceProviderUser(int CurrentUserID)
        {
            var result = _service.GetServiceProviderUser(CurrentUserID);
            return result;

        }
        [HttpGet("usertype/{userTypeId}")]
        public List<UserForAdminModel> GetByUserTypeId(int userTypeId)
        {
            var result = _service.GetNewUserRequestsByUserType(userTypeId);
            return result;

        }

        //[HttpPost("Register")]
        ////[ValidateModel]
        //public async Task<UserModel> Post([FromBody]CreateUserModel requestModel)
        //{
        //    var item = await _service.Create(requestModel);
        //    var model = _mapper.Map<UserModel>(item);

        //    //_mailService.SendVerificationLinkEmail(item.Name, item.Email, item.ActivationCode, _appSettings.ActivationUrl);

        //    return model;
        //}

        [HttpPut("AcceptUser/{userId}")]
        public async Task<string> ActivateAccount(int userId)
        {
            var item = await _service.AcceptUser(userId);
            if (item != null)
            {
                return "User Accepted Successfully";
            }
            else
                return "Error Occured";
        }

        [HttpPut("RejectUser/{userId}")]
        public async Task<string> RejectUser(int userId)
        {
            var item = await _service.RejectUser(userId);
            if (item != null)
            {
                return "User Rejected Successfully";
            }
            else
                return "Error Occured";
        }

        [HttpPost("Authenticate")]
        //[ValidateModel]
        public UserWithToken Authenticate(AuthenticateModel requestModel)
        {
            var item = _service.Authenticate(requestModel);
            var model = _mapper.Map<UserWithToken>(item);
            return model;
        }
    }
}