using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML.Attributes;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML.Models
{
    public class SampleReportModel
    {
        public string Name { get; set; }

        [ImageURL]
        public string Logo { get; set; }
    }
}
