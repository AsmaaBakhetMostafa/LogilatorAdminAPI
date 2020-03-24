using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public static class FiledOptionExtensions
    {
        private static  IAutoMapper _mapper;

        public static FieldOptionViewModel ToModel(this FieldOption target, IAutoMapper mapper)
        {
            _mapper = mapper;
            //var model = _mapper.Map<FiledOptionViewModel>(target);
            var model = new FieldOptionViewModel();  // _mapper.Map<FormFiledViewModel>(target);
            model.Id = target.Id;
            model.CreateDate = target.CreateDate;
            model.FormFiledId = target.FormFiledId;
            model.IsActive = target.IsActive.Value;
            model.HasParent = target.HasParent;
            model.HasChild = target.HasChild;
            model.NameAr = target.NameAr;
            model.NameEn = target.NameEn;
            model.ParentId = target.ParentId;
            //Return Result
            return model;
        }
    }
    public class FieldOptionViewModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public bool HasParent { get; set; }
        public bool HasChild { get; set; }

        public int? ParentId { get; set; }
       
        public FieldOptionViewModel Parent { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public int FormFiledId { get; set; }
        public FormFieldViewModel FormFiled { get; set; }
    }
}
