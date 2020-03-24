using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Services
{
    public interface IFieldOptionService
    {
        List<FieldOption> CreateCollection(int FiledID, ICollection<FieldOptionCreateModel> model);
        IQueryable<FieldOption> GetOptionsByParentId(int parentId);
        void AddDropDownListOption(ICollection<FormFieldCreateModel> model);

        FieldOption Get(int id);
    }
}
