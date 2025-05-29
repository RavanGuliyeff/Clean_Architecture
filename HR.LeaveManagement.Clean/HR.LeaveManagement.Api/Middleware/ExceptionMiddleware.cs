using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace HR.LeaveManagement.Api.Middleware;

public class ExceptionMiddleware
{
    readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequest:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails()
                {
                    Title = badRequest.Message,
                    Status = (int)statusCode,
                    Detail = badRequest.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequest.ValidationErrors
                };
                break;

            case Exception notFoundEx when ex.GetType().IsGenericType &&
                             ex.GetType().GetGenericTypeDefinition() == typeof(NotFoundException<>):
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails()
                {
                    Title = notFoundEx.Message,
                    Status = (int)statusCode,
                    Detail = notFoundEx.InnerException?.Message,
                    Type = notFoundEx.GetType().Name,
                };
                break;

            default:
                problem = new CustomProblemDetails()
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Detail = ex.StackTrace,
                    Type = nameof(HttpStatusCode.InternalServerError),
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
