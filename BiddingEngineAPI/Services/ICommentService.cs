using TasalohWebAPI.ApiModels.Requests;
using TasalohWebAPI.ApiModels.Users;
using TasalohWebAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasalohWebAPI.Services
{
    public interface ICommentService
    {
        IQueryable<RequestComment> Get(int id);
        Task<RequestComment> Create(CreateRequestCommentsModel model);
        IQueryable<RequestComment> GetByServiceProviderID(int ServiceProviderID);
        IQueryable<RequestComment> GetByRequestID(int requestID);
    }
}
