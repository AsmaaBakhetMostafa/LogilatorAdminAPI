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
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _uniteOfwork;
        
        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;
        private readonly IFormFieldService _formFiledService;
        private readonly IFixedFiledService _fixedFiledService;
        private readonly IFixedFiledDetailsService _fixedFiledDetailsService;

        public FormService(IUnitOfWork uniteOfwork,  IOptions<AppSettings> appSettings, IAutoMapper mapper,
            IFormFieldService formFiledService,
            IFixedFiledService fixedFiledService,
            IFixedFiledDetailsService fixedFiledDetailsService
            )
        {
            _uniteOfwork = uniteOfwork;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _formFiledService = formFiledService;
            _fixedFiledService = fixedFiledService;
            _fixedFiledDetailsService = fixedFiledDetailsService;
        }
        public FormViewModel GetActiveRequestForm(int requestType)
        {
            FormViewModel ActiveForm = new FormViewModel();
            var form = _uniteOfwork.Query<Form>().Include("Fields")
                                               .Include("Fields.FiledOptions").ToList()
                                    .Where(x=>x.FormType== 1
                                    && x.RequestType==requestType
                                    && x.IsActive.Value)
                                      .OrderByDescending(x=>x.CreateDate)
                                      .FirstOrDefault();
            if (form != null)
            {
                ActiveForm = form.ToModel(_mapper);

                ActiveForm.Fileds.ToList().ForEach(item =>
                {
                    var Child = _formFiledService.ChildFiled(item.Id);
                    item.HasChild = Child == null ? false : true;
                    if (Child != null)
                        item.ChildId = Child.Id;
                    if (item.HasPredefined.HasValue)
                        if (item.HasPredefined.Value)
                            item.FiledOptions = GetPredefined(item.PredfinedID.Value);
                });

            }

            return ActiveForm;
        }
        private List<FieldOptionViewModel> GetPredefined(int PredefinedID)
        {
            _fixedFiledService.GetByID(PredefinedID);

            var LstPredfined = _fixedFiledDetailsService.GetAllPredefinedOptions(PredefinedID);
            var LstOptions= LstPredfined.Select(x => x.ToFiledOptionModel(_mapper));
            return LstOptions.ToList();
        }
        public List<FormViewModel> GetAllForms()
        {
            var form = _uniteOfwork.Query<Form>()
                                  .Include("Fields")
                                  .Include("Fields.FiledOptions")
                                  .ToList()
                                  .Where(x => x.FormType == 1)
                                  .OrderByDescending(x => x.CreateDate);


            return form.Select(x=>x.ToModel(_mapper)).ToList();
        }
        public List<FormViewModel> GetAllBiddingForms(int requestType)
        {
            var form = _uniteOfwork.Query<Form>()
                                  .Include("Fields")
                                  .Include("Fields.FiledOptions")
                                  .ToList()
                                  .Where(x => x.FormType ==2 &&
                                  x.RequestType==requestType)
                                  .OrderByDescending(x => x.CreateDate);


            return form.Select(x => x.ToModel(_mapper)).ToList();
        }
        public void Create(FormCreateModel model)
        {
            DeActivateRequestForms(model.RequestType);
            var form = new Form
            {
                CreateDate = DateTime.Now,
                Description = model.Descr,
                FormType =1,
                IsActive = true,
                NameAr=model.NameAr,
                NameEn=model.NameEn,
                RequestType=model.RequestType,
                

            };
            
              _uniteOfwork.Add(form);
             _uniteOfwork.Commit();

            _formFiledService.CreateCollection(form.Id,model.Fileds);

        }
       
        public void CreateBiddingForm(FormCreateModel model)
        {
            DeActiveBiddingForms(model.RequestType, model.UserType);
            var form = new Form
            {
                CreateDate = DateTime.Now,
                Description = model.Descr,
                FormType = 2,
                IsActive = true,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                UserType = model.UserType,
                RequestType = model.RequestType,
            };

            _uniteOfwork.Add(form);
            _uniteOfwork.Commit();

            _formFiledService.CreateCollection(form.Id, model.Fileds);

        }
        public void Edit(FormCreateModel model)
        {
            DeActivateRequestForms(model.RequestType);
            var form = new Form
            {
                CreateDate = DateTime.Now,
                Description = model.Descr,
                FormType = 1,  //mean Request
                IsActive = true,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                RequestType = model.RequestType,


            };

            _uniteOfwork.Add(form);
            _uniteOfwork.Commit();

            _formFiledService.CreateCollection(form.Id, model.Fileds);

        }
        public void EditBiddForm(FormCreateModel model)
        {
            DeActiveBiddingForms(model.RequestType,model.UserType);
            var form = new Form
            {
                CreateDate = DateTime.Now,
                Description = model.Descr,
                FormType = 2,  //mean Bidding form
                IsActive = true,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                RequestType = model.RequestType,
                UserType=model.UserType

            };

            _uniteOfwork.Add(form);
            _uniteOfwork.Commit();

            _formFiledService.CreateCollection(form.Id, model.Fileds);

        }

        
        public FormViewModel ActivateForm(int formId)
        {
            var form = _uniteOfwork.Find<Form>(formId);

            if (form == null)
            {
                throw new NotFoundException("form not found");
            }
            if (IsExistActiveForm(formId, form.RequestType))
            {
                throw new ForbiddenException("there is another form activated");
            }
            form.IsActive = true;

             _uniteOfwork.Commit();
            return form.ToModel(_mapper);
        }
        public  FormViewModel DeActivateForm(int formId)
        {
            var form = _uniteOfwork.Find<Form>(formId);

            if (form == null)
            {
                throw new NotFoundException("form not found");
            }
           
            form.IsActive = false;

            _uniteOfwork.Commit();
            return form.ToModel(_mapper);
        }

        ////////
        public FormViewModel ActivateBiddingForm(int formId)
        {
            var form = _uniteOfwork.Find<Form>(formId);

            if (form == null)
            {
                throw new NotFoundException("form not found");
            }
            if (IsExistActiveBiddingForm(formId, form.RequestType,form.UserType))
            {
                throw new ForbiddenException("there is another form activated");
            }
            form.IsActive = true;

            _uniteOfwork.Commit();
            return form.ToModel(_mapper);
        }
        public FormViewModel DeActivateBiddingForm(int formId)
        {
            var form = _uniteOfwork.Find<Form>(formId);

            if (form == null)
            {
                throw new NotFoundException("form not found");
            }

            form.IsActive = false;

            _uniteOfwork.Commit();
            return form.ToModel(_mapper);
        }
        
       private bool IsExistActiveForm(int formId, int requesttype)
        {
            var forms = _uniteOfwork.Query<Form>().ToList()
                                    .Where(x => x.IsActive.Value
                                           && x.RequestType == requesttype
                                           && x.FormType == 1

                                           && x.Id != formId);
            return forms.Any();
        }
        private bool IsExistActiveBiddingForm( int formId,int requesttype,int UserType)
        {
            var forms = _uniteOfwork.Query<Form>().ToList()
                                    .Where(x=>x.IsActive.Value
                                           && x.RequestType== requesttype
                                           && x.UserType== UserType
                                           && x.FormType==2
                                           && x.Id!= formId);
            return forms.Any();
        }
        private void DeActivateRequestForms(int requesttype)
        {
            var forms = _uniteOfwork.Query<Form>().ToList()
                                    .Where(x => x.IsActive.Value
                                           && x.FormType==1
                                           && x.RequestType == requesttype);
            foreach (var form in forms)
            {
                if (form == null)
                {
                    throw new NotFoundException("form not found");
                }

                form.IsActive = false;

                _uniteOfwork.Commit();
            }
        }
        private void DeActiveBiddingForms(int requesttype,int userType)
        {
            var forms = _uniteOfwork.Query<Form>().ToList()
                                    .Where(x => x.IsActive.Value
                                           && x.FormType==2
                                           && x.RequestType == requesttype
                                           && x.UserType == userType);
            foreach (var form in forms)
            {
                if (form == null)
                {
                    throw new NotFoundException("form not found");
                }

                form.IsActive = false;

                _uniteOfwork.Commit();
            }
        }

        public FormViewModel GetActiveBiddingForm(int userType,int requestType)
        {
            var form = GetQuery().Include("Fields")
                                 .Include("Fields.FiledOptions").Where(a => a.UserType == userType
                                            && a.FormType==2
                                            && a.IsActive.Value
                                            && a.RequestType==requestType)
                                            .OrderByDescending(x => x.CreateDate)
                                            .FirstOrDefault();
            if (form == null)
            {
                return null;
            }

            //var biddingForm = GetQuery().Include("Fields")
            //                                   .Include("Fields.FiledOptions")
            //                        .Where(x => x.RequestType == form.RequestType
            //                        && x.IsActive.Value && x.FormType == 2)
            //                          .OrderByDescending(x => x.CreateDate)
            //                          .FirstOrDefault();


            return form.ToModel(_mapper);
        }

        private IQueryable<Form> GetQuery()
        {
            return _uniteOfwork.Query<Form>();
        }

        public FormViewModel GetById(int formId)
        {
            var form = _uniteOfwork.Query<Form>().Include("Fields")
                                               .Include("Fields.FiledOptions").ToList()
                                    .Where(x => x.Id == formId)
                                      .FirstOrDefault();


            return form.ToModel(_mapper);
        }
    }

}
