using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using static IdentityModel.OidcConstants;
using CleanArchitectureTemplate.Domain.Identity.Users;

namespace CleanArchitectureTemplate.Infrastructure.Identity.IdentityServer.Utilities
{
    public class ResourceOwnerPasswordValidator(UserManager<User> userManager, SignInManager<User> signInManager) : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await userManager.FindByEmailAsync(context.UserName);

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, context.Password, false);
            if (result.Succeeded == false)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            try
            {
                user.LogLastLoginDate();
                await userManager.UpdateAsync(user);

                Dictionary<string, object> customResponse = [];

                context.Result = new GrantValidationResult(user.Id.ToString(), AuthenticationMethods.Password, customResponse: customResponse);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
