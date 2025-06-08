using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Models
{
    public class GenerateResponseModel
    {
        [JsonPropertyName("session_active")]
        public bool SessionActive { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("passwords")]
        public List<GenerateResponsePasswordModel> Passwords { get; set; }
    }
}
