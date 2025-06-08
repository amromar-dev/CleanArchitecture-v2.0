using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;
using CleanArchitectureTemplate.SharedKernels.Localizations;

namespace CleanArchitectureTemplate.Domain.SampleDomains.Rules
{
    public record SampleDomainNameRequiredRule(string Name) : IBusinessRule
    {
        public string Message => Localization.NameIsRequired;

        public bool IsBroken() => string.IsNullOrEmpty(Name);
    }
}
