using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiddingEngineAPI.ApiModels.Users;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Enum;

namespace BiddingEngineAPI.Mapping
{
    public class UserMap : IAutoMapperTypeConfigurator
    {
        public void Configure(IMapperConfigurationExpression configuration)
        {
            //var map = configuration.CreateMap<Engineer, UserForAdminModel>();
            //map.ForMember(x => x.Name, x => x.MapFrom(u => u.Name ))
            //    .ForMember(x => x.UserType_Id, x => x.MapFrom(u => u.User.UserType_Id))
            //    .ForMember(x => x.Mobile_Number, x => x.MapFrom(u => u.EngineerContacts.Where(a=>a.Is_Main && a.Engineer_Id == u.Engineer_Id).FirstOrDefault().Contact))
            //    .ForMember(x => x.Req_Date, x => x.MapFrom(u => u.Reg_Date.Date))
            //    .ForMember(x => x.User_Id, x => x.MapFrom(u => u.User_Id));

            //var mapLawyer = configuration.CreateMap<Lawyer, UserForAdminModel>();
            //mapLawyer.ForMember(x => x.Name, x => x.MapFrom(u => u.Name))
            //    .ForMember(x => x.UserType_Id, x => x.MapFrom(u => u.User.UserType_Id))
            //    .ForMember(x => x.Mobile_Number, x => x.MapFrom(u => u.LawyerContacts.Where(a => a.Is_Main && a.Lawyer_Id == u.Lawyer_Id).FirstOrDefault().Contact))
            //    .ForMember(x => x.Req_Date, x => x.MapFrom(u => u.Reg_Date.Date))
            //    .ForMember(x => x.User_Id, x => x.MapFrom(u => u.User_Id));
            var map = configuration.CreateMap<User, UserModel>();
            map.ForMember(x => x.UserType_Id, x => x.MapFrom(u => u.UserType_Id))
                .ForMember(x => x.Id, x => x.MapFrom(u => u.User_Id))
                .ForMember(x => x.UserType_Name, x => x.MapFrom(u => u.UserType.UserType_Name))
                .ForMember(a => a.Role_Id, a => a.MapFrom(u => u.RoleID))
                .ForMember(a => a.Is_SProvider, a => a.MapFrom(u => u.Is_SProvider));



            var map2 = configuration.CreateMap<User, UserWithToken>();
            map2.ForMember(x => x.Token, x => x.MapFrom(u => u.Token))
                .ForMember(x => x.UserType_Id, x => x.MapFrom(u => u.UserType_Id))
                .ForMember(x => x.Id, x => x.MapFrom(u => u.User_Id))
                .ForMember(x => x.UserType_Name, x => x.MapFrom(u => u.UserType.UserType_Name))
                .ForMember(a => a.Role_Id, a => a.MapFrom(u => u.RoleID));

        }
    }
}
