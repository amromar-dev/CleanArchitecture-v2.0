using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models;
using CleanArchitectureTemplate.Domain.Identity.Roles.Enums;
using System.Security.Claims;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer
{
    public class IdentityContext(IHttpContextAccessor httpContext) : IIdentityContext
    {
        public int GetUserId()
        {
            string nameIdentifier = GetClaim(ClaimTypes.NameIdentifier);

            _ = int.TryParse(nameIdentifier, out var userId);
            return userId;
        }

        public bool HasRole(string role)
        {
            List<string> roles = GetRoles();
            return roles.Any(r => r == role);
        }

        public List<string> GetRoles()
        {
            if (httpContext.HttpContext.User.Identity.IsAuthenticated == false)
                return [];

            IEnumerable<Claim> userClaims = httpContext.HttpContext.User.Claims;
            List<Claim> roleClaims = userClaims.Where(c => c.Type == ClaimTypes.Role).ToList();

            return roleClaims.Count > 0 ? roleClaims.Select(c => c.Value).ToList() : [];
        }

        public string GetUserName()
        {
            return GetClaim(ClaimTypes.Name);
        }

        public string GetFullName()
        {
            return GetClaim(ClaimTypes.GivenName);
        }

        public int? GetOrganizationId()
        {
            var claimValue = GetClaim(CustomClaims.OriganizationId);
            return int.TryParse(claimValue, out var organizationId) ? organizationId : null;
        }

        public int? GetCandidateId()
        {
            var claimValue = GetClaim(CustomClaims.CandidateId);
            return int.TryParse(claimValue, out var organizationId) ? organizationId : null;
        }

        public bool SystemAdmin()
        {
            return HasRole(SystemRole.SystemAdmin);
        }

        public bool Candidate()
        {
            return GetCandidateId().HasValue;
        }

        #region Private Methods

        private string GetClaim(string claimName)
        {
            return httpContext.HttpContext.User.Identity.IsAuthenticated == false ? null : (httpContext.HttpContext.User.FindFirst(claimName)?.Value);
        }

        #endregion
    }
}
