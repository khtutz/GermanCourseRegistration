using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.Web.Mappings;
using Microsoft.AspNetCore.Identity;

namespace GermanCourseRegistration.Web.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(
        this IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<GermanCourseAuthDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        services.AddAutoMapper(typeof(AutoMapperProfiles));

        return services;
    }
}
