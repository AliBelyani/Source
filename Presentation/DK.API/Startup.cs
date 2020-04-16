using DK.Data.EF.Context;
using DK.Data.EF.Repository;
using DK.Service.Interface.Repository;
using DK.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DK.Domain.Mapper.Ioc;
using DK.Service.Ioc;
using DK.API.Ioc;
using DK.Domain.DTO.Others;
using Microsoft.AspNetCore.Http;
using DK.API.Middlewares.ExceptionHandler;
using DK.API.Shared;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BMS
{
    public class Startup
    {
        private readonly SiteSettings siteSettings;
        private readonly IdentityServerSettings identityServerSettings;
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        private IHostingEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            identityServerSettings = configuration.GetSection(nameof(IdentityServerSettings)).Get<IdentityServerSettings>();
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
            services.Configure<IdentityServerSettings>(Configuration.GetSection(nameof(IdentityServerSettings)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Adds All configuration dynamicaly
            services.AddMapperConfigurations();

            //Adds all services derived from BaseService
            services.AddServices();

            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CleanArchitectureConnection"))
            );

            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddResponseCaching();

            //Identity server for handling Token and refresh tokens
            services.AddCustomizedIdentityServer4(identityServerSettings, Environment);

            //Api Authentication and authorization configurations
            services.AddApiAuthorization(identityServerSettings);

            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins , builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .WithExposedHeaders("Content-Disposition");
            }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
    }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IdentityModelEventSource.ShowPII = true;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(MyAllowSpecificOrigins);
            //app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });

            app.UseResponseCaching();
            app.UseIdentityServer();
            app.UseHttpContext();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}
