using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IRegistrationRepository
{
    Task<Registration?> GetByStudentIdAsync(Guid id);

    Task<bool> AddAsync(Registration registration);
}
