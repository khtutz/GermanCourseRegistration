using GermanCourseRegistration.Application.DependencyInjection;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.Repositories.DependencyInjection;
using GermanCourseRegistration.Repositories.Implementations;
using GermanCourseRegistration.Repositories.Interfaces;
using GermanCourseRegistration.Web.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    // DI classes from Application and Repository layers
    builder.Services
        .AddApplication()
        .AddRepository();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // Database contexts injections
    builder.Services.AddDbContext<GermanCourseRegistrationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("GermanCourseRegistrationDbConnectionString")));
    builder.Services.AddDbContext<GermanCourseAuthDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("GermanCourseAuthDbConnectionString")));

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

    // Inject repositories
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    builder.Services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
    builder.Services.AddScoped<ICourseRepository, CourseRepository>();
    builder.Services.AddScoped<ICourseOfferRepository, CourseOfferRepository>();
    builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
    builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
    builder.Services.AddScoped<ICourseMaterialOrderItemRepository, CourseMaterialOrderItemRepository>();
    builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

    // Mappers
    builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

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