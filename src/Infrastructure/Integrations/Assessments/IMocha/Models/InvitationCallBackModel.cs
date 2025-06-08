using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Models;
using System.Text.Json.Serialization;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Models
{
    public class InvitationCallBackModel
    {
        [JsonPropertyName("CandidateEmailId")]
        public string CandidateEmailId { get; set; }

        [JsonPropertyName("TestInvitationId")]
        public int TestInvitationId { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        public AssessmentChangesStatus GetStatus()
        {
            switch (Status)
            {
                case "In Progress":
                    return AssessmentChangesStatus.InProgress;

                case "Complete":
                    return AssessmentChangesStatus.Complete;

                case "Terminated":
                    return AssessmentChangesStatus.Terminated;

                case "Test Left":
                    return AssessmentChangesStatus.TestLeft;

                default: throw new NotImplementedException();
            }
        } 
    }
}
