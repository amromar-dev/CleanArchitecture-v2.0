using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitectureTemplate.API.BuildingBlocks.Attributes;
using CleanArchitectureTemplate.API.BuildingBlocks.Controllers;
using CleanArchitectureTemplate.API.DependencyInjections.Extensions;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Application.Features.SampleDomains;
using CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes;
using CleanArchitectureTemplate.Domain.Identity.Roles.Enums;
using CleanArchitectureTemplate.Domain.SampleDomains.Enums;

namespace CleanArchitectureTemplate.API.Areas.AdministrationArea
{
    /// <summary>
    /// 
    /// </summary>
    [Area(AreaExtension.SampleArea)]
    [Route("[area]/[controller]")]
    [Authorize]
    public class SampleDomainsController : BaseController
    {
        /// <summary>
        /// Get All Sample Domains Paged List
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pagedOptions"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<IRequestResult<PageList<SampleDomainOutput>>> GetAll(string search, [FromQuery] PageOption<SampleDomainSorting> pagedOptions)
            => ExecuteQueryAsync(new GetSampleDomainPagedQuery(search, pagedOptions));

        /// <summary>
        /// Get Sample Domain details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<IRequestResult<SampleDomainOutput>> GetById(int id)
             => ExecuteQueryAsync(new GetSampleDomainByIdQuery(id));

        /// <summary>
        /// Creates a new framework entity.
        /// </summary>
        [HttpPost]
        [AuthorizeRole(SystemRole.SystemAdmin, SystemRole.OrganizationAdmin)]
        public Task<IRequestResult<int>> Create(CreateSampleDomainCommand command)
          => ExecuteCommandAsync(command);

        /// <summary>
        /// Export Sample Domains
        /// </summary>
        /// <returns></returns>
        [HttpGet("Export")]
        public Task<FileResult> Export()
            => ExecuteExportFileAsync(new ExportSampleDomainsQuery());
    }
}
