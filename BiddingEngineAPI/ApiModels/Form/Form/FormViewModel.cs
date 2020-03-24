using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BiddingEngineAPI.ApiModels.Users
{
    public static class FormExtensions
    {
        private static IAutoMapper _mapper;

        public static FormViewModel ToModel(this Form target, IAutoMapper mapper)
        {
            _mapper = mapper;
            var model = new FormViewModel();  // _mapper.Map<FormFiledViewModel>(target);
            if (target != null)
            {
                model.Id = target.Id;
                model.CreateDate = target.CreateDate;
                model.Descr = target.Description;
                model.IsActive = target.IsActive.Value;
                model.NameAr = target.NameAr;
                model.NameEn = target.NameEn;
                model.RequestType = target.RequestType;
                model.UserType = target.UserType;
                model.Fileds = target.Fields != null ? target.Fields.Select(x => x.ToModel()).ToList() : null;

            }

            //Return Result
            return model;
        }
    }
    public class FormViewModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Descr { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int RequestType { get; set; }
        public int UserType { get; set; }

        public ICollection<FormFieldViewModel> Fileds { get; set; }

    }
}
