
using Microsoft.AspNetCore.Mvc;
using SHC.Application.Exceptions;
using SHC.Core.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace SHC.Presentation.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (PatientNotFoundException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Patient not found", ex);
                
            }
            catch (AppointmentOverlapException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Appointment not found", ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Server Error", ex);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, string title, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            var problem = new ProblemDetails
            {
                Status = (int)code,
                Type = "Server Error",
                Title = title,
                Detail = ex.Message
            };

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }

}
