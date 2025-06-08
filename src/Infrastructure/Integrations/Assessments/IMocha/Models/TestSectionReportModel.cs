using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Models
{
    public class TestSectionReportModel
    {
        [JsonPropertyName("sectionScore")]
        public double SectionScore { get; set; }
        
        [JsonPropertyName("candidateScore")]
        public double CandidateScore { get; set; }
       
        [JsonPropertyName("sectionID")]
        public int SectionID { get; set; }
        public double ScorePercentage => SectionScore == 0 ? 0 : Math.Round((CandidateScore / SectionScore) * 100);
    }
}
