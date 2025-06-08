namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Models
{
    public class InvitationResponseModel
    {
        public string Url { get; set; }

        public int Status { get; set; }

        public int Result { get; set; }

        public int LabInstanceId { get; set; }
    }
}
