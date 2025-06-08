using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations._base;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations
{
    public class DocumentValidator : IFileValidator
    {
        public List<string> AllowedExtensions => [".pdf", ".doc", ".docx", ".rtf", ".webp"];
        public int MaxSizeKB => 1 * 1024; // 1 MB 
    }
}
