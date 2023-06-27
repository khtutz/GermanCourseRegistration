using GermanCourseRegistration.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GermanCourseRegistration.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAdminCourseMaterialService, AdminCourseMaterialService>();
        services.AddScoped<IAdminCourseScheduleService, AdminCourseScheduleService>();
        services.AddScoped<IAdminCourseService, AdminCourseService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
