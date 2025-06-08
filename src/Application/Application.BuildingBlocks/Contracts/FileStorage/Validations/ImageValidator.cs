using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations._base;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations
{
    public class ImageValidator : IFileValidator
    {
        public List<string> AllowedExtensions => [".jpg", ".jpeg", ".png", ".bmp", ".webp"];
        public int MaxSizeKB => 1 * 1024; // 1 MB 
    }
}
