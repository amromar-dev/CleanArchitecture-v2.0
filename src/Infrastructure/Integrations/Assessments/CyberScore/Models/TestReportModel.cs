namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Models
{
    public class TestReportModel
    {
        public double ExamScore { get; set; }
        public double ExamMaxPossibleScore { get; set; }
        public double ScorePercentage => ExamMaxPossibleScore == 0 ? 0 : Math.Round((ExamScore / ExamMaxPossibleScore) * 100);
        public List<TestSectionReportModel> ActivityResults { get; set; }
        public int Status { get; set; }
    }
}
