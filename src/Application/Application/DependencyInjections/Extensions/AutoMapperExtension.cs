using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;

namespace CleanArchitectureTemplate.Application.DependencyInjections.Extensions
{
    public static class AutoMapperExtension
    {
        /// <summary>
        /// Configure Automapper lifetime DI
        /// </summary>
        /// <param name="services"></param>
        internal static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }

    public class AutoMapperGenericConfigurations : Profile
    {
        /// <summary>
        /// Configurate generic classes
        /// </summary>
        public AutoMapperGenericConfigurations()
        {
            CreateMap(typeof(PageList<>), typeof(PageList<>));
        }
    }
}
