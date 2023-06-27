namespace GermanCourseRegistration.Web.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly IWebHostEnvironment environment;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionHandlerMiddleware(
        IWebHostEnvironment environment,
        ILogger<ExceptionHandlerMiddleware> logger,
        RequestDelegate next)
    {
        this.environment = environment;
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid();
            logger.LogError(ex, $"{errorId} :\n {ex.Message}\n{ex.StackTrace}\n");

            if (environment.IsDevelopment())
            {
                HandleDevelopmentErrors(context, ex, errorId);
            }
            else
            {
                HandleProductionErrors(context);
            }   
        }
    }

    private void HandleDevelopmentErrors(
        HttpContext context, Exception ex, Guid errorId)
    {
        context.Response.Redirect($"/Home/Error?id={errorId}");
    }

    private void HandleProductionErrors(HttpContext context)
    {
        context.Response.Redirect("/Home/Error/Production");
    }
}
