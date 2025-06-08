using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureTemplate.API.BuildingBlocks.Controllers;
using CleanArchitectureTemplate.API.DependencyInjections.Extensions;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Application.Features.Identity.Account;

namespace CleanArchitectureTemplate.API.Areas.IdentityArea
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [Area(AreaExtension.IdentityArea)]
    public class AccountController : BaseController
    {
        /// <summary>
        /// Get User Profile Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Profile")]
        public Task<IRequestResult<UserOutput>> Profile()
            => ExecuteQueryAsync(new GetUserProfileQuery());
    }
}
