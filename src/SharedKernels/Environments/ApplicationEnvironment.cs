namespace CleanArchitectureTemplate.SharedKernels.Environments
{
    public static class ApplicationEnvironment
    {
        public static ApplicationEnvironmentType CurrentEnvironment { get; set; }

        public static ApplicationEnvironmentClient CurrentClient { get; set; }
    }
}
