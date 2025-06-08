using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity;

namespace CleanArchitectureTemplate.API.BuildingBlocks.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class AuthorizeRoleAttribute(params string[] requiredRoles) : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }

            if (context.HttpContext.User.Identity.IsAuthenticated == false)
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }

            using IServiceScope scope = context.HttpContext.RequestServices.CreateScope();
            IIdentityContext identityContext = scope.ServiceProvider.GetRequiredService<IIdentityContext>();
            List<string> userRoles = identityContext.GetRoles();

            var matchedRoles = userRoles.Intersect(requiredRoles);
            if (matchedRoles.Any() == false)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
