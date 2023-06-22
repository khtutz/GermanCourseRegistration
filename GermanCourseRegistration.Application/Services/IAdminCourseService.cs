using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IAdminCourseService
{
    Task<CourseResult> GetByIdAsync(Guid id);

    Task<IEnumerable<CourseResult>> GetAllAsync();

    Task<bool> AddAsync(
        string level,
        int part,
        string? description,
        Guid createdBy,
        DateTime createdOn);

    Task<CourseResult> UpdateAsync(
        Guid id,
        string level,
        int part,
        string? description,
        Guid lastModifiedBy,
        DateTime lastModifiedOn);

    Task<CourseResult> DeleteAsync(Guid id);
}
