using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.SharedKernels.ExportFiles.ExportCSV
{
    public interface ICSVFileUtility
    {
        Task<byte[]> Export<T>(string[] headers, List<T> data);
        Task<List<T>> Parse<T>(IFormFile formFile);
    }
}
