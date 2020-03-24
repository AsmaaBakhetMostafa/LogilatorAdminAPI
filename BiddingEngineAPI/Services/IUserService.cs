using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Services
{
    public interface IUserService
    {
        IQueryable<User> Get();
        User Get(int id);
        List<UserForAdminModel> GetNewUserRequestsByUserType(int userType);
        Task<User> Create(CreateUserModel model);

        Task<User> AcceptUser(int userId);
        Task<User> RejectUser(int userId);

        User Authenticate(AuthenticateModel model);
        List<UserChatViewModel> GetServiceProviderUser(int CurrentUserID);
    }
}
