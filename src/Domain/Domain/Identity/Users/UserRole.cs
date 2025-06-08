using Microsoft.AspNetCore.Identity;
using CleanArchitectureTemplate.Domain.Identity.Roles;

namespace CleanArchitectureTemplate.Domain.Identity.Users
{
    public class UserRole : IdentityUserRole<int>
    {
        public Role Role { get; private set; }
    }
}
