using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity.Models;
using CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes;
using CleanArchitectureTemplate.Domain.Identity.Roles;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Configurations.Base;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace Mersvo.Services.Identity.Infrastructure.Persistence.DomainConfigurations
{
    public static class IdentityConfigurations
    {
        public static void ApplyIdentityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyUserConfigurations();
            modelBuilder.ApplyRoleConfigurations();
            modelBuilder.ApplyUserClaimsConfigurations();
            modelBuilder.ApplyUserRoleConfigurations();
            modelBuilder.ApplyDefaultConfigurations();
            modelBuilder.ApplyPersistedGrantStoresConfigurations();
        }

        #region Private Methods

        private static void ApplyUserConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", "Identity");
            modelBuilder.Entity<User>().HasQueryFilter(a => a.Auditing.IsDeleted == false);

            modelBuilder.Entity<User>().HasMany(x => x.UserRoles).WithOne().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().Metadata.FindNavigation(nameof(User.UserRoles)).SetPropertyAccessMode(PropertyAccessMode.Field);
            modelBuilder.Entity<User>().Navigation(x => x.UserRoles).AutoInclude(true);

            modelBuilder.Entity<User>().HasMany(x => x.UserClaims).WithOne().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().Metadata.FindNavigation(nameof(User.UserClaims)).SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<User>().OwnsOne(x => x.Auditing, cb =>
            {
                cb.Property(nameof(Auditing.CreatedAt)).HasColumnName(nameof(Auditing.CreatedAt));
                cb.Property(nameof(Auditing.CreatedBy)).HasColumnName(nameof(Auditing.CreatedBy));
                cb.Property(nameof(Auditing.ModifiedAt)).HasColumnName(nameof(Auditing.ModifiedAt));
                cb.Property(nameof(Auditing.ModifiedBy)).HasColumnName(nameof(Auditing.ModifiedBy));
                cb.Property(nameof(Auditing.DeletedAt)).HasColumnName(nameof(Auditing.DeletedAt));
                cb.Property(nameof(Auditing.DeletedBy)).HasColumnName(nameof(Auditing.DeletedBy));
                cb.Property(nameof(Auditing.IsDeleted)).HasColumnName(nameof(Auditing.IsDeleted));
            });

            modelBuilder.Entity<User>().Ignore(x => x.PhoneNumber);
            modelBuilder.Entity<User>().OwnsOne(x => x.Phone, cb =>
            {
                cb.Property(nameof(Phone.Number)).HasColumnName("PhoneNumber").HasMaxLength(Constants.RegularStringLength).HasDefaultValue(null).IsRequired(true);
                cb.Property(nameof(Phone.CountryId)).HasColumnName("PhoneCountryId").HasMaxLength(Constants.RegularStringLength).HasDefaultValue(null).IsRequired(true);
            });
        }

        private static void ApplyUserClaimsConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClaim>().ToTable("UsersClaims", "Identity");
        }

        private static void ApplyRoleConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Roles", "Identity");
        }

        private static void ApplyUserRoleConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().ToTable("UsersRoles", "Identity");
            modelBuilder.Entity<UserRole>().HasOne(x => x.Role).WithMany().HasForeignKey("RoleId").IsRequired();
            modelBuilder.Entity<UserRole>().Navigation(x => x.Role).AutoInclude(true);
        }

        private static void ApplyDefaultConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UsersLogins", "Identity");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UsersTokens", "Identity");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RolesClaims", "Identity");
        }

        private static void ApplyPersistedGrantStoresConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersistedGrant>().HasKey(x => x.Key);
            modelBuilder.Entity<PersistedGrant>().ToTable("PersistedGrantStores");

            modelBuilder.Entity<PersistedGrant>().Property(x => x.Key).HasMaxLength(200).ValueGeneratedNever();
            modelBuilder.Entity<PersistedGrant>().Property(x => x.Type).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<PersistedGrant>().Property(x => x.SubjectId).HasMaxLength(200);
            modelBuilder.Entity<PersistedGrant>().Property(x => x.SessionId).HasMaxLength(100);
            modelBuilder.Entity<PersistedGrant>().Property(x => x.ClientId).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<PersistedGrant>().Property(x => x.Description).HasMaxLength(200);
            modelBuilder.Entity<PersistedGrant>().Property(x => x.CreationTime).IsRequired();
            // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
            // apparently anything over 4K converts to nvarchar(max) on SqlServer
            modelBuilder.Entity<PersistedGrant>().Property(x => x.Data).HasMaxLength(50000).IsRequired();

            modelBuilder.Entity<PersistedGrant>().HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
            modelBuilder.Entity<PersistedGrant>().HasIndex(x => new { x.SubjectId, x.SessionId, x.Type });
            modelBuilder.Entity<PersistedGrant>().HasIndex(x => x.Expiration);
        }

        #endregion
    }
}
