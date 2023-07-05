using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GermanCourseRegistration.DataContext.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GermanCourseRegistrationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(
                "GermanCourseRegistrationDbConnectionString")));

        services.AddDbContext<GermanCourseAuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(
                "GermanCourseAuthDbConnectionString")));

        return services;
    }
}
