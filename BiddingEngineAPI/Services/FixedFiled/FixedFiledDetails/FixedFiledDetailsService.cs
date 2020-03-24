using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BiddingEngineAPI.ApiModels.Users;
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
using BiddingEngineAPI.EFCore.Enum;

namespace BiddingEngineAPI.Services
{
    public class FixedFiledDetailsService : IFixedFiledDetailsService
    {
        private readonly IUnitOfWork _uniteOfwork;
        
        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;

        public FixedFiledDetailsService(IUnitOfWork uniteOfwork,  IOptions<AppSettings> appSettings, IAutoMapper mapper)
        {
            _uniteOfwork = uniteOfwork;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public List<FixedFiledDetails> GetAllPredefinedOptions(int FixedfieldID)
        {
            var fixedDetails = _uniteOfwork.Query<FixedFiledDetails>()
                                  .Where(x => x.FixedFiledId == FixedfieldID);
                                 
            return fixedDetails.ToList();
        }

    }
}
