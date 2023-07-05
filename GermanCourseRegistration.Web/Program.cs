using GermanCourseRegistration.Application.DependencyInjection;
using GermanCourseRegistration.DataContext.DependencyInjection;
using GermanCourseRegistration.Repositories.DependencyInjection;
using GermanCourseRegistration.Web.DependencyInjection;
using GermanCourseRegistration.Web.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    // Logging
    var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File(
            "Logs/GermanCourseRegistration_Log.txt",
            rollingInterval: RollingInterval.Day)
        .MinimumLevel.Information()
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    builder.Services
        .AddWeb()
        .AddApplication()
        .AddRepository()
        .AddDbContext(builder.Configuration);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    //if (!app.Environment.IsDevelopment())
    //{
    //    app.UseExceptionHandler("/Home/Error");
    //    app.UseHsts();
    //}

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}