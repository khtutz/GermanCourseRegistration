namespace GermanCourseRegistration.Application.Services;

public interface IRegistrationService
{
    Task<bool> AddAsync(dynamic model);
}
