using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Interfaces;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Integrations.Assessments.Models;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Configurations;
using CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha.Models;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using CleanArchitectureTemplate.SharedKernels.Extensions;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureTemplate.Infrastructure.Integrations.Assessments.IMocha
{
    public class IMochaService : IIMochaAssessment
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<IMochaService> logger;
        private readonly IHttpContextAccessor context;
        private readonly IMochaConfig mochaConfig;

        public IMochaService(ILogger<IMochaService> logger, IHttpClientFactory factory, IHttpContextAccessor context, IMochaConfig mochaConfig)
        {
            this.logger = logger;
            this.context = context;
            this.mochaConfig = mochaConfig;
            httpClient = factory.CreateClient(nameof(IMochaService));
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", mochaConfig.Key);
        }

        public async Task<AssessmentInvitationModel> GenerateInvitation(string assessmentId, string candidateEmail, string candidateName, string candidateOrganization = null)
        {
            try
            {
                var request = new InvitationRequestModel()
                {
                    Email = candidateEmail,
                    Name = candidateName,
                    TestId = assessmentId,
                    Key = mochaConfig.Key,
                    Callbackurl = $"{context.GetBaseUrl()}//{mochaConfig.CallBackAPI}"
                };

                StringContent jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"tests/{assessmentId}/invite", jsonContent);
                string responseContent = await response.Content.ReadAsStringAsync();
                string unescapedString = UnescapedString(responseContent);

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error generating invitation: {response.StatusCode} - {unescapedString}");

                InvitationResponseModel invitationResponse = JsonSerializer.Deserialize<InvitationResponseModel>(unescapedString);

                return string.IsNullOrEmpty(invitationResponse.TestUrl) == false
                    ? new AssessmentInvitationModel(invitationResponse.TestInvitationId.ToString(), invitationResponse.TestUrl)
                    : throw new HttpRequestException($"Error generating invitation: {response.StatusCode} - {unescapedString}");
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
            return Task.FromResult(new AssessmentChangesModel(invitationCallBack.GetStatus(), invitationCallBack.TestInvitationId.ToString()));
        }

        public async Task<AssessmentResultModel> GetAssessmentResult(string testInvitationId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"reports/{testInvitationId}?reportType=1");
            string responseContent = await response.Content.ReadAsStringAsync();
            string unescapedString = UnescapedString(responseContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error get results: {response.StatusCode} - {unescapedString}");

            TestReportModel result = JsonSerializer.Deserialize<TestReportModel>(unescapedString);
            return new AssessmentResultModel()
            {
                Score = (int)result.ScorePercentage,
                Sections = result.Sections?.Select((section, index) => new AssessmentSectionResultModel() { Score = (int)section.ScorePercentage, SectionNumber = index + 1 }).ToList(),
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
