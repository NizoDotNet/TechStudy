namespace TechStudy.RazorPages.Helpers.Middlewares;

public class ExceptionHandler : IMiddleware
{
    private readonly ILogger<ExceptionHandler> _logger;
    public ExceptionHandler(ILogger<ExceptionHandler> logger)
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
            _logger.LogError(
                "{Exception} in {Route}", 
                ex.ToString(), 
                context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }

}
