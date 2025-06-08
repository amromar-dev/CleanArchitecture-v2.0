using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML.Enums;

namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.HTML
{
    public interface IHtmlRenderService
    {
        Task<string> RenderAsync<T>(TemplateType templateKey, T model);
    }
}
