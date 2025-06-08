using CleanArchitectureTemplate.Domain.SampleDomains.Enums;
namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class SampleDomainOutput
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public SampleDomainStatus Status { get;  set; }
        public string Description { get;  set; }
    }
}
