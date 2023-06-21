using GermanCourseRegistration.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GermanCourseRegistration.Repositories.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(
        this IServiceCollection services)
    {
        services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}
