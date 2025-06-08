using CleanArchitectureTemplate.Infrastructure.FileStorage.FileLocalStorage.Configurations;
using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Enums;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Interfaces;
using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations._base;

namespace CleanArchitectureTemplate.Infrastructure.FileStorage.FileLocalStorage
{
    public class FileLocalStorageService : IFileStorage
    {
        private readonly FileLocalStorageConfig config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public FileLocalStorageService(FileLocalStorageConfig config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Asynchronously stores a file from a file in the specified file category.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the stored file's identifier or path.</returns>
        public async Task<string> StoreAsync(IFormFile file, FileCategory fileCategory, IFileValidator validator, string fileName = default)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(validator);

            GuardAgainstInvalidFile(file, validator);

            var storeFileName = fileName;
            if (string.IsNullOrEmpty(storeFileName))
                storeFileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";

            var storeFolder = GetOrCreateFolder(fileCategory);
            var storeFilePath = Path.Combine(storeFolder, storeFileName);

            using var fileStream = new FileStream(storeFilePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return $"{fileCategory}/{storeFileName}";
        }

        /// <summary>
        /// Asynchronously deletes a file from the specified file category.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task DeleteAsync(string fileName)
        {
            ArgumentException.ThrowIfNullOrEmpty(fileName);

            var storeFolder = config.StoragePath;
            var storeFile = Path.Combine(storeFolder, fileName);

            if (File.Exists(storeFile))
                File.Delete(storeFile);

            return Task.CompletedTask;
        }

        public string GetBaseURL()
        {
            return config.StoragePath;
        } 

        #region Private Methods

        private string GetOrCreateFolder(FileCategory fileCategory)
        {
            var folderPath = Path.Combine(config.StoragePath, fileCategory.ToString());

            try
            {
                Directory.CreateDirectory(folderPath);
                return folderPath;
            }
            catch
            {
                throw new BadRequestException(Localization.ErrorCreatingFolder);
            }
        }

        private static void GuardAgainstInvalidFile(IFormFile file, IFileValidator fileValidator)
        {
            var extension = Path.GetExtension(file.FileName);
            var allowedExtension = fileValidator.AllowedExtensions.Any(ex => ex.Equals(extension, StringComparison.InvariantCultureIgnoreCase));
            if (allowedExtension == false)
                throw new FieldValidationException($"{file.FileName}", $"{Localization.NoAllowedExtension} {string.Join(',', allowedExtension)}");

            var allowedSize = file.Length <= fileValidator.MaxSizeKB * 1024;
            if (allowedSize == false)
                throw new FieldValidationException($"{file.FileName}", $"{Localization.ExceedFileSize} {fileValidator.MaxSizeKB} KB");
        }
        #endregion
    }
}
