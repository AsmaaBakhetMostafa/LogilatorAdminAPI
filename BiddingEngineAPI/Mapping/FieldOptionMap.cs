using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Mapping
{
    public class FieldOptionMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<FieldOption, FieldOptionViewModel>();
            map.ForMember(x => x.Id, x => x.MapFrom(u => u.Id))
                .ForMember(x => x.HasChild, x => x.MapFrom(u => u.HasChild))
                .ForMember(x => x.FormFiled, x => x.Ignore());

        }
    }
}
