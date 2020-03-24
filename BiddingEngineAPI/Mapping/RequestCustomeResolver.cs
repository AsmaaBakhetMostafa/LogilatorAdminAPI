using AutoMapper;
using BiddingEngineAPI.ApiModels.Request;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.Mapping
{
    public class RequestCustomeResolver :  IValueResolver<RequestDetail, RequestDetailModel, string>
    {
        private readonly IFieldOptionService _fieldOptionService;
        public RequestCustomeResolver(IFieldOptionService fieldOptionService)
        {
            _fieldOptionService = fieldOptionService;
        }
        
        public string Resolve(RequestDetail source, RequestDetailModel destination, string destMember, ResolutionContext context)
        {
            if (source.FormField.FiledType == EFCore.Enum.FieldType.Dropdown || 
                source.FormField.FiledType == EFCore.Enum.FieldType.CheckBox ||
                source.FormField.FiledType == EFCore.Enum.FieldType.RadioButton)
            {
                
                var value = _fieldOptionService.Get(Convert.ToInt32(source.Value));
                return value == null ? "" : value.NameAr;
            }
            return source.Value;
        }
    }
}
