using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Enums;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations._base;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Interfaces
{
    public interface IFileStorage
    {
        /// <summary>
        /// Asynchronously stores a file from a file in the specified file category.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the stored file's identifier or path.</returns>
        Task<string> StoreAsync(IFormFile file, FileCategory fileCategory, IFileValidator validator, string fileName = default);

        /// <summary>
        /// Asynchronously deletes a file from the specified file category.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(string fileName);

        /// <summary>
        /// Get The Base URL
        /// </summary>
        /// <returns></returns>
        string GetBaseURL();
    }
}
