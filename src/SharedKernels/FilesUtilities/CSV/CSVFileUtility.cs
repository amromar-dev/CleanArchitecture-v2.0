using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;
using System.Globalization;
using System.Text;

namespace CleanArchitectureTemplate.SharedKernels.ExportFiles.ExportCSV
{
    public class CSVFileUtility : ICSVFileUtility
    {
        public Task<byte[]> Export<T>(string[] headers, List<T> data)
        {
            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine(string.Join(",", headers));

            foreach (var item in data)
            {
                var row = string.Join(",", item.GetType().GetProperties()
            .Select(p => {
                var value = p.GetValue(item)?.ToString() ?? string.Empty;
                return value.Contains(",") ? $"\"{value}\"" : value;
            }));
                csvBuilder.AppendLine(row);
            }

            return Task.FromResult(Encoding.UTF8.GetBytes(csvBuilder.ToString()));
        }

        public async Task<List<T>> Parse<T>(IFormFile formFile)
        {
            byte[] fileBytes = await ConvertToByteArray(formFile);
            
            using var memoryStream = new MemoryStream(fileBytes);
            using var reader = new StreamReader(memoryStream);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = (context) =>
                {
                    throw new Exception($"Missing field in row {context.Context.Parser.Row}. Raw data: {context.Context.Parser.RawRecord}");
                },
                BadDataFound = (context) =>
                {
                    throw new Exception($"Bad data detected in row {context.Context.Parser.Row}: {context.Context.Parser.RawRecord}");
                }
            };

            using var csv = new CsvReader(reader, config);

            var records = new List<T>();

            var fileRecords = csv.GetRecords<T>();
            foreach (var record in fileRecords)
            {
                records.Add(record);
            }

            return await Task.FromResult(records);
        }

        #region Private Methods
        
        private static async Task<byte[]> ConvertToByteArray(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                throw new BusinessRuleException(Localization.FileEmpty);

            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            return fileBytes == null || fileBytes.Length == 0 ? throw new BusinessRuleException(Localization.FileEmpty) : fileBytes;
        }

        #endregion
    }
}
