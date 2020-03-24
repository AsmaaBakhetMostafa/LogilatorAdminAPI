using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;
using BiddingEngineAPI.ApiModels;
using BiddingEngineAPI.EFCore.DAL;
using BiddingEngineAPI.Mapping;
using BiddingEngineAPI.Exceptions;

namespace BiddingEngineAPI.Services
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAutoMapper _mapper;

        public RequestTypeService(IUnitOfWork uow, IAutoMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        
        public IQueryable<RequestType> Get()
        {
            var query = GetQuery();

            return query;
        }

        private IQueryable<RequestType> GetQuery()
        {   
            return _uow.Query<RequestType>();
               
        }

        public IQueryable<RequestType> Get(int id)
        {
            var RequestType = GetQuery().Where(x => x.RequestType_Id == id);

            if (RequestType == null)
            {
                throw new NotFoundException("RequestType is not found");
            }

            return RequestType; 
        }

    }
}
