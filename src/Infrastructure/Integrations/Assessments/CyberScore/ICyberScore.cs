using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Interfaces;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Models;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Configurations;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.CyberScore
{
    public class CyberScoreService : ICyberScoreAssessment
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<CyberScoreService> logger;
        private readonly IHttpContextAccessor context;
        private readonly CyberScoreConfig mochaConfig;

        public CyberScoreService(ILogger<CyberScoreService> logger, IHttpClientFactory factory, IHttpContextAccessor context, CyberScoreConfig mochaConfig)
        {
            this.logger = logger;
            this.context = context;
            this.mochaConfig = mochaConfig;
            httpClient = factory.CreateClient(nameof(CyberScoreService));
            httpClient.DefaultRequestHeaders.Add("api_key", mochaConfig.Key);
        }

        public async Task<AssessmentInvitationModel> GenerateInvitation(string assessmentId, string candidateEmail, string candidateName, string candidateOrganization = null)
        {
            try
            {
                var nameSplitted = candidateName.Split(" ");
                HttpResponseMessage response = await httpClient.GetAsync($"launch?labid={assessmentId}&userid={candidateEmail}&firstname={nameSplitted.First()}&lastname={nameSplitted.Last()}&email={candidateEmail}");
                string responseContent = await response.Content.ReadAsStringAsync();
                string unescapedString = UnescapedString(responseContent);
                
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error generating invitation: {response.StatusCode} - {unescapedString}");

                InvitationResponseModel invitationResponse = JsonSerializer.Deserialize<InvitationResponseModel>(unescapedString);
                if (invitationResponse.Status != 1)
                    throw new HttpRequestException($"Error generating invitation: {invitationResponse.Result} - {unescapedString}");
                
                return string.IsNullOrEmpty(invitationResponse.LabInstanceId.ToString()) == false
                    ? new AssessmentInvitationModel(invitationResponse.LabInstanceId.ToString(), invitationResponse.Url)
                    : throw new HttpRequestException($"Error generating invitation: {invitationResponse.Result} - {unescapedString}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AssessmentId: {assessmentId}");
                throw new HttpRequestException($"Error generating invitation");
            }
        }

        public Task<AssessmentChangesModel> ParseAssessmentChangesCallBack(string requestPayload)
        {
            InvitationCallBackModel invitationCallBack = JsonSerializer.Deserialize<InvitationCallBackModel>(requestPayload);
            return Task.FromResult(new AssessmentChangesModel(invitationCallBack.GetStatus(), invitationCallBack.Id.ToString()));
        }

        public async Task<AssessmentResultModel> GetAssessmentResult(string testInvitationId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"details?labinstanceid={testInvitationId}");
            string responseContent = await response.Content.ReadAsStringAsync();
            string unescapedString = UnescapedString(responseContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error get results: {response.StatusCode} - {unescapedString}");

            TestReportModel result = JsonSerializer.Deserialize<TestReportModel>(unescapedString);
            return new AssessmentResultModel()
            {
                Score = (int)result.ExamScore,
                Sections = result.ActivityResults?.Select((section, index) => new AssessmentSectionResultModel() { Score = (int)section.GetPercentage(result.ExamMaxPossibleScore), SectionNumber = index + 1 }).ToList(),
            };
        }

        #region Private Methods

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
