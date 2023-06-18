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
        Course? course = null;

        try
        {
            course = await courseRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        return new CourseResult(course);
    }

    public async Task<IEnumerable<CourseResult>> GetAllAsync()
    {
        var courses = Enumerable.Empty<Course>();

        try
        {
            courses = await courseRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

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
        bool isAdded = false;

        var course = new Course()
        {
            Level = level,
            Part = part,
            Description = description,
            CreatedBy = createdBy,
            CreatedOn = createdOn
        };

        try
        {
            isAdded = await courseRepository.AddAsync(course);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

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
        Course? updatedCourse = null;

        try
        {
            updatedCourse = await courseRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        return new CourseResult(updatedCourse);
    }

    public async Task<CourseResult> DeleteAsync(Guid id)
    {
        Course? deletedCourse = null;

        try
        {
            deletedCourse = await courseRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
            WriteLine(ex.StackTrace);
        }

        return new CourseResult(deletedCourse);
    }
}
