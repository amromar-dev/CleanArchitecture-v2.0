using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Configurations
{
    public record CyberScoreConfig : IConfig
    {
        public string BaseAddress { get; set; }
        
        public string Key { get; set; }
        
        public string CallBackAPI { get; set; }

        public string JsonFileName => "CyberScore";
    }
}
