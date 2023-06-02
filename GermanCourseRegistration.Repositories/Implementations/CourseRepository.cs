using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using GermanCourseRegistration.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories.Implementations;

public class CourseRepository : ICourseRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public CourseRepository(GermanCourseRegistrationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Course>> GetByIdsAsync(List<Guid> ids)
    {
        return await dbContext.Courses
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await dbContext.Courses.ToListAsync();
    }

    public async Task<Course> AddAsync(Course course)
    {
        await dbContext.AddAsync(course);
        await dbContext.SaveChangesAsync();

        return course;
    }

    public async Task<Course?> UpdateAsync(Course course)
    {
        var existingCourse = await dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == course.Id);

        if (existingCourse == null) return null;

        existingCourse.Level = course.Level;
        existingCourse.Part = course.Part;
        existingCourse.Description = course.Description;
        existingCourse.LastModifiedBy = course.LastModifiedBy;
        existingCourse.LastModifiedOn = course.LastModifiedOn;

        await dbContext.SaveChangesAsync();

        return existingCourse;
    }

    public async Task<Course?> DeleteAsync(Guid id)
    {
        var existingCourse = await dbContext.Courses.FindAsync(id);

        if (existingCourse != null)
        {
            dbContext.Courses.Remove(existingCourse);
            await dbContext.SaveChangesAsync();
        }

        return existingCourse;
    }
}
