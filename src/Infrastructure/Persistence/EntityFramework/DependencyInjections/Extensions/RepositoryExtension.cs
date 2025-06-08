using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Domain.SampleDomains.Interfaces;
using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Repositories.Base;
using CleanArchitectureTemplate.Domain.Identity.Users.Interfaces;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.DependencyInjections.Extensions
{
    public static class RepositoryExtension
    {
        /// <summary>
        /// Extension method to configure repository services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to configure.</param>
        internal static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Generic repositories

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            // Define repositories
            services.AddScoped<ISampleDomainRepository, SampleDomainRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
