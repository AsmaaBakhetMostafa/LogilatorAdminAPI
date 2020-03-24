using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiddingEngineAPI.ApiModels.Bid;
using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Mapping
{
    public class BidMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var map = configuration.CreateMap<Bid, BidModel>()
                .ForMember(x => x.BidDetails, x => x.MapFrom(u => u.BidDetails.OrderBy(a => a.FormField == null ? 0 : a.FormField.Order)));

            var reqDetailmap = configuration.CreateMap<BidDetail, BidDetailModel>()
                .ForMember(x => x.FieldTypeId, x => x.MapFrom(u => (int)u.FormField.FiledType))
                .ForMember(x => x.formFieldNameAr, x => x.MapFrom(u => u.FormField.NameAr))
                .ForMember(x => x.formFieldNameEn, x => x.MapFrom(u => u.FormField.NameEn))
                .ForMember(x => x.Order, x => x.MapFrom(u => u.FormField.Order));
            //.ForMember(x => x.FormFieldTypeId, x => x.MapFrom(u => u.FormField.));
            //.ForMember(x => x.Value, d => d.MapFrom<BidCustomeResolver>());

            var createreqmap = configuration.CreateMap<CreateBidModel, Bid>()
                .ForMember(x => x.CreateDate, x => x.MapFrom(u => DateTime.Now));

            var createReqDetailsmap = configuration.CreateMap<CreateBidDetailModel, BidDetail>();
            createReqDetailsmap.ForMember(x => x.FormFieldId, x => x.MapFrom(u => u.FormFieldId));
                
        }
    }
}
