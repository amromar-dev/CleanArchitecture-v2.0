using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Models
{
    public class TestReportModel
    {
        [JsonPropertyName("competencies")]
        public List<TestReportCompetencyModel> Competencies { get; set; } = [];

        public int Score => Competencies.Count > 0 ? Competencies.Sum(c => c.Value) / Competencies.Count : 0;
    }
}
