using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using CleanArchitectureTemplate.SharedKernels.Exceptions.Base;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results;
using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.Application.BuildingBlocks.Executions.Results.Errors;

namespace CleanArchitectureTemplate.API.Middlewares
{
    /// <summary>
    /// Middleware to handle exceptions and return appropriate HTTP responses.
    /// </summary>
    /// <remarks>
    /// This middleware captures and handles specific exceptions (e.g., FieldsValidationException, NotFoundException, BaseException)
    /// and returns corresponding error responses to the client.
    /// </remarks>
    /// <param name="next">Delegate to call the next middleware in the pipeline.</param>
    /// <param name="hostEnvironment">Provides information about the hosting environment the application is running in.</param>
    public class ExceptionMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (FieldsValidationException ex) 
            {
                await HandleFieldsValidationException(context, ex);
            }
            catch (NotFoundException ex) 
            {
                await HandleNotFoundException(context, ex);
            }
            catch (BaseException ex)
            {
                await HandleBaseException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleOtherException(context, ex);
            }
        }

        #region Private Methods

        private static async Task HandleFieldsValidationException(HttpContext context, FieldsValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var failure = new RequestValidationError(ex.Message, ex.ExceptionCode, ex.Validations);
            var response = RequestResult<RequestValidationError>.ErrorResponse(failure);

            await context.Response.WriteAsync(ToJson(response));
        }

        private static async Task HandleNotFoundException(HttpContext context, NotFoundException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            var failure = new RequestError(ex.Message, ex.ExceptionCode);
            var response = RequestResult<RequestError>.ErrorResponse(failure);

            await context.Response.WriteAsync(ToJson(response));
        }

        private static async Task HandleBaseException(HttpContext context, BaseException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var failure = new RequestError(ex.Message, ex.ExceptionCode);
            var response = RequestResult<RequestError>.ErrorResponse(failure);

            await context.Response.WriteAsync(ToJson(response));
        }
        
        private async Task HandleOtherException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = hostEnvironment.IsProduction() ? HttpStatusCode.InternalServerError.ToString() : ex.Message;
            var failure = new RequestError(error, (int)HttpStatusCode.InternalServerError);
            var response = RequestResult<RequestError>.ErrorResponse(failure);

            await context.Response.WriteAsync(ToJson(response));
        }

        private static string ToJson<T>(T response)
        {
            return JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            });
        } 
        #endregion
    }
}
