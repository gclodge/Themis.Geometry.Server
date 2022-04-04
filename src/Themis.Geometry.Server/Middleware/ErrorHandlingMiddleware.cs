namespace Themis.Geometry.Server.Middleware;

using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception err)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = GetStatusCode(err);

            var result = JsonSerializer.Serialize(new { message = err?.Message });
            await response.WriteAsync(result);
        }
    }

    static int GetStatusCode(Exception e)
    {
        return e switch
        {
            AppException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError,
        };
    }
}
