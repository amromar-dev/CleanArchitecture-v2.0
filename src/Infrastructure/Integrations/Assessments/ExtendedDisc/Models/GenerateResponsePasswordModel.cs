using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Models
{
    public class GenerateResponsePasswordModel
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
