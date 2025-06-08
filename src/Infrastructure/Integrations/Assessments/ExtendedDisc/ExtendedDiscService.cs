using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Interfaces;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Models;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Configurations;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc.Models;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.SharedKernels.Extensions;
using Microsoft.Extensions.Logging;
using CleanArchitectureTemplate.SharedKernels.Exceptions;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.ExtendedDisc
{
    public class ExtendedDiscService : IExtendedDiscAssessment
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ExtendedDiscService> logger;
        private readonly IHttpContextAccessor context;
        private readonly ExtendedDiscConfig config;

        public ExtendedDiscService(ILogger<ExtendedDiscService> logger, IHttpClientFactory factory, IHttpContextAccessor context, ExtendedDiscConfig config)
        {
            this.logger = logger;
            this.context = context;
            this.config = config;
            httpClient = factory.CreateClient(nameof(ExtendedDiscService));
        }

        public async Task<AssessmentInvitationModel> GenerateInvitation(string assessmentId, string candidateEmail, string candidateName, string candidateOrganization)
        {
            try
            {
                if (assessmentId != config.AssessmentId)
                    throw new NotFoundException();

                await RegisterCallbackURL();

                GenerateResponsePasswordModel invitation = await GenerateInvitation() ?? throw new NotFoundException("Invitation Password");

                string candidateFirstName = candidateName.Split(" ").FirstOrDefault();
                string candidateLastName = candidateName.Split(" ").LastOrDefault();

                HttpResponseMessage response = await httpClient.GetAsync($"response_users?first_name={candidateFirstName}&last_name={candidateLastName}&organization={candidateOrganization}&email={candidateEmail}&password={invitation.Value}");

                return response.IsSuccessStatusCode
                    ? new AssessmentInvitationModel(invitation.Value, invitation.Link)
                    : throw new HttpRequestException($"Error assign assessment: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AssessmentId: {assessmentId}");
                throw new HttpRequestException($"Error generating invitation");
            }
        }

        public Task<AssessmentChangesModel> ParseAssessmentChangesCallBack(string requestPayload)
        {
            return Task.FromResult(new AssessmentChangesModel(AssessmentChangesStatus.Complete, requestPayload));
        }

        public async Task<AssessmentResultModel> GetAssessmentResult(string testInvitationId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"report?password={testInvitationId}");
            string responseContent = await response.Content.ReadAsStringAsync();
            
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error get results: {response.StatusCode} - {responseContent}");

            TestReportModel result = JsonSerializer.Deserialize<TestReportModel>(responseContent);

            return new AssessmentResultModel()
            {
                Score = result.Score,
                Sections = result.Competencies?.Select((section, index) => new AssessmentSectionResultModel() { Score = section.Value, SectionNumber = index + 1 }).ToList(),
            };
        }

        #region Private Methods

        private async Task RegisterCallbackURL()
        {
            var request = new SettingsModel()
            {
                Value = $"{context.GetBaseUrl()}//{config.CallBackAPI}"
            };

            StringContent jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync($"settings", jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error generating invitation (settings): {response.StatusCode}");
        }

        private async Task<GenerateResponsePasswordModel> GenerateInvitation()
        {
            var response = await httpClient.GetAsync($"generate");
            string responseContent = await response.Content.ReadAsStringAsync();

            GenerateResponseModel generatedResponse1 = JsonSerializer.Deserialize<GenerateResponseModel>(responseContent);
            return generatedResponse1.Passwords.FirstOrDefault();
        }

        private static string UnescapedString(string content)
        {
            try
            {
                return JsonSerializer.Deserialize<string>(content);
            }
            catch (Exception)
            {
                return content;
            }
        }

        #endregion
    }
}
