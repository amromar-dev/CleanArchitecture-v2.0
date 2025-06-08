using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.FileGenerators.PDF;
using System.Runtime.Loader;

namespace CleanArchitectureTemplate.Infrastructure.FileGenerators.PDF.DependencyInjections
{
    public static class PDFDependencyInjection
    {
        /// <summary>
        /// Registers the pdf services with the dependency injection container.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigurePDF(this IServiceCollection services)
        {
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(AppContext.BaseDirectory, "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton<IPDFService, PDFService>();
        }
    }

    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }
        protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }
    }
}
