using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Configurations
{
    public record ExtendedDiscConfig : IConfig
    {
        public string BaseAddress { get; set; }
        
        public string AssessmentId { get; set; }
        
        public string CallBackAPI { get; set; }

        public string JsonFileName => "ExtendedDisc";
    }
}
