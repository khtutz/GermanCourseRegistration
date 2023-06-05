using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Repositories.Interfaces;

public interface ICourseMaterialRepository
{
    Task<CourseMaterial?> GetByIdAsync(Guid id);

    Task<IEnumerable<CourseMaterial>> GetAllAsync();

    Task<bool> AddAsync(CourseMaterial courseMaterial);

    Task<CourseMaterial?> UpdateAsync(CourseMaterial courseMaterial);

    Task<CourseMaterial?> DeleteAsync(Guid id);
}
