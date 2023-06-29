using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface IRegistrationRepository : IRepository<Registration, Guid> 
{
    Task<Registration?> GetByStudentIdAsync(Guid id);
}
