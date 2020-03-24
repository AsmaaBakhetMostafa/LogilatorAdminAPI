using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Services
{
    public interface IFormService
    {
        void Create(FormCreateModel model);
        void Edit(FormCreateModel model);
        //void CreateBidForm(FormCreateModel model);
        void CreateBiddingForm(FormCreateModel model);

        void EditBiddForm(FormCreateModel model);
        FormViewModel GetActiveBiddingForm(int userType, int requestType);
        FormViewModel GetActiveRequestForm(int requestType);
        FormViewModel ActivateForm(int formId);
        FormViewModel DeActivateForm(int formId);
        List<FormViewModel> GetAllForms();
        List<FormViewModel> GetAllBiddingForms( int requestType);

        //FormViewModel GetActiveBiddingForm(int formId);
        FormViewModel GetById(int formId);
        FormViewModel DeActivateBiddingForm(int formId);
        FormViewModel ActivateBiddingForm(int formId);

    }
}
