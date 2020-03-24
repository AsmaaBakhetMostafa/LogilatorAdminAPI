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

namespace BiddingEngineAPI.Services
{
    public class FieldOptionService : IFieldOptionService
    {
        private readonly IUnitOfWork _uniteOfwork;
        
        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;

        public FieldOptionService(IUnitOfWork uniteOfwork,  IOptions<AppSettings> appSettings, IAutoMapper mapper)
        {
            _uniteOfwork = uniteOfwork;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        public List<FieldOption> CreateCollection(int FiledID,ICollection<FieldOptionCreateModel> model)
        {
            List<FieldOption> AddedOptions = new List<FieldOption>();
            foreach (var filed in model)
            {
                AddedOptions.Add(Create(FiledID,filed));

            }
            return AddedOptions;
        }
        public FieldOption Create(int FiledID ,FieldOptionCreateModel model, int? ParentID=null)
        {

            var filedOption = new FieldOption
            {
                CreateDate = DateTime.Now,
                FormFiledId = FiledID,
                IsActive = true,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                HasParent = model.HasParent,
                HasChild=model.HasChild,
                ParentId = ParentID,
                

            };

            _uniteOfwork.Add(filedOption);
             _uniteOfwork.Commit();

            return filedOption;
        }

        public IQueryable<FieldOption> GetOptionsByParentId(int parentId)
        {
            var query = GetQuery().Where(a=>a.ParentId == parentId);

            return query;
        }

        public IQueryable<FieldOption> Get()
        {
            var query = GetQuery();

            return query;
        }

        private IQueryable<FieldOption> GetQuery()
        {
            return _uniteOfwork.Query<FieldOption>();
        }
        public void AddDropDownListOption(ICollection<FormFieldCreateModel> model)
        {
            List<FieldOptionCreateModel> Neop = new List<FieldOptionCreateModel>();
            List<FormFieldCreateModel> UpdatedList = new List<FormFieldCreateModel>();
            //order DropLevel 
            foreach (var op in model)
            {
                op.FiledOptions.ToList().ForEach(i => { i.FiledID = op.ID;i.ParentFiledID = op.ParentID; });
                Neop.AddRange(op.FiledOptions);
            }
            foreach (var dropobj in model)
            {
                foreach (var op in dropobj.FiledOptions)
                {
                    dropobj.HasChild = Neop.Where(x =>  x.parentOptionIndex == op.OptionIndex
                                                    && x.FiledID == op.ParentFiledID).Any();
                
                    if (op.HasParent)
                    {
                        var parent = Neop.Where(x => x.OptionIndex == op.parentOptionIndex
                                                    &&x.FiledID==op.ParentFiledID).FirstOrDefault();
                        if (parent != null)
                        {
                            
                            var Newobj = Create(dropobj.ID, op, parent.ID);
                            op.ID = Newobj.Id;
                        }
                        else
                        {
                            var Newobj = Create(dropobj.ID, op);
                            op.ID = Newobj.Id;
                        }
                    }
                    else
                    {
                        var Newobj = Create(dropobj.ID, op);
                        op.ID = Newobj.Id;

                    }
                }
            }

        }

        public FieldOption Get(int id)
        {
            var query = GetQuery().Where(a => a.Id == id).FirstOrDefault();

            return query;
        }
    }
}
