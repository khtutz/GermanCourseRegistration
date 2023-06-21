using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ICourseRepository : IRepository<Course, Guid>
{
    Task<IEnumerable<Course>> GetByIdsAsync(List<Guid> ids);
}
