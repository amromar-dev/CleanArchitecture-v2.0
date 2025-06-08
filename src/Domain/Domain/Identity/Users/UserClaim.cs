using Microsoft.AspNetCore.Identity;

namespace CleanArchitectureTemplate.Domain.Identity.Users
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public UserClaim()
        {

        }

        public UserClaim(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}
