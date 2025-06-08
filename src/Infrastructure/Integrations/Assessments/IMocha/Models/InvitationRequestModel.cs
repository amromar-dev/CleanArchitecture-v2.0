using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Models
{
    public class InvitationRequestModel
    {
        public string Key { get; set; }

        [JsonPropertyName("testId")]
        public string TestId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("callbackurl")]
        public string Callbackurl { get; set; }
    }
}
