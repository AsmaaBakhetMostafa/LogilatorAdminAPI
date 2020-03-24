using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Mapping;
using BiddingEngineAPI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.ApiModels.Users
{
    public static class FormFiledExtensions
    {
        private static readonly IAutoMapper _mapper;

        public static IServiceProvider provider;
        public static FormFieldViewModel ToModel(this FormField target)
        {
          //  var _service=(FormFieldService)provider.GetService(typeof(IFormFieldService));
       //     var Child = _service.ChildFiled(target.Id);

            var model = new FormFieldViewModel();  // _mapper.Map<FormFiledViewModel>(target);
            model.Id = target.Id;
            model.CreateDate = target.CreateDate;
            model.FiledType =(int)target.FiledType;
            model.FormId = target.FormId;
            model.IsActive = target.IsActive.Value;
            model.IsRequired = target.IsRequired;
            model.HasParent = target.HasParent;
            model.HasChild = target.HasChild;
            model.NameAr = target.NameAr;
            model.NameEn = target.NameEn;
            model.Hint = target.Hint;
            model.Order = target.Order;
            model.ParentId = target.ParentId;
            model.HasPredefined = target.HasPredefined;
            model.PredfinedID = target.PredfinedID;
            model.FiledOptions = target.FiledOptions.Select(x => x.ToModel(_mapper)).ToList();
            //Return Result
            return model;
        }
    }
    public class FormFieldViewModel
    {
       
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Hint { get; set; }

        public bool IsRequired { get; set; } 
        public bool HasParent { get; set; }
        public bool HasChild{ get; set; }
        public int? ChildId { get; set; }

        public int FiledType { get; set; }
        public int Order { get; set; }

        public int FormId { get; set; }

        public int? ParentId { get; set; }
        public FormFieldViewModel Parent { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool? HasPredefined { get; set; }
        public int? PredfinedID { get; set; }
        public ICollection<FieldOptionViewModel> FiledOptions { get; set; }

    }
}
