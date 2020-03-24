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
    public class FixedFiledService : IFixedFiledService
    {
        private readonly IUnitOfWork _uniteOfwork;

        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;
        private readonly IFixedFiledDetailsService _fixedFiledDetailsService;

        public FixedFiledService(IUnitOfWork uniteOfwork, IOptions<AppSettings> appSettings, IAutoMapper mapper,
            IFixedFiledDetailsService fixedFiledDetailsService)
        {
            _uniteOfwork = uniteOfwork;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _fixedFiledDetailsService = fixedFiledDetailsService;
        }
        public List<FixedFiledViewModel> Get()
        {
            var fixedfileds = _uniteOfwork.Query<FixedFiled>()
                                //.Include("Fields.FixedFiledDetails")
                                .ToList();
            // .Where(x => x.IsActive);


            return fixedfileds.Select(x => x.ToModel(_mapper)).ToList();
        }
        public FixedFiledViewModel GetByID(int fixedFiledId)
        {
            var fixedfiled = _uniteOfwork.Find<FixedFiled>(fixedFiledId);
            return fixedfiled.ToModel(_mapper);
        }
    }
}
