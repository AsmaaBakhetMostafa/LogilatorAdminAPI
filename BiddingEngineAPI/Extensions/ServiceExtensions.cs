using BiddingEngineAPI.EFCore;
using BiddingEngineAPI.EFCore.DAL;
using BiddingEngineAPI.EFCore.Model;
using BiddingEngineAPI.Filters;
using BiddingEngineAPI.Helpers;
using BiddingEngineAPI.Mapping;
using BiddingEngineAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using AutoMapper;
//using BiddingEngineAPI.Validation;
//using BiddingEngineAPI.Services.LawyerS;
//using Microsoft.OpenApi.Models;

namespace BiddingEngineAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:sqlConnection"];

            services.AddEntityFrameworkSqlServer();

            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork>(ctx => new EFUnitOfWork(ctx.GetRequiredService<DatabaseContext>()));

            services.AddScoped<IActionTransactionHelper, ActionTransactionHelper>();
            services.AddScoped<UnitOfWorkFilterAttribute>();

        }
       
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // configure jwt authentication
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddTransient<IFieldOptionService, FieldOptionService>();
            var mapperConfig = AutoMapperConfigurator.Configure();
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(x => mapper);
            services.AddTransient<IAutoMapper, AutoMapperAdapter>();
            
        }

        public static void AddServices(this IServiceCollection services)
        {
            var exampleProcessorType = typeof(UserService);
            var types = (from t in exampleProcessorType.GetTypeInfo().Assembly.GetTypes()
                         where t.Namespace == exampleProcessorType.Namespace
                               && t.GetTypeInfo().IsClass
                               && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                         select t).ToArray();

            foreach (var type in types)
            {
                var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }

        }

        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "BiddingEngin API", Version = "v1" });
            });
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddMvc(setup => {
                //...mvc setup...
                setup.Filters.Add(typeof(ValidateModelAttribute));
            }).AddFluentValidation(fv => {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            //services.AddMvc()
            //    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EngineerValidator>());
        }

    }
}
