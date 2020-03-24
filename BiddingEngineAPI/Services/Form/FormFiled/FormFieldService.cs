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
    public class FormFieldService : IFormFieldService
    {
        private readonly IUnitOfWork _uniteOfwork;
        
        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;
        private readonly IFieldOptionService _filedOptionService;

        public FormFieldService(IUnitOfWork uniteOfwork,  IOptions<AppSettings> appSettings, IAutoMapper mapper,
            IFieldOptionService filedOptionService)
        {
            _uniteOfwork = uniteOfwork;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _filedOptionService = filedOptionService;
        }
        public void CreateCollection(int FormId,ICollection<FormFieldCreateModel> model)
        {
            ////
            var LstOtherFileds = model.Where(x => x.FiledType != (int)FieldType.Dropdown).ToList();
            foreach (var filed in LstOtherFileds)
            {
                var NewObj = Create(FormId, filed);
                _filedOptionService.CreateCollection(NewObj.Id, filed.FiledOptions);

            }


            var LstDropFileds = model.Where(x => x.FiledType == (int)FieldType.Dropdown).ToList();
            AddDropDownList(FormId, LstDropFileds);



        }
     
        public FormField Create(int FormId, FormFieldCreateModel model,int? parentId=null)
        {
          
            var formField = new FormField
            {
                CreateDate = DateTime.Now,
                FiledType=(FieldType) model.FiledType,
                FormId = FormId,
                IsActive = true,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                Hint = model.Hint,
                HasParent = model.HasParent,
                HasChild = model.HasChild,
                IsRequired = model.IsRequried,//? model.IsRequried,
                ParentId = parentId,
                Order=model.order,
                HasPredefined=model.HasPredefined,
                PredfinedID=model.PredefinedID

            };

            _uniteOfwork.Add(formField);
             _uniteOfwork.Commit();

            return formField;
        }
        public FormField ChildFiled(int ParentFiledID)
        {
          var Child=  _uniteOfwork.Query<FormField>()
                .Where(x=>x.ParentId== ParentFiledID).FirstOrDefault();
            return Child;
        }
        public void AddDropDownList(int FormId, ICollection<FormFieldCreateModel> model)
        {
           List<FormFieldCreateModel> UpdatedList = new List<FormFieldCreateModel>();
            //order DropLevel 
            foreach (var dropobj in model)
            {
                var childs = model.Where(x => x.ParentIndex == dropobj.FiledIndex);
                dropobj.HasChild = childs.Any();
                if (dropobj.HasParent)
                {
                    var parent = model.Where(x => x.FiledIndex == dropobj.ParentIndex).FirstOrDefault();
                    var Newobj = Create(FormId, dropobj, parent.ID);
                        dropobj.ID = Newobj.Id;
                        dropobj.ParentID = parent.ID;

                }
                else
                { 

                    var Newobj= Create(FormId, dropobj);
                    dropobj.ID = Newobj.Id;
                 
                }
            }
            _filedOptionService.AddDropDownListOption(model);

        }
    }
}
