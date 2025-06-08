namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Models
{
    public class TestSectionReportModel
    {
        public int ActivityId { get; set; }

        public double Score { get; set; }

        public double GetPercentage(double examMaxPossibleScore)
        {
            return examMaxPossibleScore == 0 ? 0 : Math.Round((Score / examMaxPossibleScore) * 100);
        }
    }
}
