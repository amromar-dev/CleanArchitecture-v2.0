using Microsoft.AspNetCore.Mvc;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Commands;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Queries;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.API.BuildingBlocks.Controllers
{
    [ApiController]
    [Route("[area]/[controller]")]
    public class BaseController : ControllerBase
    {
        private IRequestExecution _requestExecution;
        protected IRequestExecution RequestExecution => _requestExecution
            ??= HttpContext.RequestServices.GetService<IRequestExecution>()
            ?? throw new ArgumentNullException(nameof(RequestExecution), "IRequestExecution service not registered");

        /// <summary>
        /// Execute command with return result
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<IRequestResult<TResult>> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
            => RequestExecution.ExecuteAsync(command);

        /// <summary>
        /// Execute query
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public Task<IRequestResult<TResult>> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
            => RequestExecution.QueryAsync(query);

        /// <summary>
        /// Execute command without returned result
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IRequestResult<bool>> ExecuteAsync<TResult>(ICommand command)
        {
            await RequestExecution.ExecuteAsync(command);
            return RequestResult<bool>.SuccessResponse(true);
        }

        /// <summary>
        /// Execute query file result
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<FileResult> ExecuteExportFileAsync(IQuery<RequestFile> query)
        {
            IRequestResult<RequestFile> result = await RequestExecution.QueryAsync(query);

            string dateTimeFormat = result.Data.IncludeTime ? $"{DateTime.UtcNow:yyyy-MM-dd HHmmss}" : $"{DateTime.UtcNow:yyyy-MM-dd}";
            string fullFileName = $"{result.Data.FileName} - {dateTimeFormat}.{result.Data.Type.GetExtension()}";
            
            return File(result.Data.Bytes, result.Data.Type.GetMimeType(), fullFileName);
        }
    }
}
