using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiddingEngineAPI.ApiModels.Request;
using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Mapping
{
    public class RequestMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            var reqDetailmap = configuration.CreateMap<RequestDetail, RequestDetailModel>()
                .ForMember(x => x.FieldTypeId, x => x.MapFrom(u => (int)u.FormField.FiledType))
                .ForMember(x => x.formFieldNameAr, x => x.MapFrom(u => u.FormField.NameAr))
                .ForMember(x => x.formFieldNameEn, x => x.MapFrom(u => u.FormField.NameEn))
                .ForMember(x => x.Order, x => x.MapFrom(u => u.FormField.Order));

            var map = configuration.CreateMap<Request, RequestModel>()
                .ForMember(x => x.FormNameAr, x => x.MapFrom(u => u.Form.NameAr))
                .ForMember(x => x.FormNameEn, x => x.MapFrom(u => u.Form.NameEn))
                .ForMember(x => x.RequestType, x => x.MapFrom(u => u.Form.RequestType))
                .ForMember(x => x.RequestDetails, x => x.MapFrom(u => u.RequestDetails.OrderBy(a => a.FormField == null ? 0 : a.FormField.Order)));
            //.ForAllMembers(opts => opts.Condition((src, dest) => src != null));

            var mapRequestList = configuration.CreateMap<Request, RequestListModel>();
            //.ForMember(x => x.FormFieldTypeId, x => x.MapFrom(u => u.FormField.));
            //.ForMember(x => x.Value, d => d.MapFrom<RequestCustomeResolver>());

            var createreqmap = configuration.CreateMap<CreateRequestModel, Request>()
                .ForMember(x => x.CreateDate, x => x.MapFrom(u => DateTime.Now));

            var createReqDetailsmap = configuration.CreateMap<CreateRequestDetailModel, RequestDetail>();
            createReqDetailsmap.ForMember(x => x.FormFieldId, x => x.MapFrom(u => u.FormFieldId));

            var reversemap = configuration.CreateMap<RequestModel, Request>();
            var reversereqDetailmap = configuration.CreateMap<RequestDetailModel, RequestDetail>();
        }
    }
}
