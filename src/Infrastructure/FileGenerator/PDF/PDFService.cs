using PuppeteerSharp;
using PuppeteerSharp.Media;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.PDF;

namespace CleanArchitectureTemplate.Infrastructure.FileGenerators.PDF
{
    public class PDFService() : IPDFService
    {
        public async Task<byte[]> ConvertHTMLToPDF(string html, bool isPortrait = false, string logoBase64 = null)
        {
            await new BrowserFetcher().DownloadAsync();

            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html, new NavigationOptions
            {
                WaitUntil = [WaitUntilNavigation.Networkidle0]
            });

            string headerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "header.html");
            string headerHtml = File.Exists(headerPath) ? File.ReadAllText(headerPath) : string.Empty;

            string footerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "footer.html");
            string footerHtml = File.Exists(footerPath) ? File.ReadAllText(footerPath) : string.Empty;

            if (string.IsNullOrEmpty(logoBase64) == false)
            {
                headerHtml = headerHtml.Replace("{{logo}}", logoBase64);
                footerHtml = footerHtml.Replace("{{logo}}", logoBase64);
            }

            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "70px",
                    Bottom = "40px",
                },
                Landscape = !isPortrait,
                //OmitBackground = true,
                DisplayHeaderFooter = !string.IsNullOrEmpty(footerHtml),
                FooterTemplate = !string.IsNullOrEmpty(footerHtml) ? footerHtml : null,
                HeaderTemplate = !string.IsNullOrEmpty(headerHtml) ? headerHtml : null,
            });

            return pdfBytes;
        }
    }
}
