using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingEngineAPI.EFCore.Model
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>()))
            {
                if (!context.UserTypes.Any())
                {
                    context.UserTypes.AddRange(
                        new UserType
                        {
                            UserType_Id = 1,
                            UserType_Name = "Admin"
                        },

                        new UserType
                        {
                            UserType_Id = 2,
                            UserType_Name = "Shipper"
                        },

                        new UserType
                        {
                            UserType_Id = 3,
                            UserType_Name = "ServiceProvider"
                        }
                    );
                }

                if (!context.RequestTypes.Any())
                {
                    context.RequestTypes.AddRange(
                        new RequestType
                        {
                            RequestType_Id = 1,
                            RequestType_Name = "طلب شحن"
                        },

                        new RequestType
                        {
                            RequestType_Id = 2,
                            RequestType_Name = "طلب مخالصة جمارك"
                        }
                    );
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            IsActive = true,
                            Is_Deleted = false,
                            Is_Online = false,
                            Password = "$2a$11$LjGq/9.T63JTV67EZOsEpu3adj1ZPNM9dfX06oTo/BcBpyA8Ryyk.",
                            RoleID = 1,
                            UserName = "admin",
                            UserType_Id = 1
                        },

                        new User
                        {
                            IsActive = true,
                            Is_Deleted = false,
                            Is_Online = false,
                            Password = "$2a$11$LjGq/9.T63JTV67EZOsEpu3adj1ZPNM9dfX06oTo/BcBpyA8Ryyk.",
                            RoleID = 2,
                            UserName = "shipper",
                            UserType_Id = 2
                        },
                        new User
                        {
                            IsActive = true,
                            Is_Deleted = false,
                            Is_Online = false,
                            Password = "$2a$11$LjGq/9.T63JTV67EZOsEpu3adj1ZPNM9dfX06oTo/BcBpyA8Ryyk.",
                            RoleID = 3,
                            UserName = "sp",
                            UserType_Id = 3
                        }
                    );
                }
                
                context.SaveChanges();

            }
        }
    }
}
