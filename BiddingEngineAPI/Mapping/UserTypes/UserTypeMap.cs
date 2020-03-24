using AutoMapper;
using BiddingEngineAPI.ApiModels;
using BiddingEngineAPI.ApiModels.Request;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.Mapping
{
    public class UserTypeMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var mapEng = configuration.CreateMap<UserType, UserTypeModel>();
        }
    }
}
