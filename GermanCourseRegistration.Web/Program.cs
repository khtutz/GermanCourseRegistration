using GermanCourseRegistration.Application.DependencyInjection;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.DataContext.DependencyInjection;
using GermanCourseRegistration.Repositories.DependencyInjection;
using GermanCourseRegistration.Web.Mappings;
using GermanCourseRegistration.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
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
        .AddApplication()
        .AddRepository()
        .AddDbContexts(builder.Configuration);

    builder.Services.AddControllersWithViews();

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<GermanCourseAuthDbContext>();

    // Register password settings
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    });

    builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
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