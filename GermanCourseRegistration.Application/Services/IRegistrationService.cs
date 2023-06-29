using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IRegistrationService
{
    Task<bool> AddAsync(dynamic model);

    Task<RegistrationResult> GetByStudentIdAsync(Guid id);
}
