using GermanCourseRegistration.Application.Interfaces.Repositories;
using GermanCourseRegistration.DataContext;
using GermanCourseRegistration.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.Repositories;

public class CourseRepository
    : Repository<Course, Guid>, ICourseRepository
{
    private readonly GermanCourseRegistrationDbContext dbContext;

    public CourseRepository(GermanCourseRegistrationDbContext dbContext)
        : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Course>> GetByIdsAsync(List<Guid> ids)
    {
        return await dbContext.Courses
            .Where(c => ids.Contains(c.Id))
            .ToListAsync();
    }
}
