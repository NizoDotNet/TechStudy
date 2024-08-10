
namespace TechStudy.Helpers.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) 
        {
            _logger.LogError("Unknown exception occurred: {exception}", ex);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
