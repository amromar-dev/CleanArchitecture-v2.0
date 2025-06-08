using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity;
using CleanArchitectureTemplate.Domain.Identity.Roles;
using CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.PersistedGrants;
using CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Resources;
using CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Utilities;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.DependencyInjections
{
    public static class IdentityServerDependencyInjection
    {
        /// <summary>
        /// Configures the Identity Server services.
        /// </summary>
        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityContext, IdentityContext>();
            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            var identity = services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
            });

            identity.AddEntityFrameworkStores<ApplicationDbContext>();
            identity.AddDefaultTokenProviders();

            var builder = services.AddIdentityServer();
            builder.AddInMemoryClients(ClientResources.Clients);
            builder.AddInMemoryApiResources(APIResources.Resources);
            builder.AddInMemoryApiScopes(APIScopes.Scopes);
            builder.AddDeveloperSigningCredential();

            services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddScoped<IProfileService, ProfileService>();

            builder.AddJwtBearerClientAuthentication();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("IdentityServer:Authority");
                options.Audience = configuration.GetValue<string>("IdentityServer:Audience");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
