using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id);

    Task<IEnumerable<Course>> GetByIdsAsync(List<Guid> ids);

    Task<IEnumerable<Course>> GetAllAsync();

    Task<bool> AddAsync(Course course);

    Task<Course?> UpdateAsync(Course course);

    Task<Course?> DeleteAsync(Guid id);
}
