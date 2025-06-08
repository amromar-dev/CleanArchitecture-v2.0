using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.FileStorage.FileLocalStorage.Configurations
{
    public record FileLocalStorageConfig : IConfig
    {
        public string StoragePath { get; set; }

        public string JsonFileName => "FileLocalStorage";
    }
}
