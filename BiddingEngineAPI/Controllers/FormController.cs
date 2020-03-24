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
    public class FormController : ControllerBase
    {
        private readonly IFormService _service;
        private readonly IAutoMapper _mapper;
        private readonly IMailService _mailService;
        private readonly AppSettings _appSettings;


        public FormController(IFormService service, IMailService mailService, IAutoMapper mapper, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _mailService = mailService;
            _mapper = mapper;
            _service = service;
        }

     
        [HttpPost("CreateNewForm")]
        //[ValidateModel]
        public void CreateNewForm(FormCreateModel requestModel)
        {
             _service.Create(requestModel);
        }

        [HttpPost("UpdateForm")]
        //[ValidateModel]
        public void UpdateForm(FormCreateModel requestModel)
        {
            _service.Edit(requestModel);
        }
        [HttpPost("UpdateBiddForm")]
        //[ValidateModel]
        public void UpdateBiddForm(FormCreateModel requestModel)
        {
            _service.EditBiddForm(requestModel);
        }
        [HttpPost("CreateNewBiddingForm")]
        //[ValidateModel]
        public void CreateNewBiddingForm(FormCreateModel requestModel)
        {
            _service.CreateBiddingForm(requestModel);
        }
        //[Authorize]
        [HttpGet("GetActiveRequestForm/{RequestType}")]
        public FormViewModel GetActiveRequestForm(int RequestType)
        {
            var result = _service.GetActiveRequestForm(RequestType);


            return result;

        }
      
        [HttpGet("GetAllForm")]
        public List<FormViewModel> GetAllForm()
        {
            var result = _service.GetAllForms();
            return result;

        }
        [HttpGet("GetAllBiddingForm")]
        public List<FormViewModel> GetAllBiddingForm(int requestType)
        {
            var result = _service.GetAllBiddingForms(requestType);
            return result;

        }
        
        [HttpPut("ActivateForm/{formId}")]
        public FormViewModel ActivateForm(int formId)
        {
            var form =  _service.ActivateForm(formId);
            return form;
        }
        [HttpPut("DeActivateForm/{formId}")]
        public FormViewModel DeActivateForm(int formId)
        {
            var form =  _service.DeActivateForm(formId);
            return form;
        }

        ///////
        [HttpPut("ActivateBiddingForm/{formId}")]
        public FormViewModel ActivateBiddingForm(int formId)
        {
            var form = _service.ActivateBiddingForm(formId);
            return form;
        }
        [HttpPut("DeActivateBiddingForm/{formId}")]
        public FormViewModel DeActivateBiddingForm(int formId)
        {
            var form = _service.DeActivateBiddingForm(formId);
            return form;
        }

        [HttpGet("GetActiveBiddingForm/{userType}/{requestType}")]
        public FormViewModel GetActiveBiddingForm(int userType,int requestType)
        {
            var result = _service.GetActiveBiddingForm(userType,requestType);
            
            return result;

        }

        [HttpGet("{formId}")]
        public FormViewModel GetById(int formId)
        {
            var result = _service.GetById(formId);

            return result;

        }
    }
}