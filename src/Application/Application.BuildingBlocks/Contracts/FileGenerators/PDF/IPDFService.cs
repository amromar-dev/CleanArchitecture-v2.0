namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.PDF
{
    public interface IPDFService
    {
        Task<byte[]> ConvertHTMLToPDF(string html, bool isPortrait = false, string logoBase64 = null);
    }
}
