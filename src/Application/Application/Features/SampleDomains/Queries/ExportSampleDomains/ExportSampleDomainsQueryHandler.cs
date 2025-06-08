using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Domain.SampleDomains.Enums;
using CleanArchitectureTemplate.SharedKernels.ExportFiles.ExportCSV;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class ExportSampleDomainsQueryHandler(ICSVFileUtility csvFileUtility) : QueryHandler<ExportSampleDomainsQuery, RequestFile>
    {
        public override async Task<IRequestResult<RequestFile>> Handle(ExportSampleDomainsQuery request, CancellationToken cancellationToken)
        {
            string[] headers = ["Name", "Description", "Status"];
            List<SampleDomainOutput> data = GenerateDataToExport();

            var fileBytes = await csvFileUtility.Export(headers, data);

            return ResultFile(fileBytes, RequestFileTypes.Csv, "Sample Domain");
        }

        #region Private Methods

        private static List<SampleDomainOutput> GenerateDataToExport()
        {
            return
            [
                new()
                {
                    Name = "Sample 1",
                    Description = "Description 1",
                    Status = SampleDomainStatus.Active,
                },
                new()
                {
                    Name = "Sample 2",
                    Description = "Description 2",
                    Status = SampleDomainStatus.Active,
                },
                new()
                {
                    Name = "Sample 2",
                    Description = "Description 2",
                    Status = SampleDomainStatus.InActive,
                },
                new()
                {
                    Name = "Sample 3",
                    Description = "Description 3",
                    Status = SampleDomainStatus.InActive,
                },
            ];
        }

        #endregion
    }
}
