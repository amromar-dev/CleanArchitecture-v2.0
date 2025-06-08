using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Models
{
    public class TestReportModel
    {
        [JsonPropertyName("testInvitationId")]
        public int TestInvitationId { get; set; }

        [JsonPropertyName("candidateEmail")]
        public string CandidateEmail { get; set; }

        [JsonPropertyName("scorePercentage")]
        public double ScorePercentage { get; set; }

        [JsonPropertyName("sections")]
        public List<TestSectionReportModel> Sections { get; set; }
    }
}
