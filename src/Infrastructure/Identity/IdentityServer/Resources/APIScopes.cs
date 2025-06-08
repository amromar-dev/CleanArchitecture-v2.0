using IdentityServer4.Models;
using IdentityModel;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Resources
{
    public class APIScopes
    {
        public static IEnumerable<ApiScope> Scopes =>
            new List<ApiScope>()
            {
                IdentityScope,
                CleanArchitectureTemplateScope
            };

        #region Defined Scopes

        private static readonly ApiScope IdentityScope = new()
        {
            Name = "Identity",
            Description = "Access to identity apis",
            UserClaims =
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.Email,
                JwtClaimTypes.Role,
                CustomClaims.OriganizationId,
                CustomClaims.CandidateId,
            },
        };        

        private static readonly ApiScope CleanArchitectureTemplateScope = new()
        {
            Name = "CleanArchitectureTemplate",
            Description = "Access to cleanarchitecturetemplate apis"
        };

        #endregion
    }
}
