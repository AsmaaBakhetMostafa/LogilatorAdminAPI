using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BiddingEngineAPI.ApiModels.Users
{
    public static class FixedFiledDetailsExtensions
    {
        private static IAutoMapper _mapper;

        public static FieldOptionViewModel ToFiledOptionModel(this FixedFiledDetails target, IAutoMapper mapper)
        {
            _mapper = mapper;
            var model = new FieldOptionViewModel();  
            if (target != null)
            {
                model.Id = target.Id;
                model.NameAr = target.NameAr;
                model.NameEn = target.NameEn;
              //  model.Fileds = target.Fields != null ? target.Fields.Select(x => x.ToModel()).ToList() : null;

            }

            //Return Result
            return model;
        }
    }
    public class FixedFiledDetailsViewModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        //public ICollection<FormFieldViewModel> Fileds { get; set; }

    }
}
