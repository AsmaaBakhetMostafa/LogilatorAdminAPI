using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoWrapper;
using BiddingEngineAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http;
using BiddingEngineAPI.EFCore;
using Microsoft.EntityFrameworkCore;
using BiddingEngineAPI.Filters;
using BiddingEngineAPI.EFCore.Model;
using FluentValidation.AspNetCore;
using BiddingEngineAPI.Hubs;
//using BiddingEngineAPI.Validation;

namespace BiddingEngineAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            //services.ConfigureCors();
            services.ConfigureDbContext(Configuration);
            services.ConfigureIISIntegration();
            services.ConfigureJwt(Configuration);
            services.AddServices();
            services.ConfigureAutoMapper();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc(options => { 
                options.Filters.Add(new ApiExceptionFilter());
                options.Filters.Add(typeof(ValidateModelAttribute));

            }).AddNewtonsoftJson().AddFluentValidation(fv => {
              //  fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                //fv.RegisterValidatorsFromAssemblyContaining<EngineerValidator>();
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader());
            //});
            services.AddControllers();
            services.ConfigureSwagger();
           // services.AddSignalR();

            services.AddSignalR(config => config.EnableDetailedErrors = true);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
             builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .Build());

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");

            InitDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bidding API V1");
            });

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            
          //  app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
          //  
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { ShowStatusCode = true });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               // endpoints.MapHub<MyChatHub>("/mychathub");
            });
            app.UseSignalR(config => config.MapHub<MyChatHub>("/mychathub"));

            //app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            //app.MapWhen(x => !x.Request.Path.Value.StartsWith("/swagger"), builder =>
            //{
            //    builder.UseMvc(routes =>
            //    {
            //        routes.MapSpaFallbackRoute(
            //            "spa-fallback",
            //            new { controller = "Home", action = "Index" });
            //    });
            //});
        }

        private void InitDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                context.Database.Migrate();

                var services = serviceScope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }
    }
}
