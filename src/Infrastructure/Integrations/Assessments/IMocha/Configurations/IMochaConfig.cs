using CleanArchitectureTemplate.SharedKernels.DependencyInjections;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Configurations
{
    public record IMochaConfig : IConfig
    {
        public string BaseAddress { get; set; }
        
        public string Key { get; set; }
        
        public string CallBackAPI { get; set; }

        public string JsonFileName => "IMocha";
    }
}
