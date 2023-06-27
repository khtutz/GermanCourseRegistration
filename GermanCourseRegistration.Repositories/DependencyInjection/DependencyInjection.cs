using GermanCourseRegistration.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GermanCourseRegistration.Repositories.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(
        this IServiceCollection services)
    {
        services.AddScoped<ICourseMaterialOrderItemRepository, CourseMaterialOrderItemRepository>();
        services.AddScoped<ICourseMaterialRepository, CourseMaterialRepository>();
        services.AddScoped<ICourseOfferRepository, CourseOfferRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITimetableRepository, TimetableRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return services;
    }
}
