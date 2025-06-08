using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Models;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Models
{
    public class InvitationCallBackModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompletionStatus { get; set; }
        public double? ExamScore { get; set; }

        public AssessmentChangesStatus? GetStatus()
        {
            return CompletionStatus <= 2 ? null : ExamScore.HasValue ? AssessmentChangesStatus.Complete : AssessmentChangesStatus.InProgress;
        }
    }
}
