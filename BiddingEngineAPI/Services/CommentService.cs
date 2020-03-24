using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasalohWebAPI.ApiModels.Requests;
using TasalohWebAPI.EFCore.DAL;
using TasalohWebAPI.EFCore.Model;
using TasalohWebAPI.Exceptions;
using TasalohWebAPI.Helpers;

namespace TasalohWebAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        
        public CommentService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<RequestComment> Create(CreateRequestCommentsModel model)
        {
            var requestComment = new RequestComment
            {
                Comment = model.Comment.Trim(),
                ServiceProviderID= model.ServiceProviderID,
                RequestID = model.RequestID
            };

            _uow.Add(requestComment);
            await _uow.CommitAsync();
           
            return requestComment;
        }

        private IQueryable<RequestComment> GetQuery()
        {   
            return _uow.Query<RequestComment>();
               
        }

        public IQueryable<RequestComment> Get(int id)
        {
            var request = GetQuery().Where(x => x.RequestID == id);

            if (request == null)
            {
                throw new NotFoundException("Request Comments is not found");
            }

            return request; 
        }

        public IQueryable<RequestComment> GetByRequestID(int requestID)
        {
            var request = GetQuery().Where(x => x.RequestID == requestID);

            if (request == null)
            {
                throw new NotFoundException("Request Comments is not found");
            }

            return request;
        }

        public IQueryable<RequestComment> GetByServiceProviderID(int ServiceProviderID)
        {
            var request = GetQuery().Where(x => x.ServiceProviderID == ServiceProviderID);

            if (request == null)
            {
                throw new NotFoundException("Request Comments is not found");
            }

            return request;
        }
    }
}
