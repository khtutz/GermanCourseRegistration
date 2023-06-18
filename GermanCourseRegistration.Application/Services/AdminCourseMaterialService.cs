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
        var courseMaterials = Enumerable.Empty<CourseMaterial>();

        try
        {
            courseMaterials = await courseMaterialRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        var results = new List<CourseMaterialResult>();

        foreach (var courseMaterial in courseMaterials)
        {
            results.Add(new CourseMaterialResult(courseMaterial));
        }

        return results;
    }

    public async Task<CourseMaterialResult> AddAsync(
        string name, 
        string description, 
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

        bool isAdded = false;

        try
        {
            isAdded = await courseMaterialRepository.AddAsync(courseMaterial);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        return new CourseMaterialResult(courseMaterial);
    }

    public async Task<CourseMaterialResult> UpdateAsync(
        Guid id, 
        string name, 
        string description, 
        string category, 
        decimal price, 
        Guid lastModifiedBy, 
        DateTime lastModifiedOn)
    {
        CourseMaterial? courseMaterial = null;

        try
        {
            courseMaterial = await courseMaterialRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        return new CourseMaterialResult(courseMaterial);
    }

    public async Task<CourseMaterialResult> DeleteAsync(Guid id)
    {
        CourseMaterial? deletedCourseMaterial = null;

        try
        {
            deletedCourseMaterial = await courseMaterialRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        return new CourseMaterialResult(deletedCourseMaterial);
    }
}
