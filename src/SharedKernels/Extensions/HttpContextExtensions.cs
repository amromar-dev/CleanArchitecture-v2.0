using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.SharedKernels.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetBaseUrl(this IHttpContextAccessor context)
        {
            var request = context.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
}
