using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.Application.ServiceResults;
using GermanCourseRegistration.EntityModels;

namespace GermanCourseRegistration.Application.Services;

public class AdminCourseService : IAdminCourseService
{
    private readonly ICourseRepository courseRepository;

    public AdminCourseService(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }

    public async Task<CourseResult> GetByIdAsync(Guid id)
    {
        Course? course = await courseRepository.GetByIdAsync(id);

        return new CourseResult(course);
    }

    public async Task<IEnumerable<CourseResult>> GetAllAsync()
    {
        var courses = await courseRepository.GetAllAsync();

        var courseResults = courses.Select(c => new CourseResult(c));

        return courseResults;
    }

    public async Task<bool> AddAsync(
        string level, 
        int part, 
        string description, 
        Guid createdBy, 
        DateTime createdOn)
    {   
        var course = new Course()
        {
            Level = level,
            Part = part,
            Description = description,
            CreatedBy = createdBy,
            CreatedOn = createdOn
        };

        bool isAdded = await courseRepository.AddAsync(course);

        return isAdded;
    }

    public async Task<CourseResult> UpdateAsync(
        Guid id, 
        string level, 
        int part, 
        string description, 
        Guid lastModifiedBy, 
        DateTime lastModifiedOn)
    {
        Course? updatedCourse = await courseRepository.GetByIdAsync(id);

        return new CourseResult(updatedCourse);
    }

    public async Task<CourseResult> DeleteAsync(Guid id)
    {
        Course? deletedCourse = await courseRepository.DeleteAsync(id);

        return new CourseResult(deletedCourse);
    }
}
