using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id);

    Task<IEnumerable<Course>> GetByIdsAsync(List<Guid> ids);

    Task<IEnumerable<Course>> GetAllAsync();

    Task<Course> AddAsync(Course course);

    Task<Course?> UpdateAsync(Course course);

    Task<Course?> DeleteAsync(Guid id);
}
