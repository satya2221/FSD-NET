using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace _7._Intro_to_ASP;

public class GlobalExceptionHandlerCustom : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, message) = MapException(exception);
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var serializeErrorResponse = JsonSerializer.Serialize(new MessageResponseDto(statusCode,message));
        await httpContext.Response.WriteAsync(serializeErrorResponse);

        return true;
    }

    private static (int StatusCode, string Message) MapException(Exception exception)
    {
        return exception switch{
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
            NullReferenceException => (StatusCodes.Status404NotFound, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server occured. Please contact Admin")
        };
    }
}
