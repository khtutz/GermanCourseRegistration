using GermanCourseRegistration.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            // Log the exception
            var errorId = Guid.NewGuid();
            logger.LogError(ex, $"{errorId} : {ex.Message}");

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

    private static void HandleDevelopmentErrors(
        HttpContext context, Exception ex, Guid errorId)
    {
        context.Response.Redirect($"/Home/Error?id={errorId}&message={Uri.EscapeDataString(ex.Message)}&stackTrace={Uri.EscapeDataString(ex.StackTrace!)}");
    }

    private static void HandleProductionErrors(HttpContext context)
    {
        context.Response.Redirect("/Home/Error/Production");
    }
}
