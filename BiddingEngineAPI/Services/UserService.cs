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
//using BiddingEngineAPI.Services.LawyerS;

namespace BiddingEngineAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        
        private readonly AppSettings _appSettings;

        private readonly IAutoMapper _mapper;
        private readonly IChatMessageService _chatMessageService;

        public UserService(IUnitOfWork uow,  IOptions<AppSettings> appSettings, IAutoMapper mapper,
            IChatMessageService chatMessageService)
        {
            _uow = uow;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _chatMessageService = chatMessageService;
        }

        public async Task<User> Create(CreateUserModel model)
        {
            var username = model.Email.Trim();

            if (GetQuery().Any(u => u.UserName == username))
            {
                throw new BadRequestException("The username is already in use");
            }

            var user = new User
            {
                UserName = model.Email.Trim(),
                Password = model.Password.Trim().WithBCrypt(),
                RoleID = model.UserType ,
                IsActive = false,
                ActivationCode = Guid.NewGuid().ToString()

            };

            _uow.Add(user);
            await _uow.CommitAsync();
            
            return user;
        }

        public IQueryable<User> Get()
        {
            var query = GetQuery();

            return query;
        }
        public List<UserChatViewModel> GetServiceProviderUser(int CurrentUserID)
        {
            List<UserChatViewModel> LstUserChat = new List<UserChatViewModel>();
           var query = _uow.Query<User>()
                          .Where(x=>x.Is_SProvider
                          && x.User_Id!=CurrentUserID)
                            .ToList();
            foreach (var obj in query)
            { 
                UserChatViewModel UserChat = new UserChatViewModel();
                UserChat.User = obj;
                var LstMssg= _chatMessageService.Get().Where(x =>
                   x.FromUserId == obj.User_Id
                   && x.ToUserId == CurrentUserID
                   || (
                     x.FromUserId == CurrentUserID
                   && x.ToUserId == obj.User_Id
                   )
                   ).ToList();
                UserChat.ChatMessages.AddRange(LstMssg);
                LstUserChat.Add(UserChat);
            }
            return LstUserChat;
        }
        private IQueryable<User> GetQuery()
        {
            return _uow.Query<User>();
        }

        public User Get(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.User_Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }

        public async Task<User> AcceptUser(int userId)
        {
            var user = GetQuery().FirstOrDefault(x => x.User_Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.IsActive = true;
            
            await _uow.CommitAsync();
            return user;
        }

        public async Task<User> RejectUser(int userId)
        {
            var user = GetQuery().FirstOrDefault(x => x.User_Id == userId);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.IsActive = false;

            await _uow.CommitAsync();
            return user;
        }

        public User Authenticate(AuthenticateModel model)
        {
            var user = GetQuery().Include("UserType").FirstOrDefault(x => x.UserName == model.Username);

            // return null if user not found
            if (user == null)
                throw new BadRequestException("Wrong username or password");

            if (!user.IsActive.HasValue || user.IsActive.Value == false)
            {
                throw new ForbiddenException("User is not activated");
            }
            //if (user.Password!=model.Password)
            //{
            //    throw new BadRequestException("Wrong username or password");
            //}
            if (!user.Password.VerifyWithBCrypt(model.Password))
            {
                throw new BadRequestException("Wrong username or password");
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            
            return user;
        }

        public List<UserForAdminModel> GetNewUserRequestsByUserType(int userType)
        {
            var result = new List<UserForAdminModel>();
            if (userType == (int)UserTypeEnum.Engineer)
            {
         //       var query = _engineerService.Get().Where(a => a.User.IsActive == null && a.User.UserType_Id == userType);
           //     result = _mapper.Map<Engineer, UserForAdminModel>(query).ToList();
                
            }
            else if (userType == (int)UserTypeEnum.Lawyer)
            {
             //   var query = _lawyerService.Get().Where(a => a.User.IsActive == null && a.User.UserType_Id == userType);
              //  result = _mapper.Map<Lawyer, UserForAdminModel>(query).ToList();
            }

            return result;
        }
    }
}
