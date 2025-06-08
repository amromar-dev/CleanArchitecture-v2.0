using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Domain.Identity.Roles;
using CleanArchitectureTemplate.Domain.Identity.Roles.Enums;
using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;
using System.Reflection;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.DependencyInjections.Extensions
{
    public static class DbContextExtension
    {
        /// <summary>
        /// Configure Entity Framework DbContext with SQL Server using the provided connection string
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        internal static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("DbConnectionString");
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
        }

        /// <summary>
        /// Initializes the database by applying any pending migrations to the database.
        /// This method ensures that the database schema is up-to-date with the current model.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> used to configure the application pipeline.</param>
        internal static void MigrateDatabase(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        /// <summary>
        /// Initializes the database by applying any pending migrations to the database.
        /// This method ensures that the database schema is up-to-date with the current model.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> used to configure the application pipeline.</param>
        internal static void SeedsDatabase(this IApplicationBuilder app)
        {
            app.SeedsRoles();
            app.SeedsUsers();
        }

        #region Private Methods

        private static void SeedsUsers(this IApplicationBuilder app)
        {
            try
            {
                using IServiceScope scope = app.ApplicationServices.CreateScope();
                using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                string username = "superadmin@org.com";

                var superAdminExist = userManager.FindByNameAsync(username).Result;
                if (superAdminExist != null)
                    return;

                var superAdmin = new User(username, "Super Admin", string.Empty, null, 0);

                var identityUserResult = userManager.CreateAsync(superAdmin, "P@ssw0rd").Result;
                if (identityUserResult.Succeeded == false)
                    throw new BusinessRuleException(Localization.ErrorRegisteringUser);

                userManager.AddToRolesAsync(superAdmin, new List<string>() { SystemRole.SystemAdmin }).Wait();
            }
            catch (Exception)
            {

            }
        }

        private static void SeedsRoles(this IApplicationBuilder app)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                var type = typeof(SystemRole);
                var rolesProperties = type.GetFields(BindingFlags.Public | BindingFlags.Static).ToList();

                foreach (var property in rolesProperties)
                {
                    var role = property.GetValue(type)?.ToString();
                    if (role == null)
                        continue;

                    var roleExist = roleManager.RoleExistsAsync(role).Result;
                    if (roleExist)
                        continue;

                    roleManager.CreateAsync(new Role() { Name = role, ConcurrencyStamp = Guid.NewGuid().ToString() }).Wait();
                }
            }
            catch (Exception)
            {

            }
        } 
        #endregion
    }
}
