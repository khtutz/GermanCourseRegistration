using GermanCourseRegistration.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GermanCourseRegistration.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAdminCourseMaterialService, AdminCourseMaterialService>();

        return services;
    }
}
