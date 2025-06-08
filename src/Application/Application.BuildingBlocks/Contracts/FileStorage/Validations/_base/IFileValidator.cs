namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Validations._base
{
    public interface IFileValidator
    {
        List<string> AllowedExtensions { get; }

        public int MaxSizeKB { get; }
    }
}
