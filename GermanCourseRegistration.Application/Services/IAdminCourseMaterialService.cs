using GermanCourseRegistration.Application.ServiceResults;

namespace GermanCourseRegistration.Application.Services;

public interface IAdminCourseMaterialService
{
    Task<CourseMaterialResult> GetByIdAsync(Guid id);

    Task<IEnumerable<CourseMaterialResult>> GetAllAsync();

    Task<bool> AddAsync(
        string name,
        string? description,
        string category,
        decimal price,
        Guid createdBy,
        DateTime createdOn);

    Task<CourseMaterialResult> UpdateAsync(
        Guid id,
        string name,
        string? description,
        string category,
        decimal price,
        Guid lastModifiedBy,
        DateTime lastModifiedOn);

    Task<CourseMaterialResult> DeleteAsync(Guid id);

    IEnumerable<string> GetCourseMaterialCategories();
}
