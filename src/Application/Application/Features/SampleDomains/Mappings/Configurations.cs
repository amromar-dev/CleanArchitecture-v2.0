using AutoMapper;
using CleanArchitectureTemplate.Domain.SampleDomains;

namespace CleanArchitectureTemplate.Application.Features.SampleDomains
{
    public class Configurations : Profile
    {
        public Configurations()
        {
            CreateMap<SampleDomain, SampleDomainOutput>();
        }
    }
}
