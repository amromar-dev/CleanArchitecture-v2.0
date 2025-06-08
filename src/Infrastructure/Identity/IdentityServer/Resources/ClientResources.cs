using IdentityServer4.Models;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Resources
{
    public static class ClientResources
    {
        public static IEnumerable<Client> Clients => new List<Client>
        {
            CleanArchitectureTemplatePortalClient
        };

        #region Defined Clients        

        private static readonly Client CleanArchitectureTemplatePortalClient = new()
        {
            ClientId = "cleanarchitecturetemplate_portal",
            ClientName = "CleanArchitectureTemplate",
            AllowOfflineAccess = true,
            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
            UpdateAccessTokenClaimsOnRefresh = true,
            RefreshTokenUsage = TokenUsage.OneTimeOnly,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            //AccessTokenLifetime = 60 * 60 * 24, // 1 day, 
            AccessTokenLifetime = 60 * 15, // 15 min,
            SlidingRefreshTokenLifetime = 60 * 60 * 24 * 15, // 15 days
            AbsoluteRefreshTokenLifetime = 60 * 60 * 24 * 30, // 30 days
            AllowAccessTokensViaBrowser = true,
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowedScopes = { "Identity", "CleanArchitectureTemplate" },
            RequireClientSecret = false,
            AlwaysSendClientClaims = true,
        };

        #endregion
    }
}
