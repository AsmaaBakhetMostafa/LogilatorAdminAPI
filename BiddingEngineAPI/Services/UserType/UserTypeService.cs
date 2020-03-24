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
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAutoMapper _mapper;

        public UserTypeService(IUnitOfWork uow, IAutoMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        
        public IQueryable<UserType> Get()
        {
            var query = GetQuery();

            return query;
        }

        private IQueryable<UserType> GetQuery()
        {   
            return _uow.Query<UserType>();
               
        }

        public IQueryable<UserType> Get(int id)
        {
            var UserType = GetQuery().Where(x => x.UserType_Id == id);

            if (UserType == null)
            {
                throw new NotFoundException("UserType is not found");
            }

            return UserType; 
        }

    }
}
