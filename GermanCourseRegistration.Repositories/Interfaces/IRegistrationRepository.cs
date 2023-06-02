using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface IRegistrationRepository
{
    Task<Registration?> GetByStudentIdAsync(Guid id);

    Task<Registration> AddAsync(Registration registration);
}
