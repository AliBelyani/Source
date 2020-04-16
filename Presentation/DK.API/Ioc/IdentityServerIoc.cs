using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using DK.Domain.DTO.Others;
using DK.Domain.Entity.Security;
using DK.Data.EF.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using DK.Service.Service.Security;

namespace DK.API.Ioc
{
    public static class IdentityServerIoc
    {
        public static void AddCustomizedIdentityServer4(this IServiceCollection services, IdentityServerSettings config, IHostingEnvironment Environment)
        {
            IdentityServerInMemomryConfig xConfig = MapJsonToConfig(config);

            services.AddIdentityCore<User>()
               .AddEntityFrameworkStores<ApplicationDBContext>()
               .AddDefaultTokenProviders()
               .AddUserManager<UserManager<User>>()
               .AddSignInManager<ApplicationSignInManager>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;                
            }
            );

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(xConfig.IdentityResources)
                .AddInMemoryApiResources(xConfig.Apis)
                .AddInMemoryClients(xConfig.Clients)
                .AddAspNetIdentity<User>()
                .AddResourceOwnerValidator<ASPIdentityPasswordValidator<User>>();
            
            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                builder.AddDeveloperSigningCredential();
            }
        }

        private static IdentityServerInMemomryConfig MapJsonToConfig(IdentityServerSettings jsonSettings)
        {
            IdentityServerInMemomryConfig xFinalConfig = new IdentityServerInMemomryConfig();

            #region --Resources--

            if (jsonSettings.IdentityResources != null && jsonSettings.IdentityResources.Any())
            {
                xFinalConfig.IdentityResources = jsonSettings.IdentityResources.Select(s =>
                {
                    switch (s.ToLower())
                    {
                        case "openid":
                            return new IdentityResources.OpenId();
                        default:
                            return null;
                    }
                }).ToArray();
            }

            #endregion

            #region --Apis--
            if(jsonSettings.Apis != null && jsonSettings.Apis.Any())
            {
                xFinalConfig.Apis = jsonSettings.Apis.Select(s =>
                    new ApiResource(s.Name, s.DisplayName)
                    {
                        UserClaims = s.UserClaims.Select(u => u).ToList()
                    }
                ).ToList();
            }
            #endregion

            #region --Clients--

            if(jsonSettings.Clients != null && jsonSettings.Clients.Any())
            {
                xFinalConfig.Clients = jsonSettings.Clients.Select(s => new Client
                {
                    AccessTokenLifetime = s.AccessTokenLifeTime,
                    AllowedGrantTypes = GetGrantTypeFromString(s.AllowedGrantTypes),
                    ClientId = s.ClientID,
                    AlwaysIncludeUserClaimsInIdToken = s.AlwaysIncludeUserClaimsInIdToken,
                    AlwaysSendClientClaims = s.AlwaysSendClientClaims,
                    AllowedCorsOrigins = s.AllowCorsOrigins.ToList(),
                    RequireClientSecret = s.RequireClientSecret,
                    AllowedScopes = s.AllowedScopes.ToList(),
                    AllowOfflineAccess = s.AllowOfflineAccess,
                    AbsoluteRefreshTokenLifetime = 2592000

                }).ToArray();
            }
            #endregion

            return xFinalConfig;
        }

        private static ICollection<string> GetGrantTypeFromString(string GrantType)
        {
            switch(GrantType.ToLower())
            {
                case "password":
                    return  GrantTypes.ResourceOwnerPassword;
                case "implicit":
                    return GrantTypes.Implicit;
                default:
                    return null;
            }
        }

    }

    public class IdentityServerInMemomryConfig
    {
        public IdentityResource[] IdentityResources { get; set; }
        public List<ApiResource> Apis { get; set; }
        public Client[] Clients { get; set; }
    }

}
