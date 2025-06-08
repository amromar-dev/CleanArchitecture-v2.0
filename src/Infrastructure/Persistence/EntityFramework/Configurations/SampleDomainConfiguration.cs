using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Configurations.Base;
using CleanArchitectureTemplate.Domain.SampleDomains;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EntityFramework.Configurations
{
    public class SampleDomainConfiguration : BaseEntityConfiguration<SampleDomain, int>
    {
        public override void Configure(EntityTypeBuilder<SampleDomain> builder)
        {
            base.Configure(builder);

            builder.ToTable("SampleDomains");
            builder.Property(s => s.Name).IsRequired().HasMaxLength(Constants.MediumStringLength);
            builder.Property(s => s.Description).HasMaxLength(Constants.MaxStringLength);
        }
    }
}
