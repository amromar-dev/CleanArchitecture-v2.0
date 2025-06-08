using IdentityServer4.Models;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Resources
{
    public class APIResources
    {
        public static IEnumerable<ApiResource> Resources =>
             new List<ApiResource>()
            {
                IdentityApi,
                CleanArchitectureTemplateApi
            };

        #region Defined API Resources

        private static readonly ApiResource IdentityApi = new()
        {
            Name = "Identity",
            Scopes = ["Identity", "user_info"],
        };

        private static readonly ApiResource CleanArchitectureTemplateApi = new()
        {
            Name = "CleanArchitectureTemplate",
            Scopes = ["CleanArchitectureTemplate"],
        };

        #endregion
    }
}
