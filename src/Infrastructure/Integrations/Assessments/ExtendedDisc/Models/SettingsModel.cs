using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Models
{
    public class SettingsModel
    {
        [JsonPropertyName("key")]
        public string Key { get; } = "sga_elguards_report_url";

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
