using RazorLight;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML.Attributes;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML.Enums;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileStorage.Interfaces;
using System.Reflection;

namespace CleanArchitectureTemplate.Infrastructure.FileGenerators.HTML
{
    public class RazorRenderService : IHtmlRenderService
    {
        private readonly RazorLightEngine engine;
        private readonly IFileStorage fileStorage;

        public RazorRenderService(IFileStorage fileStorage)
        {
            engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(RazorRenderService))
                .SetOperatingAssembly(typeof(RazorRenderService).Assembly)
                .UseMemoryCachingProvider()
                .Build();

            this.fileStorage = fileStorage;
        }

        public async Task<string> RenderAsync<T>(TemplateType templateKey, T model)
        {
            ReplaceURLs(model);

            string templateName = GetTemplateName(templateKey);

            var result = await engine.CompileRenderAsync(templateName, model);
            var assetUrl = $"{AppDomain.CurrentDomain.BaseDirectory}Assets".Replace("\\", "/");
            return result.Replace("{AssetsUrl}", assetUrl);
        }

        #region Private Methods

        private static string GetTemplateName(TemplateType templateKey)
        {
            return templateKey switch
            {
                TemplateType.CandidateDetailedReport => "Templates.SampleReport",
                _ => throw new NotImplementedException(),
            };
        }

        private void ReplaceURLs<T>(T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            Type type = model.GetType();
            PropertyInfo[] properties = type.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ImageURLAttribute)))
                .ToArray();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string) && property.CanWrite)
                {
                    var currentValue = property.GetValue(model, null)?.ToString();
                    if (currentValue == null)
                        continue;

                    var newValue = ConvertUrlToBase64(currentValue);
                    property.SetValue(model, newValue);
                }
            }
        }

        private string ConvertUrlToBase64(string imageUrl)
        {
            string imageFullURL = Path.Combine(fileStorage.GetBaseURL(), imageUrl);
            if (File.Exists(imageFullURL) == false)
                return string.Empty;

            try
            {
                byte[] imageBytes = File.ReadAllBytes(imageFullURL);
                string base64String = Convert.ToBase64String(imageBytes);
                string mimeType = "image/png"; 

                return $"data:{mimeType};base64,{base64String}";
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
