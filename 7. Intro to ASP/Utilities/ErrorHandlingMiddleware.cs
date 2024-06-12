
using System.Net.Mime;
using System.Text.Json;

namespace _7._Intro_to_ASP;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try{
            await next(context);
        }
        catch(Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var serializeErrorResponse = JsonSerializer.Serialize("dada");
            await context.Response.WriteAsync(serializeErrorResponse);
        }
    }
}
