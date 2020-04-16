using DK.API.Middlewares.Authorization;
using DK.Domain.DTO.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DK.API.Ioc
{
    public static class ApiAuthorizationIoc
    {
        public static void AddApiAuthorization(this IServiceCollection services, IdentityServerSettings settings)
        {

            //map Claim type to asp identity standards.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add("name", ClaimTypes.Name);

            //for Validating tokens
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = settings.IdentityServerAuthority;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "BMSAPI";
                });

            services.AddScoped<IAuthorizationHandler, AttributeAuthorizationHandler>();

            services.AddAuthorization(option =>
                option.AddPolicy("Permission", builder =>
                    builder.AddRequirements(new PermissionAuthorizationRequirement())
                        .RequireAuthenticatedUser()
                )
            );

        }
    }
}
