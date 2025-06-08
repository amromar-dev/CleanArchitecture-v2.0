using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Models
{
    public class InvitationResponseModel
    {
        [JsonPropertyName("testInvitationId")]
        public int TestInvitationId { get; set; }

        [JsonPropertyName("testUrl")]
        public string TestUrl { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
