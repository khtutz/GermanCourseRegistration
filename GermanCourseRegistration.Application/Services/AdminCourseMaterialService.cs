using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class AdminCourseMaterialService : IAdminCourseMaterialService
{
    private readonly ICourseMaterialRepository courseMaterialRepository;

    public AdminCourseMaterialService(ICourseMaterialRepository courseMaterialRepository)
    {
        this.courseMaterialRepository = courseMaterialRepository;
    }

    public Task<CourseMaterialResult> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CourseMaterialResult>> GetAllAsync()
    {
        var courseMaterials = await courseMaterialRepository.GetAllAsync();

        var courseMaterialResults = courseMaterials.Select(cm => new CourseMaterialResult(cm));

        return courseMaterialResults;
    }

    public async Task<bool> AddAsync(
        string name, 
        string? description, 
        string category, 
        decimal price, 
        Guid createdBy, 
        DateTime createdOn)
    {
        var courseMaterial = new CourseMaterial()
        {
            Name = name,
            Description = description,
            Category = category,
            Price = price,
            CreatedBy = createdBy,
            CreatedOn = createdOn
        };

        bool isAdded = await courseMaterialRepository.AddAsync(courseMaterial);

        return isAdded;
    }

    public async Task<CourseMaterialResult> UpdateAsync(
        Guid id, 
        string name, 
        string? description, 
        string category, 
        decimal price, 
        Guid lastModifiedBy, 
        DateTime lastModifiedOn)
    {
        CourseMaterial? courseMaterial = await courseMaterialRepository.GetByIdAsync(id);

        return new CourseMaterialResult(courseMaterial);
    }

    public async Task<CourseMaterialResult> DeleteAsync(Guid id)
    {
        CourseMaterial? deletedCourseMaterial = await courseMaterialRepository.DeleteAsync(id);

        return new CourseMaterialResult(deletedCourseMaterial);
    }

    public IEnumerable<string> GetCourseMaterialCategories()
    {
        var categories = new List<string>()
        {
            CourseMaterial.PhysicalCopyCategory,
            CourseMaterial.DigitalCopyCategory,
            CourseMaterial.AudioCopyCategory
        };

        return categories;
    }
}
