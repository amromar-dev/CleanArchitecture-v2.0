using Mersvo.Services.Identity.Infrastructure.Persistence.DomainConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureTemplate.Domain.Identity.Roles;
using System.Reflection;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public class ApplicationDbContext(DbContextOptions options) :
            IdentityDbContext<User, Role, int, UserClaim, UserRole, IdentityUserLogin<int>, 
            IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // Apply Identity Configuration After Default ones
            modelBuilder.ApplyIdentityConfigurations();
        }
    }
}
