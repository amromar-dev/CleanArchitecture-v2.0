using CleanArchitectureTemplate.Infrastructure.FileStorage.FileLocalStorage.Configurations;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Interfaces;
using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.FileStorage.FileLocalStorage.DependencyInjections
{
    public static class FileLocalStorageDependencyInjection
    {
        /// <summary>
        /// Registers the file storage services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureFileLocalStorage(this IServiceCollection services)
        {
            services.AddSingleton<IFileStorage, FileLocalStorageService>();
            services.Configure<FileLocalStorageConfig>();
        }
    }
}
