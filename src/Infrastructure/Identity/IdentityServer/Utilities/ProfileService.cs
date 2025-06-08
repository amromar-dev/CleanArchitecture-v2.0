using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Utilities
{
    public sealed class ProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory) : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            var userClaims = await userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            var roles = await userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
                claims.Add(new Claim(JwtClaimTypes.Role, roleName));

            foreach (var clientClaim in context.Client.Claims)
                claims.Add(new Claim(clientClaim.Type, clientClaim.Value));

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FullName));
            claims.Add(new Claim(CustomClaims.UserType, ((int)user.GetUserType()).ToString()));
            
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            context.IsActive = user?.IsActive() == true;
        }
    }
}
